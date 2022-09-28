using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        Close = new RelayCommand(CloseExecute);
    }

    public ICommand Close
    {
        get; init;
    }

    private void CloseExecute()
    {
        APIViewModel.API.Close();
    }

}
