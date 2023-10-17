using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class Cart
    {
        public int CartId { get; set; } 

        public User User { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
        public bool Ordered { get; set; }
        public string? OrderedOn { get; set; }

    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public int OfferId { get; set; }
        public string? OfferTitle { get; set; }
        public int Discount { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageName { get; set; }
    }

    //public Product Product { get; set; } = new Product();

}
