using System.Collections.Generic;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Manager
    {

        public static Dictionary<DWORD_PTR, Entity> Entities = new Dictionary<DWORD_PTR, Entity>();

        /// <summary>
        /// Updates the entity list.
        /// </summary>
        public static void Update()
        {
            Entities.Clear();
            for( uint i = 0; i < 0x2800; i++ )
            {
                DWORD_PTR dwPointer = Process.Read<DWORD_PTR>( Offsets.EntityManager.MainEntityList + ( i * 4 ) );
                if( dwPointer != 0 && ( Process.Read<uint>( dwPointer ) & 0xFFFF ) == i )
                    Entities.Add( dwPointer, new Entity( dwPointer ) );
            }
        }

        /// <summary>
        /// Obtains the local player entity.
        /// </summary>
        public static Entity GetLocalPlayer()
        {
            DWORD_PTR dwPointer = Delegates.EntityUtil_GetPlayerEnt();
            if( dwPointer == 0 )
                return null;

            if( !Entities.ContainsKey( dwPointer ) )
                Entities.Add( dwPointer, new Entity( dwPointer ) );

            return Entities[dwPointer];
        }

    }
}
