using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{

    [XmlInclude(typeof(Supplies))]
    [XmlInclude(typeof(LandAnimal))]
    [XmlInclude(typeof(SeaAnimal))]
    [XmlInclude(typeof(AirAnimal))]
    [XmlInclude(typeof(Animal))]
    [XmlInclude(typeof(Clothing))]
    [XmlInclude(typeof(Food))]
    [XmlRoot(ElementName = "Item")]
    public class Item : INotifyPropertyChanged
    {

        // This event can only be called from the class in which it is instanstiated so it also present in the subclasses
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        // Constants used for Filtering Later
        public static int LandPet = 1;
        public static int AirPet = 2;
        public static int SeaPet = 3;


        public Item()
        {
            Description = "";
            Price = "";
            Stock = 0;
            ItemName = "";
            ImagePath = "";
            animalAttachment = 0;
        }

        public Item(string description, string price, int stock, string name, string imagePath,int animalConnection)
        {
            Description = description;
            Price = price;
            Stock = stock;
            ItemName = name;
            ImagePath = imagePath;
            animalAttachment = animalConnection;
        }

        [XmlIgnore]
        private string description;
        [XmlElement(DataType = "string", ElementName = "Description")]
        public string Description {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }
        [XmlIgnore]
        private string price;
        [XmlElement(DataType = "string", ElementName = "Price")]
        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }

        [XmlIgnore]
        private int stock;
        [XmlElement(DataType = "int", ElementName = "Stock")]
        public int Stock
        {
            get { return stock; }
            set
            {
                if (value.GetType().ToString() == "Int")
                {
                    stock = value;
                }
                else
                {
                    stock = Int32.Parse(value.ToString());
                }
                PropertyChanged(this, new PropertyChangedEventArgs("Stock"));
            }
        }

        [XmlIgnore]
        private string itemName;[XmlElement(DataType = "string", ElementName = "ItemName")]
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemName"));
            }
        }

        [XmlIgnore]
        private string imagePath;
        [XmlElement(DataType = "string", ElementName = "ImagePath")]
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
            }
        }

        // This member will be the connection between a specific supply and the animal it is for
        // 1= Land Animal , 2 = Air Animal, 3 = Sea Animal
        [XmlElement(DataType = "int", ElementName = "animalAttachment")]
        public int animalAttachment;

    }
}
