using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Launcher
{
    public partial class frmMain : Form
    {

        private Dictionary<string, Process> ProcessList = new Dictionary<string, Process>();

        #region Form events
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load( object sender, EventArgs e )
        {
            Globals.CheckUpdatesForm.ShowDialog( this );
            RefreshProcessList();
        }

        private void menuOptions_File_Exit_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void menuOptions_Tools_Inject_Click( object sender, EventArgs e )
        {
            InjectCurrentProcess();
        }

        private void menuOptions_Tools_Eject_Click( object sender, EventArgs e )
        {
            EjectCurrentProcess();
        }

        private void menuOptions_Tools_Refresh_Click( object sender, EventArgs e )
        {
            RefreshProcessList();
        }

        private void menuOptions_Help_CheckUpdates_Click( object sender, EventArgs e )
        {
            Globals.CheckUpdatesForm.ShowDialog( this );
        }

        private void menuOptions_Help_About_Click( object sender, EventArgs e )
        {
            Globals.AboutForm.ShowDialog( this );
        }

        private void lstProcesses_DoubleClick( object sender, EventArgs e )
        {
            ToggleInjectionCurrentProcess();
        }
        #endregion

        private void RefreshProcessList()
        {
            ProcessList.Clear();
            lstProcesses.Items.Clear();

            foreach( Process pProcess in Process.GetProcessesByName( "GameClient" ) )
            {
                string sPID = pProcess.Id.ToString();
                ProcessList.Add( sPID, pProcess );

                IntPtr hProcess = Memory.OpenProcess( pProcess.Id );
                DWORD_PTR dwBase = (DWORD_PTR) pProcess.MainModule.BaseAddress;

                string sPlayerName = Memory.Read<bool>( hProcess, dwBase + Offsets.General.IsInGame ) ?
                    Memory.Read( hProcess, dwBase + Offsets.General.PlayerName, CharSet.Ansi ) : "Not in-game";

                Memory.CloseHandle( hProcess );

                bool bIsInjected = false;
                foreach( ProcessModule pModule in pProcess.Modules )
                {
                    if( pModule.ModuleName != "Hack.dll" )
                        continue;

                    bIsInjected = true;
                    break;
                }

                ListViewItem pListItem = lstProcesses.Items.Add( sPID );
                pListItem.SubItems.Add( "GameClient.exe" );
                pListItem.SubItems.Add( sPlayerName );
                pListItem.SubItems.Add( bIsInjected ? "Yes" : "No" );
            }
        }

        private void ToggleInjectionCurrentProcess()
        {
            if( lstProcesses.SelectedItems.Count != 1 )
                return;

            if( lstProcesses.SelectedItems[0].SubItems[3].Text == "No" )
                InjectCurrentProcess();
            else
                EjectCurrentProcess();
        }

        private void InjectCurrentProcess()
        {
            if( lstProcesses.SelectedItems.Count != 1 || lstProcesses.SelectedItems[0].SubItems[3].Text != "No" )
                return;

            if( InjectProcess( ProcessList[lstProcesses.SelectedItems[0].Text] ) )
                lstProcesses.SelectedItems[0].SubItems[3].Text = "Yes";
        }

        private void EjectCurrentProcess()
        {
            if( lstProcesses.SelectedItems.Count != 1 || lstProcesses.SelectedItems[0].SubItems[3].Text != "Yes" )
                return;

            if( EjectProcess( ProcessList[lstProcesses.SelectedItems[0].Text] ) )
                lstProcesses.SelectedItems[0].SubItems[3].Text = "No";
        }

        private bool InjectProcess( Process pProcess )
        {
            if( !File.Exists( Globals.ModulePath ) )
                return false;

            IntPtr hProcess = Memory.OpenProcess( pProcess.Id );
            if( hProcess == IntPtr.Zero )
                return false;

            DWORD_PTR dwResult = Memory.LoadLibraryEx( hProcess, Globals.ModulePath );
            Memory.CloseHandle( hProcess );

            if( dwResult == 0 )
                Functions.PlaySound( "Resources\\Error.wav" );
            else
                Functions.PlaySound( "Resources\\Success.wav" );

            return dwResult != 0;
        }

        private bool EjectProcess( Process pProcess )
        {
            return false;
        }

    }
}
