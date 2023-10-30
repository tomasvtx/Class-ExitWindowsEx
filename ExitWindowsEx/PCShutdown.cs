using static Enums;

namespace ExitWindowsEx
{
    /// <summary>
    /// Struktura pro řízení různých operací ukončení systému na PC.
    /// </summary>
    public struct PCShutdown
    {
        /// <summary>
        /// Provede odhlášení (LogOff) na PC.
        /// </summary>
        public static void LogOff() => ApiBase.Execute(ExitWindows.LogOff, "LogOff");

        /// <summary>
        /// Vypne PC.
        /// </summary>
        public static void ShutDown() => ApiBase.Execute(ExitWindows.ShutDown, "ShutDown");

        /// <summary>
        /// Restartuje PC.
        /// </summary>
        public static void Reboot() => ApiBase.Execute(ExitWindows.Reboot, "Reboot");

        /// <summary>
        /// Vypne napájení PC (Power Off).
        /// </summary>
        public static void PowerOff() => ApiBase.Execute(ExitWindows.PowerOff, "PowerOff");

        /// <summary>
        /// Restartuje aplikace a procesy na PC.
        /// </summary>
        public static void RestartApps() => ApiBase.Execute(ExitWindows.RestartApps, "RestartApps");

        /// <summary>
        /// Spustí hybridní režim na PC.
        /// </summary>
        public static void Hybrid() => ApiBase.Execute(ExitWindows.Hybrid, "Hybrid");

        /// <summary>
        /// Pokusí se spustit hybridní režim, pokud selže, provede vypnutí PC.
        /// </summary>
        /// <returns>True, pokud byl hybridní režim spuštěn úspěšně; jinak false.</returns>
        public static bool TryHybrid()
        {
            try
            {
                ApiBase.Execute(ExitWindows.Hybrid, "Hybrid");
                return true;
            }
            catch
            {
                ApiBase.Execute(ExitWindows.ShutDown, "ShutDown");
                return false;
            }
        }

        /// <summary>
        /// Vynutí ukončení aplikací a procesů a následné vypnutí PC.
        /// </summary>
        public static void Force() => ApiBase.Execute(ExitWindows.Force, "Force");

        /// <summary>
        /// Vynutí ukončení aplikací a procesů, pokud jsou zamrzlé, a následné vypnutí PC.
        /// </summary>
        public static void ForceIfHung() => ApiBase.Execute(ExitWindows.ForceIfHung, "ForceIfHung");
    }
}
