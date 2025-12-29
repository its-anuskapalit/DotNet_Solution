using System;
/// <summary>
/// <para>
/// Entry point for the MediSure Clinic billing application.
/// </para>
/// <para>
/// This console program allows the user to create patient bills,
/// view the most recent bill, clear billing history, and exit the system
/// using a menu-driven interface.
/// </para>
/// </summary>
namespace MediSure_Clinic
{
    class Program
    {
        /// <summary>
        /// Starts the MediSure Clinic billing application and repeatedly displays
        /// a menu until the user chooses to exit.
        /// </summary>
        static void Main()
        {
            // Create billing service instance
            BillingService service = new BillingService();
            // Flag to control application exit
            bool exit = false;
            // Main menu loop
            while (!exit)
            {
                #region user choice
                // Display menu options
                Console.WriteLine("\n================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                // Read user choice
                string choice = Console.ReadLine();
                #endregion
                #region menu display
                // Handle menu selection
                switch (choice)
                {
                    case "1":
                        service.CreateBill();
                        break;
                    case "2":
                        service.ViewBill();
                        break;
                    case "3":
                        service.ClearBill();
                        break;
                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 to 4.");
                        break;
               }
                #endregion
            }
        }

    }
}
