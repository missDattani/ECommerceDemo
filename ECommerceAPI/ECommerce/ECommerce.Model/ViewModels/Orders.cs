using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int PaymentId { get; set; }
        public string CreatedAt { get; set; }
    }
}
