using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Refrigerator
    {
        public static int uniqueId = 1;
        public int ID { get; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberOfShelves { get; set; }
        public List<Shelf> Shelves { get; set; }

        public Refrigerator(string model, string color, int numberOfShelves)
        {
            ID = uniqueId++;
            Model = model;
            Color = color;
            NumberOfShelves = numberOfShelves;
            Shelves = new List<Shelf>();
        }

        public override string ToString()
        {
            string shelfList = string.Join(", ", Shelves);
            return $"Refrigerator ID: {ID}\nModel: {Model}\nColor: {Color}\nNumber of Shelves: {NumberOfShelves}\nShelves: {shelfList}";
        }

        public double GetRemainingSpaceInRefrigerator()
        {
            double totalSpaceInRefrigerator = 0;
            foreach (Shelf shelf in Shelves)
            {
                totalSpaceInRefrigerator += shelf.GetRemainingSpaceShelf();
            }
            return totalSpaceInRefrigerator;
        }

        public void PutItemInFridge(Item item)
        {
            foreach (Shelf shelf in Shelves)
            {
                if (shelf.GetRemainingSpaceShelf() >= item.SpaceInCm)
                {
                    shelf.Items.Add(item);
                    item.ItemShelf = shelf.ID; 
                    shelf.ShelfSpace -= item.SpaceInCm;
                    Console.WriteLine("A new item has been placed in the refrigerator");
                }
                else
                    Console.WriteLine("No space found in the fridge");
            }
        }

        public Item RemoveItemFromFridge(int idItem)
        {
            Item item = null;
            foreach (Shelf shelf in Shelves)
            {
               item = shelf.RemoveItemFromShelf(idItem);
            }
            return item;
        }

        public void ThrowingExpiredItemsFromFridge()
        {
            foreach(Shelf shelf in Shelves)
            {
                shelf.ThrowingExpiredItemsFromShelf();
            }
        }

        public List<List<Item>> WantEat(string type, string kosher)
        {
            List<List<Item>> itemsInFridge = new List<List<Item>>();
            
            foreach(Shelf shelf in Shelves)
            {
                itemsInFridge.Add(shelf.WantEat(type, kosher));
            }
            return itemsInFridge;
        }

        //public double SumOfFreeSpace()
        //{
        //    double sum = 0;
        //    foreach(Shelf shelf in Shelves)
        //    {
        //        sum += shelf.ShelfSpace;
        //    }
        //    return sum;
        //}

        public List<Refrigerator> SortRefrigeratorsByFreeSpace(List<Refrigerator> refrigerators)
        {
            refrigerators.Sort((ref1, ref2) => ref2.GetRemainingSpaceInRefrigerator().CompareTo(ref1.GetRemainingSpaceInRefrigerator()));
            return refrigerators;
        }

        public List<Item> SortItemsInFridge()
        {
            List<Item> itemsInFridge = new List<Item>();
            List<Item> itemsInShelf = new List<Item>();
            foreach(Shelf shelf in Shelves)
            {
                itemsInShelf = shelf.SortItemsByDate(shelf.Items);
                itemsInFridge.AddRange(itemsInShelf);
            }
            itemsInFridge.Sort((item1, item2) => item1.ExpiryDate.CompareTo(item2.ExpiryDate));
            return itemsInFridge;
        }

        public void ThrowingProductsForShopping()
        {
            DateTime expiresInThreeDays = DateTime.Now.Date.AddDays(3);
            DateTime expiresInWeek = DateTime.Now.Date.AddDays(7);
            DateTime expiresInOneDay = DateTime.Now.Date.AddDays(1);
            string dairy = "Dairy";
            string meat = "Meat";
            string fur = "Fur";

            foreach (Shelf shelf in Shelves)
            {
                shelf.ThrowingProducts(dairy, expiresInThreeDays);
            }
                Console.WriteLine("The dairy products that expire in three days were thrown in the trash to make space for shopping");
            if (GetRemainingSpaceInRefrigerator() < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    shelf.ThrowingProducts(meat, expiresInWeek);
                }
                Console.WriteLine("Now the meat products that expire in week were thrown in the trash to make space for shopping");

            }
            if (GetRemainingSpaceInRefrigerator() < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    shelf.ThrowingProducts(fur, expiresInOneDay);
                }
                Console.WriteLine("And now the fur products that expire in one dey were thrown in the trash to make space for shopping");

            }

            Console.WriteLine("Now you have enough free space to shop");

        }

        public double CheckingFreeSpaceForShopping()
        {
            DateTime expiresInThreeDays = DateTime.Now.Date.AddDays(3);
            DateTime expiresInWeek = DateTime.Now.Date.AddDays(7);
            DateTime expiresInOneDay = DateTime.Now.Date.AddDays(1);
            string dairy = "Dairy";
            string meat = "Meat";
            string fur = "Fur";
            double sumOfSpace = GetRemainingSpaceInRefrigerator();

            //בדיקה האם כאשר יזרקו את המוצרים באמת המקום יתפנה      
            if (sumOfSpace < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    sumOfSpace += shelf.CheckingFreeSpaceProducts(dairy, expiresInThreeDays);
                }
            }
            else
            {
                Console.WriteLine("After you throw away the expired products you will have enough space to shop");
            }
            if (sumOfSpace < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    sumOfSpace += shelf.CheckingFreeSpaceProducts(meat, expiresInWeek);
                }
            }
            else
            {
                Console.WriteLine("After you throw away the dairy products that expire in three days, you'll have enough plenty of space to shop");
            }
            if (sumOfSpace < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    sumOfSpace += shelf.CheckingFreeSpaceProducts(fur, expiresInOneDay);
                }
            }
            else
            {
                Console.WriteLine("After you throw away the dairy products that expire in three days and the meat products that expire in week, you'll have enough plenty of space to shop");
            }

            if (sumOfSpace < 20)
            {
                Console.WriteLine("You don't have enough space in the fridge, so this is not the time to shop");
            }

            return sumOfSpace;         

        }

        public void PreparingFridgeForShopping()
        {
            if(GetRemainingSpaceInRefrigerator() < 20)
            {
                ThrowingExpiredItemsFromFridge();
                if (GetRemainingSpaceInRefrigerator() < 20)
                {
                    if (CheckingFreeSpaceForShopping() >= 20)
                    {
                        ThrowingProductsForShopping();
                    }
                }
            }
            else
                Console.WriteLine("You have enough space in the fridge to shop and you don't have to throw anything away");
        }



    }
    }

