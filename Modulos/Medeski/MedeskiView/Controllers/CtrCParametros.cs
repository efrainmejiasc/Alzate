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
    public class CtrCParametros : ApiController
    {
        IParametros Iparametros = new CParametros();

        public IList<GE_TPARAMETROS> GetAllGridView()
        {
            try
            {
                return Iparametros.GetAllGridview();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPARAMETROS> GetAll()
        {
            try
            {
                return Iparametros.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPARAMETROS> GetListbyClase(String clase)
        {
            try
            {
                return Iparametros.GetListbyClase(clase);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPARAMETROS pr)
        {
            try
            {
                Iparametros.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPARAMETROS pr)
        {
            try
            {
                Iparametros.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
