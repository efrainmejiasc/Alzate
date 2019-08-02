using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwSalidaPresupuesto
    {
        IList<VW_SALIDA_PRESUPUESTO> GetAllxUser(string strUsuario);
        IList<VW_SALIDA_PRESUPUESTO> GetAll();
        Double GetSumSalidaGastosArea();
        Decimal GetSumItem(int inItem);
    }
}
