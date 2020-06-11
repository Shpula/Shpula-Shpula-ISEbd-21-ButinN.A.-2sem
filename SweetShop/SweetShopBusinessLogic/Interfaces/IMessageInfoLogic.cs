using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.ViewModels;
using System.Collections.Generic;
namespace SweetShopBusinessLogic.Interfaces
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);
        void Create(MessageInfoBindingModel model);
    }
}