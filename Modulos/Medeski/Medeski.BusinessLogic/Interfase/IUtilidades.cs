using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.BusinessLogic.Class;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IUtilidades
    {
        DataTable ExceltoDataTable(int p_hoja, String p_archivo);
        IList<DTOCuadroServicio> GenerarCuadroServicio();
        DataTable CalcularValorItemServidor();
    }
}
