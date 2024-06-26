using KursachWPF.Models;
using KursachWPF.View.Auth.NavigationPanel;
using KursachWPF.View.MainWindowView;
using KursachWPF.View.MainWindowView.MainWindowPages;
using KursachWPF.View.MainWindowView.NavigationPanel;
using KursachWPF.ViewModels.AuthVM;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.NavPanelVM;

public class NavigationPanelVM : ViewModelBase
{
    private object _currentView;
    private KursachContext _db;
    public static NavigationPanel NPanel;
    public static NavAuth NWindow;
    private string _fullName;
    
    public string FullName
    {
        get { return _fullName; }
        set { _fullName = value; OnPropertyChanged(FullName); }
    }

    public object CurrentView
    {
        get { return _currentView; }
        set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
    }
    
    public RelayCommand HomeCommand { get; set; }
    public RelayCommand ListCommand { get; set; }
    public RelayCommand SettingCommand { get; set; }
    public RelayCommand CurrentAccountCommand { get; set; }
    
    private void Home(object obj) => CurrentView = new Home();
    private void List(object obj) => CurrentView = new List();
    private void Setting(object obj) => CurrentView = new Setting();
    private void CurrentAccount(object obj) => CurrentView = new CurrentAccount();

    public NavigationPanelVM()
    {
        // Connection
        _db = new KursachContext();
        
        // Command
        HomeCommand = new RelayCommand(Home);
        ListCommand = new RelayCommand(List);
        SettingCommand = new RelayCommand(Setting);
        CurrentAccountCommand = new RelayCommand(CurrentAccount);

        // Startup Page
        CurrentView = new Home();
        
        // Content account
        NWindow = new NavAuth();
        
        var account = _db.Accounts.FirstOrDefault(u => u.Login == LoginVM._login);
        FullName = $"{account.Surname} {account.Name}";
    }
}