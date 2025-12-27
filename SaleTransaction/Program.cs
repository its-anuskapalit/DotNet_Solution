using QuickMartTraders;
//class containing application entry point
class Program
{
    //application entry point
    public static void Main()
    {
        //create service instance
        TransactionService service = new TransactionService();
        bool exit = false;

        //application loop
        while (!exit)
        {
            //display menu
            Console.WriteLine("\n================== QuickMart Traders ==================");
            Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
            Console.WriteLine("2. View Last Transaction");
            Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");

            string choice = Console.ReadLine();

            //handle user choice
            switch (choice)
            {
                case "1":
                    service.CreateTransaction();
                    break;
                case "2":
                    service.ViewTransaction();
                    break;
                case "3":
                    service.Recalculate();
                    break;
                case "4":
                    Console.WriteLine("Thank you. Application closed normally.");
                    exit = true;
                    break;
                //default case for invalid input
                default:
                    Console.WriteLine("Invalid option. Please choose between 1 to 4.");
                    break;
            }
        }
    }
}