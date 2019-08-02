using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CDistribucionInfraest : Interfase.IDistribucionInfraest
    {
        private readonly IDistribInfraest CRUD;

        public CDistribucionInfraest()
        {
            CRUD = new DistribInfraest();   
        }

        public IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductoItem(int periodo, int producto, int item)
        {
            try
            {
                return CRUD.GetList(x => x.dinf_periodo == periodo && x.dinf_producto == producto && x.dinf_producto_item == item);
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
                using (var context = new Entities())
                {
                    var consulta = from se in context.GE_TSERVIDORES
                                   join di in context.GE_TDISTRIBUCIONINFRAESTRUCTURA on se.serv_consecutivo equals di.dinf_servidor
                                   where di.dinf_periodo == inPeriodo && di.dinf_producto == inProductoInfraest && di.dinf_producto_item == inItem && di.dinf_tipo == strTipo && se.serv_activo == 1
                                   select new
                                   {
                                       serv_consecutivo = se.serv_consecutivo,
                                       serv_descripcion = se.serv_nombre,
                                       di_valor = di.dinf_valor,
                                       di_consecutivo = di.dinf_consecutivo
                                   };

                    IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> lista = new List<GE_TDISTRIBUCIONINFRAESTRUCTURA>();

                    foreach (var item in consulta)
                    {
                        GE_TSERVIDORES s = new GE_TSERVIDORES();
                        GE_TDISTRIBUCIONINFRAESTRUCTURA d = new GE_TDISTRIBUCIONINFRAESTRUCTURA();
                        s.serv_consecutivo = item.serv_consecutivo;
                        s.serv_nombre = item.serv_descripcion;
                        d.dinf_consecutivo = item.di_consecutivo;
                        d.dinf_valor = item.di_valor;
                        d.dinf_periodo = inPeriodo;
                        d.dinf_producto = inProductoInfraest;
                        d.dinf_producto_item = inItem;
                        d.dinf_servidor = item.serv_consecutivo;
                        d.GE_TSERVIDORES = s;
                        lista.Add(d);
                    }

                    return lista;
                }
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
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join di in context.GE_TDISTRIBUCIONINFRAESTRUCTURA on pr.prod_consecutivo equals di.dinf_producto
                                   where di.dinf_periodo == inPeriodo && di.dinf_servidor == inServidor && di.dinf_producto_item == null && di.dinf_tipo == strTipo && pr.prod_activo  == 1
                                   select new
                                   {
                                       pr_consecutivo = pr.prod_consecutivo,
                                       pr_nombre = pr.prod_descripcion,
                                       di_valor = di.dinf_valor,
                                       di_consecutivo = di.dinf_consecutivo
                                   };

                    IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> lista = new List<GE_TDISTRIBUCIONINFRAESTRUCTURA>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TDISTRIBUCIONINFRAESTRUCTURA d = new GE_TDISTRIBUCIONINFRAESTRUCTURA();
                        p.prod_descripcion = item.pr_nombre;
                        p.prod_consecutivo = item.pr_consecutivo;
                        d.dinf_consecutivo = item.di_consecutivo;
                        d.dinf_valor = item.di_valor;
                        d.dinf_periodo = inPeriodo;
                        d.dinf_producto = item.pr_consecutivo;
                        d.dinf_producto_item = null;
                        d.dinf_servidor = inServidor;
                        d.GE_TPRODUCTOS = p;
                        lista.Add(d);
                    }

                    return lista;
                }
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TDISTRIBUCIONINFRAESTRUCTURA[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }

        public void Update(params GE_TDISTRIBUCIONINFRAESTRUCTURA[] objeto)
        {
            try
            {
                CRUD.Update(objeto);
            }
            catch
            {
                throw;
            }
        }
    }
}
