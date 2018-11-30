using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{

    [XmlRoot(ElementName = "User")]
    //CONSTRUCTOR
    public class User
    {
            
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

        public User()
        {

        }

        public User(string uName, string pWord, string firstName, string lastName, string email,bool seller, PaymentInfo cardInfo)
        {
            UserName = uName;
            Password = pWord;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            isSeller = seller;
            creditCardInfo = cardInfo;
        }


    }
  }

