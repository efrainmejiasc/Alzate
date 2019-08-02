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
    public class CtrVwVlrServDataCenter : ApiController
    {
        IVwVlrServDataCenter items = new CVwVlrServDataCenter();

        public IList<VW_VLR_SERV_DATACENTER> GetAll()
        {
            try
            {
                IList<VW_VLR_SERV_DATACENTER> vw = items.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_VLR_SERV_DATACENTER> GetAllxServ(int inServ)
        {
            try
            {
                IList<VW_VLR_SERV_DATACENTER> vw = items.GetAllxServ(inServ);
                return vw;
            }
            catch
            {
                throw;
            }
        }
        
    }
}
