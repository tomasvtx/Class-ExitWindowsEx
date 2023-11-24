using static Enums;
using System;

namespace ExitWindowsEx
{
    /// <summary>
    /// SecurityStructures for controlling various system shutdown operations on a PC.
    /// </summary>
    public struct PCShutdown
    {
        /// <summary>
        /// Logs off the PC.
        /// </summary>
        public static void LogOff() => ExecuteSafe(ExitWindows.LogOff, "LogOff");

        /// <summary>
        /// Shuts down the PC.
        /// </summary>
        public static void ShutDown() => ExecuteSafe(ExitWindows.ShutDown, "ShutDown");

        /// <summary>
        /// Restarts the PC.
        /// </summary>
        public static void Reboot() => ExecuteSafe(ExitWindows.Reboot, "Reboot");

        /// <summary>
        /// Powers off the PC.
        /// </summary>
        public static void PowerOff() => ExecuteSafe(ExitWindows.PowerOff, "PowerOff");

        /// <summary>
        /// Restarts applications and processes on the PC.
        /// </summary>
        public static void RestartApps() => ExecuteSafe(ExitWindows.RestartApps, "RestartApps");

        /// <summary>
        /// Starts the hybrid mode on the PC.
        /// </summary>
        public static void Hybrid() => ExecuteSafe(ExitWindows.Hybrid, "Hybrid");

        /// <summary>
        /// Tries to start the hybrid mode; if it fails, shuts down the PC.
        /// </summary>
        /// <returns>True if the hybrid mode was started successfully; otherwise, false.</returns>
        public static bool TryHybrid()
        {
            try
            {
                ExecuteSafe(ExitWindows.Hybrid, "Hybrid");
                return true;
            }
            catch
            {
                ExecuteSafe(ExitWindows.ShutDown, "ShutDown");
                return false;
            }
        }

        /// <summary>
        /// Forces the termination of applications and processes, followed by shutting down the PC.
        /// </summary>
        public static void Force() => ExecuteSafe(ExitWindows.Force, "Force");

        /// <summary>
        /// Forces the termination of applications and processes if they are frozen, followed by shutting down the PC.
        /// </summary>
        public static void ForceIfHung() => ExecuteSafe(ExitWindows.ForceIfHung, "ForceIfHung");

        // Execute a shutdown operation and handle exceptions
        private static void ExecuteSafe(ExitWindows operation, string operationName)
        {
            try
            {
                ApiBase.Execute(operation, operationName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing {operationName} operation: {ex.Message}");
                // Add additional logging or error handling as needed
            }
        }
    }
}
