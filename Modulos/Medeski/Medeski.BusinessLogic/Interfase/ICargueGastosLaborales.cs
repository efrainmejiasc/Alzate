using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueGastosLaborales
    {
        IList<GE_TCARGUEARCHIVOSLABORAL> leerExcel(string subCat, String strHoja, String strArchivo);
        IList<GE_TCARGUEARCHIVOSLABORAL> leerDatos(string subCat, int pHojaIndex, String pRutaArchivo, string usuario);
        void add(params GE_TCARGUEARCHIVOSLABORAL[] objeto);
        void Guardar(IList<GE_TCARGUEARCHIVOSLABORAL> lstPpto, String strUsr);
        IList<GE_TCARGUEARCHIVOSLABORAL> GetAll();
    }
}
