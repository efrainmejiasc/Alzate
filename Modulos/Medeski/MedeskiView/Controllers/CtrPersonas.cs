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
    public class CtrPersonas : ApiController
    {
        IPersonas pers = new CPersonas();

        public IList<GE_TPERSONAS> GetAll()
        {
            try
            {
                return pers.GetAllInfo();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERSONAS> GetAllActive()
        {
            try
            {
                return pers.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERSONAS> GetAllJefe()
        {
            try
            {
                return pers.GetAllJefe();
            }
            catch
            {
                throw;
            }
        }

        public GE_TPERSONAS GetbyUsuario(string strUsuario)
        {
            try
            {
                return pers.GetbyUsuario(strUsuario);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERSONAS> GetAllActiveOrderBy()
        {
            try
            {
                return pers.GetAllActiveOrderBy();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERSONAS> GetAllActiveJefeOrderBy(string strUsuario)
        {
            try
            {
                return pers.GetAllActiveJefeOrderBy(strUsuario);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERSONAS> GetAllActivexArea(string strUsuario)
        {
            try
            {
                return pers.GetAllActivexArea(strUsuario);
            }
            catch
            {
                throw;
            }

        }

        public GE_TPERSONAS GetbyConsecutivo(int inConsecutivo)
        {
            try
            {
                return pers.GetbyConsecutivo(inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPERSONAS persona)
        {
            try
            {
                pers.Add(persona);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPERSONAS persona)
        {
            try
            {
                pers.Update(persona);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TPERSONAS> GetAllInfo()
        {
            try
            {
                return pers.GetAllInfo();                
            }
            catch
            {
                throw;
            }
        }
    }
}
