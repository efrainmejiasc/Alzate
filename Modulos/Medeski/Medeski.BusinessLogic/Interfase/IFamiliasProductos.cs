using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IFamiliasProductos
    {
        IList<GE_TFAMILIAS_PRODUCTOS> GetAll();
        GE_TFAMILIAS_PRODUCTOS GetSingle(int consecutivo);
        GE_TFAMILIAS_PRODUCTOS GetByHijo(int hijo);
        IList<GE_TFAMILIAS_PRODUCTOS> GetByPadre(int padre);
        
    }
}
