using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Process
    {

        /// <summary>
        /// Read a value of any type from memory.
        /// </summary>
        private static unsafe T ReadInternal<T>( DWORD_PTR dwAddress )
        {
            Type tType = typeof( T );

            if( tType == typeof( bool ) )
                return (T)(object) *(bool*) dwAddress;
            if( tType == typeof( byte ) )
                return (T)(object) *(byte*) dwAddress;
            if( tType == typeof( char ) )
                return (T)(object) *(char*) dwAddress;
            if( tType == typeof( double ) )
                return (T)(object) *(double*) dwAddress;
            if( tType == typeof( int ) )
                return (T)(object) *(int*) dwAddress;
            if( tType == typeof( long ) )
                return (T)(object) *(long*) dwAddress;
            if( tType == typeof( short ) )
                return (T)(object) *(short*) dwAddress;
            if( tType == typeof( float ) )
                return (T)(object) *(float*) dwAddress;
            if( tType == typeof( uint ) )
                return (T)(object) *(uint*) dwAddress;
            if( tType == typeof( ulong ) )
                return (T)(object) *(ulong*) dwAddress;
            if( tType == typeof( ushort ) )
                return (T)(object) *(ushort*) dwAddress;
            if( tType == typeof( string ) )
                return (T)(object) Marshal.PtrToStringAnsi( (IntPtr) dwAddress );

            throw new Exception( "[Process.ReadInternal]: Value type was not supported for reading!" );
        }

        /// <summary>
        /// Read a struct from memory.
        /// </summary>
        public static T ReadStruct<T>( DWORD_PTR dwAddress ) where T : struct
        {
            return (T) Marshal.PtrToStructure( (IntPtr) dwAddress, typeof( T ) );
        }

        /// <summary>
        /// Read a byte array from memory.
        /// </summary>
        public static byte[] ReadBuffer( DWORD_PTR dwAddress, int nLength )
        {
            List<byte> liBuffer = new List<byte>( nLength );
            for( int i = 0; i < nLength; i++ )
                liBuffer.Add( Read<byte>( dwAddress + (DWORD_PTR) i ) );

            return liBuffer.ToArray();
        }

        /// <summary>
        /// Read a value of any type from memory.
        /// </summary>
        public static T Read<T>( params DWORD_PTR[] dwAddresses )
        {
            if( dwAddresses.Length == 0 )
                throw new Exception( "[Process.Read]: Read address was not specified!" );

            if( dwAddresses.Length == 1 )
                return ReadInternal<T>( dwAddresses[0] );

            DWORD_PTR dwLast = 0;
            for( int i = 0; i < dwAddresses.Length; i++ )
            {
                if( i == dwAddresses.Length - 1 )
                    return ReadInternal<T>( dwAddresses[i] + dwLast );

                dwLast = ReadInternal<DWORD_PTR>( dwLast + dwAddresses[i] );
            }

            // Should never get to this point.
            return default( T );
        }

        /// <summary>
        /// Writes a value of any type to memory.
        /// </summary>
        private static unsafe void WriteInternal<T>( DWORD_PTR dwAddress, T tValue, bool bProtect )
        {
            Type tType = typeof( T );
            MemoryProtection dwOldProtect = 0;

            if( tType == typeof( string ) )
            {
                string sData = (string)(object) tValue;
                byte[] bBuffer = new byte[sData.Length + 1];

                for( int i = 0; i < sData.Length; i++ )
                    bBuffer[i] = (byte) sData[i];

                if( bProtect )
                    WriteBufferProtected( dwAddress, bBuffer );
                else
                    WriteBuffer( dwAddress, bBuffer );

                return;
            }

            if( bProtect )
                WinAPI.VirtualProtect( (IntPtr) dwAddress, 8, MemoryProtection.ExecuteReadWrite, out dwOldProtect );

            if( tType == typeof( bool ) )
                *(bool*) dwAddress = (bool)(object) tValue;
            else if( tType == typeof( byte ) )
                *(byte*) dwAddress = (byte)(object) tValue;
            else if( tType == typeof( char ) )
                *(char*) dwAddress = (char)(object) tValue;
            else if( tType == typeof( double ) )
                *(double*) dwAddress = (double)(object) tValue;
            else if( tType == typeof( int ) )
                *(int*) dwAddress = (int)(object) tValue;
            else if( tType == typeof( long ) )
                *(long*) dwAddress = (long)(object) tValue;
            else if( tType == typeof( short ) )
                *(short*) dwAddress = (short)(object) tValue;
            else if( tType == typeof( float ) )
                *(float*) dwAddress = (float)(object) tValue;
            else if( tType == typeof( uint ) )
                *(uint*) dwAddress = (uint)(object) tValue;
            else if( tType == typeof( ulong ) )
                *(ulong*) dwAddress = (ulong)(object) tValue;
            else if( tType == typeof( ushort ) )
                *(ushort*) dwAddress = (ushort)(object) tValue;
            else
            {
                if( bProtect )
                    WinAPI.VirtualProtect( (IntPtr) dwAddress, 8, dwOldProtect, out dwOldProtect );
                throw new Exception( "[Process.WriteInternal]: Value type \"" + tType + "\" was not supported for writing!" );
            }

            if( bProtect )
                WinAPI.VirtualProtect( (IntPtr) dwAddress, 8, dwOldProtect, out dwOldProtect );
        }

        /// <summary>
        /// Writes a value of any type to memory.
        /// </summary>
        public static void Write<T>( DWORD_PTR dwAddress, T tValue )
        {
            WriteInternal( dwAddress, tValue, false );
        }

        /// <summary>
        /// Writes a value of any type to memory.
        /// </summary>
        public static void WriteProtected<T>( DWORD_PTR dwAddress, T tValue )
        {
            WriteInternal( dwAddress, tValue, true );
        }

        public static void WriteStruct<T>( DWORD_PTR dwAddress, T tValue ) where T : struct
        {
            try
            { Marshal.StructureToPtr( tValue, (IntPtr) dwAddress, true ); }
            catch
            { throw new Exception( "[Process.WriteStruct]: Could not read from struct!" ); }
        }

        /// <summary>
        /// Writes a byte array to memory.
        /// </summary>
        public static void WriteBuffer( DWORD_PTR dwAddress, byte[] bBuffer )
        {
            WriteBuffer( dwAddress, bBuffer, bBuffer.Length );
        }

        /// <summary>
        /// Writes a byte array to memory.
        /// </summary>
        public static unsafe void WriteBuffer( DWORD_PTR dwAddress, byte[] bBuffer, int nLength )
        {
            for( int i = 0; i < nLength; i++ )
                *(byte*)( dwAddress + (DWORD_PTR) i ) = bBuffer[i];
        }

        /// <summary>
        /// Writes a byte array to protected memory.
        /// </summary>
        public static void WriteBufferProtected( DWORD_PTR dwAddress, byte[] bBuffer )
        {
            WriteBufferProtected( dwAddress, bBuffer, bBuffer.Length );
        }

        /// <summary>
        /// Writes a byte array to protected memory.
        /// </summary>
        public static unsafe void WriteBufferProtected( DWORD_PTR dwAddress, byte[] bBuffer, int nLength )
        {
            MemoryProtection dwOldProtect = 0;
            WinAPI.VirtualProtect( (IntPtr) dwAddress, (uint) nLength, MemoryProtection.ExecuteReadWrite, out dwOldProtect );

            for( int i = 0; i < nLength; i++ )
                *(byte*)( dwAddress + (DWORD_PTR) i ) = bBuffer[i];

            WinAPI.VirtualProtect( (IntPtr) dwAddress, (uint) nLength, dwOldProtect, out dwOldProtect );
        }


        /// <summary>
        /// Registers a function into a delegate.
        /// </summary>
        public static T RegisterDelegate<T>( DWORD_PTR dwAddress ) where T : class
        {
            return Marshal.GetDelegateForFunctionPointer( (IntPtr) dwAddress, typeof( T ) ) as T;
        }

        /// <summary>
        /// Sets a flag to memory.
        /// </summary>
        public static unsafe void SetFlag( DWORD_PTR dwAddress, uint dwFlags )
        {
            *(uint*) dwAddress |= dwFlags;
        }

        /// <summary>
        /// Sets a flag to memory.
        /// </summary>
        public static void SetFlag( DWORD_PTR dwAddress, Enum dwFlags )
        {
            SetFlag( dwAddress, Convert.ToUInt32( dwFlags ) );
        }

        /// <summary>
        /// Unsets a flag from memory.
        /// </summary>
        public static unsafe void UnsetFlag( DWORD_PTR dwAddress, uint dwFlags )
        {
            *(uint*) dwAddress &= ~dwFlags;
        }

        /// <summary>
        /// Unsets a flag from memory.
        /// </summary>
        public static void UnsetFlag( DWORD_PTR dwAddress, Enum dwFlags )
        {
            UnsetFlag( dwAddress, Convert.ToUInt32( dwFlags ) );
        }

        /// <summary>
        /// Determines whether a flag is set in memory.
        /// </summary>
        public static unsafe bool IsFlagSet( DWORD_PTR dwAddress, uint dwFlags )
        {
            return ( *(uint*) dwAddress & dwFlags ) == dwFlags;
        }

        /// <summary>
        /// Determines whether a flag is set in memory.
        /// </summary>
        public static bool IsFlagSet( DWORD_PTR dwAddress, Enum dwFlags )
        {
            return IsFlagSet( dwAddress, Convert.ToUInt32( dwFlags ) );
        }

        /// <summary>
        /// Basic memory data compare function used by FindPattern.
        /// </summary>
        private static unsafe bool DataCompare( DWORD_PTR dwAddress, byte[] bBytes, IEnumerable<bool> bMask )
        {
            return !bMask.Where( ( t, i ) => t && bBytes[i] != *(byte*)( dwAddress + (DWORD_PTR) i ) ).Any();
        }

        /// <summary>
        /// Basic find pattern function.
        /// This is NOT safe. It may cause an access violation when you don't specify proper lengths.
        /// </summary>
        public static DWORD_PTR FindPattern( DWORD_PTR dwAddress, uint uLength, string sPattern )
        {
            string[] sSplit = sPattern.Split( ' ' );
            int nIndex = 0;
            byte[] bBytes = new byte[sSplit.Length];
            bool[] bMask = new bool[sSplit.Length];
            foreach( string sToken in sSplit )
            {
                if( sToken.Length > 2 )
                    throw new InvalidDataException( "[Process.FindPattern]: Invalid token: " + sToken + "." );
                if( sToken.Contains( "?" ) )
                    bMask[nIndex++] = false;
                else
                {
                    byte bData = Byte.Parse( sToken, NumberStyles.HexNumber );
                    bBytes[nIndex] = bData;
                    bMask[nIndex] = true;
                    nIndex++;
                }
            }

            for( uint i = 0; i < uLength; i++ )
            {
                if( DataCompare( dwAddress + i, bBytes, bMask ) )
                    return ( dwAddress + i );
            }

            throw new InvalidDataException( "[Process.FindPattern]: Pattern not found." );
        }

        /// <summary>
        /// Gets VirtualMethod from given vTableIndex.
        /// </summary>
        public static unsafe DWORD_PTR GetVirtualMethod( DWORD_PTR dwOffset, int nVTableIndex )
        {
            DWORD_PTR** __vmt = (DWORD_PTR**) dwOffset;
            return ( *__vmt )[nVTableIndex];
        }

        public class Detour
        {
            private Delegate OriginalFunction_t, NewFunction_t;
            private DWORD_PTR OriginalFunctionAddress, NewFunctionAddress;
            private List<byte> OriginalBytes, NewBytes;

            public bool IsApplied { get; private set; }

            /// <summary>
            /// Creates a detour which may be applied to functions, to have them redirect to other specified functions.
            /// </summary>
            public Detour( Delegate tOriginalFunction, Delegate tNewFunction )
            {
                OriginalFunction_t = tOriginalFunction;
                OriginalFunctionAddress = (DWORD_PTR) Marshal.GetFunctionPointerForDelegate( OriginalFunction_t );
                OriginalBytes = new List<byte>( ReadBuffer( OriginalFunctionAddress, 2 + sizeof( DWORD_PTR ) ) );

                NewFunction_t = tNewFunction;
                NewFunctionAddress = (DWORD_PTR) Marshal.GetFunctionPointerForDelegate( NewFunction_t );
                NewBytes = new List<byte>( 2 + sizeof( DWORD_PTR ) );
                NewBytes.Add( 0x68 );
                NewBytes.AddRange( BitConverter.GetBytes( NewFunctionAddress ) );
                NewBytes.Add( 0xC3 );

                IsApplied = false;
            }

            /// <summary>
            /// Applies the detour.
            /// </summary>
            public void Apply()
            {
                WriteBufferProtected( OriginalFunctionAddress, NewBytes.ToArray() );
                IsApplied = true;
            }

            /// <summary>
            /// Removes the detour.
            /// </summary>
            public void Remove()
            {
                WriteBufferProtected( OriginalFunctionAddress, OriginalBytes.ToArray() );
                IsApplied = false;
            }

            /// <summary>
            /// Calls the original, unaltered function.
            /// </summary>
            public T CallOriginal<T>( params object[] oArgs )
            {
                Remove();
                object tResult = OriginalFunction_t.DynamicInvoke( oArgs );
                Apply();
                return (T) tResult;
            }

        }
    }
}
