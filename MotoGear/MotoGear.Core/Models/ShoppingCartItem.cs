using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.Models
{
    public class ShoppingCartItem : BaseEntity
    {
        public string ShoppingCartId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
