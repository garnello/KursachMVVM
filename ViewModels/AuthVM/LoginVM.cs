using System.Windows;
using System.Windows.Input;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.View.Auth;
using KursachWPF.View.MainWindowView.NavigationPanel;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.AuthVM;
public class LoginVM : ViewModelBase
{
    private readonly KursachContext _db;
    public static string _login;
    private static string _password;
    private string _correctInput;
    
    public string Login
    {
        get => _login;
        set { _login = value; OnPropertyChanged(Login); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(Password); }
    }
    
    public string CorrectInput
    {
        get => _correctInput;
        set { _correctInput = value; OnPropertyChanged(CorrectInput); }
    }
    
    public RelayCommand LogInCommand { get; }
    
    public void OnLogin(object parameter)
    {
        
        Account user = _db.Accounts.FirstOrDefault(u => u.Login == _login && u.Password == _password);
        if (user != null)
        {
            NavigationPanel mw = new NavigationPanel();
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            currentWindow.Close();
            mw.ShowDialog();
            
        }
        else
        {
            CorrectInput =  "Неверный логин или пароль";
            OnPropertyChanged(nameof(CorrectInput));
        }
    }


    public LoginVM()
    {
        _db = new KursachContext();
        LogInCommand = new RelayCommand(OnLogin);
    }
}
