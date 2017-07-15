using A.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace A
{
    public class FileAccessRepository
    {
        private static Dictionary<int, string> _types;
        private IEnumerable<Record> _records;
        private XMLHelper _xmlHelper;
         
        public FileAccessRepository(bool generateRandomData,string path)
        {

            _types = new Dictionary<int, string>();
            _types.Add(1, "red");
            _types.Add(2, "green");
            _types.Add(3, "blue");


            _xmlHelper = new XMLHelper(path);

            if ((generateRandomData) || (_records != null && _records.Count() < 1000))
                GenerateRamdomData();

        }
        public List<List<Record>> GenerateNSizeRamdomData(string type,int nClients)
        {
            var recordsWithType = _records.Where(x => x.Type.ToLower() == type.ToLower()).ToList();
            int chunk = recordsWithType.Count / nClients; 
            return recordsWithType.Chunks(chunk);
        }
        public void GenerateRamdomData()
        {
            for (int i = 0; i <= 10; i++)
            {
                var randomIndex = new Random().Next(1, 3);
                var record = new Record
                {
                    Type = _types[randomIndex],
                    Value = randomIndex
                };
                _xmlHelper.WriteRecord(record);
            }
            _records = GetAllRamdomData();
        }
        public IEnumerable<Record> GetAllRamdomData() {
           return _xmlHelper.ReadRecord(); 
        }       

    }

}
