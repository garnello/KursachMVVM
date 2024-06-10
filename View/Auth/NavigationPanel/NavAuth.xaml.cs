using System.Windows;
using System.Windows.Input;
using KursachWPF.ViewModels.AuthVM;

namespace KursachWPF.View.Auth.NavigationPanel;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class NavAuth
{
    public static NavAuth NWindow;

    public NavAuth()
    {
        
        InitializeComponent();
        NWindow = this;
        
    }
    

    private void BtnMinimize_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    
    private void MovingWindow(object sender, RoutedEventArgs e)
    {
        if (Mouse.LeftButton == MouseButtonState.Pressed) NWindow.DragMove();
    }
}