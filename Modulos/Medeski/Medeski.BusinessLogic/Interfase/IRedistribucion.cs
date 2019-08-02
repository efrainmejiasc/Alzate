using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IRedistribucion
    {
        IList<GE_TREDISTRIBUCION> GetAll();
        IList<GE_TREDISTRIBUCION> GetByIdProducto(int inProducto, int inPeriodo);
        void Add(params GE_TREDISTRIBUCION[] objeto);
        void Update(params GE_TREDISTRIBUCION[] objeto);
    }
}
