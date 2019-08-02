using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IHistoricoPYG
    {
        IList<GE_THISTORICOPYG> GetAll();
        IList<GE_THISTORICOPYG> GetAllActive();
        void Add(params GE_THISTORICOPYG[] objeto);
        void Update(params GE_THISTORICOPYG[] objeto);
        void guardar(IList<GE_THISTORICOPYG> p_lstDrivers);
    }
}
