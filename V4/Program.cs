using System;
using System.IO;
using System.Linq;

namespace V3
{
    class Program
    {

		public static string filepath;

        static void Main(string[] args)
        {

			filepath = args[0];

			var Exists = (File.Exists(filepath));

			if (!Exists)
			{

				Console.Clear();
				Record("ID", "First Name", "Last Name", "Age", "Savings", "Password", filepath);
				Console.WriteLine("\nThe file of records has been created!\n");
			
			}

			while (true)
            {

                Menu();

            }
		}

        public static void Menu()
        {

            Console.WriteLine("\n 	DataRegister App! v2.0.0\n========================================");
            Console.WriteLine("\n1. Registry a record");
            Console.WriteLine("2. View the file of records");
            Console.WriteLine("3. Record Finder");
            Console.WriteLine("4. Record Remover");
            Console.WriteLine("5. Record Editer");
            Console.WriteLine("6. Exit\n");

            string rightselec = Console.ReadLine();

            switch (rightselec)
            {

                case "1":

                    Console.Clear();
                    Procedure();

                    break;

                case "2":

                    Console.Clear();
                    ToList();

                    break;

                case "3":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string id = numInput();
                    Finder(id);

                    break;

                case "4":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string remove = numInput();
                    Delete(remove);

                    break;

                case "5":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string change = numInput();
                    Edit(change);

                    break;

                case "6":

                    Console.Clear();
                    Environment.Exit(0);

                    break;

                default:

                    Console.Clear();
					Console.WriteLine("\nSomething went wrong, try again.\n");
                    Menu();

                    break;

            }

        }

		public static void Procedure()
		{

            while (true)
            {

                Console.WriteLine("\nEnter the ID: ");
                string id = numInput();

                if (UniqueID(id))
                {

                    Console.WriteLine("\nThis ID has already been recorded.");
                    break;

                }

                Console.WriteLine("\nEnter the First Name: ");
                string fname = stdInput();
                Console.WriteLine("\nEnter the Last Name: ");
                string lname = stdInput();
                Console.WriteLine("\nEnter the Age: ");
                string age = numInput();
                Console.WriteLine("\nEnter the Savings: ");
                string saving = decInput();
                string fpw;
                string spw;
                do
                {
                    
                    Console.WriteLine("\nEnter the Password: ");
                    fpw = pwInput();
                    Console.WriteLine("\nConfirm the Password: ");
                    spw = pwInput();

                } while (fpw != spw);
                
                Console.WriteLine("\nSave [S]; Discart[D]; Exit[E]");
                string Selection = Console.ReadLine();

                switch (Selection.ToLower())
                {

                    case "s":

                        Record(id, fname, lname, age, saving, fpw, filepath);
                        Console.Clear();
                        Console.WriteLine("\nRecord registered correctly!\n");

                        break;

                    case "d":

                        Console.Clear();
                        Procedure();

                        break;

                    case "e":

                        Console.Clear();
                        break;

                    default:

                        Console.Clear();
                        Console.WriteLine("\nSomething went wrong, try again.");

                        break;

                }

                break;

            }


		}

		public static void Record(string ID, string FirstName, string LastName, string Age, string Savings, string Password, string filepath)
		{

			try
			{

				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
				{

					file.WriteLine(ID + "," + FirstName + "," + LastName + "," + Age + "," + Savings + "," + Password);

				}

			}

			catch(Exception exc)
			{

				throw new ApplicationException("This program failed to run correctly: ", exc);

			}

		}

        public static void ToList()
        {

            string[] data = File.ReadAllLines(filepath);
            Console.WriteLine("");
            
            foreach (var lines in data)
            {

                Console.WriteLine(lines);

            }

        }

        public static void Finder(string id)
        {       

            var lines = File.ReadLines(filepath);
            int counter = 0;

            foreach(var line in lines)
            {
               
                var identity = line.Split(new char[] {','});

                if (identity[0] == id)
                {

                    Console.Clear();
                    Console.WriteLine("Record found!\n" + line);
                    counter = 1;

                }

            }

            if (counter == 0)
            {
                
                Console.Clear();
                Console.WriteLine("That record doesn't exist.");

            }

        }

        public static bool UniqueID(string id)
        {

            var content = File.ReadLines(filepath);
            bool verify = false;

            foreach (var item in content)
            {

                var exists = item.Split(new char[] {','});

                if (exists[0] == id)
                {

                    return !verify;

                }
                
            }

            return verify;

        }

        public static void Delete(string id)
        {

            bool counter = false;
            var content = File.ReadAllLines(filepath);
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);

            foreach (var item in content)
            {

                var element = item.Split(",");

                if (element[0] == id)
                {

                    Console.Clear();
                    Console.WriteLine("The record has been deleted.");
                    counter = !counter;
                    continue;

                }

                file.WriteLine(item);

            }

            if (counter == false)
            {

                Console.Clear();
                Console.WriteLine("That record is not in the Registry File.");

            }

            file.Close();

        }

        public static void Edit(string id)
        {

            bool counter = false;
            var content = File.ReadAllLines(filepath);
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);

            foreach (var item in content)
            {

                var element = item.Split(",");

                if (element[0] == id)
                {

                    Console.Clear();
                    Console.WriteLine("Proceed to make the changes!");
                    Console.WriteLine("\nEnter the new First Name: ");
                    string fname = stdInput();
                    Console.WriteLine("\nEnter the new Last Name: ");
                    string lname = stdInput();
                    Console.WriteLine("\nEnter the new Age: ");
                    string age = numInput();
                    Console.WriteLine("\nEnter the new Savings: ");
                    string saving = decInput();
                    string fpw, spw;

                    do
                    {

                        Console.WriteLine("\nEnter the new Password: ");
                        fpw = pwInput();
                        Console.WriteLine("\nConfirm the new Password: ");
                        spw = pwInput();
                        
                    } while (fpw != spw);

                    if (element[1] != fname)
                    {

                        Console.WriteLine("\nChanges in the First Name made successfully!");

                    }

                    if (element[2] != lname)
                    {

                        Console.WriteLine("\nChanges in the Last Name made successfully!");

                    }

                    if (element[3] != age)
                    {

                        Console.WriteLine("\nChanges in the Age made successfully!");

                    }

                    if (element[4] != saving)
                    {

                        Console.WriteLine("\nChanges in the Savings made successfully!");

                    }

                    if (element[5] != fpw)
                    {

                        Console.WriteLine("\nChanges in the Password made successfully!");

                    }

                    if ((element[1] == fname) && (element[2] == lname) && (element[3] == age) && (element[4] == saving) && (element[5] == fpw))
                    {

                        Console.WriteLine("\nIt appears no changes has been made.");

                    }

                    file.WriteLine(id + "," + fname + "," + lname + "," + age + "," + saving + "," + fpw);
                    counter = !counter;
                    continue;

                }

                file.WriteLine(item);

            }

            if (counter == false)
            {

                Console.Clear();
                Console.WriteLine("That record is not in the Registry File.");

            }

            file.Close();

        }

        public static string pwInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsLetterOrDigit(key.KeyChar) || char.IsPunctuation(key.KeyChar) || char.IsSymbol(key.KeyChar))
                {

                    if (key.KeyChar != 44)
                    {

                        input += key.KeyChar.ToString();
                        Console.Write("*");

                    }

                }

            }

            return input;

        }

        public static string numInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsDigit(key.KeyChar))
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

            }

            return input;

        }

        public static string decInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsDigit(key.KeyChar) || key.KeyChar == 46)
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

            }

            return input;

        }

        public static string stdInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsLetterOrDigit(key.KeyChar))
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

            }

            return input;

        }

    }
}
