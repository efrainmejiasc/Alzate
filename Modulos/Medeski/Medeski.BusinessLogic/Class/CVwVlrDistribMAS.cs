using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrDistribMAS : Interfase.IVwVlrDistribMAS
    {
      private readonly IVwVlrDistribMAS CRUD;

       public CVwVlrDistribMAS()
       {
           CRUD = new VwVlrDistribMAS();
       }

       public IList<VW_VLR_DISTRIB_MAS> GetAll()
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
    }
}
