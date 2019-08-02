using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueDrivers_20180509
    {
        IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo);
        IList<GE_TPRODUCTOS> obtenerProductos();
        IList<GE_TCENTROSCOSTOS> obtenerCCostos();
        IList<GE_TCENTROSOPERACION> obtenerCOperacion();
        void guardar(IList<GE_TFACTURACIONCARGUEDRIVERS> p_lstDrivers);
        IList<DTOgenericoCargueArchivos> cargarDriversActivos();
    }
}
