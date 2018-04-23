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

        private Timer timer;

        public TrayApplication()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = InteropHelper.MouseAccelleration ? Resources.AppIconAccellerated : Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };

            trayIcon.DoubleClick += TrayIcon_DoubleClick;
            trayIcon.ContextMenu = BuildContextMenu();

            timer = new Timer()
            {
                Enabled = true,
                Interval = (int)TimeSpan.FromMinutes(1).TotalMilliseconds
            };

            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e) => UpdateTrayIcon();

        private void UpdateTrayIcon()
        {
            var mouseAccelleration = InteropHelper.MouseAccelleration;
            var tipText = $"Mouse accelleration: {(mouseAccelleration ? "On" : "Off")}";

            trayIcon.Icon = mouseAccelleration ? Resources.AppIconAccellerated : Resources.AppIcon;
            trayIcon.Text = tipText;
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
            UpdateTrayIcon();
        }

        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
