using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace PetShop
{
    public class AirAnimal : Animal
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public AirAnimal() : base()
        {
            MaxFlightHeight = 0;
        }

        public AirAnimal(string description, string price, int stock, string name, string imagePath, double fligtHeight, int animalAttachment) : base(description,price,stock,name,imagePath, animalAttachment)
        {
            MaxFlightHeight = fligtHeight;
        }

        [XmlIgnore]
        private double maxFlightHeight;

        [XmlElement(DataType = "double", ElementName = "MaxFlightHeight")]
        public double MaxFlightHeight
        {
            get { return maxFlightHeight; }
            set
            {
                maxFlightHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MaxFlightHeight") );
            }
        }

    }
}
