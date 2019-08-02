using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDelegados
    {
        IList<GE_TDELEGADOS> GetAll();
        IList<GE_TDELEGADOS> GetAllActive();
        IEnumerable<GE_TDELEGADOS> GetAllDelegados(string strUsuario);
        GE_TDELEGADOS GetSingle(int cnosecutivo);
        GE_TDELEGADOS GetByDelegado(int delegado);
        GE_TDELEGADOS GetByDelegadoFase(int delegado, int fase);
        IList<GE_TDELEGADOS> GetByJefeFase(int jefe, int fase);
        void Add(params GE_TDELEGADOS[] objeto);
        void Update(params GE_TDELEGADOS[] objeto);
    }
}
