namespace RemoteDebugger
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sourceCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseTraceDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.VsplitContainer = new System.Windows.Forms.SplitContainer();
            this.LeftsplitContainer = new System.Windows.Forms.SplitContainer();
            this.RightFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.transmitNexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VsplitContainer)).BeginInit();
            this.VsplitContainer.Panel1.SuspendLayout();
            this.VsplitContainer.Panel2.SuspendLayout();
            this.VsplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftsplitContainer)).BeginInit();
            this.LeftsplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceCodeToolStripMenuItem,
            this.transmitNexToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1496, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sourceCodeToolStripMenuItem
            // 
            this.sourceCodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseTraceDataToolStripMenuItem});
            this.sourceCodeToolStripMenuItem.Name = "sourceCodeToolStripMenuItem";
            this.sourceCodeToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.sourceCodeToolStripMenuItem.Text = "Source Code";
            // 
            // parseTraceDataToolStripMenuItem
            // 
            this.parseTraceDataToolStripMenuItem.Name = "parseTraceDataToolStripMenuItem";
            this.parseTraceDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.parseTraceDataToolStripMenuItem.Text = "Parse TraceData";
            this.parseTraceDataToolStripMenuItem.Click += new System.EventHandler(this.parseTraceDataToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1018);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1496, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // VsplitContainer
            // 
            this.VsplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VsplitContainer.Location = new System.Drawing.Point(0, 24);
            this.VsplitContainer.Name = "VsplitContainer";
            // 
            // VsplitContainer.Panel1
            // 
            this.VsplitContainer.Panel1.Controls.Add(this.LeftsplitContainer);
            // 
            // VsplitContainer.Panel2
            // 
            this.VsplitContainer.Panel2.Controls.Add(this.RightFlow);
            this.VsplitContainer.Size = new System.Drawing.Size(1496, 994);
            this.VsplitContainer.SplitterDistance = 1000;
            this.VsplitContainer.SplitterWidth = 6;
            this.VsplitContainer.TabIndex = 2;
            // 
            // LeftsplitContainer
            // 
            this.LeftsplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftsplitContainer.Location = new System.Drawing.Point(0, 0);
            this.LeftsplitContainer.Name = "LeftsplitContainer";
            this.LeftsplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftsplitContainer.Size = new System.Drawing.Size(1000, 994);
            this.LeftsplitContainer.SplitterDistance = 885;
            this.LeftsplitContainer.SplitterWidth = 6;
            this.LeftsplitContainer.TabIndex = 0;
            // 
            // RightFlow
            // 
            this.RightFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.RightFlow.Location = new System.Drawing.Point(0, 0);
            this.RightFlow.Name = "RightFlow";
            this.RightFlow.Size = new System.Drawing.Size(488, 992);
            this.RightFlow.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // transmitNexToolStripMenuItem
            // 
            this.transmitNexToolStripMenuItem.Name = "transmitNexToolStripMenuItem";
            this.transmitNexToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.transmitNexToolStripMenuItem.Text = "Transmit Nex";
            this.transmitNexToolStripMenuItem.Click += new System.EventHandler(this.transmitNexToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 1040);
            this.Controls.Add(this.VsplitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Remote Debugger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.VsplitContainer.Panel1.ResumeLayout(false);
            this.VsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VsplitContainer)).EndInit();
            this.VsplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftsplitContainer)).EndInit();
            this.LeftsplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sourceCodeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem parseTraceDataToolStripMenuItem;
		private System.Windows.Forms.SplitContainer VsplitContainer;
		private System.Windows.Forms.SplitContainer LeftsplitContainer;
		private System.Windows.Forms.FlowLayoutPanel RightFlow;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem transmitNexToolStripMenuItem;
    }
}

