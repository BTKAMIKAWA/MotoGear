using MotoGear.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MotoGear.Core.Contracts
{
    public interface ICartService
    {
        void AddToShoppingCart(HttpContextBase httpContext, string productId);
        List<CartItemViewModel> GetCartItems(HttpContextBase httpContext);
        CartSummaryViewModel GetCartSummary(HttpContextBase httpContext);
        void RemoveFromShoppingCart(HttpContextBase httpContext, string itemId);
        void ClearCart(HttpContextBase httpContext);
    }
}
