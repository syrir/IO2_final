using System;
using System.Windows.Forms;
using MVC_Controler;
using MVC_Model;

namespace MVC_View
{
    public partial class AdressView : Form, IAdressView
    {
        private readonly string file = "plik.json";
        private readonly string file2 = "plik2.json";

        private ContactController _controller;


        public bool validateFields()
        {
            Contact xd = new Contact();
            if (radioButton1.Checked)
            {
                if (xd.validatePhone(phone.Text) && xd.validateString(Last.Text) && xd.validateString(First.Text)) return true;
            }
            else
            {
                if (xd.validateEmail(phone.Text) && xd.validateString(Last.Text) && xd.validateString(First.Text)) return true;
            }

            return false;
        }
        public AdressView()
        {
            InitializeComponent();
        }

        private void Clear_Fields()
        {
            First.Text = "";
            Last.Text = "";
            phone.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

        #region Events raised back to controller

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
                var n = _controller.Users.Count;
                var v = n.ToString();
                if (validateFields())
                {
                    _controller.AddNewContact(First.Text, Last.Text, phone.Text, v);
                    _controller.Save();
                    _controller.Users.Sort();
                    if (radioButton1.Checked)
                    {
                        _controller.UpdateFile(_controller.Users, file);
                    }
                    else
                    {
                        _controller.UpdateFile(_controller.Users, file2);
                    }
                    Clear_Fields();
                    _controller.LoadView();
                    MessageBox.Show("Pomyslnie dodano rekord");
                }
                else
                {
                    MessageBox.Show("Niepoprawne lub niepełne dane");
                
                }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _controller.RemoveContact();
            if (radioButton1.Checked)
            {
                _controller.UpdateFile(_controller.Users, file);
            }
            else
            {
                _controller.UpdateFile(_controller.Users, file2);
            }
            Clear_Fields();
            MessageBox.Show("Pomyslnie usunieto rekordy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                _controller.LoadFromFile(file);
            }
            if(radioButton2.Checked)
            {
                _controller.LoadFromFile(file2);
            }

            _controller.LoadView();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                _controller.SelectedContactChanged(listView1.SelectedItems[0].Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (First.Text.Length == 0 && Last.Text.Length == 0 && phone.Text.Length == 0)
            {
                return ;
            }
            _controller.Save();
            Clear_Fields();
            if (radioButton1.Checked)
            {
                _controller.UpdateFile(_controller.Users, file);
            }
            else
            {
                _controller.UpdateFile(_controller.Users, file2);
            }
        }

        #endregion

        #region IAdresView implementation

        public void SetController(ContactController controller)
        {
            _controller = controller;
        }

        public void ClearGrid()
        {
            listView1.Columns.Clear();

            listView1.Columns.Add("ID", 15, HorizontalAlignment.Left);
            listView1.Columns.Add("Imie", 94, HorizontalAlignment.Left);
            listView1.Columns.Add("Nazwisko", 94, HorizontalAlignment.Left);
            listView1.Columns.Add("telefon/Email", 94, HorizontalAlignment.Left);

            listView1.Items.Clear();
        }

        public void AddContactToGrid(Contact user)
        {
            ListViewItem parent;
            parent = listView1.Items.Add(user.ID);
            parent.SubItems.Add(user.FirstName);
            parent.SubItems.Add(user.LastName);
            parent.SubItems.Add(user.Phone);
        }

        public void UpdateGridWithChangedContact(Contact user)
        {
            ListViewItem rowToUpdate = null;
            foreach (ListViewItem row in listView1.Items)
            {
                if (row.Text == user.ID)
                {
                    rowToUpdate = row;
                }
            }
            if (rowToUpdate != null)
            {
                rowToUpdate.Text = user.ID;
                rowToUpdate.SubItems[1].Text = user.FirstName;
                rowToUpdate.SubItems[2].Text = user.LastName;
                rowToUpdate.SubItems[3].Text = user.Phone;
            }
        }

        public void RemoveContactFromGrid(Contact user)
        {
            ListViewItem rowToDelete = null;
            foreach (ListViewItem row in listView1.Items)
            {
                if (row.Text == user.ID)
                {
                    rowToDelete = row;
                }
            }
            if (rowToDelete != null)
            {
                listView1.Items.Remove(rowToDelete);
                listView1.Focus();
            }
        }

        public string GetIdOfSelectedContactInGrid()
        {
            if (listView1.SelectedItems.Count > 0)
                return listView1.SelectedItems[0].Text;
            return "";
        }

        public void SetSelectedContactInGrid(Contact user)
        {
            foreach (ListViewItem row in listView1.Items)
            {
                if (row.Text == user.ID)
                    row.Selected = true;
            }
        }

        public string FirstName
        {
            get { return First.Text; }
            set { First.Text = value; }
        }

        public string LastName
        {
            get { return Last.Text; }
            set { Last.Text = value; }
        }

        public string Phone
        {
            get { return phone.Text; }
            set { phone.Text = value; }
        }

        #endregion

        private void AdressView_Load(object sender, EventArgs e)
        {
            _controller.LoadView();
        }
    }
}