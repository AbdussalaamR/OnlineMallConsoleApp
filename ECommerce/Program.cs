using System;

namespace ECommerce
{
    class Program
    {
        static void Main()
        {
            ECommerceContext context = new ECommerceContext();
            AdminRepository admin = new AdminRepository(context);
            ProductRepository product = new ProductRepository(context, admin);
            CustomerRepository customerRepository = new CustomerRepository(context);
            OrderRepository orderRepository = new OrderRepository(context, product, customerRepository);
            // admin.CreateAdmin();
            //admin.PrintAllTransactions();
            // product.UpdateStock();
            // product.CreateProduct();
            // product.RemoveProduct();
            // product.GetAllProducts();
            // customerRepository.CustomerSignUp();
            // customerRepository.MyTransactionHistory();
            // orderRepository.Purchase();

            Console.WriteLine("*****ARICOASTER E-STORE*****");
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("1. Customer Menu\n" +
                              "2. Staff Menu\n" +
                              "3. Log out");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    FirstCustomerMenu();
                    Main();
                    break;
                case 2:
                    FirstStaffMenu();
                    Main();
                    break;
                case 3:
                    Console.WriteLine("Thanks for checking by. See you again, bye!");
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Main();
                    break;
            }

        }

        public static void FirstCustomerMenu()
        {
            ECommerceContext context = new ECommerceContext();
            AdminRepository admin = new AdminRepository(context);
            ProductRepository product = new ProductRepository(context, admin);
            CustomerRepository customerRepository = new CustomerRepository(context);
            OrderRepository orderRepository = new OrderRepository(context, product, customerRepository);

            Console.WriteLine("Welcome!");
            Console.WriteLine("CUSTOMER MENU");
            Console.WriteLine("1. Sign up\n" +
                              "2. Browse Products\n" +
                              "3. Log in\n" +
                              "4. Back");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    customerRepository.CustomerSignUp();
                    FirstCustomerMenu();
                    break;
                case 2:
                    product.GetProducts();
                    FirstCustomerMenu();
                    break;
                case 3:
                    var customer = customerRepository.GetCustomer();
                    CustomerMenu(customer);
                    break;
                case 4:
                    Main();
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Main();
                    break;
            }
        }

        public static void CustomerMenu(Customer customer)
        {
            ECommerceContext context = new ECommerceContext();
            AdminRepository admin = new AdminRepository(context);
            ProductRepository product = new ProductRepository(context, admin);
            CustomerRepository customerRepository = new CustomerRepository(context);
            OrderRepository orderRepository = new OrderRepository(context, product, customerRepository);

            Console.WriteLine($"Welcome {customer.FirstName}!");
            Console.WriteLine("MENU");
            Console.WriteLine("1. Make Purchase\n" + 
                              "2. My Transaction History\n" +
                              "3. Update Details\n" +
                              "4. Back");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    orderRepository.Purchase(customer);
                    CustomerMenu(customer);
                    break;
                case 2:
                    customerRepository.MyTransactionHistory(customer);
                    CustomerMenu(customer);
                    break;
                case 3:
                    customerRepository.UpdateCustomer(customer);
                    break;
                case 4:
                    FirstCustomerMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Main();
                    break;


            }
        }

        public static void FirstStaffMenu()
        {
            ECommerceContext context = new ECommerceContext();
            AdminRepository admin = new AdminRepository(context);
            ProductRepository product = new ProductRepository(context, admin);
            CustomerRepository customerRepository = new CustomerRepository(context);
            OrderRepository orderRepository = new OrderRepository(context, product, customerRepository);

            Console.WriteLine("Welcome!");
            Console.WriteLine("CUSTOMER MENU");
            Console.WriteLine("1. Sign up\n" +
                              "2. Log in\n" +
                              "3. Back");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    admin.CreateAdmin();
                    FirstStaffMenu();
                    break;
                case 2:
                    var staff = admin.GetAdmin();
                    StaffMenu(staff);
                    break;
                case 3:
                    Main();
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Main();
                    break;
            }
        }
        public static void StaffMenu(Admin staff)
        {
            ECommerceContext context = new ECommerceContext();
            AdminRepository admin = new AdminRepository(context);
            ProductRepository product = new ProductRepository(context, admin);
            CustomerRepository customerRepository = new CustomerRepository(context);
            Console.WriteLine($"Welcome {staff.FirstName}!");
            Console.WriteLine("MENU");
            Console.WriteLine("1. Add Stock\n" +
                              "2. Update Stock\n" +
                              "3. Transaction History\n" +
                              "4. Update Details\n" +
                              "5. Sales/Profit Records\n" +
                              "6. Get Available products\n" +
                              "7. View All customers\n" +
                              "8. Delete order record\n" +
                              "9. Back");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    product.CreateProduct(staff);
                    StaffMenu(staff);
                    break;
                case 2:
                    product.UpdateStock(staff);
                    StaffMenu(staff);
                    break;
                case 3:
                    admin.AllTransactions();
                    StaffMenu(staff);
                    break;
                case 4:
                    admin.UpdateAdmin(staff);
                    StaffMenu(staff);
                    break;
                case 5:
                    admin.Ledger();
                    StaffMenu(staff);
                    break;
                case 6:
                    product.GetAllProducts();
                    StaffMenu(staff);
                    break;
                case 7:
                    admin.GetAllCustomers(staff);
                    StaffMenu(staff);
                    break;
                case 8:
                    admin.DeleteOrder(staff);
                    StaffMenu(staff);
                    break;
                case 9:
                    FirstStaffMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Main();
                    break;
            }
        }
    }
}