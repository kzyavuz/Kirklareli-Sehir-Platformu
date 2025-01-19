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
    public class EFBlogDal : GenericRepository<Blog>, IBlogDal
    {
        private readonly Context _context;

        public EFBlogDal(Context context)
        {
            _context = context;
        }

        public List<string> ListBlogCategory()
        {
            return _context.BlogCategories.Select(x => x.CategoryName).Distinct().ToList();
        }
    }
}
