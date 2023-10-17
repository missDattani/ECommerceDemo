using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class Payment
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public string Type { get; set; }
        public string Provider { get; set; }
        public bool Available { get; set; }
        public string Reason { get; set; }
        public int UserId { get; set; }
        public int TotalAmount { get; set; }
        public int ShippingCharges { get; set; }
        public int AmountReduced { get; set; }
        public int AmountPaid { get; set; }
        public string createdAt { get; set; }
    }
}
