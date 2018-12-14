using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carpentry.Core.Models;

namespace Carpentry.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        public Carpenter Carpenter { get; set; }
        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
