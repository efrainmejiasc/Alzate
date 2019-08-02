using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IRedistribucionDrivers
    {
        void eliminarActivos(IList<GE_TCARGUEDRIVERS> cargue);
        void guardar(IList<GE_TREDISTRIBUCION_DRIVERS> p_lstDrivers);
    }
}
