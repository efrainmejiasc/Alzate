using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVlrCuadroServicio
    {
        IList<GE_TVLR_CUADRO_SERVICIO> GetAll();
    }
}
