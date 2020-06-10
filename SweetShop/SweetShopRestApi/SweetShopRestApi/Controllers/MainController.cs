using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.BusinessLogics;
using SweetShopBusinessLogic.Interfaces;
using SweetShopBusinessLogic.ViewModels;
using SweetShopRestApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SweetShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IProductLogic _pack;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IProductLogic pack, MainLogic main)
        {
            _order = order;
            _pack = pack;
            _main = main;
        }
        [HttpGet]
        public List<ProductModel> GetProductList() => _pack.Read(null)?.Select(rec => Convert(rec)).ToList();
        [HttpGet]
        public ProductModel GetProduct(int packId) => Convert(_pack.Read(new ProductBindingModel
        { Id = packId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private ProductModel Convert(ProductViewModel model)
        {
            if (model == null) return null;
            return new ProductModel
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price
            };
        }
    }
}