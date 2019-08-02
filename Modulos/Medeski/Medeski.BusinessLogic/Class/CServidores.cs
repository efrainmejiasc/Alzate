using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CServidores : Interfase.IServidores
    {
        private readonly IServidores CRUD;

        public CServidores()
        {
            CRUD = new Servidores();   
        }

        public IList<GE_TSERVIDORES> GetAll()
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

        public GE_TSERVIDORES GetById(int id)
        {
            try
            {
                GE_TSERVIDORES serv = CRUD.GetSingle(i => i.serv_consecutivo == id);
                return serv;
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TSERVIDORES> GetAllActive()
        {
            try
            {
                IList<GE_TSERVIDORES> serv = CRUD.GetList(i => i.serv_activo == 1);
                return serv;
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TSERVIDORES[] objeto)
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

        public void Update(params GE_TSERVIDORES[] objeto)
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

    }
}
