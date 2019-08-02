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
    public class CtrVwVlrRedistribucion : ApiController
    {
        IVwVlrRedistribucion items = new CVwVlrRedistribucion();

        public IList<VW_VLR_REDISTRIBUCION> GetAll()
        {
            try
            {
                IList<VW_VLR_REDISTRIBUCION> vw = items.GetAll();
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public IList<VW_VLR_REDISTRIBUCION> GetByIdProducto(int inProducto)
        {
            try
            {
                IList<VW_VLR_REDISTRIBUCION> vw = items.GetByIdProducto(inProducto);
                return vw;
            }
            catch
            {
                throw;
            }
        }

        public Decimal GetSumProducto(int inProducto)
        {
            try
            {
                return items.GetSumProducto(inProducto);
            }
            catch
            {
                throw;
            }
        }
    }
}
