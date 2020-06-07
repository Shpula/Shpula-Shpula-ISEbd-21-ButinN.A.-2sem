using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.Interfaces;
using SweetShopBusinessLogic.ViewModels;
using SweetShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweetShopDatabaseImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        public void CreateOrUpdate(ProductBindingModel model)
        {
            using (var context = new SweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Product element = context.Products.FirstOrDefault(rec =>
                       rec.ProductName == model.ProductName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть пакет с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Products.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Product();
                            context.Products.Add(element);
                        }
                        element.ProductName = model.ProductName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productComponents = context.ProductIngredients.Where(rec
                           => rec.ProductId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.ProductIngredients.RemoveRange(productComponents.Where(rec =>
                            !model.ProductIngredients.ContainsKey(rec.IngredientId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateComponent in productComponents)
                            {
                                updateComponent.Count =
                               model.ProductIngredients[updateComponent.IngredientId].Item2;

                                model.ProductIngredients.Remove(updateComponent.IngredientId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.ProductIngredients)
                        {
                            context.ProductIngredients.Add(new ProductIngredient
                            {
                                ProductId = element.Id,
                                IngredientId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(ProductBindingModel model)
        {
            using (var context = new SweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.ProductIngredients.RemoveRange(context.ProductIngredients.Where(rec =>
                        rec.ProductId == model.Id));
                        Product element = context.Products.FirstOrDefault(rec => rec.Id
                       == model.Id);
                        if (element != null)
                        {
                            context.Products.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            using (var context = new SweetShopDatabase())
            {
                return context.Products
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new ProductViewModel
               {
                   Id = rec.Id,
                   ProductName = rec.ProductName,
                   Price = rec.Price,
                   ProductIngredients = context.ProductIngredients
                .Include(recPC => recPC.Ingredient)
               .Where(recPC => recPC.ProductId == rec.Id)
               .ToDictionary(recPC => recPC.IngredientId, recPC =>
                (recPC.Ingredient?.IngredientName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}