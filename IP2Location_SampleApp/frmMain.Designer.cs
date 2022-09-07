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
            this.btnGetIPInfoToConsole = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(23, 144);
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
            this.txtResult.Location = new System.Drawing.Point(22, 164);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(720, 211);
            this.txtResult.TabIndex = 6;
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(22, 105);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(358, 23);
            this.txtIP.TabIndex = 5;
            this.txtIP.Text = "188.70.11.199";
            // 
            // btnGetIPInfoToTextBox
            // 
            this.btnGetIPInfoToTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetIPInfoToTextBox.Location = new System.Drawing.Point(386, 78);
            this.btnGetIPInfoToTextBox.Name = "btnGetIPInfoToTextBox";
            this.btnGetIPInfoToTextBox.Size = new System.Drawing.Size(91, 50);
            this.btnGetIPInfoToTextBox.TabIndex = 4;
            this.btnGetIPInfoToTextBox.Text = "Get IP Info to TextBox";
            this.btnGetIPInfoToTextBox.UseVisualStyleBackColor = true;
            this.btnGetIPInfoToTextBox.Click += new System.EventHandler(this.btnGetIPInfoToTextBox_Click);
            // 
            // rbIPv4
            // 
            this.rbIPv4.AutoSize = true;
            this.rbIPv4.Checked = true;
            this.rbIPv4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIPv4.Location = new System.Drawing.Point(23, 78);
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
            this.rbIPv6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIPv6.Location = new System.Drawing.Point(102, 78);
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
            this.lblPath.Location = new System.Drawing.Point(23, 17);
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
            this.txtPath.Location = new System.Drawing.Point(22, 37);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(683, 23);
            this.txtPath.TabIndex = 5;
            this.txtPath.Text = "C:\\Temp\\IP2Location\\IP2LOCATION-LITE-DB11.IPV6.BIN";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPath.Location = new System.Drawing.Point(711, 37);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(31, 23);
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
            // btnGetIPInfoToConsole
            // 
            this.btnGetIPInfoToConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetIPInfoToConsole.Location = new System.Drawing.Point(483, 78);
            this.btnGetIPInfoToConsole.Name = "btnGetIPInfoToConsole";
            this.btnGetIPInfoToConsole.Size = new System.Drawing.Size(91, 50);
            this.btnGetIPInfoToConsole.TabIndex = 4;
            this.btnGetIPInfoToConsole.Text = "Get IP Info to Console";
            this.btnGetIPInfoToConsole.UseVisualStyleBackColor = true;
            this.btnGetIPInfoToConsole.Click += new System.EventHandler(this.btnGetIPInfoToConsole_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 397);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.rbIPv6);
            this.Controls.Add(this.rbIPv4);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.btnGetIPInfoToConsole);
            this.Controls.Add(this.btnGetIPInfoToTextBox);
            this.Name = "frmMain";
            this.Text = "IP2Location - Sample Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
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
        private System.Windows.Forms.Button btnGetIPInfoToConsole;
    }
}

