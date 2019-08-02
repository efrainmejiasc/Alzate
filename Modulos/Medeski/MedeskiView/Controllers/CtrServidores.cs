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
    public class CtrServidores : ApiController
    {
        IServidores serv = new CServidores();

        public IList<GE_TSERVIDORES> GetAll()
        {
            try
            {
                return serv.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TSERVIDORES> GetAllGridView()
        {
            try
            {
                return serv.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TSERVIDORES> GetAllActive()
        {
            try
            {
                return serv.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public GE_TSERVIDORES GetById(int inConsecutivo)
        {
            try
            {
                return serv.GetById(inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TSERVIDORES pr)
        {
            try
            {
                serv.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TSERVIDORES pr)
        {
            try
            {
                serv.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
