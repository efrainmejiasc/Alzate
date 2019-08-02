using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace MedeskiView.Controllers
{
    public class CtrCentroCosto : ApiController
    {
        ICentroCosto Iceco = new CCentroCosto();
        
        public IList<GE_TCENTROSCOSTOS> GetAll()
        {
            try
            {
                return Iceco.GetAll();
            }
            catch
            {
                throw;
            }
        }
        
        public IList<GE_TCENTROSCOSTOS> GetAllActive()
        {
            try
            {
                return Iceco.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCENTROSCOSTOS> GetAllUsuarioCentros(string strUsuario, string strSubCateg)
        {
            try
            {
                return Iceco.GetAllUsuarioCentros(strUsuario, strSubCateg);
            }
            catch
            {
                throw;
            }
        }
        
        public IEnumerable<GE_TCENTROSCOSTOS> GetAllUsuarioxCuenta(string strUsuario, string strSubCateg)
        {
            try
            {
                return Iceco.GetAllUsuarioxCuenta(strUsuario, strSubCateg);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TCENTROSCOSTOS centro) 
        {
            try
            {
                Iceco.Add(centro);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TCENTROSCOSTOS centro)
        {
            try
            {
                Iceco.Update(centro);
                return Ok(true);
            }
            catch 
            {
                throw;
            }            
        }

        public IEnumerable<GE_TCENTROSCOSTOS> GetAllCuentaxParametros()
        {
            try 
            {
                return Iceco.GetAllCuentaxParametros();
            }
            catch
            {
                throw;
            }
        }

        public GE_TCENTROSCOSTOS GetSingle(int id)
        {
            try
            {
                return Iceco.GetSingle(id);
            }
            catch
            {
                throw;
            }
        }

    }
}
