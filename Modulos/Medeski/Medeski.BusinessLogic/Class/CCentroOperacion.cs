using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CCentroOperacion : Interfase.ICentroOperacion
    {
        private readonly ICentroOperacion _CRUDCEO;

        public CCentroOperacion()
        {
            _CRUDCEO = new CentroOperacion();
        }

        public void Add(params GE_TCENTROSOPERACION[] objeto)
        {
            try
            {
                _CRUDCEO.Add(objeto);
            }
            catch
            {
                throw;
            }
        }

        public void Update(params GE_TCENTROSOPERACION[] objeto)
        {
            try
            {
                _CRUDCEO.Update(objeto);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCENTROSOPERACION> GetAll()
        {
            try
            {
                return _CRUDCEO.GetAll();
            }
            catch
            {
                throw;
            }
        }


        public GE_TCENTROSOPERACION GetSingle(String p_centro_operacion)
        {
            try
            {
                return _CRUDCEO.GetSingle(x => x.ceop_codigo == p_centro_operacion);
            }
            catch
            {
                throw;
            }
        }

    }
}
