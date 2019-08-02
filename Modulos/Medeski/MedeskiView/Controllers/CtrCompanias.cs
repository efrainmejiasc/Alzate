using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrCompanias : ApiController
    {
        ICompanias companias = new CCompanias();

        public IList<GE_TCOMPANIAS> GetAllActive()
        {
            try
            {
                return companias.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCOMPANIAS> GetAll()
        {
            try
            {
                return companias.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public GE_TCOMPANIAS GetSingle(int consecutivo)
        {
            try
            {
                return companias.GetSingle(consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TCOMPANIAS compania)
        {
            try
            {
                companias.Add(compania);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }


        public IHttpActionResult Update(GE_TCOMPANIAS compania)
        {
            try
            {
                companias.Update(compania);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }
    }
}