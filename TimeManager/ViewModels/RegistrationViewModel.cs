using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class RegistrationViewModel
    {
        private IUserStorage _userStorage;
        private MainViewModel _main;
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public RegistrationViewModel(MainViewModel main, IUserStorage userStorage)
        {
            _userStorage = userStorage;
            _main = main;
            RegisterCommand = new RelayCommand(RegisterUser);
        }

        public RelayCommand RegisterCommand { get; set; }

        private void RegisterUser()
        {
            var user = _userStorage.GetAllUsers().FirstOrDefault(x => x.Login == Login);
            if (user == null)
            {
                var newUser = new User()
                {
                    Login = Login,
                    Password = Password,
                };
                _userStorage.Create(newUser);
                var thisUser = _userStorage.GetAllUsers().FirstOrDefault(x => x.Login == Login);

                _main.User = new UserModel(thisUser);
                _main.GoToDailyTasks();
            }
            else
            {
                MessageBox.Show("this login is busy");
            }
        }
    }
}
