using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDistribMAS
    {
        IList<GE_TDISTRIBUCIONMASPROCESOS> GetAllProductosDistrib(int inPeriodo);
        void Add(params GE_TDISTRIBUCIONMASPROCESOS[] objeto);
        void Update(params GE_TDISTRIBUCIONMASPROCESOS[] objeto);
    }
}
