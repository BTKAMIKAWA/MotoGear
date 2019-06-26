using MotoGear.Core.Contracts;
using MotoGear.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MotoGear.Services
{
    public class CartService
    {
        IRepository<Product> productContext;
        IRepository<ShoppingCart> shoppingCartContext;

        public const string ShoppingCartSessionName = "eCommerceShoppingCart";

        public CartService(IRepository<Product> ProductContext, IRepository<ShoppingCart> ShoppingCartContext)
        {
            this.shoppingCartContext = ShoppingCartContext;
            this.productContext = ProductContext;
        }

        private ShoppingCart GetShoppingCart(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(ShoppingCartSessionName);

            ShoppingCart shoppingCart = new ShoppingCart();

            if (cookie != null)
            {
                string shoppingCartId = cookie.Value;
                if (!string.IsNullOrEmpty(shoppingCartId))
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
            cookie.Expires = DateTime.Now.AddDays(3);
            httpContext.Response.Cookies.Add(cookie);

            return shoppingCart;
        }

        public void AddToShoppingCart(HttpContextBase httpContext, string productId)
        {
            ShoppingCart shoppingCart = GetShoppingCart(httpContext, true);
            CartItem item = shoppingCart.CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
            {
                item = new CartItem()
                {
                    CartId = shoppingCart.Id,
                    ProductId = productId,
                    Quantity = 1
                };

                shoppingCart.CartItems.Add(item);
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
            CartItem item = shoppingCart.CartItems.FirstOrDefault(i => i.ProductId == itemId);

            if(item != null)
            {
                shoppingCart.CartItems.Remove(item);
                shoppingCartContext.Commit();
            }
        }
    }
}
