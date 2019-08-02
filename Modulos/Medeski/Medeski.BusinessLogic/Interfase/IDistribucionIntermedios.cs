using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDistribucionIntermedios
    {
        IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllProductoItem(int periodo, int producto, int item);
        IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllProductosDistribuidos(int inPeriodo, int inProductoIntermedio, int inItem);
        void Add(params GE_TDISTRIBUCIONINTERMEDIOS[] objeto);
        void Update(params GE_TDISTRIBUCIONINTERMEDIOS[] objeto);

    }
}
