using System.Windows.Input;
using KursachWPF.View.MainWindowView;
using KursachWPF.View.MainWindowView.AccountView;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.MainWindowVM.AccountVM;


public class NavigationAccountVM : ViewModelBase
{
    private object _accountView;
    public object AccountView
    {
        get { return _accountView; }
        set { _accountView = value; OnPropertyChanged(); }
    }
    
    public ICommand AvatarCommand { get; set; }
    public ICommand EmailCommand { get; set; }
    public ICommand PasswordCommand { get; set; }
    
    
    public void Avatar(object obj) => AccountView = new Avatar();
    private void Email(object obj) => AccountView = new Email();
    private void Password(object obj) => AccountView = new Password();


    public NavigationAccountVM()
    {
        AvatarCommand = new RelayCommand(Avatar);
        EmailCommand = new RelayCommand(Email);
        PasswordCommand = new RelayCommand(Password);

        // Startup Page
        AccountView = new Avatar();
    }
}