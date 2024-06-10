using System.Collections.ObjectModel;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.ViewModels.General;
using Microsoft.EntityFrameworkCore;

namespace KursachWPF.ViewModels.NavPanelVM.ListVM;

public class AccountVM : ViewModelBase
{
    private KursachContext _db;
    public ObservableCollection<Account> Accounts { get; set; }

    private async void LoadData()
    {
        Accounts = new ObservableCollection<Account>(await _db.Accounts.ToListAsync());
    }
    
    public AccountVM()
    {
        _db = new KursachContext();
        LoadData();
    }
}