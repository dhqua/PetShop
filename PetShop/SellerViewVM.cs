using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class SellerViewVM : INotifyPropertyChanged
    {
        public MainWindowVM MainView;
        public List<User> Users;


        public User currentUser;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public SellerViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = MainView.Users;
            currentUser = MainView.CurrentUser;


        }
    }
}
