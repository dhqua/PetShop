using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class SellerViewVM : MainViewSuper
    {

        public SellerViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = MainView.Users;
            CurrentUser = MainView.CurrentUser;


        }
    }
}
