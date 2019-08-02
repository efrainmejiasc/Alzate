using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IPeriodoPresupuesto
    {
        IList<GE_TPERIODOPRESUPUESTO> GetAll();
        IList<GE_TPERIODOPRESUPUESTO> GetAllActive();
        GE_TPERIODOPRESUPUESTO GetPeriodoActivo();
        void Add(params GE_TPERIODOPRESUPUESTO[] objeto);
        void Update(params GE_TPERIODOPRESUPUESTO[] objeto);
        IList<GE_TPERIODOPRESUPUESTO> FindByAnoEtapa(int ano, string etapa);
        GE_TPERIODOPRESUPUESTO GetByAnioPaso(int ano, int etapa);
        int LoadTransactions(int buscar, int nuevo);
    }
}
