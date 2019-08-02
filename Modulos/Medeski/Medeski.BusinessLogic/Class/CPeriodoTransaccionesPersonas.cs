using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CPeriodoTransaccionesPersonas : Interfase.IPeriodoTransaccPersonas
    {
        private readonly IPeriodoTransaccPersonas CRUD;

        public CPeriodoTransaccionesPersonas()
        {
            CRUD = new PeriodoTransaccPersonas();   
        }

        public IList<GE_TPERIODOTRANSACCPERSONAS> GetAll()
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

        public IList<GE_TPERIODOTRANSACCPERSONAS> GetByIdPeriodoTransacc(int inPeriodoTransacc)
        {
            try
            {
                return CRUD.GetList(x => x.ptrp_periodo_transacc == inPeriodoTransacc);
            }
            catch
            {
                throw;
            }

        }

        public void Add(params GE_TPERIODOTRANSACCPERSONAS[] objeto)
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

        public void Update(params GE_TPERIODOTRANSACCPERSONAS[] objeto)
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

        public void Delete(int inPeriodoTransacc)
        {
            try
            {
                CRUD.DeleteWhere(i => i.ptrp_periodo_transacc == inPeriodoTransacc);
            }
            catch
            {
                throw;
            }
        }
    }
}
