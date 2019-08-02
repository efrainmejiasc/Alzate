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
    public class CtrVarEconomicas : ApiController
    {
        IVarEconomicas varEc = new CVarEconomicas();

        public IHttpActionResult Add(GE_TVARECONOMICAS pr)
        {
            try
            {
                varEc.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TVARECONOMICAS pr)
        {
            try
            {
                varEc.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TVARECONOMICAS> GetByMonedaAno(int moneda, int inAno)
        {
            try
            {
                return varEc.GetByMonedaAno(moneda, inAno);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public IList<GE_TVARECONOMICAS> GetAll(int inAno)
        {
            try
            {
                return varEc.GetAll(inAno);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TVARECONOMICAS> GetAllActive(int inAno)
        {
            try
            {
                return varEc.GetAllActive(inAno);
            }
            catch
            {
                throw;
            }
        }

        public GE_TVARECONOMICAS GetById(int inConsecutivo)
        {
            try
            {
                return varEc.GetById(inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public GE_TVARECONOMICAS GetByAnoMes(int inMes, int inMoneda, int inAno)
        {
            try
            {
                return varEc.GetByAnoMes(inMes, inMoneda, inAno);
            }
            catch
            {
                throw;
            }
        }
    }
}
