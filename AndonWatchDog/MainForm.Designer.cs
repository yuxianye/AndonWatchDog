namespace AndonWatchDog
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_interval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cb_Startup = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.nudMonitorOn = new System.Windows.Forms.NumericUpDown();
            this.nudMonitorOff = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_interval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonitorOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonitorOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Anodn WatchDog";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 124);
            // 
            // showMainToolStripMenuItem
            // 
            this.showMainToolStripMenuItem.Name = "showMainToolStripMenuItem";
            this.showMainToolStripMenuItem.Size = new System.Drawing.Size(175, 30);
            this.showMainToolStripMenuItem.Text = "Show Main";
            this.showMainToolStripMenuItem.Click += new System.EventHandler(this.showMainToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Enabled = false;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(175, 30);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(175, 30);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(175, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Enabled = false;
            this.btn_Start.Location = new System.Drawing.Point(68, 298);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(150, 69);
            this.btn_Start.TabIndex = 6;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(379, 298);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(150, 69);
            this.btn_Stop.TabIndex = 7;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 122);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Watch interval(min):";
            // 
            // nud_interval
            // 
            this.nud_interval.Location = new System.Drawing.Point(227, 120);
            this.nud_interval.Margin = new System.Windows.Forms.Padding(4);
            this.nud_interval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nud_interval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_interval.Name = "nud_interval";
            this.nud_interval.Size = new System.Drawing.Size(76, 28);
            this.nud_interval.TabIndex = 1;
            this.toolTip1.SetToolTip(this.nud_interval, "Automatically set the EDGE browser on focus after x minutes");
            this.nud_interval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_interval.ValueChanged += new System.EventHandler(this.nud_interval_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 249);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Watch Web Title:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(227, 246);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(295, 28);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "GEFASOFT Legato Montage";
            // 
            // cb_Startup
            // 
            this.cb_Startup.AutoSize = true;
            this.cb_Startup.Checked = true;
            this.cb_Startup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Startup.Location = new System.Drawing.Point(380, 126);
            this.cb_Startup.Margin = new System.Windows.Forms.Padding(4);
            this.cb_Startup.Name = "cb_Startup";
            this.cb_Startup.Size = new System.Drawing.Size(142, 22);
            this.cb_Startup.TabIndex = 5;
            this.cb_Startup.Text = "Auto Startup";
            this.toolTip1.SetToolTip(this.cb_Startup, "Windows user login ,this app will startup automatic");
            this.cb_Startup.UseVisualStyleBackColor = true;
            this.cb_Startup.CheckedChanged += new System.EventHandler(this.cb_Startup_CheckedChanged);
            // 
            // nudMonitorOn
            // 
            this.nudMonitorOn.Location = new System.Drawing.Point(227, 162);
            this.nudMonitorOn.Margin = new System.Windows.Forms.Padding(4);
            this.nudMonitorOn.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudMonitorOn.Name = "nudMonitorOn";
            this.nudMonitorOn.Size = new System.Drawing.Size(76, 28);
            this.nudMonitorOn.TabIndex = 2;
            this.toolTip1.SetToolTip(this.nudMonitorOn, "set monitor on at x clock");
            this.nudMonitorOn.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudMonitorOn.ValueChanged += new System.EventHandler(this.nudMonitorOn_ValueChanged);
            // 
            // nudMonitorOff
            // 
            this.nudMonitorOff.Location = new System.Drawing.Point(227, 205);
            this.nudMonitorOff.Margin = new System.Windows.Forms.Padding(4);
            this.nudMonitorOff.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudMonitorOff.Name = "nudMonitorOff";
            this.nudMonitorOff.Size = new System.Drawing.Size(76, 28);
            this.nudMonitorOff.TabIndex = 3;
            this.toolTip1.SetToolTip(this.nudMonitorOff, "set monitor off at x clock");
            this.nudMonitorOff.Value = new decimal(new int[] {
            19,
            0,
            0,
            0});
            this.nudMonitorOff.ValueChanged += new System.EventHandler(this.nudMonitorOff_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AndonWatchDog.Properties.Resources.legato_logo;
            this.pictureBox1.Location = new System.Drawing.Point(60, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AndonWatchDog.Properties.Resources.LOGO;
            this.pictureBox2.Location = new System.Drawing.Point(338, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(191, 71);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 164);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Monitor on at :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 207);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Monitor off at :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 392);
            this.Controls.Add(this.nudMonitorOff);
            this.Controls.Add(this.nudMonitorOn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cb_Startup);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nud_interval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Andon WatchDog V2.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_interval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonitorOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonitorOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_interval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox cb_Startup;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMonitorOn;
        private System.Windows.Forms.NumericUpDown nudMonitorOff;
    }
}

