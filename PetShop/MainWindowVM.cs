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
    public class MainWindowVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //XML Serializer
        public  XmlSerializer Xmler = new XmlSerializer(typeof(List<User>));
        public  XmlSerializer productXmler = new XmlSerializer(typeof(ObservableCollection<Item>));

        //List of Users
        public  List<User> Users = new List<User>();
        public ObservableCollection<Item> Items = new ObservableCollection<Item>();
        

        //Main csv data file
        public readonly  string XmlPath = "users.xml";
        public readonly  string itemsXmlPath = "items.xml";

        public User CurrentUser;


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

        public MainWindowVM()
        {
            //Items.Add(new Item("Test description", "2.50", 8, "Test Item", @"\img\lion.jpg"));
            //WriteItemXmlFile(Items);
            // Test item list
            readItems();
            readUsers();
        }


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



        //Write to Xml file
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
