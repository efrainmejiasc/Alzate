using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IProductos
    {
        IList<GE_TPRODUCTOS> GetAll();
        IList<GE_TPRODUCTOS> GetById(int inConsecutivo);
        void Add(params GE_TPRODUCTOS[] objeto);
        void Update(params GE_TPRODUCTOS[] objeto);
        IList<GE_TPRODUCTOS> GetAllGridView();
        IList<GE_TPRODUCTOS> GetAllIntermedios();
        IEnumerable<GE_TPRODUCTOS> GetAllTipoComponente(string strTipoComponente);
        IEnumerable<GE_TPRODUCTOS> GetAllDirectos(string strTipoComponente);
        IList<GE_TPRODUCTOS> GetAllActive();
        IList<GE_TPRODUCTOS> GetAllUsuario(string strUsuario, string strSubCat);
        IEnumerable<GE_TPRODUCTOS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat, int inCeco);
        GE_TPRODUCTOS GetSingle(String p_producto);
        GE_TPRODUCTOS GetSingle(int consecutivo);
        IList<GE_TPRODUCTOS> GetAllRedistribuir();        
    }
}
