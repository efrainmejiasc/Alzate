using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CParametrosGenerales : Interfase.IParametrosGenerales
    {
        private readonly IParametrosGrales CRUD;

        public CParametrosGenerales()
        {
            CRUD = new ParametrosGrales();
        }

        public IList<PARAMETROSGRALES> GetAll()
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

        public void Add(params PARAMETROSGRALES[] objeto)
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

        public void Update(params PARAMETROSGRALES[] objeto)
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

        public void Remove(params PARAMETROSGRALES[] objeto)
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

        public PARAMETROSGRALES GetByClase(string strClase)
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
