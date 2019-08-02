using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrVlrCuadroServicioDetalle : ApiController
    {
        IVlrCuadroServicioDetalle salida = new CVlrCuadroServicioDetalle();
        public IList<GE_TVLR_CUADRO_SERVICIO_DETALLE> GetAll()
        {
            try
            {
                IList<GE_TVLR_CUADRO_SERVICIO_DETALLE> vw = salida.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}