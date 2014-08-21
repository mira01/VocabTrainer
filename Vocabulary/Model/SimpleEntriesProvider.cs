using System.Collections.Generic;
using System.Xml.Serialization;

namespace Vocabulary.Model
{
    class SimpleEntriesProvider : IEntriesProvider
    {
        public IEnumerable<dictionaryEntry> GetEntries()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(dictionary));
            //Stream reader = new FileStream("C:\\Users\\Mira\\Desktop\\dict2.xml", FileMode.Open);
            //var dict = (dictionary)serializer.Deserialize(reader);
            return null;
        }
    }
}
