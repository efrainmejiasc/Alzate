using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICentroCosto
    {
        IList<GE_TCENTROSCOSTOS> GetAll();
        IList<GE_TCENTROSCOSTOS> GetAllActive();
        GE_TCENTROSCOSTOS GetSingle(int id);
        IEnumerable<GE_TCENTROSCOSTOS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat);
        IList<GE_TCENTROSCOSTOS> GetAllUsuarioCentros(string strUsuario, string strSubCat);
        IEnumerable<GE_TCENTROSCOSTOS> GetAllCuentaxParametros();
        GE_TCENTROSCOSTOS GetSingle(String p_centro_costos);
        GE_TCENTROSCOSTOS GetSingle(String p_centro_costos, int p_empresa);
        void Add(params GE_TCENTROSCOSTOS[] objeto);
        void Update(params GE_TCENTROSCOSTOS[] objeto);
    }
}
