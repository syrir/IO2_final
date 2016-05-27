using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace MVC_Model
{
   
    [Serializable()]
    public class Contact
    {
        public enum TypeOfContact
        {
            Phone = 1, Email = 2
        }
        [XmlElement("FirstName")]
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (value.Length > 50)
                    Console.WriteLine("Error! FirstName must be less than 51 characters!");
                else
                    _FirstName = value;

            }
        }
        [XmlElement("LastName")]
        private string _LastName;
        public string LastName
        {
           get { return _LastName; }
            set
            {
                if (value.Length > 50)
                    Console.WriteLine("Error! FirstName must be less than 51 characters!");
                else
                    _LastName = value;

            }
        }
        [XmlElement("Email")]
        private  string _Email;
        public  string Email
        {
            get { return _Email; }
            set
            {
                if (value.Length > 50)
                    Console.WriteLine("Error! FirstName must be less than 51 characters!");
                else
                    _Email= value;
            }
        }
        [XmlElement("Phone")]
        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (value.Length > 14)
                    Console.WriteLine("Error! FirstName must be less than 15 characters!");
                else
                    _Phone = value;
            }
        }
        [XmlElement("Type")]
        private TypeOfContact _Type;
        public TypeOfContact Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        [XmlElement("ID")]
        private string _ID;
        public string ID
        {
            get { return _ID; }
            set
            {
                if (value.Length > 9)
                    Console.WriteLine("Error! ID must be less than 10 characters!");
                else
                    _ID = value;
            }
        }
        public Contact(string fn, string ln, string var, string ID , TypeOfContact type)
        {
            _FirstName = fn;
            _LastName = ln;
            if (type.ToString() == "Phone")
                _Phone = var;
            else
                _Email = var;
            _ID=ID;
        }          
     
    }
}
