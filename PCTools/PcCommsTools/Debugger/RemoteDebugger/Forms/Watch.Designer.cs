namespace RemoteDebugger.Docks
{
	partial class Watch
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBreakpoints = new System.Windows.Forms.TabPage();
            this.tabWatch = new System.Windows.Forms.TabPage();
            this.tabCallStack = new System.Windows.Forms.TabPage();
            this.tabMemory = new System.Windows.Forms.TabPage();
            this.tabMem = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBreakpoints);
            this.tabControl1.Controls.Add(this.tabWatch);
            this.tabControl1.Controls.Add(this.tabCallStack);
            this.tabControl1.Controls.Add(this.tabMemory);
            this.tabControl1.Controls.Add(this.tabMem);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(467, 474);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            // 
            // tabBreakpoints
            // 
            this.tabBreakpoints.Location = new System.Drawing.Point(4, 22);
            this.tabBreakpoints.Name = "tabBreakpoints";
            this.tabBreakpoints.Size = new System.Drawing.Size(459, 448);
            this.tabBreakpoints.TabIndex = 3;
            this.tabBreakpoints.Text = "Breakpoints";
            this.tabBreakpoints.UseVisualStyleBackColor = true;
            // 
            // tabWatch
            // 
            this.tabWatch.Location = new System.Drawing.Point(4, 22);
            this.tabWatch.Name = "tabWatch";
            this.tabWatch.Size = new System.Drawing.Size(459, 448);
            this.tabWatch.TabIndex = 2;
            this.tabWatch.Text = "Watch";
            this.tabWatch.UseVisualStyleBackColor = true;
            // 
            // tabCallStack
            // 
            this.tabCallStack.Location = new System.Drawing.Point(4, 22);
            this.tabCallStack.Name = "tabCallStack";
            this.tabCallStack.Padding = new System.Windows.Forms.Padding(3);
            this.tabCallStack.Size = new System.Drawing.Size(459, 448);
            this.tabCallStack.TabIndex = 1;
            this.tabCallStack.Text = "CallStack";
            this.tabCallStack.UseVisualStyleBackColor = true;
            // 
            // tabMemory
            // 
            this.tabMemory.Location = new System.Drawing.Point(4, 22);
            this.tabMemory.Name = "tabMemory";
            this.tabMemory.Size = new System.Drawing.Size(459, 448);
            this.tabMemory.TabIndex = 4;
            this.tabMemory.Text = "Reg Memory";
            this.tabMemory.UseVisualStyleBackColor = true;
            // 
            // tabMem
            // 
            this.tabMem.Location = new System.Drawing.Point(4, 22);
            this.tabMem.Name = "tabMem";
            this.tabMem.Size = new System.Drawing.Size(459, 448);
            this.tabMem.TabIndex = 5;
            this.tabMem.Text = "Memory";
            this.tabMem.UseVisualStyleBackColor = true;
            // 
            // Watch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 474);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Watch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabCallStack;
		private System.Windows.Forms.TabPage tabWatch;
		private System.Windows.Forms.TabPage tabBreakpoints;
		private System.Windows.Forms.TabPage tabMemory;
		private System.Windows.Forms.TabPage tabMem;
	}
}