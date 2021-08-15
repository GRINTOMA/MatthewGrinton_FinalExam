/*
 * Author Matthew Grinton
 * Final Exam
 * Last edited on: 04/20/2021
 */
using ConsoleTables;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MatthewGrinton_FinalExam
{
    class Program
    {
        //Creating private static variables to be used throughout the program,
        //3 adapters and datasets to handle the products and sales,
        //a boolean variable called check to determine if user input is valid,
        //a generic string to be used for the majority of user input,
        //two integer values, one which is initialized at 0, and the other which
        //is primarily used in association with the boolean check variable for
        //validating user input, lastly there are two string arrays one is the signup
        //menu and the other is the main menu.
        private static SqlDataAdapter adapter;
        private static SqlDataAdapter saleAdapter;
        private static SqlDataAdapter invoiceAdapter;
        private static DataSet ds;
        private static DataSet sds;
        private static DataSet ids;
        private static bool check;
        private static string userIn;
        private static int flag = 0;
        private static int x;
        private static string[] signUp = 
            {"Press 1: Re-enter login credentials",
            "Press 2: Sign up as a new user"};
        private static string[] menu = 
            {"Press 1: Edit Customer information\n",
            "Press 2: Buy Product\n",
            "Press 3: Exit Application\n"};
        //The main method calls the login method and handles the signoff message to the user.
        static void Main(string[] args)
        {
            Car c = new Car();
            Console.WriteLine(c);


            /*      Login();
                  Console.Clear();
                  Console.WriteLine("Thank you for using Matthew Grinton's Application");*/





            return;
        }
        //The Login() method is where the user will be prompted to provide login credentials
        //The user cannot leave until a successful login has occured.
        static void Login()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Matthew Grinton's Application");
            Console.WriteLine("Please provide login credentials: ");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            if(!LoginCheck(username, password))
            {
                Console.WriteLine("Username or Password incorrect!");
                foreach (string s in signUp)
                {
                    Console.WriteLine(s);
                }
                Console.Write(">");
                userIn = Console.ReadLine();
                check = int.TryParse(userIn, out x);
                if (check)
                {
                    switch (int.Parse(userIn))
                    {
                        //Within the switch case the user is choosing between
                        //Option 1: Re-entering login credentials
                        //Option 2: Signing up as a new user
                        case 1:
                            Login();
                            break;
                        case 2:
                            Console.WriteLine("You Will Need to Provide Some Data to Create Your Profile");
                            Console.WriteLine("What the customer's first name?");
                            Console.Write(">");
                            string fname = Console.ReadLine();
                            Console.WriteLine("What the customer's last name?");
                            Console.Write(">");
                            string lname = Console.ReadLine();
                            Console.WriteLine("What city is the customer from?");
                            Console.Write(">");
                            string city = Console.ReadLine();
                            //As the credit limit is the only thing that needs to be validated
                            //this do/while loop will force the user to enter a number that is valid within
                            //the constraints of the CreditCheck() method
                            do
                            {
                                while (flag == 0)
                                {
                                    Console.WriteLine("What the customer's credit limit?");
                                    Console.Write(">");
                                    string credit = Console.ReadLine();
                                    if (CreditCheck(credit))
                                    {
                                        Customer c = new Customer(fname, lname, city, decimal.Parse(credit));
                                        flag = 1;
                                        AddUser(c);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Credit limit must be a number.");
                                        Console.ReadLine();
                                        Console.Clear();
                                    }

                                    break;
                                }
                            } while (flag != 1);
                            //Resetting the global variable flag to be used elsewhere
                            flag = 0;
                            break;
                    }
                }
            }
            else
            {
                //Creating a Customer object to pass all of the customer's
                //Information from the Login() method to the LoggedIn() method
                Customer c = new Customer(username);
                GetUser(username, out c);
                LoggedIn(c);
                //Returning nothing so that once the user is done in LoggedIn()
                //It will then return back to main
                return;
            }
        }
        //The LoggedIn() method is where the user has already logged in and now can interact
        //with the menu options.
        static void LoggedIn(Customer c)
        {
            //Clearing the console at the start of this method does two things
            //First it will clear everything off the console from the Login() method
            //As well this will serve to keep the screen decluttered if the user decides
            //to continue using the program after having finished with one of the menu options
            Console.Clear();
            do
            {
                while (flag == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome " + c.cFirstName + "\n" +
                         "Please select an option from the menu");
                    foreach (string s in menu)
                    {
                        Console.WriteLine(s);
                    }
                    Console.Write(">");
                    userIn = Console.ReadLine();
                    check = int.TryParse(userIn, out x);
                    if (check)
                    {
                        //This switch case gives the user the options: 
                        //Option 1: Edit Customer Profile
                        //Option 2: Buy Product
                        //Option 3: Exit Application
                        switch (int.Parse(userIn))
                        {
                            //Editing the Customer Profile
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Edit Customer Profile");
                                Console.WriteLine("------------------------------------");
                                Console.WriteLine("Press 1: Edit City\n"+ 
                                    "Press 2: Edit Credit Limit\n" + 
                                    "Press 3: Back to Previous Menu\n");
                                Console.Write(">");
                                userIn = Console.ReadLine();
                                check = int.TryParse(userIn, out x);
                                if (check)
                                {
                                    //This switch case gives the user the options: 
                                    //Option 1: Edit Customer City
                                    //Option 2: Edit Customer Credit Limit
                                    //Option 3: Return to previous menu
                                    switch (int.Parse(userIn))
                                    {
                                        //Edit Customer City
                                        case 1:
                                            Console.Clear();
                                            Console.WriteLine("Edit City");
                                            //Even though the current customer is sent to the LoggedIn() method
                                            //GetUser() is called here to refresh the information in case the customer
                                            //uses this application more than once within this method
                                            GetUser(c.cLastName, out c);
                                            Console.WriteLine(c.cFirstName + "'s City is currently set to " + c.city);
                                            Console.WriteLine("------------------------------------");
                                            Console.WriteLine("What is the new city?");
                                            Console.Write(">");
                                            string city = Console.ReadLine();
                                            EditCity(c.cFirstName, city);
                                            Console.Clear();
                                            break;
                                        //Edit Customer Credit Limit
                                        case 2:
                                            Console.Clear();
                                            //Even though the current customer is sent to the LoggedIn() method
                                            //GetUser() is called here to refresh the information in case the customer
                                            //uses this application more than once within this method
                                            GetUser(c.cLastName, out c);
                                            do
                                            {
                                                while (flag == 0)
                                                {
                                                    Console.WriteLine("Edit Credit Limit");
                                                    Console.WriteLine(c.cFirstName + "'s Credit Limit is currently set to " + c.creditLimit);
                                                    Console.WriteLine("------------------------------------");
                                                    Console.WriteLine("What is the new credit limit?");
                                                    Console.Write(">");
                                                    string credit = Console.ReadLine();
                                                    if (CreditCheck(credit))
                                                    {
                                                        EditCreditLimit(c.cFirstName, credit);
                                                        Console.Clear();
                                                        flag = 1;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Credit limit must be a number.");
                                                        Console.ReadLine();
                                                        Console.Clear();
                                                    }

                                                    break;
                                                }
                                            } while (flag != 1);
                                            //Resetting flag
                                            flag = 0;
                                            break;
                                        //Return to previous menu
                                        case 3:
                                            break;
                                    }
                                    Console.Clear();
                                }
                                break;
                            //Buy Product
                            case 2:
                                //GetProducts() will fill the DataSet with the product table's information
                                GetProducts();
                                //Creating a view of only distinct product types
                                DataView view = new DataView(ds.Tables[0]);
                                DataTable distinctTypes = new DataTable();
                                distinctTypes = view.ToTable(true, "ProductType");
                                DataRow[] dr = distinctTypes.Select();
                                Console.Clear();
                                Console.WriteLine("Buy Products");
                                //Creating a ConsoleTable which will format the table into a more
                                //visually pleasing table that can be controlled using it's created methods
                                //Author of ConsoleTables is: khalidabuhakmeh
                                var table = new ConsoleTable("Product Type");
                                foreach (DataRow r in dr)
                                {
                                    table.AddRow(r["ProductType"].ToString());
                                }
                                //Using the alternative format to not show the number of rows at the bottom of the screen
                                table.Write(Format.Alternative);
                                Console.WriteLine("Please select product type");
                                Console.Write(">");
                                string type = Console.ReadLine();
                                Console.Clear();
                                //Refreshing the DataRow to specifically filter by the selected type
                                dr = ds.Tables[0].Select("ProductType = '" + type + "'");
                                //dr.Length != 0 will check to see if the DataRow is empty
                                if (dr.Length != 0)
                                {
                                    //Creating a new ConsoleTable using the headings specified
                                    var tableByType = new ConsoleTable("Product ID", "Description", "Product Type", "MSRP", "# On Hand");
                                    foreach (DataRow r in dr)
                                    {
                                        tableByType.AddRow(r["ProductNum"].ToString(),
                                            r["Descr"].ToString(),
                                            r["ProductType"].ToString(),
                                            r["MSRP"].ToString(),
                                            r["Onhand"].ToString());
                                    }
                                    tableByType.Write(Format.Alternative);
                                    Console.WriteLine("Please select a product by it's ID");
                                    Console.Write(">");
                                    string product = Console.ReadLine();
                                    Console.Clear();
                                    check = int.TryParse(product, out x);
                                    if (check)
                                    {
                                        //Once again refreshing the DataRow filtering the data to only include the product that has
                                        //the selected product type and product number
                                        dr = ds.Tables[0].Select("ProductNum = '" + int.Parse(product) + "' AND ProductType = '" + type + "'");
                                        //dr.Length != 0 will check to see if the DataRow is empty
                                        if (dr.Length != 0)
                                        {
                                            int qtyFlag = 0;
                                            Product p = new Product(int.Parse(product));
                                            do
                                            {
                                                while (qtyFlag == 0)
                                                {
                                                    //Creating a ConsoleTable with the individual product in it using the specified headers
                                                    var tableByProduct = new ConsoleTable("Product ID", "Description", "Product Type", "MSRP", "# On Hand");
                                                    foreach (DataRow r in dr)
                                                    {
                                                        tableByProduct.AddRow(r["ProductNum"].ToString(),
                                                            r["Descr"].ToString(),
                                                            r["ProductType"].ToString(),
                                                            r["MSRP"].ToString(),
                                                            r["Onhand"].ToString());
                                                        int pNum = int.Parse(r["ProductNum"].ToString());
                                                        string descr = r["Descr"].ToString();
                                                        string ptype = r["ProductType"].ToString();
                                                        decimal msrp = decimal.Parse(r["MSRP"].ToString());
                                                        int onHand = int.Parse(r["Onhand"].ToString());
                                                        p = new Product(pNum, descr, ptype, msrp, onHand);
                                                    }
                                                    tableByProduct.Write(Format.Alternative);
                                                    Console.WriteLine("Please specify a quantity for purchase?");
                                                    Console.Write(">");
                                                    string qty = Console.ReadLine();
                                                    check = int.TryParse(qty, out x);
                                                    if (check)
                                                    {
                                                        //Making sure the specified quantity is greater than 0
                                                        if (int.Parse(qty) > 0)
                                                        {
                                                            //If the quantity is greater than 0 the program then check
                                                            //to make sure it's lessthan or equal to the quantity of stock
                                                            //on hand
                                                            if (p.onHand >= int.Parse(qty))
                                                            {
                                                                //If both of these if statements are true it runs the AddSale() method
                                                                AddSale(c.cNumber, p, int.Parse(qty), out qtyFlag);
                                                                Console.Clear();
                                                            }
                                                            //This catches for there not being enough stock
                                                            else
                                                            {
                                                                Console.WriteLine("Not enough quantity on hand to fill sale, new quantity required\n" +
                                                                    "Please press ENTER to return to quantity input screen");
                                                                Console.ReadLine();
                                                                Console.Clear();

                                                            }
                                                        }
                                                        //This catches for if the entered quantity is less than or equal to 0
                                                        else
                                                        {
                                                            Console.WriteLine("Quantity must be greater than 0\n" +
                                                                "Please press ENTER to return to quantity input screen");
                                                            Console.ReadLine();
                                                            Console.Clear();
                                                        }
                                                    }
                                                    //This catches for the entered quantity not being a numeric value
                                                    else
                                                    {
                                                        Console.WriteLine("Quantity must be a number\n" +
                                                            "Please press ENTER to return to quantity input screen");
                                                        Console.ReadLine();
                                                        Console.Clear();
                                                    }
                                                }
                                            } while (qtyFlag != 1);
                                        }
                                        //This catches if the product id that was entered is either not on the list or not in the database
                                        else
                                        {
                                            Console.WriteLine("Product not available\n" +
                                                "Please press ENTER to return to product id selection");
                                            Console.ReadLine();
                                            Console.Clear();
                                        }
                                    }
                                    //This catches if the product id entered isn't a number
                                    else
                                    {
                                        Console.WriteLine("Product ID must be a number\n" +
                                            "Please press ENTER to return to product id selection");
                                        Console.ReadLine();
                                        Console.Clear();
                                    }
                                }
                                //This catches if the entered product type isn't in the database
                                else
                                {
                                    Console.WriteLine("Selected type not listed\n" +
                                        "Please press ENTER to return to product type selection");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                break;
                            //Return to previous menu
                            case 3:
                                return;
                        }
                    }
                }
            } while (flag != 0);
        }
        //This method verifys the login information entered
        //Will return true or false
        static bool LoginCheck(string uName, string pass)
        {
            string query = "SELECT * FROM customer_detail WHERE custusername = @username";
            string cs = GetConnectionString("LocalDB");
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@username", uName);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string username = (string)reader["CustUserName"];
                    string password = (string)reader["CustPassword"];

                    if (username != null && password != pass)
                    {
                        return false;
                    }
                    else if(username == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //This checks the entered credit limit to determine if it's a number
        static bool CreditCheck(string credit)
        {
            decimal d;
            if(decimal.TryParse(credit, out d))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //This will add a new user into the database to be able to login
        //Once completed this method then calls AddUserCredentials()
        static void AddUser(Customer c)
        {
            string query = "INSERT INTO customers (CustFirstName, CustLastName, City, CreditLimit) " +
                "VALUES(@fname, @lname, @city, @climit)";
            string cs = GetConnectionString("LocalDB");
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@fname", c.cFirstName);
                cmd.Parameters.AddWithValue("@lname", c.cLastName);
                cmd.Parameters.AddWithValue("@city", c.city);
                cmd.Parameters.AddWithValue("@climit", c.creditLimit);

                SqlDataReader reader = cmd.ExecuteReader();
                AddUserCredentials(c.cFirstName, c.cLastName);
            }
        }
        //Adds the new users username and newly generated password
        //This method calls GeneratePassword() to generate the new
        //user's password
        static void AddUserCredentials(string fname, string lname)
        {
            string password = GeneratePassword(fname, lname);
            string query = "INSERT INTO customer_detail (CustUserName, CustPassword)" +
                "VALUES(@uName, @pass)";
            string cs = GetConnectionString("LocalDB");
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@uName", lname);
                cmd.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = cmd.ExecuteReader();
            }
            Login();
        }
        //This will create a password for the new user based on the specified
        //password generation method in the assignment 
            //Even letters in firstname followed by the last three letters of the last name
        static string GeneratePassword(string fname, string lname)
        {
            string password;
            var firstHalf = string.Empty;
            if (lname.Length < 4)
            {
                if (fname.Length < 2)
                {
                    return password = fname + lname;
                }
                else
                {
                    for (int i = 0; i < fname.Length; i++)
                    {
                        if (i % 2 != 0)
                        {
                            firstHalf += fname[i];
                        }
                    }
                    return password = firstHalf + lname;
                }
            }
            else
            {
                var secondHalf = lname.Substring(lname.Length - 3);
                if (fname.Length < 2)
                {
                    return password = fname + secondHalf;
                }
                else
                {
                    for (int i = 0; i < fname.Length; i++)
                    {
                        if (i % 2 != 0)
                        {
                            firstHalf += fname[i];
                        }
                    }
                    return password = firstHalf + secondHalf;
                }
            }
        }
        //This method pulls the current user's information based on their last name
        static void GetUser(string lname, out Customer c)
        {
            c = new Customer(lname);
            string query = "SELECT * FROM customers WHERE CustLastName = @lname";
            string cs = GetConnectionString("LocalDB");
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@lname", lname);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int customerNumber = (int)reader["CustNum"];
                    string firstName = (string)reader["CustFirstName"];
                    string lastName = (string)reader["CustLastName"];
                    string custCity = (string)reader["City"];
                    decimal credit = (decimal)reader["CreditLimit"];
                    c = new Customer(customerNumber, firstName, lastName, custCity, credit);
                }
            }
        }
        //This method populates the Products DataSet and DataTable
        //This also creates a local primary key for the new DataSet and DataTable
        static void GetProducts()
        {
            ds = new DataSet();
            string query = "SELECT * FROM product";
            string cs = GetConnectionString("LocalDB");
            SqlConnection conn = new SqlConnection(cs);
            adapter = new SqlDataAdapter(query, conn);
            adapter.Fill(ds);
            DataTable tblProducts = ds.Tables[0];
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tblProducts.Columns[0];
            tblProducts.PrimaryKey = pk;
        }
        //This method populates the Sales DataSet and DataTable
        static void GetSales()
        {
            sds = new DataSet();
            string query = "SELECT * FROM sales";
            string cs = GetConnectionString("LocalDB");
            SqlConnection conn = new SqlConnection(cs);
            saleAdapter = new SqlDataAdapter(query, conn);
            saleAdapter.Fill(sds);
            DataTable tblSales = sds.Tables[0];
        }
        //This method populates the SalesInvoice DataSet and DataTable
        static void GetInvoices()
        {
            ids = new DataSet();
            string query = "SELECT * FROM salesinvoice";
            string cs = GetConnectionString("LocalDB");
            SqlConnection conn = new SqlConnection(cs);
            invoiceAdapter = new SqlDataAdapter(query, conn);
            invoiceAdapter.Fill(ids);
            DataTable tblInvoices = ids.Tables[0];
        }
        //Edits the current user's city in the database
        static void EditCity(string fname, string city)
        {
            string query = "UPDATE customers SET City = @city WHERE CustFirstName = @fname";
            string cs = GetConnectionString("LocalDB");
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@city", city);

                SqlDataReader reader = cmd.ExecuteReader();
            }
        }
        //Edits the current user's credit limit in the database
        //Also calls the CheckCredit() method
        static void EditCreditLimit(string fname, string limit)
        {
            if (CreditCheck(limit))
            {
                string query = "UPDATE customers SET CreditLimit = @limit WHERE CustFirstName = @fname";
                string cs = GetConnectionString("LocalDB");
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@limit", limit);

                    SqlDataReader reader = cmd.ExecuteReader();
                }
            }
        }
        //With the given information from the customer this will add a new row into the
        //sales database
        //Also calls the AddInvoice() method
        static void AddSale(int cNum, Product p, int qty,out int qtyFlag)
        {
            GetSales();

            DateTime d = DateTime.Now;
            int filled = 1;

            DataRow sdr = sds.Tables[0].NewRow();
            sdr[1] = d;
            sdr[2] = filled;
            sdr[3] = cNum;

            sds.Tables[0].Rows.Add(sdr);

            SqlCommandBuilder cmd = new SqlCommandBuilder(saleAdapter);
            saleAdapter.InsertCommand = cmd.GetInsertCommand();
            saleAdapter.Update(sds.Tables[0]);

            int salesNum = sds.Tables[0].Rows.Count;
            qtyFlag = 1;
            AddInvoice(p, qty, salesNum);
        }
        //With the given information from the customer this will add a new row into the
        //sales invoice database
        //Also calls the UpdateOnHand() method
        static void AddInvoice(Product p, int qty, int salesNum)
        {
            GetInvoices();

            DataRow dr = ids.Tables[0].NewRow();
            dr[0] = salesNum;
            dr[1] = p.productNumber;
            dr[2] = qty;
            dr[3] = p.msrp;

            ids.Tables[0].Rows.Add(dr);

            SqlCommandBuilder cmd = new SqlCommandBuilder(invoiceAdapter);
            invoiceAdapter.InsertCommand = cmd.GetInsertCommand();
            invoiceAdapter.Update(ids.Tables[0]);
            UpdateOnHand(p, qty);
        }
        //Once the sale and invoice have been created and updated into the databases
        //this will edit the total stock on hand for the selected product
        static void UpdateOnHand(Product p, int qty)
        {
            DataRow dr = ds.Tables[0].Rows.Find(p.productNumber);
            dr[4] = p.onHand - qty;

            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = cmd.GetUpdateCommand();
            adapter.Update(ds.Tables[0]);
        }
        //Pulls the connection string from the config.json file
        //Almost every method uses this to some degree which is why it wasn't
        //specified.
        static string GetConnectionString(string connectionString)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(System.IO.Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();

            return config["ConnectionStrings:"+ connectionString];
        }
    }
}