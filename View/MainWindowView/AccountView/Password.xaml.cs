using System.Windows;
using System.Windows.Controls;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.ViewModels.Authorization;
using KursachWPF.ViewModels.General;
using KursachWPF.ViewModels.MainWindowVM.AccountVM;

namespace KursachWPF.View.MainWindowView.AccountView;

public partial class Password : UserControl
{
    private KursachContext db;
    
    public Password()
    {
        InitializeComponent();
        db = new KursachContext();
    }
}