#if WIN32
using System.Collections.Generic;
using System.Windows.Forms;
using DWORD_PTR = System.UInt32;
#elif WIN64
using DWORD_PTR = System.UInt64;
#endif

namespace Hack
{
    public static class Detours
    {
        
        public static Dictionary<string, Process.Detour> List = new Dictionary<string, Process.Detour>();  

        /// <summary>
        /// Initializes all detours for use, adds them to the list, and applies them.
        /// </summary>
        public static void Initialize()
        {
            ApplyAllDetours();
        }

        /// <summary>
        /// Applies all detours in the list.
        /// </summary>
        public static void ApplyAllDetours()
        {
        }

        /// <summary>
        /// Removes all detours in the list.
        /// </summary>
        public static void RemoveAllDetours()
        {
        }

    }
}
