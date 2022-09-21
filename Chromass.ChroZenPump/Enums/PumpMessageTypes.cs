namespace Chromass.ChroZenPump
{
    public enum PumpMessageTypes : byte
	{
		None = 0,
		State,
		ExtIn,
		ExtOut, // not used
		Error,
	}
}
