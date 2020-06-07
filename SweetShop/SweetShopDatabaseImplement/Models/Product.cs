using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SweetShopDatabaseImplement.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public decimal Price { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<ProductIngredient> ProductIngredients { get; set; }
    }
}