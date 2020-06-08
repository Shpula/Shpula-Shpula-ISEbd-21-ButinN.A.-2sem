using SweetShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweetShopBusinessLogic.HelperModels
{
    class PDFInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportProductIngredientsViewModel> ProductIngredients { get; set; }
    }
}
