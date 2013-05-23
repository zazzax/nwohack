#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Delegates
    {

        public static TypeDefinitions.EntityUtil_EntGetTarget_t EntityUtil_EntGetTarget;
        public static TypeDefinitions.EntityUtil_GetEntNameToFit_t EntityUtil_GetEntNameToFit;
        public static TypeDefinitions.EntityUtil_GetPlayerEnt_t EntityUtil_GetPlayerEnt;

        public static TypeDefinitions.UIGen_Chat_AddMessage_t UIGen_Chat_AddMessage;
        public static TypeDefinitions.UIGen_Chat_PrepareMessage_t UIGen_Chat_PrepareMessage;

        /// <summary>
        /// Initializes delegate functions.
        /// </summary>
        public static void Initialize()
        {
            EntityUtil_EntGetTarget = Process.RegisterDelegate<TypeDefinitions.EntityUtil_EntGetTarget_t>( Offsets.EntityManager.EntityUtil_EntGetTarget );
            EntityUtil_GetEntNameToFit = Process.RegisterDelegate<TypeDefinitions.EntityUtil_GetEntNameToFit_t>( Offsets.EntityManager.EntityUtil_GetEntNameToFit );
            EntityUtil_GetPlayerEnt = Process.RegisterDelegate<TypeDefinitions.EntityUtil_GetPlayerEnt_t>( Offsets.EntityManager.EntityUtil_GetPlayerEnt );

            UIGen_Chat_AddMessage = Process.RegisterDelegate<TypeDefinitions.UIGen_Chat_AddMessage_t>( Offsets.Functions.UIGen_Chat_AddMessage );
            UIGen_Chat_PrepareMessage = Process.RegisterDelegate<TypeDefinitions.UIGen_Chat_PrepareMessage_t>( Offsets.Functions.UIGen_Chat_PrepareMessage );
        }

    }
}
