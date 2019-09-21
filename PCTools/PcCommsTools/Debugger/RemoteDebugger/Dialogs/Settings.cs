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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteDebugger.Properties;
namespace RemoteDebugger
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            // Load settings
            remoteAddress.Text = Properties.Settings.Default.remoteAddress;
            remotePort.Minimum = 1;
            remotePort.Maximum = 65535;
            remotePort.Increment = 1;
            remotePort.Value = Properties.Settings.Default.remotePort;
        }

        private void clickCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void clickOk(object sender, EventArgs e)
        {
            Properties.Settings.Default.remoteAddress = remoteAddress.Text;
            Properties.Settings.Default.remotePort = (int)remotePort.Value;
            Properties.Settings.Default.Save();
            Program.telnetConnection.UpdateSettings(Properties.Settings.Default.remoteAddress, Properties.Settings.Default.remotePort);
            Close();
        }
    }
}
