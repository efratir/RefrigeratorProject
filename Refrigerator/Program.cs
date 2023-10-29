// See https://aka.ms/new-console-template for more information
using System;
using System.Globalization;

namespace Refrigerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Refrigerator> refrigeratorItems = new List<Refrigerator>();
            Refrigerator refrigerator1 = new Refrigerator("Samsung", "Black", 7);
            Shelf s1 = new Shelf(1, 23);
            Shelf s2 = new Shelf(2, 23);
            Shelf s3 = new Shelf(3, 20);
            Shelf s4 = new Shelf(4, 20);
            Shelf s5 = new Shelf(5, 17);
            Shelf s6 = new Shelf(6, 17);
            Shelf s7 = new Shelf(7, 15);
            refrigerator1.AddShelf(s1);
            refrigerator1.AddShelf(s2);
            refrigerator1.AddShelf(s3);
            refrigerator1.AddShelf(s4);
            refrigerator1.AddShelf(s5);
            refrigerator1.AddShelf(s6);
            refrigerator1.AddShelf(s7);
            Item milk = new Item("milk", "Drink", "Dairy", new DateTime(2022, 10, 30), 2);
            Item eggs = new Item("eggs", "food", "fur", new DateTime(2024, 3, 15), 12);
            refrigerator1.PutItemInFridge(milk);
            refrigerator1.PutItemInFridge(eggs);
            Console.WriteLine(refrigerator1);

            try
            {
                while (true)
                {
                    DisplayMenu();
                    int choice = GetMenuChoice();

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine(refrigerator1.GetAllShelves());
                            break;
                        case 2:
                            Console.WriteLine(refrigerator1.GetFreeSpace());
                            break;
                        case 3:
                            AddItemToRefrigerator(refrigerator1);
                            break;
                        case 4:
                            { 
                            refrigerator1.RemoveItemFromFridge(milk.ID);
                            Console.WriteLine(refrigerator1);
                            }
                            break;
                        case 5:
                            Console.WriteLine(refrigerator1.CleanRefrigerator());
                            break;
                        case 6:
                             WhatToEat(refrigerator1);
                            break;
                        case 7:
                            Console.WriteLine(refrigerator1.SortItemsInFridge());
                            break;
                        case 8:
                            refrigerator1.SortedShelves(refrigerator1.Shelves);
                            break;
                        case 9:
                            Console.WriteLine(refrigerator1.SortRefrigeratorsByFreeSpace(refrigeratorItems));
                            break;
                        case 10:
                            refrigerator1.PreparingFridgeForShopping();
                            break;
                        case 100:
                            Console.WriteLine("Closing the system. Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
               
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1: Print all items in the refrigerator");
            Console.WriteLine("2: Print remaining space in the refrigerator");
            Console.WriteLine("3: Add an item to the refrigerator");
            Console.WriteLine("4: Remove an item from the refrigerator");
            Console.WriteLine("5: Clean the refrigerator and print checked items");
            Console.WriteLine("6: Ask what to eat and bring a product");
            Console.WriteLine("7: Print all products sorted by expiration date");
            Console.WriteLine("8: Print all shelves arranged by free space");
            Console.WriteLine("9: Print all refrigerators arranged by free space");
            Console.WriteLine("10: Prepare the refrigerator for shopping");
            Console.WriteLine("100: Close the system");
        }
        static int GetMenuChoice()
        {
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            int choice;
            while (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Console.Write("Enter your choice: ");
                input = Console.ReadLine();
            }
            return choice;
        }

        static void AddItemToRefrigerator(Refrigerator refrigerator)
        {
            string name = null;
            string type = null;
            string kosher = null;
            DateTime expiryDate = DateTime.Now;
            double spaceInCm = 0;

            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Enter an item name:");
                name = Console.ReadLine();

                Console.WriteLine("Enter an item type:");
                type = Console.ReadLine();

                Console.WriteLine("Enter an item kosher:");
                kosher = Console.ReadLine();

                Console.WriteLine("Enter an item expiry date (format: dd/MM/yyyy):");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid expiry date format. Please try again.");
                }

                Console.WriteLine("Enter an item space in cm:");
                if (double.TryParse(Console.ReadLine(), out spaceInCm))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid space in cm. Please try again.");
                }
            }

            Item item = new Item(name, type, kosher, expiryDate, spaceInCm);
            refrigerator.PutItemInFridge(item);
            Console.WriteLine(item);
        }
            static void WhatToEat(Refrigerator refrigerator)
            {
                List<Item> listItems = new List<Item>();
                Console.WriteLine("What do you want to eat? (Drink/Food and Dairy/Meat/Fur)");
                string type = Console.ReadLine();
                string kosher = Console.ReadLine();

                listItems = refrigerator.WhaTotEat(type, kosher);
                foreach (Item item in listItems)
                {
                Console.WriteLine(item);
                 }

            }

       
    }
}