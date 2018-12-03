using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    // Class representing user credit card information
    [XmlRoot(ElementName = "PaymentInfo")]
    public class PaymentInfo
    {
        [XmlElement(DataType = "string", ElementName = "CardNumber")]
        public string CardNumber { get; set; }

        [XmlElement(DataType = "int", ElementName = "Month")]
        public int Month { get; set; }

        [XmlElement(DataType = "int", ElementName = "Year")]
        public int Year { get; set; }

        [XmlElement(DataType = "string", ElementName = "CVC")]
        public string CVC { get; set; }


        public PaymentInfo()
        {
            CardNumber = "";
            Month = 0;
            Year = 0;
            CVC = "";
        }

        public PaymentInfo(
            string cardNum,
            int month,
            int year,
            string cvc)
        {
            CardNumber = cardNum;
            Month = month;
            Year = year;
            CVC = cvc;
        }
    }
}
