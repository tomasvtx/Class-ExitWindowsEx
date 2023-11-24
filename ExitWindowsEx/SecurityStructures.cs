using System;
using System.Runtime.InteropServices;

namespace ExitWindowsEx
{
    /// <summary>
    /// Contains structures representing LUID (Locally Unique Identifier) and token privileges.
    /// </summary>
    internal class SecurityStructures
    {
        /// <summary>
        /// Represents the LUID (Locally Unique Identifier).
        /// </summary>
        internal struct LocallyUniqueIdentifier
        {
            /// <summary>
            /// The low part of the LUID.
            /// </summary>
            internal uint LowPart;

            /// <summary>
            /// The high part of the LUID.
            /// </summary>
            internal int HighPart;
        }

        /// <summary>
        /// Represents the combination of LUID and attributes.
        /// </summary>
        internal struct LuidAttributes
        {
            /// <summary>
            /// The LUID (Locally Unique Identifier).
            /// </summary>
            internal LocallyUniqueIdentifier Luid;

            /// <summary>
            /// The attributes.
            /// </summary>
            internal uint Attributes;
        }

        /// <summary>
        /// Represents the privileges in the process token.
        /// </summary>
        internal struct TokenPrivileges
        {
            /// <summary>
            /// The count of privileges.
            /// </summary>
            public uint PrivilegeCount;

            /// <summary>
            /// An array of privileges.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LuidAttributes[] Privileges;
        }
    }
}
