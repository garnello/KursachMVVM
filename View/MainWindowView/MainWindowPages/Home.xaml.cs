using System.Windows.Controls;
using KursachWPF.Models;
using KursachWPF.ViewModels.Authorization;

namespace KursachWPF.View.MainWindowView.MainWindowPages;

public partial class Home : UserControl
{
    private KursachContext db;
    public Home()
    {
        db = new KursachContext();
        InitializeComponent();
        
        /*string loggedLogin = LoginVM._login;
        var account = db.Accounts.FirstOrDefault(u => u.Login == loggedLogin);
        string name = $"Добро пожаловать, {account.Name}";
        TextWelcome.Text = name;*/
    }
}