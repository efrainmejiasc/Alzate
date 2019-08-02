using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedeskiView.Controllers
{
    public class CtrVwVlrGenteTecnicaProd
    {
        IVwVlrGenteTecnicaProd genteTecnicaProducto = new CVwVlrGenteTecnicaProd();

        public IList<VW_VLR_GENTE_TECNICA_PROD> getAll(String p_nombreServidor)
        {
            try
            {
                return genteTecnicaProducto.getAllByserver(p_nombreServidor);
            }
            catch
            {

                throw;
            }
        }

        public IList<String> getServer()
        {
            try
            {
                IList<String> lstServidores = new List<String>();
                return lstServidores = genteTecnicaProducto.getAllServer();
            }
            catch
            {
                throw;
            }
        }
    }
}
