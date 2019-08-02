using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;

namespace MedeskiView.Controllers
{
    public class CtrVwCuadroServicioTotal : ApiController
    {
        IVwVlrCuadroServicioTotal salida = new CVwVlrCuadroServicioTotal();
        public IList<VW_VLR_CUADRO_SERVICIO_TOTAL> GetAll()
        {
            try
            {
                IList<VW_VLR_CUADRO_SERVICIO_TOTAL> vw = salida.GetAll().OrderBy(x => x.servicio).ThenByDescending(x => x.Total).ToList();
                return vw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public VW_VLR_CUADRO_SERVICIO_TOTAL GetByproducto(string producto)
        {
            try
            {
                return salida.GetByProducto(producto);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult UpdateCuadros()
        {
            try
            {
                salida.UpdateCuadros();
                return Ok(true);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
