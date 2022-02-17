using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class AdminRepository
    {
        private readonly ECommerceContext _context;

        public AdminRepository(ECommerceContext context)
        {
            _context = context;
        }
        
        public bool CreateAdmin()
        {
            Console.Write("Enter FirstName: ");
            var FName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            var LName = Console.ReadLine ();
            Console.Write("Enter Email: ");
            var email = Console.ReadLine();
            Console.Write("Enter Password (password should be minimum of 8 characters and unique)");
            var passWord = Console.ReadLine();
            var admin = new Admin
            {

                FirstName = FName,
                LastName = LName,
                Email = email,
                Password = passWord
            };
            Console.Write($"Congratulations {FName}! Your account has been created successfully");
            _context.Admin.Add(admin);
            _context.SaveChanges();
            return true;
        }

        public Admin GetAdmin()
        {
            Console.WriteLine("Enter your email address");
            var emailad = Console.ReadLine();
            Console.WriteLine("Enter your password");
            var password = Console.ReadLine();
            // var admin1 = from p in _context.Admin where p.Email == emailad && p.Password == password select p;
            var admin = _context.Admin.Where(x => x.Email == emailad && x.Password == password).SingleOrDefault();
            return admin;
        }
        public void GetAllAdmin()
        {
            var admin = _context.Admin.ToList();
                foreach (var item in admin)
                {
                    Console.WriteLine($"{item.Id}\t{item.FirstName} {item.LastName} {item.Email}");
                }
        }
        
        public void UpdateAdmin(Admin admin)
        {
            if (admin == null)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                Console.Write("Enter new firstName: ");
                var FName = Console.ReadLine();
                Console.Write("Enter new lastName: ");
                var LName = Console.ReadLine ();
                Console.Write("Enter Email: ");
                var email = Console.ReadLine();
                Console.Write("Enter Password (password should be minimum of 8 characters and unique): ");
                var passWord = Console.ReadLine();
                admin.FirstName = FName;
                admin.LastName = LName;
                admin.Email = email;
                admin.Password = passWord;
                _context.Admin.Update(admin);
                _context.SaveChanges();
                Console.WriteLine("Details updated successfully");
            }
        }
        public void RemoveAdmin()
        {
            Console.Write("Enter Id: ");
            int id = int.Parse(Console.ReadLine());
            var admin = _context.Admin.Find(id);
            if (admin == null)
            {
                Console.WriteLine("Enter correct input for Id"); 
            }
            else
            {
                _context.Admin.Remove(admin);
                _context.SaveChanges();
                Console.WriteLine("Account deleted successfuly");
            }
        }
        public void GetAllCustomers(Admin admin)
        {
            if (admin == null)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                var customers = _context.Customers.ToList();
                foreach (var item in customers)
                {
                    Console.WriteLine($"{item.Id}\t{item.FirstName} {item.LastName} {item.Email}");
                }
            }
        }
        public void DeleteCustomer()
        {
            var admin = GetAdmin();
            if (admin == null)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                Console.Write("Enter Id: ");
                int id = int.Parse(Console.ReadLine());
                var custom = _context.Customers.Find(id);
                if (custom == null)
                {
                    Console.WriteLine("Enter correct input for Id");
                }
                else
                {
                    _context.Customers.Remove(custom);
                    _context.SaveChanges();
                    Console.WriteLine("Customer removed successfuly");
                }
            }
        }
        
        public void Ledger()
        {
            var order = _context.Orders.Include(c => c.OrderProducts).ThenInclude(d => d.Product).ToList();
            double totalSales = 0;
            double totalProfit = 0;
            foreach (var item in order)
            {
                foreach (var item2 in item.OrderProducts)
                {
                    var amount = item2.Product.SellingPrice * item2.Quantity;
                    var profit = (item2.Product.SellingPrice - item2.Product.CostPrice) * item2.Quantity;
                    totalSales += amount;
                    totalProfit += profit;
                    
                    Console.WriteLine($"{item.Id}\t{item.TimeOfOrder}\t{item2.Product.Name}\t{item2.Quantity}\t{amount}\t{profit}");
                }
                
            }
            Console.WriteLine($"Total Sales: {totalSales}");
            Console.WriteLine($"Total Profit: {totalProfit}");
        }
        public void AllTransactions()
        {
            var order = _context.Orders.Include(c => c.OrderProducts).ThenInclude(d => d.Product).ToList();
            Console.WriteLine("Id\tTimeOfOrder\t\tProduct Name\t\tQuantity\t{amount}");
            foreach (var item in order)
            {
                foreach (var item2 in item.OrderProducts)
                {
                    var amount = item2.Product.SellingPrice * item2.Quantity;

                    Console.WriteLine($"{item.Id}\t{item.TimeOfOrder}\t{item2.Product.Name}\t{item2.Quantity}\t{amount}");
                }
            }
        }
        
        public void DeleteOrder(Admin admin)
        {
            if (admin == null)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                Console.Write("Enter  order Id: ");
                int id = int.Parse(Console.ReadLine());
                var order = _context.Orders.Find(id);
                if (order == null)
                {
                    Console.WriteLine("Enter correct input for Id");
                }
                else
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    Console.WriteLine("Order removed successfuly");
                }
            }
        }
    }
}