using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Item
    {
        public static int uniqueId = 1;
        public int ID { get;}
        public string Name { get; set; }
        public int ItemShelf { get; set; }
        public string Type { get; set; }
        public string Kosher { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double SpaceInCm { get; set; }

        public Item(string name, string type, string kosher, DateTime expiryDate, double spaceInCm)
        {
            ID = uniqueId++;
            Name = name;
            Type = type;
            Kosher = kosher;
            ExpiryDate = expiryDate;
            SpaceInCm = spaceInCm;
        }
        public override string ToString()
        {
            return $"Item ID: {ID}\nName: {Name}\nShelf: {ItemShelf}\nType: {Type}\nKosher: {Kosher}\nExpiry Date: {ExpiryDate.ToShortDateString()}\nSpaceCm: {SpaceInCm} ";
        }

       


    }
}
