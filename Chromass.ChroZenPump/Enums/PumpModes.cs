namespace Chromass.ChroZenPump
{
    public enum PumpModes : byte
    {
        Quarternary = 0,
        Binary,
        Isocratic,
        // for Multi-Link
        Elute0 = 0x00,     // %A
        Elute1 = 0x10,     // %B
        Elute2 = 0x20,     // %C
        Elute3 = 0x30,     // %D
    }
}
