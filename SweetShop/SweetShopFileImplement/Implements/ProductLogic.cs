﻿using System;
using System.Collections.Generic;
using System.Text;
using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.Interfaces;
using SweetShopBusinessLogic.ViewModels;
using System.Linq;
using SweetShopFileImplement;
using SweetShopFileImplement.Models;

namespace SweetShopFileImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        private readonly FileDataListSingleton source;
        public ProductLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ProductBindingModel model)
        {
            Product element = source.Products.FirstOrDefault(rec => rec.ProductName ==
           model.ProductName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть продукт с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Products.Count > 0 ? source.Ingredients.Max(rec =>
               rec.Id) : 0;
                element = new Product { Id = maxId + 1 };
                source.Products.Add(element);
            }
            element.ProductName = model.ProductName;
            element.Price = model.Price;
            source.ProductIngredients.RemoveAll(rec => rec.ProductId == model.Id &&
           !model.ProductIngredients.ContainsKey(rec.IngredientId));
            var updateIngredients = source.ProductIngredients.Where(rec => rec.ProductId ==
           model.Id && model.ProductIngredients.ContainsKey(rec.IngredientId));
            foreach (var updateIngredient in updateIngredients)
            {
                updateIngredient.Count =
               model.ProductIngredients[updateIngredient.IngredientId].Item2;
                model.ProductIngredients.Remove(updateIngredient.IngredientId);
            }
            int maxPCId = source.ProductIngredients.Count > 0 ?
           source.ProductIngredients.Max(rec => rec.Id) : 0;
            foreach (var pc in model.ProductIngredients)
            {
                source.ProductIngredients.Add(new ProductIngredient
                {
                    Id = ++maxPCId,
                    ProductId = element.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(ProductBindingModel model)
        {
            source.ProductIngredients.RemoveAll(rec => rec.ProductId == model.Id);
            Product element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Products.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            return source.Products
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ProductViewModel
            {
                Id = rec.Id,
                ProductName = rec.ProductName,
                Price = rec.Price,
                ProductIngredients = source.ProductIngredients
            .Where(recPC => recPC.ProductId == rec.Id)
           .ToDictionary(recPC => recPC.IngredientId, recPC =>
            (source.Ingredients.FirstOrDefault(recC => recC.Id ==
           recPC.IngredientId)?.IngredientName, recPC.Count))
            })
            .ToList();
        }
    }
}