using System.Windows.Input;
using CDS.Chromass.ChroZenPump;
using CDS.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

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

    public UIElement? MonitorView => Device.CreateReferInstance("Monitor") as UIElement;
    public UIElement? ContollerView => Device.CreateReferInstance("Controller") as UIElement;

    public ICommand Close
    {
        get; init;
    }

    private void CloseExecute()
    {
        APIViewModel.API.Close();
    }

}
