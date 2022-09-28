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

    public InformationViewModel InformationViewModel
    {
        get; init;
    }
    public APIViewModel(API api)
    {
        API = api;

        ConnectionViewModel = new ConnectionViewModel(this);
        InformationViewModel = new InformationViewModel(this);

    }
}
