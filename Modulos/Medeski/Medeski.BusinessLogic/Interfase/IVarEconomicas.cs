using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVarEconomicas
    {
        IList<GE_TVARECONOMICAS> GetAll(int inAno);
        IList<GE_TVARECONOMICAS> GetByMonedaAno(int moneda, int inAno);
        IList<GE_TVARECONOMICAS> GetAllActive(int inAno);
        GE_TVARECONOMICAS GetById(int inConsecutivo);
        GE_TVARECONOMICAS GetByAnoMes(int inMes, int inMoneda, int inAno);
        void Add(params GE_TVARECONOMICAS[] objeto);
        void Update(params GE_TVARECONOMICAS[] objeto);
    }
}
