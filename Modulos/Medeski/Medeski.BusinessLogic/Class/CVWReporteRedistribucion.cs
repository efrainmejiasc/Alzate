using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVWReporteRedistribucion : Interfase.IVWReporteRedistribucion
    {
        private readonly IVWReporteRedistribucion CRUD = new VWReporteRedistribucion();

        public IList<VW_REPORTE_REDISTRIBUCION> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch
            {
                throw;
            }
        }

    }
}
