using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrRedistribucion
    {
        IList<VW_VLR_REDISTRIBUCION> GetAll();
        IList<VW_VLR_REDISTRIBUCION> GetByIdProducto(int inProducto);
        Decimal GetSumProducto(int inProducto);
    }
}
