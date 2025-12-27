using System;
namespace MediSure_Clinic
{
    class Program
    {
        static void Main()
        {
            BillingService service = new BillingService();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();
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
            }
        }
    }
}
