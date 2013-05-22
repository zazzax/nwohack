using System.Windows.Forms;

namespace Hack
{
    public static class Program
    {

        public static int Initialize( string sArg )
        {
            Globals.Initialize();
            Offsets.Initialize();
            Delegates.Initialize();

            Functions.AddChatMessage( "NWOHack loaded successfully!" );
            return 0;
        }

    }
}
