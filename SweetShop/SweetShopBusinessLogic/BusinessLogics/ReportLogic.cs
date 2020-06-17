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
        private readonly IIngredientLogic softLogic;
        private readonly IProductLogic packLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IProductLogic packLogic, IIngredientLogic softLogic,
            IOrderLogic orderLogic)
        {
            this.softLogic = softLogic;
            this.packLogic = packLogic;
            this.orderLogic = orderLogic;
        }

        public List<ReportProductIngredientViewModel> GetProductIngredient()
        {
            var packs = packLogic.Read(null);
            var list = new List<ReportProductIngredientViewModel>();
            foreach (var pack in packs)
            {
                foreach (var ps in pack.ProductIngredients)
                {
                    var record = new ReportProductIngredientViewModel
                    {
                        ProductName = pack.ProductName,
                        IngredientName = ps.Value.Item1,
                        Count = ps.Value.Item2
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
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов",
                Products = packLogic.Read(null)
            });
        }

        /// <summary>
        /// Сохранение закусок с указаеним продуктов в файл-Excel
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
        /// Сохранение закусок с продуктами в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductIngredientsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PDFInfo
            {
                FileName = model.FileName,
                Title = "Детализация продуктов ",
                ProductIngredients = GetProductIngredient()
            });
        }
    }
}