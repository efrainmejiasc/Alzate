using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CVlrCuadroServicio: Interfase.IVlrCuadroServicio
    {
        private readonly IVlrCuadroServicio CRUD;

        public CVlrCuadroServicio()
        {
            CRUD = new VlrCuadroServicio();
        }
        public IList<GE_TVLR_CUADRO_SERVICIO> GetAll()
        {
            try
            {
                IList<GE_TVLR_CUADRO_SERVICIO> cuadro = CRUD.GetAll();
                return cuadro;
            }
            catch
            {
                throw;
            }
        }
    }
}
