using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.ViewModels.AuthVM;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.NavPanelVM.AccountVM;

public class AvatarVM : ViewModelBase
{
    private KursachContext _db;
    private static string _loginUser = LoginVM._login;
    public Account _userAccount;
    
    public Account UserAccount
    {
        get => _userAccount;
        set { _userAccount = value; OnPropertyChanged(nameof(UserAccount)); }
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
            
            await _db.SaveChangesAsync();
        }
    }

    public AvatarVM()
    {
        _db = new KursachContext();
        UserAccount = _db.Accounts.FirstOrDefault(a => a.Login == _loginUser);
        ChangeAvatarCommand = new RelayCommand(ChangeAvatar);
    }
}