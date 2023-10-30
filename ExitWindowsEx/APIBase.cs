using static Enums;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;

namespace ExitWindowsEx
{
    /// <summary>
    /// Základní struktura pro vykonávání operací ukončení systému.
    /// </summary>
    internal struct ApiBase
    {
        /// <summary>
        /// Spustí operaci ukončení systému s danými parametry.
        /// </summary>
        /// <param name="uFlags">Typ operace ukončení systému.</param>
        /// <param name="proces">Název procesu spojený s operací.</param>
        internal static void Execute(ExitWindows uFlags, string proces)
        {
            IntPtr tokenHandle = IntPtr.Zero;

            try
            {
                // Otevře token procesu, který reprezentuje oprávnění a atributy procesu.
                if (!Dll.OpenProcessToken(Process.GetCurrentProcess().Handle, 40u, out tokenHandle))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Nepodařilo se otevřít token procesu.");
                }

                Struct.TokenPrivileges tokenPrivileges = default;
                tokenPrivileges.PrivilegeCount = 1u;
                tokenPrivileges.Privileges = new Struct.LUIDAttributes[1];
                Struct.TokenPrivileges newState = tokenPrivileges;
                newState.Privileges[0].Attributes = 2u;

                // Získá hodnotu oprávnění (LUID) pro dané oprávnění.
                if (!Dll.LookupPrivilegeValue(null, "SeShutdownPrivilege", out newState.Privileges[0].Luid))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Nepodařilo se získat hodnotu oprávnění.");
                }

                // Upraví oprávnění v procesním tokenu.
                if (!Dll.AdjustTokenPrivileges(tokenHandle, false, ref newState, 0u, IntPtr.Zero, IntPtr.Zero))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Nepodařilo se upravit oprávnění v procesním tokenu.");
                }

                // Spustí operaci ukončení systému.
                if (!Dll.ExitWindowsEx(uFlags, ShutdownReason.MajorApplication | ShutdownReason.MinorInstallation | ShutdownReason.FlagPlanned))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), $"Nepodařilo se provést operaci {proces} systému.");
                }
            }
            finally
            {
                // Uzavře otevřený objekt (token).
                if (tokenHandle != IntPtr.Zero)
                {
                    Dll.CloseHandle(tokenHandle);
                }
            }

        }
    }
}
