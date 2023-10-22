using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refrigerator
{
    internal class Shelf
    {
        public static int uniqueId = 1;
        public int ID { get; }
        public int FloorNumber { get; set; }
        public double ShelfSpace { get; set; }
        public List<Item> Items { get; set; }

        public Shelf(int floorNumber, double shelfSpace)
        {
            ID = uniqueId++;
            FloorNumber = floorNumber;
            ShelfSpace = shelfSpace;
            Items = new List<Item>();
        }

        public override string ToString()
        {
            string itemList = string.Join(", ", Items);
            return $"Shelf ID: {ID}\nFloor Number: {FloorNumber}\nShelf Space: {ShelfSpace} square meters\nItems on Shelf: {itemList}";
        }

        public string ReturnAllItems()
        {
            string items = "";

            foreach (Item item in Items)
            {
                if (Items.Count() == 0)
                {
                    Console.WriteLine("The refrigerator is empty");
                }
                else 
                { 
                items += item.ToString();
                }
            }
            return items;
           
          }
             

        public double GetRemainingSpaceShelf()
        {
            double remainingShelfSpace = 0;
            double allSpaceOfItems = 0;
            if (Items.Count() == 0)
                return ShelfSpace;
            else 
            { 
            foreach (Item item in Items)
            {
                allSpaceOfItems += item.SpaceInCm;
            }

            if (allSpaceOfItems < ShelfSpace) 
                { 
                remainingShelfSpace = ShelfSpace - allSpaceOfItems;
                }
            else 
                { 
                remainingShelfSpace = 0;
                }

             return remainingShelfSpace;
            }
        }

        public Item FindItem(int idItem)
        {
            foreach (Item item in Items)
            {
                if (item.ID == idItem)
                    return item;
            }
            return null;
        }

        public Item RemoveItemFromShelf(int idItem)
        {
            Item removeItem = null;
            foreach (Item item in Items)
            {
                if (item.ID == idItem)
                {
                    removeItem = item;                  
                }
            }
            Items.Remove(removeItem);
            Console.WriteLine(removeItem);
            return removeItem;
        }

        public void ThrowingExpiredItemsFromShelf()
        {
           
           Items.RemoveAll(item => item.ExpiryDate < DateTime.Now);
        }

        public List<Item> WhatEat(string type, string kosher)
        {
            List<Item> items = new List<Item>();
            foreach (Item item in Items)
            {
                if (item.Type.Equals(type) && item.Kosher.Equals(kosher) && item.ExpiryDate < DateTime.Now)
                    items.Add(item);
            }
            return items;
        }

       

        public List<Item> SortItemsByDate(List<Item> items)
        {
            items.Sort((item1, item2) => item1.ExpiryDate.CompareTo(item2.ExpiryDate));

            return items;
        }

        public double CheckingFreeSpaceProducts(string kosher, DateTime date)
        {
            double sum = 0;
            foreach (Item item in Items)
            {
                if (item.Kosher.Equals(kosher) && item.ExpiryDate <= date)
                    sum += item.SpaceInCm;
            }
            return sum;
        }

        public string CheckingProductsExpireSoonOfString(string kosher, DateTime date)
        {
            string items = "";
            foreach (Item item in Items)
            {
                if (item.Kosher.Equals(kosher) && item.ExpiryDate <= date)
                    items += item.ToString();
            }
            return items;
        }

        public void ThrowingProducts(string kosher, DateTime date)
        {
            foreach (Item item in Items)
            {
                if (item.Kosher.Equals(kosher) && item.ExpiryDate <= date)
                {
                    RemoveItemFromShelf(item.ID);
                }
            }
        }

       



    }
}

