using System;
/// <summary>
/// <para>
/// The MediSure_Clinic namespace contains all classes required for managing
/// patient billing in the MediSure Clinic application.
/// </para>
/// <para>
/// It supports bill creation, insurance-based discount calculation,
/// final payable computation, and viewing or clearing the latest bill.
/// </para>
/// </summary>
namespace MediSure_Clinic
{
    #region class to store properties
    /// <summary>
    /// Represents a patient billing record containing consultation,
    /// laboratory, and medicine charges along with calculated billing totals.
    /// </summary>
    public class PatientBill
    {
        //bill properties
        public string BillId;
        public string PatientName;
        public bool HasInsurance;
        public decimal ConsultationFee;
        public decimal LabCharges;
        public decimal MedicineCharges;
        public decimal GrossAmount;
        public decimal DiscountAmount;
        public decimal FinalPayable;
        public static PatientBill LastBill;
        public static bool HasLastBill = false;
    }
    #endregion
    /// <summary>
    /// Provides operations to create, calculate, view, and clear patient bills
    /// within the MediSure Clinic billing system.
    /// </summary>
    public class BillingService
    {
        #region method to create bill
        //method to create a new bill
        public void CreateBill()
        {
            //create a new bill object
            PatientBill bill = new PatientBill();

            //get user inputs
            Console.Write("Enter Bill ID: ");
            bill.BillId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill ID cannot be empty.");
                return;
            }
            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            Console.Write("Is the patient insured? (Y/N): ");
            string ins = Console.ReadLine();

            //validate insurance input
            if (ins == "Y" || ins == "y")
                bill.HasInsurance = true;
            else if (ins == "N" || ins == "n")
                bill.HasInsurance = false;
            else
            {
                Console.WriteLine("Invalid input. Enter Y or N only.");
                return;
            }

            //get and validate charges
            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.ConsultationFee) || bill.ConsultationFee <= 0)
            {
                Console.WriteLine("Consultation Fee must be greater than 0.");
                return;
            }
            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.LabCharges) || bill.LabCharges < 0)
            {
                Console.WriteLine("Lab Charges must be zero or greater.");
                return;
            }
            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.MedicineCharges) || bill.MedicineCharges < 0)
            {
                Console.WriteLine("Medicine Charges must be zero or greater.");
                return;
            }
            //calculate the bill
            CalculateBill(bill);

            PatientBill.LastBill = bill;
            PatientBill.HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            PrintCalculation(bill);
        }
        #endregion
        #region method to view bill
        /// <summary>
        /// Displays the complete details of the most recently created patient bill,
        /// including all charges, discount applied, and the final payable amount.
        /// </summary>
        public void ViewBill()
        {
            if (!PatientBill.HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }
            //retrieve the last bill
            PatientBill bill = PatientBill.LastBill;

            //display bill details
            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine("BillId: " + bill.BillId);
            Console.WriteLine("Patient: " + bill.PatientName);
            Console.WriteLine("Insured: " + (bill.HasInsurance ? "Yes" : "No"));
            Console.WriteLine("Consultation Fee: " + bill.ConsultationFee);
            Console.WriteLine("Lab Charges: " + bill.LabCharges);
            Console.WriteLine("Medicine Charges: " + bill.MedicineCharges);
            Console.WriteLine("Gross Amount: " + bill.GrossAmount);
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount);
            Console.WriteLine("Final Payable: " + bill.FinalPayable);
            Console.WriteLine("--------------------------------");
        }
        #endregion
        #region method to clear bill
        /// <summary>
        /// Clears the most recently stored patient bill and resets the billing state,
        /// indicating that no bill is currently available for viewing or recalculation.
        /// </summary>
        public void ClearBill()
        {
            PatientBill.LastBill = null;
            PatientBill.HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
        #endregion
        #region method to calculate the bill
        /// <summary>
        /// Computes the gross amount, insurance-based discount, and final payable
        /// amount for the specified patient bill.
        /// </summary>
        /// <param name="bill">The patient bill for which billing totals are calculated.</param>
        private void CalculateBill(PatientBill bill)
        {
            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;

            if (bill.HasInsurance)
                bill.DiscountAmount = bill.GrossAmount * 0.10m;
            else
                bill.DiscountAmount = 0;

            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;
        }
        #endregion
        #region method for printing
        /// <summary>
        /// Prints the calculated gross amount, discount amount, and final payable
        /// amount for the specified patient bill.
        /// </summary>
        /// <param name="bill">The patient bill whose calculated values are displayed.</param>
        private void PrintCalculation(PatientBill bill)
        {
            Console.WriteLine("Gross Amount: " + bill.GrossAmount);
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount);
            Console.WriteLine("Final Payable: " + bill.FinalPayable);
            Console.WriteLine("------------------------------------------------------------");
        }
    }
    #endregion
}
