using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


using MVC_Controler;
using MVC_Model;
using MVC_View;

namespace AdressBookRun
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AdressView view = new AdressView();
            view.Visible = false;
            IList contacts=new ArrayList();
            contacts.Add(new Contact("a", "b", "1", "0"));
            ContactController controller = new ContactController(view,contacts);
            view.ShowDialog();

            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AdressView());*/
        }
    }
}
