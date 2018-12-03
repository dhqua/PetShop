using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace PetShop
{
    // Container for the entire application
    // Everything else is this application are user controls which plug into this file
    public class MainWindowVM : INotifyPropertyChanged
    {

        // Needed for Bindings to function
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //XML Serializers
        public  XmlSerializer Xmler = new XmlSerializer(typeof(List<User>));
        public  XmlSerializer productXmler = new XmlSerializer(typeof(ObservableCollection<Item>));

        //List of Users and Items
        public  List<User> Users = new List<User>();

        private ObservableCollection<Item> items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }
        

        //Main xml file names
        public readonly  string XmlPath = "users.xml";
        public readonly  string itemsXmlPath = "items.xml";


        // View and User Controllers
        public User CurrentUser;


        //Determines what user control is currently displaying
        private object _activeView;
        public object ActiveView
        {
            get { return _activeView; }
            set
            {
                _activeView = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ActiveView") );

            }
        }


        // Default Constructor
        public MainWindowVM()
        {
            // Unit test for XML implementation
            //Items.Add(new LandAnimal("Test description", "2.50", 8, "Test Animal", @"\img\lion.jpg",2.3,Item.LandPet));
            //WriteItemXmlFile(Items);

            // Read users and products from XML files 
            readItems();
            readUsers();
        }



        // Reads Users from XML File and Opens the Login View
        public void readUsers()
        {
            if (File.Exists(XmlPath))
            {
                try
                {
                    using (FileStream ReadStream = new FileStream(XmlPath, FileMode.Open, FileAccess.Read))
                    {
                        Users = Xmler.Deserialize(ReadStream)
                        as List<User>;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to read XML file", ex.InnerException);
                    MessageBox.Show($"Unable to read XML file\nInnerException:{ ex.InnerException.Message}");
                }
            }

            ActiveView = new LoginVM(this);

        }


        // Reads products from Items xml file
        public void readItems()
        {
            if (File.Exists(itemsXmlPath))
            {
                try
                {
                    using (FileStream ReadStream = new FileStream(itemsXmlPath, FileMode.Open, FileAccess.Read))
                    {
                        Items = productXmler.Deserialize(ReadStream)
                        as ObservableCollection<Item>;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to read product file", ex.InnerException);
                    MessageBox.Show($"Unable to read XML file\nInnerException:{ ex.InnerException.Message}");
                }
            }

        }



        //Write users to Xml file
        public void WriteXmlFile(List<User> users)
        {
            //Ensure there isn't an empty Xml file
            if (users.Count == 0)
            {
                if (File.Exists(XmlPath))
                {
                    File.Delete(XmlPath);
                }
            }
            else
            {
                using (FileStream fs = new FileStream(XmlPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    Xmler.Serialize(fs, users);
                }
            }
        }


        // Updates products in XML file
        public void WriteItemXmlFile(ObservableCollection<Item> items)
        {
            //Ensure there isn't an empty Xml file
            if (items.Count == 0)
            {
                if (File.Exists(itemsXmlPath))
                {
                    File.Delete(itemsXmlPath);
                }
            }
            else
            {
                using (FileStream fs = new FileStream(itemsXmlPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    productXmler.Serialize(fs, items);
                }
            }
        }

    }
}
