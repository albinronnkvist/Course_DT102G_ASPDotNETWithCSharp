using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbshop.Models
{
    public class ProductDetail
    {
        // Constructor
        public ProductDetail() { }

        // Auto-implemented properties
        // Id
        public int Id { get; set; }

        // CategoryId
        public int CategoryId { get; set; }

        // ProductName
        [Required(ErrorMessage = "Ange ett produktnamn")]
        [DisplayName("Produktnamn")]
        public string ProductName { get; set; }

        // ProductKeywords
        [Required(ErrorMessage = "Ange nyckelord")]
        [DisplayName("Nyckelord")]
        public string ProductKeywords { get; set; }

        // ProductDescription
        [Required(ErrorMessage = "Ange en beskrivning")]
        [DisplayName("Beskrivning")]
        public string ProductDescription { get; set; }

        // ProductPrice
        [Required(ErrorMessage = "Ange ett pris")]
        [DisplayName("Pris")]
        public decimal ProductPrice { get; set; }

        // ProductImage
        [Required(ErrorMessage = "Ladda upp en bild")]
        [DisplayName("Bild")]
        public byte[] ProductImage { get; set; }

        // ProductImageDescription
        [Required(ErrorMessage = "Ange en bildbeskrivning")]
        [DisplayName("Bildbeskrivning")]
        public string ProductImageDescription { get; set; }
    }
}