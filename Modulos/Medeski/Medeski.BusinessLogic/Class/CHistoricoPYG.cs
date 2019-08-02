using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CHistoricoPYG : Interfase.IHistoricoPYG
    {
        private readonly IHistoricoPYG CRUD = new HistoricoPYG();

        public IList<GE_THISTORICOPYG> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IList<GE_THISTORICOPYG> GetAllActive()
        {
            try
            {
                return CRUD.GetList(i => i.vent_activo == 1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void Add(params GE_THISTORICOPYG[] objeto)
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
        

        public void Update(params GE_THISTORICOPYG[] objeto)
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


        public void guardar(IList<GE_THISTORICOPYG> p_lstDrivers)
        {
            try
            {
                eliminarActivos(p_lstDrivers);

                IList<GE_THISTORICOPYG> array = new List<GE_THISTORICOPYG>();

                foreach (GE_THISTORICOPYG driver in p_lstDrivers)
                {
                    CRUD.Add(driver);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void eliminarActivos(IList<GE_THISTORICOPYG> cargue)
        {
            try
            {
                IList<GE_THISTORICOPYG> drivers = new List<GE_THISTORICOPYG>();

                foreach (var item in cargue)
                {
                    item.vent_activo = 0;
                    drivers.Add(item);
                }

                CRUD.Update(drivers.ToArray());
            }
            catch
            {
                throw;
            }
        }        
    }
}
