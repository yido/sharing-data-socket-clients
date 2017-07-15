using A.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Helper
{ 
    public static class DataSerializer
    {
        public static byte[] Serialize(this IEnumerable<Record> items)
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m, System.Text.Encoding.UTF8, true))
                {
                    foreach (var item in items)
                    {
                        var itemBytes = item.Serialize();
                        writer.Write(itemBytes.Length);
                        writer.Write(itemBytes);
                    }

                }

                return m.ToArray();
            }
        }

        public static List<Record> Deserialize(byte[] data)
        {
            var ret = new List<Record>();
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m, System.Text.Encoding.UTF8))
                {
                    while (m.Position < m.Length)
                    {
                        var itemLength = reader.ReadInt32();
                        var itemBytes = reader.ReadBytes(itemLength);
                        var item = Record.Desserialize(itemBytes);
                        ret.Add(item);
                    }
                }
            }

            return ret;
        }
    }
}
