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
    public class CtrDistribucionInfraest : ApiController
    {
        IDistribucionInfraest dist = new CDistribucionInfraest();

        public IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductoItem(int periodo, int producto, int item)
        {
            try
            {
                return dist.GetAllProductoItem(periodo, producto, item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductosDistribuidos(int inPeriodo, int inProductoInfraest, int inItem, string strTipo)
        {
            try
            {
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> listF = new List<GE_TDISTRIBUCIONINFRAESTRUCTURA>();
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> listD = dist.GetAllProductosDistribuidos(inPeriodo, inProductoInfraest, inItem, strTipo);
                CtrServidores se = new CtrServidores();
                IList<GE_TSERVIDORES> serv = se.GetAllActive();
                int inCont = 0;

                foreach(GE_TSERVIDORES s in serv)
                {
                    GE_TDISTRIBUCIONINFRAESTRUCTURA d = new GE_TDISTRIBUCIONINFRAESTRUCTURA();
                    GE_TSERVIDORES sr = new GE_TSERVIDORES();
                    IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> iBusq = listD.Where(x => x.dinf_servidor == s.serv_consecutivo).ToList();

                    if (iBusq.Count > 0)
                    {
                        d.dinf_consecutivo = iBusq[0].dinf_consecutivo;
                        d.dinf_periodo = iBusq[0].dinf_periodo;
                        d.dinf_producto = iBusq[0].dinf_producto;
                        d.dinf_producto_item = iBusq[0].dinf_producto_item;
                        d.dinf_servidor = iBusq[0].dinf_servidor;
                        d.dinf_valor = iBusq[0].dinf_valor;
                    }
                    else
                    {
                        d.dinf_periodo = inPeriodo;
                        d.dinf_producto = inProductoInfraest;
                        d.dinf_producto_item = inItem;
                        d.dinf_servidor = s.serv_consecutivo;
                        d.dinf_valor = 0;
                        d.dinf_consecutivo = inCont;
                        inCont--;
                    }

                    sr.serv_consecutivo = s.serv_consecutivo;
                    sr.serv_nombre = s.serv_nombre;
                    d.GE_TSERVIDORES = sr;

                    listF.Add(d);
                }

                return listF.OrderByDescending(x => x.dinf_valor).ThenBy(x => x.GE_TSERVIDORES.serv_nombre).ToList();
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductosDistribuidosServ(int inPeriodo, int inServidor, string strTipo)
        {
            try
            {
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> listF = new List<GE_TDISTRIBUCIONINFRAESTRUCTURA>();
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> listD = dist.GetAllProductosDistribuidosServ(inPeriodo, inServidor, strTipo);
                CtrProductos pr = new CtrProductos();
                IList<GE_TPRODUCTOS> prD = pr.GetAllDirectos("DIRECTO");
                int inCont = 0;

                foreach (GE_TPRODUCTOS p in prD)
                {
                    GE_TDISTRIBUCIONINFRAESTRUCTURA d = new GE_TDISTRIBUCIONINFRAESTRUCTURA();
                    GE_TPRODUCTOS pd = new GE_TPRODUCTOS();
                    IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> iBusq = listD.Where(x => x.dinf_producto == p.prod_consecutivo).ToList();

                    if (iBusq.Count > 0)
                    {
                        d.dinf_consecutivo = iBusq[0].dinf_consecutivo;
                        d.dinf_periodo = iBusq[0].dinf_periodo;
                        d.dinf_producto = iBusq[0].dinf_producto;
                        d.dinf_producto_item = iBusq[0].dinf_producto_item;
                        d.dinf_servidor = iBusq[0].dinf_servidor;
                        d.dinf_valor = iBusq[0].dinf_valor;
                    }
                    else
                    {
                        d.dinf_periodo = inPeriodo;
                        d.dinf_producto = p.prod_consecutivo;
                        d.dinf_producto_item = null;
                        d.dinf_servidor = inServidor;
                        d.dinf_valor = 0;
                        d.dinf_consecutivo = inCont;
                        inCont--;
                    }

                    pd.prod_consecutivo = p.prod_consecutivo;
                    pd.prod_descripcion = p.prod_descripcion;
                    pd.prod_codigo = p.prod_codigo;
                    d.GE_TPRODUCTOS = pd;

                    listF.Add(d);
                }

                return listF.OrderByDescending(x => x.dinf_valor).ThenBy(x => x.GE_TPRODUCTOS.prod_descripcion).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TDISTRIBUCIONINFRAESTRUCTURA pr)
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

        public IHttpActionResult Update(GE_TDISTRIBUCIONINFRAESTRUCTURA pr)
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
