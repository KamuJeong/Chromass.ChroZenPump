using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chromass.ChroZenPump.UI.ViewModels;
public class APIViewModel : ObservableObject
{
    public API API
    {
        get; init;
    }

    public ConnectionViewModel ConnectionViewModel
    {
        get; init;
    }

    public APIViewModel(API api)
    {
        API = api;
        API.StateUpdated += API_StateUpdated;

        ConnectionViewModel = new ConnectionViewModel(this);

    }

    private void API_StateUpdated(object? sender, APIs.State e)
    {
        if (API.IsConnected != IsConnected)
        {
            IsConnected = API.IsConnected;
            ConnectionViewModel.IsVisible = !IsConnected;
            if (!IsConnected)
                ConnectionViewModel.IsEditable = true; 
        }
    }

    public bool isConnected;
    public bool IsConnected
    {
        get => isConnected;
        set => SetProperty(ref isConnected, value);
    }
}
