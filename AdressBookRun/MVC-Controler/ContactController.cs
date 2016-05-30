using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MVC_Model;


namespace MVC_Controler
{
    public class ContactController
    {
        private readonly IAdressView _view;
        private List<Contact> _users;
        private Contact _selectedContact;


        public ContactController(IAdressView view, List<Contact> users)
        {
            _view = view;
            _users = users;
            view.SetController(this);
        }

        public List<Contact> Users
        {
            get { return _users; }
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
            foreach (var usr in _users)
                _view.AddContactToGrid(usr);

            //_view.SetSelectedContactInGrid((Contact)_users[0]);
        }

        public void SelectedContactChanged(string selectedContactId)
        {
            foreach (var usr in _users)
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

        public void AddNewContact(string fn, string ln, string var, string id)
        {
            _selectedContact = new Contact(fn /*firstname*/,
                ln /*lastname*/,
                var /*var*/,
                id /*id*/
                );

            updateViewDetailValues(_selectedContact);
        }

        public void RemoveContact()
        {
            var id = _view.GetIdOfSelectedContactInGrid();
            Contact userToRemove = null;

            if (id != "")
            {
                foreach (var usr in _users)
                {
                    if (usr.ID == id)
                    {
                        userToRemove = usr;
                        break;
                    }
                }

                if (userToRemove != null)
                {
                    var newSelectedIndex = _users.IndexOf(userToRemove);
                    _users.Remove(userToRemove);
                    _view.RemoveContactFromGrid(userToRemove);

                    if (newSelectedIndex > -1 && newSelectedIndex < _users.Count)
                    {
                        _view.SetSelectedContactInGrid(_users[newSelectedIndex]);
                    }
                }
            }
        }

        public void Save()
        {
            updateContactWithViewValues(_selectedContact);
            if (!_users.Contains(_selectedContact))
            {
                // Add new user
                _users.Add(_selectedContact);
                _view.AddContactToGrid(_selectedContact);
            }
            else
            {
                // Update existing
                _view.UpdateGridWithChangedContact(_selectedContact);
            }
            _view.SetSelectedContactInGrid(_selectedContact);
        }

        public void UpdateFile(List<Contact> list, string _fn)
        {
            var xd = new StorageService();
            xd.Save(_fn, list);
        }


        public void LoadFromFile(string _fn)
        {
            var xd = new StorageService();
            try
            {
                _users.Clear();
                _users = xd.Load(_fn);
            }
            catch (Exception)
            {
                MessageBox.Show("Musisz najpierw cos zapisac żeby odczytywać");
            }
            LoadView();
        }
    }
}
