using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IPersonas
    {
        IList<GE_TPERSONAS> GetAll();
        IList<GE_TPERSONAS> GetAllActive();
        IList<GE_TPERSONAS> GetAllJefe();
        IList<GE_TPERSONAS> GetAllInfo();
        GE_TPERSONAS GetbyUsuario(string strUsuario);
        IEnumerable<GE_TPERSONAS> GetAllActiveOrderBy();
        IEnumerable<GE_TPERSONAS> GetAllActiveJefeOrderBy(string strUsuario);
        IEnumerable<GE_TPERSONAS> GetAllActivexArea(string strUsuario);
        GE_TPERSONAS GetbyConsecutivo(int inConsecutivo);
        GE_TPERSONAS GetSingle(String p_identificacionPersona);
        void Add(params GE_TPERSONAS[] objeto);
        void Update(params GE_TPERSONAS[] objeto);
    }
}
