using DataAccessLayer.Repository;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFUyeliklerDal : GenericRepository<Uyelikler>, IUyeliklerDal
    {
        private readonly Context _context;

        public EFUyeliklerDal(Context context)
        {
            _context = context;
        }

        public IEnumerable<Uyelikler> SearchUyelikler(string query)
        {
            return _context.Uyeliklers
                  .Where(u => u.UyelikTipi.Contains(query))
                  .ToList();
        }
    }
}
