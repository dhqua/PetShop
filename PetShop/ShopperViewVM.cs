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
    public class ShopperViewVM : INotifyPropertyChanged
    {

        public ShopperViewVM()
        {

        }

        public ShopperViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = MainView.Users;
            Items = MainView.Items;
            CurrentUser = mainView.CurrentUser;

        }
        public MainWindowVM MainView;
        public List<User> Users;
        public string SaveLocation = "receipt.txt";

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

        // COMMANDS AND EVENT HANDLERS

        //public User currentUser = new User();
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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
                if( SelectedItem.Stock > 0)
                {
                    SelectedItem.Stock -= 1;
                    CurrentUser.Cart.Add(SelectedItem);
                    CurrentUser.Total += double.Parse(SelectedItem.Price);
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
            if(CurrentUser.Cart.Count > 0)
            {
                MainView.WriteItemXmlFile(MainView.Items);
                MainView.ActiveView = new PurchaseRecieptVM(MainView, CurrentUser);
            }
            else
            {
                MessageBox.Show("Your cart is empty!");
            }
        }




        public ICommand ExitCommand
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

        private void saveAsClicked(object obj)
        {
            StringBuilder textFile = new StringBuilder();
            foreach (Item pet in CurrentUser.Cart)
            {
                textFile.Append(pet.ItemName + " - $" + pet.Price + "\n");
            }

            textFile.Append("---------------------\n");
            textFile.Append("Total = $" + CurrentUser.Total);

            SaveFileDialog saveAsPrompt = new SaveFileDialog();
            saveAsPrompt.Title = "Save receipt to your computer";
            saveAsPrompt.Filter = "Text File|*.txt";
            saveAsPrompt.ShowDialog();

            if (saveAsPrompt.FileName != "")
            {
                SaveLocation = saveAsPrompt.FileName;
                File.WriteAllText(SaveLocation, textFile.ToString());
                MessageBox.Show("Reciept saved sucessfully to " + SaveLocation);
            }


        }

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

        private void saveClicked(object obj)
        {
            StringBuilder textFile = new StringBuilder();
            foreach (Item pet in CurrentUser.Cart)
            {
                textFile.Append(pet.ItemName + " - $" + pet.Price + "\n" );
            }

            textFile.Append("---------------------\n");
            textFile.Append("Total = $" + CurrentUser.Total);

            File.WriteAllText(SaveLocation, textFile.ToString());
            MessageBox.Show("Reciept saved sucessfully to " + SaveLocation);

        }



        /*
        void saveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder csvFile = new StringBuilder();

            string csvHeader = string.Format("Emp ID,Name Prefix, First Name,Last Name, Gender, Date of Birth, SSN, Phone No. ,E Mail, Street Address,Street Name, City, State, Zip, StartDate, Salary, User Name,Password,Permissions Level{0}", Environment.NewLine);

            // Adds header to csvFile string to be written out
            csvFile.Append(csvHeader);

            foreach (Employee toWrite in Employees)
            {
                //Values 0-19 represent the data values and 20 represents the newline character
                string formattedEmployee = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}{19}",
                                                         toWrite.IDNumber.ToString(),
                                                         toWrite.Prefix,
                                                         toWrite.FirstName,
                                                         toWrite.LastName,
                                                         toWrite.Gender,
                                                         toWrite.BirthDate,
                                                         toWrite.SocSec,
                                                         toWrite.Phone,
                                                         toWrite.Email,
                                                         toWrite.EmployeeAddress.HouseNumber.ToString(),
                                                         toWrite.EmployeeAddress.StreetName,
                                                         toWrite.EmployeeAddress.City,
                                                         toWrite.EmployeeAddress.State,
                                                         toWrite.EmployeeAddress.ZipCode.ToString(),
                                                         toWrite.StartDate,
                                                         toWrite.Salary.ToString(),
                                                         toWrite.UserName,
                                                         toWrite.Password,
                                                         toWrite.PermLevel.ToString(),
                                                         Environment.NewLine
                                                         );

                csvFile.Append(formattedEmployee);
            }

            SaveFileDialog saveWindow = new SaveFileDialog();
            saveWindow.Filter = "Comma Separated Values|*.csv";
            saveWindow.Title = "Save employees as externally";
            saveWindow.ShowDialog();

            if (saveWindow.FileName != "")
            {
                File.WriteAllText(saveWindow.FileName, csvFile.ToString());
                MessageBox.Show("Employees saved sucessfully");
            }
        }*/


    }
}
