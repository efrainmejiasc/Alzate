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
    public class CtrProductosItems : ApiController
    {
        IProductosItems prit = new CProductosItems();

        public IList<GE_TPRODUCTOSITEMS> GetAllGridViewXprod(int idProd)
        {
            try
            {
                return prit.GetAllGridViewXprod(idProd);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOSITEMS> GetAllGridView()
        {
            try
            {
                return prit.GetAllGridView();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPRODUCTOSITEMS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat, int inCeco, int inProd)
        {
            try
            {
                return prit.GetAllUsuarioxCuenta(strUsuario, strSubCat, inCeco, inProd);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPRODUCTOSITEMS pr)
        {
            try
            {
                prit.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPRODUCTOSITEMS pr)
        {
            try
            {
                prit.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public GE_TPRODUCTOSITEMS GetItemxNombre(string strNombre)
        {
            try
            {
                return prit.GetItemxNombre(strNombre);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOSITEMS> GetByProducto(int idProducto)
        {
            try
            {
                return prit.GetByProducto(idProducto);
            }
            catch
            {
                throw;
            }
        }
    }
}
