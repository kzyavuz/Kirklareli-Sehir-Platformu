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
    public class CityStartManager : ICityStartService
    {
        private readonly ICityStartDal _cityStartDal;

        public CityStartManager(ICityStartDal cityStartDal)
        {
            _cityStartDal = cityStartDal;
        }

        public void TAdd(CityStart t)
        {
            _cityStartDal.Insert(t);
        }

        public void TDelete(CityStart t)
        {
            _cityStartDal.Delete(t);
        }

        public CityStart TGetByID(int id)
        {
            return _cityStartDal.GetByID(id);
        }

        public List<CityStart> TGetList()
        {
            return _cityStartDal.GetList().OrderByDescending(x => x.CreateDateTime).ToList();
        }

        public void TUpdate(CityStart t)
        {
            _cityStartDal.Update(t);
        }
    }
}
