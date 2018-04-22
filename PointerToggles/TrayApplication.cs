using PointerToggles.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointerToggles
{
    public class TrayApplication : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public TrayApplication()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Exit", Exit)
            }),
                Visible = true
            };

            trayIcon.DoubleClick += TrayIcon_DoubleClick;
            trayIcon.ContextMenu = BuildContextMenu();
        }

        private static ContextMenu BuildContextMenu()
        {
            var c = new ContextMenu();

            c.MenuItems.Add(new MenuItem("Exit", CmExit));

            return c;
        }

        private static void CmExit(object sender, EventArgs e) => Application.Exit();

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            var enabled = InteropHelper.ToggleMouseAccelleration();
            var tipText = $"Mouse accelleration: {(enabled ? "On" : "Off")}";
            trayIcon.ShowBalloonTip(1000, "Pointer Toggles", tipText, ToolTipIcon.Info);
            trayIcon.Text = tipText;
        }

        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
