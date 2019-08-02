using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CFamiliasProductos : Interfase.IFamiliasProductos
    {
        private readonly IFamiliasProductos CRUD;

        public CFamiliasProductos()
        { 
            CRUD = new FamiliasProductos();
        }

        public IList<GE_TFAMILIAS_PRODUCTOS> GetAll()
        {
            try
            {
                return CRUD.GetAll();
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
                return CRUD.GetSingle(x => x.fami_consecutivo == consecutivo);
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
                return CRUD.GetSingle(x => x.fam_producto == hijo);
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
                return CRUD.GetList(x => x.fam_padre == padre);
            }
            catch
            {
                throw;
            }
        }

    }
}
