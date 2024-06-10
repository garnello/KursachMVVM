using System.Windows;
using System.Windows.Controls;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.ViewModels.General;

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