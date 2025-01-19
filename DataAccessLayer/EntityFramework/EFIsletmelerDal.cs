using DataAccessLayer.Repository;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using DataAccessLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFIsletmelerDal : GenericRepository<Isletmeler>, IIsletmelerDal
    {
        private readonly Context _c;

        public EFIsletmelerDal(Context c)
        {
            _c = c;
        }

        public void ConvertStandOutFalse(int id)
        {
            var values = _c.Isletmelers.Find(id);
            values.StandOutStatus = false;
            _c.Update(values);
            _c.SaveChanges();
        }

        public void ConvertStandOutTrue(int id)
        {
            var values = _c.Isletmelers.Find(id);
            values.StandOutStatus = true;
            values.StandOutDateTime = DateTime.Now;
            _c.Update(values);
            _c.SaveChanges();
        }

        public void ConvertToFalseIsletme(int id)
        {
            var values = _c.Isletmelers.Find(id);
            values.Status = "Reddedildi";
            values.StandOutStatus = false;
            values.RejectionDateTime = DateTime.Now;
            _c.Update(values);
            _c.SaveChanges();
        }

        public void ConvertToTrueIsletme(int id)
        {
            var values = _c.Isletmelers.Find(id);
            values.Status = "Onaylandı";
            values.ApprovalDateTime = DateTime.Now;
            _c.Update(values);
            _c.SaveChanges();
        }
    }
}
