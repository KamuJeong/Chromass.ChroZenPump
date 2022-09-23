using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chromass.ChroZenPump.UI.ViewModels;
public class ConnectionViewModel : ObservableObject
{
    public ConnectionViewModel(APIViewModel apiViewModel)
    {
        APIViewModel = apiViewModel;

        Connect = new AsyncRelayCommand(ConnectExecuteAsync);
        Cancel = new RelayCommand(CancelExecute);
    }

    public APIViewModel APIViewModel
    {
        get; init;
    }

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

    private bool isVisible = true;
    public bool IsVisible
    {
        get => isVisible;
        set => SetProperty(ref isVisible, value);
    }

    private bool isEditable = true;
    public bool IsEditable
    {
        get => isEditable;
        set => SetProperty(ref isEditable, value);
    }

    private bool isConnecting;
    public bool IsConnecting
    {
        get => isConnecting;
        set => SetProperty(ref isConnecting, value);
    }

    public ICommand Connect
    {
        get;
    }

    private async Task ConnectExecuteAsync()
    {
        IsEditable = false;
        IsConnecting = true;

        await APIViewModel.API.ConnectAsync(Address!, Port, CancellationToken.None);

        IsConnecting = false;
        if (!APIViewModel.IsConnected)
        {
            IsEditable = true;
        }
    }

    public ICommand Cancel
    {
        get;
    }

    private void CancelExecute() => APIViewModel.API.Close();
}
