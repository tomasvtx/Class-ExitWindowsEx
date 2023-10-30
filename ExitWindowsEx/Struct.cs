using System;
using System.Runtime.InteropServices;

namespace ExitWindowsEx
{
    internal class Struct
    {
        /// <summary>
        /// Struktura reprezentující LUID (Lokální unikátní identifikátor).
        /// </summary>
        internal struct Luid
        {
            /// <summary>
            /// Dolní část LUID
            /// </summary>
            internal uint LowPart;

            /// <summary>
            /// Horní část LUID
            /// </summary>
            internal int HighPart;  
        }

        /// <summary>
        /// Struktura reprezentující LUID a atributy.
        /// </summary>
        internal struct LUIDAttributes
        {
            /// <summary>
            /// LUID (Lokální unikátní identifikátor)
            /// </summary>
            internal Luid Luid;

            /// <summary>
            /// Atributy
            /// </summary>
            internal uint Attributes; 
        }

        /// <summary>
        /// Struktura reprezentující oprávnění v procesním tokenu.
        /// </summary>
        internal struct TokenPrivileges
        {
            /// <summary>
            /// Počet oprávnění
            /// </summary>
            public uint PrivilegeCount;

            /// <summary>
            /// Pole oprávnění
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUIDAttributes[] Privileges;  
        }
    }
}
