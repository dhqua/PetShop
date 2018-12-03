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

        // Required for bindings to work
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        // Container for the window, list of valid users, and user used to check login info
        public MainWindowVM MainView;
        public List<User> Users;
        public User queryUser;


        public LoginVM()
        {
        }

        // Constructor needs container which also contains valid user list
        public LoginVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = mainView.Users;
        }

        // User entered user name
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

        // User entered password
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


        // EVENT HANDLERS


        //  Binded to login Button
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

        // Verifies user info and logs into the app
        private void LoginClicked(object obj)
        {
            // Password box can not be access directly so this work around send the entire box
            // as a command parameter of the login button click
            PasswordBox passwordWorkAround = obj as PasswordBox;

            if (obj != null)
            {
                Password = passwordWorkAround.Password;
            }

            // If both fields have been filled
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(passwordWorkAround.Password))
            {
                // Gets user with matching user name from the user list
                queryUser = Users.FirstOrDefault(user => user.UserName == Username);
                
                // If the user exist and the password matches what the user entered
                if( queryUser != null && Password == queryUser.Password)
                {
                    // Checks account type and starts the app
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

        // Swtiches to create account window
        private void createClicked(object obj)
        {
            MainView.ActiveView = new CreateUserVM(MainView);
        }

    }
}
