using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{

    [XmlRoot(ElementName = "User")]
    //CONSTRUCTOR
    public class User : INotifyPropertyChanged
    {

        public User()
        {
            // Initializes Cart
            Cart = new ObservableCollection<Item>();
            Total = 0;
            NumOfItemsInCart = 0;
        }

        public User(string uName, string pWord, string firstName, string lastName, string email, bool seller, PaymentInfo cardInfo)
        {
            UserName = uName;
            Password = pWord;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            isSeller = seller;
            creditCardInfo = cardInfo;

            // Initializes Cart
            Cart = new ObservableCollection<Item>();
            Total = 0;
            NumOfItemsInCart = 0;
        }


        [XmlElement(DataType = "string", ElementName = "UserName")]
            public string UserName { get; set; }

            [XmlElement(DataType = "string", ElementName = "Password")]
            public string Password { get; set; }

            [XmlElement(DataType = "string", ElementName = "FirstName")]
            public string FirstName { get; set; }

            [XmlElement(DataType = "string", ElementName = "LastName")]
            public string LastName { get; set; }

            [XmlElement(DataType = "string", ElementName = "Email")]
            public string Email { get; set; }

            [XmlElement(DataType = "boolean", ElementName = "isSeller")]
            public bool isSeller { get; set; }  

            [XmlElement(ElementName ="UserPaymentInfo")]
            public PaymentInfo creditCardInfo { get; set; }

        // Cart Implementation
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        [XmlIgnore]
        private double total;

        [XmlElement(ElementName ="Total")]
        public double Total
        {
            get { return total; }
            set
            {
                total = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Total"));
            }
        }

        [XmlIgnore]
        private double numOfItemsInCart;

        [XmlElement(ElementName = "ItemsInCart")]
        public double NumOfItemsInCart
        {
            get { return numOfItemsInCart; }
            set
            {
                numOfItemsInCart = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NumOfItemsInCart"));
            }
        }


        [XmlIgnore]
        private ObservableCollection<Item> cart;
        [XmlIgnore]
        //[XmlElement(ElementName ="Cart")]
        public ObservableCollection<Item> Cart
        {
            get { return cart; }
            set
            {
                cart = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
            }
        }

        


    }
  }

