using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DataGridClass
    {
        private int _money;

        public string Name { get; set; }
        public string TypeName { get; set; }
        
        public int Money
        {
            get => _money;
            set
            {
                IsIncome = value >= 1;
                _money = Math.Abs(value);
            }
        }
        public bool IsIncome { get; set; }
        public DataGridClass(string name, string typeName, int money)
        {
            Name = name;
            TypeName = typeName;
            Money = money;
        }
    }
}
