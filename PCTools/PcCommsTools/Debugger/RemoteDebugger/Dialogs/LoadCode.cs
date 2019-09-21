/*
 
The MIT License (MIT) 
 
Copyright (c) 2017 Savoury SnaX 
 
Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions: 
 
The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software. 
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
SOFTWARE. 

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDebugger
{
    public partial class LoadCode : Form
    {
        public LoadCode()
        {
            InitializeComponent();

            numAddress.Minimum = 16384;
            numAddress.Maximum = 65535;
            numAddress.Increment = 1;
            numAddress.Value = Properties.Settings.Default.loadCodeAddress;
            checkAutoBreak.Checked = Properties.Settings.Default.loadCodeAutoBreak;
            checkAutoStart.Checked = Properties.Settings.Default.loadCodeAutoStart;
            checkAutoBreak.Enabled = checkAutoStart.Checked;
            fileName.Text = Properties.Settings.Default.loadCodeFileName;
        }

        string FileRequester(string initialFolder,string filter)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = initialFolder;
            openFileDialog1.Filter = filter;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileNames[0];
            }
            return "";
        }

        private void clickChooseFile(object sender, EventArgs e)
        {
            string path = fileName.Text;
            try
            {
                path = Path.GetFullPath(path);
            }
            catch
            {
                path = "";
            }
            fileName.Text = FileRequester(path, "bin files (*.bin)|*.bin|All files (*.*)|*.*");
        }

        void NoResponse(string[] response,int tag)
        {

        }

        void LoadResponse(string[] data,int tag)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { Close(); });
            }
            else
            {
                Close();
            }
        }

        private void clickOk(object sender, EventArgs e)
        {
            Properties.Settings.Default.loadCodeFileName = fileName.Text;
            Properties.Settings.Default.loadCodeAddress = (int)numAddress.Value;
            Properties.Settings.Default.loadCodeAutoStart = checkAutoStart.Checked;
            Properties.Settings.Default.loadCodeAutoBreak = checkAutoBreak.Checked;
            Properties.Settings.Default.Save();

            try
            {
                FileStream inFile = File.OpenRead(fileName.Text);

                long length = inFile.Length;
                string toSend = "wmm " + numAddress.Value.ToString();
                for (int a = 0; a < length; a++)
                {
                    toSend += " " + (int)inFile.ReadByte();
                }
                if (!checkAutoStart.Checked)
                    Program.telnetConnection.SendCommand(toSend, LoadResponse);
                else
                    Program.telnetConnection.SendCommand(toSend, NoResponse);

                if (checkAutoStart.Checked && checkAutoBreak.Checked)
                {
                    MainForm.myButtonBar.SwapMode(true);
                    for (int a = 0; a < 10; a++)        // HACK- for some reason immediately after entering step mode,commands get ignored, this hack works around that.
                    {
                        Program.telnetConnection.SendCommand("cpu-step", NoResponse);
                        Program.telnetConnection.SendCommand("set-register pc=" + numAddress.Value.ToString(), NoResponse);
                    }
                }
                if (checkAutoStart.Checked)
                {
                    Program.telnetConnection.SendCommand("set-register pc="+numAddress.Value.ToString(), LoadResponse);
                }
            }
            catch
            {
                // just consume for now
            }
            
        }

        private void clickCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void checkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            checkAutoBreak.Enabled = checkAutoStart.Checked;
        }
    }
}
