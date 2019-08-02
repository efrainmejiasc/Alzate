using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrGente : ApiController
    {
        CGente gente = new CGente();

        public IList<GE_TGENTE> GetAll()
        {
            try
            {
                return gente.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public GE_TGENTE GetSingle(int consecutivo)
        {
            try
            {
                return gente.GetSingle(consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(params GE_TGENTE[] persona)
        {
            try
            {
                gente.Add(persona);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(params GE_TGENTE[] persona)
        {
            try
            {
                gente.Update(persona);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TGENTE> GetAllInfo(string usuario)
        {
            try 
            {
                return gente.GetAllInfo(usuario);
            }
            catch
            {
                throw;
            }
        }

        public GE_TGENTE getByPersonaId(int id)
        {
            try
            {
                return gente.getByPersonaId(id);
            }
            catch
            {
                throw;
            }
        }
    }
}