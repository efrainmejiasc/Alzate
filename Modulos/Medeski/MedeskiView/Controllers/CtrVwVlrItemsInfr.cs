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
    public class CtrVwVlrItemsInfr : ApiController
    {
        IVwVlrItemsInfr items = new CVwVlrItemsInfr();

        public IList<VW_VLR_ITEMS_INFRAESTRUCTURA> GetAll()
        {
            try
            {
                IList<VW_VLR_ITEMS_INFRAESTRUCTURA> vw = items.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_VLR_ITEMS_INFRAESTRUCTURA> GetAllxServ(int inServ)
        {
            try
            {
                IList<VW_VLR_ITEMS_INFRAESTRUCTURA> vw = items.GetAllxServ(inServ);
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}
