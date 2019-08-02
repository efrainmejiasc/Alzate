using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVWProductosSinPpto : Interfase.IVWProductosSinPpto
    {
        IVWProductosSinPpto CRUD = new VWProductosSinPpto();

        public IList<VW_PRODUCTOS_SIN_PPTO> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
