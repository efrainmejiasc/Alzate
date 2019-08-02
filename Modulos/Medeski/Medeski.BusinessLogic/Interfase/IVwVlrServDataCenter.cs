using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrServDataCenter
    {
        IList<VW_VLR_SERV_DATACENTER> GetAll();
        IList<VW_VLR_SERV_DATACENTER> GetAllxServ(int inServ);
    }
}
