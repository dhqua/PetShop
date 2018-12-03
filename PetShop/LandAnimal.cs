using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    public class LandAnimal : Animal
    {
        // Required for bindings to work
        public override event PropertyChangedEventHandler PropertyChanged = delegate { };

        public LandAnimal() : base()
        {
            LandSpeed = 0;
        }

        public LandAnimal(string description, string price, int stock, string name, string imagePath, double landSpeed, int animalAttachment) : base( description,  price,  stock,  name,  imagePath, animalAttachment)
        {
            LandSpeed = landSpeed;
        }

        [XmlIgnore]
        private double landSpeed;

        [XmlElement(DataType = "double", ElementName = "LandSpeed")]
        public double LandSpeed
        {
            get { return landSpeed; }
            set
            {
                landSpeed = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LandSpeed"));
            }
        }
    }

}
