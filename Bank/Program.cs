using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;
using System.Data;
using System.Collections;

namespace Bank
{

    class Program
    {

        static void Main(string[] args)
        {
            decimal Balance;
            Customer Newcustomer = new Customer();
            Bank_Admin admin = new Bank_Admin("john", "okoh", 001);
            Bank_Admin Manager = new Bank_Admin("John", "Smith", 101);


            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
            //using (SqlConnection connect = new SqlConnection(connectionString))
            //{
            //    connect.Open();
            //    string query = "select AccountNum from customer";
            //    SqlCommand command = new SqlCommand(query, connect);
            //    command.CommandType = CommandType.Text;
            //    command.CommandText = query;

            //    SqlDataAdapter myAdapter = new SqlDataAdapter();
            //    myAdapter.SelectCommand = command;

            //    DataSet myDataset = new DataSet();
            //    myAdapter.Fill(myDataset);

            //    ArrayList myArray = new ArrayList();
            //    foreach (DataRow dtrow in myDataset.Tables[0].Rows)
            //    {
            //        myArray.Add(dtrow);
            //    }
            //    connect.Close();
            //}

            List<string> be = Newcustomer.getFromDataBase().ToList();
    
                  

            Console.ForegroundColor = ConsoleColor.Blue;
              Console.WriteLine("*****************************************\n*\t\t\t\t\t*\n*\t  Welcome to Diamond Bank \t*\n*\t\t\t\t\t*\n*****************************************");
              Console.ResetColor();
              Console.WriteLine("Choose an Option: \n 1. Login as Staff. \n 2. Login as Customer");
            int b = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (b)
            {
                case 1:
                    Console.WriteLine("Choose option: \n 1. login as Manager \n 2. login as Sales Staff.");
                     ushort choic = ushort.Parse(Console.ReadLine());
                    Console.Clear();
                    if (choic == 1)
                    {
                        Console.WriteLine("Please enter User Name:");
                        string Userinput = Console.ReadLine();
                        Console.Clear();
                        string Username = Manager.FirstName + " " + Manager.LastName;
                        while (Userinput != Username)
                        {
                            Console.WriteLine("Please enter User Name:");
                            Userinput = Console.ReadLine();
                            Console.Clear();
                        }
                        Console.WriteLine("Choose your prefered Operation:\n 1. Register New staff \n 2. View Staff Records \n 3. View Customers");
                        int task = int.Parse(Console.ReadLine());
                        Console.Clear();
                        switch (task)
                        {
                            case 1:
                                string NewStaff = Manager.Add_New_Staff();
                                Console.WriteLine(NewStaff);
                                break;
                            case 2:
                                  string Staff = Manager.View_Staff();
                                  Console.WriteLine(Staff);
                                break;
                            case 3:
                                string CustomersDetail = Manager.Check_Customers_Detail();
                                Console.WriteLine(CustomersDetail);
                                break;
                            default:
                                Console.WriteLine("Wrong Choice selection.");
                                break;
                        }
                    }
                    else
                    {


                        Console.WriteLine("Please enter Your Full Name:");
                        string admName = Console.ReadLine();
                        Console.Clear();
                        string fullName = admin.FirstName + " " + admin.LastName;
                        Console.WriteLine("Please Enter Your Id:");
                        ushort admId = ushort.Parse(Console.ReadLine());
                        Console.Clear();
                        while (admName != fullName)
                        {
                            Console.WriteLine("Please enter Your Full Name:");
                            admName = Console.ReadLine();

                            Console.Clear();
                            fullName = admin.FirstName + " " + admin.LastName;
                            Console.WriteLine("Please Enter Your Id:");
                            admId = ushort.Parse(Console.ReadLine());
                            Console.Clear();
                        }
                        if (admName == fullName)
                        {
                            Console.WriteLine("Verified.");
                            Console.WriteLine("Welcome {0}", fullName);
                        }
                        Console.WriteLine("\n Select Your prefered Operation: \n 1. Register New Customer. \n 2. View Customers Detail. \n 3. View customer Detail.");
                        ushort option;
                        option = ushort.Parse(Console.ReadLine());
                        Console.Clear();
                        //try
                        //{
                        //    option = ushort.Parse(Console.ReadLine());

                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex);
                        //    Console.WriteLine("You Must Enter an Integer Value.");
                        //}


                        while (option > 3)
                        {
                            Console.WriteLine("Select Your prefered Operation: \n 1. Register New Customer. \n 2. View Customers Detail.\n 3. View a Customer Detail.");
                            option = ushort.Parse(Console.ReadLine());
                            Console.Clear();
                        }
                        switch (option)
                        {
                            case 1:
                                string register = admin.RegisterCustomer();
                                Console.WriteLine(register);
                                break;
                            case 2:
                                string View = admin.Check_Customers_Detail();
                                Console.WriteLine(View);
                                break;
                            case 3:
                                string views = admin.Details();
                                Console.WriteLine(views);
                                break;
                            default:
                                Console.WriteLine("Wrong Option. Please try Again.");
                                break;
                        }
                    }

                    break;
                case 2:

                    Console.WriteLine("Enter Your Account Number:");// this code validates user input with database data.
                    int numb = Convert.ToInt32(Console.ReadLine());
                    string query = "select Name from customer where AccountNum = @AccountNum";
                    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pope francis ogbonna\Documents\Visual Studio 2015\Projects\Bank\Bank\Diamond.mdf;Integrated Security=True";
                    using (SqlConnection connect = new SqlConnection(connectionString))
                    {
                        connect.Open();
                        SqlCommand command = new SqlCommand(query, connect);
                        command.Parameters.Add("AccountNum", SqlDbType.Int).Value = numb;
                        using (SqlDataReader show = command.ExecuteReader())
                        {
                            if (show.Read())
                            {
                                Console.WriteLine("Welcome To Diamond Bank  " + show[0]);
                            }
                            //while (!show.Read())
                            //{
                            //    Console.WriteLine("Enter Your Account Number:");
                            //    numb = Convert.ToInt32(Console.ReadLine());
                            //    command = new SqlCommand(query, connect);
                            //    command.Parameters.Add("AccountNum", SqlDbType.Int).Value = numb;
                            //    if (show.Read())
                            //    {
                            //        Console.WriteLine("Welcome To Diamond Bank  " + show[0]);
                            //    }
                            //}
                            else
                            {
                                Console.WriteLine("wrong Account Number");
                            }
                            connect.Close();
                        }
                    } // End of database connection for validating user input data.

                    //Console.WriteLine("Please enter your Account Name");
                   // string Name= Console.ReadLine();
                    //Console.Clear(); // Clears the console for next user input

                    //Console.WriteLine("please enter Your Account Number");
                    //uint accNumber = uint.Parse(Console.ReadLine());
                    //Console.Clear();

                    Console.WriteLine("Enter your Pin");
                    ushort p = ushort.Parse(Console.ReadLine());
                    Console.Clear();

                    //string newcustomer = Newcustomer.FirstName + " " + Newcustomer.LastName;
                    //while (Name != newcustomer || p != Newcustomer.Pin) //Loop for account verification.
                    //{
                    //    Console.WriteLine("Please enter your Account Name");
                    //    Name = Console.ReadLine();
                    //    Console.Clear();
                    //    Console.WriteLine("please enter Your Account Number");
                    //    accNumber = uint.Parse(Console.ReadLine());
                    //    Console.Clear();
                    //    Console.WriteLine("Enter your Pin");
                    //    p = ushort.Parse(Console.ReadLine());
                    //    Console.Clear();
                    //}

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\t\t Verified.");
                    Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\t\t Welcome to Diamond Bank.");
                    Console.ResetColor();
                    Console.WriteLine("\n\nChoose Your Prefered Operation: \n 1. Make Withdrawal \n 2. Check Account Balance \n 3. Transfer \n 4. Pay Bills \n 5. Deposit \n 6. Change Pin");
                    short choice = short.Parse(Console.ReadLine());
                    Console.Clear();
                    while (choice > 6)
                    {
                        Console.WriteLine("You Must Choose a valid Operation");
                        Console.WriteLine("\n Choose Your Prefered Operation: \n 1. Make Withdrawal \n 2. Check Account Balance \n 3. Transfer \n 4. Pay Bills \n 5. Deposit \n 6. Change Pin");
                        choice = short.Parse(Console.ReadLine());
                        Console.Clear();
                    }
                    switch (choice)
                    {
                        case 1:
                            Balance = Newcustomer.Withdrawal();
                            Console.WriteLine("your current balance is {0}", Balance);
                            break;
                        case 2:
                            Balance = Newcustomer.Check_Balance();
                            Console.WriteLine("your current balance is {0}", Balance);
                            break;
                        case 3:
                            Balance = Newcustomer.Transfer();
                            Console.WriteLine("your current balance is {0}", Balance);
                            break;
                        case 4:
                            Balance = Newcustomer.Pay_Bills();
                            Console.WriteLine("your current balance is {0}", Balance);
                            break;
                        case 5:
                            Balance = Newcustomer.Deposit();
                            break;
                        case 6:
                            int newPin = Newcustomer.ChangePin();
                            Console.WriteLine("Your Pin has been Changed Successfully.");
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Please Enter a Valid Option.");
                    break;
            }

            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t \t\n Thanks For Banking With Us!");
            
            Console.ReadLine();
        }
    }
}
