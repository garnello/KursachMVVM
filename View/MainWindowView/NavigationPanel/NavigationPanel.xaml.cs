using System.Windows;
using System.Windows.Input;
using NavAuth = KursachWPF.View.Auth.NavigationPanel.NavAuth;

namespace KursachWPF.View.MainWindowView.NavigationPanel;

public partial class NavigationPanel : Window
{
    public static NavigationPanel NPanel;
    public static NavAuth NWindow;
    public NavigationPanel()
    {
        InitializeComponent();
    }
    
    private void BtnMinimize_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    
    private void BtnMaximized_OnClick(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
        else WindowState = WindowState.Normal;
        
    }

    private void BtnClose_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void MovingWindow(object sender, RoutedEventArgs e)
    {
        if (Mouse.LeftButton == MouseButtonState.Pressed) NPanel.DragMove();
    }

    private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
    {
        NPanel.Close();
        NWindow.Show();
    }
}