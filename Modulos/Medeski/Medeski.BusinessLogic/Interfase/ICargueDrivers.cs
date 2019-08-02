using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueDrivers
    {
        IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo, string usuario);
        IList<GE_TPRODUCTOS> obtenerProductos();
        IList<VW_PRODUCTOS_DIRECTOS> obtenerProductosDirectos();
        IList<GE_TDRIVERS> obtenerDrivers();
        IList<GE_TCOMPANIAS> obtenerCompanias();
        IList<GE_TCENTROSCOSTOS> obtenerCCostos();
        IList<GE_TCENTROSOPERACION> obtenerCOperacion();
        void guardar(IList<GE_TCARGUEDRIVERS> p_lstDrivers, string usuario);
        IList<DTOgenericoCargueArchivos> cargarDriversActivos();
    }
}
