using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SektorCategoryManager : ISektorCategoryService
    {
        private readonly ISektorCategoryDal _sektorCategoryDal;

        public SektorCategoryManager(ISektorCategoryDal sektorCategoryDal)
        {
            _sektorCategoryDal = sektorCategoryDal;
        }

        public void TAdd(SektorCategory t)
        {
            _sektorCategoryDal.Insert(t);
        }

        public void TDelete(SektorCategory t)
        {
            _sektorCategoryDal.Delete(t);
        }

        public SektorCategory TGetByID(int id)
        {
            return _sektorCategoryDal.GetByID(id);
        }

        public List<SektorCategory> TGetList()
        {
            return _sektorCategoryDal.GetList();
        }

        public void TUpdate(SektorCategory t)
        {
            _sektorCategoryDal.Update(t);
        }
    }
}
