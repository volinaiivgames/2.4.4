using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp1
{
    class DateTimeClass
    {
        private static List<DateTimeClass> dateTimeList = new List<DateTimeClass>();
        private static string directory = Directory.GetCurrentDirectory() + "/" + "dateTimeList.json";

        public DateTime Date { get; set; }
        public List<DataGridClass> DataGridList { get; set; } = new List<DataGridClass>();
        public DateTimeClass(DateTime date, List<DataGridClass> dataGridList)
        {
            Date = date;
            DataGridList = dataGridList;
        }

        public static int GetLabelText()
        {
            int value = 0;
            foreach (var item in dateTimeList)
            {
                foreach (var itemGrid in item.DataGridList)
                {
                    if (itemGrid.IsIncome) value += itemGrid.Money;
                    else value -= itemGrid.Money;
                }
            }
            return value;
        }

        public static DataGridClass AddDataGrid(DateTime date, DataGridClass data)
        {
            if (dateTimeList.Count == 0) dateTimeList.Add(new DateTimeClass(date, new List<DataGridClass>() { data }));
            else foreach (var item in dateTimeList) if (item.Date.Equals(date)) item.DataGridList.Add(data);
            return data;
        }

        public static List<DateTimeClass> GetDateTimeList() => dateTimeList;

        public static DataGridClass GetDateTimeIndex(int index, DateTime date)
        {
            foreach (DateTimeClass item in dateTimeList)
            {
                if (item.Date.Equals(date))
                {
                    for (int i = 0; i < item.DataGridList.Count; i++)
                    {
                        if (i == index) return item.DataGridList[i];
                    }
                }
            }
            return null;
        }

        public static bool EditDateTimeIndex(int index, DateTime date, DataGridClass data)
        {
            foreach (DateTimeClass item in dateTimeList)
            {
                if (item.Date.Equals(date))
                {
                    for (int i = 0; i < item.DataGridList.Count; i++)
                    {
                        if (i == index)
                        {
                            item.DataGridList[i] = data;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static List<DateTimeClass> LoadDateTimeList()
        {
            List<DateTimeClass> data = File.Exists(directory) ? JsonConvert.DeserializeObject<List<DateTimeClass>>(File.ReadAllText(directory)) : null;
            if (data != null) dateTimeList = data;
            return dateTimeList;
        }

        public static void DeleteDateTimeListIndex(int index, DateTime date)
        {
            foreach (DateTimeClass item in dateTimeList)
            {
                if (item.Date.Equals(date))
                {
                    for (int i = 0; i < item.DataGridList.Count; i++)
                    {
                        if (i == index) item.DataGridList.RemoveAt(i);
                    }
                }
            }
        }

        public static void SaveDateTimeList()
        {
            if (dateTimeList.Count != 0)
            {
                string data = JsonConvert.SerializeObject(dateTimeList);
                using (StreamWriter sw = new StreamWriter(directory))
                {
                    sw.Write(data);
                }
            }
        }
    }
}
