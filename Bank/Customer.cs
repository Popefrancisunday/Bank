using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Bank
{
    class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ushort Pin { get; set; }
        public uint AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Customer() // Class constructor
        {
            
        }
        public decimal Withdrawal() // Begining of Method for withdrawal
        {
            decimal InitialBalance = 2000;
            decimal Amount;
           
            Console.WriteLine("Enter Amount:");
            Amount = decimal.Parse(Console.ReadLine());
            Console.Clear();

            while(Amount>=InitialBalance)
            {
                Console.WriteLine("Invalid Transaction! You Can't Withdraw More than Your Balane");
                Console.WriteLine("Enter Amount:");
                Amount = decimal.Parse(Console.ReadLine());
                Console.Clear();
            }
                 Balance = InitialBalance - Amount;
            Console.WriteLine("{0} has been debited into your Account", Amount);
            return Balance;
        }  // Withdrawal Method ends here!

        public decimal Transfer()    // Method for making transfer begins here...
        {
            decimal Amount;
            decimal  InitialBalance=2000;
            Console.WriteLine("Choose Your prefered Bank: \n 1. Diamond Bank \n 2. First Bank");
            byte option = byte.Parse(Console.ReadLine());
            Console.Clear();
            switch (option)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Please Enter amount:");
                        Amount = decimal.Parse(Console.ReadLine());
                        Console.Clear();

                        while (Amount >= InitialBalance)
                        {
                            Console.WriteLine("Insuffcient Balance. ");
                            Console.WriteLine("Please Enter amount:");
                            Amount = decimal.Parse(Console.ReadLine());
                            Console.Clear();
                        }
                        if (Amount < InitialBalance)
                        {
                            Console.WriteLine("Enter Recipient's Name:");
                            string Recipients = Console.ReadLine();
                            Balance = InitialBalance - Amount;
                            Console.WriteLine("You have Transfered {0} To {1}", Amount, Recipients);
                        }
                    }
                    catch (Exception )
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("you entered a non-integer value! Please try again.");
                    }
                   
                  
                    break;
                case 2:
                    Console.WriteLine("Please Enter amount:");
                    Amount = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                    while(Amount >= InitialBalance)
                    {
                        Console.WriteLine("Insuffiient Balance. ");
                        Console.WriteLine("Please Enter amount:");
                        Amount = decimal.Parse(Console.ReadLine());
                        Console.Clear();
                    }
                        Console.WriteLine("Enter Recipient Name:");
                        string Recipient = Console.ReadLine();
                        Balance = InitialBalance - Amount;
                        Console.WriteLine("You have Transfered {0} To {1}", Amount, Recipient);
                    break;
                default:
                    Console.WriteLine("Please Choose a valid Option.");
                    break;
            }
            return Balance;
        }  // Transfere Method Ends here!

        public decimal Deposit ()   // Method for Making Deposit Begins Here
        {
            decimal Amount;
            decimal InitialBalance = 2000;
            Console.WriteLine("Enter Amount:");
            Amount = decimal.Parse(Console.ReadLine());
            Balance = InitialBalance + Amount;
            Console.WriteLine("Your Deposit of {0} Was Successful.", Amount);
            return Balance;
        }    // Deposit Method Ends here!

        public decimal Check_Balance ()  // Method for checking Account Balance
        {
            decimal InitialBalance =2000;
            decimal New_Balance = Balance + InitialBalance;
            return New_Balance;
        }     // Method for Checking of Account Balance Ends Here!

        public decimal Pay_Bills ()
        {
            decimal Amount;
            string Address;
            decimal InitialBalance = 2000;
            Console.WriteLine("Select Your Bill: \n 1. Water Bill \n 2. DSTV Subscription");
            byte option = byte.Parse(Console.ReadLine());
            Console.Clear();
            switch (option)
            {
                case 1:
                    Console.WriteLine("Please Enter Your Address:");
                     Address = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Please Enter Amount:");
                    Amount = decimal.Parse(Console.ReadLine());
                    Console.Clear();
                    while (Amount > InitialBalance)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Insufficient Balance.");
                        Console.ResetColor(); //Rstores the default console color
                        Console.WriteLine("Please Enter New Amount:");
                        Amount = decimal.Parse(Console.ReadLine());
                        Console.Clear();
                    }
                    Balance = InitialBalance - Amount;
                    Console.WriteLine("{0} Has been Deducted for Your Water Bill.",Amount);
                    break;
                case 2:
                    Console.WriteLine("Please Enter Your 4 Digits DSTV Code:");
                    ushort code = ushort.Parse(Console.ReadLine());
                    Console.Clear();
                    while (code>=9999)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\t Invalid DSTV Code! Please Check Your Code Properly.");
                        Console.ResetColor();

                        Console.WriteLine("Please Enter Your 4 Digits DSTV Code:");
                        code = ushort.Parse(Console.ReadLine());
                        Console.Clear();
                    }
                    if (code < 9999)
                    {
                        Console.WriteLine("Please Enter Amount:");
                        Amount = decimal.Parse(Console.ReadLine());
                        while (Amount > InitialBalance) // Loop to ensure customers don't withdraw more than their initial balance
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Insufficient Balance.");
                            Console.ResetColor();
                            Console.WriteLine("Please Enter New Amount:");
                            Amount = decimal.Parse(Console.ReadLine());
                            Console.Clear();
                        }
                        Balance = InitialBalance - Amount;
                        Console.WriteLine("{0} Has been Deducted for Your DSTV Subscription.",Amount);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
            return Balance;
        }

        public int ChangePin()
        {
            int result;
            ushort Cpin;
            ushort oldPin;
            Console.WriteLine("Enter Old Pin:");
            oldPin = ushort.Parse(Console.ReadLine());
            Console.Clear();
            while (oldPin == Bank_Admin.defaultPin)
            {
                Console.WriteLine("Enter Old Pin:");
                oldPin = ushort.Parse(Console.ReadLine());
                Console.Clear();
            }
            Console.WriteLine("Enter New Pin:");
            Cpin = ushort.Parse(Console.ReadLine());
            Console.Clear();
            
            string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                string query="update table customer set Pin=Cpin";
                SqlCommand change_pin = new SqlCommand(query, connect);
                result = change_pin.ExecuteNonQuery();
                connect.Close();
            }
            return result;
        }

        public List<string> getFromDataBase()
        {
            List<string> result = new List<string>();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            string query = "select Name from customer;";
            try
            {
                con.Open();
                // DataTable tap = new DataTable();
                // new SqlDataAdapter(query, con).Fill(tap);
                //result = tap.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("Name")).ToList();
                SqlCommand command = new SqlCommand(query, con);
                SqlDataReader List = command.ExecuteReader();
                while (List.Read() ==true)
                {
                    result.Add(List.ToString());
                    Console.WriteLine(result);
                }
            }
            catch (Exception)
            {
                //Exception   
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return result;
        }
    }

}
