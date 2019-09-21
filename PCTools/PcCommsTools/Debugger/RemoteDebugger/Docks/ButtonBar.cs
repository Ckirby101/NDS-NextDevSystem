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
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RemoteDebugger
{
    public partial class ButtonBar : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        string viewName;

        public ButtonBar(string viewname)
        {
            viewName = viewname;
            InitializeComponent();
        }

        override protected string GetPersistString()
        {
            return viewName;
        }

        void commandResponse(string[] s,int tag)
        {

        }

        bool LoadThingToAddress(int address,string filter)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = filter;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            string toSend = "wmm " + address.ToString();
                            for (int a = 0; a < myStream.Length; a++)
                            {
                                toSend += " " + (int)myStream.ReadByte();
                            }
                            Program.telnetConnection.SendCommand(toSend, commandResponse);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return false;
        }

        private void clickLoadScreen(object sender, EventArgs e)
        {
            LoadThingToAddress(16384, "scr files (*.scr)|*.scr|All files (*.*)|*.*");
        }

        private void clickStep(object sender, EventArgs e)
        {
            Program.telnetConnection.SendCommand("cpu-step", commandResponse);
        }

        public void SwapMode(bool pause)
        {
            if (Program.InStepMode && !pause)
            {
                Program.telnetConnection.SendCommand("disable-breakpoints", commandResponse);
                Program.telnetConnection.SendCommand("exit-cpu-step", commandResponse);
                Program.InStepMode = false;
                buttonPause.Text = "Pause";
            }
            if (!Program.InStepMode && pause)
            {
                Program.telnetConnection.SendCommand("enter-cpu-step", commandResponse);
                Program.telnetConnection.SendCommand("enable-breakpoints", commandResponse);
                Program.InStepMode = true;
                buttonPause.Text = "Resume";
            }
        }

        private void clickPause(object sender, EventArgs e)
        {
            SwapMode(!Program.InStepMode);
        }

        private void ButtonBar_Load(object sender, EventArgs e)
        {

        }

        private void clickStepOver(object sender, EventArgs e)
        {
            Program.telnetConnection.SendCommand("cpu-step-over", commandResponse);
        }

        private void clickRun(object sender, EventArgs e)
        {
            Program.telnetConnection.SendCommand("run", commandResponse);
        }

        private void clickLoadCode(object sender, EventArgs e)
        {
            using (LoadCode dialog = new LoadCode())
            {
                dialog.ShowDialog();
            }
        }
    }
}
