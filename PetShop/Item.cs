using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{


    [XmlRoot(ElementName = "Item")]
    public class Item : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public Item()
        {
                
        }

        public Item(string description, string price, int stock, string name, string imagePath)
        {
            Description = description;
            Price = price;
            Stock = stock;
            ItemName = name;
            ImagePath = imagePath;
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
                stock = value;
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

    }
}
