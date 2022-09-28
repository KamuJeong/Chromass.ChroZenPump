namespace Chromass.ChroZenPump
{
    public enum MessageTypes : byte
	{
		None = 0,
		State,
		ExtIn,
		ExtOut, // not used
		Error,
	}
}
