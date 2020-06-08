using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.Interfaces;
using SweetShopBusinessLogic.ViewModels;
using SweetShopDatabaseImplement.Models;
using System.Linq;

namespace SweetShopDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new SweetShopDatabase())
            {
                Order element;
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }
                element.ProductId = model.ProductId == 0 ? element.ProductId : model.ProductId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new SweetShopDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                        if (order != null)
                        {
                            context.Orders.Remove(order);
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        context.SaveChanges();
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

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new SweetShopDatabase())
            {
                return context.Orders.Where(rec => model == null ||
                     (rec.Id == model.Id && model.Id.HasValue) ||
                     (model.DateFrom.HasValue && model.DateTo.HasValue &&
                     (rec.DateCreate >= model.DateFrom) && (rec.DateCreate <= model.DateTo))).ToList().Select(rec => new OrderViewModel()
                 {
                         Id = rec.Id,
                         ProductId = rec.ProductId,
                         ProductName = context.Products.FirstOrDefault((r) => r.Id == rec.ProductId).ProductName,
                         Count = rec.Count,
                         DateCreate = rec.DateCreate,
                         DateImplement = rec.DateImplement,
                         Status = rec.Status,
                         Sum = rec.Sum
                     })
            .ToList();
            }
        }
    }
}