using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class ShopperViewVM : INotifyPropertyChanged
    {
        public MainWindowVM MainView;
        public List<User> Users;

        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentUser"));
            }
        }


        public User currentUser = new User();

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ShopperViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = MainView.Users;
            CurrentUser = mainView.CurrentUser;

        }
    }
}
