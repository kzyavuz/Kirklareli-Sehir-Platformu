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
    public class BannerManager : IBannerService
    {
        private readonly IBannerDal _bannerDal;

        public BannerManager(IBannerDal bannerDal)
        {
            _bannerDal = bannerDal;
        }

        public void TAdd(banner t)
        {
            _bannerDal.Insert(t);
        }

        public void TDelete(banner t)
        {
            _bannerDal.Delete(t);
        }

        public banner TGetByID(int id)
        {
            return _bannerDal.GetByID(id);
        }

        public List<banner> TGetList()
        {
            return _bannerDal.GetList();
        }

        public void TUpdate(banner t)
        {
            _bannerDal.Update(t);
        }
    }
}
