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

    public SystemViewModel SystemViewModel
    {
        get; init;
    }

    public ControlViewModel ControlViewModel
    {

        get; init;
    }

    public APIViewModel(API api)
    {
        API = api;

        ConnectionViewModel = new ConnectionViewModel(this);
        SystemViewModel = new SystemViewModel(this);
        ControlViewModel = new ControlViewModel(this);
    }
}
