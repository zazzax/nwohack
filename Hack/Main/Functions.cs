using System;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Functions
    {

        /// <summary>
        /// Adds a formatted message to the player's chat window.
        /// </summary>
        public static void AddChatMessage( string sFormat, params object[] sParams )
        {
            string sMessage = String.Format( sFormat, sParams );
            DWORD_PTR dwMessage = Delegates.UIGen_Chat_PrepareMessage( 0, 0, ChatLogEntryType.System, 0, sMessage, 0 );
            Delegates.UIGen_Chat_AddMessage( dwMessage, 0 );
        }

    }
}
