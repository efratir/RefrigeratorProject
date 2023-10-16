using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Shelf
    {
        public int ID { get; set; }
        public int FloorNumber { get; set; }
        public double ShelfSpace { get; set; }
        public List<Item> Items { get; set; }

        public Shelf(int id, int floorNumber, double shelfSpace, List<Item> items)
        {
            ID = id;
            FloorNumber = floorNumber;
            ShelfSpace = shelfSpace;
            Items = items;
        }

    }
}
