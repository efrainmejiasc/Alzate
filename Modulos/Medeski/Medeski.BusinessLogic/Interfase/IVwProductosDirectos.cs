using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwProductosDirectos
    {
        IList<VW_PRODUCTOS_DIRECTOS> GetAll();
        VW_PRODUCTOS_DIRECTOS GetByProducto(String producto);

    }
}
