namespace RemoteDebugger.Docks
{
	partial class callstack
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
			this.callstacklistbox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// callstacklistbox
			// 
			this.callstacklistbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.callstacklistbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.callstacklistbox.FormattingEnabled = true;
			this.callstacklistbox.ItemHeight = 18;
			this.callstacklistbox.Location = new System.Drawing.Point(0, 0);
			this.callstacklistbox.Name = "callstacklistbox";
			this.callstacklistbox.Size = new System.Drawing.Size(428, 450);
			this.callstacklistbox.TabIndex = 0;
			this.callstacklistbox.DoubleClick += new System.EventHandler(this.callstacklistbox_DoubleClick);
			// 
			// callstack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(428, 450);
			this.ControlBox = false;
			this.Controls.Add(this.callstacklistbox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "callstack";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox callstacklistbox;
	}
}