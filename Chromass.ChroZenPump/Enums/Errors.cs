namespace Chromass.ChroZenPump;

public enum Errors
{
    None = 0,
    Config,
    Setup,
    Service,
    HighPressure,
    LowPressure,
    HighFlow,
    LowFlow,
    Leakage,
    Degassor,
    Motor,
    Bubble
}

public static class ErrorsExtension
{
    public static string Detail(this Errors error)
    {
        switch (error)
        {
            case Errors.Config: return "Configuration fail";
            case Errors.Setup: return "Setup fail";
            case Errors.Service: return "Calibration fail";
            case Errors.HighPressure: return "High pressure limit error";
            case Errors.LowPressure: return "Low pressure limit error";
            case Errors.HighFlow: return "High flow limit error";
            case Errors.LowFlow: return "Low flow limit error";
            case Errors.Leakage: return "Leakage error";
            case Errors.Degassor: return "Degassor error";
            case Errors.Motor: return "Motor error";
            case Errors.Bubble: return "Bubble warning";
        }
        return "Unknown error";
    }
}
