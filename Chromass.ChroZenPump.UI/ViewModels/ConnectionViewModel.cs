using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Chromass.ChroZenPump.APIs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chromass.ChroZenPump.UI.ViewModels;
public class ConnectionViewModel : ObservableObject
{
    public ConnectionViewModel(APIViewModel apiViewModel)
    {
        APIViewModel = apiViewModel;

        apiViewModel.API.StateUpdated += API_StateUpdated;

        Connect = new AsyncRelayCommand(ConnectExecuteAsync);
        Cancel = new RelayCommand(CancelExecute);


    }

    public APIViewModel APIViewModel
    {
        get; init;
    }

    private void API_StateUpdated(object? sender, State e)
    {
        if (new[] { Statuses.Initializing, Statuses.Halt }.Contains(e.Status))
        {
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(IsVisible));
            OnPropertyChanged(nameof(IsEditable));
        }
    }

    public bool IsConnected => APIViewModel.API.IsConnected;

    private string? address = "localhost";
    public string? Address
    {
        get => address;
        set => SetProperty(ref address, value);
    }

    private int port = 4242;
    public int Port
    {
        get => port;
        set => SetProperty(ref port, value);
    }

    private bool isConnecting;
    public bool IsConnecting
    {
        get => isConnecting;
        set
        {
            SetProperty(ref isConnecting, value);
            OnPropertyChanged(nameof(IsEditable));
        }
    }

    public bool IsVisible => !IsConnected;

    public bool IsEditable => !IsConnecting && !IsConnected;

    public ICommand Connect
    {
        get;
    }

    private async Task ConnectExecuteAsync()
    {
        IsConnecting = true;

        await APIViewModel.API.ConnectAsync(Address!, Port, CancellationToken.None);

        IsConnecting = false;
    }

    public ICommand Cancel
    {
        get;
    }

    private void CancelExecute() => APIViewModel.API.Close();
}
