namespace ASCOM.Vedrus
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbLang = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNetworkTimeout = new System.Windows.Forms.TextBox();
            this.txtCacheRead = new System.Windows.Forms.TextBox();
            this.txtCacheConnect = new System.Windows.Forms.TextBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridOutputSwitch = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipaddr = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.linkAstromania = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.linkAviosys = new System.Windows.Forms.LinkLabel();
            this.picAstromania = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SwitchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SwitchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SwitchROFlag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SwitchDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOutputSwitch)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAstromania)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Image = global::ASCOM.Vedrus.Properties.Resources.ASCOM;
            resources.ApplyResources(this.picASCOM, "picASCOM");
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cmbLang);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtNetworkTimeout);
            this.groupBox3.Controls.Add(this.txtCacheRead);
            this.groupBox3.Controls.Add(this.txtCacheConnect);
            this.groupBox3.Controls.Add(this.chkTrace);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // cmbLang
            // 
            this.cmbLang.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.Vedrus.Properties.Settings.Default, "Language", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLang.FormattingEnabled = true;
            resources.ApplyResources(this.cmbLang, "cmbLang");
            this.cmbLang.Name = "cmbLang";
            this.cmbLang.Text = global::ASCOM.Vedrus.Properties.Settings.Default.Language;
            this.myToolTip.SetToolTip(this.cmbLang, resources.GetString("cmbLang.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtNetworkTimeout
            // 
            this.txtNetworkTimeout.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.Vedrus.Properties.Settings.Default, "Network_timeout", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.txtNetworkTimeout, "txtNetworkTimeout");
            this.txtNetworkTimeout.Name = "txtNetworkTimeout";
            this.txtNetworkTimeout.Text = global::ASCOM.Vedrus.Properties.Settings.Default.Network_timeout;
            this.myToolTip.SetToolTip(this.txtNetworkTimeout, resources.GetString("txtNetworkTimeout.ToolTip"));
            // 
            // txtCacheRead
            // 
            this.txtCacheRead.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.Vedrus.Properties.Settings.Default, "ReadCacheTimeout", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.txtCacheRead, "txtCacheRead");
            this.txtCacheRead.Name = "txtCacheRead";
            this.txtCacheRead.Text = global::ASCOM.Vedrus.Properties.Settings.Default.ReadCacheTimeout;
            this.myToolTip.SetToolTip(this.txtCacheRead, resources.GetString("txtCacheRead.ToolTip"));
            // 
            // txtCacheConnect
            // 
            this.txtCacheConnect.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.Vedrus.Properties.Settings.Default, "CheckCacheTimeout", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.txtCacheConnect, "txtCacheConnect");
            this.txtCacheConnect.Name = "txtCacheConnect";
            this.txtCacheConnect.Text = global::ASCOM.Vedrus.Properties.Settings.Default.CheckCacheTimeout;
            this.myToolTip.SetToolTip(this.txtCacheConnect, resources.GetString("txtCacheConnect.ToolTip"));
            // 
            // chkTrace
            // 
            resources.ApplyResources(this.chkTrace, "chkTrace");
            this.chkTrace.Checked = global::ASCOM.Vedrus.Properties.Settings.Default.trace;
            this.chkTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrace.Name = "chkTrace";
            this.myToolTip.SetToolTip(this.chkTrace, resources.GetString("chkTrace.ToolTip"));
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridOutputSwitch);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dataGridOutputSwitch
            // 
            this.dataGridOutputSwitch.AllowUserToAddRows = false;
            this.dataGridOutputSwitch.AllowUserToDeleteRows = false;
            this.dataGridOutputSwitch.AllowUserToResizeRows = false;
            this.dataGridOutputSwitch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridOutputSwitch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SwitchId,
            this.SwitchName,
            this.SwitchROFlag,
            this.SwitchDescription});
            resources.ApplyResources(this.dataGridOutputSwitch, "dataGridOutputSwitch");
            this.dataGridOutputSwitch.Name = "dataGridOutputSwitch";
            this.dataGridOutputSwitch.RowHeadersVisible = false;
            this.dataGridOutputSwitch.ShowCellToolTips = false;
            this.myToolTip.SetToolTip(this.dataGridOutputSwitch, resources.GetString("dataGridOutputSwitch.ToolTip"));
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ipaddr);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.login);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pass);
            this.groupBox1.Controls.Add(this.port);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ipaddr
            // 
            resources.ApplyResources(this.ipaddr, "ipaddr");
            this.ipaddr.Name = "ipaddr";
            this.ipaddr.Text = global::ASCOM.Vedrus.Properties.Settings.Default.ipaddr;
            this.myToolTip.SetToolTip(this.ipaddr, resources.GetString("ipaddr.ToolTip"));
            // 
            // login
            // 
            resources.ApplyResources(this.login, "login");
            this.login.Name = "login";
            this.login.Text = global::ASCOM.Vedrus.Properties.Settings.Default.iplogin;
            this.myToolTip.SetToolTip(this.login, resources.GetString("login.ToolTip"));
            // 
            // pass
            // 
            resources.ApplyResources(this.pass, "pass");
            this.pass.Name = "pass";
            this.pass.Text = global::ASCOM.Vedrus.Properties.Settings.Default.ippass;
            this.myToolTip.SetToolTip(this.pass, resources.GetString("pass.ToolTip"));
            // 
            // port
            // 
            resources.ApplyResources(this.port, "port");
            this.port.Name = "port";
            this.port.Text = global::ASCOM.Vedrus.Properties.Settings.Default.ipport;
            this.myToolTip.SetToolTip(this.port, resources.GetString("port.ToolTip"));
            // 
            // linkAstromania
            // 
            resources.ApplyResources(this.linkAstromania, "linkAstromania");
            this.linkAstromania.Name = "linkAstromania";
            this.linkAstromania.TabStop = true;
            this.myToolTip.SetToolTip(this.linkAstromania, resources.GetString("linkAstromania.ToolTip"));
            this.linkAstromania.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAstromania_LinkClicked);
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            this.myToolTip.SetToolTip(this.lblVersion, resources.GetString("lblVersion.ToolTip"));
            // 
            // linkAviosys
            // 
            resources.ApplyResources(this.linkAviosys, "linkAviosys");
            this.linkAviosys.Name = "linkAviosys";
            this.linkAviosys.TabStop = true;
            this.myToolTip.SetToolTip(this.linkAviosys, resources.GetString("linkAviosys.ToolTip"));
            this.linkAviosys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAviosys_LinkClicked);
            // 
            // picAstromania
            // 
            resources.ApplyResources(this.picAstromania, "picAstromania");
            this.picAstromania.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAstromania.Image = global::ASCOM.Vedrus.Properties.Resources.logo_48_blue;
            this.picAstromania.Name = "picAstromania";
            this.picAstromania.TabStop = false;
            this.picAstromania.Click += new System.EventHandler(this.BrowseToAstromania);
            this.picAstromania.DoubleClick += new System.EventHandler(this.BrowseToAstromania);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblVersion);
            this.groupBox5.Controls.Add(this.linkAviosys);
            this.groupBox5.Controls.Add(this.linkAstromania);
            this.groupBox5.Controls.Add(this.picAstromania);
            this.groupBox5.Controls.Add(this.picASCOM);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // SwitchId
            // 
            resources.ApplyResources(this.SwitchId, "SwitchId");
            this.SwitchId.Name = "SwitchId";
            this.SwitchId.ReadOnly = true;
            this.SwitchId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SwitchName
            // 
            resources.ApplyResources(this.SwitchName, "SwitchName");
            this.SwitchName.Name = "SwitchName";
            this.SwitchName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SwitchROFlag
            // 
            resources.ApplyResources(this.SwitchROFlag, "SwitchROFlag");
            this.SwitchROFlag.Name = "SwitchROFlag";
            // 
            // SwitchDescription
            // 
            this.SwitchDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.SwitchDescription, "SwitchDescription");
            this.SwitchDescription.Name = "SwitchDescription";
            this.SwitchDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SetupDialogForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridOutputSwitch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAstromania)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox ipaddr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridOutputSwitch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip myToolTip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCacheConnect;
        private System.Windows.Forms.LinkLabel linkAstromania;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel linkAviosys;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbLang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCacheRead;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox picAstromania;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNetworkTimeout;
        private System.Windows.Forms.DataGridViewTextBoxColumn SwitchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SwitchName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SwitchROFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn SwitchDescription;
    }
}