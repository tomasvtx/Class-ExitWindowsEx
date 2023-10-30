using System;
using System.Runtime.InteropServices;
using static Enums;

namespace ExitWindowsEx
{
    /// <summary>
    /// Poskytuje metody pro volání operací ukončení systému, správu tokenů procesu a práci s oprávněními.
    /// </summary>
    internal struct Dll
    {
        /// <summary>
        /// exitWindowsEx - Volá operace pro ukončení systému (např. vypnout nebo restartovat).
        /// </summary>
        /// <param name="uFlags"></param>
        /// <param name="dwReason"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ExitWindowsEx(ExitWindows uFlags, Enums.ShutdownReason dwReason);

        /// <summary>
        /// openProcessToken - Otevře token procesu, který reprezentuje oprávnění a atributy procesu.
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <param name="DesiredAccess"></param>
        /// <param name="TokenHandle"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        /// <summary>
        /// lookupPrivilegeValue - Získá hodnotu oprávnění (LUID) pro dané oprávnění.
        /// </summary>
        /// <param name="lpSystemName"></param>
        /// <param name="lpName"></param>
        /// <param name="lpLuid"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out Struct.Luid lpLuid);

        /// <summary>
        /// closeHandle - Uzavře otevřený objekt (např. token).
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// adjustTokenPrivileges - Upraví oprávnění v procesním tokenu.
        /// </summary>
        /// <param name="TokenHandle"></param>
        /// <param name="DisableAllPrivileges"></param>
        /// <param name="NewState"></param>
        /// <param name="Zero"></param>
        /// <param name="Null1"></param>
        /// <param name="Null2"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges, ref Struct.TokenPrivileges NewState, uint Zero, IntPtr Null1, IntPtr Null2);
    }
}
