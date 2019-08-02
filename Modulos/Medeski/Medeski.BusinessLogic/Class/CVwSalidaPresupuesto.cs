using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVwSalidaPresupuesto : Interfase.IVwSalidaPresupuesto
    {
        private readonly IVwSalidaPresupuesto CRUD;

        public CVwSalidaPresupuesto()
        {
            CRUD = new VwSalidaPresupuesto();   
        }

        public IList<VW_SALIDA_PRESUPUESTO> GetAllxUser(string strUsuario)
        {
            try
            {
                IList<VW_SALIDA_PRESUPUESTO> vw = CRUD.GetList(i => i.pers_usudom == strUsuario);
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_SALIDA_PRESUPUESTO> GetAll()
        {
            try
            {
                IList<VW_SALIDA_PRESUPUESTO> vw = CRUD.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public Double GetSumSalidaGastosArea()
        {
            try
            {
                double sumaGastos = Convert.ToDouble(CRUD.GetAll().Where(a => !a.sali_tipo.Contains("CE")).Sum(b => b.valor));
                return sumaGastos;
            }
            catch
            {
                throw;
            }
        }

        public Decimal GetSumItem(int inItem)
        {
            try
            {
                decimal sumaItem = Convert.ToDecimal(CRUD.GetAll().Where(a => a.iditem.Equals(inItem)).Sum(b => b.valor));
                return sumaItem;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
