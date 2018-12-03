using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    public class SeaAnimal : Animal
    {
        // Required for bindings to work
        public override event PropertyChangedEventHandler PropertyChanged = delegate { };

        public SeaAnimal() : base()
        {
            SwimSpeed = 0;
        }

        public SeaAnimal(string description, string price, int stock, string name, string imagePath, double swimSpeed, int animalAttachment) : base( description,  price,  stock,  name,  imagePath, animalAttachment)
        {
            SwimSpeed = swimSpeed;
        }

        [XmlIgnore]
        private double swimSpeed;

        [XmlElement(DataType = "double", ElementName = "SwimSpeed")]
        public double SwimSpeed
        {
            get { return swimSpeed; }
            set
            {
                swimSpeed = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SwimSpeed"));
            }
        }


    }
}
