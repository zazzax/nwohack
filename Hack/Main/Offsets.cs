#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Offsets
    {

        public static class Functions
        {
            public static DWORD_PTR
                UIGen_Chat_AddMessage               = 0x00668C00,
                UIGen_Chat_PrepareMessage           = 0x004B4950;
        }

        public static void Initialize()
        {
            DWORD_PTR dwBase = WinAPI.GetModuleHandle( null );

            Functions.UIGen_Chat_AddMessage += dwBase;
            Functions.UIGen_Chat_PrepareMessage += dwBase;
        }

    }
}
