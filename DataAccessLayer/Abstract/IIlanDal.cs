using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IIlanDal : IGenericDal<Ilanlar>
    {
        public void ConvertToTrueIlan(int id);
        public void ConvertToFalseIlan(int id);

        public void ConvertStandOutTrue(int id);
        public void ConvertStandOutFalse(int id);
    }
}
