using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SweetShopBusinessLogic.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название пра")]
        public string IngredientName { get; set; }
    }
}