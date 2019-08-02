using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrServDataCenter : Interfase.IVwVlrServDataCenter
    {
        private readonly IVwVlrServDataCenter CRUD;

       public CVwVlrServDataCenter()
       {
           CRUD = new VwVlrServDataCenter();
       }

       public IList<VW_VLR_SERV_DATACENTER> GetAll()
       {
           try
           {
               return CRUD.GetAll();
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
               IList<VW_VLR_SERV_DATACENTER> vw = CRUD.GetList(i => i.dinf_servidor == inServ);
               return vw;
           }
           catch
           {
               throw;
           }
       }
    }
}
