
using System.Windows.Input;
using KursachWPF.View.Auth;
using KursachWPF.View.Auth.NavigationPanel;
using KursachWPF.View.MainWindowView;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.AuthVM;

public class NavigationAuthVM : ViewModelBase
{
    public object _authView;
    
    public object AuthView
    {
        get { return _authView; }
        set 
        { 
            _authView = value; 
            OnPropertyChanged(nameof(AuthView)); 
        }
    }
    
    public ICommand LogInViewCommand { get; set; }
    public ICommand RecoveryPasswordViewCommand { get; set; }
    public ICommand SignUpViewCommand { get; set; }
    
    private void LogIn(object obj) => AuthView = new LogInView();
    private void RecoveryPasswordView(object obj) => AuthView = new RecoveryPasswordView();
    private void SigUp(object obj) => AuthView = new SignUpView();
    

    public NavigationAuthVM()
    {
        LogInViewCommand = new RelayCommand(LogIn);
        SignUpViewCommand = new RelayCommand(SigUp);
        RecoveryPasswordViewCommand = new RelayCommand(RecoveryPasswordView);
        // Startup Page
        AuthView = new LogInView();
    }
}