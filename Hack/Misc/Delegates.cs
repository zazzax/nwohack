#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Delegates
    {

        public static TypeDefinitions.UIGen_Chat_AddMessage_t UIGen_Chat_AddMessage;
        public static TypeDefinitions.UIGen_Chat_PrepareMessage_t UIGen_Chat_PrepareMessage;

        public static void Initialize()
        {
            UIGen_Chat_AddMessage = Process.RegisterDelegate<TypeDefinitions.UIGen_Chat_AddMessage_t>( Offsets.Functions.UIGen_Chat_AddMessage );
            UIGen_Chat_PrepareMessage = Process.RegisterDelegate<TypeDefinitions.UIGen_Chat_PrepareMessage_t>( Offsets.Functions.UIGen_Chat_PrepareMessage );
        }

    }
}
