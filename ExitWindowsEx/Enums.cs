using System;

/// <summary>
/// Contains enumerations representing values for system shutdown operations and reasons.
/// </summary>
internal struct Enums
{
    /// <summary>
    /// Enumeration for system shutdown operations that can be called by the ExitWindowsEx method.
    /// </summary>
    [Flags]
    internal enum ExitWindows : uint
    {
        LogOff = 0x0u,
        ShutDown = 0x1u,
        Reboot = 0x2u,
        PowerOff = 0x8u,
        RestartApps = 0x40u,
        Hybrid = 0x400000u,
        Force = 0x4u,
        ForceIfHung = 0x10u
    }

    /// <summary>
    /// Enumeration representing reasons for system shutdown that can be used with the ExitWindowsEx method.
    /// </summary>
    internal enum ShutdownReason : uint
    {
        MajorApplication = 0x40000u,
        MajorHardware = 0x10000u,
        MajorLegacyApi = 0x70000u,
        MajorOperatingSystem = 0x20000u,
        MajorOther = 0x0u,
        MajorPower = 0x60000u,
        MajorSoftware = 0x30000u,
        MajorSystem = 0x50000u,
        MinorBlueScreen = 0xFu,
        MinorCordUnplugged = 0xBu,
        MinorDisk = 0x7u,
        MinorEnvironment = 0xCu,
        MinorHardwareDriver = 0xDu,
        MinorHotfix = 0x11u,
        MinorHung = 0x5u,
        MinorInstallation = 0x2u,
        MinorMaintenance = 0x1u,
        MinorMMC = 0x19u,
        MinorNetworkConnectivity = 0x14u,
        MinorNetworkCard = 0x9u,
        MinorOther = 0x0u,
        MinorOtherDriver = 0xEu,
        MinorPowerSupply = 0xAu,
        MinorProcessor = 0x8u,
        MinorReconfig = 0x4u,
        MinorSecurity = 0x13u,
        MinorSecurityFix = 0x12u,
        MinorSecurityFixUninstall = 0x18u,
        MinorServicePack = 0x10u,
        MinorServicePackUninstall = 0x16u,
        MinorTermSrv = 0x20u,
        MinorUnstable = 0x6u,
        MinorUpgrade = 0x3u,
        MinorWMI = 0x15u,
        FlagUserDefined = 0x40000000u,
        FlagPlanned = 0x80000000u
    }
}
