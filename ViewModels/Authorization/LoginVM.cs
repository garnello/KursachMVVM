using System.Windows;
using KursachWPF.Models;
using KursachWPF.View;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.Authorization;
public class LoginVM : ViewModelBase
{
    private readonly KursachContext _db;
    public static string _login;
    private static string _password;

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged(Login);
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(Password);
        }
    }

    public RelayCommand LoginCommand { get; }
    
    public void OnLogin(object parameter)
    {
        
        var user = _db.Accounts.FirstOrDefault(u => u.Login == _login && u.Password == _password);
        if (user != null)
        {
            MessageBox.Show("Login successful!");
            View.MainWindowView.NavigationPanel.MainWindow mw = new View.MainWindowView.NavigationPanel.MainWindow();
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            currentWindow.Close();
            mw.ShowDialog();
            
        }
        else
        {
            MessageBox.Show("Invalid username or password");
        }
    }

    public LoginVM()
    {
        _db = new KursachContext();
        LoginCommand = new RelayCommand(OnLogin);
    }
}
