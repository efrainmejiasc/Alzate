using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrProdInfr
    {
        IList<VW_VLR_PROD_INFRAESTRUCTURA> GetAll();
        IList<VW_VLR_PROD_INFRAESTRUCTURA> GetAllxServ(int inServ);
    }
}
