using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrCuadroServicioTotal
    {
        IList<VW_VLR_CUADRO_SERVICIO_TOTAL> GetAll();
        VW_VLR_CUADRO_SERVICIO_TOTAL GetByProducto(string producto);
        void UpdateCuadros();
    }
}
