using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    public class Supplies : Item
    {
        
        public Supplies() : base()
        {
            animalAttachment = 1;
        }

        public Supplies(string description, string price, int stock, string name, string imagePath, int animalType) : base(description,price,stock,name,imagePath, animalType)
        {
            animalAttachment = animalType;
        }

        




        // Function that attacheds supply to a pet
        // Is needed in order for filter function to work correctly
        public int getSupplyType()
        {
            return animalAttachment;
        }
    }
}
