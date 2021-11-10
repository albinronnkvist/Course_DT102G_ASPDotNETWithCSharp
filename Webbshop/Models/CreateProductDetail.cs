using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class CreateProductDetail
    {
        public ProductDetail Product { get; set; }
        public IEnumerable<CategoryDetail> CategoryList { get; set; }
    }
}