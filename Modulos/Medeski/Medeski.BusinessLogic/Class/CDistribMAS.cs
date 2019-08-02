using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CDistribMAS : Interfase.IDistribMAS
    {
        private readonly IDistribMAS CRUD;

        public CDistribMAS()
        {
            CRUD = new DistribMASProcesos();   
        }

        public IList<GE_TDISTRIBUCIONMASPROCESOS> GetAllProductosDistrib(int inPeriodo)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join di in context.GE_TDISTRIBUCIONMASPROCESOS on pr.prod_consecutivo equals di.dmas_producto
                                   where di.dmas_periodo == inPeriodo && pr.prod_activo == 1
                                   select new
                                   {
                                       pr_consecutivo = pr.prod_consecutivo,
                                       pr_nombre = pr.prod_descripcion,
                                       di_valor = di.dmas_valor,
                                       di_consecutivo = di.dmas_consecutivo
                                   };

                    IList<GE_TDISTRIBUCIONMASPROCESOS> lista = new List<GE_TDISTRIBUCIONMASPROCESOS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TDISTRIBUCIONMASPROCESOS d = new GE_TDISTRIBUCIONMASPROCESOS();
                        p.prod_descripcion = item.pr_nombre;
                        p.prod_consecutivo = item.pr_consecutivo;
                        d.dmas_consecutivo = item.di_consecutivo;
                        d.dmas_valor = item.di_valor;
                        d.dmas_periodo = inPeriodo;
                        d.dmas_producto = item.pr_consecutivo;
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

        public void Add(params GE_TDISTRIBUCIONMASPROCESOS[] objeto)
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

        public void Update(params GE_TDISTRIBUCIONMASPROCESOS[] objeto)
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
