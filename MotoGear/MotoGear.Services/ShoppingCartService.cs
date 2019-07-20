using MotoGear.Core.Contracts;
using MotoGear.Core.Models;
using MotoGear.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MotoGear.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        IRepository<Product> productContext;
        IRepository<ShoppingCart> shoppingCartContext;

        public const string ShoppingCartSessionName = "eCommerceShoppingCart";

        public ShoppingCartService(IRepository<Product> ProductContext, IRepository<ShoppingCart> ShoppingCartContext)
        {
            this.shoppingCartContext = ShoppingCartContext;
            this.productContext = ProductContext;
        }

        private ShoppingCart GetShoppingCart(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(ShoppingCartSessionName);

            ShoppingCart shoppingCart = new ShoppingCart();

            if(cookie != null)
            {
                string shoppingCartId = cookie.Value;
                if(!string.IsNullOrEmpty(shoppingCartId))
                {
                    shoppingCart = shoppingCartContext.Find(shoppingCartId);
                }
                else
                {
                    if (createIfNull)
                    {
                        shoppingCart = CreateNewShoppingCart(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    shoppingCart = CreateNewShoppingCart(httpContext);
                }
            }
            return shoppingCart;
        }

        private ShoppingCart CreateNewShoppingCart(HttpContextBase httpContext)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCartContext.Insert(shoppingCart);
            shoppingCartContext.Commit();

            HttpCookie cookie = new HttpCookie(ShoppingCartSessionName);
            cookie.Value = shoppingCart.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return shoppingCart;
        }

        public void AddToShoppingCart(HttpContextBase httpContext, string productId)
        {
            ShoppingCart shoppingCart = GetShoppingCart(httpContext, true);
            ShoppingCartItem item = shoppingCart.ShoppingCartItems.FirstOrDefault(i => i.ProductId == productId);

            if(item==null)
            {
                item = new ShoppingCartItem()
                {
                    ShoppingCartId = shoppingCart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                shoppingCart.ShoppingCartItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            shoppingCartContext.Commit();
        }

        public void RemoveFromShoppingCart(HttpContextBase httpContext, string itemId)
        {
            ShoppingCart shoppingCart = GetShoppingCart(httpContext, true);
            ShoppingCartItem item = shoppingCart.ShoppingCartItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                shoppingCart.ShoppingCartItems.Remove(item);
                shoppingCartContext.Commit();
            }
        }

        public List<ShoppingCartItemViewModel> GetShoppingCartItems(HttpContextBase httpContext)
        {
            ShoppingCart shoppingCart = GetShoppingCart(httpContext, false);

            if (shoppingCart != null)
            {
                var results = (from b in shoppingCart.ShoppingCartItems
                               join p in productContext.Collection() on b.ProductId equals p.Id
                               select new ShoppingCartItemViewModel()
                               {
                                   Id = b.Id,
                                   Quantity = b.Quantity,
                                   ProductName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price
                               }).ToList();
                return results;
            }
            else
            {
                return new List<ShoppingCartItemViewModel>();
            }
        }

        public ShoppingCartSummaryViewModel GetShoppingCartSummary(HttpContextBase httpContext)
        {
            ShoppingCart shoppingCart = GetShoppingCart(httpContext, false);
            ShoppingCartSummaryViewModel model = new ShoppingCartSummaryViewModel(0, 0);
            if(shoppingCart != null)
            {
                int? shoppingCartCount = (from item in shoppingCart.ShoppingCartItems select item.Quantity).Sum();

                decimal? shoppingCartTotal = (from item in shoppingCart.ShoppingCartItems join p in productContext.Collection() on item.ProductId equals p.Id select item.Quantity * p.Price).Sum();

                model.ShoppingCartCount = shoppingCartCount ?? 0;
                model.ShoppingCartTotal = shoppingCartTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }
    }
}
