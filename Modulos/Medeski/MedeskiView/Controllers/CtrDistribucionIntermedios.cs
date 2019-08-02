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
    public class CtrDistribucionIntermedios : ApiController
    {
        IDistribucionIntermedios dist = new CProductosIntermedios();

        public IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllProductoItem(int periodo, int producto, int item)
        {
            try
            {
                return dist.GetAllProductoItem(periodo, producto, item);
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllProductosDistribuidos(int inPeriodo, int inProductoIntermedio, int inItem)
        {
            try
            {
                return dist.GetAllProductosDistribuidos(inPeriodo, inProductoIntermedio, inItem);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllDistribucion(int inPeriodo, int inProductoIntermedio, int inItem)
        {
            try
            {
                IList<GE_TDISTRIBUCIONINTERMEDIOS> listF = new List<GE_TDISTRIBUCIONINTERMEDIOS>();
                IList<GE_TDISTRIBUCIONINTERMEDIOS> listD = dist.GetAllProductosDistribuidos(inPeriodo, inProductoIntermedio, inItem);
                CtrProductos pr = new CtrProductos();
                IList<GE_TPRODUCTOS> prD = pr.GetAllTipoComponente("DIRECTO");
                CtrVwSalidaPresupuesto salida = new CtrVwSalidaPresupuesto();
                Decimal dbValor = salida.GetSumItem(inItem);

                foreach(GE_TPRODUCTOS p in prD)
                {
                    GE_TDISTRIBUCIONINTERMEDIOS d = new GE_TDISTRIBUCIONINTERMEDIOS();
                    GE_TPRODUCTOS pd = new GE_TPRODUCTOS();
                    IList<GE_TDISTRIBUCIONINTERMEDIOS> iBusq = listD.Where(x => x.dint_producto_directo == p.prod_consecutivo).ToList();
                    if (iBusq.Count > 0)
                    {
                        d.dint_producto_directo = iBusq[0].dint_producto_directo;
                        d.dint_producto_intermedio = iBusq[0].dint_producto_intermedio;
                        d.dint_item_intermedio = iBusq[0].dint_item_intermedio;
                        d.dint_valor = iBusq[0].dint_valor;
                        d.dint_valor_asignado = iBusq[0].dint_valor * dbValor;
                        d.dint_consecutivo = iBusq[0].dint_consecutivo;
                        d.dint_valor_item = dbValor;
                    }
                    else
                    {
                        d.dint_producto_directo = p.prod_consecutivo;
                        d.dint_producto_intermedio = inProductoIntermedio;
                        d.dint_item_intermedio = inItem;
                        d.dint_valor = 0;
                        d.dint_valor_asignado = 0;
                        d.dint_consecutivo = 0;
                        d.dint_valor_item = dbValor;
                    }

                    pd.prod_consecutivo = p.prod_consecutivo;
                    pd.prod_descripcion = p.prod_descripcion;
                    pd.prod_codigo = p.prod_codigo;
                    d.GE_TPRODUCTOS = pd;
                    
                    listF.Add(d);
                }

                return listF.OrderByDescending(x => x.dint_valor).ThenBy(x => x.GE_TPRODUCTOS.prod_descripcion).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TDISTRIBUCIONINTERMEDIOS pr)
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

        public IHttpActionResult Update(GE_TDISTRIBUCIONINTERMEDIOS pr)
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
