using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointerToggles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var mouseParams = new int[3];

            // Get the current values.
            InteropHelper.SystemParametersInfoGet(InteropHelper.SPI_GETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), 0);

            // Modify the acceleration value as directed.
            mouseParams[2] = mouseParams[2] == 0 ? 1 : 0;

            // Update the system setting.
            InteropHelper.SystemParametersInfoSet(InteropHelper.SPI_SETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), SPIF.SPIF_SENDCHANGE);

            trayIcon.ShowBalloonTip(1000, "Pointer Toggles", $"Mouse accelleration: {(mouseParams[2] == 1 ? "On" : "Off")}", ToolTipIcon.Info);
        }
    }
}
