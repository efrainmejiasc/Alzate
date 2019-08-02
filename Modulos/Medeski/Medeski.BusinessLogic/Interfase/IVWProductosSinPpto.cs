using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVWProductosSinPpto
    {
        IList<VW_PRODUCTOS_SIN_PPTO> GetAll();
    }
}
