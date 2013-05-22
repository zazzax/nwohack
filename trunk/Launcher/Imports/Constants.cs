using System;

namespace Launcher
{

    /// <summary>
    /// Memory allocation flags - compatible with WinAPI functions.
    /// </summary>
    [Flags]
    public enum AllocationType : uint
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    /// <summary>
    /// Page protection flags - compatible with WinAPI functions.
    /// </summary>
    [Flags]
    public enum MemoryProtection : uint
    {
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }

    /// <summary>
    /// Memory freeing flags - compatible with WinAPI functions.
    /// </summary>
    [Flags]
    public enum FreeType : uint
    {
        Decommit = 0x4000,
        Release = 0x8000
    }

    /// <summary>
    /// Process access flags - compatible with WinAPI functions.
    /// </summary>
    [Flags]
    public enum ProcessAccessFlags : uint
    {
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VMOperation = 0x00000008,
        VMRead = 0x00000010,
        VMWrite = 0x00000020,
        DupHandle = 0x00000040,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        Synchronize = 0x00100000
    }

    /// <summary>
    /// Thread access flags - compatible with WinAPI functions.
    /// </summary>
    [Flags]
    public enum ThreadAccessFlags : uint
    {
        StandardRightsRequired = 0x000F0000,
        Synchronize = 0x00100000,
        AllAccess = 0x001F03FF
    }

}
