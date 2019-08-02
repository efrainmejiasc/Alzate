using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrCargueGastosArea : ApiController
    {
        ICargueGastosArea IcargueGtosArea = new CCargueGastosArea();

        public IEnumerable<DTOgenericoCargueArchivos> LeerExcel(string hoja, string archivo)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstArchivos = IcargueGtosArea.validaInformacionExcel(hoja, archivo);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }
        public void Guardar(IList<GE_TDISTRIBUCIONCARGUEGA> lstGastosArea)
        {
            try
            {
                IcargueGtosArea.guardarGastosArea(lstGastosArea);
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> cargarActuales()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstActuales = new List<DTOgenericoCargueArchivos>();
                return lstActuales = IcargueGtosArea.obtenerActuales();
            }
            catch 
            {
                
                throw;
            }
        }
    }
}