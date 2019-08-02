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
    public class CtrCargueGastosLaborales : ApiController
    {
        ICargueGastosLaborales Icargue = new CCargueGastosLaborales();

        public IEnumerable<GE_TCARGUEARCHIVOSLABORAL> LeerExcel(string subCat, string hoja, string archivo)
        {
            try
            {
                IList<GE_TCARGUEARCHIVOSLABORAL> lstArchivos = Icargue.leerExcel(subCat, hoja, archivo);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TCARGUEARCHIVOSLABORAL> LeerDatos(string subCat, int pHojaIndex, String pRutaArchivo, string usuario)
        {
            try
            {
                IList<GE_TCARGUEARCHIVOSLABORAL> lstArchivos = Icargue.leerDatos(subCat, pHojaIndex, pRutaArchivo, usuario);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }
        

        public IHttpActionResult Guardar(IList<GE_TCARGUEARCHIVOSLABORAL> lstPpto, String strUsr)
        {
            try
            {
                Icargue.Guardar(lstPpto, strUsr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCARGUEARCHIVOSLABORAL> GetAll()
        {
            try
            {
                return Icargue.GetAll();
            }
            catch
            {
                throw;
            }
        }
    }
}
