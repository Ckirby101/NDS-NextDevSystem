namespace RemoteDebugger
{
    partial class Disassembly
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
            this.DissasemblyDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DissasemblyDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DissasemblyDataGrid
            // 
            this.DissasemblyDataGrid.AllowUserToAddRows = false;
            this.DissasemblyDataGrid.AllowUserToDeleteRows = false;
            this.DissasemblyDataGrid.AllowUserToResizeColumns = false;
            this.DissasemblyDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DissasemblyDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DissasemblyDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DissasemblyDataGrid.Location = new System.Drawing.Point(0, 0);
            this.DissasemblyDataGrid.MultiSelect = false;
            this.DissasemblyDataGrid.Name = "DissasemblyDataGrid";
            this.DissasemblyDataGrid.ReadOnly = true;
            this.DissasemblyDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DissasemblyDataGrid.Size = new System.Drawing.Size(284, 244);
            this.DissasemblyDataGrid.TabIndex = 0;
            this.DissasemblyDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DissasemblyDataGrid_CellContentClick);
            this.DissasemblyDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Disassembly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 244);
            this.Controls.Add(this.DissasemblyDataGrid);
            this.Name = "Disassembly";
            this.Text = "Disassembly";
            ((System.ComponentModel.ISupportInitialize)(this.DissasemblyDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DissasemblyDataGrid;
    }
}