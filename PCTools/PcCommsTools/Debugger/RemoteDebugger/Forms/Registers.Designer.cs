namespace RemoteDebugger
{
    partial class Registers
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
            this.label15 = new System.Windows.Forms.Label();
            this.RegExF = new System.Windows.Forms.TextBox();
            this.RegR = new System.Windows.Forms.TextBox();
            this.RegA = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RegI = new System.Windows.Forms.TextBox();
            this.RegExA = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegPC = new System.Windows.Forms.TextBox();
            this.RegF = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RegSP = new System.Windows.Forms.TextBox();
            this.RegHL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RegIY = new System.Windows.Forms.TextBox();
            this.RegExHL = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RegIX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.RegDE = new System.Windows.Forms.TextBox();
            this.RegExBC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.RegExDE = new System.Windows.Forms.TextBox();
            this.RegBC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RegFlags = new System.Windows.Forms.Label();
            this.BankData = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.z80 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.stack = new System.Windows.Forms.TextBox();
            this.HWRegisters = new System.Windows.Forms.TabPage();
            this.hwpanel = new System.Windows.Forms.Panel();
            this.nextPC = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.z80.SuspendLayout();
            this.HWRegisters.SuspendLayout();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(330, 40);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 13);
            this.label15.TabIndex = 63;
            this.label15.Text = "R";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegExF
            // 
            this.RegExF.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RegExF.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegExF.Location = new System.Drawing.Point(190, 36);
            this.RegExF.Name = "RegExF";
            this.RegExF.Size = new System.Drawing.Size(110, 23);
            this.RegExF.TabIndex = 42;
            // 
            // RegR
            // 
            this.RegR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegR.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegR.Location = new System.Drawing.Point(347, 37);
            this.RegR.Name = "RegR";
            this.RegR.Size = new System.Drawing.Size(110, 23);
            this.RegR.TabIndex = 62;
            // 
            // RegA
            // 
            this.RegA.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegA.Location = new System.Drawing.Point(27, 7);
            this.RegA.Name = "RegA";
            this.RegA.Size = new System.Drawing.Size(110, 23);
            this.RegA.TabIndex = 32;
            this.RegA.Text = "$00 / 256";
            this.RegA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegA_KeyDown);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(333, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 13);
            this.label16.TabIndex = 61;
            this.label16.Text = "I";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "A";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegI
            // 
            this.RegI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegI.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegI.Location = new System.Drawing.Point(347, 7);
            this.RegI.Name = "RegI";
            this.RegI.Size = new System.Drawing.Size(110, 23);
            this.RegI.TabIndex = 60;
            // 
            // RegExA
            // 
            this.RegExA.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RegExA.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegExA.Location = new System.Drawing.Point(190, 7);
            this.RegExA.Name = "RegExA";
            this.RegExA.Size = new System.Drawing.Size(110, 23);
            this.RegExA.TabIndex = 34;
            this.RegExA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegExA_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(21, 13);
            this.label13.TabIndex = 59;
            this.label13.Text = "PC";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "A\'";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegPC
            // 
            this.RegPC.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegPC.Location = new System.Drawing.Point(27, 147);
            this.RegPC.Name = "RegPC";
            this.RegPC.Size = new System.Drawing.Size(110, 23);
            this.RegPC.TabIndex = 58;
            this.RegPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegPC_KeyDown);
            // 
            // RegF
            // 
            this.RegF.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegF.Location = new System.Drawing.Point(27, 36);
            this.RegF.Name = "RegF";
            this.RegF.Size = new System.Drawing.Size(110, 23);
            this.RegF.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(167, 179);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(21, 13);
            this.label14.TabIndex = 57;
            this.label14.Text = "SP";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "F";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegSP
            // 
            this.RegSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegSP.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegSP.Location = new System.Drawing.Point(190, 177);
            this.RegSP.Name = "RegSP";
            this.RegSP.Size = new System.Drawing.Size(110, 23);
            this.RegSP.TabIndex = 56;
            this.RegSP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegSP_KeyDown);
            // 
            // RegHL
            // 
            this.RegHL.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegHL.Location = new System.Drawing.Point(27, 64);
            this.RegHL.Name = "RegHL";
            this.RegHL.Size = new System.Drawing.Size(110, 23);
            this.RegHL.TabIndex = 38;
            this.RegHL.Text = "$FFFF / 65536";
            this.RegHL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegHL_KeyDown);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(325, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 55;
            this.label11.Text = "IY";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "HL";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegIY
            // 
            this.RegIY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegIY.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegIY.Location = new System.Drawing.Point(347, 92);
            this.RegIY.Name = "RegIY";
            this.RegIY.Size = new System.Drawing.Size(110, 23);
            this.RegIY.TabIndex = 54;
            this.RegIY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegIY_KeyDown);
            // 
            // RegExHL
            // 
            this.RegExHL.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RegExHL.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegExHL.Location = new System.Drawing.Point(190, 64);
            this.RegExHL.Name = "RegExHL";
            this.RegExHL.Size = new System.Drawing.Size(110, 23);
            this.RegExHL.TabIndex = 40;
            this.RegExHL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegExHL_KeyDown);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(325, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 53;
            this.label12.Text = "IX";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "HL\'";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegIX
            // 
            this.RegIX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RegIX.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegIX.Location = new System.Drawing.Point(347, 65);
            this.RegIX.Name = "RegIX";
            this.RegIX.Size = new System.Drawing.Size(110, 23);
            this.RegIX.TabIndex = 52;
            this.RegIX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegIX_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "F\'";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(163, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "BC\'";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegDE
            // 
            this.RegDE.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegDE.Location = new System.Drawing.Point(27, 92);
            this.RegDE.Name = "RegDE";
            this.RegDE.Size = new System.Drawing.Size(110, 23);
            this.RegDE.TabIndex = 44;
            this.RegDE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegDE_KeyDown);
            // 
            // RegExBC
            // 
            this.RegExBC.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RegExBC.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegExBC.Location = new System.Drawing.Point(190, 117);
            this.RegExBC.Name = "RegExBC";
            this.RegExBC.Size = new System.Drawing.Size(110, 23);
            this.RegExBC.TabIndex = 50;
            this.RegExBC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegExBC_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 45;
            this.label8.Text = "DE";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "BC";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegExDE
            // 
            this.RegExDE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RegExDE.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegExDE.Location = new System.Drawing.Point(190, 91);
            this.RegExDE.Name = "RegExDE";
            this.RegExDE.Size = new System.Drawing.Size(110, 23);
            this.RegExDE.TabIndex = 46;
            this.RegExDE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegExDE_KeyDown);
            // 
            // RegBC
            // 
            this.RegBC.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegBC.Location = new System.Drawing.Point(27, 118);
            this.RegBC.Name = "RegBC";
            this.RegBC.Size = new System.Drawing.Size(110, 23);
            this.RegBC.TabIndex = 48;
            this.RegBC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegBC_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(163, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "DE\'";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegFlags
            // 
            this.RegFlags.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RegFlags.AutoSize = true;
            this.RegFlags.Location = new System.Drawing.Point(187, 150);
            this.RegFlags.Name = "RegFlags";
            this.RegFlags.Size = new System.Drawing.Size(52, 13);
            this.RegFlags.TabIndex = 65;
            this.RegFlags.Text = "F C N HC";
            this.RegFlags.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BankData
            // 
            this.BankData.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BankData.AutoSize = true;
            this.BankData.Location = new System.Drawing.Point(49, 203);
            this.BankData.Name = "BankData";
            this.BankData.Size = new System.Drawing.Size(88, 13);
            this.BankData.TabIndex = 66;
            this.BankData.Text = "$00 $01 $02 $03";
            this.BankData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 203);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 67;
            this.label18.Text = "Banks";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(161, 148);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 13);
            this.label17.TabIndex = 68;
            this.label17.Text = "flgs";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.z80);
            this.tabControl1.Controls.Add(this.HWRegisters);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 247);
            this.tabControl1.TabIndex = 69;
            // 
            // z80
            // 
            this.z80.Controls.Add(this.nextPC);
            this.z80.Controls.Add(this.label20);
            this.z80.Controls.Add(this.label19);
            this.z80.Controls.Add(this.stack);
            this.z80.Controls.Add(this.RegF);
            this.z80.Controls.Add(this.BankData);
            this.z80.Controls.Add(this.label18);
            this.z80.Controls.Add(this.label17);
            this.z80.Controls.Add(this.RegBC);
            this.z80.Controls.Add(this.label10);
            this.z80.Controls.Add(this.label8);
            this.z80.Controls.Add(this.label15);
            this.z80.Controls.Add(this.RegFlags);
            this.z80.Controls.Add(this.RegR);
            this.z80.Controls.Add(this.RegDE);
            this.z80.Controls.Add(this.label16);
            this.z80.Controls.Add(this.label5);
            this.z80.Controls.Add(this.RegI);
            this.z80.Controls.Add(this.label14);
            this.z80.Controls.Add(this.RegExF);
            this.z80.Controls.Add(this.RegSP);
            this.z80.Controls.Add(this.RegHL);
            this.z80.Controls.Add(this.label11);
            this.z80.Controls.Add(this.label3);
            this.z80.Controls.Add(this.RegIY);
            this.z80.Controls.Add(this.RegA);
            this.z80.Controls.Add(this.label12);
            this.z80.Controls.Add(this.RegPC);
            this.z80.Controls.Add(this.RegIX);
            this.z80.Controls.Add(this.RegExA);
            this.z80.Controls.Add(this.label2);
            this.z80.Controls.Add(this.label13);
            this.z80.Controls.Add(this.label1);
            this.z80.Controls.Add(this.label7);
            this.z80.Controls.Add(this.RegExDE);
            this.z80.Controls.Add(this.RegExBC);
            this.z80.Controls.Add(this.RegExHL);
            this.z80.Controls.Add(this.label9);
            this.z80.Controls.Add(this.label6);
            this.z80.Controls.Add(this.label4);
            this.z80.Location = new System.Drawing.Point(4, 22);
            this.z80.Name = "z80";
            this.z80.Padding = new System.Windows.Forms.Padding(3);
            this.z80.Size = new System.Drawing.Size(471, 221);
            this.z80.TabIndex = 0;
            this.z80.Text = "Z80";
            this.z80.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(347, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(33, 13);
            this.label19.TabIndex = 70;
            this.label19.Text = "stack";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // stack
            // 
            this.stack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stack.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stack.Location = new System.Drawing.Point(347, 147);
            this.stack.Multiline = true;
            this.stack.Name = "stack";
            this.stack.ReadOnly = true;
            this.stack.Size = new System.Drawing.Size(110, 68);
            this.stack.TabIndex = 69;
            // 
            // HWRegisters
            // 
            this.HWRegisters.Controls.Add(this.hwpanel);
            this.HWRegisters.Location = new System.Drawing.Point(4, 22);
            this.HWRegisters.Name = "HWRegisters";
            this.HWRegisters.Padding = new System.Windows.Forms.Padding(3);
            this.HWRegisters.Size = new System.Drawing.Size(471, 221);
            this.HWRegisters.TabIndex = 1;
            this.HWRegisters.Text = "Next IO";
            this.HWRegisters.UseVisualStyleBackColor = true;
            // 
            // hwpanel
            // 
            this.hwpanel.AutoScroll = true;
            this.hwpanel.Location = new System.Drawing.Point(2, 2);
            this.hwpanel.Name = "hwpanel";
            this.hwpanel.Size = new System.Drawing.Size(465, 219);
            this.hwpanel.TabIndex = 0;
            // 
            // nextPC
            // 
            this.nextPC.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nextPC.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextPC.Location = new System.Drawing.Point(27, 177);
            this.nextPC.Name = "nextPC";
            this.nextPC.Size = new System.Drawing.Size(110, 23);
            this.nextPC.TabIndex = 71;
            this.nextPC.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(0, 180);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 13);
            this.label20.TabIndex = 72;
            this.label20.Text = "NPC";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // Registers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 251);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(460, 39);
            this.Name = "Registers";
            this.Text = "Registers";
            this.tabControl1.ResumeLayout(false);
            this.z80.ResumeLayout(false);
            this.z80.PerformLayout();
            this.HWRegisters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox RegExF;
		private System.Windows.Forms.TextBox RegR;
		private System.Windows.Forms.TextBox RegA;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox RegI;
		private System.Windows.Forms.TextBox RegExA;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox RegPC;
		private System.Windows.Forms.TextBox RegF;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox RegSP;
		private System.Windows.Forms.TextBox RegHL;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox RegIY;
		private System.Windows.Forms.TextBox RegExHL;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox RegIX;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox RegDE;
		private System.Windows.Forms.TextBox RegExBC;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox RegExDE;
		private System.Windows.Forms.TextBox RegBC;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label RegFlags;
		private System.Windows.Forms.Label BankData;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage z80;
        private System.Windows.Forms.TabPage HWRegisters;
        private System.Windows.Forms.Panel hwpanel;
        private System.Windows.Forms.TextBox stack;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox nextPC;
        private System.Windows.Forms.Label label20;
    }
}