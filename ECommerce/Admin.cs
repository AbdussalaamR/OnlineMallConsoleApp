using System.Collections.Generic;

namespace ECommerce
{
    public class Admin
    {
        public int Id { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public  List<Product> _Products { get; set; }
    }
}