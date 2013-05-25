using System;
using System.IO;
using System.Media;
using System.Security.Cryptography;

namespace Launcher
{
    public static class Functions
    {

        private static SoundPlayer SoundPlayer = new SoundPlayer();
        private static MD5CryptoServiceProvider CryptoService = new MD5CryptoServiceProvider();

        /// <summary>
        /// Plays a wave sound file.
        /// </summary>
        public static void PlaySound( string sFileName )
        {
            if( !File.Exists( sFileName ) || !sFileName.EndsWith( ".wav" ) )
                return;

            SoundPlayer.SoundLocation = sFileName;
            SoundPlayer.Play();
        }

        /// <summary>
        /// Gets the MD5 hash of a file.
        /// </summary>
        public static string MD5( string sFilePath )
        {
            FileStream fsStream = null;

            try
            {
                fsStream = new FileStream( sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );
                byte[] bHashValue = CryptoService.ComputeHash( fsStream );
                fsStream.Close();

                return BitConverter.ToString( bHashValue ).Replace( "-", "" );
            }
            catch
            {
                if( fsStream != null )
                    fsStream.Close();

                return "00000000000000000000000000000000";
            }
        }

    }
}
