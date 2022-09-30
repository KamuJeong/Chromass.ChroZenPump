using Chromass.ChroZenPump.UI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Chromass.ChroZenPump.UI.Views;

public sealed partial class MainPage : Page
{
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

            //ConnectionPage.Opacity = 1;
            //MainView.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
        else if (ConnectionPage.Opacity == 1)
        {
            VisualStateManager.GoToState(this, "Connected", true);
//            ConnectionPageAnimation.Begin();
        }
    }
}
