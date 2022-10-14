using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CDS.Chromass.ChroZenPump.ViewModels;
using CDS.Core;
using CDS.InstrumentModel;
using Chromass.ChroZenPump;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CDS.Chromass.ChroZenPump.Views;
public sealed partial class MonitorView : UserControl
{
    public MonitorView(Device device)
    {
        ViewModel = new MonitorViewModel(device);

        this.InitializeComponent();

        ViewModel.Device.API.MessageReceived += API_MessageReceived;
    }

    private void API_MessageReceived(object? sender, global::Chromass.ChroZenPump.APIs.Message e)
    {
        if (e.Type == MessageTypes.State)
        {
            switch ((Statuses)e.NewValue)
            {
                case Statuses.Error:    VisualStateManager.GoToState(this, "Error", false); break;
                case Statuses.Run:      VisualStateManager.GoToState(this, "Run", false); break;
                default:                VisualStateManager.GoToState(this, "Normal", false); break;
            }
        }
    }

    private MonitorViewModel? ViewModel
    {
        get;
    }
}
