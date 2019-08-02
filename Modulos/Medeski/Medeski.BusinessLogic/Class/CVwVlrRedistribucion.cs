using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrRedistribucion : Interfase.IVwVlrRedistribucion
    {
        private readonly IVwVlrRedistribucion CRUD;

        public CVwVlrRedistribucion()
        {
            CRUD = new VwVlrRedistribucion();
        }

        public IList<VW_VLR_REDISTRIBUCION> GetAll()
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

        public IList<VW_VLR_REDISTRIBUCION> GetByIdProducto(int inProducto)
        {
            try
            {
                return CRUD.GetList(x => x.idprod == inProducto);
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public Decimal GetSumProducto(int inProducto)
        {
            try
            {
                decimal suma = Convert.ToDecimal(CRUD.GetAll().Where(a => a.idprod.Equals(inProducto)).Sum(b => b.total));
                return suma;
            }
            catch
            {
                throw;
            }
        }
    }
}
