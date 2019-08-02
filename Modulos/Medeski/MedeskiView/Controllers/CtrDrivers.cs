using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrDrivers : ApiController
    {
        IDrivers IDrivers = new CDrivers();

        public IList<GE_TDRIVERS> GetAll() 
        {
            try
            {
                return IDrivers.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TDRIVERS> GetAllActive()
        {
            try
            {
                return IDrivers.GetAllActive();
            }
            catch
            {
                throw;
            }
        }
        
        
        public GE_TDRIVERS GetSingle(int consecutivo) 
        {
            try
            {
                return IDrivers.GetSingle(consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TDRIVERS driver)
        {
            try
            {
                IDrivers.Add(driver);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TDRIVERS driver)
        {
            try
            {
                IDrivers.Update(driver);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

    }
}