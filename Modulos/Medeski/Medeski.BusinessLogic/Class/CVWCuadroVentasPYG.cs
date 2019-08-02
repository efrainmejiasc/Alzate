using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVWCuadroVentasPYG : Interfase.IVWCuadroVentasPYG
    {
        private readonly IVWCuadroVentasPYG CRUD = new VWCuadroVentasPYG();

        public IList<VW_CUADRO_VENTAS_PYG> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
