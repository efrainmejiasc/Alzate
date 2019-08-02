using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CSalidaPresupuesto : Interfase.ISalidaPresupuesto
    {

        private readonly ISalidaPresupuesto CRUD = new SalidaPresupuesto();

        public IList<GE_TSALIDAPRESUPUESTO> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<GE_TSALIDAPRESUPUESTO> GetByPeriodoTransacc(int periodo)
        {
            try
            {
                return CRUD.GetList(x => x.sali_periodo_transacc == periodo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(params GE_TSALIDAPRESUPUESTO[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(params GE_TSALIDAPRESUPUESTO[] objeto)
        {
            try
            {
                CRUD.Update(objeto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteByPeriodoTransacc(int periodo)
        {
            try
            {
                CRUD.DeleteWhere(x => x.sali_periodo_transacc == periodo);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
