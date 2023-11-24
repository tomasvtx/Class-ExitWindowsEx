using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static Enums;

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
                    LogError("Failed to open the process token.");
                    return;
                }

                SecurityStructures.TokenPrivileges tokenPrivileges = default;
                tokenPrivileges.PrivilegeCount = 1u;
                tokenPrivileges.Privileges = new SecurityStructures.LuidAttributes[1];
                SecurityStructures.TokenPrivileges newState = tokenPrivileges;
                newState.Privileges[0].Attributes = 2u;

                // Retrieves the privilege value (LUID) for the specified privilege.
                if (!Dll.LookupPrivilegeValue(null, "SeShutdownPrivilege", out newState.Privileges[0].Luid))
                {
                    LogError("Failed to retrieve the privilege value.");
                    return;
                }

                // Modifies the privileges in the process token.
                if (!Dll.AdjustTokenPrivileges(tokenHandle, false, ref newState, 0u, IntPtr.Zero, IntPtr.Zero))
                {
                    LogError("Failed to adjust privileges in the process token.");
                    return;
                }

                // Executes the system shutdown operation.
                if (!Dll.ExitWindowsEx(uFlags, ShutdownReason.MajorApplication | ShutdownReason.MinorInstallation | ShutdownReason.FlagPlanned))
                {
                    LogError($"Failed to perform the {process} system operation.");
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

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="errorMessage">The error message to log.</param>
        private static void LogError(string errorMessage)
        {
            // Add your logging mechanism here (e.g., logging to a file, event log, etc.).
            Console.WriteLine($"Error: {errorMessage}");
        }
    }
}