
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;

namespace PetShop
{
    public class ShopperViewVM : MainViewSuper
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        ObservableCollection<Item> backupProductList = new ObservableCollection<Item>();

        private string petFilter;
        public string PetFilter
        {
            get { return petFilter; }
            set
            {
                if (value != null)
                {
                    petFilter = value.ToString();
                }
                PropertyChanged(this, new PropertyChangedEventArgs("PetFilter"));
            }
        }

        private string supplyFilter;
        public string SupplyFilter
        {
            get { return supplyFilter; }
            set
            {
                if (value != null)
                {
                    supplyFilter = value.ToString();
                }
                PropertyChanged(this, new PropertyChangedEventArgs("SupplyFilter"));
            }
        }


        public ShopperViewVM() : base()
        {
        }

        public ShopperViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = MainView.Users;
            Items = MainView.Items;
            CurrentUser = MainView.CurrentUser;

        }

        // Test constructor for filter
        public ShopperViewVM(MainWindowVM mainView, ObservableCollection<Item> resultItems)
        {
            MainView = mainView;
            Users = MainView.Users;
            Items = resultItems;
            CurrentUser = MainView.CurrentUser;

        }


        public ICommand FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new DelegateCommand(filterClicked);
                }

                return filterCommand;
            }
        }
        DelegateCommand filterCommand;

        private void filterClicked(object obj)
        {
            // Saves current state of product list before it is filterd
            foreach (Item product in Items)
            {
                backupProductList.Add(product);
            }

            ObservableCollection<Item> results = new ObservableCollection<Item>();

            // Pet filter logic
            if (PetFilter != null)
            {

                // Only adds pets if the user did not specify supplies
                if ( SupplyFilter.Contains("All Supplies"))
                { 
                    foreach (Item product in Items)
                    {

                        // Checks if item is an animal
                        if (product.GetType().Equals(typeof(LandAnimal)) && (petFilter.Contains("Land Pet") || PetFilter.Contains("All Pets")) )
                        {
                            results.Add(product);
                        }

                        if (product.GetType().Equals(typeof(AirAnimal)) && (petFilter.Contains("Air Pet") || PetFilter.Contains("All Pets")))
                        {
                            results.Add(product);
                        }

                        if (product.GetType().Equals(typeof(SeaAnimal)) && (petFilter.Contains("Sea Pet") || PetFilter.Contains("All Pets")))
                        {
                            results.Add(product);
                        }
                    }
            }
            }


            // Supply filter login
            if (SupplyFilter != null)
            {
                foreach (Item product in Items)
                {
                   if(PetFilter.Contains("All Pets") )
                    {
                        if(SupplyFilter.Contains("All Supplies"))
                        {
                            if (product.GetType().Equals(typeof(Food)) || product.GetType().Equals(typeof(Clothing)) )
                                results.Add(product);
                        }
                        else if (SupplyFilter.Contains("Food"))
                        {
                            if(product.GetType().Equals(typeof(Food)))
                            {
                                results.Add(product);
                            }
                        }
                        else if(SupplyFilter.Contains("Clothing"))
                        {
                            if (product.GetType().Equals(typeof(Clothing)))
                            {
                                results.Add(product);
                            }
                        }
                    }
                   else if(PetFilter.Contains("Land Pet") )
                    {
                        if(product.animalAttachment == Item.LandPet && product.GetType().Equals(typeof(Food)) && ( SupplyFilter.Contains("Food") || SupplyFilter.Contains("All Supplies")) )
                        {
                            results.Add(product);
                        }
                        else if (product.animalAttachment == Item.LandPet && product.GetType().Equals(typeof(Clothing)) && SupplyFilter.Contains("Clothing"))
                        {
                            results.Add(product);
                        }
                    }
                    else if (PetFilter.Contains("Air Pet"))
                    {
                        if (product.animalAttachment == Item.AirPet && product.GetType().Equals(typeof(Food)) &&  ( SupplyFilter.Contains("Food") || SupplyFilter.Contains("All Supplies") ))
                        {
                            results.Add(product);
                        }
                        else if (product.animalAttachment == Item.AirPet && product.GetType().Equals(typeof(Clothing))  && SupplyFilter.Contains("Clothing"))
                        {
                            results.Add(product);
                        }
                    }
                    else if (PetFilter.Contains("Sea Pet"))
                    {
                        if (product.animalAttachment == Item.SeaPet && product.GetType().Equals(typeof(Food)) && (SupplyFilter.Contains("Food") || SupplyFilter.Contains("All Supplies") ))
                        {
                            results.Add(product);
                        }
                        else if (product.animalAttachment == Item.SeaPet && product.GetType().Equals(typeof(Clothing)) && SupplyFilter.Contains("Clothing"))
                        {
                            results.Add(product);
                        }
                    }


                }

            }

            // Passes the main window container, the results list, and the shopping winodw to return to
            MainView.ActiveView = new FilterWindowVM(MainView, results, this);
        }
    }
}
