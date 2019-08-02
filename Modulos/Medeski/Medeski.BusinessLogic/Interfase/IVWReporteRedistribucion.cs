using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVWReporteRedistribucion
    {
        IList<VW_REPORTE_REDISTRIBUCION> GetAll();
    }
}
