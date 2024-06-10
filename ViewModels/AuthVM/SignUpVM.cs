using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KursachWPF.Models;
using KursachWPF.View.Auth;
using KursachWPF.View.Auth.NavigationPanel;
using KursachWPF.View.MainWindowView.NavigationPanel;
using KursachWPF.ViewModels.General;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;


namespace KursachWPF.ViewModels.AuthVM;

public class SignUpVM : ViewModelBase
{
    private readonly KursachContext _db;
    
    private string _accountName;
    private string _accountSurname;
    private string _accountLogin;
    private string _accountPassword;
    private string _accountEmail;
    private string _tagCorrectFIO;
    private string _tagLogAPas;
    private byte[] _accountImage;
    private string _generatedCode;
    private string _confirmationCode;
    
    private Visibility _visibilityStackPanelFIO;
    private Visibility _visibilityStackPanelLogAPas;
    private Visibility _visibilityStackPanelEmail;
    private Visibility _visibilityStackPanelEmailCode;
    private Visibility _visibilityStackPanelAvatar;
    private Visibility _visibilityButtonEmail;
    
    private static readonly Regex EmailRegex = 
        new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    
    // Property
    
    public string AccountName
    {
        get => _accountName;
        set { _accountName = value; OnPropertyChanged(AccountName); }
    }
    
    public string AccountSurname
    {
        get => _accountSurname;
        set { _accountSurname = value; OnPropertyChanged(AccountSurname); }
    }
    
    public string AccountLogin
    {
        get => _accountLogin;
        set { _accountLogin = value; OnPropertyChanged(AccountLogin); }
    }
    
    public string AccountPassword
    { 
        get => _accountPassword;
        set { _accountPassword = value; OnPropertyChanged(AccountPassword); }
    }
    
    public string AccountEmail
    {
        get => _accountEmail;
        set { _accountEmail = value; OnPropertyChanged(AccountEmail); }
    }
    
    public string TagCorrectFIO
    {
        get => _tagCorrectFIO;
        set { _tagCorrectFIO = value; OnPropertyChanged(TagCorrectFIO); }
    }

    public string TagLogAPas
    {
        get => _tagLogAPas;
        set { _tagLogAPas = value; OnPropertyChanged(TagLogAPas); }
    }
    
    public byte[] AccountImage
    {
        get => _accountImage;
        set { _accountImage = value; OnPropertyChanged(nameof(AccountImage)); }
    }
    
    public string ConfirmationCode
    {
        get { return _confirmationCode; }
        set { _confirmationCode = value; OnPropertyChanged(ConfirmationCode); }
    }
    
    public Visibility VisibilityStackPanelLogAPas
    {
        get => _visibilityStackPanelLogAPas;
        set { _visibilityStackPanelLogAPas = value; OnPropertyChanged(); }
    }
    
    public Visibility VisibilityStackPanelEmail
    {
        get => _visibilityStackPanelEmail;
        set { _visibilityStackPanelEmail = value; OnPropertyChanged(); }
    }
    
    public Visibility VisibilityStackPanelFIO
    {
        get => _visibilityStackPanelFIO;
        set { _visibilityStackPanelFIO = value; OnPropertyChanged(); }
    }
    
    public Visibility VisibilityStackPanelEmailCode
    {
        get => _visibilityStackPanelEmailCode;
        set { _visibilityStackPanelEmailCode = value; OnPropertyChanged(); }
    }
    
    public Visibility VisibilityStackPanelAvatar
    {
        get => _visibilityStackPanelAvatar;
        set { _visibilityStackPanelAvatar = value; OnPropertyChanged(); }
    }
    
    public Visibility VisibilityButtonEmail
    {
        get => _visibilityButtonEmail;
        set { _visibilityButtonEmail = value; OnPropertyChanged(); }
    }
    
    // Functions
    
    private static string GenerateRandomCode()
    {
        Random randonCode = new Random();
        return randonCode.Next(1000, 9999).ToString();
    }
    
    private bool IsValidEmail()
    {
        return EmailRegex.IsMatch(_accountEmail);
    }
    
    // Tags commands
    
    public ICommand AddingAccountCommand { get; set; }
    public ICommand CheckCorrectFIOCommand { get; set; }
    public ICommand CheckLogAPasCommand { get; set; }
    public ICommand CheckEmailCommand { get; set; }
    public ICommand CheckEmailCodeCommand { get; set; }
    public ICommand SelectionAvatarCommand { get; set; }


    // Command
    
    private void CheckCorrectFIO(object parameter)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(_accountSurname) && string.IsNullOrWhiteSpace(_accountName))
                _tagCorrectFIO = "Поля Фамилия и Имя не введены.";
            else if(string.IsNullOrWhiteSpace(_accountSurname))
                _tagCorrectFIO = "Поля Фамилия не введено.";
            else if (string.IsNullOrWhiteSpace(_accountName))
                _tagCorrectFIO = "Поля Имя не введено.";
            else
            {
                _visibilityStackPanelFIO = Visibility.Collapsed;
                _visibilityStackPanelLogAPas = Visibility.Visible;
                _tagCorrectFIO = string.Empty;
            }
    
            OnPropertyChanged(nameof(TagCorrectFIO));
            OnPropertyChanged(nameof(VisibilityStackPanelFIO));
            OnPropertyChanged(nameof(VisibilityStackPanelLogAPas));
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка: " + e.Message);
        }
        
    }

    private void CheckLogAPas(object parameter)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(_accountLogin) && string.IsNullOrWhiteSpace(_accountPassword))
                _tagLogAPas = "Поля Логина и Пароля не введены.";
            else if(string.IsNullOrWhiteSpace(_accountLogin))
                _tagLogAPas = "Поле Логин не введено.";
            else if (string.IsNullOrWhiteSpace(_accountPassword))
                _tagLogAPas = "Поле Пароль не введено.";
            else if (_accountLogin.Length < 3 || _accountLogin.Length > 20)
                _tagLogAPas = "Логин должен быть от 3 до 20 символов.";
            else if (_accountPassword.Length < 6 || _accountPassword.Length > 20)
                _tagLogAPas = "Пароль должен быть от 6 до 20 символов.";
            else
            {
                _visibilityStackPanelLogAPas = Visibility.Collapsed;
                _visibilityStackPanelEmail = Visibility.Visible;
                _tagLogAPas = string.Empty;
            }
    
            OnPropertyChanged(nameof(TagLogAPas));
            OnPropertyChanged(nameof(VisibilityStackPanelEmail));
            OnPropertyChanged(nameof(VisibilityStackPanelLogAPas));
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка: " + e.Message);
            throw;
        }
    }
    
    private void CheckEmailCode(object parameter)
    {
        if (!string.Equals(ConfirmationCode, _generatedCode, StringComparison.OrdinalIgnoreCase))
            MessageBox.Show("Ошибка! Код подтверждения не совпадает");
        else
        {
            _visibilityStackPanelEmail = Visibility.Collapsed;
            _visibilityStackPanelEmailCode = Visibility.Collapsed;
            _visibilityStackPanelAvatar = Visibility.Visible;
        }
        OnPropertyChanged(nameof(VisibilityStackPanelEmail));
        OnPropertyChanged(nameof(VisibilityStackPanelEmailCode));
        OnPropertyChanged(nameof(VisibilityStackPanelAvatar));
    }

    private void CheckEmail(object parameter)
    {
        try
        {
            _generatedCode = GenerateRandomCode();
            
            // Create message
            
            MailMessage message = new MailMessage();
            message.From = new MailAddress("yarbashalilov@bk.ru", "Kursach code");
            message.To.Add(_accountEmail);
            message.Subject = "Попытка смены вашей почты";
            message.Body = "Мы заметили, что на вашем аккаунте обнаружена попытка смена почты на устройстве. " +
                           "Если это вы, введите код подтвержения. А если нет, то мы советуем сменить ваш пароль." +
                           "Ваш код подтверждения: " + _generatedCode;
            
            // Property
            
            SmtpClient client = new SmtpClient("smtp.bk.ru", 587);
            client.Credentials = new NetworkCredential("yarbashalilov@bk.ru", "kiRK9fXuc3wMGH4UMxqh");
            client.EnableSsl = true;
            
            // Logic
            
            if (_accountEmail == null) MessageBox.Show("Ошибка! Вы не ввели почту");
            else if (!IsValidEmail()) MessageBox.Show("Ошибка! Вы ввели неправльно почту");
            else
            {
                client.SendMailAsync(message);
                _visibilityButtonEmail = Visibility.Collapsed;
                _visibilityStackPanelEmailCode = Visibility.Visible;
            }
            
            OnPropertyChanged(nameof(VisibilityButtonEmail));
            OnPropertyChanged(nameof(VisibilityStackPanelEmailCode));
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        } 
    }

    private async void SelectionAvatar(object parameter)
    {
        try
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
                    _accountImage = new byte[fileStream.Length];
                    await fileStream.ReadAsync(_accountImage, 0, (int)fileStream.Length);
                    OnPropertyChanged(nameof(AccountImage));
                }
                OnPropertyChanged(nameof(AccountImage));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private void AddingAccount(object parameter)
    {
        var newUser = new Account
        {
            Avatar = _accountImage, 
            Email = _accountEmail,
            Login = _accountLogin,
            Password = _accountPassword,
            Name = _accountName,
            Surname = _accountSurname
        };

        _db.Add(newUser);
        _db.SaveChanges();

        NavigationPanel np = new NavigationPanel();
        Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        currentWindow.Close();
        np.ShowDialog();
    }

    public SignUpVM()
    {
        _db = new KursachContext();
        AddingAccountCommand = new RelayCommand(AddingAccount);
        CheckCorrectFIOCommand = new RelayCommand(CheckCorrectFIO);
        CheckLogAPasCommand = new RelayCommand(CheckLogAPas);
        CheckEmailCommand = new RelayCommand(CheckEmail);
        CheckEmailCodeCommand = new RelayCommand(CheckEmailCode);
        SelectionAvatarCommand = new RelayCommand(SelectionAvatar);

        _visibilityStackPanelEmail = Visibility.Collapsed;
        _visibilityStackPanelEmailCode = Visibility.Collapsed;
        _visibilityStackPanelLogAPas = Visibility.Collapsed;
        _visibilityStackPanelAvatar = Visibility.Collapsed;
        _visibilityStackPanelFIO = Visibility.Visible;
        _visibilityButtonEmail = Visibility.Visible;
    }
}