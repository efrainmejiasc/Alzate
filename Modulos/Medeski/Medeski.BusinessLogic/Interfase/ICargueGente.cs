using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueGente
    {
        void guardar(IList<GE_TGENTE> p_lstGente);
        IList<GE_TGENTE> getAllPeriodoActivo();
        IList<GE_TGENTE> getAll();
    }
}
