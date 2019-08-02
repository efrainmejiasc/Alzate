using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrItemsInfr
    {
        IList<VW_VLR_ITEMS_INFRAESTRUCTURA> GetAll();
        IList<VW_VLR_ITEMS_INFRAESTRUCTURA> GetAllxServ(int inServ);
    }
}
