using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KursachWPF.View.MainWindowView;
using KursachWPF.View.MainWindowView.MainWindowPages.ListPages;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.NavPanelVM.Pages;

public class ListVM : ViewModelBase
{
    private object _listView;
    
    public object ListView
    {
        get { return _listView; }
        set { _listView = value; OnPropertyChanged(nameof(ListView)); }
    }
    
    public ICommand AccountViewCommand { get; set; }
    public ICommand EmployeeViewCommand { get; set; }
    public ICommand ReceiptViewCommand { get; set; }
    public ICommand UniformViewCommand { get; set; }
    public ICommand WriteOffViewCommand { get; set; }
    
    
    private void Employee(object obj) => ListView = new Employee();
    
    private void Account(object obj) => ListView = new Account();
    
    private void Receipt(object obj) => ListView = new Receipt();
    
    private void Uniform(object obj) => ListView = new Uniform();
    
    private void WriteOff(object obj) => ListView = new WriteOff();
    

    public ListVM()
    {
        AccountViewCommand = new RelayCommand(Account);
        EmployeeViewCommand = new RelayCommand(Employee);
        ReceiptViewCommand = new RelayCommand(Receipt);
        UniformViewCommand = new RelayCommand(Uniform);
        WriteOffViewCommand = new RelayCommand(WriteOff);

        ListView = new Employee();
        OnPropertyChanged(nameof(ListView));
        
    }
}