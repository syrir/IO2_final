using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace MVC_Model
{
    [Serializable]
    public class Contact
    {
        /*public enum TypeOfContact
        {
            Phone = 1, Email = 2
        }*/
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

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (value.Length > 50)
                    MessageBox.Show("Error! FirstName must be less than 51 characters!");
                else
                    _LastName = value;
            }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (value.Length > 24)
                    MessageBox.Show("Error! Phone/Email must be less than 25 characters!");
                else
                    _Phone = value;
            }
        }

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

        public Contact(string fn, string ln, string var, string ID)
        {
            _FirstName = fn;
            _LastName = ln;
            _Phone = var;
            _ID = ID;
        }

        public Contact()
        {
        }
        public bool validatePhone(string xd)
        {
            int value;
            return int.TryParse(xd,out value);
        }
        public bool validateEmail(string xd)
        {
            try
            {
                MailAddress m = new MailAddress(xd);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool validateString(string xd)
        {
            return xd.Length>0;
        }
    }
}
