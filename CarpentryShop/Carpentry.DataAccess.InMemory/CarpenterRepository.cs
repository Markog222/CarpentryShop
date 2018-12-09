using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Carpentry.Core.Models;

namespace Carpentry.DataAccess.InMemory
{
    class CarpenterRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Carpenter> carpenters;

        public CarpenterRepository()
        {
            carpenters = cache["Carpenters"] as List<Carpenter>;
            if (carpenters == null)
            {
                carpenters = new List<Carpenter>();
            }
        }

        public void Commit()
        {
            cache["carpenters"] = carpenters;
        }

        public void Insert(Carpenter p)
        {
            carpenters.Add(p);
        }

        public void Update(Carpenter product)
        {
            Carpenter productToUpdate = carpenters.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public Carpenter Find(string Id)
        {
            Carpenter product = carpenters.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public IQueryable<Carpenter> Collection()
        {
            return carpenters.AsQueryable();
        }

        public void Delete(string Id)
        {
            Carpenter productToDelete = carpenters.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                carpenters.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product no found");
            }

        }
    }
}
