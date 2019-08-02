using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrItemsDataCenter : Interfase.IVwVlrItemsDataCenter
    {
        private readonly IVwVlrItemsDataCenter CRUD;

        public CVwVlrItemsDataCenter()
       {
           CRUD = new VwVlrItemsDataCenter();   
       }

       public IList<VW_VLR_ITEMS_DATACENTER> GetAll()
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

       public IList<VW_VLR_ITEMS_DATACENTER> GetAllxServ(int inServ)
       {
           try
           {
               IList<VW_VLR_ITEMS_DATACENTER> vw = CRUD.GetList(i => i.dinf_servidor == inServ);
               return vw;
           }
           catch
           {
               throw;
           }
       }
    }
}
