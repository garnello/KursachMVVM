using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using KursachWPF.Components;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.View.MainWindowView.AccountView;
using KursachWPF.ViewModels.Authorization;
using KursachWPF.ViewModels.General;
using System.Linq;

namespace KursachWPF.ViewModels.MainWindowVM.AccountVM.Methods;

public class ChangeAvatarMethod : ViewModelBase
{
    private KursachContext _db;
    private static string _loginUser = LoginVM._login;
    public Account _userAccount;

    public Account UserAccount
    {
        get => _userAccount;
        set
        {
            _userAccount = value;
            OnPropertyChanged(nameof(UserAccount));
        }
    }
    
    public ICommand ChangeAvatarCommand { get; }

    private async void ChangeAvatar(object parameter)
    {
        var openFileDialog = new OpenFileDialog()
        {
            Filter = "Image files (*.png; *.jpeg; *.jpg) | *.png; *.jpeg; *.jpg",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            using (var fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            {
                UserAccount.Avatar = new byte[fileStream.Length];
                await fileStream.ReadAsync(UserAccount.Avatar, 0, (int)fileStream.Length);
                OnPropertyChanged(nameof(UserAccount));
            }
            OnPropertyChanged(nameof(UserAccount));
            await _db.SaveChangesAsync();
            MessageBox.Show("Чтобы изменения полностью пременились, перезагрузить приложение");
        }
    }

    public ChangeAvatarMethod()
    {
        _db = new KursachContext();
        UserAccount = _db.Accounts.FirstOrDefault(a => a.Login == _loginUser);
        ChangeAvatarCommand = new RelayCommand(ChangeAvatar);
    }
}