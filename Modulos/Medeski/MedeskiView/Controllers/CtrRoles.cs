using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrRoles : ApiController
    {
        IRoles CRUD = new CRoles();

        public IList<GE_TROLES> GetRoles()
        {
            try
            {
                return CRUD.GetRoles();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IList<GE_TROLES> GetRolesActivos()
        {
            try
            {
                return CRUD.GetRolesActivos();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TROLES objeto)
        {
            try
            {
                CRUD.Add(objeto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TROLES objeto)
        {
            try
            {
                CRUD.Update(objeto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}