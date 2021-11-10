using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class ViewModelPCC
    {
        public IEnumerable<ProductDetail> ProductList { get; set; }
        public IEnumerable<CategoryDetail> CategoryList { get; set; }
        public CategoryDetail SingleCategory { get; set; }
    }
}