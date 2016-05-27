using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MVC_Model;
using MVC_Controler;


namespace MVC_View
{
    public partial class AdressView : Form,IAdressView
    {
        private string file="plik";
        private string file2 ="plik2";
        public AdressView()
        {
            InitializeComponent();
        }
        private void Clear_Fields()
        {
            this.First.Text = "";
            this.Last.Text = "";
            this.phone.Text = "";
        }
        ContactController _controller;
        #region Events raised back to controller

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int c = 0;
            if (this.First.Text.Length > 0) c++;
            if (this.Last.Text.Length > 0) c++;
            if (this.phone.Text.Length > 0) c++;
            if(c>0)
            {
                this._controller.AddNewContact(this.First.Text, this.Last.Text, this.phone.Text, this._controller.Users.Count.ToString());
                this._controller.Save();
                Clear_Fields();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this._controller.RemoveContact();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                _controller.LoadFromFile(_controller.Users, file);
            }
            else
            {
                _controller.LoadFromFile(_controller.Users, file2);
            }
            this._controller.LoadView();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
                this._controller.SelectedContactChanged(this.listView1.SelectedItems[0].Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.First.Text.Length==0 && this.Last.Text.Length == 0 && this.phone.Text.Length== 0)
            {
                this._controller.RemoveContact();
            }
            this._controller.Save();
            Clear_Fields();
        }
        #endregion


        #region IAdresView implementation


       public void SetController(ContactController controller)
        {
            _controller=controller;
        }
       public void ClearGrid()
        {
            this.listView1.Columns.Clear();

            this.listView1.Columns.Add("ID",15,HorizontalAlignment.Left);
            this.listView1.Columns.Add("First Name",94,HorizontalAlignment.Left);
            this.listView1.Columns.Add("Last Name",94,HorizontalAlignment.Left);
            this.listView1.Columns.Add("Phone/Email",94,HorizontalAlignment.Left);

            this.listView1.Items.Clear();
            _controller.UpdateFile(_controller.Users,file);

        }
       public void AddContactToGrid(Contact user)
        {
            ListViewItem parent;
            parent=this.listView1.Items.Add(user.ID);
            parent.SubItems.Add(user.FirstName);
            parent.SubItems.Add(user.LastName);
            parent.SubItems.Add(user.Phone);
            _controller.UpdateFile(_controller.Users,file);
        }
       public void UpdateGridWithChangedContact(Contact user)
        {
            ListViewItem rowToUpdate=null;
            foreach (ListViewItem row in this.listView1.Items)
	        {
		        if(row.Text==user.ID)
                {
                    rowToUpdate=row;
                }
        	}
            if(rowToUpdate!=null)
            {
                rowToUpdate.Text=user.ID;
                rowToUpdate.SubItems[1].Text=user.FirstName;
                rowToUpdate.SubItems[2].Text=user.LastName;
                rowToUpdate.SubItems[3].Text=user.Phone;


            }
            _controller.UpdateFile(_controller.Users,file);
        }
       public void RemoveContactFromGrid(Contact user)
        {
            ListViewItem rowToDelete=null;
            foreach (ListViewItem row in this.listView1.Items)
	        {
                if(row.Text==user.ID)
                {
                    rowToDelete=row;
                }
            }
            if(rowToDelete!=null)
            {
                this.listView1.Items.Remove(rowToDelete);
                this.listView1.Focus();
                _controller.UpdateFile(_controller.Users,file);
            }
        }

        public string GetIdOfSelectedContactInGrid()
        {
            if(this.listView1.SelectedItems.Count>0)
                return this.listView1.SelectedItems[0].Text;
            else
                return"";
        }
        public void SetSelectedContactInGrid(Contact user)
        {
            foreach (ListViewItem row in this.listView1.Items)
            {
                if (row.Text == user.ID)
                    row.Selected = true;
            }
        }
        public string FirstName
        {
            get { return this.First.Text; }
            set { this.First.Text = value; }
        }
        public string LastName
        {
            get { return this.Last.Text; }
            set { this.Last.Text = value; }
        }
        public string Phone 
        {
            get{return this.phone.Text;}
            set{this.phone.Text = value;}
        }

        #endregion

    }
}


