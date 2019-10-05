namespace RemoteDebugger.Dialogs
{
	partial class Watches
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
            this.watchesGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addbutton = new System.Windows.Forms.Button();
            this.labeladdtext = new System.Windows.Forms.TextBox();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.watchesGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // watchesGrid
            // 
            this.watchesGrid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.watchesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.watchesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Value,
            this.Value16,
            this.Delete});
            this.watchesGrid.Location = new System.Drawing.Point(-3, 44);
            this.watchesGrid.MultiSelect = false;
            this.watchesGrid.Name = "watchesGrid";
            this.watchesGrid.Size = new System.Drawing.Size(434, 405);
            this.watchesGrid.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addbutton);
            this.panel1.Controls.Add(this.labeladdtext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 38);
            this.panel1.TabIndex = 1;
            // 
            // addbutton
            // 
            this.addbutton.Location = new System.Drawing.Point(230, 9);
            this.addbutton.Name = "addbutton";
            this.addbutton.Size = new System.Drawing.Size(75, 23);
            this.addbutton.TabIndex = 1;
            this.addbutton.Text = "add";
            this.addbutton.UseVisualStyleBackColor = true;
            this.addbutton.Click += new System.EventHandler(this.addbutton_Click);
            // 
            // labeladdtext
            // 
            this.labeladdtext.Location = new System.Drawing.Point(4, 9);
            this.labeladdtext.Name = "labeladdtext";
            this.labeladdtext.Size = new System.Drawing.Size(220, 20);
            this.labeladdtext.TabIndex = 0;
            // 
            // Variable
            // 
            this.Variable.DataPropertyName = "Variable";
            this.Variable.HeaderText = "Watch";
            this.Variable.Name = "Variable";
            this.Variable.Width = 140;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "8Bit";
            this.Value.Name = "Value";
            this.Value.Width = 130;
            // 
            // Value16
            // 
            this.Value16.DataPropertyName = "Value16";
            this.Value16.HeaderText = "16Bit";
            this.Value16.Name = "Value16";
            this.Value16.Width = 130;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "del";
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Width = 30;
            // 
            // Watches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.watchesGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Watches";
            ((System.ComponentModel.ISupportInitialize)(this.watchesGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView watchesGrid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button addbutton;
		private System.Windows.Forms.TextBox labeladdtext;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value16;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}