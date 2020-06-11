using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.ViewModels;
using System.Collections.Generic;
namespace SweetShopBusinessLogic.Interfaces
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);
        void CreateOrUpdate(ImplementerBindingModel model);
        void Delete(ImplementerBindingModel model);
    }
}