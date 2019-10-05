namespace RemoteDebugger
{
	partial class SourceWindow
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
            this.SourceTab = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.FunctionComboBox = new System.Windows.Forms.ComboBox();
            this.continuebutton = new System.Windows.Forms.Button();
            this.stepoverbutton = new System.Windows.Forms.Button();
            this.breakbutton = new System.Windows.Forms.Button();
            this.stepbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SourceTab
            // 
            this.SourceTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceTab.Location = new System.Drawing.Point(3, 38);
            this.SourceTab.Name = "SourceTab";
            this.SourceTab.SelectedIndex = 0;
            this.SourceTab.Size = new System.Drawing.Size(954, 281);
            this.SourceTab.TabIndex = 0;
            this.SourceTab.SelectedIndexChanged += new System.EventHandler(this.SourceTab_SelectedIndexChanged);
            this.SourceTab.TabIndexChanged += new System.EventHandler(this.SourceTab_TabIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SourceTab, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 322);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.FunctionComboBox);
            this.panel1.Controls.Add(this.continuebutton);
            this.panel1.Controls.Add(this.stepoverbutton);
            this.panel1.Controls.Add(this.breakbutton);
            this.panel1.Controls.Add(this.stepbutton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 35);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(597, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "goto:";
            // 
            // FunctionComboBox
            // 
            this.FunctionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FunctionComboBox.FormattingEnabled = true;
            this.FunctionComboBox.Location = new System.Drawing.Point(634, 8);
            this.FunctionComboBox.Name = "FunctionComboBox";
            this.FunctionComboBox.Size = new System.Drawing.Size(323, 21);
            this.FunctionComboBox.Sorted = true;
            this.FunctionComboBox.TabIndex = 5;
            this.FunctionComboBox.SelectedIndexChanged += new System.EventHandler(this.FunctionComboBox_SelectedIndexChanged);
            // 
            // continuebutton
            // 
            this.continuebutton.BackColor = System.Drawing.Color.PaleGreen;
            this.continuebutton.Location = new System.Drawing.Point(0, 0);
            this.continuebutton.Name = "continuebutton";
            this.continuebutton.Size = new System.Drawing.Size(90, 36);
            this.continuebutton.TabIndex = 3;
            this.continuebutton.Text = "CONTINUE";
            this.continuebutton.UseVisualStyleBackColor = false;
            this.continuebutton.Visible = false;
            this.continuebutton.Click += new System.EventHandler(this.continuebutton_Click);
            // 
            // stepoverbutton
            // 
            this.stepoverbutton.BackColor = System.Drawing.Color.SkyBlue;
            this.stepoverbutton.Location = new System.Drawing.Point(310, 0);
            this.stepoverbutton.Name = "stepoverbutton";
            this.stepoverbutton.Size = new System.Drawing.Size(90, 36);
            this.stepoverbutton.TabIndex = 2;
            this.stepoverbutton.Text = "STEP OVER";
            this.stepoverbutton.UseVisualStyleBackColor = false;
            this.stepoverbutton.Click += new System.EventHandler(this.stepoverbutton_Click);
            // 
            // breakbutton
            // 
            this.breakbutton.BackColor = System.Drawing.Color.Salmon;
            this.breakbutton.Location = new System.Drawing.Point(0, 0);
            this.breakbutton.Name = "breakbutton";
            this.breakbutton.Size = new System.Drawing.Size(90, 36);
            this.breakbutton.TabIndex = 1;
            this.breakbutton.Text = "BREAK";
            this.breakbutton.UseVisualStyleBackColor = false;
            this.breakbutton.Click += new System.EventHandler(this.breakbutton_Click);
            // 
            // stepbutton
            // 
            this.stepbutton.BackColor = System.Drawing.Color.SkyBlue;
            this.stepbutton.Location = new System.Drawing.Point(214, 0);
            this.stepbutton.Name = "stepbutton";
            this.stepbutton.Size = new System.Drawing.Size(90, 36);
            this.stepbutton.TabIndex = 0;
            this.stepbutton.Text = "STEP";
            this.stepbutton.UseVisualStyleBackColor = false;
            this.stepbutton.Click += new System.EventHandler(this.stepbutton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(480, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 36);
            this.button1.TabIndex = 7;
            this.button1.Text = "Focus PC";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SourceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 322);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SourceWindow";
            this.Text = "SourceWindow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabControl SourceTab;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button stepoverbutton;
		private System.Windows.Forms.Button breakbutton;
		private System.Windows.Forms.Button stepbutton;
		private System.Windows.Forms.Button continuebutton;
		private System.Windows.Forms.ComboBox FunctionComboBox;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}