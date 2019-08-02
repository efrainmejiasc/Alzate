using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrFamiliasProductos : ApiController
    {
        IFamiliasProductos familias = new CFamiliasProductos();

        public IList<GE_TFAMILIAS_PRODUCTOS> GetAll()
        {
            try
            {
                return familias.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public GE_TFAMILIAS_PRODUCTOS GetSingle(int consecutivo)
        {
            try
            {
                return familias.GetSingle(consecutivo);
            }
            catch
            {
                throw;
            }
        }


        public GE_TFAMILIAS_PRODUCTOS GetByHijo(int hijo)
        {
            try
            {
                return familias.GetByHijo(hijo);
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TFAMILIAS_PRODUCTOS> GetByPadre(int padre)
        {
            try
            {
                return familias.GetByPadre(padre);
            }
            catch
            {
                throw;
            }
        }
    }
}