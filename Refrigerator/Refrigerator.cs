using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Refrigerator
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberOfShelves { get; set; }
        public List<Shelf> Shelves { get; set; }

        public Refrigerator(int id, string model, string color, int numberOfShelves, List<Shelf> shelves)
        {
            ID = id;
            Model = model;
            Color = color;
            NumberOfShelves = numberOfShelves;
            Shelves = shelves;
        }

    }
}
