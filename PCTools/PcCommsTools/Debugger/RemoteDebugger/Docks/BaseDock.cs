using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDebugger
{
    public abstract class BaseDock : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        static int luid = 0;
        public string viewName;
        public BaseDock(string viewname)
        {
            viewName = viewname+":"+luid;
            FormClosing += BaseDock_FormClosing;
        }

        private void BaseDock_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            MainForm.myDocks.Remove(this);
        }

        public abstract void RequestUpdate();
    }
}
