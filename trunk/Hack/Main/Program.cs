using System;
using System.Windows.Forms;

namespace Hack
{
    public static class Program
    {

        /// <summary>
        /// Initializes all necessary components of the hack.
        /// </summary>
        public static int Initialize( string sArg )
        {
            try
            {
                Globals.Initialize();
                Offsets.Initialize();
                Delegates.Initialize();
                Detours.Initialize();

                Manager.Update();

                Functions.AddChatMessage( "NWOHack loaded successfully!" );
                Functions.AddChatMessage( "Dumping {0} entities...", Manager.Entities.Count );
                Functions.AddChatMessage( "--------------------------------------" );

                foreach( Entity pEntity in Manager.Entities.Values )
                {
                    Functions.AddChatMessage( "Entity name: {0}", pEntity.GetName() );
                    Functions.AddChatMessage( "Entity pointer: 0x{0:X8}", pEntity.Pointer );
                    Functions.AddChatMessage( "--------------------------------------" );
                }
            }
            catch( Exception e )
            { MessageBox.Show( "NWOHack encountered an exception!\n\n\"" + e.Message + "\"", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error ); }

            return 0;
        }

    }
}
