using System.Windows.Input;
using CDS.Chromass.ChroZenPump;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chromass.ChroZenPump.UI.ViewModels;

public class MainViewModel : ObservableRecipient
{
    public ChroZenPumpDevice Device
    {
        get; init;
    }

    public APIViewModel APIViewModel    
    {
        get; set;
    }

    public MainViewModel()
    {
        Device = new ChroZenPumpDevice(null, null);
        APIViewModel = new APIViewModel(Device.API);

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
