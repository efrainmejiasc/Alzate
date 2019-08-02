using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CRedistribucion : Interfase.IRedistribucion
    {
        private readonly IRedistribucion CRUD;

        public CRedistribucion()
        {
            CRUD = new Redistribucion();
        }

        public void Add(params GE_TREDISTRIBUCION[] objeto)
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

        public void Update(params GE_TREDISTRIBUCION[] objeto)
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

        public IList<GE_TREDISTRIBUCION> GetAll()
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

        public IList<GE_TREDISTRIBUCION> GetByIdProducto(int inProducto, int inPeriodo)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join re in context.GE_TREDISTRIBUCION on pr.prod_consecutivo equals re.redi_producto_orig
                                   join pr1 in context.GE_TPRODUCTOS on re.redi_producto_dist equals pr1.prod_consecutivo
                                   where re.redi_periodo == inPeriodo && pr.prod_activo == 1 && re.redi_producto_orig == inProducto
                                   select new
                                   {
                                       pr_consecutivo = pr.prod_consecutivo,
                                       pr_nombre = pr.prod_descripcion,
                                       pr_consecutivo_dis = pr1.prod_consecutivo,
                                       pr_nombre_dis = pr1.prod_descripcion,
                                       re_consecutivo = re.redi_consecutivo,
                                       re_valor = re.redi_valor
                                   };

                    IList<GE_TREDISTRIBUCION> lista = new List<GE_TREDISTRIBUCION>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TPRODUCTOS p1 = new GE_TPRODUCTOS();
                        GE_TREDISTRIBUCION d = new GE_TREDISTRIBUCION();
                        p.prod_descripcion = item.pr_nombre;
                        p.prod_consecutivo = item.pr_consecutivo;
                        p1.prod_descripcion = item.pr_nombre;
                        p1.prod_consecutivo = item.pr_consecutivo;
                        d.redi_consecutivo = item.re_consecutivo;
                        d.redi_valor = item.re_valor;
                        d.redi_periodo = inPeriodo;
                        d.redi_producto_orig = item.pr_consecutivo;
                        d.redi_producto_dist = item.pr_consecutivo_dis;
                        d.GE_TPRODUCTOS = p;
                        d.GE_TPRODUCTOS1 = p1;
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
    }
}
