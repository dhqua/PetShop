using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop
{
    public class EditPetVM : MainViewSuper
    {
        public EditPetVM() : base()
        {

        }

        // Requires mainView to switch back to seller screen, and items represents the store inventory
        public EditPetVM(MainWindowVM mainView, ObservableCollection<Item> items)
        {
            MainView = mainView;
            CurrentUser = MainView.CurrentUser;
            Items = items;
        }


        // Overrides the logout and exit function from the SuperClass
        // Seller class needs to write XML in these cases to make sure the edit was saved
        public override ICommand LogoutCommand
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
            CurrentUser.Cart.Clear();
            CurrentUser.Total = 0;
            CurrentUser.NumOfItemsInCart = 0;
            CurrentUser = new User();
            MainView.WriteItemXmlFile(Items);
            MainView.ActiveView = new LoginVM(MainView);

        }


        // Command Delegate and Function To Exit Program, overrides to ensure XML is saved
        public override ICommand ExitCommand
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
            MainView.WriteItemXmlFile(Items);
            Application.Current.Shutdown();
        }

    }
}
