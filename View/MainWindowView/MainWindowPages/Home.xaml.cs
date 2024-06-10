using System.Windows.Controls;
using KursachWPF.Models;
using KursachWPF.Models.Context;

namespace KursachWPF.View.MainWindowView.MainWindowPages;

public partial class Home : UserControl
{
    private KursachContext _db;
    public Home()
    {
        _db = new KursachContext();
        InitializeComponent();
    }
}