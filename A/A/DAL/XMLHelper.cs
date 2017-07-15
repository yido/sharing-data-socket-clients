using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace A.DAL
{
    public class XMLHelper
    {
        public XMLHelper(){
            Path = "Input.xml";
        }
        public XMLHelper(string path) {
            Path = path + ".xml";
        }

        public string Path { get; set; }
        public void WriteRecord(Record record)
        {
            if (File.Exists(Path))
            {
                XDocument xDocument = XDocument.Load(Path);
                XElement root = xDocument.Element("data");
                XElement row = new XElement("record");
                row.Add(new XAttribute("value", record.Value));
                row.Add(new XAttribute("type", record.Type));
                root.Add(row);
                xDocument.Save(Path);
            }
            else
            {

                XmlWriter xmlWriter = XmlWriter.Create(Path);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("data");
                xmlWriter.WriteStartElement("record");
                xmlWriter.WriteAttributeString("value", record.Value.ToString());
                xmlWriter.WriteAttributeString("type", record.Type);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
        }

        public List<Record> ReadRecord()
        {
            Data data;
            using (StreamReader reader = new StreamReader(Path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                data = (Data)serializer.Deserialize(reader);
            }
            return data.Records;
        }
    }
}
