using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrGenteTecnicaProd : Interfase.IVwVlrGenteTecnicaProd
    {
        private readonly IVwVlrGenteTecnProd CRUD;

        public CVwVlrGenteTecnicaProd()
        {
            CRUD = new VwVlrGenteTecnicaProd();
        }

        public IList<String> getAllServer()
        {
            try
            {
                IList<String> lstServidoresConProductos = new List<String>();
                return lstServidoresConProductos = CRUD.GetAll().GroupBy(a => a.serv_nombre).Select(b => b.Key).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_VLR_GENTE_TECNICA_PROD> getAllByserver(String p_nombreServidor)
        {
            try
            {
                return CRUD.GetAll().Where(a => a.serv_nombre == p_nombreServidor).ToList();
            }
            catch
            {
                
                throw;
            }
        }

    }
}
