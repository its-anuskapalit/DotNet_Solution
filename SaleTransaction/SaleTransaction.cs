
/// <summary>
/// <para>
/// QuickMart Traders is a console-based billing and profit analysis system
/// designed to record sales invoices and evaluate business performance.
/// </para>
/// <para>
/// The application enables users to create transactions, automatically
/// calculate profit or loss values, view the most recent invoice, and
/// recalculate results whenever required.
/// </para>
/// </summary>
using System;
namespace QuickMartTraders
{
    #region class to represent a sale transaction
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
    #endregion

    #region method signatures for transaction operations
    /// <summary>
    /// Captures user input for a new sales transaction, validates all values,
    /// calculates profit or loss, and stores the transaction as the latest record.
    /// </summary>
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
            #endregion

            #region main logic to calculate profit/loss
            //perform calculations
            Calculate(saleTransaction);
            //store the last transaction
            SaleTransaction.LastTransaction = saleTransaction;
            SaleTransaction.HasLastTransaction = true;
            Console.WriteLine("\nTransaction saved successfully.");
            PrintCalculation(saleTransaction);
        }
        #endregion

        #region method to view last transaction
        /// <summary>
        /// Displays the details and profit or loss summary of the most recent
        /// sales transaction stored in the system.
        /// </summary>
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
        #endregion
        #region method to recalculate profit/loss
        /// <summary>
        /// Recomputes the profit or loss values for the last recorded transaction
        /// and prints the updated calculation results.
        /// </summary>
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
        #endregion
        #region private helper methods
        /// <summary>
        /// Calculates the profit or loss amount, status, and profit margin percentage
        /// based on the purchase and selling amounts of the provided transaction.
        /// </summary>
        /// <param name="saleTransaction">The sales transaction for which the calculation is performed.</param>
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

        #endregion
        #region method to print calculation results
        /// <summary>
        /// Prints the calculated profit or loss status, amount, and profit margin
        /// percentage for the specified sales transaction.
        /// </summary>
        /// <param name="saleTransaction">The transaction whose calculation results are printed.</param>
        private void PrintCalculation(SaleTransaction saleTransaction)
        {
            Console.WriteLine("Status: " + saleTransaction.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + saleTransaction.ProfitOrLossAmount);
            Console.WriteLine("Profit Margin (%): " + saleTransaction.ProfitMarginPercent);
            Console.WriteLine("------------------------------------------------------");
        }
        #endregion
    }
}
