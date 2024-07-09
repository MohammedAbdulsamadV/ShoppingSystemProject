using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
namespace ShoppingSystemProject_Generics_Collections_
{
    internal class Program
    {
        public static List<string> Cart = new List<string>();
        public static Dictionary<String,double> ItemPrice = new Dictionary<String, double>() {
            { "Camera",3000},{"Bag",150},{"Laptop",5000},{"TV",3400}
        };
        public static Stack <string> stack = new Stack<string>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1- Add item to Cart");
                Console.WriteLine("2- View Cart");
                Console.WriteLine("3- Removw Item from Cart");
                Console.WriteLine("4- Checkout");
                Console.WriteLine("5- Undo Last Action");
                Console.WriteLine("6- Exit");

                Console.WriteLine("Enter Your Choice");
                string choice = Console.ReadLine();
                int choiceINT = Convert.ToInt32(choice);

                switch (choiceINT)
                {
                    case 1:
                        AddItem<string>();
                        break;
                    case 2:
                        ViewCart();
                        break;
                    case 3:
                        RemoveItem();
                        break;
                    case 4:
                        Checkout();
                        break;
                    case 5:
                        UndoLastAction();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }

        private static void AddItem<T>()
        {
            Console.WriteLine("The available items");
            foreach (var item in ItemPrice)
            {
                Console.WriteLine($"Item:{item.Key},Price:{item.Value}");
            }
            Console.WriteLine("Enter your product name");
            string itemname = Console.ReadLine();
            if (ItemPrice.ContainsKey(itemname))
            {
                Cart.Add(itemname);
                stack.Push($"{itemname} Added");
                Console.WriteLine($"{itemname} Successfully Added");
            }else
                Console.WriteLine("Not Available now");
        }
        private static void ViewCart()
        {
            Console.WriteLine("Your Items:");
            if (Cart.Any())
            {
                var itempricecollection = GetCartPrices();
               
                foreach (var item in itempricecollection)
                {
                    Console.WriteLine($"Item:{item.Item1},Price{item.Item2}");
                }

            }
            else
                Console.WriteLine("Your Cart is empty");
        }

        private static IEnumerable<Tuple<string, double>> GetCartPrices()
        {
           var cartprice = new List<Tuple<string,double>>();
            foreach (var item in Cart)
            {
                double price = 0;
                bool found = ItemPrice.TryGetValue(item,out price);
                if (found is true)
                {
                    Tuple<string, double> itemprice = new Tuple<string, double>(item,price);
                    cartprice.Add(itemprice);
                }
            }
            return cartprice;
        }
        private static void RemoveItem()
        {
            ViewCart();
            if (Cart.Any())
            {
                Console.WriteLine("Select the item you want to remove");
                string item = Console.ReadLine();
                if (Cart.Contains(item))
                {
                    Cart.Remove(item);
                    stack.Push($"{item} Removed");
                    Console.WriteLine($"{item} Successfully Removed");
                }else
                    Console.WriteLine("Not found");

            }
        }
        private static void Checkout()
        {
            if (Cart.Any())
            {
                double totalprice = 0;
                IEnumerable<Tuple<string, double>> itemsprice = GetCartPrices();
                foreach (var item in itemsprice)
                {
                    if (itemsprice.Contains(item))
                    {
                        totalprice += item.Item2;
                    }
                }
                Console.WriteLine($"Total Price : {totalprice}");
                Console.WriteLine("Thank you for shopping with us ");
                Cart.Clear();
                stack.Push("checkout");
            }
            else
                Console.WriteLine("Cart is Empty");
        }

        private static void UndoLastAction()
        {
            if (stack.Count > 0)
            {
                string lastaction = stack.Pop();
                Console.WriteLine($"your last action {lastaction}");
                var action = lastaction.Split();
                if (lastaction.Contains("Added"))
                {
                    Cart.Remove(action[0]);
                }
                else if (lastaction.Contains("Removed"))
                {
                    Cart.Add(action[0]);
                }else
                    Console.WriteLine("checkout can not be undo, please ask for refund ");
            }
        }


    }
}
