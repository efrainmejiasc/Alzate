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
    public class CtrDistribMAS : ApiController
    {
        IDistribMAS dist = new CDistribMAS();

        public IList<GE_TDISTRIBUCIONMASPROCESOS> GetAllProductosDistrib(int inPeriodo)
        {
            try
            {
                IList<GE_TDISTRIBUCIONMASPROCESOS> listF = new List<GE_TDISTRIBUCIONMASPROCESOS>();
                IList<GE_TDISTRIBUCIONMASPROCESOS> listD = dist.GetAllProductosDistrib(inPeriodo);
                CtrProductos pr = new CtrProductos();
                IList<GE_TPRODUCTOS> prD = pr.GetAllDirectos("DIRECTO");
                int inCont = 0;

                foreach (GE_TPRODUCTOS p in prD)
                {
                    GE_TDISTRIBUCIONMASPROCESOS d = new GE_TDISTRIBUCIONMASPROCESOS();
                    GE_TPRODUCTOS pd = new GE_TPRODUCTOS();
                    IList<GE_TDISTRIBUCIONMASPROCESOS> iBusq = listD.Where(x => x.dmas_producto == p.prod_consecutivo).ToList();

                    if (iBusq.Count > 0)
                    {
                        d.dmas_consecutivo = iBusq[0].dmas_consecutivo;
                        d.dmas_periodo = iBusq[0].dmas_periodo;
                        d.dmas_producto = iBusq[0].dmas_producto;
                        d.dmas_valor = iBusq[0].dmas_valor;
                    }
                    else
                    {
                        d.dmas_periodo = inPeriodo;
                        d.dmas_producto = p.prod_consecutivo;
                        d.dmas_valor = 0;
                        d.dmas_consecutivo = inCont;
                        inCont--;
                    }

                    pd.prod_consecutivo = p.prod_consecutivo;
                    pd.prod_descripcion = p.prod_descripcion;
                    pd.prod_codigo = p.prod_codigo;
                    d.GE_TPRODUCTOS = pd;

                    listF.Add(d);
                }

                return listF.OrderByDescending(x => x.dmas_valor).ThenBy(x => x.GE_TPRODUCTOS.prod_descripcion).ToList();
            }
            catch
            {
                throw;
            }
        }
        
        public IHttpActionResult Add(GE_TDISTRIBUCIONMASPROCESOS pr)
        {
            try
            {
                dist.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TDISTRIBUCIONMASPROCESOS pr)
        {
            try
            {
                dist.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
