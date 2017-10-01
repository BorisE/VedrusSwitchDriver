using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using System.Web.Script.Serialization;

using ASCOM;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;


namespace ASCOM.Vedrus
{

    // Получить данные
    //      ПО ВСЕМ:
    //      http://192.168.2.199/power/get/
    //      ответ: {"boris_pc":1,"boris_scope":0,"roman_pc":0,"roman_scope":0}
    //
    //      ПО КОНКРЕТНОМУ:
    //      http://192.168.2.199/relay.php?channel=boris_scope
    //      ответ: {"status":200,"state":0,"channel":"boris_scope"}
    //  
    // Протокол на изменение:
    //      post на http://192.168.2.199/relay.php
    //      channel: boris_scope
    //      state: 1 или 0
    //      channel "boris_pc" - это самоубийство компу :)


    /// <summary>
    /// Class for working with Vedrus device
    /// </summary>
    public class Web_switch_hardware_class
    {
        internal bool debugFlag = false;

        /// <summary>
        /// Used to test hardaware connection lost failure
        /// </summary>
        internal bool EmulateConnectionLostFlag=false;
        internal Int32 EmulateConnectionLostProbability = 3; //rand(1,EmulateConnectionLostProbability)


        public string ip_addr, ip_port, ip_login, ip_pass;

        public string channel1_name = "boris_scope" , channel2_name = "boris_pc";

        /// <summary>
        /// Temp output sensors state
        /// </summary>
        public List<switchDataRawClass> SWITCH_DATA_LIST = new List<switchDataRawClass>();


        /// <summary>
        /// connected?
        /// </summary>
        public bool hardware_connected_flag = false;

        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        public static TraceLogger tl, tlsem;

        /// <summary>
        /// Semaphor for blocking concurrent requests
        /// </summary>
        public static Semaphore VedrusSemaphore;

        /// <summary>
        /// error message (on hardware level) - don't forget, that there is another one on driver level
        /// all this done for saving error message text during exception and display it to user (MaximDL tested)
        /// </summary>
        public string ASCOM_ERROR_MESSAGE = "";

        //CACHING
        public static DateTime EXPIRED_CACHE = new DateTime(2010, 05, 12, 13, 15, 00); //CONSTANT FOR MARKING AN OLD TIME
        //Caching connection check
        private DateTime lastConnectedCheck = EXPIRED_CACHE; //when was the last hardware checking provided for connect state
        public static int CACHE_CONNECTED_CHECK_MAX_INTERVAL = 20; //how often to held hardware checking (in seconds)
        //Caching output read
        private DateTime lastOutputReadCheck = EXPIRED_CACHE; //when was the last hardware checking provided for connect state
        public static int CACHE_OUTPUT_MAX_INTERVAL = 2; //how often to held hardware checking (in seconds)

        /// <summary>
        /// Constructor of Web_switch_hardware_class
        /// </summary>
        public Web_switch_hardware_class(bool traceState)
        {
            tl = new TraceLogger("", "Vedrus_Hardware");
            tl.Enabled = traceState; //now we can set trace state, specified by user

            tlsem = new TraceLogger("", "Vedrus_Switch_hardware_semaphore");
            tlsem.Enabled = true;


            tl.LogMessage("Switch_constructor", "Starting initialisation");

            hardware_connected_flag = false;

            VedrusSemaphore = new Semaphore(1, 2, "VedrusSwitch");

            tl.LogMessage("Switch_constructor", "Exit");
        }

        /// <summary>
        /// This method is used to connect to IP device
        /// </summary>
        public void Connect()
        {
            tl.LogMessage("Switch_Connect", "Enter");

            //reset cache
            clearCache();

            //if current state of connection coincidies with new state then do nothing
            if (hardware_connected_flag)
            {
                tl.LogMessage("Switch_Connect", "Exit because of no state change");
                return;
            }

            // check (forced) if there is connection with hardware
            if (IsHardwareReachable(true))
            {
                tl.LogMessage("Switch_Connect", "Connected");
                return;
            }
            else
            {
                tl.LogMessage("Switch_Connect", "Couldn't connect to Vedrus control device on [" + ip_addr + "]");
                ASCOM_ERROR_MESSAGE = "Couldn't connect to Vedrus control device on [" + ip_addr + "]";
                //throw new ASCOM.DriverException(ASCOM_ERROR_MESSAGE);

            }
            tl.LogMessage("Switch_Connect", "Exit");
        }

        // if we need to ESTABLISH CONNECTION
        /// <summary>
        /// Use this method to disconnect from IP device
        /// </summary>
        public void Disconnect()
        {
            tl.LogMessage("Switch_Disconnect", "Enter");

            //reset cache
            clearCache();

            //if already disonnected then do nothing
            if (!hardware_connected_flag)
            {
                tl.LogMessage("Switch_Disconnect", "Was not connected");
                return;
            }
            else
            { 
                // Change flag to not connected
                hardware_connected_flag = false;
                tl.LogMessage("Switch_Disconnect", "Exit");
            }
        }

        /// <summary>
        /// Check if device is available
        /// </summary>
        /// <param name="forcedflag">[bool] if function need to force noncached checking of device availability</param>
        /// <returns>true is available, false otherwise</returns>
        public bool IsHardwareReachable(bool forcedflag = false)
        {
            tl.LogMessage("Switch_IsHardareReachable", "Enter (forced flag=" + forcedflag.ToString()+")");

            //Check - if forced mode? (=no cache, no async)
            if (forcedflag)
            {
                hardware_connected_flag = false;
                checkLink_forced();
            }
            else
            {
            //Usual mode
                
                //Measure how much time have passed since last HARDWARE measure
                TimeSpan passed = DateTime.Now - lastConnectedCheck;
                if (passed.TotalSeconds > CACHE_CONNECTED_CHECK_MAX_INTERVAL)
                {
                    // check that the driver hardware connection exists and is connected to the hardware
                    tl.LogMessage("Switch_IsConnected", String.Format("Using cached value but starting background read [in cache was: {0}s]...", passed.TotalSeconds));

                    // reset cache. Note that this check inserted here not in DownloadComlete. 
                    // This is because I am afraid of long query wait which will force to produce many queries to IP Device. 
                    // Should move to timeout check (but don't know how)
                    lastConnectedCheck = DateTime.Now;

                    //read
                    //checkLink_async();
                    checkLink_forced(); //use forced variant for this build
                }
                else
                {
                    // do nothing, use previos value
                    tl.LogMessage("Switch_IsConnected", "Using cached value [in cache:" + passed.TotalSeconds + "s]");
                }
            }
            tl.LogMessage("Switch_IsConnected", "Exit. Return value: " + hardware_connected_flag);
            return hardware_connected_flag;
        }


        /// <summary>
        /// Check the availability of IP server by starting async read from input sensors. 
        /// Not used in this build
        /// </summary>
        internal void ___checkLink_async______()
        {
            tl.LogMessage("CheckLink_async", "Enter (thread" + Thread.CurrentThread.ManagedThreadId + ")");

            //Check - address was specified?
            if (string.IsNullOrEmpty(ip_addr))
            {
                hardware_connected_flag = false;
                tl.LogMessage("CheckLink_async", "(thread" + Thread.CurrentThread.ManagedThreadId + ") ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                //throw new ASCOM.DriverException("checkLink_async error");
                return;
            }

            string siteipURL;

            // Vedrus style
            // http://192.168.2.199/power/get/
            // {"boris_pc":1,"boris_scope":0,"roman_pc":0,"roman_scope":0}
            siteipURL = "http://" + ip_addr + ":" + ip_port + "/power/get/";


            //FOR DEBUGGING
            if (debugFlag)
            {
                siteipURL = "http://localhost/power/get.php";
            }
            Uri uri_siteipURL = new Uri(siteipURL);
            tl.LogMessage("CheckLink_async", "(thread" + Thread.CurrentThread.ManagedThreadId + ") download url:" + siteipURL);

            // Send http query
            MyWebClient client = new MyWebClient();
            try
            {
                //tl.LogMessage("Semaphore", "WaitOne");
                tlsem.LogMessage("checkLink_async", "WaitOne (thread"+Thread.CurrentThread.ManagedThreadId+")");
                VedrusSemaphore.WaitOne(); // lock working with IP9212
                tlsem.LogMessage("checkLink_async", "WaitOne passed (thread" + Thread.CurrentThread.ManagedThreadId + ")");

                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(___checkLink_DownloadCompleted);
                client.DownloadDataAsync(uri_siteipURL);

                tl.LogMessage("CheckLink_async", "(thread" + Thread.CurrentThread.ManagedThreadId + ") http request was sent");
            }
            catch (Exception e)
            {
                //tl.LogMessage("Semaphore", "Release");
                VedrusSemaphore.Release();//unlock ip9212 device for others
                tlsem.LogMessage("checkLink_async", "(thread" + Thread.CurrentThread.ManagedThreadId + ") Release on exception");
            
                hardware_connected_flag = false;

                tl.LogMessage("CheckLink_async", "(thread" + Thread.CurrentThread.ManagedThreadId + ") error:" + ((WebException)e).Status);
                tl.LogMessage("CheckLink_async", "(thread" + Thread.CurrentThread.ManagedThreadId + ") exit on web error");

                return;
            }                
             
        }

        /// <summary>
        /// Event hadler for async download (checkLink_async)
        /// Not used in this build
        /// </summary>
        internal void ___checkLink_DownloadCompleted(Object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                tl.LogMessage("checkLink_DownloadCompleted", "Download complete");
            }
            catch { 
            // Object was disposed before download complete, so we should release all and exit
                return;
            }

            VedrusSemaphore.Release();//unlock ip9212 device for others
            //tl.LogMessage("Semaphore", "Release");
            tlsem.LogMessage("checkLink_DownloadCompleted", "Release");

            if (e.Error != null)
            {
                hardware_connected_flag = false;
                tl.LogMessage("checkLink_DownloadCompleted", "error: " + e.Error.Message);
                return;
            }

            if (e.Result != null && e.Result.Length > 0)
            {
                string downloadedData = Encoding.Default.GetString(e.Result);
                
                //check for integrity
                if (downloadedData.IndexOf(channel1_name) >= 0)
                {
                    hardware_connected_flag = true;
                    tl.LogMessage("checkLink_DownloadCompleted", "ok");
                }
                else
                {
                    hardware_connected_flag = false;
                    tl.LogMessage("checkLink_DownloadCompleted", "string not found");
                }

                //////////////////////////////////////////////////////////////
                //EmulateConnectionLost
                if (EmulateConnectionLostFlag)
                {
                    Random rand = new Random(DateTime.Now.Millisecond);
                    int flag = rand.Next(1, EmulateConnectionLostProbability);
                    if (flag == 1)
                    {
                        hardware_connected_flag = false;
                    }
                }
                //////////////////////////////////////////////////////////////

            }
            else
            {
                tl.LogMessage("checkLink_DownloadCompleted", "bad result");
                hardware_connected_flag = false;
            }
            return;
        }

        /// <summary>
        /// Check the availability of IP server by straight read (NON ASYNC manner)
        /// </summary>  
        /// <returns>Aviability of IP server </returns> 
        public bool checkLink_forced()
        {
            tl.LogMessage("Switch_checkLink_forced", "Enter");

            //Just call getOutputStatus() method. It would check and also parse output data as a side bonus :)
            getOutputStatus(); 

            tl.LogMessage("Switch_checkLink_forced", "Exit. Returning status: " + hardware_connected_flag.ToString());
            return hardware_connected_flag;
        }

        /// <summary>
        /// Get output sensor status
        /// </summary>
        /// <returns>Returns bool TRUE or FALSE</returns> 
        public bool? getOutputSwitchStatus(int SwitchId, bool forcedflag = false)
        {
            tl.LogMessage("getOutputSwitchStatus", "Enter (" + SwitchId+")");

            //Measure how much time have passed since last HARDWARE input reading
            TimeSpan passed = DateTime.Now - lastOutputReadCheck;
            if (forcedflag || passed.TotalSeconds > CACHE_OUTPUT_MAX_INTERVAL)
            {
                // Read output data for ALL SWITCHES
                tl.LogMessage("getOutputSwitchStatus", String.Format("Cached expired, read hardware values [in cache was: {0}s]...", passed.TotalSeconds));
                getOutputStatus();

                // Reset read cache moment
                lastOutputReadCheck = DateTime.Now;
            }
            else
            {
                // use previos value
                tl.LogMessage("getOutputSwitchStatus", "Using cached values [in cache:" + passed.TotalSeconds + "s]");
            }

            ///////////////
            bool? curSwitchState = null;
            if (SWITCH_DATA_LIST.Count() < (SwitchId+1))
            {
                curSwitchState = null;
            }
            else
            {
                curSwitchState = (SWITCH_DATA_LIST[SwitchId].Val == 1);
            }
            
            tl.LogMessage("getOutputSwitchStatus", "getOutputSwitchStatus(" + SwitchId + "):" + curSwitchState);

            tl.LogMessage("getOutputSwitchStatus", "Exit");
            return curSwitchState;
        }



        /// <summary>
        /// Get output relay status
        /// </summary>
        /// <returns>Returns int array [0..8] with status flags of each realya status. arr[0] is for read status (-1 for error, 1 for good read, 0 for smth else)</returns> 
        public bool getOutputStatus()
        {
            tl.LogMessage("getOutputStatus", "Enter");

            // get the ip9212 settings from the profile
            //readSettings();

            if (string.IsNullOrEmpty(ip_addr))
            {
                tl.LogMessage("getOutputStatus", "ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "getOutputStatus(): no IP address was specified";
                SWITCH_DATA_LIST.Clear();
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return input_state_arr;
            }


            string siteipURL;
            if (debugFlag)
            {
            //FOR DEBUGGING
                siteipURL = "http://localhost/power/get.php";
            }
            else
            {
            // Vedrus style
                // http://192.168.2.199/power/get/
                // {"boris_pc":1,"boris_scope":0,"roman_pc":0,"roman_scope":0}
                siteipURL = "http://" + ip_addr + ":" + ip_port + "/power/get/";
            }
            tl.LogMessage("getOutputStatus", "Download url:" + siteipURL);


            // Send http query
            tlsem.LogMessage("getOutputStatus", "WaitOne");
            VedrusSemaphore.WaitOne(); // lock working with IP9212

            string s = "";
            MyWebClient client = new MyWebClient();
            try
            {
                Stream data = client.OpenRead(siteipURL);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();

                VedrusSemaphore.Release();//unlock ip9212 device for others
                tlsem.LogMessage("getOutputStatus", "Release");

                //Bonus: checkconnection
                hardware_connected_flag = true;
                lastConnectedCheck = DateTime.Now;

                tl.LogMessage("getOutputStatus", "Download str:" + s);
            }
            catch (Exception e)
            {
                VedrusSemaphore.Release();//unlock ip9212 device for others
                tlsem.LogMessage("getOutputStatus", "Release on WebException");

                //Bonus: checkconnection
                hardware_connected_flag = false;
                SWITCH_DATA_LIST.Clear();

                tl.LogMessage("getOutputStatus", "Error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "getInputStatus(): Couldn't reach network server";
                //throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                Trace("> IP9212_harware.getOutputStatus(): exit by web error");
                tl.LogMessage("getOutputStatus", "Exit by web error");

                return false; //error
            }

            // Parse data
            //{ "boris_pc":1,"boris_scope":0,"roman_pc":0,"roman_scope":0}
            try
            {
                //Deserialize into dictionary
                Dictionary<string, Int16> relaysRead = new Dictionary<string, Int16>();
                var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                relaysRead = jsSerializer.Deserialize<Dictionary<string, Int16>>(s);

                //Read into LIST with SWITCH values
                List<switchDataRawClass> tempSwitchData = new List<switchDataRawClass>();

                SWITCH_DATA_LIST.Clear();
                foreach (KeyValuePair<string, Int16> rel in relaysRead)
                {
                    SWITCH_DATA_LIST.Add(new switchDataRawClass() { Name = rel.Key, Val = rel.Value });
                }

                lastOutputReadCheck = DateTime.Now; //mark cache was renewed
                tl.LogMessage("getOutputStatus", "Data was read");
            }
            catch (Exception ex)
            {
                SWITCH_DATA_LIST.Clear();
                tl.LogMessage("getOutputStatus", "ERROR parsing data (Exception: " + ex.Message + ")!");
                tl.LogMessage("getOutputStatus", "exit by parse error");
                return false; //error
            }
            return true;
        }

        public bool setOutputStatus(int PortIndex, bool bPortValue)
        {
            tl.LogMessage("setOutputStatus", "Enter (" + PortIndex + "," + bPortValue + ")");

            //get channel name
            string ChannelName = Switch.SwitchData[PortIndex].Name;

            return setOutputStatus(ChannelName, bPortValue);
        }

        /// <summary>
        /// Chage output relay state
        /// </summary>
        /// <param name="PortNumber">Relay port number, int [1..9]</param>
        /// <param name="bPortValue">Port value flase = 0, true = 1</param>
        /// <returns>Returns true in case of success</returns> 
        public bool setOutputStatus(string PortName, bool bPortValue)
        {
            tl.LogMessage("setOutputStatus", "Enter (" + PortName + "," + bPortValue + ")");

            //convert port value to int
            int intPortValue = (bPortValue ? 1 : 0);


            //return data
            bool ret = false;

            if (string.IsNullOrEmpty(ip_addr))
            {
                tl.LogMessage("setOutputStatus", "ERROR (ip_addr wasn't set)!");
                // report a problem with the port name
                ASCOM_ERROR_MESSAGE = "setOutputStatus(): no IP address was specified";
                throw new ASCOM.ValueNotSetException(ASCOM_ERROR_MESSAGE);
                //return ret;
            }

            string paramString = "channel=" + PortName + "&state=" + intPortValue;
            string siteipURL;
            if (debugFlag)
            {
                //FOR DEBUGGING
                siteipURL = "http://localhost/power/set.php";
            }
            else
            {
                // Vedrus style
                // http://192.168.2.199/relay.php
                // {"boris_pc":1,"boris_scope":0,"roman_pc":0,"roman_scope":0}
                siteipURL = "http://" + ip_addr + ":" + ip_port + "/relay.php";
            }
            tl.LogMessage("setOutputStatus", "Download url:" + siteipURL);
            tl.LogMessage("setOutputStatus", "Param String:" + paramString);

            
            // Send http query
            tlsem.LogMessage("setOutputStatus", "WaitOne"); 
            VedrusSemaphore.WaitOne(); // lock working with IP9212
            tlsem.LogMessage("setOutputStatus", "WaitOne passed");
            string s = "";
            MyWebClient client = new MyWebClient();
            try
            {
                s = client.UploadPOST(siteipURL, paramString);

                VedrusSemaphore.Release();//unlock ip9212 device for others
                tlsem.LogMessage("setOutputStatus", "Release");

                tl.LogMessage("setOutputStatus", "Download str:" + s);

                ret = true;
            }
            catch (Exception e)
            {
                VedrusSemaphore.Release();//unlock ip9212 device for others
                tlsem.LogMessage("setOutputStatus", "Release on WebException");

                ret = false;

                tl.LogMessage("setOutputStatus", "Error:" + e.Message);
                ASCOM_ERROR_MESSAGE = "setOutputStatus(" + PortName + "," + intPortValue + "): Couldn't reach network server";
                //throw new ASCOM.NotConnectedException(ASCOM_ERROR_MESSAGE);
                tl.LogMessage("setOutputStatus", "Exit by web error");
                return ret;
            }
            
            // Reset cached read values
            lastOutputReadCheck = EXPIRED_CACHE;

            return ret;
        }

        private void clearCache()
        {
            //reset cache
            lastConnectedCheck = EXPIRED_CACHE;
            lastOutputReadCheck = EXPIRED_CACHE;
        }


        /// <summary>
        /// Tracing (logging) - 3 overloaded method
        /// </summary>
        public void Trace(string st)
        {
            Console.WriteLine(st);
            try
            {
                using (StreamWriter outfile = File.AppendText("d:/ascom_ip9212_logfile.log"))
                {
                    outfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), st);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Write trace file error! [" + e.Message + "]");
            }
        }

        public void Trace(int st)
        {
            Console.WriteLine(st);
        }

        public void Trace(string st, int[] arr_int)
        {
            string st_out = st;
            foreach (int el in arr_int)
            {
                st_out = st_out + el + " ";
            }

            Console.WriteLine(st_out);

            try
            {
                using (StreamWriter outfile = File.AppendText("d:/ascom_ip9212_logfile.log"))
                {
                    outfile.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), st_out);
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Write trace file error! [" + e.Message + "]");
            }
        }


        /// <summary>
        /// Standart dispose method
        /// </summary>
        public void Dispose()
        {
            tl.Dispose();
            tl = null;

            VedrusSemaphore.Dispose();
            VedrusSemaphore = null;
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class switchDataRawClass
    {
        public string Name = "";
        public Int16 Val = -1;
    }



    /// <summary>
    /// Override default WebClient calss to include TimeOut parameter
    /// </summary>
    public class MyWebClient : WebClient
    {
        static public int Timeout = 5 * 1000;

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = Timeout;
            return w;
        }

        public string UploadPOST(string SiteUri, string ParamString)
        {
            //string URI = "http://www.myurl.com/post.php";
            //string myParameters = "param1=value1&param2=value2&param3=value3";

            this.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string HtmlResult = this.UploadString(SiteUri, ParamString);

            return HtmlResult;
        }


    }

}
