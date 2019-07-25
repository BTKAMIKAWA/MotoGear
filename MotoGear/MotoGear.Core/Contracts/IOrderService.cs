using MotoGear.Core.Models;
using MotoGear.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<ShoppingCartItemViewModel> shoppingCartItems);
    }
}
