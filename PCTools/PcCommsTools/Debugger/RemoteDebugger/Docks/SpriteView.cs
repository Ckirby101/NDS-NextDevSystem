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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDebugger
{
    public class PictureBoxWithInterpolationMode : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
    }

    public partial class SpriteView  : BaseDock
    {
        Bitmap screenImage;
        Color[] palette = new Color[256];

        public SpriteView(string name,string viewname) : base(viewname)
        {
            InitializeComponent();

            screenImage = new Bitmap(16, 16);
            TabText = name;
            pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;

            numPattern.Minimum = 0;
            numPattern.Maximum = 63;
            numPattern.Increment = 1;

            // Create palette (its fixed in spec next I think)
            for (int a=0;a<256;a++)
            {
                byte t = (byte)a;

                int r = (t & 0xE0);
                int g = (t & 0x1C) << 3;
                int b = (t & 0x03) << 6;

                palette[a] = Color.FromArgb(r, g, b);
            }
        }

        override protected string GetPersistString()
        {
            return viewName;
        }

        public override void RequestUpdate()
        {
            int pattern = (int)numPattern.Value;
            Program.telnetConnection.SendCommand("tbblue-get-pattern "+pattern, Callback);
        }

        void UIUpdate(string[] items)
        {
            // Turn the stream of hex into a stream of bytes
            if (items.Count() < 1)
                return;

            byte[] t = new byte[16*16];
            for (int a=0;a<16*16;a++)
            {
                string bp = ""+items[0][a * 3 + 0] + items[0][a * 3 + 1];
                t[a] = Convert.ToByte(bp, 16);
            }

            // Now render the bitmap

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    byte palVal = t[x + y * 16];
                    Color c = palette[palVal];
                    screenImage.SetPixel(x, y, c);
                }
            }
//            if (updated)
            {
                pictureBox1.Image = screenImage;
                pictureBox1.Invalidate(true);
            }

        }

        void Callback(string[] response,int tag)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { UIUpdate(response); });
                }
                else
                {
                    UIUpdate(response);
                }
            }
            catch
            {

            }
        }


    }
}
