using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Xml.Serialization;
using System.IO;

using MVC_Model;


namespace MVC_Controler
{
   public class ContactController
    {
        IAdressView _view;
        public IList _users;
        Contact _selectedContact;
        string _file_name="bazadanych";

        public ContactController(IAdressView view, IList users)
        {
            _view = view;
            _users = users;
            view.SetController(this);
        }
        public IList Users
        {
            get { return ArrayList.Adapter(_users); }
        }

        private void updateViewDetailValues(Contact usr)
        {
            _view.FirstName = usr.FirstName;
            _view.LastName = usr.LastName;
            _view.Phone = usr.Phone;
        }
        private void updateContactWithViewValues(Contact usr)
        {
            usr.FirstName = _view.FirstName;
            usr.LastName = _view.LastName;
            usr.Phone = _view.Phone;
        }
        public void LoadView()
        {
            _view.ClearGrid();
            foreach (Contact usr in _users)
                _view.AddContactToGrid(usr);

            //_view.SetSelectedContactInGrid((Contact)_users[0]);

        }
        public void SelectedContactChanged(string selectedContactId)
        {
            foreach (Contact usr in this._users)
            {
                if (usr.ID == selectedContactId)
                {
                    _selectedContact = usr;
                    updateViewDetailValues(usr);
                    _view.SetSelectedContactInGrid(usr);
                    break;
                }
            }
        }
        public void AddNewContact(string fn,string ln,string var,string id)
        {
            _selectedContact = new Contact(fn/*firstname*/,
                                    ln /*lastname*/,
                                      var/*var*/,
                                     id/*id*/
                                     );

            this.updateViewDetailValues(_selectedContact);

        }
        public void RemoveContact()
        {
            string id = this._view.GetIdOfSelectedContactInGrid();
            Contact userToRemove = null;

            if (id != "")
            {
                foreach (Contact usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToRemove = usr;
                        break;
                    }
                }

                if (userToRemove != null)
                {
                    int newSelectedIndex = this._users.IndexOf(userToRemove);
                    this._users.Remove(userToRemove);
                    this._view.RemoveContactFromGrid(userToRemove);

                    if (newSelectedIndex > -1 && newSelectedIndex < _users.Count)
                    {
                        this._view.SetSelectedContactInGrid((Contact)_users[newSelectedIndex]);
                    }
                }
            }
        }
        public void Save()
        {
            updateContactWithViewValues(_selectedContact);
            if (!this._users.Contains(_selectedContact))
            {
                // Add new user
                this._users.Add(_selectedContact);
                this._view.AddContactToGrid(_selectedContact);
                UpdateFile(_users, _file_name);
            }
            else
            {
                // Update existing
                this._view.UpdateGridWithChangedContact(_selectedContact);
            }
            _view.SetSelectedContactInGrid(_selectedContact);

        }
        public void UpdateFile( IList  list,string _fn)
        {
            
        }           

        
        public void LoadFromFile(IList list, string _fn)
        {
    
        }


    }
}
