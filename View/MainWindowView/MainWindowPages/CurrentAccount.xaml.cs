using System.Windows;
using System.Windows.Controls;
using KursachWPF.ViewModels.MainWindowVM.AccountVM;
using KursachWPF.ViewModels.MainWindowVM.MainWindowPages;

namespace KursachWPF.View.MainWindowView.MainWindowPages;

public partial class CurrentAccount : UserControl
{
    public static CurrentAccount curAcc;
    public CurrentAccount()
    {
        InitializeComponent();
        curAcc = this;
    }
}