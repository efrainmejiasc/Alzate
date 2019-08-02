using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;


namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrProdDataCenter : Interfase.IVwVlrProdDataCenter
    {
        private readonly IVwVlrProdDataCenter CRUD;

       public CVwVlrProdDataCenter()
       {
           CRUD = new VwVlrProdDataCenter();
       }

       public IList<VW_VLR_PROD_DATACENTER> GetAll()
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

       public IList<VW_VLR_PROD_DATACENTER> GetAllxServ(int inServ)
       {
           try
           {
               IList<VW_VLR_PROD_DATACENTER> vw = CRUD.GetList(i => i.dinf_servidor == inServ);
               return vw;
           }
           catch
           {
               throw;
           }
       }
    }
}
