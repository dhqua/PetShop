using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    public class Food : Supplies
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Food() : base()
        {
            isNonPerishable = false;
        }

        public Food(string description, string price, int stock, string name, string imagePath, bool nonPerishable, int typeOfAnimal) : base( description,  price,  stock,  name,  imagePath, typeOfAnimal)
        {
            IsNonPerishable = nonPerishable;
        }

        [XmlIgnore]
        private bool isNonPerishable;

        [XmlElement(DataType = "boolean", ElementName = "IsNonPerishable")]
        public bool IsNonPerishable
        {
            get { return isNonPerishable; }
            set
            {
                isNonPerishable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsNonPerishable"));
            }
        }
    }
}
