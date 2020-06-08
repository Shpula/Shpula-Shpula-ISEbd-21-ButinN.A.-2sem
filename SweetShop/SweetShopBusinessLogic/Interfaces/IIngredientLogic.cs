using System;
using System.Collections.Generic;
using System.Text;
using SweetShopBusinessLogic.ViewModels;
using SweetShopBusinessLogic.BindingModels;

namespace SweetShopBusinessLogic.Interfaces
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> Read(IngredientBindingModel model);
        void CreateOrUpdate(IngredientBindingModel model);
        void Delete(IngredientBindingModel model);
    }
}