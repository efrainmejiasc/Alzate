using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IOpcionesMenu
    {
        IList<GE_TOPCIONESMENU> GetOpcionesMenu(int id);
        IList<GE_TOPCIONESMENU> GetOpcionesMenuxUser(string strUser);
        IList<GE_TOPCIONESMENU> GetAllOpcionesMenu();
    }
}
