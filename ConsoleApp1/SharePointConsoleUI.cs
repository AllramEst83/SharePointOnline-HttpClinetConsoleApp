using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SharePointConsoleUI
    {
        internal void StartUp()
        {
            HttpExecutor _executor = new HttpExecutor();
            var mainChoice = String.Empty; ;

            Console.WriteLine("---Welcome to the SharePoint Online HttpClient Console App---");
            Console.WriteLine("Would You like to connect to the currently set default SharePoint site and list or insert a new Url.");

            Console.WriteLine("1. Use default SharePoint site <--INSERT YOUR OWN  SP TENANT INFO IN THE CODE--");
            Console.WriteLine("2. Insert my own Url. <--DOES NOT WORK AT THE MOMENT--" + Environment.NewLine);
            Console.Write("Your choice: ");
            mainChoice = Console.ReadLine();
            Console.Clear();
            if (int.Parse(mainChoice) == 1)
            {
                do
                {
                    Console.Clear();

                    Console.WriteLine("This is the items for the List 'HttpClientList'");
                    _executor.Get();
                    Console.Write(Environment.NewLine + "What would you like to do?" + Environment.NewLine);

                    var choice = Choices();


                    if (choice == 1)
                    {
                        string id = String.Empty;
                        Console.Write("Please selecct a item id: ");
                         id = Console.ReadLine();
                        Console.Clear();
                        _executor.GetOne(int.Parse(id));
                        Console.WriteLine("Press any button to go back.");
                        Console.ReadLine();
                    }else if(choice == 2)
                    {
                        Console.WriteLine("Add item");
                        Console.WriteLine("Please write a item Title and Value.");
                        Console.Write("Title: ");
                        var title = Console.ReadLine();
                        Console.Write("Value: ");
                        var value = Console.ReadLine();

                        _executor.Post(title, value);
                        _executor.Get();


                    }else if(choice == 3)
                    {
                        Console.WriteLine("Update item");
                        Console.WriteLine("Please write a item id, Title and Value.");
                        Console.Write("Id: ");
                        var id = Console.ReadLine();
                        Console.Write("Title: ");
                        var title = Console.ReadLine();
                        Console.Write("Value: ");
                        var value = Console.ReadLine();

                        _executor.Put(title, value, Int32.Parse(id));
                        _executor.Get();
                    }
                    else if(choice == 4)
                    {
                        Console.WriteLine("Delete item");
                        Console.WriteLine("Please write a item id.");
                        Console.Write("Id: ");
                        var id = Console.ReadLine();

                        _executor.Delete(Int32.Parse(id));
                        _executor.Get();

                    }else if(choice == 5)
                    {
                        Environment.Exit(0);
                    }
                } while (int.Parse(mainChoice) == 1);
         

                
            }else if(int.Parse(mainChoice) == 2)
            {
                Console.WriteLine("Please enter a valid Sharepoint URL, Username, password and listname");

                Console.Write("URL: ");
                var url = Console.ReadLine();
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                Console.Write("Listname: ");
                var listname = Console.ReadLine();

            }
        }

        internal int Choices()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. View all columns for a item");
            Console.WriteLine("2. Add item");
            Console.WriteLine("3. Update item");
            Console.WriteLine("4. Delete item");
            Console.WriteLine("5. Exit app");
            Console.WriteLine("-------------------------------");

            Console.Write("Your choice: ");
            var choice = Console.ReadLine();
            Console.Clear();

            return Int32.Parse(choice);
        }

    }
}
