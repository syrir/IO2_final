using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace MVC_Model
{
    public class StorageService
    {
        public void Save(string filename, List<Contact> contacts)
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
