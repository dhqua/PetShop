using System.Windows.Input;

namespace PetShop
{
    public class PurchaseRecieptVM : MainViewSuper
    {

        public PurchaseRecieptVM() : base()
        {

        }

        public PurchaseRecieptVM(MainWindowVM mainView, User currentUser)
        {
            MainView = mainView;
            CurrentUser = currentUser;
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


    }
}
