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
    public class CtrVwSalidaPresupuesto : ApiController
    {
        IVwSalidaPresupuesto salida = new CVwSalidaPresupuesto();

        public IList<VW_SALIDA_PRESUPUESTO> GetAllxUser(string strUsuario)
        {
            try
            {
                IList<VW_SALIDA_PRESUPUESTO> vw = salida.GetAllxUser(strUsuario);
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_SALIDA_PRESUPUESTO> GetAll()
        {
            try
            {
                IList<VW_SALIDA_PRESUPUESTO> vw = salida.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public Decimal GetSumItem(int inItem)
        {
            try
            {
                return salida.GetSumItem(inItem);
            }
            catch
            {
                throw;
            }
        }
    }
}
