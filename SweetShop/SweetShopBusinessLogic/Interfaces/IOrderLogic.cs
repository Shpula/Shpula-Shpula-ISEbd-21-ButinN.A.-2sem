using System;
using System.Collections.Generic;
using System.Text;
using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.ViewModels;

namespace SweetShopBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);
        void CreateOrUpdate(OrderBindingModel model);
        void Delete(OrderBindingModel model);
    }
}