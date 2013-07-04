#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Offsets
    {

        public static class EntityManager
        {
            public static DWORD_PTR
                MainEntityList                      = 0x02239BE0,
                EntityUtil_EntGetTarget             = 0x006267A0,
                EntityUtil_GetEntNameToFit          = 0x002EAB00,
                EntityUtil_GetPlayerEnt             = 0x006235F0;
        }

        public static class Functions
        {
            public static DWORD_PTR
                UIGen_Chat_AddMessage               = 0x0066B9B0,
                UIGen_Chat_PrepareMessage           = 0x004B6B80;
        }

        public static class Patches
        {
            public static DWORD_PTR
                NoStamina                           = 0x0050F3F9;
        }

        public static void Initialize()
        {
            DWORD_PTR dwBase = WinAPI.GetModuleHandle( null );

            EntityManager.MainEntityList += dwBase;
            EntityManager.EntityUtil_EntGetTarget += dwBase;
            EntityManager.EntityUtil_GetEntNameToFit += dwBase;
            EntityManager.EntityUtil_GetPlayerEnt += dwBase;

            Functions.UIGen_Chat_AddMessage += dwBase;
            Functions.UIGen_Chat_PrepareMessage += dwBase;

            Patches.NoStamina += dwBase;
        }

    }
}
