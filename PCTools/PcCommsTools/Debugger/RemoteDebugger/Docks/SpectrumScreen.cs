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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RemoteDebugger
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public RECT(RECT Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
        {
        }
        public RECT(int Left, int Top, int Right, int Bottom)
        {
            _Left = Left;
            _Top = Top;
            _Right = Right;
            _Bottom = Bottom;
        }

        public int X
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Y
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Right
        {
            get { return _Right; }
            set { _Right = value; }
        }
        public int Bottom
        {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        public int Height
        {
            get { return _Bottom - _Top; }
            set { _Bottom = value + _Top; }
        }
        public int Width
        {
            get { return _Right - _Left; }
            set { _Right = value + _Left; }
        }
        public Point Location
        {
            get { return new Point(Left, Top); }
            set
            {
                _Left = value.X;
                _Top = value.Y;
            }
        }
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                _Right = value.Width + _Left;
                _Bottom = value.Height + _Top;
            }
        }

        public static implicit operator Rectangle(RECT Rectangle)
        {
            return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
        }
        public static implicit operator RECT(Rectangle Rectangle)
        {
            return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
        }
        public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
        {
            return Rectangle1.Equals(Rectangle2);
        }
        public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
        {
            return !Rectangle1.Equals(Rectangle2);
        }

        public override string ToString()
        {
            return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(RECT Rectangle)
        {
            return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
        }

        public override bool Equals(object Object)
        {
            if (Object is RECT)
            {
                return Equals((RECT)Object);
            }
            else if (Object is Rectangle)
            {
                return Equals(new RECT((Rectangle)Object));
            }

            return false;
        }
    }


    public partial class SpectrumScreen : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        string viewName;
        Bitmap screenImage;
        bool showEmulator;
        public SpectrumScreen(string name, string viewname)
        {
            showEmulator = true;
            viewName = viewname;
            InitializeComponent();

            screenImage = new Bitmap(256, 192, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            pictureBox1.Image = screenImage;
            TabText = "Emulator Output";

            RequestUpdate();
        }
        override protected string GetPersistString()
        {
            return viewName;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static Bitmap MyPrintWindow(IntPtr hwnd)
        {
            RECT rc;
            GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            PrintWindow(hwnd, hdcBitmap, 0);    // This can hang if ZEsarUX isn't updating its display -- stick me on a thread

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }

        public void RequestUpdate()
        {
            if (showEmulator && !Program.InStepMode)
            {
                foreach (Process pList in Process.GetProcesses())
                    if (pList.MainWindowTitle.Contains("ZEsarUX"))
                        pictureBox1.Image = MyPrintWindow(pList.MainWindowHandle);
            }
            else
            {
                //Program.telnetConnection.SendCommand("read-mapped-memory 16384 8192", Callback);
            }
        }

        Color GetAttrColour(byte attr, int pixSet)
        {
            int ink = attr & 0x07;
            int paper = (attr & 0x38) >> 3;
            int bright = (attr & 0x40) >> 6;
            /*            int flash = (attr & 0x80) >> 7;

                        if (flash==1 && (flashTimer & 0x10))
                        {
                            uint8_t swap = paper;
                            paper = ink;
                            ink = swap;
                        }*/
            int red=0, green=0, blue=0;
            int col = 0;
            if (pixSet == 1)
            {
                col = ink;
            }
            else
            {
                col = paper;
            }
            if ((col & 0x01)==1)
            {
                if ((bright) == 1)
                    blue = 0xFF;
                else
                    blue = 0xCC;
            }
            if ((col & 0x02)==2)
            {
                if ((bright) == 1)
                    red = 0xFF;
                else
                    red = 0xCC;
            }
            if ((col & 0x04)==4)
            {
                if ((bright) == 1)
                    green = 0xFF;
                else
                    green = 0xCC;
            }

            return Color.FromArgb(red, green, blue);
        }

        void UIUpdate(string[] items)
        {
            // Turn the stream of hex into a stream of bytes
            if (items.Count() != 1)
                return;

            byte[] t = new byte[6912];
            for (int a=0;a<6912;a++)
            {
                string bp = ""+items[0][a * 2 + 0] + items[0][a * 2 + 1];
                t[a] = Convert.ToByte(bp, 16);
            }

            // Now render the bitmap

            for (int y = 0; y < 192; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    int pixelAddress = x / 8 + y * 32;
                    byte val = t[(pixelAddress & 0x181F) | ((pixelAddress & 0x700) >> 3) | ((pixelAddress & 0xE0) << 3)];
                    byte col = t[0x1800 | (pixelAddress & 0x1F) | ((pixelAddress & 0x1F00) >> 3)];

                    int ink;
                    if ((val & (0x80 >> (x % 8)))!=0)
                        ink = 1;
                    else
                        ink = 0;
                    screenImage.SetPixel(x, y, GetAttrColour(col,ink));
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

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            showEmulator = !showEmulator;
            if (showEmulator)
                TabText = "Emulator Output";
            else
                TabText = "Spectrum Memory Display";
        }
    }

}
