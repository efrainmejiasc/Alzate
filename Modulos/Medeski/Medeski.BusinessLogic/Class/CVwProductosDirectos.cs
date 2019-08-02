using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVwProductosDirectos : Interfase.IVwProductosDirectos
    {
        private readonly IVwProductosDirectos CRUD;

        public CVwProductosDirectos()
        {
            CRUD = new VwProductosDirectos();
        }



        public IList<VW_PRODUCTOS_DIRECTOS> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public VW_PRODUCTOS_DIRECTOS GetByProducto(string producto)
        {
            try
            {
                return CRUD.GetSingle(x => x.prod_codigo == producto);
            }
            catch
            {
                throw;
            }
        }
    }
}
