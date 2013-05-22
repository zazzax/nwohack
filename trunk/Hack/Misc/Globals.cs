using SystemProcess = System.Diagnostics.Process;

namespace Hack
{
    public static class Globals
    {

        public static SystemProcess CurrentProcess;

        /// <summary>
        /// Initializes global variables for use with NWOHack.
        /// </summary>
        public static void Initialize()
        {
            CurrentProcess = SystemProcess.GetCurrentProcess();
        }

    }
}
