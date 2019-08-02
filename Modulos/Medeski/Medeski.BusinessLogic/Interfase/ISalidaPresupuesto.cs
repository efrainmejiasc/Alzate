using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ISalidaPresupuesto
    {
        IList<GE_TSALIDAPRESUPUESTO> GetAll();
        IList<GE_TSALIDAPRESUPUESTO> GetByPeriodoTransacc(int periodo);
        void Add(params GE_TSALIDAPRESUPUESTO[] objeto);
        void Update(params GE_TSALIDAPRESUPUESTO[] objeto);
        void DeleteByPeriodoTransacc(int periodo);
    }
}
