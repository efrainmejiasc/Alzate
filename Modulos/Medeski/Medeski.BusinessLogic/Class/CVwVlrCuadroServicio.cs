using System.Collections.Generic;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrCuadroServicio : Interfase.IVwVlrCuadroServicio
    {
        private readonly IVwCuadroServicio CRUD;

        public CVwVlrCuadroServicio()
        {
            CRUD = new VwCuadroServicio();
        }
        public IList<VW_VLR_CUADRO_SERVICIO> GetAll()
        {
            try
            {
                IList<VW_VLR_CUADRO_SERVICIO> vw = CRUD.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }
    }
}
