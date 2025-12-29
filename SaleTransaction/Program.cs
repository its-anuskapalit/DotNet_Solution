/// <summary>
/// <para>
/// Entry point for the QuickMart Traders console application.
/// </para>
/// <para>
/// This program provides a menu-driven interface to create sales transactions,
/// view the latest invoice, and calculate profit or loss details.
/// </para>
/// </summary> 
using QuickMartTraders;
/// <summary>
/// Contains the application entry point and controls the main menu loop
/// for user interaction with the QuickMart Traders system.
/// </summary>
class Program
{
    //application entry point
    public static void Main()
    {
        //create service instance
        TransactionService service = new TransactionService();
        bool exit = false;

        #region menu loop
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
        #endregion
    }
}