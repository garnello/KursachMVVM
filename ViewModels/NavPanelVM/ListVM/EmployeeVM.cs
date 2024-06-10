using System.Collections.ObjectModel;
using KursachWPF.Models;
using KursachWPF.ViewModels.General;
using Microsoft.EntityFrameworkCore;

namespace KursachWPF.ViewModels.NavPanelVM.ListVM;

public class EmployeeVM : ViewModelBase
{
    private KursachContext _db;
    public ObservableCollection<Employee> Employees { get; set; }

    private async void LoadData()
    {
        Employees = new ObservableCollection<Employee>(await _db.Employees.ToListAsync());
    }

    public EmployeeVM()
    {
        _db = new KursachContext();
        LoadData();
    }
}