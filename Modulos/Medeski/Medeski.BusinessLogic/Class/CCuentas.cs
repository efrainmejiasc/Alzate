using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CCuentas : Interfase.ICuentas
    {
        private readonly ICuentas CRUD;

        public CCuentas()
        {
            CRUD = new Cuentas();   
        }

        public void Add(params GE_TCUENTAS[] objeto)
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

        public void Update(params GE_TCUENTAS[] objeto)
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

        public IList<GE_TCUENTAS> GetAll()
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

        public GE_TCUENTAS GetById(int inConsecutivo)
        {
            try
            {
                return CRUD.GetSingle(i => i.cuen_consecutivo == inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCUENTAS> GetAllActive()
        {
            try
            {
                return CRUD.GetList(i => i.cuen_activo == 1);
            }
            catch
            {
                throw;
            }
        }
    }
}
