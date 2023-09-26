namespace IP2Location_SampleApp
{
    partial class frmMain
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
            this.lblResult = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnGetIPInfoToTextBox = new System.Windows.Forms.Button();
            this.rbIPv4 = new System.Windows.Forms.RadioButton();
            this.rbIPv6 = new System.Windows.Forms.RadioButton();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lnk1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlBinFile = new System.Windows.Forms.Panel();
            this.pnlWebService = new System.Windows.Forms.Panel();
            this.rbBinFile = new System.Windows.Forms.RadioButton();
            this.rbWebService = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.pnlBinFile.SuspendLayout();
            this.pnlWebService.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(31, 219);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(52, 17);
            this.lblResult.TabIndex = 7;
            this.lblResult.Text = "Result:";
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(29, 244);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(1033, 289);
            this.txtResult.TabIndex = 6;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(29, 174);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(340, 23);
            this.txtIP.TabIndex = 5;
            this.txtIP.Text = "188.70.11.199";
            // 
            // btnGetIPInfoToTextBox
            // 
            this.btnGetIPInfoToTextBox.Location = new System.Drawing.Point(377, 145);
            this.btnGetIPInfoToTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetIPInfoToTextBox.Name = "btnGetIPInfoToTextBox";
            this.btnGetIPInfoToTextBox.Size = new System.Drawing.Size(100, 52);
            this.btnGetIPInfoToTextBox.TabIndex = 4;
            this.btnGetIPInfoToTextBox.Text = "Get IP Info";
            this.btnGetIPInfoToTextBox.UseVisualStyleBackColor = true;
            this.btnGetIPInfoToTextBox.Click += new System.EventHandler(this.btnGetIPInfoToTextBox_Click);
            // 
            // rbIPv4
            // 
            this.rbIPv4.AutoSize = true;
            this.rbIPv4.Checked = true;
            this.rbIPv4.Location = new System.Drawing.Point(30, 145);
            this.rbIPv4.Margin = new System.Windows.Forms.Padding(4);
            this.rbIPv4.Name = "rbIPv4";
            this.rbIPv4.Size = new System.Drawing.Size(53, 21);
            this.rbIPv4.TabIndex = 9;
            this.rbIPv4.TabStop = true;
            this.rbIPv4.Text = "IPv4";
            this.rbIPv4.UseVisualStyleBackColor = true;
            this.rbIPv4.CheckedChanged += new System.EventHandler(this.rbIPv4_CheckedChanged);
            // 
            // rbIPv6
            // 
            this.rbIPv6.AutoSize = true;
            this.rbIPv6.Location = new System.Drawing.Point(119, 145);
            this.rbIPv6.Margin = new System.Windows.Forms.Padding(4);
            this.rbIPv6.Name = "rbIPv6";
            this.rbIPv6.Size = new System.Drawing.Size(53, 21);
            this.rbIPv6.TabIndex = 9;
            this.rbIPv6.Text = "IPv6";
            this.rbIPv6.UseVisualStyleBackColor = true;
            this.rbIPv6.CheckedChanged += new System.EventHandler(this.rbIPv6_CheckedChanged);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(2, 2);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(138, 17);
            this.lblPath.TabIndex = 10;
            this.lblPath.Text = "IP2Location BIN File:";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(2, 23);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(597, 23);
            this.txtPath.TabIndex = 5;
            this.txtPath.Text = "\\Data\\IP2LOCATION-LITE-DB1.IPV6.BIN";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPath.Location = new System.Drawing.Point(607, 19);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(41, 28);
            this.btnSelectPath.TabIndex = 4;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "BIN";
            this.ofd.Filter = "Bin Files|*.bin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Add your API key:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApiKey.Location = new System.Drawing.Point(2, 24);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(317, 23);
            this.txtApiKey.TabIndex = 11;
            // 
            // lnk1
            // 
            this.lnk1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnk1.AutoSize = true;
            this.lnk1.Location = new System.Drawing.Point(150, 2);
            this.lnk1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnk1.Name = "lnk1";
            this.lnk1.Size = new System.Drawing.Size(169, 17);
            this.lnk1.TabIndex = 13;
            this.lnk1.TabStop = true;
            this.lnk1.Text = "https://www.ip2location.io/";
            this.lnk1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk1_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnlBinFile);
            this.groupBox1.Controls.Add(this.pnlWebService);
            this.groupBox1.Controls.Add(this.rbBinFile);
            this.groupBox1.Controls.Add(this.rbWebService);
            this.groupBox1.Location = new System.Drawing.Point(29, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1033, 117);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Source";
            // 
            // pnlBinFile
            // 
            this.pnlBinFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBinFile.Controls.Add(this.btnSelectPath);
            this.pnlBinFile.Controls.Add(this.lblPath);
            this.pnlBinFile.Controls.Add(this.txtPath);
            this.pnlBinFile.Enabled = false;
            this.pnlBinFile.Location = new System.Drawing.Point(374, 53);
            this.pnlBinFile.Name = "pnlBinFile";
            this.pnlBinFile.Size = new System.Drawing.Size(652, 51);
            this.pnlBinFile.TabIndex = 15;
            // 
            // pnlWebService
            // 
            this.pnlWebService.Controls.Add(this.label1);
            this.pnlWebService.Controls.Add(this.txtApiKey);
            this.pnlWebService.Controls.Add(this.lnk1);
            this.pnlWebService.Location = new System.Drawing.Point(21, 53);
            this.pnlWebService.Name = "pnlWebService";
            this.pnlWebService.Size = new System.Drawing.Size(323, 51);
            this.pnlWebService.TabIndex = 14;
            // 
            // rbBinFile
            // 
            this.rbBinFile.AutoSize = true;
            this.rbBinFile.Location = new System.Drawing.Point(374, 24);
            this.rbBinFile.Margin = new System.Windows.Forms.Padding(4);
            this.rbBinFile.Name = "rbBinFile";
            this.rbBinFile.Size = new System.Drawing.Size(74, 21);
            this.rbBinFile.TabIndex = 0;
            this.rbBinFile.Text = "BIN File";
            this.rbBinFile.UseVisualStyleBackColor = true;
            this.rbBinFile.CheckedChanged += new System.EventHandler(this.rbBinFile_CheckedChanged);
            // 
            // rbWebService
            // 
            this.rbWebService.AutoSize = true;
            this.rbWebService.Checked = true;
            this.rbWebService.Location = new System.Drawing.Point(21, 25);
            this.rbWebService.Margin = new System.Windows.Forms.Padding(4);
            this.rbWebService.Name = "rbWebService";
            this.rbWebService.Size = new System.Drawing.Size(106, 21);
            this.rbWebService.TabIndex = 0;
            this.rbWebService.TabStop = true;
            this.rbWebService.Text = "Web Service";
            this.rbWebService.UseVisualStyleBackColor = true;
            this.rbWebService.CheckedChanged += new System.EventHandler(this.rbWebService_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbIPv6);
            this.Controls.Add(this.rbIPv4);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnGetIPInfoToTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "frmMain";
            this.Text = "IP2Location - Sample Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBinFile.ResumeLayout(false);
            this.pnlBinFile.PerformLayout();
            this.pnlWebService.ResumeLayout(false);
            this.pnlWebService.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnGetIPInfoToTextBox;
        private System.Windows.Forms.RadioButton rbIPv4;
        private System.Windows.Forms.RadioButton rbIPv6;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.LinkLabel lnk1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbBinFile;
        private System.Windows.Forms.RadioButton rbWebService;
        private System.Windows.Forms.Panel pnlBinFile;
        private System.Windows.Forms.Panel pnlWebService;
    }
}

