using System.Windows.Forms;

namespace Launcher
{
    public static class Globals
    {

        public static frmCheckUpdates CheckUpdatesForm = new frmCheckUpdates();
        public static frmAbout AboutForm = new frmAbout();
        public static readonly string ModulePath = Application.StartupPath + "\\Host.dll";

    }
}
