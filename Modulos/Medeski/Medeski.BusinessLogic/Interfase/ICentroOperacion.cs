using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICentroOperacion
    {
        IList<GE_TCENTROSOPERACION> GetAll();
        GE_TCENTROSOPERACION GetSingle(String p_centro_operacion);
        void Add(params GE_TCENTROSOPERACION[] objeto);
        void Update(params GE_TCENTROSOPERACION[] objeto);
    }
}
