using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PetShop
{
    public class LoginVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MainWindowVM MainView;
        public List<User> Users;

        public User queryUser;

        public LoginVM()
        {
        }

        public LoginVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = mainView.Users;
        }


        private string _userName;

        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));

            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));

            }
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
            PasswordBox passwordWorkAround = obj as PasswordBox;

            if (obj != null)
            {
                Password = passwordWorkAround.Password;
            }

            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(passwordWorkAround.Password))
            {
                queryUser = Users.FirstOrDefault(user => user.UserName == Username);
                
                if( queryUser != null && Password == queryUser.Password)
                {
                    if(queryUser.isSeller)
                    {
                        MainView.CurrentUser = queryUser;
                        MainView.ActiveView = new SellerViewVM(MainView);
                    }
                    else
                    {
                        MainView.CurrentUser = queryUser;
                        MainView.ActiveView = new ShopperViewVM(MainView);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!");
            }

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
