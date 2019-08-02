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
    public class CtrOpcionesMenuRol : ApiController
    {
        IOpcionesMenuRol opc = new COpcionesMenuRol();

        public IList<GE_TOPCIONESMENUXROL> GetOpciones(int idRol)
        {
            return opc.GetOpciones(idRol);
        }

        
        public IHttpActionResult deleteOpcionesUsuario(int rol)
        {
            try
            {
                opc.deleteOpcionesUsuario(rol);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IHttpActionResult Add(GE_TOPCIONESMENUXROL opcion)
        {
            try
            {
                opc.Add(opcion);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
