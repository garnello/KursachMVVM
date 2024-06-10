using System.Collections.ObjectModel;
using KursachWPF.Models;
using KursachWPF.ViewModels.General;
using Microsoft.EntityFrameworkCore;

namespace KursachWPF.ViewModels.NavPanelVM.ListVM;

public class UniformVM : ViewModelBase
{
    private KursachContext _db;
    public ObservableCollection<Workwear> Workwears { get; set; }

    private async void LoadData()
    {
        Workwears = new ObservableCollection<Workwear>(await _db.Workwears.ToListAsync());
    }

    public UniformVM()
    {
        _db = new KursachContext();
        LoadData();
    }
}