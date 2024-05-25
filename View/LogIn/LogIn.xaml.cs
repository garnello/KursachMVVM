using System.Windows;
using System.Windows.Input;
using KursachWPF.ViewModels.Authorization;

namespace KursachWPF.View.LogIn;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LogIn
{
    public static LogIn LWindow;

    public LogIn()
    {
        DataContext = new LoginVM();
        InitializeComponent();
        LWindow = this;
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
        if (Mouse.LeftButton == MouseButtonState.Pressed) LogIn.LWindow.DragMove();
    }

    private void RecoveryPasswordWindow(object sender, RoutedEventArgs e)
    {
        RecoveryPasswordView RPWindow = new RecoveryPasswordView();
        RPWindow.Show();
    }
}