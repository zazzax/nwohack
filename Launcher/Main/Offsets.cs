#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Launcher
{
    public static class Offsets
    {

        public static class General
        {
            public static DWORD_PTR
                IsInGame                            = 0x02262D00,
                PlayerName                          = 0x022291EC;
        }

    }
}
