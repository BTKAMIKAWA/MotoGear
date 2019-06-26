using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.ViewModels
{
    public class CartSummaryViewModel
    {
        public int ShoppingCartCount { get; set; }
        public decimal ShoppingCartTotal { get; set; }

        public CartSummaryViewModel()
        {

        }

        public CartSummaryViewModel(int shoppingCartCount, decimal shoppingCartTotal)
        {
            this.ShoppingCartCount = shoppingCartCount;
            this.ShoppingCartTotal = shoppingCartTotal;

        }

    }
}
