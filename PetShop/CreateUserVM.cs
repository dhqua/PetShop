using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetShop
{
    public class CreateUserVM : INotifyPropertyChanged
    {

        // Needed for bindings to work
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        // Container, list of users, and temp user that will be added to the database
        MainWindowVM MainView;
        List<User> Users;
        User tempUser;

        // Constructor
        public CreateUserVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = mainView.Users;
        }

        // Username of the account to be added
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        // Password of the account to be added
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

        // First name of the account to be added
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        //Last name of the account to be added
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        // email of the account to be added
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        // Credit card number of the account to be added
        private string creditCardNum;
        public string CreditCardNum
        {
            get { return creditCardNum; }
            set
            {
                creditCardNum = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CreditCardNum"));
            }
        }

        // Exp month of account to be added
        private string expirationMonth;
        public string ExpirationMonth
        {
            get { return expirationMonth; }
            set
            {
                expirationMonth = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExpirationMonth"));
            }
        }

        // Exp year of account to be added
        private string expirationYear;
        public string ExpirationYear
        {
            get { return expirationYear; }
            set
            {
                expirationYear = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExpirationYear"));
            }
        }

        // CVC code of account to be added
        private string cvcCode;
        public string CvcCode
        {
            get { return cvcCode; }
            set
            {
                cvcCode = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CvcCode"));
            }
        }

        // Bool binded to radio button, determines the account type of the user
        private bool isSeller;
        public bool IsSeller
        {
            get { return isSeller; }
            set
            {
                isSeller = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSeller"));
            }
        }

        // EVENT HANDLERS

        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                {
                    _createCommand = new DelegateCommand(CreateClicked);
                }

                return _createCommand;
            }
        }
        DelegateCommand _createCommand;

        // Creates user account and writes changes to XML files
        private void CreateClicked(object obj)
        {
            // Cannot access password directly so this work around passes the entire password box as a parameter
            if (obj != null)
            {
                PasswordBox workArond = obj as PasswordBox;
                Password = workArond.Password;
            }

            // Ensures the fields are not blank, creates new user and writes to file
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(CvcCode))
            {
                tempUser = new User(UserName, Password, FirstName, LastName, Email, isSeller, new PaymentInfo(CreditCardNum, Int32.Parse(ExpirationMonth.ToString()), Int32.Parse(ExpirationYear.ToString()), CvcCode));
                Users.Add(tempUser);

                MainView.WriteXmlFile(Users);
                MainView.ActiveView = new LoginVM(MainView);
            }
            else
            {
                MessageBox.Show("Please fill all input boxes!");
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new DelegateCommand(CancelClicked);
                }

                return _cancelCommand;
            }
        }
        DelegateCommand _cancelCommand;

        // Switches back to login screen
        private void CancelClicked(object obj)
        {
            MainView.ActiveView = new LoginVM(MainView);
        }

    }
}
