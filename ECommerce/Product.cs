using System.Collections;
using System.Collections.Generic;

namespace ECommerce
{
    public class Product
    {
        public  int Id { get; set; }
        public  string Name { get; set; }
        public  double SellingPrice { get; set; }
        public  double CostPrice { get; set; }
        public List<OrderProduct> _OrderProducts { get; set; } = new List<OrderProduct>();
        public  int AdminId { get; set; }
        public  Admin _Admin { get; set; }
        public  int Quantity { get; set; }

        
    }
}