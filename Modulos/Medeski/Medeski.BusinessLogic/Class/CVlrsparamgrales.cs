using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CVlrsparamgrales : Interfase.IVlrsPrmgrales
    {
        private readonly IVlrprmgrales CRUD;

        public CVlrsparamgrales()
        {
            CRUD = new Vlrprmgrales();
        }

        public IList<VLRSPRMGRALES> GetAll()
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

        public void Add(params VLRSPRMGRALES[] objeto)
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

        public void Update(params VLRSPRMGRALES[] objeto)
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

        public void Remove(params VLRSPRMGRALES[] objeto)
        {
            try
            {
                CRUD.Remove(objeto);
            }
            catch
            {
                throw;
            }
        }

        public VLRSPRMGRALES GetByClase(string strClase)
        {
            try
            {
                return CRUD.GetSingle(d => d.pmgr_parametro.Equals(strClase));
            }
            catch
            {
                throw;
            }
        }

        public int GetNextSequenceValue(string nombreSequencia)
        {
            try
            {
                return CRUD.GetNextSequenceValue(nombreSequencia);
            }
            catch
            {
                throw;
            }
        }
    }
}
