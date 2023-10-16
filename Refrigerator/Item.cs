using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ItemShelf { get; set; }
        public string Type { get; set; }
        public string Kosher { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double SpaceInCm { get; set; }

        public Item(int id, string name, int itemShelf, string type, string kosher, DateTime expiryDate, double spaceInCm)
        {
            ID = id;
            Name = name;
            ItemShelf = itemShelf;
            Type = type;
            Kosher = kosher;
            ExpiryDate = expiryDate;
            SpaceInCm = spaceInCm;
        }
    }
}
