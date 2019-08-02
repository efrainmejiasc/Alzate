using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IGente
    {
        IList<GE_TGENTE> GetAll();
        IList<GE_TGENTE> GetAllInfo(string usuario);
        GE_TGENTE GetSingle(int consecutivo);
        void Add(params GE_TGENTE[] objeto);
        void Update(params GE_TGENTE[] objeto);
    }
}
