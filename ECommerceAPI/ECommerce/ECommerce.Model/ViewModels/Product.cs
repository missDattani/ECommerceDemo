using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class Product
    {
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
}
