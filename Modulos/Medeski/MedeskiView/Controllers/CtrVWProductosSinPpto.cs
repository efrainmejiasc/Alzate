using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrVWProductosSinPpto : ApiController
    {
        IVWProductosSinPpto CRUD = new CVWProductosSinPpto();

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