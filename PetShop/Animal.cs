using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    // Super class for all animals
    public abstract class Animal : Item
    {
        public Animal() : base()
        {

        }

        public Animal(string description, string price, int stock, string name, string imagePath, int animalAttachment) : base(description,price,stock,name,imagePath, animalAttachment)
        {

        }
    }
}
