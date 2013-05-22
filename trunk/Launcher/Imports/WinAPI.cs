using System;
using System.Runtime.InteropServices;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Launcher
{
    public static class WinAPI
    {

        [DllImport( "kernel32.dll", CharSet = CharSet.Unicode )]
        public static extern IntPtr GetModuleHandleW( string module );

        [DllImport( "kernel32.dll", CharSet = CharSet.Unicode )]
        public static extern IntPtr LoadLibraryW( string lpFileName );

        [DllImport( "kernel32.dll" )]
        public static extern DWORD_PTR VirtualAllocEx( IntPtr hProcess, DWORD_PTR lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect );

        [DllImport( "kernel32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool VirtualFreeEx( IntPtr hProcess, DWORD_PTR lpAddress, uint dwSize, FreeType dwFreeType );

        [DllImport( "kernel32.dll" )]
        public static extern DWORD_PTR GetProcAddress( IntPtr hModule, string procName );

        [DllImport( "kernel32.dll" )]
        public static extern IntPtr CreateRemoteThread( IntPtr hProcess, uint lpThreadAttributes, uint dwStackSize, DWORD_PTR dwStartAddress, DWORD_PTR dwParameter, uint dwCreationFlags, out uint dwThreadId );

        [DllImport( "kernel32.dll" )]
        public static extern uint WaitForSingleObject( IntPtr hHandle, uint dwMilliseconds );

        [DllImport( "kernel32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool GetExitCodeThread( IntPtr hThread, out uint dwExitCode );

        [DllImport( "kernel32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool CloseHandle( IntPtr hObject );

        [DllImport( "kernel32.dll" )]
        public static extern IntPtr OpenProcess( ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId );

        [DllImport( "kernel32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool WriteProcessMemory( IntPtr hProcess, DWORD_PTR dwAddress, IntPtr lpBuffer, uint dwSize, out uint dwBytesWritten );

        [DllImport( "kernel32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool ReadProcessMemory( IntPtr hProcess, DWORD_PTR dwAddress, IntPtr lpBuffer, uint dwSize, out uint dwBytesRead );

        [DllImport( "kernel32.dll" )]
        public static extern IntPtr OpenThread( ThreadAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwThreadId );

        [DllImport( "kernel32.dll" )]
        public static extern uint SuspendThread( IntPtr hThread );

        [DllImport( "kernel32.dll" )]
        public static extern uint ResumeThread( IntPtr hThread );

    }
}
