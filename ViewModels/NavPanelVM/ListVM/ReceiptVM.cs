using System.Collections.ObjectModel;
using KursachWPF.Models;
using KursachWPF.View.MainWindowView.MainWindowPages.ListPages;
using KursachWPF.ViewModels.General;
using Microsoft.EntityFrameworkCore;

namespace KursachWPF.ViewModels.NavPanelVM.ListVM;

public class ReceiptVM : ViewModelBase
{
    
    private KursachContext _db;
    public ObservableCollection<WorkwearReceiptHistory> Receipts { get; set; }

    private async void LoadData()
    {
        Receipts = new ObservableCollection<WorkwearReceiptHistory>(await _db.WorkwearReceiptHistories.ToListAsync());
    }

    public ReceiptVM()
    {
        _db = new KursachContext();
        LoadData();
    }
}