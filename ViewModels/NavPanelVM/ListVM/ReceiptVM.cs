/*using System.Collections.ObjectModel;
using KursachWPF.Models.Context;
using KursachWPF.View.MainWindowView.MainWindowPages.ListPages;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.NavPanelVM.ListVM;

public class ReceiptVM : ViewModelBase
{
    
    private KursachContext _db;
    public ObservableCollection<Receipt> Receipts { get; set; }

    private async void LoadData()
    {
        Receipts = new ObservableCollection<Receipt>(await _db.Receipts.ToListAsync());
    }

    public EmployeeVM()
    {
        _db = new KursachContext();
        LoadData();
    }
}*/