using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrOpcionesMenu : ApiController
    {
        IOpcionesMenu opc = new COpcionesMenu();

        public IList<GE_TOPCIONESMENU> GetOpcionesMenu(int id)
        {
            try
            {
                return opc.GetOpcionesMenu(id);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TOPCIONESMENU> GetOpcionesMenuxUser(string strUser)
        {
            try
            {
                return opc.GetOpcionesMenuxUser(strUser);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TOPCIONESMENU> GetAllOpcionesMenu()
        {
            try
            {
                return opc.GetAllOpcionesMenu();
            }
            catch
            {
                throw;
            }
        }

    }
}
