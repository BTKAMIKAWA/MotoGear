using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.ViewModels
{
    public class ShoppingCartSummaryViewModel
    {
        public int ShoppingCartCount { get; set; }
        public decimal ShoppingCartTotal { get; set; }

        public ShoppingCartSummaryViewModel()
        {

        }

        public ShoppingCartSummaryViewModel(int shoppingCartCount, decimal shoppingCartTotal)
        {
            this.ShoppingCartCount = shoppingCartCount;
            this.ShoppingCartTotal = shoppingCartTotal;

        }

    }
}
