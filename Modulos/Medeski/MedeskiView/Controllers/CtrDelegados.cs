using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrDelegados : ApiController
    {
        IDelegados IDelegado = new CDelegados();

        public IHttpActionResult Add(GE_TDELEGADOS delegado)
        {
            try
            {
                IDelegado.Add(delegado);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }


        public IHttpActionResult Update(GE_TDELEGADOS delegado)
        {
            try
            {
                IDelegado.Update(delegado);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TDELEGADOS> GetAll()
        {
            try
            {
                return IDelegado.GetAll();
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TDELEGADOS> GetAllActive()
        {
            try
            {
                return IDelegado.GetAllActive();
            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<GE_TDELEGADOS> GetAllDelegados(String jefe)
        {
            try
            {
                return IDelegado.GetAllDelegados(jefe);
            }
            catch
            {
                throw;
            }
        }
        
        public GE_TDELEGADOS GetSingle(int consecutivo)
        {
            try
            {
                return IDelegado.GetSingle(consecutivo);
            }
            catch
            {
                throw;
            }
        }


        public GE_TDELEGADOS GetByDelegado(int delegado)
        {
            try
            {
                return IDelegado.GetByDelegado(delegado);
            }
            catch
            {
                throw;
            }
        }


        public GE_TDELEGADOS GetByDelegadoFase(int delegado, int fase)
        {
            try
            {
                return IDelegado.GetByDelegadoFase(delegado, fase);
            }
            catch
            {
                throw;
            }
        }



        public IList<GE_TDELEGADOS> GetByJefeFase(int jefe, int fase)
        {
            try
            {
                return IDelegado.GetByJefeFase(jefe, fase);
            }
            catch
            {
                throw;
            }
        }
    }
}