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
    public class CtrVlrsParamGrales : ApiController
    {
        IVlrsPrmgrales IVlrsPrmgrales = new CVlrsparamgrales();

        public VLRSPRMGRALES GetByClase(string strClase)
        {
            try
            {
                return IVlrsPrmgrales.GetByClase(strClase);
            }
            catch
            {
                throw;
            }
        }
    }
}
