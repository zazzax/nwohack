using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Launcher
{
    public static class Memory
    {

        #region Constants
        private const int MaxStringLength = 512;
        #endregion

        #region Helper functions
        public static uint Reverse( uint uData )
        {
            byte[] bNewBytes = new byte[4];
            bNewBytes[0] = (byte)( uData << 24 >> 24 );
            bNewBytes[1] = (byte)( uData << 16 >> 24 );
            bNewBytes[2] = (byte)( uData << 08 >> 24 );
            bNewBytes[3] = (byte)( uData << 00 >> 24 );

            return BitConverter.ToUInt32( bNewBytes, 0 );
        }
        #endregion

        #region Open/Close
        public static IntPtr OpenProcess( int nProcessId )
        {
            return WinAPI.OpenProcess( ProcessAccessFlags.CreateThread | ProcessAccessFlags.VMOperation |
                                       ProcessAccessFlags.VMRead | ProcessAccessFlags.VMWrite |
                                       ProcessAccessFlags.QueryInformation, false, nProcessId );
        }

        public static IntPtr OpenThread( int nThreadId )
        {
            return WinAPI.OpenThread( ThreadAccessFlags.AllAccess, false, (uint) nThreadId );
        }

        public static void CloseHandle( IntPtr hObject )
        {
            WinAPI.CloseHandle( hObject );
        }
        #endregion

        #region Write
        public static unsafe bool Write<T>( IntPtr hProcess, DWORD_PTR dwAddress, T tData )
        {
            if( typeof( T ) == typeof( string ) )
                return Write( hProcess, dwAddress, (string)(object) tData, CharSet.Ansi );
            
            byte[] bBuffer;
            uint uTypeSize = (uint) Marshal.SizeOf( typeof( T ) );
            switch( uTypeSize )
            {
                case 1:
                    bBuffer = BitConverter.GetBytes( (byte)(object) tData );
                    break;

                case 2:
                    bBuffer = BitConverter.GetBytes( (ushort)(object) tData );
                    break;

                case 4:
                    if( typeof( T ) == typeof( float ) )
                        bBuffer = BitConverter.GetBytes( (float)(object) tData );
                    else
                        bBuffer = BitConverter.GetBytes( (uint)(object) tData );
                    break;

                case 8:
                    bBuffer = BitConverter.GetBytes( (ulong)(object) tData );
                    break;

                default:
                    throw new Exception( "Memory.Write Exception: Invalid type size!" );
            }

            bool bResult = false;
            uint dwBytesWritten = 0;

            fixed( byte* bMemoryBuffer = bBuffer )
                bResult = WinAPI.WriteProcessMemory( hProcess, dwAddress, (IntPtr) bMemoryBuffer, uTypeSize, out dwBytesWritten );

            return bResult && dwBytesWritten == uTypeSize;
        }

        public static unsafe bool Write( IntPtr hProcess, DWORD_PTR dwAddress, string sString, CharSet sCharacterSet )
        {
            byte[] bBuffer;
            if( sCharacterSet == CharSet.None || sCharacterSet == CharSet.Ansi )
                bBuffer = Encoding.ASCII.GetBytes( sString );
            else
                bBuffer = Encoding.Unicode.GetBytes( sString );

            bool bResult = false;
            uint dwBytesWritten = 0;
            uint dwStringLength = (uint) bBuffer.Length;

            fixed( byte* bMemoryBuffer = bBuffer )
                bResult = WinAPI.WriteProcessMemory( hProcess, dwAddress, (IntPtr) bMemoryBuffer, dwStringLength, out dwBytesWritten );

            return bResult && dwBytesWritten == dwStringLength;
        }
        #endregion

        #region Read
        public static T Read<T>( IntPtr hProcess, DWORD_PTR dwAddress )
        {
            if( typeof( T ) == typeof( string ) )
                return (T)(object) Read( hProcess, dwAddress, CharSet.Ansi );

            IntPtr lpBuffer = IntPtr.Zero;
            bool bResult = false;
            uint dwSize = 0;
            uint dwBytesRead = 0;

            try
            {
                dwSize = (uint) Marshal.SizeOf( typeof( T ) );
                lpBuffer = Marshal.AllocHGlobal( (int) dwSize );
                
                bResult = WinAPI.ReadProcessMemory( hProcess, dwAddress, lpBuffer, dwSize, out dwBytesRead );

                if( bResult && dwBytesRead == dwSize )
                    return (T) Marshal.PtrToStructure( lpBuffer, typeof( T ) );
            }
            catch { }

            if( lpBuffer != IntPtr.Zero )
                Marshal.FreeHGlobal( lpBuffer );

            return default( T );
        }

        public static string Read( IntPtr hProcess, DWORD_PTR dwAddress, CharSet sCharacterSet )
        {
            if( sCharacterSet == CharSet.None || sCharacterSet == CharSet.Ansi )
            {
                // One byte character set
                uint uCurrentPosition = 0;
                uint uMaxLength = MaxStringLength;

                byte[] bStringBuffer = new byte[uMaxLength];
                byte bCurrentByte = Read<byte>( hProcess, dwAddress );

                while( bCurrentByte != 0 && uCurrentPosition < uMaxLength )
                {
                    bStringBuffer[uCurrentPosition++] = bCurrentByte;
                    bCurrentByte = Read<byte>( hProcess, dwAddress + uCurrentPosition );
                }

                return Encoding.ASCII.GetString( bStringBuffer );
            }
            else
            {
                // Two byte character set
                uint uCurrentPosition = 0;
                uint uMaxLength = MaxStringLength * 2;

                byte[] bStringBuffer = new byte[uMaxLength];
                byte bFirstByte = Read<byte>( hProcess, dwAddress );
                byte bSecondByte = Read<byte>( hProcess, dwAddress + 1 );

                while( bFirstByte != 0 && bSecondByte != 0 && uCurrentPosition < uMaxLength )
                {
                    bStringBuffer[uCurrentPosition++] = bFirstByte;
                    bStringBuffer[uCurrentPosition++] = bSecondByte;

                    bFirstByte = Read<byte>( hProcess, dwAddress + uCurrentPosition );
                    bSecondByte = Read<byte>( hProcess, dwAddress + uCurrentPosition + 1 );
                }

                return Encoding.Unicode.GetString( bStringBuffer );
            }
        }

        public static byte[] Read( IntPtr hProcess, DWORD_PTR dwAddress, uint dwLength )
        {
            byte[] bBuffer = new byte[dwLength];
            IntPtr lpBuffer = IntPtr.Zero;
            bool bResult = false;
            uint dwBytesRead = 0;

            try
            {
                lpBuffer = Marshal.AllocHGlobal( (int) dwLength );
                bResult = WinAPI.ReadProcessMemory( hProcess, dwAddress, lpBuffer, dwLength, out dwBytesRead );

                if( bResult && dwBytesRead == dwLength )
                    Marshal.Copy( lpBuffer, bBuffer, 0, (int) dwLength );
            }
            catch { }

            if( lpBuffer != IntPtr.Zero )
                Marshal.FreeHGlobal( lpBuffer );

            return bBuffer;
        }
        #endregion

        #region Allocate/Free
        public static DWORD_PTR Allocate( IntPtr hProcess, uint dwSize )
        {
            return WinAPI.VirtualAllocEx( hProcess, 0, dwSize,
                    AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite );
        }

        public static bool Free( IntPtr hProcess, DWORD_PTR dwAddress )
        {
            return WinAPI.VirtualFreeEx( hProcess, dwAddress,
                    0, FreeType.Release );
        }
        #endregion

        #region DLL Injection/Loading
        public static DWORD_PTR LoadLibraryEx( IntPtr hProcess, string sModule )
        {
            DWORD_PTR dwResult = 0;
            IntPtr hKernel32 = WinAPI.GetModuleHandleW( "kernel32.dll" );
            if( hKernel32 != IntPtr.Zero )
            {
                DWORD_PTR dwLoadLibrary = WinAPI.GetProcAddress( hKernel32, "LoadLibraryW" );
                if( dwLoadLibrary != 0 )
                {
                    byte[] bBuffer = Encoding.Unicode.GetBytes( sModule );
                    uint dwBufferLength = (uint) bBuffer.Length + 2;
                    DWORD_PTR dwCodeCave = Memory.Allocate( hProcess, dwBufferLength );
                    if( dwCodeCave != 0 )
                    {
                        if( Memory.Write( hProcess, dwCodeCave, sModule, CharSet.Unicode ) )
                        {
                            uint dwThreadId = 0;
                            IntPtr hThread = WinAPI.CreateRemoteThread( hProcess, 0, 0,
                                    dwLoadLibrary, dwCodeCave, 0, out dwThreadId );

                            if( hThread != IntPtr.Zero )
                            {
                                uint dwWaitResult = WinAPI.WaitForSingleObject( hThread, 0xFFFFFFFF );
                                if( dwWaitResult == 0 )
                                    WinAPI.GetExitCodeThread( hThread, out dwResult );

                                WinAPI.CloseHandle( hThread );
                            }
                        }

                        Memory.Free( hProcess, dwCodeCave );
                    }
                }
            }
            
            return dwResult;
        }

        public static IntPtr Import( string sModule )
        {
            return WinAPI.LoadLibraryW( sModule );
        }
        #endregion

        #region DLL Ejection/Unloading
        public static bool FreeLibraryEx( IntPtr hProcess, IntPtr hRemoteModule )
        {
            uint dwResult = 0;
            IntPtr hKernel32 = WinAPI.GetModuleHandleW( "kernel32.dll" );
            if( hKernel32 != IntPtr.Zero )
            {
                DWORD_PTR dwFreeLibrary = WinAPI.GetProcAddress( hKernel32, "FreeLibrary" );
                if( dwFreeLibrary != 0 )
                {
                    uint dwThreadId = 0;
                    IntPtr hThread = WinAPI.CreateRemoteThread(hProcess, 0, 0,
                            dwFreeLibrary, (DWORD_PTR) hRemoteModule, 0, out dwThreadId );

                    if( hThread != IntPtr.Zero )
                    {
                        uint dwWaitResult = WinAPI.WaitForSingleObject( hThread, 0xFFFFFFFF );
                        if( dwWaitResult == 0 )
                            WinAPI.GetExitCodeThread( hThread, out dwResult );

                        WinAPI.CloseHandle( hThread );
                    }
                }
            }

            return dwResult != 0;
        }
        #endregion

        #region Internal calls
        public static string Call( IntPtr hProcess, CallingConvention cCallingConvention, CharSet cCharacterSet, DWORD_PTR dwAddress, params object[] oParams )
        {
            return Read( hProcess, Call<DWORD_PTR>( hProcess, cCallingConvention, dwAddress, oParams ), cCharacterSet );
        }

        public static T Call<T>( IntPtr hProcess, CallingConvention cCallingConvention, DWORD_PTR dwAddress, params object[] oParams )
        {
            Type tManagedType = typeof( T );

            DWORD_PTR dwExecuteCave = Allocate( hProcess, 1024 );
            DWORD_PTR dwReturnCave = Allocate( hProcess, 12 );
            uint uByteIndex = 0;

            List<object> lsParams = new List<object>( oParams );

            if( cCallingConvention == CallingConvention.ThisCall ||
                cCallingConvention == CallingConvention.FastCall ||
                cCallingConvention == CallingConvention.StdCall )
            {
                // MOV ECX, lsParams[0]
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xB9 );
                Write<DWORD_PTR>( hProcess, dwExecuteCave + uByteIndex, Reverse( (DWORD_PTR) lsParams[0] ) );
                uByteIndex += sizeof( DWORD_PTR );
                lsParams.RemoveAt( 0 );
            }

            lsParams.Reverse();
            foreach( object oParam in lsParams )
            {
                // PUSH oParam
                Type tParamType = oParam.GetType();
                int nSize = Marshal.SizeOf( tParamType );

                if( nSize == 1 )
                    Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0x6A );
                else
                    Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0x68 );

                if( tParamType == typeof( bool ) )
                     Write<bool>( hProcess, dwExecuteCave + uByteIndex, (bool) oParam );
                else if( tParamType == typeof( byte ) )
                    Write<byte>( hProcess, dwExecuteCave + uByteIndex, (byte) oParam );
                else if( tParamType == typeof( char ) )
                    Write<char>( hProcess, dwExecuteCave + uByteIndex, (char) oParam );
                else if( tParamType == typeof( short ) )
                    Write<short>( hProcess, dwExecuteCave + uByteIndex, (short) oParam );
                else if( tParamType == typeof( ushort ) )
                    Write<ushort>( hProcess, dwExecuteCave + uByteIndex, (ushort) oParam );
                else if( tParamType == typeof( int ) )
                    Write<int>( hProcess, dwExecuteCave + uByteIndex, (int) oParam );
                else if( tParamType == typeof( uint ) )
                    Write<uint>( hProcess, dwExecuteCave + uByteIndex, (uint) oParam );
                else if( tParamType == typeof( long ) )
                    Write<long>( hProcess, dwExecuteCave + uByteIndex, (long) oParam );
                else if( tParamType == typeof( ulong ) )
                    Write<ulong>( hProcess, dwExecuteCave + uByteIndex, (ulong) oParam );
                else if( tParamType == typeof( float ) )
                    Write<float>( hProcess, dwExecuteCave + uByteIndex, (float) oParam );
                else if( tParamType == typeof( double ) )
                    Write<double>( hProcess, dwExecuteCave + uByteIndex, (double) oParam );

                uByteIndex += (uint) nSize;
            }

            // MOV EAX, dwAddress
            Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xB8 );
            Write<DWORD_PTR>( hProcess, dwExecuteCave + uByteIndex, Reverse( dwAddress ) );
            uByteIndex += sizeof( DWORD_PTR );

            // CALL EAX
            Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xFF );
            Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xD0 );

            if( cCallingConvention == CallingConvention.Cdecl )
            {
                // ADD ESP, lsParams.Count * 4
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0x83 );
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xC4 );
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, (byte)( lsParams.Count * 4 ) );
            }

            if( tManagedType == typeof( float ) )
            {
                // FSTP DWORD [dwReturnCave]
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xD9 );
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0x1D );
                Write<DWORD_PTR>( hProcess, dwExecuteCave + uByteIndex, Reverse( dwReturnCave ) );
                uByteIndex += sizeof( DWORD_PTR );
            }
            else
            {
                // MOV DWORD [dwReturnCave], EAX
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0xA3 );
                Write<DWORD_PTR>( hProcess, dwExecuteCave + uByteIndex, Reverse( dwReturnCave ) );
                uByteIndex += sizeof( DWORD_PTR );

                // MOV DWORD [dwReturnCave + 4], EDX
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0x89 );
                Write<byte>( hProcess, dwExecuteCave + uByteIndex++, 0x15 );
                Write<DWORD_PTR>( hProcess, dwExecuteCave + uByteIndex, Reverse( dwReturnCave + 4 ) );
                uByteIndex += sizeof( DWORD_PTR );
            }

            // RET
            Write<byte>( hProcess, dwExecuteCave + uByteIndex, 0xC3 );

            uint dwThreadId = 0;
            IntPtr hThread = WinAPI.CreateRemoteThread( hProcess, 0, 0,
                    dwExecuteCave, 0, 0, out dwThreadId );

            if( hThread == IntPtr.Zero )
                throw new Exception( "Memory.Call Exception: Could not call function!" );
            
            WinAPI.WaitForSingleObject( hThread, 0xFFFFFFFF );
            WinAPI.CloseHandle( hThread );

            T tReturn = default( T );
            if( tManagedType == typeof( bool ) )
                tReturn = (T) (object) Read<bool>( hProcess, dwReturnCave );
            if( tManagedType == typeof( byte ) )
                tReturn = (T) (object) Read<byte>( hProcess, dwReturnCave );
            if( tManagedType == typeof( char ) )
                tReturn = (T) (object) Read<char>( hProcess, dwReturnCave );
            if( tManagedType == typeof( short ) )
                tReturn = (T) (object) Read<short>( hProcess, dwReturnCave );
            if( tManagedType == typeof( ushort ) )
                tReturn = (T) (object) Read<ushort>( hProcess, dwReturnCave );
            if( tManagedType == typeof( int ) )
                tReturn = (T) (object) Read<int>( hProcess, dwReturnCave );
            if( tManagedType == typeof( uint ) )
                tReturn = (T) (object) Read<uint>( hProcess, dwReturnCave );
            if( tManagedType == typeof( long ) )
                tReturn = (T) (object) Read<long>( hProcess, dwReturnCave );
            if( tManagedType == typeof( ulong ) )
                tReturn = (T) (object) Read<ulong>( hProcess, dwReturnCave );
            if( tManagedType == typeof( float ) )
                tReturn = (T) (object) Read<float>( hProcess, dwReturnCave );
            if( tManagedType == typeof( double ) )
                tReturn = (T) (object) Read<double>( hProcess, Read<DWORD_PTR>( hProcess, dwReturnCave ) );

            Free( hProcess, dwReturnCave );
            Free( hProcess, dwExecuteCave );

            return tReturn;
        }
        #endregion

        #region External Calls
        public static T CallExport<T>( IntPtr hProcess, IntPtr hImportModule, IntPtr hExportModule, string sExportFunction, uint dwParam = 0 )
        {
            uint dwExitCode = 0;
            DWORD_PTR dwAddress = WinAPI.GetProcAddress( hImportModule, sExportFunction );
            if( dwAddress == 0 )
                throw new Exception( "Memory.CallExport Exception: Export function was not found in import module!" );

            uint dwThreadId = 0;
            dwAddress = dwAddress - (DWORD_PTR) hImportModule + (DWORD_PTR) hExportModule;
            IntPtr hThread = WinAPI.CreateRemoteThread( hProcess, 0, 0,
                    dwAddress, dwParam, 0, out dwThreadId );

            if( hThread == IntPtr.Zero )
                throw new Exception( "Memory.CallExport Exception: Could not call export function!" );

            uint uWaitResult = WinAPI.WaitForSingleObject( hThread, 0xFFFFFFFF );
            if( uWaitResult == 0 )
                WinAPI.GetExitCodeThread( hThread, out dwExitCode );

            WinAPI.CloseHandle( hThread );
            if( uWaitResult != 0 )
                throw new Exception( "Memory.CallExport Exception: Export function never returned!" );

            if( typeof( T ) == typeof( string ) )
                return (T)(object) Read( hProcess, (DWORD_PTR) dwExitCode, CharSet.Ansi );
            else if( typeof( T ) == typeof( byte ) )
                return (T)(object)(byte) dwExitCode;
            else if( typeof( T ) == typeof( char ) )
                return (T)(object)(char) dwExitCode;
            else if( typeof( T ) == typeof( short ) )
                return (T)(object)(short) dwExitCode;
            else if( typeof( T ) == typeof( ushort ) )
                return (T)(object)(ushort) dwExitCode;
            else if( typeof( T ) == typeof( int ) )
                return (T)(object)(int) dwExitCode;
            else if( typeof( T ) == typeof( uint ) )
                return (T)(object)(uint) dwExitCode;
            else if( typeof( T ) == typeof( long ) )
                return (T)(object)(long) dwExitCode;
            else if( typeof( T ) == typeof( ulong ) )
                return (T)(object)(ulong) dwExitCode;
            else if( typeof( T ) == typeof( bool ) )
                return (T)(object) Convert.ToBoolean( dwExitCode );

            throw new Exception( "Memory.CallExport Exception: Could not identify return type!" );
        }

        public static void CallExport( IntPtr hProcess, IntPtr hImportModule, IntPtr hExportModule, string sExportFunction, uint dwParam = 0 )
        {
            DWORD_PTR dwAddress = WinAPI.GetProcAddress( hImportModule, sExportFunction );
            if( dwAddress == 0 )
                throw new Exception( "Memory.CallExport Exception: Export function was not found in import module!" );

            uint dwThreadId = 0;
            dwAddress = dwAddress - (DWORD_PTR) hImportModule + (DWORD_PTR) hExportModule;
            IntPtr hThread = WinAPI.CreateRemoteThread( hProcess, 0, 0,
                    dwAddress, dwParam, 0, out dwThreadId );

            if( hThread == IntPtr.Zero )
                throw new Exception( "Memory.CallExport Exception: Could not call export function!" );

            WinAPI.CloseHandle( hThread );
        }
        #endregion

    }
}