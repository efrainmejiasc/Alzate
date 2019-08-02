using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrReporteRedistribucion : ApiController
    {
        CVWReporteRedistribucion CReporte = new CVWReporteRedistribucion();

        public IList<VW_REPORTE_REDISTRIBUCION> GetAll()
        {
            try
            {
                return CReporte.GetAll();
            }
            catch
            {
                throw;
            }
        }
    }
}