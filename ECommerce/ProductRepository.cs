using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce
{
    public class ProductRepository
    {
        private readonly ECommerceContext _context;
        private readonly AdminRepository _adminRepository;

        public ProductRepository(ECommerceContext context, AdminRepository adminRepository)
        {
            _context = context;
            _adminRepository = adminRepository;
        }

        public void CreateProduct(Admin admin)
        {
            
            if (admin == null)
            {
               Console.WriteLine("No matches found");
            }
            else
            {
                Console.WriteLine("Enter the name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter the cost price");
                double costPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the selling  price");
                double sellingPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the AdminId");
                int adminId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter quantity of products");
                int qty = int.Parse(Console.ReadLine());
                var product = new Product
                {
                    Name = name,
                    CostPrice = costPrice,
                    SellingPrice = sellingPrice,
                    AdminId = adminId,
                    Quantity = qty
                };
                _context.Products.Add(product);
                _context.SaveChanges();
                Console.WriteLine("Products added successfully");

            }
            
        }
        
        public void GetAllProducts()
        {
            var products = _context.Products.ToList();
            Console.WriteLine("Id\tName\tQuantity\tSelling Price");
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Id}\t{item.Name}\t{item.Quantity}\t\t{item.SellingPrice}");
            }
        }
        public void GetProducts()
        {
            var products = _context.Products.ToList();
            Console.WriteLine("Id\tName\tPrice");
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Id}\t{item.Name}\t{item.SellingPrice}");
            }
        }

        public List<Product> GetSelectedProducts(Dictionary<int, int> goods)
        {
            foreach (var item in goods)
            {
               // var  a = from p in _context.Products  where
            }
            var products = _context.Products.Where(x => goods.ContainsKey(x.Id)).ToList();
            return products;
        }

        public void UpdateStock(Admin admin)
        {
            if (admin == null)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                Console.WriteLine("Enter the Id of product to update");
                var prodId = int.Parse(Console.ReadLine());
                var product = _context.Products.Find(prodId);
                if (product == null)
                {
                    Console.WriteLine("No matches found! Enter correct input for Id"); 
                }
                else
                {
                    Console.WriteLine("Enter the new cost price");
                    double costPrice = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new selling  price");
                    double sellingPrice = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter quantity of products");
                    int qty = int.Parse(Console.ReadLine());
                    product.CostPrice = costPrice;
                    product.SellingPrice = sellingPrice;
                    product.Quantity = qty;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    Console.WriteLine("Product updated successfully");
                }
            }
        }

        public void RemoveProduct()
        {
            Console.Write("Enter product Id: ");
            int id = int.Parse(Console.ReadLine());
            var prod = _context.Products.Find(id);
            if (prod == null)
            {
                Console.WriteLine("Enter correct input for Id"); 
            }
            else
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
                Console.WriteLine("Product removed successfuly");
            }
        }
    }
} 