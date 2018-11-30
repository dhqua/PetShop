using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PetShop
{
    public class LoginVM
    {


        public MainWindowVM MainView;
        public List<User> Users;

        public User queryUser;

        string Username;

        string Password;

        public LoginVM()
        {
        }

        public LoginVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = mainView.Users;
        }
        

        

        /*
        private bool ValidatAllEntries()
        {
            if (string.IsNullOrWhiteSpace(userName.Text))
            { return false; }
            if (string.IsNullOrWhiteSpace(pwBox.Password))
            { return false; }

            return true;
        }
        */



        // EVENT HANDLERS


        //  Login Button
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(LoginClicked);
                }

                return _loginCommand;
            }
        }
        DelegateCommand _loginCommand;

        private void LoginClicked(object obj)
        {
            MessageBox.Show("Login clicked");
        }

        // Create User Button

        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                {
                    _createCommand = new DelegateCommand(createClicked);
                }

                return _createCommand;
            }
        }
        DelegateCommand _createCommand;

        private void createClicked(object obj)
        {
            MainView.ActiveView = new CreateUserVM(MainView);
        }

    }
}
