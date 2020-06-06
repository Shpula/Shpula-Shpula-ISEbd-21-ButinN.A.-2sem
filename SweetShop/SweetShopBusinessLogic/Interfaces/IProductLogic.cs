using System;
using System.Collections.Generic;
using System.Text;
using SweetShopBusinessLogic.ViewModels;
using SweetShopBusinessLogic.BindingModels;

namespace SweetShopBusinessLogic.Interfaces
{
    public interface IProductLogic
    {
        List<ProductViewModel> Read(ProductBindingModel model);
        void CreateOrUpdate(ProductBindingModel model);
        void Delete(ProductBindingModel model);
    }
}