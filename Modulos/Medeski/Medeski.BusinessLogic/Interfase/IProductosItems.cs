using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IProductosItems
    {
        IList<GE_TPRODUCTOSITEMS> GetByProducto(int idProducto);
        void Add(params GE_TPRODUCTOSITEMS[] objeto);
        void Update(params GE_TPRODUCTOSITEMS[] objeto);
        IList<GE_TPRODUCTOSITEMS> GetAllGridView();
        IList<GE_TPRODUCTOSITEMS> GetAllGridViewXprod(int idProd);
        IEnumerable<GE_TPRODUCTOSITEMS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat, int inCeco, int inProd);
        GE_TPRODUCTOSITEMS GetItemxNombre(string strNombre);
    }
}
