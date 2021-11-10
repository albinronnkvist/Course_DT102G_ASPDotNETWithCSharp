using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class OrderDetail
    {
        // Constructor
        public OrderDetail() { }

        // Auto-implemented properties
        // Id
        public int Id { get; set; }

        // ProductId
        public int ProductId { get; set; }

        // UserId
        public string UserId { get; set; }

        // ProductAmount
        public int ProductAmount { get; set; }

        // OrderDate
        public DateTime OrderDate { get; set; }

        // IsOrdered
        public bool IsOrdered { get; set; }
    }
}