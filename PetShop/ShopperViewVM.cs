
namespace PetShop
{
    public class ShopperViewVM : MainViewSuper
    {

        public ShopperViewVM()
        {

        }

        public ShopperViewVM(MainWindowVM mainView) 
        {
            MainView = mainView;
            Users = MainView.Users;
            Items = MainView.Items;
            CurrentUser = MainView.CurrentUser;
        }
        

    }
}
