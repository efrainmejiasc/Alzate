using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrPeriodoTransaccPersonas : ApiController
    {
        IPeriodoTransaccPersonas Iperiodo = new CPeriodoTransaccionesPersonas();

        public IList<GE_TPERIODOTRANSACCPERSONAS> GetAll()
        {
            try
            {
                return Iperiodo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPERIODOTRANSACCPERSONAS pr)
        {
            try
            {
                Iperiodo.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPERIODOTRANSACCPERSONAS pr)
        {
            try
            {
                Iperiodo.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public void Delete(int inPeriodoTransacc)
        {
            try
            {
                Iperiodo.Delete(inPeriodoTransacc);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERIODOTRANSACCPERSONAS> GetByIdPeriodoTransacc(int inPeriodoTransacc)
        {
            try
            {
                return Iperiodo.GetByIdPeriodoTransacc(inPeriodoTransacc);
            }
            catch
            {
                throw;
            }

        }
    }
}
