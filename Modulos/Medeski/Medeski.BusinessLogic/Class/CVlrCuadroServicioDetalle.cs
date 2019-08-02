using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVlrCuadroServicioDetalle: Interfase.IVlrCuadroServicioDetalle
    {
        private readonly IVlrCuadroServicioDetalle CRUD;

        public CVlrCuadroServicioDetalle()
        {
            CRUD = new VlrCuadroServicioDetalle();
        }
        public IList<GE_TVLR_CUADRO_SERVICIO_DETALLE> GetAll()
        {
            try
            {
                IList<GE_TVLR_CUADRO_SERVICIO_DETALLE> cuadro = CRUD.GetAll();
                return cuadro;
            }
            catch
            {
                throw;
            }
        }
    }
}
