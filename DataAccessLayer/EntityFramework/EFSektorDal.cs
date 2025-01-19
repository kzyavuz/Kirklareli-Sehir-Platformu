using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFSektorDal : GenericRepository<Sektor>, ISektorDal
    {
        private readonly Context _context;

        public EFSektorDal(Context context)
        {
            _context = context;
        }

        public List<string> ListSektorCategory()
        {
            return _context.SektorCategories.Select(x => x.CategoryName).Distinct().ToList();
        }
    }
}
