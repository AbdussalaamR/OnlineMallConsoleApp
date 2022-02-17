using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce
{
    public class OrderRepository
    {
        private readonly ECommerceContext _context;
        private readonly ProductRepository _productRepository;
        private readonly CustomerRepository _customerRepository;

        public OrderRepository(ECommerceContext context, ProductRepository productRepository, CustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        
        public void Purchase(Customer customer)
        {
            bool response = true;
            var goods = new Dictionary<int, int>();
            while (response == true)
            {
                Console.WriteLine("Enter id of product you wish to buy");
                   int id = int.Parse(Console.ReadLine());
                var a = (from p in _context.Products.Where(l => l.Id == id) select p).SingleOrDefault() ;
                if (a == null)
                {
                Console.WriteLine("Product not found, enter correct product Id");
                return;
                }
                if (a != null)
                {
                    Console.WriteLine("Enter quantity of the product you wish to buy");
                    int qty = int.Parse(Console.ReadLine());
                    if (a.Quantity < qty)
                    {
                       Console.WriteLine($"Sorry, we presently do not have up to that quantity.\n"+
                                         $"Enter values equal to or less than {a.Quantity}");
                       qty = int.Parse(Console.ReadLine());
                    }
                    else if (a.Quantity >= qty)
                    {
                        if (goods.ContainsKey(id))
                        {
                            goods[id] = goods[id] + qty;
                        }
                        else
                        {
                            goods.Add(id, qty);
                            a.Quantity -= qty;   
                        }
                        
                    }
                }
                Console.WriteLine("Enter Y to select more products or N to stop");
                string answer = Console.ReadLine().ToLower();
                if (answer == "n")
                {
                    response = false;
                }
            }
            double totalCost = 0;
                
                Console.WriteLine("Goods in cart:");
                foreach (var item in goods)
                {
                    double cost = 0;
                    var prod = _context.Products.Find(item.Key);
                    cost = (prod.SellingPrice) * (item.Value);
                    totalCost += cost;
                    Console.WriteLine($"{prod.Name}\t{item.Value}\t{cost}");
                }
                Console.WriteLine($"Total cost of goods: {totalCost}");
                Console.WriteLine($"Proceed to payment?\n"+
                                  "1. Yes\n"+
                                  "2. No");
                var ans = Convert.ToInt32(Console.ReadLine());
                if (ans == 1)
                {
                    var order = new Order
                    {
                        CustomerId = customer.Id,
                        TimeOfOrder = DateTime.Now,
                        Customer = customer,

                    };
                    // var products = _productRepository.GetSelectedProducts(goods);
                    Console.WriteLine($"Thanks for your purchase");
                    foreach (var item in goods)
                    { 
                        double cost = 0;
                        var prod = _context.Products.Find(item.Key);
                        cost = (prod.SellingPrice) * (item.Value);
                        totalCost += cost;
                        var orderproduct = new OrderProduct
                        {
                            Order = order,
                            OrderId = order.Id,
                            Product = prod,
                            ProductId = item.Key,
                            Quantity = item.Value
                        };
                        order.OrderProducts.Add(orderproduct);
                    }
                
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                }
                
            
        }
        
    }
}