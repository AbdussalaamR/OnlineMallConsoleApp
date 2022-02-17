using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class CustomerRepository
    {
        private readonly ECommerceContext _context;

        public CustomerRepository(ECommerceContext context)
        {
            _context = context;
        }

        public void CustomerSignUp()
        {
            Console.WriteLine("WELCOME!");
            Console.Write("Enter FirstName: ");
            var FName = Console.ReadLine();
            Console.Write("Enter LastName: ");
            var LName = Console.ReadLine ();
            Console.Write("Enter Email: ");
            var email = Console.ReadLine();
            Console.Write("Enter Password (password should be minimum of 8 characters and unique): ");
            var passWord = Console.ReadLine();
            var customer = new Customer()
            {

                FirstName = FName,
                LastName = LName,
                Email = email,
                Password = passWord
            };
            Console.WriteLine($"Congratulations {FName}! Your account has been created successfully\n"+
            "Click other options to continue");
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        
        public Customer GetCustomer()
        {
            Console.WriteLine("Enter your email address");
            var emailad = Console.ReadLine();
            Console.WriteLine("Enter your password");
            var password = Console.ReadLine();
            var customer = _context.Customers.Include(c =>c.Orders).ThenInclude(o =>o.OrderProducts).ThenInclude(p => p.Product).Where(x => x.Email == emailad && x.Password == password).SingleOrDefault();
            return customer;
        }
        public void MyTransactionHistory(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                foreach (var item in customer.Orders)
                {
                    foreach (var item2 in item.OrderProducts)
                    { 
                        Console.WriteLine($"{item.TimeOfOrder}\t{item2.Product.Name}\t{item2.Quantity}\t{item2.Product.SellingPrice * item2.Quantity} ");
                    }
                }
            }
        }

        public void UpdateCustomer(Customer custom)
        {
            if (custom == null)
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
                custom.FirstName = FName;
                custom.LastName = LName;
                custom.Email = email;
                custom.Password = passWord;
                _context.Customers.Update(custom);
                _context.SaveChanges();
                Console.WriteLine("Details updated successfully");
            }
        }

        
        
    }
}