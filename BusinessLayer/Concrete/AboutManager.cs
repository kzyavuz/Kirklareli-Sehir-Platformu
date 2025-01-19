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
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public void TAdd(AboutMe t)
        {
            _aboutDal.Insert(t);
        }

        public void TDelete(AboutMe t)
        {
            _aboutDal.Delete(t);
        }

        public AboutMe TGetByID(int id)
        {
            return _aboutDal.GetByID(id);
        }

        public List<AboutMe> TGetList()
        {
            return _aboutDal.GetList();
        }

        public void TUpdate(AboutMe t)
        {
            _aboutDal.Update(t);
        }
    }
}
