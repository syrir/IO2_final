using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Windows.Forms;
using System.IO;

namespace MVC_Model
{
    public class StorageService
    {
        public void Save(string filename, List<Contact> contacts)
        {
            var content = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            MessageBox.Show(content);
            File.WriteAllText(filename, content, Encoding.UTF8);
        }

        public List<Contact> Load(string filename)
        {
            var content = File.ReadAllText(filename, Encoding.UTF8);
            MessageBox.Show(content);
            var test=JsonConvert.DeserializeObject<List<Contact>>(content);

            return test;
        }
    }
}
