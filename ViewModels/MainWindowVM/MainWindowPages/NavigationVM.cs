using System.Windows.Input;
using KursachWPF.View.MainWindowView;
using KursachWPF.View.MainWindowView.MainWindowPages;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.MainWindowVM.MainWindowPages;

public class NavigationVM : ViewModelBase
{
    private object _currentView;
    public object CurrentView
    {
        get { return _currentView; }
        set { _currentView = value; OnPropertyChanged(); }
    }
    
    public RelayCommand HomeCommand { get; set; }
    public RelayCommand ListCommand { get; set; }
    public RelayCommand SettingCommand { get; set; }
    public RelayCommand CurrentAccountCommand { get; set; }
    
    private void Home(object obj) => CurrentView = new Home();
    private void List(object obj) => CurrentView = new List();
    private void Setting(object obj) => CurrentView = new Setting();
    private void CurrentAccount(object obj) => CurrentView = new CurrentAccount();

    public NavigationVM()
    {
        HomeCommand = new RelayCommand(Home);
        ListCommand = new RelayCommand(List);
        SettingCommand = new RelayCommand(Setting);
        CurrentAccountCommand = new RelayCommand(CurrentAccount);

        // Startup Page
        CurrentView = new Home();
    }
}