using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IServidores
    {
        IList<GE_TSERVIDORES> GetAll();
        GE_TSERVIDORES GetById(int inConsecutivo);
        IList<GE_TSERVIDORES> GetAllActive();
        void Add(params GE_TSERVIDORES[] objeto);
        void Update(params GE_TSERVIDORES[] objeto);
    }
}
