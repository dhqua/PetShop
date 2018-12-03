using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop
{
    public class CartViewVM : MainViewSuper
    {

        public CartViewVM() : base()
        {

        }

        // mainView need to switch back to main shopper view, and user is needed to access the cart
        public CartViewVM(MainWindowVM mainView, User currentUser)
        {
            MainView = mainView;
            CurrentUser = currentUser;
        }



        public ICommand RemoveCartCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new DelegateCommand(removeClicked);
                }

                return _removeCommand;
            }
        }
        DelegateCommand _removeCommand;


        private void removeClicked(object obj)
        {
            // Removes item item from cart and decrements tracking variables
            if(SelectedItem != null)
            {
                // This line adds item back to main invetory
                // Even though this item is in cart it is still conected to main inventory through bindings
                SelectedItem.Stock += 1;

                CurrentUser.Total -= double.Parse(SelectedItem.Price);
                CurrentUser.NumOfItemsInCart -= 1;
                CurrentUser.Cart.Remove(SelectedItem);

            }
            else
            {
                MessageBox.Show("Must select an item to remove!");
            }

        }


        public ICommand BackCartCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new DelegateCommand(backClicked);
                }

                return _backCommand;
            }
        }
        DelegateCommand _backCommand;

        // Swithes back to main shopper view
        private void backClicked(object obj)
        {
            MainView.ActiveView = new ShopperViewVM(MainView);
        }


    }
}
