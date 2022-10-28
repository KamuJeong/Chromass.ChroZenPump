using System.Reflection;
using CDS.Core;
using CDS.InstrumentModel;
using Chromass.ChroZenPump.UI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Chromass.ChroZenPump.UI.Views;

public sealed partial class MainPage : Page
{
//    private UIElement? monitor;

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();

        var controller = ViewModel.ContollerView as FrameworkElement;
        ContentArea.Children.Add(controller);


//        ViewModel.APIViewModel.ConnectionViewModel.PropertyChanged += ConnectionViewModel_PropertyChanged;
    }

    //private void ConnectionViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    //{
    //    if (ViewModel.APIViewModel.ConnectionViewModel.IsVisible)
    //    {
    //        VisualStateManager.GoToState(this, "NotConnected", false);

    //        if (monitor is not null)
    //        {
    //            ViewStack.Children.Remove(monitor);
    //            monitor = null;
    //        }

    //        //ConnectionPage.Opacity = 1;
    //        //MainView.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
    //    }
    //    else if (ConnectionPage.Opacity == 1)
    //    {
    //        VisualStateManager.GoToState(this, "Connected", true);
    //        //            ConnectionPageAnimation.Begin();

    //        if (monitor is null)
    //        {
    //            if (ViewModel.Device.CreateReferInstance("Monitor") is UIElement ele)
    //            {
    //                ViewStack.Children.Add(ele);
    //                monitor = ele;
    //            }
    //        }
    //    }
    //}
}
