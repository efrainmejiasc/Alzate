using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrEncabDistribMAS : Interfase.IVwVlrEncabDistribMAS
    {
       private readonly IVwEncabDistribMAS CRUD;

       public CVwVlrEncabDistribMAS()
       {
           CRUD = new VwVlrEncabDistribMAS();
       }

       public IList<VW_VLR_ENCABEZ_DISTRIB_MAS> GetAll()
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
