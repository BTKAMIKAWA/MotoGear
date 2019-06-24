using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.Core.Models
{
    public class Product : BaseEntity
    {
        
        [Required]
        public string Brand { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        [Required, MinLength(5)]
        public string Description { get; set; }
        [Required, Range(0, 1500)]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        public string Image { get; set; }

    }
}
