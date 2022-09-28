using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chromass.ChroZenPump.UI.ViewModels;

public class InformationViewModel : ObservableObject
{
    public APIViewModel APIViewModel
    {
        get; init;
    }

    public InformationViewModel(APIViewModel apiViewModel)
    {
        APIViewModel = apiViewModel;

        APIViewModel.API.Controller.Information.Updated += (s, e) => OnPropertyChanged(string.Empty);
    }

    public string? Model => APIViewModel.API.Information.Model;
    public int Version => APIViewModel.API.Information.Version;
    public string? SN => APIViewModel.API.Information.SN;
}
