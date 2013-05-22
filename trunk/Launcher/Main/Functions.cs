using System.IO;
using System.Media;

namespace Launcher
{
    public static class Functions
    {

        private static SoundPlayer SoundPlayer = new SoundPlayer();

        public static void PlaySound( string sFileName )
        {
            if( !File.Exists( sFileName ) || !sFileName.EndsWith( ".wav" ) )
                return;

            SoundPlayer.SoundLocation = sFileName;
            SoundPlayer.Play();
        }

    }
}
