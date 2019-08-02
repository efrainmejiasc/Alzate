using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrGastosViaje : ApiController
    {
        IGastosViaje gv = new CGastosViaje();

        public IList<GE_TCALCULOGASTOSVIAJE> GetAll()
        {
            try
            {
                return gv.GetAll();
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
                return gv.GetxGrupo(inIdGrupo);
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
                return gv.GetxDestino(inIdDestino);
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
                return gv.GetGrupoDestino(inIdGrupo, inIdDestino);
            }
            catch
            {
                throw;
            }
        }
    }
}
