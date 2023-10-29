using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Item
    {
        
        public Guid ID { get;}
        public string Name { get; set; }
        public int ShelfNumber { get; set; }
        public string Type { get; set; }
        public string Kosher { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double SpaceInCm { get; set; }

        public Item(string name, string type, string kosher, DateTime expiryDate, double spaceInCm)
        {
            ID = new Guid();
            Name = name;
            Type = type;
            Kosher = kosher;
            ExpiryDate = expiryDate;
            SpaceInCm = spaceInCm;
        }
        public override string ToString()
        {
            return $"Item ID: {ID}\n" +
                   $"Name: {Name}\n" +
                   $"Shelf: {ShelfNumber}\n" +
                   $"Type: {Type}\n" +
                   $"Kosher: {Kosher}\n" +
                   $"Expiry Date: {ExpiryDate.ToShortDateString()}\n" +
                   $"SpaceCm: {SpaceInCm} ";
        }
    }
}
