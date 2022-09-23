using CommunityToolkit.Mvvm.ComponentModel;

namespace Chromass.ChroZenPump.UI.ViewModels;

public class MainViewModel : ObservableRecipient
{
    public APIViewModel APIViewModel    
    {
        get; init;
    }

    public MainViewModel(APIViewModel apiViewModel)
    {
        APIViewModel = apiViewModel;
    }
}
