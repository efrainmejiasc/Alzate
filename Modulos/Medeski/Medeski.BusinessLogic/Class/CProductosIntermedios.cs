using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    internal class DistribucionIntermComparer : IEqualityComparer<GE_TDISTRIBUCIONINTERMEDIOS>
    {
        public bool Equals(GE_TDISTRIBUCIONINTERMEDIOS x, GE_TDISTRIBUCIONINTERMEDIOS y)
        {
            if (string.Equals(x.dint_consecutivo.ToString(), y.dint_consecutivo.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GE_TDISTRIBUCIONINTERMEDIOS obj)
        {
            return obj.dint_consecutivo.GetHashCode();
        }
    }
    
    public class CProductosIntermedios : Interfase.IDistribucionIntermedios
    {
        private readonly  IDistribIntermedios CRUD;

        public CProductosIntermedios()
        {
            CRUD = new DistribIntermedios();   
        }


        public IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllProductoItem(int periodo, int producto, int item)
        {
            try
            {
                return CRUD.GetList(x => x.dint_periodo == periodo && x.dint_producto_intermedio == producto && x.dint_item_intermedio == item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IList<GE_TDISTRIBUCIONINTERMEDIOS> GetAllProductosDistribuidos(int inPeriodo, int inProductoIntermedio, int inItem)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join di in context.GE_TDISTRIBUCIONINTERMEDIOS on  pr.prod_consecutivo equals di.dint_producto_directo
                                   join it in context.GE_TPRODUCTOSITEMS on di.dint_item_intermedio equals it.prit_consecutivo
                                   where di.dint_periodo == inPeriodo && di.dint_producto_intermedio == inProductoIntermedio && pr.prod_activo == 1 && it.prit_consecutivo == inItem
                                   select new
                                   {
                                       prod_consecutivo = pr.prod_consecutivo,
                                       prod_descripcion = pr.prod_descripcion,
                                       di_valor = di.dint_valor,
                                       di_consecutivo = di.dint_consecutivo
                                   };

                    IList<GE_TDISTRIBUCIONINTERMEDIOS> listaProductos = new List<GE_TDISTRIBUCIONINTERMEDIOS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TDISTRIBUCIONINTERMEDIOS d = new GE_TDISTRIBUCIONINTERMEDIOS();
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        d.dint_producto_directo = item.prod_consecutivo;
                        d.dint_producto_intermedio = inProductoIntermedio;
                        d.dint_valor = item.di_valor;
                        d.dint_consecutivo = item.di_consecutivo;
                        d.GE_TPRODUCTOS = p;
                        listaProductos.Add(d);
                    }

                    return listaProductos;
                }
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TDISTRIBUCIONINTERMEDIOS[] objeto)
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

        public void Update(params GE_TDISTRIBUCIONINTERMEDIOS[] objeto)
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
