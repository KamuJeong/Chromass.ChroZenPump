using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CDS.Chromass.ChroZenPump.ViewModels;
using CDS.Core;
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
public sealed partial class ConnectionView : UserControl
{
    private ConnectionViewModel? viewModel;
    public ConnectionViewModel? ViewModel
    {
        get => viewModel;
        internal set
        {
            if (viewModel != value)
            {
                viewModel = value;
                if (viewModel != null)
                {
                    new WeakEventSubscriber<ControllerViewModel, ConnectionView, PropertyChangedEventArgs>(
                        viewModel.Controller,
                        nameof(viewModel.Controller.PropertyChanged),
                        this,
                        (sub, s, e) =>
                        {
                            if (e.PropertyName == "VisualState")
                            {
                                if (sub.viewModel?.Controller.VisualState == "Connecting")
                                {
                                    VisualStateManager.GoToState(sub, "Connecting", false);
                                }
                                else if (sub.viewModel?.Controller.VisualState == "NotConnected")
                                {
                                    VisualStateManager.GoToState(sub, "NotConnected", false);
                                }
                                else
                                {
                                    VisualStateManager.GoToState(sub, "Connected", false);
                                }
                            }
                        });
                }
            }
        }
    }

    public ConnectionView()
    {
        this.InitializeComponent();
    }
}
