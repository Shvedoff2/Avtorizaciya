using Avtorizaciya.Model;
using Avtorizaciya.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Avtorizaciya.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ILoginService _loginService;
        public ICommand proverkaAdminCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand UpdateTableCommand { get; set; }

        public ICommand LoginCommand { get; set; }
        public IProverkaService _proverkaService;
        public IRegistService _registService;
        public ICommand RegistCommand { get; set; }
        public string ErrorMessage { get; set; }
        public ICommand OpenRegisterCommand { get; set; }
        public ICommand OpenSetCommand { get; set; }
        public ICommand ChangeToDarkThemeCommand { get; set; }
        public ICommand ChangeToLightThemeCommand { get; set; }
        public ICommand BackCommand { get; set; }
        private bool _isRegistrationSuccessful;
        public IRegistService _AddUserService;
        public ICommand AddUserCommand { get; set; }
        private readonly TableService tableService = new TableService();
        public ObservableCollection<Login1> Logins { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }

        private ObservableCollection<Login1> _users;
        public ObservableCollection<Login1> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        private Login1 _selectedUser;
        public Login1 SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public ICommand OpenAddUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        private UserBusinessLogic _userBusinessLogic;
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        private string _userNameR;
        public string UserNameR
        {
            get { return _userNameR; }
            set
            {
                _userNameR = value;
                OnPropertyChanged("UserNameR");
            }
        }

        private string _passwordR;
        public string PasswordR
        {
            get { return _passwordR; }
            set
            {
                _passwordR = value;
                OnPropertyChanged("PasswordR");
            }
        }
        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string _loginRed;
        public string LoginRed
        {
            get { return _loginRed; }
            set
            {
                _loginRed = value;
                OnPropertyChanged("Login");
            }
        }
        public string _passwordRed;
        public string PasswordRed
        {
            get { return _passwordRed; }
            set
            {
                _passwordRed = value;
                OnPropertyChanged("Password");
            }
        }
        public string _nameRed;
        public string NameRed
        {
            get { return _nameRed; }
            set
            {
                _nameRed = value;
                OnPropertyChanged("Name");
            }
        }
        private bool _isDarkTheme;

        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                _isDarkTheme = value;
                OnPropertyChanged("IsDarkTheme");
            }
        }
        private bool isImageChanged = false;

        public ICommand ChangeImageCommand { get; }

        public bool IsImageChanged
        {
            get => isImageChanged;
            set
            {
                isImageChanged = value;
                OnPropertyChanged(nameof(IsImageChanged));
            }
        }
        public MainWindowViewModel(ILoginService loginService, IRegistService registService, IProverkaService proverkaService)
        {
            _loginService = loginService;
            LoginCommand = new RelayCommand(Login, CanLogin);
            _registService = registService;
            RegistCommand = new RelayCommand(Register, CanLogin);
            _proverkaService = proverkaService;
            OpenRegisterCommand = new RelayCommand(OpenRegister);
            ChangeToDarkThemeCommand = new RelayCommand(ChangeToDarkTheme);
            ChangeToLightThemeCommand = new RelayCommand(ChangeToLightTheme);
            OpenSetCommand = new RelayCommand(OpenSet);
            BackCommand = new RelayCommand(Back);
            AddUserCommand = new RelayCommand(AddUser, CanLogin);
            _proverkaService = proverkaService;
            _registService = registService; _userBusinessLogic = new UserBusinessLogic();
            Users = _userBusinessLogic.LoadUsers();
            CloseCommand = new RelayCommand(CloseW);
            OpenAddUserCommand = new RelayCommand(OpenAdd);
            EditUserCommand = new RelayCommand(OpenEdit);
            UpdateCommand = new RelayCommand(UpdateUser);
            UpdateTableCommand = new RelayCommand(UpdateTable);
            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeCommand = new RelayCommand(Maximize);
            DeleteUserCommand = new RelayCommand(param =>
            {
                if (SelectedUser == null)
                {
                    MessageBox.Show("Выберите пользователя!");
                    return;
                }
                if (SelectedUser.Role == "admin")
                {
                    MessageBox.Show("Вы не можете удалять данные админа");
                    return;
                }
                _userBusinessLogic.DeleteUser(SelectedUser);
                Users = _userBusinessLogic.LoadUsers();
            });
        }
        public void OpenAdd(object obj)
        {
            AddUser Adduser = new AddUser();
            Adduser.DataContext = this;
            Adduser.Show();
            Application.Current.Windows[0].Close();
        }
        public void UpdateTable(object obj)
        {
            Users = _userBusinessLogic.LoadUsers();
        }
        public void OpenEdit(object obj)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }
            if (SelectedUser.Role == "admin")
            {
                MessageBox.Show("Вы не можете изменять данные админа");
                return;
            }
            EditUser Edituser = new EditUser();
            Edituser.DataContext = this;
            Edituser.Show();
            Application.Current.Windows[0].Close();
        }
        private void CloseW(object obj)
        {
            Application.Current.Windows[0].Close();
        }
        private void Minimize(object obj)
        {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }
        private void Maximize(object obj)
        {
            IsImageChanged = !IsImageChanged;
            if (Application.Current.Windows[0].Height == 450)
            {
                Application.Current.Windows[0].WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.Windows[0].WindowState = WindowState.Normal;
            }
        }
        public void OpenSet(object obj)
        {
            Theme settings = new Theme();
            settings.DataContext = this;
            settings.Show();
            Application.Current.Windows[0].Visibility = Visibility.Hidden;
        }
        public void Back(object obj)
        {
            Application.Current.Windows[1].Close(); 
            Application.Current.Windows[0].Visibility = Visibility.Visible;
        }
        private void ChangeToDarkTheme(object obj)
        {
            IsDarkTheme = true;
        }
        private void ChangeToLightTheme(object obj)
        {
            IsDarkTheme = false;
        }
        private void OpenRegister(object obj)
        {
            RegistUser regist = new RegistUser();
            regist.DataContext = this;
            regist.Show();
        }
        private bool CanLogin(object obj)
        {
            return true;
        }

        private void Login(object obj)
        {
            if (_loginService.ValidateUser(UserName, Password))
            {
                MessageBox.Show("Пароль верный");
                ErrorMessage = "";
                OnPropertyChanged("ErrorMessage");
                DataBase Database = new DataBase();
                Database.DataContext = this;
                Database.Show();
                Application.Current.Windows[0].Close();
            }
            else
            {
                ErrorMessage = "Wrong login or password";
                OnPropertyChanged("ErrorMessage");
            }
        }
        private void AddUser(object obj)
        {
            if (UserNameR == null || PasswordR == null || Name == null)
            {
                MessageBox.Show("Введите данные");
                return;
            }
            if (_proverkaService.ValidateUser(UserNameR))
            {
                MessageBox.Show("Такой пользователь уже существует");
                return;
            }
            _registService.ValidateUser(UserNameR, PasswordR, Name);
            MessageBox.Show("Добавление успешно!");
            DataBase Database = new DataBase();
            Database.DataContext = this;
            Database.Show();
            Application.Current.Windows[0].Close();
            Users = _userBusinessLogic.LoadUsers();
        }
        private void UpdateUser(object obj)
        {
            if (LoginRed == null || PasswordRed == null || NameRed == null)
            {
                MessageBox.Show("Введите данные");
                return;
            }
            _userBusinessLogic.UpdateUser(SelectedUser, LoginRed, PasswordRed, NameRed);
            Users = _userBusinessLogic.LoadUsers();
            DataBase Database = new DataBase();
            Database.DataContext = this;
            Database.Show();
            Application.Current.Windows[0].Close();
        }
        private void Register(object obj)
        {
            if (UserNameR == null || PasswordR == null || Name == null)
            {
                MessageBox.Show("Введите данные");
                return;
            }
            if (_proverkaService.ValidateUser(UserNameR))
            {
                MessageBox.Show("Такой пользователь уже существует");
            }
            else
            {
                _registService.ValidateUser(UserNameR, PasswordR, Name);
                MessageBox.Show("Регистрация успешна!");

                LoginUser main = new LoginUser();
                main.DataContext = this;
                main.Show();
                Application.Current.Windows[0].Close();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}