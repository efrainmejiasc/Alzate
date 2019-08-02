using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IPeriodoTransaccPersonas
    {
        IList<GE_TPERIODOTRANSACCPERSONAS> GetAll();
        IList<GE_TPERIODOTRANSACCPERSONAS> GetByIdPeriodoTransacc(int inPeriodoTransacc);
        void Add(params GE_TPERIODOTRANSACCPERSONAS[] objeto);
        void Update(params GE_TPERIODOTRANSACCPERSONAS[] objeto);
        void Delete(int inPeriodoTransacc);
    }
}
