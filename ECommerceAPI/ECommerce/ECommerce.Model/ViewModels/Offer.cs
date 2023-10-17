using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class Offer
    {
        public int OfferId { get; set; }
        public string? Title { get; set; }
        public int Discount { get; set; }
    }
}
