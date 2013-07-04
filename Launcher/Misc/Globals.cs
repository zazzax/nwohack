using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Launcher
{
    public static class Globals
    {

        public static readonly frmCheckUpdates CheckUpdatesForm = new frmCheckUpdates();
        public static readonly frmAbout AboutForm = new frmAbout();

        public static readonly Dictionary<string, string> ClientHashes = new Dictionary<string, string>();

        public static readonly string GameVersion = "1.20130529g.33";
        public static readonly string CurrentVersion = Application.ProductVersion;
        public static readonly string StartupPath = Application.StartupPath;
        public static readonly string TempPath = Environment.GetEnvironmentVariable( "TEMP" ) + "\\NWOHack";

        public static readonly string ModulePath = StartupPath + "\\Host.dll";
        public static readonly string ClientHashPath = TempPath + "\\client.txt";
        public static readonly string VersionPath = TempPath + "\\version.txt";

        public static readonly string ClientHashURL = "https://dl.dropboxusercontent.com/u/2650876/NWOHack/client.txt";
        public static readonly string VersionURL = "https://dl.dropboxusercontent.com/u/2650876/NWOHack/version.txt";
        public static readonly string BinaryDirectoryURL = "https://dl.dropboxusercontent.com/u/2650876/NWOHack/Binaries/";

    }
}
