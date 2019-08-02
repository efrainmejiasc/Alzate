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
    public class CtrProductos : ApiController
    {
        IProductos prod = new CProductos();

        public IList<GE_TPRODUCTOS> GetAll()
        {
            try
            {
                return prod.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllActive()
        {
            try
            {
                return prod.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllGridView()
        {
            try
            {
                return prod.GetAllGridView();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllUsuario(string strUsuario, string strSubCat)
        {
            try
            {
                return prod.GetAllUsuario(strUsuario, strSubCat);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPRODUCTOS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat, int inCeco)
        {
            try
            {
                return prod.GetAllUsuarioxCuenta(strUsuario, strSubCat, inCeco);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllIntermedios()
        {
            try
            {
                return prod.GetAllIntermedios();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllRedistribuir()
        {
            try
            {
                return prod.GetAllRedistribuir();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllTipoComponente(string strTipoComponente)
        {
            try
            {
                return prod.GetAllTipoComponente(strTipoComponente).ToList<GE_TPRODUCTOS>();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllDirectos(string strTipoComponente)
        {
            try
            {
                return prod.GetAllDirectos(strTipoComponente).ToList<GE_TPRODUCTOS>();
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPRODUCTOS pr)
        {
            try
            {
                prod.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPRODUCTOS pr)
        {
            try
            {
                prod.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

    }
}
