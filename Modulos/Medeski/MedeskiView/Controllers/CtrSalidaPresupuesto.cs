using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrSalidaPresupuesto : ApiController
    {
        private readonly ISalidaPresupuesto IsalidaP = new CSalidaPresupuesto();

        public IList<GE_TSALIDAPRESUPUESTO> GetAll()
        {
            try
            {
                return IsalidaP.GetAll();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IList<GE_TSALIDAPRESUPUESTO> GetByPeriodoTransacc(int periodo)
        {
            try
            {
                return IsalidaP.GetByPeriodoTransacc(periodo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IHttpActionResult Add(params GE_TSALIDAPRESUPUESTO[] objeto)
        {
            try
            {
                IsalidaP.Add(objeto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IHttpActionResult Update(params GE_TSALIDAPRESUPUESTO[] objeto)
        {
            try
            {
                IsalidaP.Update(objeto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IHttpActionResult DeleteByPeriodoTransacc(int periodo)
        {
            try
            {
                IsalidaP.DeleteByPeriodoTransacc(periodo);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}