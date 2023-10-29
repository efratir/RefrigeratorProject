using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Refrigerator
    {
        public Guid ID { get; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumberOfShelves { get; set; }
        public List<Shelf> Shelves { get; set; }

        public Refrigerator(string model, string color, int numberOfShelves)
        {
            ID = new Guid();
            Model = model;
            Color = color;
            NumberOfShelves = numberOfShelves;
            Shelves = new List<Shelf>();
        }

        public override string ToString()
        {
            string shelfList = string.Join(", ", Shelves);
            return $"Refrigerator ID: {ID}\n" +
                   $"Model: {Model}\n" +
                   $"Color: {Color}\n" +
                   $"Number of Shelves: {NumberOfShelves}\n" +
                   $"Shelves: {shelfList}";
        }

        public string GetAllShelves()
        {
            Console.WriteLine(ToString());

            if (Shelves.Count() == 0)
            {
                Console.WriteLine("The refrigerator is empty");
                return "";
            }

            string shelves = string.Join("", Shelves.Select(shelf => shelf.ToString()));

            Console.WriteLine(shelves);
            
            return shelves;
        }

        public void AddShelf(Shelf shelf)
        {
            Shelves.Add(shelf);
        }

        public double GetFreeSpace()
        {
            double totalSpaceInRefrigerator = Shelves.Sum(shelf => shelf.GetFreeSpace());
            return totalSpaceInRefrigerator;
        }
        public void PutItemInFridge(Item item)
        {
            int index = 0;
            var shelfWithEnoughSpace = Shelves.FirstOrDefault(shelf => GetFreeSpace() >= item.SpaceInCm);

            if (shelfWithEnoughSpace != null)
            {
                index = Shelves.IndexOf(shelfWithEnoughSpace) + 1;
                shelfWithEnoughSpace.FreeSpaceOfShelf -= item.SpaceInCm;
                shelfWithEnoughSpace.Items.Add(item);
                item.ShelfNumber = index;
                Console.WriteLine("A new item has been placed in the refrigerator");
            }
            else
            {
                Console.WriteLine("No space found in the fridge");
            }
        }

        public Item RemoveItemFromFridge(Guid idItem)
        {
            Item item = null;
            foreach (Shelf shelf in Shelves)
            {
                if (shelf.Items.Contains(item))
                {
                    item = shelf.RemoveItem(idItem);
                    break;
                }
            }
            return item;
        }
        public void ThrowingExpiredItemsFromFridge()
        {
            Shelves.ForEach(shelf => shelf.ThrowingExpiredItems());
        }

        public List<Item> WhaTotEat(string type, string kosher)
        {
            return Shelves.SelectMany(shelf => shelf.WhatToEat(type, kosher)).ToList();
        }

        public List<Refrigerator> SortRefrigeratorsByFreeSpace(List<Refrigerator> refrigerators)
        {
            refrigerators.Sort((ref1, ref2) => ref2.GetRemainingSpaceInRefrigerator().CompareTo(ref1.GetRemainingSpaceInRefrigerator()));
            return refrigerators;
        }

        public List<Shelf> SortedShelves(List<Shelf> shelves)
        {
            List<Shelf> sortShelves = new List<Shelf>();
            sortShelves = shelves;
            sortShelves.Sort((shelf1, shelf2) => shelf2.FreeSpaceOfShelf.CompareTo(shelf1.FreeSpaceOfShelf));
            sortShelves.ForEach(shelf => Console.WriteLine(shelf));

            return sortShelves;
        }
        public List<Item> SortItemsInFridge()
        {
            List<Item> itemsInFridge = Shelves.SelectMany(shelf => shelf.Items).ToList();
            itemsInFridge = itemsInFridge.OrderBy(item => item.ExpiryDate).ToList();
            itemsInFridge.ForEach(item => Console.WriteLine(item));
        
            return itemsInFridge;
        }

        //Checking whether they will remove products whose expiration date is close, there will be enough space to shop
        public double CheckingFreeSpaceForShopping()
        {
            double sumOfSpace = GetFreeSpace();
   
            if (sumOfSpace < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    sumOfSpace += shelf.CheckingFreeSpaceProducts("Dairy", DateTime.Now.Date.AddDays(3));
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
                    sumOfSpace += shelf.CheckingFreeSpaceProducts("Meat", DateTime.Now.Date.AddDays(7));
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
                    sumOfSpace += shelf.CheckingFreeSpaceProducts("Fur", DateTime.Now.Date.AddDays(1));
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

        //Removing products that expire soon
        public void ThrowingProductsForShopping()
        {
            foreach (Shelf shelf in Shelves)
            {
                shelf.ThrowingProductsExpireSoon("Dairy", DateTime.Now.Date.AddDays(3));
            }
            Console.WriteLine("The dairy products that expire in three days were thrown in the trash to make space for shopping");
            if (GetFreeSpace() < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    shelf.ThrowingProductsExpireSoon("Meat", DateTime.Now.Date.AddDays(7));
                }
                Console.WriteLine("Now the meat products that expire in week were thrown in the trash to make space for shopping");

            }
            if (GetFreeSpace() < 20)
            {
                foreach (Shelf shelf in Shelves)
                {
                    shelf.ThrowingProductsExpireSoon("Fur", DateTime.Now.Date.AddDays(1));
                }
                Console.WriteLine("And now the fur products that expire in one dey were thrown in the trash to make space for shopping");

            }

            Console.WriteLine("Now you have enough free space to shop");

        }
        //Preparing the fridge for shopping
        public void PreparingFridgeForShopping()
        {
            if(GetFreeSpace() < 20)
            {
                ThrowingExpiredItemsFromFridge();
                if (GetFreeSpace() < 20)
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
        //Cleaning the refrigerator of products whose expiration date is soon - for the purpose of printing
        public string CleanRefrigerator()
        {
            string itemsString = "";

            foreach(Shelf shelf in Shelves)
            {
                itemsString += shelf.CheckingProductsExpireSoon("Dairy", DateTime.Now.Date.AddDays(3));
            }
            foreach(Shelf shelf in Shelves)
            {
                itemsString += shelf.CheckingProductsExpireSoon("Meat", DateTime.Now.Date.AddDays(7));
            }
            foreach (Shelf shelf in Shelves)
            {
                itemsString += shelf.CheckingProductsExpireSoon("Fur", DateTime.Now.Date.AddDays(1));
            }

            return itemsString;
        }

    }
    }

