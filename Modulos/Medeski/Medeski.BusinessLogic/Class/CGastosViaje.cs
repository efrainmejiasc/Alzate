using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CGastosViaje : Interfase.IGastosViaje
    {
        private readonly GastosViaje CRUD;

        public CGastosViaje()
        {
            CRUD = new GastosViaje();
        }

        public IList<GE_TCALCULOGASTOSVIAJE> GetAll()
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

        public IList<GE_TCALCULOGASTOSVIAJE> GetxGrupo(int inIdGrupo)
        {
            try
            {
                return CRUD.GetList(x => x.tari_estado == 1 && x.tari_grupo == inIdGrupo);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCALCULOGASTOSVIAJE> GetxDestino(int inIdDestino)
        {
            try
            {
                return CRUD.GetList(x => x.tari_estado == 1 && x.tari_destino == inIdDestino);
            }
            catch
            {
                throw;
            }
        }

        public GE_TCALCULOGASTOSVIAJE GetGrupoDestino(int inIdGrupo, int inIdDestino)
        {
            try
            {
                return CRUD.GetSingle(x => x.tari_estado == 1 && x.tari_grupo == inIdGrupo && x.tari_destino == inIdDestino);
            }
            catch
            {
                throw;
            }
        }
    }
}
