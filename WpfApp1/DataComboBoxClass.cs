using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DataComboBoxClass
    {
        private static List<DataComboBoxClass> dataComboBoxList = new List<DataComboBoxClass>();
        private static string directory = Directory.GetCurrentDirectory() + "/" + "dataComboBoxList.json";

        public string Name { get; set; }
        public DataComboBoxClass(string name)
        {
            Name = name;
        }

        public static DataComboBoxClass AddDataComboBoxList(string text)
        {
            var data = new DataComboBoxClass(text);
            if (text.Length != 0) dataComboBoxList.Add(new DataComboBoxClass(text));
            return data;
        }

        public static List<DataComboBoxClass> LoadDataComboBoxList()
        {
            var data = File.Exists(directory) ? JsonConvert.DeserializeObject<List<DataComboBoxClass>>(File.ReadAllText(directory)) : null;
            if (data != null) dataComboBoxList = data;
            return dataComboBoxList;
        }

        public static void SaveDataComboBoxList()
        {
            if (dataComboBoxList.Count != 0)
            {
                string data = JsonConvert.SerializeObject(dataComboBoxList);
                using (StreamWriter sw = new StreamWriter(directory))
                {
                    sw.Write(data);
                }
            }
        }
    }
}
