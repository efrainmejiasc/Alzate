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
    public class CtrRedistribucion : ApiController
    {
        IRedistribucion dist = new CRedistribucion();

        public IHttpActionResult Add(GE_TREDISTRIBUCION pr)
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

        public IHttpActionResult Update(GE_TREDISTRIBUCION pr)
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

        public IList<GE_TREDISTRIBUCION> GetByIdProducto(int inProducto, int inPeriodo)
        {
            try
            {
                IList<GE_TREDISTRIBUCION> listF = new List<GE_TREDISTRIBUCION>();
                IList<GE_TREDISTRIBUCION> listD = dist.GetByIdProducto(inProducto, inPeriodo);
                CtrProductos pr = new CtrProductos();
                IList<GE_TPRODUCTOS> prD = pr.GetAllTipoComponente("DIRECTO");
                CtrVwVlrRedistribucion salida = new CtrVwVlrRedistribucion();
                Decimal dbValor = salida.GetSumProducto(inProducto);

                foreach (GE_TPRODUCTOS p in prD)
                {
                    GE_TREDISTRIBUCION d = new GE_TREDISTRIBUCION();
                    GE_TPRODUCTOS pd = new GE_TPRODUCTOS();
                    IList<GE_TREDISTRIBUCION> iBusq = listD.Where(x => x.redi_producto_dist == p.prod_consecutivo).ToList();
                    if (iBusq.Count > 0)
                    {
                        d.redi_consecutivo = iBusq[0].redi_consecutivo;
                        d.redi_periodo = inPeriodo;
                        d.redi_producto_dist = iBusq[0].redi_producto_dist;
                        d.redi_producto_orig = inProducto;
                        d.redi_valor = iBusq[0].redi_valor;
                        d.redi_valor_asignado = iBusq[0].redi_valor * dbValor;
                        d.redi_valor_producto = dbValor;
                    }
                    else
                    {
                        d.redi_consecutivo = 0;
                        d.redi_periodo = inPeriodo;
                        d.redi_producto_dist = p.prod_consecutivo;
                        d.redi_producto_orig = inProducto;
                        d.redi_valor = 0;
                        d.redi_valor_asignado = 0;
                        d.redi_valor_producto = dbValor;
                    }

                    pd.prod_consecutivo = p.prod_consecutivo;
                    pd.prod_descripcion = p.prod_descripcion;
                    pd.prod_codigo = p.prod_codigo;
                    d.GE_TPRODUCTOS = pd;

                    listF.Add(d);
                }

                return listF.OrderByDescending(x => x.redi_valor).ThenBy(x => x.GE_TPRODUCTOS.prod_descripcion).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
