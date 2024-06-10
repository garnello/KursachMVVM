using System.Collections.ObjectModel;
using KursachWPF.Models;
using KursachWPF.ViewModels.General;
using Microsoft.EntityFrameworkCore;

namespace KursachWPF.ViewModels.NavPanelVM.ListVM;

public class WriteOffVM : ViewModelBase
{
    private KursachContext _db;
    public ObservableCollection<WriteOffHistory> WriteOffs { get; set; }

    private async void LoadData()
    {
        WriteOffs = new ObservableCollection<WriteOffHistory>(await _db.WriteOffHistories.ToListAsync());
    }

    public WriteOffVM()
    {
        _db = new KursachContext();
        LoadData();
    }
}