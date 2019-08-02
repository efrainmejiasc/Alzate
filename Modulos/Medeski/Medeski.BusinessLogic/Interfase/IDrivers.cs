using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDrivers
    {
        IList<GE_TDRIVERS> GetAll();
        IList<GE_TDRIVERS> GetAllActive();
        GE_TDRIVERS GetSingle(int consecutivo);
        void Add(params GE_TDRIVERS[] objeto);
        void Update(params GE_TDRIVERS[] objeto);
    }
}
