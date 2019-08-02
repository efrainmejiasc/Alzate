using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVlrsPrmgrales
    {
        IList<VLRSPRMGRALES> GetAll();
        VLRSPRMGRALES GetByClase(string strClase);
        void Add(params VLRSPRMGRALES[] objeto);
        void Update(params VLRSPRMGRALES[] objeto);
        void Remove(params VLRSPRMGRALES[] objeto);
        int GetNextSequenceValue(string nombreSequencia);
    }
}
