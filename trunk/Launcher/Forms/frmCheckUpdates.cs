using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Launcher
{
    public partial class frmCheckUpdates : Form
    {

        private WebClient WebClient = new WebClient();
        private List<FileDownload> DownloadQueue = new List<FileDownload>();
        private int CurrentDownloadPosition, DownloadCount;

        private string LatestVersion, NewBinary, NewBinaryPath;

        public delegate void FileDownloadCompleteEventHandler( string sFilePath );
        public event FileDownloadCompleteEventHandler FileDownloadComplete;

        public bool IsDownloadActive { get; private set; }
        
        public frmCheckUpdates()
        {
            InitializeComponent();
        }

        private void frmCheckUpdates_Load( object sender, EventArgs e )
        {
            FileDownloadComplete += frmCheckUpdates_FileDownloadComplete;

            WebClient.DownloadProgressChanged += ( _, pUpdate ) =>
            { barDownloadProgress.Value = pUpdate.ProgressPercentage; };

            WebClient.DownloadFileCompleted += ( _, pUpdate ) =>
            {
                if( pUpdate.Error != null )
                    throw pUpdate.Error;

                FileDownloadComplete( DownloadQueue[0].SavePath );
                DownloadQueue.RemoveAt( 0 );

                IsDownloadActive = false;
            };
        }

        private void frmCheckUpdates_FileDownloadComplete( string sFilePath )
        {
            if( sFilePath == Globals.VersionPath )
            {
                LatestVersion = File.ReadAllText( Globals.VersionPath );
                if( LatestVersion != Globals.CurrentVersion )
                {
                    DialogResult nDialogResult = 
                            MessageBox.Show( "A new version of NWOHack is available (v" + LatestVersion + ")! Would you like to download it?",
                                                "New update available", MessageBoxButtons.YesNo, MessageBoxIcon.Information );

                    if( nDialogResult == DialogResult.No )
                        return;

                    NewBinary = "NWOHack_" + LatestVersion + ".zip";
                    NewBinaryPath = Globals.TempPath + "\\" + NewBinary;
                    QueueDownload( Globals.BinaryDirectoryURL + NewBinary, NewBinaryPath );
                    
                    if( !IsDownloadActive )
                        StartDownloadQueue();
                }
            }
            else if( sFilePath == NewBinaryPath )
            {
                Process.Start( NewBinaryPath );
                Application.Exit();
            }
            else if( sFilePath == Globals.ClientHashPath )
            {
                Globals.ClientHashes.Clear();

                string[] sClientHashes = File.ReadAllLines( Globals.ClientHashPath );
                foreach( string sClientHash in sClientHashes )
                {
                    if( sClientHash == "" || sClientHash.StartsWith( "#" ) )
                        continue;

                    string[] sClientHashInfo = sClientHash.Split( new[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
                    if( sClientHashInfo.Length != 2 )
                        continue;

                    Globals.ClientHashes.Add( sClientHashInfo[0], sClientHashInfo[1] );
                }
            }
        }

        private void tmrUpdateDownloadQueue_Tick( object sender, EventArgs e )
        {
            if( IsDownloadActive )
                return;

            ++CurrentDownloadPosition;
            barDownloadProgress.Value = 0;

            if( DownloadQueue.Count == 0 )
            {
                tmrUpdateDownloadQueue.Enabled = false;
                UpdateTitle();

                Thread.Sleep( 1000 );
                Hide();
                return;
            }

            IsDownloadActive = true;
            WebClient.DownloadFileAsync( DownloadQueue[0].URI, DownloadQueue[0].SavePath );
            UpdateTitle();
        }

        /// <summary>
        /// Checks for updates relating to NWOHack. 
        /// 
        /// You may add CUSTOM_BUILD to your compiler symbols if you wish to 
        /// disable version checks against the official version.
        /// </summary>
        public void CheckUpdates()
        {
            if( !Directory.Exists( Globals.TempPath ) )
                Directory.CreateDirectory( Globals.TempPath );

            QueueDownload( Globals.ClientHashURL, Globals.ClientHashPath );

            #if !CUSTOM_BUILD 
            QueueDownload( Globals.VersionURL, Globals.VersionPath );
            #endif

            StartDownloadQueue();
        }

        /// <summary>
        /// Updates the title of the updater form.
        /// </summary>
        public void UpdateTitle()
        {
            if( DownloadCount == 0 )
                Text = "NWOHack :: Check Updates";
            else if( CurrentDownloadPosition <= DownloadCount )
                Text = String.Format( "NWOHack :: Check Updates - Downloading file {0} of {1}...",
                                        CurrentDownloadPosition, DownloadCount );
            else
                Text = "NWOHack :: Check Updates - All downloads complete!";
        }

        /// <summary>
        /// Places download information into the queue.
        /// </summary>
        public void QueueDownload( string sURL, string sSavePath )
        {
            DownloadQueue.Add( new FileDownload( new Uri( sURL ), sSavePath ) );
            ++DownloadCount;
        }

        /// <summary>
        /// Starts the download queue.
        /// </summary>
        public void StartDownloadQueue()
        {
            tmrUpdateDownloadQueue.Enabled = true;
        }

        /// <summary>
        /// Resets all download queue information and cancels any current downloads.
        /// </summary>
        public void ResetDownloadQueue()
        {
            if( IsDownloadActive )
                WebClient.CancelAsync();

            barDownloadProgress.Value = 0;
            tmrUpdateDownloadQueue.Enabled = false;

            CurrentDownloadPosition = 0;
            DownloadCount = 0;
            DownloadQueue.Clear();
            IsDownloadActive = false;
            
            UpdateTitle();
        }

    }
}
