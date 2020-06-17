using System;
using System.Collections.Generic;
using System.Text;
using SweetShopBusinessLogic.BindingModels;
using SweetShopBusinessLogic.Interfaces;
using SweetShopBusinessLogic.ViewModels;
using SweetShopListImplement.Models;

namespace SweetShopListImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        private readonly DataListSingleton source;
        public ProductLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ProductBindingModel model)
        {
            Product tempProduct = model.Id.HasValue ? null : new Product { Id = 1 };
            foreach (var Product in source.Products)
            {
                if (Product.ProductName == model.ProductName && Product.Id != model.Id)
                {
                    throw new Exception("Уже есть продукт с таким названием");
                }
                if (!model.Id.HasValue && Product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = Product.Id + 1;
                }
                else if (model.Id.HasValue && Product.Id == model.Id)
                {
                    tempProduct = Product;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Products.Add(CreateModel(model, tempProduct));
            }
        }
        public void Delete(ProductBindingModel model)
        {
            // удаляем записи ингредиенты ингредиенты при удалении продукты
            for (int i = 0; i < source.ProductIngredients.Count; ++i)
            {
                if (source.ProductIngredients[i].ProductId == model.Id)
                {
                    source.ProductIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Products.Count; ++i)
            {
                if (source.Products[i].Id == model.Id)
                {
                    source.Products.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Product CreateModel(ProductBindingModel model, Product Product)
        {
            Product.ProductName = model.ProductName;
            Product.Price = model.Price;
            //обновляем существуюущее ингредиенты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.ProductIngredients.Count; ++i)
            {
                if (source.ProductIngredients[i].Id > maxPCId)
                {
                    maxPCId = source.ProductIngredients[i].Id;
                }
                if (source.ProductIngredients[i].ProductId == Product.Id)
                {
                    // если в модели пришла запись ингредиенты с таким id
                    if
                    (model.ProductIngredients.ContainsKey(source.ProductIngredients[i].IngredientId))
                    {
                        // обновляем количество
                        source.ProductIngredients[i].Count =
                        model.ProductIngredients[source.ProductIngredients[i].IngredientId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные


                        model.ProductIngredients.Remove(source.ProductIngredients[i].IngredientId);
                    }
                    else
                    {
                        source.ProductIngredients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.ProductIngredients)
            {
                source.ProductIngredients.Add(new ProductIngredient
                {
                    Id = ++maxPCId,
                    ProductId = Product.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return Product;
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            foreach (var Ingredient in source.Products)
            {
                if (model != null)
                {
                    if (Ingredient.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Ingredient));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Ingredient));
            }
            return result;
        }
        private ProductViewModel CreateViewModel(Product Product)
        {
            Dictionary<int, (string, int)> ProductIngredients = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.ProductIngredients)
            {
                if (pc.ProductId == Product.Id)
                {
                    string IngredientName = string.Empty;
                    foreach (var Ingredient in source.Ingredients)
                    {
                        if (pc.IngredientId == Ingredient.Id)
                        {
                            IngredientName = Ingredient.IngredientName;
                            break;
                        }
                    }
                    ProductIngredients.Add(pc.IngredientId, (IngredientName, pc.Count));
                }
            }
            return new ProductViewModel
            {
                Id = Product.Id,
                ProductName = Product.ProductName,
                Price = Product.Price,
                ProductIngredients = ProductIngredients
            };
        }
    }
}