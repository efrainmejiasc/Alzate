using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrCargueCuentasEspeciales : ApiController
    {
        ICargueCuentasEspeciales IctEspeciales = new CCargueCuentasEspeciales();

        public IEnumerable<GE_TCARGUEARCHIVOS> LeerExcel(string hoja, string archivo)
        {
            try
            {
                IList<GE_TCARGUEARCHIVOS> lstArchivos = IctEspeciales.leerExcel(hoja, archivo);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TCARGUEARCHIVOS> LeerDatos(int pHojaIndex, String pRutaArchivo)
        {
            try
            {
                IList<GE_TCARGUEARCHIVOS> lstArchivos = IctEspeciales.leerDatos(pHojaIndex, pRutaArchivo);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCARGUEARCHIVOS> GetAll()
        {
            try
            {
                return IctEspeciales.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCARGUEARCHIVOS> GetAllProd(String strProducto)
        {
            try
            {
                return IctEspeciales.GetAllProd(strProducto);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Guardar(IList<GE_TCARGUEARCHIVOS> lstPpto, String strUsr, String strProducto)
        {
            try
            {
                IctEspeciales.Guardar(lstPpto, strUsr,strProducto);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

    }
}
