using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace MVC_Model
{
    class StorageService
    {
        public void Save(string filename, IEnumerable<Contact> contacts)
        {
            var content = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(filename, content, Encoding.UTF8);
        }

        public List<Contact> Load(string filename)
        {
            var content = File.ReadAllText(filename, Encoding.UTF8);
            return JsonConvert.DeserializeObject<IEnumerable<Contact>>(content).ToList();
        }
    }
}
