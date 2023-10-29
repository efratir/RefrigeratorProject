using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Shelf
    {
        
        public Guid ID { get; }
        public int FloorNumber { get; set; }
        public double SpaceOfShelf { get; set; }
        public double FreeSpaceOfShelf { get; set; }
        public List<Item> Items { get; set; }

        public Shelf(int floorNumber, double spaceOfShelf)
        {
            ID = new Guid();
            FloorNumber = floorNumber;
            SpaceOfShelf = spaceOfShelf;
            FreeSpaceOfShelf = spaceOfShelf;
            Items = new List<Item>();
        }

        public override string ToString()
        {
            string itemList = string.Join(", ", Items);

            string result = $"Shelf ID: {ID}\n";
            result += $"Floor Number: {FloorNumber}\n";
            result += $"Shelf Space: {SpaceOfShelf} square meters\n";
            result += $"Free Shelf Space: {FreeSpaceOfShelf} square meters\n";
            result += $"Items on Shelf: {itemList}";

            return result;
        }

        public string GetAllItems()
        {
            string items = "";

            if (Items.Count == 0)
            {
                Console.WriteLine("The refrigerator is empty");
            }
            else
            {
                items = string.Join("", Items.Select(item => item.ToString()));
            }

            return items;
        }


        public double GetFreeSpace()
        {
            return FreeSpaceOfShelf;
            //double remainingShelfSpace = 0;
            //double allSpaceOfItems = 0;
            //if (Items.Count() == 0)
            //    return SpaceOfShelf;
            //else 
            //{ 
            //foreach (Item item in Items)
            //{
            //    allSpaceOfItems += item.SpaceInCm;
            //}

            //if (allSpaceOfItems < SpaceOfShelf) 
            //    { 
            //    remainingShelfSpace = SpaceOfShelf - allSpaceOfItems;
            //    }
            //else 
            //    { 
            //    remainingShelfSpace = 0;
            //    }

            // return remainingShelfSpace;
            //}
        }

        public Item FindItemById(Guid idItem)
        {
            return Items.FirstOrDefault(item => item.ID == idItem);
        }

        public Item RemoveItem(Guid idItem)
        {
            Item removeItem = Items.FirstOrDefault(item => item.ID == idItem);

            if (removeItem != null)
            {
                FreeSpaceOfShelf += removeItem.SpaceInCm;
                Items.Remove(removeItem);
                Console.WriteLine(removeItem);
            }

            return removeItem;
        }

        public void ThrowingExpiredItems()
        {    
           Items.RemoveAll(item => item.ExpiryDate < DateTime.Now);
        }

        public List<Item> WhatToEat(string type, string kosher)
        {
            return Items.Where(item => IsItemMatchCondition(item, type, kosher))
                        .ToList();
        }

        private bool IsItemMatchCondition(Item item, string type, string kosher)
        {
            return item.Type.Equals(type) && item.Kosher.Equals(kosher) && item.ExpiryDate < DateTime.Now;
        }

        public List<Item> SortItemsByDate(List<Item> items)
        {
            return items.OrderBy(item => item.ExpiryDate).ToList();
        }

        //The sum of the free space of the blocks under check whether to be thrown
        public double CheckingFreeSpaceProducts(string kosher, DateTime date)
        {
            return Items.Where(item => IsItemMatchCondition(item, "", kosher) && item.ExpiryDate <= date)
                        .Sum(item => item.SpaceInCm);
        }

        //The products whose expiration date is close and are being tested to see if they can be thrown away, chained to a string
        public string CheckingProductsExpireSoon(string kosher, DateTime date)
        {
            return string.Join("", Items.Where(item => IsItemMatchCondition(item, "", kosher) && item.ExpiryDate <= date)
                                        .Select(item => item.ToString()));
        }

        public void ThrowingProductsExpireSoon(string kosher, DateTime date)
        {
            foreach (Item item in Items)
            {
                if (IsItemMatchCondition(item, "", kosher) && item.ExpiryDate <= date)
                {
                    RemoveItem(item.ID);
                }
            }
        }
    }
}