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
    public class CtrParametrosGenerales : ApiController
    {
        IParametrosGenerales IParamGrales = new CParametrosGenerales();

        public PARAMETROSGRALES GetByClase(string strClase)
        {
            try
            {
                return IParamGrales.GetByClase(strClase);
            }
            catch
            {
                throw;
            }
        }

        public IList<PARAMETROSGRALES> GetAll()
        {
            try
            {
                return IParamGrales.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
