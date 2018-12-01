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
    public class CartViewVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public CartViewVM()
        {

        }

        public CartViewVM(MainWindowVM mainView, User currentUser)
        {
            MainView = mainView;
            CurrentUser = currentUser;
        }

        MainWindowVM MainView;

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

        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
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
            if(SelectedItem != null)
            {
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


        private void backClicked(object obj)
        {
            MainView.ActiveView = new ShopperViewVM(MainView);
        }


    }
}
