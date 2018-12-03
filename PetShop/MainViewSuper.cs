using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop
{
    // Super Class VM that powers the user controls for the main window of the application
    public abstract class MainViewSuper: INotifyPropertyChanged
    {

        // This class is never instanstiated but the subclasses use this empty constructor
        public MainViewSuper()
        {
            MainView = new MainWindowVM();
            Users = new List<User>();
            Items = new ObservableCollection<Item>();
            CurrentUser = new User();
        }

        // Main Container of the application
        public MainWindowVM MainView;
        public List<User> Users;

        // Save location for purchase receipts
        public string SaveLocation = "receipt.txt";


        // Container for main display of app 
        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }

        // Current user logged into the app
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

        // Item currently selected in the main ListBox
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


        // COMMANDS AND EVENT HANDLERS

        // Required for Bindings to Work
        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };


        // Command to add currently selected item to cart
        public ICommand AddToCartCommand
        {
            get
            {
                if (addToCartCommand == null)
                {
                    addToCartCommand = new DelegateCommand(AddToCart);
                }

                return addToCartCommand;
            }
        }
        DelegateCommand addToCartCommand;


        private void AddToCart(object obj)
        {
            if (SelectedItem != null)
            {
                // Ensures item is not sold out
                if (SelectedItem.Stock > 0)
                {
                    // User user has a cart as a data member, the cart is also a observable collection of items
                    SelectedItem.Stock -= 1;
                    CurrentUser.Cart.Add(SelectedItem);
                    // The total and items in the cart are tracked separetly from the cart, since they are need for data bindings
                    CurrentUser.Total += SelectedItem.Price;
                    CurrentUser.NumOfItemsInCart += 1;
                }
                else
                {
                    MessageBox.Show("Out of stock!");
                }
            }
            else
            {
                MessageBox.Show("Must select and item!");

            }
        }


        public ICommand ViewCartCommand
        {
            get
            {
                if (viewCartCommand == null)
                {
                    viewCartCommand = new DelegateCommand(ViewCart);
                }

                return viewCartCommand;
            }
        }
        DelegateCommand viewCartCommand;

        // Switches the view to the cart view by changing the Active view attribute
        private void ViewCart(object obj)
        {
            MainView.ActiveView = new CartViewVM(MainView, CurrentUser);
        }




        public ICommand CheckoutCommand
        {
            get
            {
                if (checkoutCommand == null)
                {
                    checkoutCommand = new DelegateCommand(checkoutClicked);
                }

                return checkoutCommand;
            }
        }
        DelegateCommand checkoutCommand;

        private void checkoutClicked(object obj)
        {
            if (CurrentUser.Cart.Count > 0)
            {
                // Saves the main item list to memory since the items in the cart have already been deducted
                MainView.WriteItemXmlFile(MainView.Items);
                // Switches to screen that shows final receipt
                MainView.ActiveView = new PurchaseRecieptVM(MainView, CurrentUser);
            }
            else
            {
                MessageBox.Show("Your cart is empty!");
            }
        }


        public virtual ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new DelegateCommand(logoutClicked);
                }

                return logoutCommand;
            }
        }
        DelegateCommand logoutCommand;

        private void logoutClicked(object obj)
        {
            // Clears the cart when the user logouts out
            // must be done, otherwise items will be deducted from inventory
            // Even thoght the use hasnt made a purchase
            CurrentUser.Cart.Clear();
            CurrentUser.Total = 0;
            CurrentUser.NumOfItemsInCart = 0;

            // Sets user to an empty user to ensure everything is disconnected from the main window
            CurrentUser = new User();

            //Returns to login screen
            MainView.ActiveView = new LoginVM(MainView);

        }




        // Command Delegate and Function To Exit Program
        public virtual ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(exitClicked);
                }

                return exitCommand;
            }
        }
        DelegateCommand exitCommand;

        private void exitClicked(object obj)
        {
            Application.Current.Shutdown();
        }

        public ICommand SaveAsCommand
        {
            get
            {
                if (saveAsCommand == null)
                {
                    saveAsCommand = new DelegateCommand(saveAsClicked);
                }

                return saveAsCommand;
            }
        }
        DelegateCommand saveAsCommand;


        // Function to save recipet to a user defined location
        private void saveAsClicked(object obj)
        {
            //Adds every item and price in the cart to string
            StringBuilder textFile = new StringBuilder();
            foreach (Item pet in CurrentUser.Cart)
            {
                textFile.Append(pet.ItemName + " - $" + pet.Price.ToString() + "\n");
            }

            //Adds total to string
            textFile.Append("---------------------\n");
            textFile.Append("Total = $" + CurrentUser.Total);

            //Opens save window
            SaveFileDialog saveAsPrompt = new SaveFileDialog();
            saveAsPrompt.Title = "Save receipt to your computer";
            saveAsPrompt.Filter = "Text File|*.txt";
            saveAsPrompt.ShowDialog();

            if (saveAsPrompt.FileName != "")
            {
                // Updates savelocation to ensure that when user selects the regular save its saved corectly
                SaveLocation = saveAsPrompt.FileName;
                
                // File is written to memory
                File.WriteAllText(SaveLocation, textFile.ToString());
                MessageBox.Show("Reciept saved sucessfully to " + SaveLocation);
            }

        }


        // Command and Function to save recipet to default location
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new DelegateCommand(saveClicked);
                }

                return saveCommand;
            }
        }
        DelegateCommand saveCommand;

        // Saves receipt to default location
        private void saveClicked(object obj)
        {
            StringBuilder textFile = new StringBuilder();
            foreach (Item pet in CurrentUser.Cart)
            {
                textFile.Append(pet.ItemName + " - $" + pet.Price.ToString() + "\n");
            }

            textFile.Append("---------------------\n");
            textFile.Append("Total = $" + CurrentUser.Total);

            File.WriteAllText(SaveLocation, textFile.ToString());
            MessageBox.Show("Reciept saved sucessfully to " + SaveLocation);

        }


        public ICommand BackPetCommand
        {
            get
            {
                if (backPetCommand == null)
                {
                    backPetCommand = new DelegateCommand(backPetClicked);
                }

                return backPetCommand;
            }
        }
        DelegateCommand backPetCommand;

        // Back butten for the Add Pet,Edit Pet,and Remove Pet windows
        private void backPetClicked(object obj)
        {
            // Saves binding changes if switching back to main screen
            MainView.WriteItemXmlFile(Items);
            MainView.ActiveView = new SellerViewVM(MainView);
        }

    }
}

