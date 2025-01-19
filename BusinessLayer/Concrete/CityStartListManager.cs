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
    public class CityStartListManager : ICityStartListService
    {
        private readonly ICityStartListDal _cityStartListDal;

        public CityStartListManager(ICityStartListDal cityStartListDal)
        {
            _cityStartListDal = cityStartListDal;
        }

        public void TAdd(CityStartList t)
        {
            _cityStartListDal.Insert(t);
        }

        public void TDelete(CityStartList t)
        {
            _cityStartListDal.Delete(t);
        }

        public CityStartList TGetByID(int id)
        {
            return _cityStartListDal.GetByID(id);
        }

        public List<CityStartList> TGetList()
        {
            return _cityStartListDal.GetList();
        }

        public void TUpdate(CityStartList t)
        {
            _cityStartListDal.Update(t);
        }
    }
}
