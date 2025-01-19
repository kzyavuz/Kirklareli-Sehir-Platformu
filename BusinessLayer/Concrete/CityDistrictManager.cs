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
    public class CityDistrictManager : ICityDistrictService
    {
        private readonly ICityDistrictDal _cityDistrictDal;

        public CityDistrictManager(ICityDistrictDal cityDistrictDal)
        {
            _cityDistrictDal = cityDistrictDal;
        }

        public void TAdd(CityDistrict t)
        {
            _cityDistrictDal.Insert(t);
        }

        public void TDelete(CityDistrict t)
        {
            _cityDistrictDal.Delete(t);
        }

        public CityDistrict TGetByID(int id)
        {
            return _cityDistrictDal.GetByID(id);
        }

        public List<CityDistrict> TGetList()
        {
            return _cityDistrictDal.GetList();
        }

        public void TUpdate(CityDistrict t)
        {
            _cityDistrictDal.Update(t);
        }
    }
}
