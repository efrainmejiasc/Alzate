using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrVWCuadroVentasPYG : ApiController
    {
        IVWCuadroVentasPYG ICuadro = new CVWCuadroVentasPYG();

        public IList<VW_CUADRO_VENTAS_PYG> GetAll()
        {
            try
            {
                return ICuadro.GetAll();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}