using System.Windows;
 using System.Windows.Input;
 using KursachWPF.Models;
 using KursachWPF.Models.Context;
 using KursachWPF.View.LogIn;
 using KursachWPF.View.MainWindowView.AccountView;
 using KursachWPF.ViewModels.Authorization;
 using KursachWPF.ViewModels.General;
 using Microsoft.EntityFrameworkCore;
 
 namespace KursachWPF.ViewModels.MainWindowVM.AccountVM;

 public class ChangePasswordMethod : ViewModelBase
 {
     private readonly KursachContext _db;
     private static string _loginUser = LoginVM._login;
     
     private string _currentPassword;
     private string _newPassword;
     private string _confirmNewPassword;
     private Visibility _stackPanelVisibility;

     public Visibility StackPanelVisibility
     {
         get => _stackPanelVisibility;
         set
         {
             _stackPanelVisibility = value;
             OnPropertyChanged();
         }
     }
     
     
     public string CurrentPassword
     {
         get { return _currentPassword; }
         set
         {
             _currentPassword = value;
             OnPropertyChanged(CurrentPassword);
         }
     }
     
     public string NewPassword
     {
         get { return _newPassword; }
         set
         {
             _newPassword = value;
             OnPropertyChanged(NewPassword);
         }
     }
     
     public string ConfirmNewPassword
     {
         get { return _confirmNewPassword; }
         set
         {
             _confirmNewPassword = value;
             OnPropertyChanged(ConfirmNewPassword);
         }
     }
     
     public RelayCommand ChangePasswordCommand { get; }
     public RelayCommand CheckCurrentPasswordCommand { get; }

     public void CheckCurrnetPasswword(object parameter)
     {
         try
         {
             var user = _db.Accounts.FirstOrDefault(u => u.Login == _loginUser);

             if (user == null) MessageBox.Show("Ошибка");
             else if (user.Password != CurrentPassword) MessageBox.Show("Неверно введен пароль");
             else StackPanelVisibility = Visibility.Visible;
         }
         catch (Exception e)
         {
             MessageBox.Show("Ошибка " + e.Message);
             throw;
         }
     }

     public void ChangePassword(object parameter)
     {
         try
         {
             var user = _db.Accounts.FirstOrDefault(u => u.Login == _loginUser);
             
             if (NewPassword != ConfirmNewPassword) MessageBox.Show("Неверно повторен пароль");
             else
             {
                 user.Password = NewPassword;
                 _db.SaveChanges();
                 MessageBox.Show("Пароль успешно сменен!");
                 StackPanelVisibility = Visibility.Collapsed;
             }
         }
         catch (Exception e)
         {
             MessageBox.Show("Ошибка " + e.Message);
             throw;
         }
     }

     public ChangePasswordMethod()
     {
         _db = new KursachContext();
         CheckCurrentPasswordCommand = new RelayCommand(CheckCurrnetPasswword);
         ChangePasswordCommand = new RelayCommand(ChangePassword);
         StackPanelVisibility = Visibility.Collapsed;
     }
 }