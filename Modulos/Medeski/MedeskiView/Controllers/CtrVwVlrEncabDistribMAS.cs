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
    public class CtrVwVlrEncabDistribMAS : ApiController
    {
        IVwVlrEncabDistribMAS items = new CVwVlrEncabDistribMAS();

        public IList<VW_VLR_ENCABEZ_DISTRIB_MAS> GetAll()
        {
            try
            {
                IList<VW_VLR_ENCABEZ_DISTRIB_MAS> vw = items.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}
