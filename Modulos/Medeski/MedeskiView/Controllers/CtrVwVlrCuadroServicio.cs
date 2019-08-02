using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;

namespace MedeskiView.Controllers
{
    public class CtrVwVlrCuadroServicio : ApiController
    {
        IVwVlrCuadroServicio salida = new CVwVlrCuadroServicio();
        public IList<VW_VLR_CUADRO_SERVICIO> GetAll()
        {
            try
            {
                IList<VW_VLR_CUADRO_SERVICIO> vw = salida.GetAll().OrderBy(x => x.producto).ToList();
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}
