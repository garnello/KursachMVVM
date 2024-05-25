using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using KursachWPF.Models;
using KursachWPF.Models.Context;
using KursachWPF.ViewModels.Authorization;
using KursachWPF.ViewModels.General;
using Newtonsoft.Json;
using MessageBox = System.Windows.Forms.MessageBox;

namespace KursachWPF.ViewModels.MainWindowVM.AccountVM.Methods;

public class ChangeEmailMethod : ViewModelBase
{
    private readonly KursachContext _db;
    private static string _loginUser = LoginVM._login;

    private string _emailText;
    private string _generatedCode;
    private string _confirmationCode;
    private Visibility _stackPanelVisibility;
    private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    // Property
    
    public Visibility StackPanelVisibility
    {
        get => _stackPanelVisibility;
        set
        {
            _stackPanelVisibility = value;
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
    
    
    // Tag command
    
    public ICommand SendCodeCommand { get; set; }
    public ICommand ChangeEmailCommand { get; set; }
    
    // Command
    

    private void ChangeEmail(object parameter)
    {
        var userEmail = _db.Accounts.FirstOrDefault(u => u.Login == _loginUser);
        
        if (ConfirmationCode != _generatedCode) MessageBox.Show("Ошибка! Код подтверждения не совпадает");
        else
        {
            userEmail.Email = EmailText;
            _db.SaveChanges();
            EmailText = "";
            StackPanelVisibility = Visibility.Collapsed;
            MessageBox.Show("Вы успешно изменили почту!");
        }
    }
    
    private void SendConfirmationCode(object parameter)
    {
        try
        {
            var userEmail = _db.Accounts.FirstOrDefault(u => u.Login == _loginUser);
            _generatedCode = GenerateRandomCode();
            // Create message
            
            MailMessage message = new MailMessage();
            message.From = new MailAddress("yarbashalilov@bk.ru", "Kursach code");
            message.To.Add(userEmail.Email);
            message.Subject = "Попытка смены вашей почты";
            message.Body = "Мы заметили, что на вашем аккаунте обнаружена попытка смена почты на устройстве. " +
                           "Если это вы, введите код подтвержения. А если нет, то мы советуем сменить ваш пароль." +
                           "Ваш код подтверждения: " + _generatedCode;
            
            // Property
            
            SmtpClient client = new SmtpClient("smtp.bk.ru", 587);
            client.Credentials = new NetworkCredential("yarbashalilov@bk.ru", "MyPassword");
            client.EnableSsl = true;
            
            // Logic
            
            if (EmailText == null) MessageBox.Show("Ошибка! Вы не ввели почту");
            else if (!IsValidEmail()) MessageBox.Show("Ошибка! Вы ввели неправльно почту");
            else
            {
                client.SendMailAsync(message);
                StackPanelVisibility = Visibility.Visible;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
    
    public ChangeEmailMethod()
    {
        _db = new KursachContext();
        SendCodeCommand = new RelayCommand(SendConfirmationCode);
        ChangeEmailCommand = new RelayCommand(ChangeEmail);
        StackPanelVisibility = Visibility.Collapsed;
    }
    
}
