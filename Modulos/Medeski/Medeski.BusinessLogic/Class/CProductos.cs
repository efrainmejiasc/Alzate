using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    internal class ProductoComparer : IEqualityComparer<GE_TPRODUCTOS>
    {
        public bool Equals(GE_TPRODUCTOS x, GE_TPRODUCTOS y)
        {
            if (string.Equals(x.prod_consecutivo.ToString(), y.prod_consecutivo.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GE_TPRODUCTOS obj)
        {
            return obj.prod_consecutivo.GetHashCode();
        }
    }
    
    public class CProductos : Interfase.IProductos
    {
        private readonly IProductos CRUD;

        public CProductos()
        {
            CRUD = new Productos();   
        }

        public IList<GE_TPRODUCTOS> GetAll()
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

        public IList<GE_TPRODUCTOS> GetAllGridView()
        {
            try
            {
                 using (var context = new Entities())
                 {
                     var consulta = from pr in context.GE_TPRODUCTOS
                                    join persona in context.GE_TPERSONAS on pr.prod_responsable equals persona.pers_consecutivo
                                    join param in context.GE_TPARAMETROS on pr.prod_componente equals param.parm_consecutivo
                                    join param2 in context.GE_TPARAMETROS on pr.prod_tipo_producto equals param2.parm_consecutivo into tipoProd
                                    from tipoProd2 in tipoProd.DefaultIfEmpty()
                                    select new
                                    {
                                        prod_consecutivo = pr.prod_consecutivo,
                                        prod_descripcion = pr.prod_descripcion,
                                        prod_codigo = pr.prod_codigo,
                                        prod_criterio = pr.prod_criterio,
                                        prod_contrato = pr.prod_contrato,
                                        prod_intermedio = pr.prod_intermedio,
                                        prod_activo = pr.prod_activo,
                                        prod_componente = pr.prod_componente,
                                        prod_tipo_licencia = pr.prod_tipo_licencia,
                                        prod_servicio = pr.prod_serv_venta,
                                        prod_driver1 = pr.prod_driver1,
                                        prod_driver2 = pr.prod_driver2,
                                        pers_consecutivo = persona.pers_consecutivo,
                                        pers_nombres = persona.pers_nombres,
                                        parm_consecutivo = param.parm_consecutivo,
                                        parm_descripcion = param.parm_descripcion,
                                        prod_redistribuir = pr.prod_redistribuir,
                                        prod_tipo_producto = tipoProd2.parm_descripcion == null ? "" : tipoProd2.parm_descripcion
                                    };
                     IList<GE_TPRODUCTOS> listaProductos = new List<GE_TPRODUCTOS>();
                     foreach (var item in consulta)
                     {
                         GE_TPRODUCTOS pr = new GE_TPRODUCTOS();
                         GE_TPERSONAS p = new GE_TPERSONAS();
                         GE_TPARAMETROS para = new GE_TPARAMETROS();
                         
                         GE_TPARAMETROS para2 = new GE_TPARAMETROS();
                         para2.parm_descripcion = item.prod_tipo_producto;

                         pr.prod_activo = item.prod_activo;
                         pr.prod_codigo = item.prod_codigo;
                         pr.prod_componente = item.prod_componente;
                         pr.prod_consecutivo = item.prod_consecutivo;
                         pr.prod_contrato = item.prod_contrato;
                         pr.prod_criterio = item.prod_criterio;
                         pr.prod_descripcion = item.prod_descripcion;
                         pr.prod_intermedio = item.prod_intermedio;
                         pr.prod_tipo_licencia = item.prod_tipo_licencia;
                         pr.prod_responsable = item.pers_consecutivo;
                         pr.prod_driver1 = item.prod_driver1;
                         pr.prod_driver2 = item.prod_driver2;
                         pr.prod_serv_venta = item.prod_servicio;
                         p.pers_consecutivo = item.pers_consecutivo;
                         p.pers_nombres = item.pers_nombres;
                         para.parm_consecutivo = item.parm_consecutivo;
                         para.parm_descripcion = item.parm_descripcion;
                         pr.GE_TPERSONAS = p;
                         pr.GE_TPARAMETROS = para;
                         pr.GE_TPARAMETROS2 = para2;
                         pr.prod_redistribuir = item.prod_redistribuir;
                         listaProductos.Add(pr);
                     }
                     return listaProductos;
                 }
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllUsuario(string strUsuario, string strSubCat)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta =  from pr in context.GE_TPRODUCTOS
                                    join para in context.GE_TPARAMETROS on pr.prod_tipo equals para.parm_consecutivo
                                    join pers in context.GE_TPERSONAS on pr.prod_responsable equals pers.pers_consecutivo
                                    where pers.pers_usudom == strUsuario && para.parm_descripcion == strSubCat && pr.prod_activo == 1

                                   select new
                                   {
                                       prod_consecutivo = pr.prod_consecutivo,
                                       prod_descripcion = pr.prod_descripcion
                                   };

                    IList<GE_TPRODUCTOS> listaProductos = new List<GE_TPRODUCTOS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        listaProductos.Add(p);
                    }

                    // return listaProductos.Distinct(new ProductoComparer());
                    return listaProductos;
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPRODUCTOS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat, int inCeco)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join prit in context.GE_TPRODUCTOSITEMS on pr.prod_consecutivo equals prit.prit_producto
                                   join cccl in context.GE_TCUENTASCLASIFICACION on prit.prit_cuenta equals cccl.cucl_cuenta
                                   join ceco in context.GE_TCENTROSCOSTOS on cccl.cucl_ccosto equals ceco.cost_consecutivo
                                   join cepe in context.GE_TCENTROCOSTOPERSONA on ceco.cost_consecutivo equals cepe.cepe_ceco
                                   join cccs in context.GE_TCUENTASCLASIFICACION on cepe.cepe_ceco equals cccs.cucl_ccosto
                                   join para in context.GE_TPARAMETROS on cccs.cucl_subcategoria equals para.parm_consecutivo
                                   join pers in context.GE_TPERSONAS on cepe.cepe_pers equals pers.pers_consecutivo
                                   where pers.pers_usudom == strUsuario && para.parm_descripcion == strSubCat && ceco.cost_activo == 1
                                   && cepe.cepe_activo == 1 && prit.prit_activo == 1 && pr.prod_activo == 1 && ceco.cost_consecutivo == inCeco
                                   && pr.prod_responsable == pers.pers_consecutivo
                                   select new
                                   {
                                       prod_consecutivo = pr.prod_consecutivo,
                                       prod_descripcion = pr.prod_descripcion
                                   };

                    IList<GE_TPRODUCTOS> listaProductos = new List<GE_TPRODUCTOS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        listaProductos.Add(p);
                    }

                    return listaProductos.Distinct(new ProductoComparer());
                }
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetById(int id)
        {
            try
            {
                IList<GE_TPRODUCTOS> opc = CRUD.GetList(i => i.prod_consecutivo == id);
                return opc;
            }
            catch
            {
                throw;
            }
        }

        public GE_TPRODUCTOS GetByProducto(string producto)
        {
            try
            {
                return CRUD.GetSingle(i => i.prod_codigo == producto);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllActive()
        {
            try
            {
                IList<GE_TPRODUCTOS> opc = CRUD.GetList(i => i.prod_activo == 1);
                return opc;
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllIntermedios()
        {
            try
            {
                IList<GE_TPRODUCTOS> opc = CRUD.GetList(i => i.prod_activo == 1 && i.prod_intermedio == 1 && i.prod_interm_no_distrib == 0);
                return opc.OrderBy(x => x.prod_descripcion).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPRODUCTOS> GetAllRedistribuir()
        {
            try
            {
                IList<GE_TPRODUCTOS> opc = CRUD.GetList(i => i.prod_activo == 1 && i.prod_redistribuir == 1);
                return opc.OrderBy(x => x.prod_descripcion).ToList();
            }
            catch
            {
                throw;
            }
        }
        
        public IEnumerable<GE_TPRODUCTOS> GetAllTipoComponente(string strTipoComponente)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join pa in context.GE_TPARAMETROS on pr.prod_componente equals pa.parm_consecutivo
                                   join cl in context.GE_TCLASESPARAMETROS on pa.clap_clase equals cl.clap_clase
                                   where pr.prod_activo == 1 && pa.parm_descripcion == strTipoComponente && pr.prod_intermedio == 0
                                   select new
                                   {
                                       prod_consecutivo = pr.prod_consecutivo,
                                       prod_descripcion = pr.prod_descripcion
                                   };

                    IList<GE_TPRODUCTOS> listaProductos = new List<GE_TPRODUCTOS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        listaProductos.Add(p);
                    }

                    return listaProductos.Distinct(new ProductoComparer());
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPRODUCTOS> GetAllDirectos(string strTipoComponente)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pr in context.GE_TPRODUCTOS
                                   join pa in context.GE_TPARAMETROS on pr.prod_componente equals pa.parm_consecutivo
                                   join cl in context.GE_TCLASESPARAMETROS on pa.clap_clase equals cl.clap_clase
                                   where pr.prod_activo == 1 && pa.parm_descripcion == strTipoComponente  && pr.prod_intermedio >= 0
                                   select new
                                   {
                                       prod_consecutivo = pr.prod_consecutivo,
                                       prod_descripcion = pr.prod_descripcion
                                   };

                    IList<GE_TPRODUCTOS> listaProductos = new List<GE_TPRODUCTOS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        listaProductos.Add(p);
                    }

                    return listaProductos.Distinct(new ProductoComparer());
                }
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TPRODUCTOS[] objeto)
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

        public void Update(params GE_TPRODUCTOS[] objeto)
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

        public GE_TPRODUCTOS GetSingle(String p_producto)
        {
            try
            {
                return CRUD.GetSingle(x => x.prod_codigo == p_producto);
            }
            catch
            {
                throw;
            }
        }
        

        public GE_TPRODUCTOS GetSingle(int codigo)
        {
            try
            {
                return CRUD.GetSingle(x => x.prod_consecutivo == codigo);
            }
            catch
            {
                throw;
            }
        }
        

    }
}
