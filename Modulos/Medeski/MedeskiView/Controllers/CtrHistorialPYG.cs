using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrHistorialPYG : ApiController
    {
        IHistoricoPYG Ihisto = new CHistoricoPYG();

        public IList<GE_THISTORICOPYG> GetAll()
        {
            try
            {
                return Ihisto.GetAll();
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_THISTORICOPYG> GetAllActive()
        {
            try
            {
                return Ihisto.GetAllActive();
            }
            catch
            {
                throw;
            }
        }


        public IHttpActionResult Add(GE_THISTORICOPYG centro)
        {
            try
            {
                Ihisto.Add(centro);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_THISTORICOPYG centro)
        {
            try
            {
                Ihisto.Update(centro);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public void Guardar(IList<GE_THISTORICOPYG> p_lstDrivers)
        {

            try
            {
                Ihisto.guardar(p_lstDrivers);
            }
            catch
            {
                throw;
            }

        }
    }
}