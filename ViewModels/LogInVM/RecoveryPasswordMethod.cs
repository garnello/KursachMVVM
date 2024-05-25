using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.View.LogIn;
using KursachWPF.ViewModels.General;

namespace KursachWPF.ViewModels.Authorization;

public class RecoveryPasswordMethod : ViewModelBase
{
    private readonly KursachContext _db;
    private string _emailText;
    private string _generatedCode;
    private string _confirmationCode;
    private string _newPassword;
    private string _confirmNewPassword;
    private Visibility _codeStackPanelVisibility;
    private Visibility _passwordStackPanelVisibility;
    private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    // Property
    
    public Visibility CodeStackPanelVisibility
    {
        get => _codeStackPanelVisibility;
        set
        {
            _codeStackPanelVisibility = value;
            OnPropertyChanged();
        }
    }
    
    public Visibility PasswordStackPanelVisibility
    {
        get => _passwordStackPanelVisibility;
        set
        {
            _passwordStackPanelVisibility = value;
            OnPropertyChanged();
        }
    }
    
    public string ConfirmationCode
    {
        get { return _confirmationCode; }
        set
        {
            _confirmationCode = value;
            OnPropertyChanged(ConfirmationCode);
        }
    }
    
    public string EmailText
    {
        get { return _emailText; }
        set
        {
            _emailText = value;
            OnPropertyChanged(EmailText);
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
    
    // Functions
    
    private static string GenerateRandomCode()
    {
        Random randonCode = new Random();
        return randonCode.Next(1000, 9999).ToString();
    }
    
    private bool IsValidEmail()
    {
        return EmailRegex.IsMatch(EmailText);
    }
    
    public bool CheckEmailExists()
    {
        return _db.Accounts.Any(a => a.Email == EmailText);
    }
    
    // Tag command
    
    public ICommand SendCodeCommand { get; set; }
    public ICommand ChangePasswordCommand { get; set; }
    public ICommand CheckConfirmationCodeCommand { get; set; }
    
    // Command
    
    private void SendConfirmationCode(object parameter)
    {
        try
        {
            _generatedCode = GenerateRandomCode();
            // Create message
            
            MailMessage message = new MailMessage();
            message.From = new MailAddress("yarbashalilov@bk.ru", "Kursach code");
            message.To.Add(EmailText);
            message.Subject = "Восстановление пароля";
            message.Body = "Мы заметили, что на вашем аккаунте обнаружена попытка восстановления пароля. " +
                           "Если это вы, введите код подтвержения. А если нет, то мы советуем обратиться к технической поддержки." +
                           "Ваш код подтверждения: " + _generatedCode;
            
            // Property
            
            SmtpClient client = new SmtpClient("smtp.bk.ru", 587);
            client.Credentials = new NetworkCredential("yarbashalilov@bk.ru", "whXEkQ1U9DpynasMq0jd");
            client.EnableSsl = true;
            
            // Logic
            
            if (EmailText == null) MessageBox.Show("Ошибка! Вы не ввели почту");
            else if (!IsValidEmail()) MessageBox.Show("Ошибка! Вы ввели неправльно почту");
            else if (!CheckEmailExists()) MessageBox.Show("Ошибка! Данная почта не зарегистрирована");
            else
            {
                client.SendMailAsync(message);
                CodeStackPanelVisibility = Visibility.Visible;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
    
    private void CheckConfirmationCode(object parameter)
    {
        if (ConfirmationCode != _generatedCode) MessageBox.Show("Ошибка! Код подтверждения не совпадает");
        else
        {
            CodeStackPanelVisibility = Visibility.Collapsed;
            PasswordStackPanelVisibility = Visibility.Visible;
        }
    }

    private void ChangePassword(object parameter)
    {
        Account user = _db.Accounts.FirstOrDefault(u => u.Email == EmailText);
        
        if (NewPassword == null) MessageBox.Show("Ошибка! Введите пароль");
        else if (NewPassword != ConfirmNewPassword) MessageBox.Show("Неверно повторен пароль");
        else
        {
            user.Password = NewPassword;
            _db.SaveChanges();
            MessageBox.Show("Пароль успешно изменен");
            Application.Current.Dispatcher.Invoke(() => RecoveryPasswordView.RPWindow.Close());
        }
    }
    
    public RecoveryPasswordMethod()
    {
        _db = new KursachContext();
        SendCodeCommand = new RelayCommand(SendConfirmationCode);
        ChangePasswordCommand = new RelayCommand(ChangePassword);
        CheckConfirmationCodeCommand = new RelayCommand(CheckConfirmationCode);
        
        CodeStackPanelVisibility = Visibility.Collapsed;
        PasswordStackPanelVisibility = Visibility.Collapsed;
    }
}