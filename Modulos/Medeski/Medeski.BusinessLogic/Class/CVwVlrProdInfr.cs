using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;


namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrProdInfr : Interfase.IVwVlrProdInfr
    {
       private readonly IVwVlrProdInf CRUD;

       public CVwVlrProdInfr()
       {
           CRUD = new VwVlrProdInfr();
       }

       public IList<VW_VLR_PROD_INFRAESTRUCTURA> GetAll()
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

       public IList<VW_VLR_PROD_INFRAESTRUCTURA> GetAllxServ(int inServ)
       {
           try
           {
               IList<VW_VLR_PROD_INFRAESTRUCTURA> vw = CRUD.GetList(i => i.dinf_servidor == inServ);
               return vw;
           }
           catch
           {
               throw;
           }
       }
    }
}
