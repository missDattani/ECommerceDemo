using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{
    public class ReviewData
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; } 
        public string? Review { get; set; }
        public string? CreatedAt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
