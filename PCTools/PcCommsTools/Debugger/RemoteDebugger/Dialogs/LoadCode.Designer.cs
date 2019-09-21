namespace RemoteDebugger
{
    partial class LoadCode
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.TextBox();
            this.numAddress = new System.Windows.Forms.NumericUpDown();
            this.checkAutoBreak = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkAutoStart = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Binary To Load";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Load To Address";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.clickChooseFile);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(98, 13);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(144, 20);
            this.fileName.TabIndex = 3;
            // 
            // numAddress
            // 
            this.numAddress.Location = new System.Drawing.Point(106, 43);
            this.numAddress.Name = "numAddress";
            this.numAddress.Size = new System.Drawing.Size(165, 20);
            this.numAddress.TabIndex = 4;
            // 
            // checkAutoBreak
            // 
            this.checkAutoBreak.AutoSize = true;
            this.checkAutoBreak.Location = new System.Drawing.Point(174, 69);
            this.checkAutoBreak.Name = "checkAutoBreak";
            this.checkAutoBreak.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkAutoBreak.Size = new System.Drawing.Size(98, 17);
            this.checkAutoBreak.TabIndex = 5;
            this.checkAutoBreak.Text = "Break On Entry";
            this.checkAutoBreak.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(197, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 24);
            this.button2.TabIndex = 6;
            this.button2.Text = "&Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.clickOk);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(116, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 24);
            this.button3.TabIndex = 7;
            this.button3.Text = "&Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.clickCancel);
            // 
            // checkAutoStart
            // 
            this.checkAutoStart.AutoSize = true;
            this.checkAutoStart.Location = new System.Drawing.Point(82, 92);
            this.checkAutoStart.Name = "checkAutoStart";
            this.checkAutoStart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkAutoStart.Size = new System.Drawing.Size(190, 17);
            this.checkAutoStart.TabIndex = 8;
            this.checkAutoStart.Text = "Jump Directly To Address On Load";
            this.checkAutoStart.UseVisualStyleBackColor = true;
            this.checkAutoStart.CheckedChanged += new System.EventHandler(this.checkAutoStart_CheckedChanged);
            // 
            // LoadCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 169);
            this.Controls.Add(this.checkAutoStart);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkAutoBreak);
            this.Controls.Add(this.numAddress);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoadCode";
            this.Text = "Load Code";
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.NumericUpDown numAddress;
        private System.Windows.Forms.CheckBox checkAutoBreak;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkAutoStart;
    }
}