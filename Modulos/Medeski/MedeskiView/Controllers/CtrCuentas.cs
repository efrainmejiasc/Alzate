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
    public class CtrCuentas : ApiController
    {
        ICuentas cta = new CCuentas();

        public IHttpActionResult Add(GE_TCUENTAS pr)
        {
            try
            {
                cta.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TCUENTAS pr)
        {
            try
            {
                cta.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCUENTAS> GetAll()
        {
            try
            {
                return cta.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public GE_TCUENTAS GetById(int inConsecutivo)
        {
            try
            {
                return cta.GetById(inConsecutivo);
            }
            catch
            {
                throw;
            }

        }

        public IList<GE_TCUENTAS> GetAllActive()
        {
            try
            {
                return cta.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

    }
}
