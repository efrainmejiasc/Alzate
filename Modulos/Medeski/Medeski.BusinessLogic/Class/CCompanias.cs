using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CCompanias : Interfase.ICompanias
    {
        private readonly ICompanias CRUD;

        public CCompanias()
        { 
            CRUD = new Companias();
        }

        public void Add(params GE_TCOMPANIAS[] objeto) 
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

        public void Update(params GE_TCOMPANIAS[] objeto)
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


        public GE_TCOMPANIAS GetSingle(int consecutivo) 
        {
            try
            {
                return CRUD.GetSingle(x => x.comp_consecutivo == consecutivo);
            }
            catch
            {
                throw;
            }

        }

        public IList<GE_TCOMPANIAS> GetAll()
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


        public IList<GE_TCOMPANIAS> GetAllActive()
        {
            try 
            {
                return CRUD.GetList(x => x.comp_activo == 1);
            }
            catch
            {
                throw;
            }
        }

    }
}
