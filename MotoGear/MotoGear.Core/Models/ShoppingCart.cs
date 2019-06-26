using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.Models
{
    public class ShoppingCart : BaseEntity
    {
        public virtual ICollection<CartItem> CartItems { get; set; }
        public ShoppingCart()
        {
            this.CartItems = new List<CartItem>();
        }
    }
}
