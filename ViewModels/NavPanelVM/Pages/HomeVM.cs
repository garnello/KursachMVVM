using KursachWPF.Models.Context;
using KursachWPF.View.MainWindowView.MainWindowPages;
using KursachWPF.ViewModels.AuthVM;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.NavPanelVM;

public class HomeVM : ViewModelBase
{
    private KursachContext _db;
    private string _name;
    
    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(Name); }
    }

    public HomeVM()
    {
        _db = new KursachContext();
        var account = _db.Accounts.FirstOrDefault(u => u.Login == LoginVM._login);
        Name = $"Добро пожаловать, {account.Name}!";
    }
}