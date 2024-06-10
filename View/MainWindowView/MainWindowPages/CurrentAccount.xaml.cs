using System.Windows;
using System.Windows.Controls;

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