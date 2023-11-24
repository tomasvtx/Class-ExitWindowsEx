using static Enums;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;

namespace ExitWindowsEx
{
    /// <summary>
    /// Basic structure for executing system shutdown operations.
    /// </summary>
    internal struct ApiBase
    {
        /// <summary>
        /// Executes the system shutdown operation with the specified parameters.
        /// </summary>
        /// <param name="uFlags">Type of system shutdown operation.</param>
        /// <param name="process">Process name associated with the operation.</param>
        internal static void Execute(ExitWindows uFlags, string process)
        {
            IntPtr tokenHandle = IntPtr.Zero;

            try
            {
                // Opens the process token representing the process's permissions and attributes.
                if (!Dll.OpenProcessToken(Process.GetCurrentProcess().Handle, 40u, out tokenHandle))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to open the process token.");
                }

                Struct.TokenPrivileges tokenPrivileges = default;
                tokenPrivileges.PrivilegeCount = 1u;
                tokenPrivileges.Privileges = new Struct.LUIDAttributes[1];
                Struct.TokenPrivileges newState = tokenPrivileges;
                newState.Privileges[0].Attributes = 2u;

                // Retrieves the privilege value (LUID) for the specified privilege.
                if (!Dll.LookupPrivilegeValue(null, "SeShutdownPrivilege", out newState.Privileges[0].Luid))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to retrieve the privilege value.");
                }

                // Modifies the privileges in the process token.
                if (!Dll.AdjustTokenPrivileges(tokenHandle, false, ref newState, 0u, IntPtr.Zero, IntPtr.Zero))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to adjust privileges in the process token.");
                }

                // Executes the system shutdown operation.
                if (!Dll.ExitWindowsEx(uFlags, ShutdownReason.MajorApplication | ShutdownReason.MinorInstallation | ShutdownReason.FlagPlanned))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), $"Failed to perform the {process} system operation.");
                }
            }
            finally
            {
                // Closes the opened object (token).
                if (tokenHandle != IntPtr.Zero)
                {
                    Dll.CloseHandle(tokenHandle);
                }
            }
        }
    }
}
