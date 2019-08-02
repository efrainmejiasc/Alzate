using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueCuentasEspeciales
    {
        IList<GE_TCARGUEARCHIVOS> GetAll();
        IList<GE_TCARGUEARCHIVOS> leerExcel(String strHoja, String strArchivo);
        IList<GE_TCARGUEARCHIVOS> leerDatos(int pHojaIndex, String pRutaArchivo);
        void Guardar(IList<GE_TCARGUEARCHIVOS> lstPpto, String strUsr, String strProducto);
        IList<GE_TCARGUEARCHIVOS> GetAllProd(String strProducto);
    }
}
