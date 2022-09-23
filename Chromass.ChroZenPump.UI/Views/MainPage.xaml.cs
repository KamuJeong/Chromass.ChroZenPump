using Chromass.ChroZenPump.UI.ViewModels;

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
    }
}
