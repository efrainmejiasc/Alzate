using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrItemsInfr : Interfase.IVwVlrItemsInfr
    {
       private readonly IVwVlrItemsInf CRUD;

       public CVwVlrItemsInfr()
       {
           CRUD = new VwVlrItemsInfr();   
       }

       public IList<VW_VLR_ITEMS_INFRAESTRUCTURA> GetAll()
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

       public IList<VW_VLR_ITEMS_INFRAESTRUCTURA> GetAllxServ(int inServ)
       {
           try
           {
               IList<VW_VLR_ITEMS_INFRAESTRUCTURA> vw = CRUD.GetList(i => i.dinf_servidor == inServ);
               return vw;
           }
           catch
           {
               throw;
           }
       }
    }
}
