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

        public AddPetVM(MainWindowVM mainView, ObservableCollection<Item> items)
        {
            MainView = mainView;
            Items = items;
        }


        // New Item Property Bindings
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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

        private void AddPet(object obj)
        {

            if(fieldsValid() )
            {
                if(Int32.Parse(productType) == 0)
                {
                    Items.Add(new LandAnimal("Test", Price, Int32.Parse(Stock), Name, ImageSource, 0, 1));
                }
                else if (Int32.Parse(productType) == 1)
                {
                    Items.Add(new AirAnimal("Test", Price, Int32.Parse(Stock), Name, ImageSource, 0, 2));
                }
                else if (Int32.Parse(productType) == 2)
                {
                    Items.Add(new SeaAnimal("Test", Price, Int32.Parse(Stock), Name, ImageSource, 0, 3));
                }
                else if (Int32.Parse(productType) == 3)
                {
                    Items.Add(new Food("Test", Price, Int32.Parse(Stock), Name, ImageSource,false,1));
                }
                else if (Int32.Parse(productType) == 4)
                {
                    Items.Add(new Food("Test", Price, Int32.Parse(Stock), Name, ImageSource, false, 2));
                }
                else if (Int32.Parse(productType) == 5)
                {
                    Items.Add(new Food("Test", Price, Int32.Parse(Stock), Name, ImageSource, false, 3));
                }
                else if (Int32.Parse(productType) == 6)
                {
                    Items.Add(new Clothing("Test", Price, Int32.Parse(Stock), Name, ImageSource, "cotton", 1));
                }
                else if (Int32.Parse(productType) == 7)
                {
                    Items.Add(new Clothing("Test", Price, Int32.Parse(Stock), Name, ImageSource, "cotton", 1));
                }
                else if (Int32.Parse(productType) == 8)
                {
                    Items.Add(new Clothing("Test", Price, Int32.Parse(Stock), Name, ImageSource, "cotton", 1));
                }

                // Write Changes file 
                MainView.WriteItemXmlFile(Items);

                // Clear fields in case user wants add another
                Name = "";
                Stock = "";
                Price = "";
                ImageSource = "";
                ProductType = "0";

                // Let user it was successful
                MessageBox.Show("The item has been added to our inventory!", "Success!");

            }
            else
            {
                MessageBox.Show("Please double check fields.", "Invalid Entry");
            }
        }



        // Command and Function to save recipet to a user define location
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

        private void openClicked(object obj)
        {
            

            OpenFileDialog openWindow = new OpenFileDialog();
            openWindow.Title = "Select Image for you new product!";
            openWindow.Filter = "Jpeg File | *.jpg | Png File| *.png | All Files | *.*";
            openWindow.ShowDialog();

            ImageSource = openWindow.FileName;

        }


        private bool fieldsValid()
        {
            return (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Stock) && !string.IsNullOrWhiteSpace(Price) && !string.IsNullOrWhiteSpace(ImageSource) && !string.IsNullOrWhiteSpace(ProductType));
        }

    }
}
