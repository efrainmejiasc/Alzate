using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CDrivers : Interfase.IDrivers
    {
        private readonly IDrivers __CRUD;

        public CDrivers()
        {
            __CRUD = new Drivers();
        }

        public void Add(params GE_TDRIVERS[] objeto){
            try
            {
                __CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }

        public void Update(params GE_TDRIVERS[] objeto)
        {
            try
            {
                __CRUD.Update(objeto);
            }
            catch
            {
                throw;
            }
        }

        public GE_TDRIVERS GetSingle(int consecutivo)
        {
            try
            {
                return __CRUD.GetSingle(x => x.driv_consecutivo == consecutivo);
            }
            catch 
            {
                throw;
            }
            
        }


        public GE_TDRIVERS GetByNombre(string nombre)
        {
            try
            {
                return __CRUD.GetSingle(x => x.driv_nombre == nombre);
            }
            catch
            {
                throw;
            }

        }

        public IList<GE_TDRIVERS> GetAll()
        {
            try
            {
                return __CRUD.GetAll();
            }
            catch
            {
                throw;
            }

            
        }


        public IList<GE_TDRIVERS> GetAllActive()
        {
            try
            {
                return __CRUD.GetList(i => i.driv_activo.Equals("1"));
            }
            catch
            {
                throw;
            }


        }
    }
}
