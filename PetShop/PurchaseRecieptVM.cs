using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop
{
    public class PurchaseRecieptVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public PurchaseRecieptVM()
        {

        }

        public PurchaseRecieptVM(MainWindowVM mainView, User currentUser)
        {
            MainView = mainView;
            CurrentUser = currentUser;
        }

        MainWindowVM MainView;
        public string SaveLocation = "receipt.txt";

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


        // Back button for the recipet page
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
            CurrentUser.Cart.Clear();
            CurrentUser.Total = 0;
            CurrentUser.NumOfItemsInCart = 0;
            MainView.ActiveView = new ShopperViewVM(MainView);
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
                textFile.Append(pet.ItemName + " - $" + pet.Price + "\n");
            }

            textFile.Append("---------------------\n");
            textFile.Append("Total = $" + CurrentUser.Total);

            File.WriteAllText(SaveLocation, textFile.ToString());
            MessageBox.Show("Reciept saved sucessfully to " + SaveLocation);

        }


    }
}
