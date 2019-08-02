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
    public class CtrCentroOperacion : ApiController
    {
        ICentroOperacion Iceop = new CCentroOperacion();
        
        public IList<GE_TCENTROSOPERACION> GetAll()
        {
            try
            {
                return Iceop.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TCENTROSOPERACION centro)
        {
            try
            {
                Iceop.Add(centro);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }
        
        
        public IHttpActionResult Update(GE_TCENTROSOPERACION centro)
        {
            try
            {
                Iceop.Update(centro);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }



        public GE_TCENTROSOPERACION GetSingle(String centro)
        {
            try
            {
                return Iceop.GetSingle(centro);
            }
            catch
            {
                throw;
            }
        }
    }
}