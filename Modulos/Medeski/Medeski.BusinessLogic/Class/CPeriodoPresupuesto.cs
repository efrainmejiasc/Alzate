using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CPeriodoPresupuesto  : Interfase.IPeriodoPresupuesto
    {
        private readonly IPeriodoPresupuesto CRUD;

        public CPeriodoPresupuesto()
        {
            CRUD = new PeriodoPresupuesto();   
        }

        public IList<GE_TPERIODOPRESUPUESTO> GetAll()
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

        public IList<GE_TPERIODOPRESUPUESTO> GetAllActive()
        {
            try
            {
                return CRUD.GetList(i => i.peri_activo == 1);
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TPERIODOPRESUPUESTO[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }

        public void Update(params GE_TPERIODOPRESUPUESTO[] objeto)
        {
            try
            {
                CRUD.Update(objeto);
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TPERIODOPRESUPUESTO> FindByAnoEtapa(int ano, string etapa)
        {
            try
            {
                return CRUD.GetList(x => x.peri_ano == ano);
            }
            catch(Exception ex)
            {
                throw;
            }
        }



        public GE_TPERIODOPRESUPUESTO GetByAnioPaso(int ano, int etapa)
        {
            try
            {
                return CRUD.GetSingle(x => x.peri_ano == ano && x.peri_paso == etapa);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public GE_TPERIODOPRESUPUESTO GetPeriodoActivo()
        {
            try
            {
                return CRUD.GetSingle(x => x.peri_activo == 1);
            }
            catch
            {
                throw;
            }
        }


        public int LoadTransactions(int buscar, int nuevo)
        {
            try
            {
                using (var context = new Entities())
                {
                    return context.sp_DuplicarValores(buscar, nuevo);
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
