using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Carpentry.Core.Models;

namespace Carpentry.DataAccess.InMemory
{
    public class CarpenterRepository
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

        public void Update(Carpenter carpenter)
        {
            Carpenter carpenterToUpdate = carpenters.Find(p => p.Id == carpenter.Id);

            if (carpenterToUpdate != null)
            {
                carpenterToUpdate = carpenter;
            }
            else
            {
                throw new Exception("Carpenter no found");
            }
        }

        public Carpenter Find(string Id)
        {
            Carpenter carpenter = carpenters.Find(p => p.Id == Id);

            if (carpenter != null)
            {
                return carpenter;
            }
            else
            {
                throw new Exception("Carpenter no found");
            }
        }

        public IQueryable<Carpenter> Collection()
        {
            return carpenters.AsQueryable();
        }

        public void Delete(string Id)
        {
            Carpenter carpenterToDelete = carpenters.Find(p => p.Id == Id);

            if (carpenterToDelete != null)
            {
                carpenters.Remove(carpenterToDelete);
            }
            else
            {
                throw new Exception("Carpenter not found");
            }

        }
    }
}
