using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop
{
    public class RemovePetViewVM : MainViewSuper
    {
        public RemovePetViewVM() : base()
        {
        }

        // mainView need to switch back to seller screen, Items is needed to actually edit inventory
        public RemovePetViewVM(MainWindowVM mainView, ObservableCollection<Item> items)
        {
            MainView = mainView;
            CurrentUser = MainView.CurrentUser;
            Items = items;
        }
        

        public ICommand RemovePetCommand
        {
            get
            {
                if (removePetCommand == null)
                {
                    removePetCommand = new DelegateCommand(removePetClick);
                }

                return removePetCommand;
            }
        }
        DelegateCommand removePetCommand;
        
        // Since the selcted item is of the listbox is already bound, removing a pet from inventory is trivial
        private void removePetClick(object obj)
        {
            if(SelectedItem != null)
            { 
                Items.Remove(SelectedItem);
                MainView.WriteItemXmlFile(Items);
            }
        }



    }
}
