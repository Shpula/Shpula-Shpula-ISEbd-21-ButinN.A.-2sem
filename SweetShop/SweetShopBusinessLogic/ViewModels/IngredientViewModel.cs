using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SweetShopBusinessLogic.Attributes;

namespace SweetShopBusinessLogic.ViewModels
{

    public class IngredientViewModel : BaseViewModel
    {
        [Column(title: "Ингредиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngredientName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "IngredientName"
        };
    }
}