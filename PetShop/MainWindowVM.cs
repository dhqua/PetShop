using System;
using System.Collections.Generic;
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

        //List of Users
        public  List<User> Users = new List<User>();

        //Main csv data file
        public readonly  string XmlPath = "users.xml";

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


    }
}
