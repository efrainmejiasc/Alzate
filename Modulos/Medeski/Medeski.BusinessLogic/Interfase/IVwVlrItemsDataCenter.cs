using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrItemsDataCenter
    {
        IList<VW_VLR_ITEMS_DATACENTER> GetAll();
        IList<VW_VLR_ITEMS_DATACENTER> GetAllxServ(int inServ);
    }
}
