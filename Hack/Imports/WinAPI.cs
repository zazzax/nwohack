using System;
using System.Runtime.InteropServices;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class WinAPI
    {

        [DllImport( "kernel32.dll" )]
        public static extern DWORD_PTR GetModuleHandle( string sModule );

        [DllImport( "kernel32.dll" )]
        public static extern bool VirtualProtect( IntPtr lpAddress, uint dwSize, MemoryProtection flNewProtect, out MemoryProtection lpflOldProtect );

    }
}
