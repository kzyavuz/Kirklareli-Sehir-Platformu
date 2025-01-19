using DataAccessLayer.Repository;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using DataAccessLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFIlanDal : GenericRepository<Ilanlar>, IIlanDal
    {
        private readonly Context _c;

        public EFIlanDal(Context c)
        {
            _c = c;
        }

        public void ConvertStandOutFalse(int id)
        {
            var values = _c.Ilanlars.Find(id);
            values.StandOutStatus = false;
            _c.Update(values);
            _c.SaveChanges();
        }

        public void ConvertStandOutTrue(int id)
        {
            var values = _c.Ilanlars.Find(id);
            values.StandOutStatus = true;
            values.StandOutDateTime = DateTime.Now;
            _c.Update(values);
            _c.SaveChanges();
        }

        //public void ConvertToIlan(int id, string status)
        //{
        //    var values = _c.Ilanlars.Find(id);
        //    values.Status = status;

        //    if (values.Status == "Reddedildi")
        //    {
        //        values.StandOutStatus = false;
        //        values.RejectionDateTime = DateTime.Now;

        //    }
        //    values.ApprovalDateTime = DateTime.Now;
        //    _c.Update(values);
        //    _c.SaveChanges();
        //}

        public void ConvertToFalseIlan(int id)
        {
            var values = _c.Ilanlars.Find(id);
            values.Status = "Reddedildi";
            values.StandOutStatus = false;
            values.RejectionDateTime = DateTime.Now;
            _c.Update(values);
            _c.SaveChanges();
        }

        public void ConvertToTrueIlan(int id)
        {
            var values = _c.Ilanlars.Find(id);
            values.Status = "Onaylandı";
            values.ApprovalDateTime = DateTime.Now;
            _c.Update(values);
            _c.SaveChanges();
        }

    }
}
