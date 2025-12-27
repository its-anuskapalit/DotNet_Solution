
/// <summary>
/// <para>)
/// •	InvoiceNo (string) — unique identifier (example: INV1001)
///•	CustomerName(string)
///•	ItemName(string)
///•	Quantity(int)
///•	PurchaseAmount(decimal) — total purchase cost for the invoice(not per-unit)
///•	SellingAmount(decimal) — total selling amount for the invoice(not per-unit)
///•	ProfitOrLossStatus(string) — PROFIT / LOSS / BREAK-EVEN(calculated)
///•	ProfitOrLossAmount(decimal) — calculated
///•	ProfitMarginPercent(decimal) — calculated(relative to PurchaseAmount)
/// </para>
/// </summary>
using System;
namespace QuickMartTraders
{ 
    public class SaleTransaction
    {
        //class properties
        public string InvoiceNo;
        public string CustomerName;
        public string ItemName;
        public int Quantity;
        public decimal PurchaseAmount;
        public decimal SellingAmount;
        public string ProfitOrLossStatus;
        public decimal ProfitOrLossAmount;
        public decimal ProfitMarginPercent;
        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;
    }

    //class to handle transaction operations
    public class TransactionService
    {
        //method to create a new transaction
        public void CreateTransaction()
        {
            //input details
            SaleTransaction saleTransaction = new SaleTransaction();

            Console.Write("Enter Invoice No: ");

            // Validate InvoiceNo is not empty
            saleTransaction.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(saleTransaction.InvoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }
            Console.Write("Enter Customer Name: ");
            saleTransaction.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            saleTransaction.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            // Validate Quantity is a positive integer
            if (!int.TryParse(Console.ReadLine(), out saleTransaction.Quantity) || saleTransaction.Quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            Console.Write("Enter Purchase Amount (total): ");
            //  Validate PurchaseAmount is a positive decimal
            if (!decimal.TryParse(Console.ReadLine(), out saleTransaction.PurchaseAmount) || saleTransaction.PurchaseAmount <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than 0.");
                return;
            }

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out saleTransaction.SellingAmount) || saleTransaction.SellingAmount < 0)
            {
                Console.WriteLine("Selling Amount must be zero or greater.");
                return;
            }

            //perform calculations
            Calculate(saleTransaction);

            //store the last transaction
            SaleTransaction.LastTransaction = saleTransaction;
            SaleTransaction.HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            PrintCalculation(saleTransaction);
        }

        //method to view the last transaction
        public void ViewTransaction()
        {
            if (!SaleTransaction.HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }
            //display details
            SaleTransaction saleTransaction = SaleTransaction.LastTransaction;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine("InvoiceNo: " + saleTransaction.InvoiceNo);
            Console.WriteLine("Customer: " + saleTransaction.CustomerName);
            Console.WriteLine("Item: " + saleTransaction.ItemName);
            Console.WriteLine("Quantity: " + saleTransaction.Quantity);
            Console.WriteLine("Purchase Amount: " + saleTransaction.PurchaseAmount);
            Console.WriteLine("Selling Amount: " + saleTransaction.SellingAmount);
            Console.WriteLine("Status: " + saleTransaction.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + saleTransaction.ProfitOrLossAmount);
            Console.WriteLine("Profit Margin (%): " + saleTransaction.ProfitMarginPercent);
            Console.WriteLine("--------------------------------------------");
        }

        //method to recalculate profit/loss and print
        public void Recalculate()
        {
            if (!SaleTransaction.HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }
            //recalculate and print
            Calculate(SaleTransaction.LastTransaction);
            PrintCalculation(SaleTransaction.LastTransaction);
        }

        //method to perform profit/loss calculations
        private void Calculate(SaleTransaction saleTransaction)
        {
            //determine profit/loss status and amounts
            if (saleTransaction.SellingAmount > saleTransaction.PurchaseAmount)
            {
                saleTransaction.ProfitOrLossStatus = "PROFIT";
                saleTransaction.ProfitOrLossAmount = saleTransaction.SellingAmount - saleTransaction.PurchaseAmount;
            }
            else if (saleTransaction.SellingAmount < saleTransaction.PurchaseAmount)
            {
                saleTransaction.ProfitOrLossStatus = "LOSS";
                saleTransaction.ProfitOrLossAmount = saleTransaction.PurchaseAmount - saleTransaction.SellingAmount;
            }
            else
            {
                saleTransaction.ProfitOrLossStatus = "BREAK-EVEN";
                saleTransaction.ProfitOrLossAmount = 0;
            }

            saleTransaction.ProfitMarginPercent = (saleTransaction.ProfitOrLossAmount / saleTransaction.PurchaseAmount) * 100;
        }

        //method to print calculation results
        private void PrintCalculation(SaleTransaction saleTransaction)
        {
            Console.WriteLine("Status: " + saleTransaction.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + saleTransaction.ProfitOrLossAmount);
            Console.WriteLine("Profit Margin (%): " + saleTransaction.ProfitMarginPercent);
            Console.WriteLine("------------------------------------------------------");
        }
    }
}
