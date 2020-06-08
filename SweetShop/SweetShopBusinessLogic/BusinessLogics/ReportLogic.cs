using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.HelperModels;
using SweetShopBusinessLogic.Interfaces;
using SweetShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetShopBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IIngredientLogic ingredientLogic;
        private readonly IProductLogic productLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IProductLogic productLogic, IIngredientLogic ingredientLogic,
            IOrderLogic orderLogic)
        {
            this.ingredientLogic = ingredientLogic;
            this.productLogic = productLogic;
            this.orderLogic = orderLogic;
        }
        public List<ReportProductIngredientsViewModel> GetProductIngredients()
        {
            var products = productLogic.Read(null);
            var list = new List<ReportProductIngredientsViewModel>();
            foreach (var product in products)
            {
                foreach (var pi in product.ProductIngredients)
                {
                    var record = new ReportProductIngredientsViewModel
                    {
                        ProductName = product.ProductName,
                        IngredientName = pi.Value.Item1,
                        Count = pi.Value.Item2
                    };
                    list.Add(record);
                }
            }

            return list;
        }

        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {         
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })          
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recc => recc.Key)
            .ToList();

            return list;
        }

        /// <summary>
        /// Сохранение комингредиентнент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список Продуктов",
                Products = productLogic.Read(null)
            });
        }

        /// <summary>
        /// Сохранение закусок с указаеним Продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {              
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        /// <summary>
        /// Сохранение закусок с Продуктами в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductIngredientsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PDFInfo
            {
                FileName = model.FileName,
                Title = "Детализация Продуктов ",
                ProductIngredients = GetProductIngredients()
            });
        }
    }
}