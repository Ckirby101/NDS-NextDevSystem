namespace RemoteDebugger.Docks
{
	partial class MemWatch
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.AddrtextBox = new System.Windows.Forms.TextBox();
            this.MEMPTRHexControl = new HexControlLibrary.HexControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AddrtextBox);
            this.panel1.Controls.Add(this.MEMPTRHexControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 450);
            this.panel1.TabIndex = 0;
            // 
            // AddrtextBox
            // 
            this.AddrtextBox.Location = new System.Drawing.Point(3, 4);
            this.AddrtextBox.Name = "AddrtextBox";
            this.AddrtextBox.Size = new System.Drawing.Size(100, 20);
            this.AddrtextBox.TabIndex = 4;
            this.AddrtextBox.TextChanged += new System.EventHandler(this.AddrtextBox_TextChanged);
            this.AddrtextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddrtextBox_KeyPress);
            // 
            // MEMPTRHexControl
            // 
            this.MEMPTRHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MEMPTRHexControl.ColumnsPerRow = 16;
            this.MEMPTRHexControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MEMPTRHexControl.Location = new System.Drawing.Point(3, 30);
            this.MEMPTRHexControl.Name = "MEMPTRHexControl";
            this.MEMPTRHexControl.Size = new System.Drawing.Size(430, 417);
            this.MEMPTRHexControl.TabIndex = 3;
            // 
            // MemWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemWatch";
            this.ShowIcon = false;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private HexControlLibrary.HexControl MEMPTRHexControl;
		private System.Windows.Forms.TextBox AddrtextBox;
	}
}