using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IClaseParametros
    {
        IList<GE_TCLASESPARAMETROS> GetAll();
        void Add(params GE_TCLASESPARAMETROS[] objeto);
        void Update(params GE_TCLASESPARAMETROS[] objeto);
        void Remove(params GE_TCLASESPARAMETROS[] objeto);
        int GetNextSequenceValue(string nombreSequencia);
    }
}
