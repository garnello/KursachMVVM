using System.Windows;
using System.Windows.Input;

namespace KursachWPF.View.LogIn;

public partial class RecoveryPasswordView : Window
{
    
    public static RecoveryPasswordView RPWindow;
    public RecoveryPasswordView()
    {
        InitializeComponent();
        RPWindow = this;
    }
    
    private void MovingWindow(object sender, RoutedEventArgs e)
    {
        if (Mouse.LeftButton == MouseButtonState.Pressed) RecoveryPasswordView.RPWindow.DragMove();
    }
}