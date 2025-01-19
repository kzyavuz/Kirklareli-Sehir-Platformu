using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IIlanService : IGenericService<Ilanlar>
    {

        public void ConvertToTrueIlan(int id);
        public void ConvertToFalseIlan(int id);

        public void ConvertStandOutTrue(int id);
        public void ConvertStandOutFalse(int id);
    }
}
