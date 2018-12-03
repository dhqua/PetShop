using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    public class Clothing : Supplies
    {
    
        // Needed for bindings to work
        public override event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Clothing() : base()
        {
            FabricMaterial = "";
        }

        public Clothing(string description, double price, int stock, string name, string imagePath,string fabric, int typeOfAnimal) : base( description,  price,  stock,  name,  imagePath, typeOfAnimal)
        {
            // Differentiates clothing from supplies
            FabricMaterial = fabric;
        }

        [XmlIgnore]
        private string fabricMaterial;

        [XmlElement(DataType = "string", ElementName = "FabricMaterial")]
        public string FabricMaterial
        {
            get { return fabricMaterial; }
            set
            {
                fabricMaterial = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FabricMaterial"));
            }
        }
    }

}
