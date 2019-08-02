using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrVlrCuadroServicio : ApiController
    {
        IVlrCuadroServicio salida = new CVlrCuadroServicio();
        public IList<GE_TVLR_CUADRO_SERVICIO> GetAll()
        {
            try
            {
                IList<GE_TVLR_CUADRO_SERVICIO> vw = salida.GetAll().OrderBy(x => x.producto).ToList();
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}