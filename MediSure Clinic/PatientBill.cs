using System;
///<summary>
/// MediSureClinic namespace to encapsulate billing related classes
/// <para>
///•	BillId (string) — unique identifier (example: BILL1001)
///•	PatientName(string)
///•	HasInsurance(bool) — true if the patient is insured
///•	ConsultationFee(decimal)
///•	LabCharges(decimal)
///•	MedicineCharges(decimal)
///•	GrossAmount(decimal) — calculated(not typed by user)
///•	DiscountAmount(decimal) — calculated(not typed by user)
///•	FinalPayable(decimal) — calculated(not typed by user)
/// </para>
/// </summary>
namespace MediSure_Clinic
{
    //class to store bill properties
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
    // class to calculate the billings
    public class BillingService
    {
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

        //method to view the last bill
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
        //method to clear the last bill
        public void ClearBill()
        {
            PatientBill.LastBill = null;
            PatientBill.HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
        //method to calculate the bill amounts
        private void CalculateBill(PatientBill bill)
        {
            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;

            if (bill.HasInsurance)
                bill.DiscountAmount = bill.GrossAmount * 0.10m;
            else
                bill.DiscountAmount = 0;

            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;
        }
        //method to print the calculation details
        private void PrintCalculation(PatientBill bill)
        {
            Console.WriteLine("Gross Amount: " + bill.GrossAmount);
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount);
            Console.WriteLine("Final Payable: " + bill.FinalPayable);
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
