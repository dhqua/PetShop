using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetShop
{
    public class AddPetVM : MainViewSuper
    {
        public AddPetVM() : base()
        {

        }

        // Main Container and Items list are need for login so they passed in the constructor
        public AddPetVM(MainWindowVM mainView, ObservableCollection<Item> items)
        {
            MainView = mainView;
            Items = items;
        }


        // New Item Property Bindings
        public override event PropertyChangedEventHandler PropertyChanged = delegate { };

        // Represent product name
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        // Represents available stock
        private string stock;
        public string Stock
        {
            get { return stock; }
            set
            {
                stock = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Stock"));
                PropertyChanged(MainView, new PropertyChangedEventArgs("Stock"));
            }
        }


        //Represents item price
        private string price;
        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }

        // Stores the file path for the item
        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImageSource"));
            }
        }

        // Stores the type of producted based on the combo box selection
        private string productType;
        public string ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProductType"));
            }
        }


        // COMMANDS AND FUNCTIONS

        public ICommand AddPetCommand
        {
            get
            {
                if (addPetCommand == null)
                {
                    addPetCommand = new DelegateCommand(AddPet);
                }

                return addPetCommand;
            }
        }
        DelegateCommand addPetCommand;

        // Adds pet to main Items list which represnts the store's inventory
        private void AddPet(object obj)
        {
            // If no fields are blank
            if(fieldsValid() )
            {
                // Adds the corresponding Item based on the item type which was selected in the combbox
                if(Int32.Parse(productType) == 0)
                {
                    Items.Add(new LandAnimal("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, 0, 1));
                }
                else if (Int32.Parse(productType) == 1)
                {
                    Items.Add(new AirAnimal("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, 0, 2));
                }
                else if (Int32.Parse(productType) == 2)
                {
                    Items.Add(new SeaAnimal("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, 0, 3));
                }
                else if (Int32.Parse(productType) == 3)
                {
                    Items.Add(new Food("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource,false,1));
                }
                else if (Int32.Parse(productType) == 4)
                {
                    Items.Add(new Food("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, false, 2));
                }
                else if (Int32.Parse(productType) == 5)
                {
                    Items.Add(new Food("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, false, 3));
                }
                else if (Int32.Parse(productType) == 6)
                {
                    Items.Add(new Clothing("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, "cotton", 1));
                }
                else if (Int32.Parse(productType) == 7)
                {
                    Items.Add(new Clothing("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, "cotton", 1));
                }
                else if (Int32.Parse(productType) == 8)
                {
                    Items.Add(new Clothing("Test", Double.Parse(Price), Int32.Parse(Stock), Name, ImageSource, "cotton", 1));
                }

                // Write Changes file 
                MainView.WriteItemXmlFile(Items);

                // Clear fields in case user wants add another product
                Name = "";
                Stock = "";
                Price = "";
                ImageSource = "";
                ProductType = "0";

                // Let user know addition was successful
                MessageBox.Show("The item has been added to our inventory!", "Success!");

            }
            else
            {
                MessageBox.Show("Please double check fields.", "Invalid Entry");
            }
        }



        public ICommand OpenFileCommand
        {
            get
            {
                if (openCommand == null)
                {
                    openCommand = new DelegateCommand(openClicked);
                }

                return openCommand;
            }
        }
        DelegateCommand openCommand;

        // Uses open file dialog to get file name of the image user wants the pet to have
        private void openClicked(object obj)
        {
            

            OpenFileDialog openWindow = new OpenFileDialog();
            openWindow.Title = "Select Image for you new product!";
            // Filters the file dialog for image files
            openWindow.Filter = "Jpeg File|*.jpg|Png File|*.png|All Files|*.*";
            openWindow.ShowDialog();

            // Sets the image source property that will be used when the object is created
            ImageSource = openWindow.FileName;

        }

        // Makes sure none of the fields are blank
        private bool fieldsValid()
        {
            return (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Stock) && !string.IsNullOrWhiteSpace(Price) && !string.IsNullOrWhiteSpace(ImageSource) && !string.IsNullOrWhiteSpace(ProductType));
        }

    }
}
