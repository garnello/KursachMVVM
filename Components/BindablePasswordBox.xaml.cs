using System.Windows;
using System.Windows.Controls;

namespace KursachWPF.Components;

public partial class BindablePasswordBox : UserControl
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox) ,
            new PropertyMetadata(string.Empty));

    public string Password
    {
        get { return (string)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }
    public BindablePasswordBox()
    {
        InitializeComponent();
        
    }

    private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = passwordBox.Password;
    }
}