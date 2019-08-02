using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueGastosArea
    {
        IList<DTOgenericoCargueArchivos> validaInformacionExcel(String p_hoja, String p_archivo);
        IList<GE_TPRODUCTOS> obtenerProductos();
        IList<GE_TCENTROSCOSTOS> obtenerCCostos();
        IList<DTOgenericoCargueArchivos> informacionExcel(String p_hoja, String p_archivo);
        IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo);
        void guardarGastosArea(IList<GE_TDISTRIBUCIONCARGUEGA> p_lstGastos);
        IList<DTOgenericoCargueArchivos> obtenerActuales();
    }
}
