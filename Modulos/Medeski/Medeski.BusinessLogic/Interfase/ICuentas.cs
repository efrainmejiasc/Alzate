using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICuentas
    {
        IList<GE_TCUENTAS> GetAll();
        IList<GE_TCUENTAS> GetAllActive();
        GE_TCUENTAS GetById(int inConsecutivo);
        void Add(params GE_TCUENTAS[] objeto);
        void Update(params GE_TCUENTAS[] objeto);
    }
}
