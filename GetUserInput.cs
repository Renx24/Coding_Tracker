
using System.Globalization;

namespace Coding_Tracker
{
    internal class GetUserInput
    {
        CodingController codingController = new();
        internal void MainMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to close the application at any point.");
                Console.WriteLine("1 to View records.");
                Console.WriteLine("2 to Add records.");
                Console.WriteLine("3 to Delete record.");
                Console.WriteLine("4 to Update record.");

                string commandInput = Console.ReadLine();

                while (string.IsNullOrEmpty(commandInput))
                {
                    Console.WriteLine("\nInvalid command. Please type a number from 0 to 4.\n");
                    commandInput = Console.ReadLine();
                }

                switch (commandInput)
                {
                    case "0":
                        Console.WriteLine("GoodBye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        codingController.Get();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        ProcessDelete();
                        break;
                    case "4":
                        ProcessUpdate();
                        break;
                    default:
                        Console.WriteLine("\nInvalid command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }

        private void ProcessUpdate()
        {
            codingController.Get();
            Console.WriteLine("\n\nPlease enter Id of entry you want to update. 0 to return to main menu.\n");
            string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("\nPlease enter a correct ID.\n");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                Console.WriteLine($"\nRecord with Id \"{id}\" does not exist.\nPlease enter a valid Id or 0 to return to main menu. ");
                ProcessUpdate();
            }

            var updateInput = "";
            bool updating = true;
            while(updating == true)
            {
                Console.WriteLine("Options:");
                Console.WriteLine("'d' - update date");
                Console.WriteLine("'t' - update duration(time)");
                Console.WriteLine("'s' - update save update");
                Console.WriteLine("'0' - return to main menu");

                updateInput = Console.ReadLine();

                switch(updateInput)
                {
                    case "d":
                        coding.Date = GetDateInput();
                        break;
                    case "t":
                        coding.Duration = GetDurationInput();
                        break;
                    case "0":
                        MainMenu();
                        updating = false;
                        break;
                    case "s":
                        updating = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid command.\n");
                        break;
                }

            }
            codingController.Update(coding);
            MainMenu();
        }

        private void ProcessAdd()
        {
            var date = GetDateInput();
            var duration = GetDurationInput();

            Coding coding = new();

            coding.Date = date;
            coding.Duration = duration;

            codingController.Post(coding);
        }

        private void ProcessDelete()
        {
            codingController.Get();
            Console.WriteLine("Enter ID of category you want to delete.");
            string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0 )
            {
                Console.WriteLine("\nPlease enter a correct ID.\n");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while(coding.Id == 0)
            {
                Console.WriteLine($"\nRecord with Id \"{id}\" does not exist.\nPlease enter a valid Id or 0 to return to main menu. ");
                ProcessDelete();
            }

            codingController.Delete(id);
        }


        internal string GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") MainMenu();

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. (format: dd-mm-yy). Type 0 to return to main menu or try again:\n\n");
                dateInput = Console.ReadLine();
                
            }

            return dateInput;
        }

        internal string GetDurationInput()
        {
            Console.WriteLine("\n\nPlease insert the duration: (hh:mm).\n\n");

            string durationInput = Console.ReadLine();

            if(durationInput == "0") MainMenu();

            while(!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\nInvalid duration. (format: hh:mm). Try again or type 0 to return to main menu");
                durationInput = Console.ReadLine();
                if (durationInput == "0") MainMenu();
            }

            return durationInput;
        }
    }
}