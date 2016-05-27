using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC_Model;

namespace MVC_Controler
{
    public interface IAdressView
    {
        string FirstName     { get; set; }
        string LastName      { get; set; }
        string Phone         { get; set; }

        
        void SetController(ContactController controller);
        void ClearGrid();
        void AddContactToGrid(Contact user);
        void UpdateGridWithChangedContact(Contact user);
        void RemoveContactFromGrid(Contact user);
        string GetIdOfSelectedContactInGrid();
        void SetSelectedContactInGrid(Contact user);
        
    }
}
