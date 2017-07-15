using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace B.Dto
{
    [Serializable]
    [XmlRoot(ElementName = "data")]
    public class Data 
    {
        [XmlElement("record", typeof(Record))]
        public List<Record> Records { get; set; }
    }
    [Serializable]
    [XmlRoot("record")]
    public  class Record
    {
        public Record() { }
        public Record(string type,int value) {
            Type = type;
            Value = value;
        }
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("value")]
        public int Value { get; set; }



        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    writer.Write(Type);
                    writer.Write(Value); 
                }
                return m.ToArray();
            }
        }

        public static Record Desserialize(byte[] data)
        {
            string Type;
            int Value; 

            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    Type = reader.ReadString();
                    Value = reader.ReadInt32(); 
                }
            }

            return new Record(Type, Value);
        }
    }
}
