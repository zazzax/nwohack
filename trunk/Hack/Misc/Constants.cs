namespace Hack
{

    /// <summary>
    /// Chat entry types - compatible with UIGen_PrepareMessage.
    /// </summary>
    public enum ChatLogEntryType : uint
    {
        Unknown = 0x0,
        Admin = 0x1,
        Channel = 0x2,
        ChatSystem = 0x3,
        Error = 0x4,
        Spy = 0x5,
        CombatSelf = 0x6,
        CombatTeam = 0x7,
        CombatOther = 0x8,
        Friend = 0x9,
        Inventory = 0xA,
        Mission = 0xB,
        NPC = 0xC,
        Reward = 0xD,
        RewardMinor = 0xE,
        System = 0xF,
        Guild = 0x10,
        Local = 0x11,
        Officer = 0x12,
        Private = 0x13,
        Private_Sent = 0x14,
        Team = 0x15,
        TeamUp = 0x16,
        Zone = 0x17,
        Match = 0x18,
        Global = 0x19,
        Minigame = 0x1A,
        Emote = 0x1B,
        Events = 0x1C,
        LootRolls = 0x1D,
        NeighborhoodChange = 0x1E
    }

}
