using System;

namespace Launcher
{
    public struct FileDownload
    {

        public Uri URI;
        public string SavePath;

        public FileDownload( Uri pURI, string sSavePath )
        { URI = pURI; SavePath = sSavePath; }

    }
}
