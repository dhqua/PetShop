
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;

namespace PetShop
{
    public class ShopperViewVM : MainViewSuper
    {
        public ShopperViewVM() : base()
        {
        }

        // Main constructor for shopper view
        public ShopperViewVM(MainWindowVM mainView)
        {
            MainView = mainView;
            Users = MainView.Users;
            Items = MainView.Items;
            CurrentUser = MainView.CurrentUser;

        }

        // Constructor used for filter, results from from filtering passed to be new listbox source
        public ShopperViewVM(MainWindowVM mainView, ObservableCollection<Item> resultItems)
        {
            MainView = mainView;
            Users = MainView.Users;
            Items = resultItems;
            CurrentUser = MainView.CurrentUser;
        }

        // Needed for bindings to work
        public override event PropertyChangedEventHandler PropertyChanged = delegate { };


        // Bound to the combo box in the view that filters by animal type
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

        // Bound to combo box in the view that filters by supply type
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

            ObservableCollection<Item> results = new ObservableCollection<Item>();

            // Null check for the combo box
            if (PetFilter != null)
            {

                // Only adds pets if the user did not specify supplies
                if ( SupplyFilter.Contains("All Supplies"))
                { 
                    foreach (Item product in Items)
                    {

                        // Adds animal to the results based on the pet filter combo box and object type
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


            // Supply filter null check
            if (SupplyFilter != null)
            {
                foreach (Item product in Items)
                {
                   if(PetFilter.Contains("All Pets") )
                    {
                        if(SupplyFilter.Contains("All Supplies"))
                        {
                            // Adds every item in inventory to the results
                            if (product.GetType().Equals(typeof(Food)) || product.GetType().Equals(typeof(Clothing)) )
                                results.Add(product);
                        }
                        else if (SupplyFilter.Contains("Food"))
                        {
                            // Adds all of the food in inventory to the results
                            if(product.GetType().Equals(typeof(Food)))
                            {
                                results.Add(product);
                            }
                        }
                        else if(SupplyFilter.Contains("Clothing"))
                        {
                            // Adds all of the clothing in invetory to results
                            if (product.GetType().Equals(typeof(Clothing)))
                            {
                                results.Add(product);
                            }
                        }
                    }
                   else if(PetFilter.Contains("Land Pet") )
                    {
                        // Adds all land pet food based on combo box and animalAttachment variable which establishes relationship between supply and animal
                        if(product.animalAttachment == Item.LandPet && product.GetType().Equals(typeof(Food)) && ( SupplyFilter.Contains("Food") || SupplyFilter.Contains("All Supplies")) )
                        {
                            results.Add(product);
                        }
                        // Adds all land pet clothing based on combo box and animalAttachment variable which establishes relationship between supply and animal
                        else if (product.animalAttachment == Item.LandPet && product.GetType().Equals(typeof(Clothing)) && SupplyFilter.Contains("Clothing"))
                        {
                            results.Add(product);
                        }
                    }
                    else if (PetFilter.Contains("Air Pet"))
                    {
                        // See land pet comments for explantion
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
                        // See land pet comments for explantion
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

            // Switches to view that diplays results
            // Passes the main window container, the results list, and the shopping window to return to
            MainView.ActiveView = new FilterWindowVM(MainView, results, this);
        }
    }
}
