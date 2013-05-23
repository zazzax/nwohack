using System;

#if WIN32
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public class Entity
    {

        public readonly DWORD_PTR Pointer;

        public Entity( DWORD_PTR dwPointer )
        {
            Pointer = dwPointer;
        }

        /// <summary>
        /// Obtains the entity's first or full name.
        /// </summary>
        public string GetName( bool bIncludeLastName = true )
        {
            return Process.Read<string>( Delegates.EntityUtil_GetEntNameToFit( Pointer, bIncludeLastName, Int32.MaxValue ) );
        }

        /// <summary>
        /// Obtains the entity's target entity.
        /// </summary>
        /// <returns></returns>
        public Entity GetTarget()
        {
            DWORD_PTR dwTargetPointer = Delegates.EntityUtil_EntGetTarget( Pointer );
            if( dwTargetPointer == 0 )
                return null;

            if( !Manager.Entities.ContainsKey( dwTargetPointer ) )
                Manager.Entities.Add( dwTargetPointer, new Entity( dwTargetPointer ) );

            return Manager.Entities[dwTargetPointer];
        }

    }
}
