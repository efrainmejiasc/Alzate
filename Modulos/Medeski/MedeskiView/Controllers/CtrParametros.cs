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
    public class CtrParametros : ApiController
    {
        IClaseParametros IClaseP = new CClaseparametros();
        IParametros Iparametros = new CParametros();

        public IList<GE_TCLASESPARAMETROS> GetAll()
        {
            try
            {
                return IClaseP.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPARAMETROS> GetidList(int clase)
        {
            try
            {
                return Iparametros.GetList(clase);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPARAMETROS> GetListbyClase(string strClase)
        {
            try
            {
                return Iparametros.GetListbyClase(strClase);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPARAMETROS> GetListbyClaseOrdenada(string strClase)
        {
            try
            {
                return Iparametros.GetListbyClaseOrdenada(strClase);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPARAMETROS> GetListbyClaseOrdenadaParametro(string strClase, int inConsecutivo)
        {
            try
            {
                return Iparametros.GetListbyClaseOrdenadaParametro(strClase, inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TCLASESPARAMETROS pr)
        {
            try
            {
                IClaseP.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TCLASESPARAMETROS pr)
        {
            try
            {
                IClaseP.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public GE_TPARAMETROS GetById(string idParametro)
        {
            try
            {
                return Iparametros.GetById(idParametro);
            }
            catch
            {
                throw;
            }
        }

        public GE_TPARAMETROS GetByConsecutivo(int inConsecutivo)
        {
            try
            {
                return Iparametros.GetByConsecutivo(inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPARAMETROS> GetByClaseCodigo(string strClase, string strCodigo)
        {
            try
            {
                return Iparametros.GetByClaseCodigo(strClase, strCodigo);
            }
            catch
            {
                throw;
            }
        }

    }
}
