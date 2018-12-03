using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop
{
    public class FilterWindowVM : MainViewSuper
    {
        // Allows filter view to retun to shopper screen, and maintain the same cart items
        ShopperViewVM ReturnWindow;

        public FilterWindowVM() : base()
        {

        }

        public FilterWindowVM(MainWindowVM mainView, ObservableCollection<Item> filterResults, ShopperViewVM returnWindow)
        {
            MainView = mainView;
            Items = filterResults;
            CurrentUser = returnWindow.CurrentUser;
            ReturnWindow = returnWindow;
        }


        public ICommand FilterBackCommand
        {
            get
            {
                if (filterBackCommand == null)
                {
                    filterBackCommand = new DelegateCommand(filterBackClicked);
                }

                return filterBackCommand;
            }
        }
        DelegateCommand filterBackCommand;
        
        // Returns to shopping window, all other data is tracked through bindings in the cart
        private void filterBackClicked(object obj)
        {
            MainView.ActiveView = ReturnWindow;
        }

        }
}
