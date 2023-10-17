using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
    }
}
