using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.View.MainWindowView.AccountView;
using KursachWPF.ViewModels.Authorization;
using KursachWPF.ViewModels.MainWindowVM.AccountVM.Methods;
using KursachWPF.ViewModels.MainWindowVM.MainWindowPages;

namespace KursachWPF.View.MainWindowView.NavigationPanel;

public partial class MainWindow : Window
{
    private KursachContext db;
    public static MainWindow MWindow;
    public static LogIn.LogIn LWindow;

    public MainWindow()
    {
        db = new KursachContext();
        InitializeComponent();
        MWindow = this;
        LWindow = new LogIn.LogIn();
        
        string loggedLogin = LoginVM._login;
        var account = db.Accounts.FirstOrDefault(u => u.Login == loggedLogin);
        string fullName = $"{account.Surname} {account.Name}";
        TextFullName.Text = fullName;
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
        if (Mouse.LeftButton == MouseButtonState.Pressed) MainWindow.MWindow.DragMove();
    }

    private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
    {
        MWindow.Close();
        LWindow.Show();
    }
}