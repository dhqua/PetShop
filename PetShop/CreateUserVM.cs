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
        User tempUser;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        MainWindowVM MainView;
        List<User> Users;


        public CreateUserVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = mainView.Users;
        }

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

        private void CreateClicked(object obj)
        {
            if (obj != null)
            {
                PasswordBox workArond = obj as PasswordBox;
                Password = workArond.Password;
            }

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

        private void CancelClicked(object obj)
        {
            MainView.ActiveView = new LoginVM(MainView);
        }

    }
}
