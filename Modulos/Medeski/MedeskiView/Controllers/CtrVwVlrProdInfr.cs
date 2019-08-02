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
    public class CtrVwVlrProdInfr : ApiController
    {
        IVwVlrProdInfr prod = new CVwVlrProdInfr();

        public IList<VW_VLR_PROD_INFRAESTRUCTURA> GetAll()
        {
            try
            {
                IList<VW_VLR_PROD_INFRAESTRUCTURA> vw = prod.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_VLR_PROD_INFRAESTRUCTURA> GetAllxServ(int inServ)
        {
            try
            {
                IList<VW_VLR_PROD_INFRAESTRUCTURA> vw = prod.GetAllxServ(inServ);
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}
