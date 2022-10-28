using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public MonitorViewModel? ViewModel
    {
        get; private set;
    }

    public MonitorView(Device device)
    {
        ViewModel = new MonitorViewModel(device);

        this.InitializeComponent();

        new WeakEventSubscriber<MonitorView, PropertyChangedEventArgs>(this,
            (s, e) =>
            {
                if (e.PropertyName == "VisualState")
                {
                    VisualStateManager.GoToState(this, ViewModel.VisualState, false);
                }
            },
            h => ViewModel.PropertyChanged += new PropertyChangedEventHandler(h),
            h => ViewModel.PropertyChanged -= new PropertyChangedEventHandler(h));
    }
}
