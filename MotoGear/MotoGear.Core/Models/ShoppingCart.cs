using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.Models
{
    public class ShoppingCart : BaseEntity
    {
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart()
        {
            this.ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}
