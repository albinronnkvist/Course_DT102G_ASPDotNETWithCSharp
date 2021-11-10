using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class CategoryDetail
    {
        // Constructor
        public CategoryDetail() { }

        // Auto-implemented properties
        // Id
        public int Id { get; set; }

        // CategoryName
        [Required(ErrorMessage = "Ange ett kategorinamn")]
        [DisplayName("Kategori")]
        public string CategoryName { get; set; }
    }
}