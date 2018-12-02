using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop
{
    public class SellerViewVM : MainViewSuper
    {

        public SellerViewVM() : base()
        {

        }
        public SellerViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            CurrentUser = MainView.CurrentUser;
        }
        

        public ICommand RemovePetCommand
        {
            get
            {
                if (removePetCommand == null)
                {
                    removePetCommand = new DelegateCommand(removePet);
                }

                return removePetCommand;
            }
        }
        DelegateCommand removePetCommand;

        private void removePet(object obj)
        {
            MainView.ActiveView = new RemovePetViewVM(MainView, MainView.Items);
        }


        public ICommand EditPetCommand
        {
            get
            {
                if (editPetCommand == null)
                {
                    editPetCommand = new DelegateCommand(editPet);
                }

                return editPetCommand;
            }
        }
        DelegateCommand editPetCommand;

        private void editPet(object obj)
        {
            MainView.ActiveView = new EditPetVM(MainView, MainView.Items);
        }

        public ICommand AddPetCommand
        {
            get
            {
                if (addPetCommand == null)
                {
                    addPetCommand = new DelegateCommand(addPet);
                }

                return addPetCommand;
            }
        }
        DelegateCommand addPetCommand;

        private void addPet(object obj)
        {
            MainView.ActiveView = new AddPetVM(MainView, MainView.Items);

        }



    }
}
