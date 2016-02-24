using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;



namespace BootcampResourceCheckoutSystem_Week2
{

    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, string> bookStatus = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Lots Of Programming Stuff", "IN" },
                { "Even More on C#", "IN" },
                { "Big Book of C#", "IN"},
                { "Databases and More", "IN" },
                { "SQL and You", "IN" },
                { "Big Book of Outdated Code", "IN" },
                { "Beginner's Guide to C#", "IN" },
                { "Beginner's Database Handbook", "IN" },
                { "C# Player's Guide", "IN" },
                { "String Comparison Basics", "IN" },
            };

            Dictionary<string, string> StudentID = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "U123", "JIMMY JONES" },
                { "U234", "JAMIE NOEL" },
                { "U345", "SCOTT BRIDGES" },
                { "U456", "ALEXANDER ZILL" },
                { "U567", "WILFORD WILLIAMS" },
             };

            List<string> students = new List<string>();

            students.Add("Jimmy Jones");
            students.Add("Jamie Noel");
            students.Add("Scott Bridges");
            students.Add("Alexander Zill");
            students.Add("Wilford Williams");
            students = students.ConvertAll(d => d.ToUpper());
            
            bool restart=true;
            {
                while (restart == true)
                {
                    Header();
                    {
                        Console.WriteLine("\nEnter Menu Item Number");
                        string menuItem = Console.ReadLine();

                        int userInput;
                        userInput = NumberCheck(menuItem);
                        int caseRestart = 0;
                        while (caseRestart == 0)
                        {
                            switch (userInput)
                        {
                                case 1:
                                    do
                                    {
                                        Console.Clear();
                                        Header();
                                        Console.WriteLine("Student Names:\n");
                                        StringBuilder names = new StringBuilder();
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            students.Sort();
                                            names.AppendLine(students[i]);
                                        }
                                        Console.WriteLine(names.ToString());
                                        userInput = DoNext(menuItem);
                                     
                                        Console.Clear();
                                        Header();
                                        continue;

                                    }
                                    while (userInput == 1);

                                    break;

                                case 2:
                                    do
                                    {
                                        Console.Clear();
                                        Header();
                                        Console.WriteLine("Available Resources:\n");

                                        StringBuilder resources = new StringBuilder();
                                        foreach (KeyValuePair<string, string> item in bookStatus.OrderBy(key => key.Key))

                                        {
                                            if (item.Value == "IN")
                                             resources.AppendLine(item.Key);
                                        }
                                        Console.WriteLine(resources.ToString());
                                        userInput = DoNext(menuItem);
                                        Console.Clear();
                                        Header();
                                        break;
                                    }
                                    while (userInput == 2);
                                    break;

                                case 3: // student account
                                    do
                                    {
                                        Console.Clear();
                                        Header();
                                        Console.WriteLine("\nType In A Four Digit User Account Number From The List Below To Print Account Information\n\n");


                                        StringBuilder account = new StringBuilder();
                                        foreach (KeyValuePair<string, string> pair in StudentID.OrderBy(key => key.Key))

                                        {
                                       account.AppendLine("User ID: " + pair.Key + " ** Name: " + pair.Value);
                                        }
                                        Console.WriteLine(account.ToString());

                                        string userNameInput = Console.ReadLine();
                                        NullOrWhiteSpace(userNameInput);
                                        userInput = NumberCheck(menuItem);
                                        string userNameInputUpper = userNameInput.ToUpper();

                                        if (StudentID.ContainsKey(userNameInputUpper))
                                        {
                                            if (bookStatus.ContainsValue(userNameInputUpper))
                                            {
                                                Console.Clear();
                                                Header();
                                                Console.WriteLine("\nThe Following Items Are Checked Out:");
                                                foreach (KeyValuePair<string, string> pair in bookStatus)
                                                {
                                                    if (pair.Value == userNameInputUpper)
                                                        Console.WriteLine(pair.Key);
                                                }

                                                StreamWriter writer = new StreamWriter("UserID" + userNameInputUpper + "_StudentAccount.txt");
                                                using (writer)
                                                {
                                                    writer.WriteLine("Bootcamp Resource Checkout System");
                                                    writer.WriteLine("Student Account For User ID: " + userNameInputUpper + ", " + StudentID[userNameInputUpper]);
                                                    writer.WriteLine("\nChecked Out Resources:\n");

                                                    foreach (KeyValuePair<string, string> pair in bookStatus)
                                                    {
                                                        if (pair.Value == userNameInputUpper)
                                                        writer.WriteLine(pair.Key);
                                                    }
                                                }
                                                Console.Clear();
                                                Console.WriteLine("\nFile Export Preview\n");
                                                using (StreamReader sr = File.OpenText("UserID" + userNameInputUpper + "_StudentAccount.txt"))
                                                {
                                                    string s = "";
                                                    while ((s = sr.ReadLine()) != null)
                                                    {

                                                        Console.WriteLine(s);
                                                    }
                                                    
                                                    Console.WriteLine("\nYour Student Account Was Successfully Exported.\nPress \"Y\" To Preview And Print A Summary Of All Library Resources.\nPress Any Other Key For The Main Menu\n");
                                                        {
                                                            string restartAsString = Console.ReadLine();
                                                            userInput = NumberCheck(menuItem);
                                                            if (restartAsString.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                                                            {

                                                                StreamWriter writer2 = new StreamWriter("UserID" + userNameInputUpper + "_AvailableResources.txt");
                                                                using (writer2)
                                                                {
                                                                    writer2.WriteLine("Bootcamp Resource Checkout System");
                                                                    writer2.WriteLine("Resource List For User ID: " + userNameInputUpper + ", " + StudentID[userNameInputUpper]);
                                                                    writer2.WriteLine("\n\nUnavailable Resources:\n");

                                                                    foreach (KeyValuePair<string, string> pair in bookStatus)
                                                                    {
                                                                        if (pair.Value != "IN")
                                                                            writer2.WriteLine(pair.Key);
                                                                    }
                                                                    writer2.WriteLine("\n\nAvailable Resources:\n");
                                                                    foreach (KeyValuePair<string, string> pair in bookStatus)
                                                                    {
                                                                        if (pair.Value == "IN")
                                                                            writer2.WriteLine(pair.Key);
                                                                    }
                                                                }

                                                                Console.Clear();
                                                                Console.WriteLine("\nBootcamp Resource Book Summary\n");
                                                                using (StreamReader sr2 = File.OpenText("UserID" + userNameInputUpper + "_AvailableResources.txt"))
                                                                {
                                                                    string s2 = "";
                                                                    while ((s2 = sr2.ReadLine()) != null)
                                                                    {
                                                                        Console.WriteLine(s2);
                                                                    }

                                                                }
                                                            }
                                                            else
                                                            {
                                                            Console.Clear();
                                                            Header();
                                                           
                                                            userInput = DoNext(menuItem);
                                                            Console.Clear();
                                                            Header();
                                                            break;
                                                        }
                                                        }
                                                    

                                                }
                                                {
                                                    Console.WriteLine();
                                                    StringBldrLine();
                                                    Console.WriteLine();
                                                    Console.WriteLine();
                                                    Header();
                                                    userInput = DoNext(menuItem);
                                            
                                                    break;
                                                }
                                            }
                                            else
                                            {

                                                Console.Clear();
                                                Header();
                                                Console.WriteLine("\nYou Have No Books Checked Out");
                                                userInput = DoNext(menuItem);
                                                Console.Clear();
                                                Header();
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("\nThat Is Not A Valid Entry");
                                            userInput = DoNext(menuItem);
                                            Console.Clear();
                                            Header();
                                            break;
                                        }

                                    }
                                    while (userInput == 3);
                                    break;

                                case 4: // checkout
                                    do
                                    {
                                        Console.Clear();
                                        Header();
                                        int booksOut = 1;
                                        if (bookStatus.ContainsValue("IN"))
                                            {

                                            Console.WriteLine("\nEnter User Account Nbr To Begin Checkout\n\n");
                                            {
                                                foreach (KeyValuePair<string, string> pair in StudentID)
                                                {
                                                    Console.WriteLine(pair.Key + "   " + pair.Value);
                                                }

                                                string userNameInput = Console.ReadLine();
                                                NullOrWhiteSpace(userNameInput);
                                                userInput = NumberCheck(menuItem);
                                                string userNameInputUpper = userNameInput.ToUpper();


                                                foreach (KeyValuePair<string, string> pair in bookStatus)
                                                    if (pair.Value == userNameInputUpper)
                                                        booksOut = booksOut + 1;

                                                if (StudentID.ContainsKey(userNameInputUpper))
                                                {
                                                    if (booksOut <= 3)

                                                    {
                                                        Console.Clear();
                                                        Header();
                                                        Console.WriteLine("\n\nChoose A Book To Checkout");
                                                        foreach (KeyValuePair<string, string> item in bookStatus.OrderBy(key => key.Key))
                                                        {
                                                            if (item.Value == "IN")
                                                                Console.WriteLine(item.Key);
                                                        }
                                                        Console.WriteLine();

                                                        string userResourceInput = Console.ReadLine();
                                                        NullOrWhiteSpace(userResourceInput);
                                                        userInput = NumberCheck(menuItem);
                                                        string userResourceInputUpper = userResourceInput.ToUpper();

                                                        if ((bookStatus.ContainsKey(userResourceInput)) && (bookStatus[userResourceInputUpper] == "IN"))

                                                        {
                                                            Console.Clear();
                                                            Header();
                                                            Console.WriteLine("\nUser ID: " + userNameInputUpper + " has checked out \"" + userResourceInputUpper + "\".");

                                                            bookStatus[userResourceInputUpper] = userNameInputUpper;

                                                            userInput = DoNext(menuItem);
                                                            Console.Clear();
                                                            Header();

                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Error: Request Unavailable");
                                                            userInput = DoNext(menuItem);
                                                            Console.Clear();
                                                            Header();
                                                            break;
                                                        }

                                                    }
                                                    if (booksOut >= 3)
                                                    {
                                                        Console.WriteLine("\nUser ID: " + userNameInputUpper + " has checked out the maximum number of resources.");
                                                        userInput = DoNext(menuItem);
                                                        Console.Clear();
                                                        Header();
                                                        break;
                                                    }

                                                }
                                                else
                                                {
                                                    Console.WriteLine("\nThat Is Not A Valid Entry");
                                                    userInput = DoNext(menuItem);
                                                    Console.Clear();
                                                    Header();
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nAll Resources Are Currently Unavailable");
                                            userInput = DoNext(menuItem);
                                            Console.Clear();
                                            Header();
                                            break;
                                        }
                                    }
                                    while (userInput == 4);
                                    break;

                                case 5: //Return
                                    do
                                    {
                                        Console.Clear();
                                        Header();
                                        Console.WriteLine("Enter User Account Nbr To Begin Checkout\n\n");
                                        
                                            foreach (KeyValuePair<string, string> pair in StudentID)
                                            {
                                                Console.WriteLine(pair.Key + "   " + pair.Value);
                                            }

                                        string userNameInput = Console.ReadLine();
                                        NullOrWhiteSpace(userNameInput);
                                        userInput = NumberCheck(menuItem);
                                        string userNameInputUpper = userNameInput.ToUpper();

                                        if (StudentID.ContainsKey(userNameInputUpper))
                                        {
                                            if (bookStatus.ContainsValue(userNameInputUpper))
                                            {
                                                Console.Clear();
                                                Header();
                                                Console.WriteLine("\nThe Following Items Are Checked Out:");
                                                foreach (KeyValuePair<string, string> pair in bookStatus)
                                                {
                                                    if (pair.Value == userNameInputUpper)
                                                        Console.WriteLine(pair.Key);
                                                }
                                                {
                                                    Console.WriteLine("\nEnter the Book Name You Want To Return");
                                                    string userResourceInput = Console.ReadLine();
                                                    NullOrWhiteSpace(userResourceInput);
                                                    userInput = NumberCheck(menuItem);
                                                    string userResourceInputUpper = userResourceInput.ToUpper();

                                                    if ((bookStatus.ContainsKey(userResourceInput)) && (bookStatus[userResourceInputUpper] == userNameInputUpper))

                                                    {
                                                        Console.Clear();
                                                        Header();
                                                        Console.WriteLine("\nUser ID: " + userNameInputUpper + " has checked in \"" + userResourceInputUpper + "\".");

                                                        bookStatus[userResourceInputUpper] = "IN";

                                                        userInput = DoNext(menuItem);
                                                        Console.Clear();
                                                        Header();

                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Header();
                                                        Console.WriteLine("\nError: Request Unavailable");
                                                        userInput = DoNext(menuItem);
                                                        Console.Clear();
                                                        Header();
                                                        break;
                                                    }
                                                    
                                                }
                                            }
                                            else
                                            {

                                                Console.Clear();
                                                Header();
                                                Console.WriteLine("\nYou Have No Books Checked Out");
                                                userInput = DoNext(menuItem);
                                                Console.Clear();
                                                Header();
                                                break;
                                            }

                                            }
                                      
                                        Console.WriteLine("\nThat Is Not A Valid Entry");
                                        userInput = DoNext(menuItem);
                                        Console.Clear();
                                        Header();
                                        break;
                                    }
                                    while (userInput == 5);
                                    break;

                                case 6:

                                    {   
                                        Console.WriteLine("\nAre you sure you want to exit? \nPress \"N\" to restart program\nPress any other key to exit");

                                        string restartAsString = Console.ReadLine();
                              
                                        userInput = NumberCheck(menuItem);
                                        
                                        if (restartAsString.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                                        {
                                  
                                            caseRestart++;
                                            Console.Clear();
                                            Header();
                                            break;
                                        }
                                        else
                                        {

                                            Console.Clear();
                                            Console.WriteLine("GoodBye");
                                            Thread.Sleep(1000);
                                            Environment.Exit(0);
                                        }
                                        break;

                                    }

                                default:
                                    {
                                        Console.WriteLine("\nThat is not a Valid Entry");
                                        userInput = DoNext(menuItem);
                                        Console.Clear();
                                        Header();
                                       
                                        break;
                                    }
                                }
                        }
                    }
                }
            }
        }
// /////////////////////////////////////////////////NullOrWhiteSpace Method /////////////////////////

        static void NullOrWhiteSpace(string stringInput)
        {
            bool a;

            a = string.IsNullOrWhiteSpace(stringInput);

            if (a == true)
            {
                Console.WriteLine("Error: Request Unavailable");

            }

        }

        ///////////////////////////////////////////////////////////////////Number Check Method///////////////////////////////////////////

        static int NumberCheck(string input)
        {
            int menuItem;

            do
            {

                bool numVer = int.TryParse(input, out menuItem);
                if ((menuItem != 0))
                {
                    return menuItem;
                }
                else if (menuItem == 0)
                {
                    Console.WriteLine("That is not a valid entry, pleae enter a number");
                    input = Console.ReadLine();
                }
            }
            while (menuItem == 0);
            return menuItem;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////HEADER METHOD/////////////////////////////////////////////////////////////////
        static void Header()
        {
           // Console.Clear();
            string title = "BOOTCAMP RESOURCES CHECKOUT SYSTEM";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title + "\n\n", Console.Title);

            Console.WriteLine("Main Menu");
            Console.WriteLine("1: View Students");
            Console.WriteLine("2: View Available Resources");
            Console.WriteLine("3: View And Print Student Accounts");
            Console.WriteLine("4: Checkout Item");
            Console.WriteLine("5: Return Item");
            Console.WriteLine("6: Exit");
            StringBldrLine();
        }
        //////////////////////////////////////////Do Next METHOD/////////////////////////////////////////////////////////////////
        static int DoNext(string menuItem)

        {
            int userInput;
            Console.WriteLine("\nWhat would you like to do next? Enter a menu number:");
            menuItem = Console.ReadLine();

            userInput = NumberCheck(menuItem);

            return userInput;

        }

        //////////////////////////////////////////StringBldrLine METHOD/////////////////////////////////////////////////////////////////
        static void StringBldrLine()
        {
            StringBuilder sb = new StringBuilder();
          sb.Append("*");
            for (int i = 1; i <= 79; i++)
            {
                sb.Append("*");
            }
             Console.WriteLine(sb);
            }
    }
}





    
        










