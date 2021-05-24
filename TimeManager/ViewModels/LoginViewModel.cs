using GalaSoft.MvvmLight.CommandWpf;
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
    public class LoginViewModel
    {
        private IUserStorage _userStorage;
        private MainViewModel _main;
        public string Login { get; set; }
        public string Password { get; set; }
        public LoginViewModel(MainViewModel main, IUserStorage userStorage)
        {
            _userStorage = userStorage;
            _main = main;
            LoginCommand = new RelayCommand(LoginUser);
        }

        public RelayCommand LoginCommand { get; set; }

        private void LoginUser()
        {
            var user = _userStorage.GetAllUsers().FirstOrDefault(x => x.Login == Login && x.Password == Password);
            if(user != null)
            {
                _main.User = new UserModel(user);
                _main.GoToDailyTasks();
            }
            else
            {
                MessageBox.Show("invalid login or password");
            }
        }
    }
}
