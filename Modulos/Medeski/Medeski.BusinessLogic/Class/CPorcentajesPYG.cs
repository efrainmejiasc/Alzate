using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CPorcentajesPYG : Interfase.IPorcentajesPYG
    {
        private readonly IPorcentajesPYG CRUD = new PorcentajesPYG();

        public IList<GE_TPORCENTAJESPYG> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<GE_TPORCENTAJESPYG> GetAllActive()
        {
            try
            {
                return CRUD.GetList(x => x.hipo_activo == 1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(params GE_TPORCENTAJESPYG[] objeto)
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

        public void Update(params GE_TPORCENTAJESPYG[] objeto)
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
    }
}
