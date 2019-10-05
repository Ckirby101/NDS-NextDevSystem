namespace RemoteDebugger
{
    partial class Breakpoint
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BPAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BPNo,
            this.BPAddr,
            this.Delete});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(284, 261);
            this.dataGridView1.TabIndex = 0;
            // 
            // BPNo
            // 
            this.BPNo.DataPropertyName = "No";
            this.BPNo.FillWeight = 38.72813F;
            this.BPNo.HeaderText = "No";
            this.BPNo.Name = "BPNo";
            this.BPNo.ReadOnly = true;
            // 
            // BPAddr
            // 
            this.BPAddr.DataPropertyName = "Address";
            this.BPAddr.FillWeight = 215.5866F;
            this.BPAddr.HeaderText = "Address";
            this.BPAddr.Name = "BPAddr";
            this.BPAddr.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.FillWeight = 45.68528F;
            this.Delete.HeaderText = "del";
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Breakpoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Breakpoint";
            this.Text = "Breakpoint";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BPAddr;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}