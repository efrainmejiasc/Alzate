using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CClaseparametros : Interfase.IClaseParametros
    {
         private readonly IClaseparametros CRUD;

        public CClaseparametros()
        {
            CRUD = new  Claseparametros();
        }

        public IList<GE_TCLASESPARAMETROS> GetAll()
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

        public void Add(params GE_TCLASESPARAMETROS[] objeto)
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

        public void Update(params GE_TCLASESPARAMETROS[] objeto)
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

        public void Remove(params GE_TCLASESPARAMETROS[] objeto)
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
