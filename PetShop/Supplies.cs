using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{

    // Super class for all supply items in inventory
    public class Supplies : Item
    {
        
        public Supplies() : base()
        {
            animalAttachment = 1;
        }

        public Supplies(string description, string price, int stock, string name, string imagePath, int animalType) : base(description,price,stock,name,imagePath, animalType)
        {
            // Establishes relationship to animal 
            // 1 - Land Animal, 2 - Air Animal, 3 - Sea Animal
            animalAttachment = animalType;
        }

    }
}
