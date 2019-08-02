using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICompanias
    {
        IList<GE_TCOMPANIAS> GetAll();
        IList<GE_TCOMPANIAS> GetAllActive();
        GE_TCOMPANIAS GetSingle(int consecutivo);
        void Add(params GE_TCOMPANIAS[] objeto);
        void Update(params GE_TCOMPANIAS[] objeto);
    }
}
