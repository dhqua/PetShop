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
        // Ensures bindings will work
        public override event PropertyChangedEventHandler PropertyChanged = delegate { };

        public AirAnimal() : base()
        {
            // property that differentiates AirAnimal from Animal
            MaxFlightHeight = 0;
        }

        public AirAnimal(string description, double price, int stock, string name, string imagePath, double fligtHeight, int animalAttachment) : base(description,price,stock,name,imagePath, animalAttachment)
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
