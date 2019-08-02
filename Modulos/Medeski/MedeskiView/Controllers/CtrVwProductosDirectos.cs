using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrVwProductosDirectos : ApiController
    {
        IVwProductosDirectos productos = new CVwProductosDirectos();

        public IList<VW_PRODUCTOS_DIRECTOS> GetAll()
        {
            try
            {
                return productos.GetAll();
            }
            catch
            {
                throw;
            }
        }


        public VW_PRODUCTOS_DIRECTOS GetByProducto(string prod)
        {
            try
            {
                return productos.GetByProducto(prod);
            }
            catch
            {
                throw;
            }
        }
    }
}