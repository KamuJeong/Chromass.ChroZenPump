using System.Reflection;
using CDS.InstrumentModel;
using Chromass.ChroZenPump.UI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Chromass.ChroZenPump.UI.Views;

public sealed partial class MainPage : Page
{
    private UIElement? monitor;

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();

        ViewModel.APIViewModel.ConnectionViewModel.PropertyChanged += ConnectionViewModel_PropertyChanged;
    }

    private void ConnectionViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (ViewModel.APIViewModel.ConnectionViewModel.IsVisible)
        {
            VisualStateManager.GoToState(this, "NotConnected", false);

            if (monitor is not null)
            {
                ViewStack.Children.Remove(monitor);
                monitor = null;
            }

            //ConnectionPage.Opacity = 1;
            //MainView.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
        else if (ConnectionPage.Opacity == 1)
        {
            VisualStateManager.GoToState(this, "Connected", true);
            //            ConnectionPageAnimation.Begin();

            if (monitor is null)
            {
                var viewType = ViewModel.Device.GetType().GetCustomAttributes<ReferAttribute>().Where(a => a.Key == "Monitor").First().Type;
                var view = viewType?.GetConstructor(new Type[] { typeof(Device) }).Invoke(new object[] { ViewModel.Device });
                if (view is UIElement ele)
                {
                    ViewStack.Children.Add(ele);
                    monitor = ele;
                }
            }
        }
    }
}
