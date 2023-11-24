using System;
using System.Runtime.InteropServices;
using static Enums;

namespace ExitWindowsEx
{
    /// <summary>
    /// Provides methods for calling system shutdown operations, managing process tokens, and working with privileges.
    /// </summary>
    internal struct Dll
    {
        /// <summary>
        /// Calls the system shutdown operations (e.g., shutdown or restart).
        /// </summary>
        /// <param name="uFlags">The type of system shutdown operation.</param>
        /// <param name="dwReason">The shutdown reason.</param>
        /// <returns>Returns true if the operation is successful; otherwise, false.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ExitWindowsEx(ExitWindows uFlags, Enums.ShutdownReason dwReason);

        /// <summary>
        /// Opens the process token, which represents the permissions and attributes of the process.
        /// </summary>
        /// <param name="processHandle">The process handle.</param>
        /// <param name="desiredAccess">The desired access.</param>
        /// <param name="tokenHandle">The resulting token handle.</param>
        /// <returns>Returns true if successful; otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        /// <summary>
        /// Retrieves the privilege value (LUID) for the specified privilege.
        /// </summary>
        /// <param name="systemName">The system name.</param>
        /// <param name="name">The privilege name.</param>
        /// <param name="luid">The resulting LUID.</param>
        /// <returns>Returns true if successful; otherwise, false.</returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(string systemName, string name, out SecurityStructures.LocallyUniqueIdentifier luid);

        /// <summary>
        /// Closes the opened object (e.g., token).
        /// </summary>
        /// <param name="handle">The handle to the object to be closed.</param>
        /// <returns>Returns true if successful; otherwise, false.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Adjusts privileges in the process token.
        /// </summary>
        /// <param name="tokenHandle">The process token handle.</param>
        /// <param name="disableAllPrivileges">A flag indicating whether to disable all privileges.</param>
        /// <param name="newState">The new state of privileges.</param>
        /// <param name="zero">Reserved; set to zero.</param>
        /// <param name="null1">Reserved; set to IntPtr.Zero.</param>
        /// <param name="null2">Reserved; set to IntPtr.Zero.</param>
        /// <returns>Returns true if successful; otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(IntPtr tokenHandle, [MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges, ref SecurityStructures.TokenPrivileges newState, uint zero, IntPtr null1, IntPtr null2);
    }
}