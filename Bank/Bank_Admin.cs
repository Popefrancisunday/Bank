using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;

namespace Bank
{
    class Bank_Admin
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ushort Id { get; set; }
      public  const ushort defaultPin = 0000;
        public Bank_Admin(string Fname, string Lname, ushort id)
        {
            this.FirstName = Fname;
            this.LastName = Lname;
            this.Id = id;
        }
       
        public string RegisterCustomer() // Method for registering New customer Starts here!!
        {

            string cFirstName;
            string cLastName;
            string AccountOfficer;
            decimal InitialBalance;
            long AccountNumber;
            string AccountType;
            int result;
            string Reg = "Registration Successful.";

            Console.WriteLine("Please Enter Customer First Name:");
            cFirstName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please Enter Customer Last Name:");
            cLastName = Console.ReadLine();
            Console.Clear();
            string Name = cFirstName + " " + cLastName;
            Console.WriteLine("Enter Account Number:");
            AccountNumber = Convert.ToInt64(Console.ReadLine());
            Console.Clear();
            while (AccountNumber > 9999999999)
            {
                Console.WriteLine("Enter Account Number:");
                AccountNumber = Convert.ToInt64(Console.ReadLine());
                Console.Clear();
            }

            Console.WriteLine("Enter Account Type:");
            AccountType = Console.ReadLine();
            Console.Clear();       
            Console.WriteLine("Enter Opening Balance:");
            InitialBalance = decimal.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Enter Account Officer:");
            AccountOfficer = Console.ReadLine();
            Console.Clear();

            try     // Connectiong to the database...
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
                 using (SqlConnection connect = new SqlConnection(connectionString))
               {
                    // using key word ensures that the database is disposed properly to aviod sql injection attack.
                    connect.Open();
                    // Sql insert query for registering a new customer
                    string query = "insert into customer(Name,AccountNum,AccountType,InitialBalance,AccountOfficer,Pin) values('" + Name + "'," + AccountNumber + ",'" + AccountType + "'," + InitialBalance + ",'" + AccountOfficer + "'," + defaultPin + ")";

                    SqlCommand command = new SqlCommand(query, connect);
                    result = command.ExecuteNonQuery();
                    Console.WriteLine(result + "Record/s inserted into Customer Table.");
                    connect.Close();
               }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            return Reg; 
        }  // Customer Registeration Method Ends here.

        public string Check_Customers_Detail() // Method to Check Customers Details By the Manager
        {                                      // Manager can only see whole customer details.
            string b = "\n\n\t Customers details...";
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\documents\visual studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    string query = "select SN,Name,AccountNum,AccountType,InitialBalance,AccountOfficer from customer";
                    SqlCommand command = new SqlCommand(query, connect);
                    
                    SqlDataReader display = command.ExecuteReader();
                    while (display.Read() == true)
                    {
                        int sn = display.GetInt32(0);
                        string name = display.GetString(1);
                        int AcNum = display.GetInt32(2);
                        string AcType = display.GetString(3);
                        decimal InBal = display.GetDecimal(4);
                       //  decimal Bal = display.GetDecimal(5);
                        string AcOfficer = display.GetString(5);
                       // int pin = display.GetInt16(7);

                           Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t {5}", sn, name, AcNum, AcType, InBal, AcOfficer);
                        }

                    }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Blue;
            return b;
        } //End of customers details viewed by the Manager...
        public string Add_New_Staff()  // Employment method for the manager alone
        {                              // This Method Add new sales Staff to the company.
            string b = "Sucessfully";
            string fName;
            string LName;
            string fullName;
            int id;
            Console.WriteLine("Enter First Name:");
            fName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter Last Name:");
            LName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter Staff ID");
            id = int.Parse(Console.ReadLine());
            Console.Clear();
            fullName = fName + " " + LName;
            string query = "insert into Staff(Name,CompanyId) values('" + fullName + "',"+ id +")";
            string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlCommand command = new SqlCommand(query, connect);
               int result= command.ExecuteNonQuery();
                Console.WriteLine(result + " Staff Added to the Company.");

                connect.Close();
            }
            return b;
        }    // End of Employment method.
        public string View_Staff()  // Method to view Banks Staff
        {                           // Method can only be accessed by Manager
            string staff = "\n\t Diamond Bank Staff Details.";
            try
            {
                string query = @"select * from Staff";
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(query, connect);
                    SqlDataReader show = command.ExecuteReader();
                    while (show.Read() == true)
                    {
                        int sn = show.GetInt32(0);
                        string name = show.GetString(1);
                        int id = show.GetInt32(2);
                        DateTime date = show.GetDateTime(3);

                        Console.WriteLine(" {0}\t {1}\t {2}\t {3}\t", sn, name, id, date);
                    }
                    connect.Close();
                }
            }
            catch ( Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return staff;
        }   // End of Bank Staff details ...
        public string Details() // Method for viewing individual customer details
        {                       // Operated by Sales staff only
            string name;
            Console.WriteLine("Enter Customer Account Name:");
            name = Console.ReadLine();
            Console.Clear();
            string b = name + "'s detail.";
            string query= "select SN, Name, AccountNum, AccountType, InitialBalance, AccountOfficer from customer where Name='"+name+"'";
            string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(query, connect);
                    SqlDataReader display = command.ExecuteReader();
                    while (display.Read() == true)
                    {
                        int sn = display.GetInt32(0);
                        string Name = display.GetString(1);
                        int AcNum = display.GetInt32(2);
                        string AcType = display.GetString(3);
                        decimal InBal = display.GetDecimal(4);
                        //  decimal Bal = display.GetDecimal(5);
                        string AcOfficer = display.GetString(5);
                        // int pin = display.GetInt16(7);

                        Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}\t {5}", sn, Name, AcNum, AcType, InBal, AcOfficer);

                    }

                    connect.Close();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            return b;
        }   // End of Method for viewing individual customers details.
    }
}
