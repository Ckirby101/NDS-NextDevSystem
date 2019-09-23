namespace RemoteDebugger.Docks
{
	partial class RegMemWatch
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
            this.HLHexControl = new HexControlLibrary.HexControl();
            this.HLlabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DEHexControl = new HexControlLibrary.HexControl();
            this.DElabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BCHexControl = new HexControlLibrary.HexControl();
            this.BClabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.IXHexControl = new HexControlLibrary.HexControl();
            this.IXlabel = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.IYHexControl = new HexControlLibrary.HexControl();
            this.IYlabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.HLHexControl);
            this.panel1.Controls.Add(this.HLlabel);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 68);
            this.panel1.TabIndex = 1;
            // 
            // HLHexControl
            // 
            this.HLHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HLHexControl.ColumnsPerRow = 16;
            this.HLHexControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HLHexControl.Location = new System.Drawing.Point(0, 19);
            this.HLHexControl.Name = "HLHexControl";
            this.HLHexControl.Size = new System.Drawing.Size(443, 46);
            this.HLHexControl.TabIndex = 2;
            // 
            // HLlabel
            // 
            this.HLlabel.AutoSize = true;
            this.HLlabel.Location = new System.Drawing.Point(5, 3);
            this.HLlabel.Name = "HLlabel";
            this.HLlabel.Size = new System.Drawing.Size(33, 13);
            this.HLlabel.TabIndex = 1;
            this.HLlabel.Text = "( HL )";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.panel4);
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(438, 456);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DEHexControl);
            this.panel2.Controls.Add(this.DElabel);
            this.panel2.Location = new System.Drawing.Point(3, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(451, 70);
            this.panel2.TabIndex = 3;
            // 
            // DEHexControl
            // 
            this.DEHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DEHexControl.ColumnsPerRow = 16;
            this.DEHexControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DEHexControl.Location = new System.Drawing.Point(0, 19);
            this.DEHexControl.Name = "DEHexControl";
            this.DEHexControl.Size = new System.Drawing.Size(443, 46);
            this.DEHexControl.TabIndex = 2;
            // 
            // DElabel
            // 
            this.DElabel.AutoSize = true;
            this.DElabel.Location = new System.Drawing.Point(5, 3);
            this.DElabel.Name = "DElabel";
            this.DElabel.Size = new System.Drawing.Size(34, 13);
            this.DElabel.TabIndex = 1;
            this.DElabel.Text = "( DE )";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BCHexControl);
            this.panel3.Controls.Add(this.BClabel);
            this.panel3.Location = new System.Drawing.Point(3, 153);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(451, 70);
            this.panel3.TabIndex = 4;
            // 
            // BCHexControl
            // 
            this.BCHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BCHexControl.ColumnsPerRow = 16;
            this.BCHexControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BCHexControl.Location = new System.Drawing.Point(0, 19);
            this.BCHexControl.Name = "BCHexControl";
            this.BCHexControl.Size = new System.Drawing.Size(443, 46);
            this.BCHexControl.TabIndex = 2;
            // 
            // BClabel
            // 
            this.BClabel.AutoSize = true;
            this.BClabel.Location = new System.Drawing.Point(5, 3);
            this.BClabel.Name = "BClabel";
            this.BClabel.Size = new System.Drawing.Size(33, 13);
            this.BClabel.TabIndex = 1;
            this.BClabel.Text = "( BC )";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.IXHexControl);
            this.panel4.Controls.Add(this.IXlabel);
            this.panel4.Location = new System.Drawing.Point(3, 229);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(451, 70);
            this.panel4.TabIndex = 5;
            // 
            // IXHexControl
            // 
            this.IXHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IXHexControl.ColumnsPerRow = 16;
            this.IXHexControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IXHexControl.Location = new System.Drawing.Point(0, 19);
            this.IXHexControl.Name = "IXHexControl";
            this.IXHexControl.Size = new System.Drawing.Size(443, 46);
            this.IXHexControl.TabIndex = 2;
            // 
            // IXlabel
            // 
            this.IXlabel.AutoSize = true;
            this.IXlabel.Location = new System.Drawing.Point(5, 3);
            this.IXlabel.Name = "IXlabel";
            this.IXlabel.Size = new System.Drawing.Size(29, 13);
            this.IXlabel.TabIndex = 1;
            this.IXlabel.Text = "( IX )";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.IYHexControl);
            this.panel5.Controls.Add(this.IYlabel);
            this.panel5.Location = new System.Drawing.Point(3, 305);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(451, 70);
            this.panel5.TabIndex = 6;
            // 
            // IYHexControl
            // 
            this.IYHexControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IYHexControl.ColumnsPerRow = 16;
            this.IYHexControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IYHexControl.Location = new System.Drawing.Point(0, 19);
            this.IYHexControl.Name = "IYHexControl";
            this.IYHexControl.Size = new System.Drawing.Size(443, 46);
            this.IYHexControl.TabIndex = 2;
            // 
            // IYlabel
            // 
            this.IYlabel.AutoSize = true;
            this.IYlabel.Location = new System.Drawing.Point(5, 3);
            this.IYlabel.Name = "IYlabel";
            this.IYlabel.Size = new System.Drawing.Size(29, 13);
            this.IYlabel.TabIndex = 1;
            this.IYlabel.Text = "( IY )";
            // 
            // RegMemWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 456);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegMemWatch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private HexControlLibrary.HexControl HLHexControl;
		private System.Windows.Forms.Label HLlabel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Panel panel2;
		private HexControlLibrary.HexControl DEHexControl;
		private System.Windows.Forms.Label DElabel;
		private System.Windows.Forms.Panel panel3;
		private HexControlLibrary.HexControl BCHexControl;
		private System.Windows.Forms.Label BClabel;
		private System.Windows.Forms.Panel panel4;
		private HexControlLibrary.HexControl IXHexControl;
		private System.Windows.Forms.Label IXlabel;
		private System.Windows.Forms.Panel panel5;
		private HexControlLibrary.HexControl IYHexControl;
		private System.Windows.Forms.Label IYlabel;
	}
}