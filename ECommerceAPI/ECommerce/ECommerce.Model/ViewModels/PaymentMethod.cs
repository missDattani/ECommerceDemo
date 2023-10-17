using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Type { get; set; }
        public string Provider { get; set; }
        public string Available { get; set; }
        public string Reason { get; set; }
    }
}
