using System.Runtime.InteropServices;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class TypeDefinitions
    {

        // Entity manager

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        public delegate DWORD_PTR EntityUtil_EntGetTarget_t( DWORD_PTR dwPointer );

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        public delegate DWORD_PTR EntityUtil_GetEntNameToFit_t( DWORD_PTR dwPointer, bool bIncludeLastName, int nMaxLength );

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        public delegate DWORD_PTR EntityUtil_GetPlayerEnt_t();



        // Functions

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        public delegate void UIGen_Chat_AddMessage_t( DWORD_PTR dwMessage, uint unk1 );

        [UnmanagedFunctionPointer( CallingConvention.Cdecl, CharSet = CharSet.Ansi )]
        public delegate DWORD_PTR UIGen_Chat_PrepareMessage_t( uint unk1, uint unk2, ChatLogEntryType uEntryType, uint unk3, string sMessage, uint unk4 );
        
    }
}
