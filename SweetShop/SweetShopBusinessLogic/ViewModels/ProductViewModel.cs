using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SweetShopBusinessLogic.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название продукта")]
        public string ProductName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ProductIngredients { get; set; }
    }
}