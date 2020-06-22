using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using SweetShopBusinessLogic.Attributes;

namespace SweetShopBusinessLogic.ViewModels
{
    [DataContract]
    public class ProductViewModel : BaseViewModel
    {
        [Column(title: "Название продукта", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string ProductName { get; set; }
        [Column(title: "Цена", width: 50)]
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> ProductIngredients { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ProductName",
            "Price"
        };
    }
}