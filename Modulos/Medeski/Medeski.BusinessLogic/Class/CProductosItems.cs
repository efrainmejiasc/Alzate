using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;


namespace Medeski.BusinessLogic.Class
{
    internal class ProductoItemComparer : IEqualityComparer<GE_TPRODUCTOSITEMS>
    {
        public bool Equals(GE_TPRODUCTOSITEMS x, GE_TPRODUCTOSITEMS y)
        {
            if (string.Equals(x.prit_consecutivo.ToString(), y.prit_consecutivo.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GE_TPRODUCTOSITEMS obj)
        {
            return obj.prit_consecutivo.GetHashCode();
        }
    }

    public class CProductosItems : Interfase.IProductosItems
    {
        private readonly IProductoItems CRUD;

        public CProductosItems()
        {
            CRUD = new ProductoItems();   
        }

        public IList<GE_TPRODUCTOSITEMS> GetByProducto(int idProducto)
        {
            try
            {
                IList<GE_TPRODUCTOSITEMS> opc = CRUD.GetList(i => i.prit_producto == idProducto);
                return opc;
            }
            catch
            {
                throw;
            }

        }

        public IList<GE_TPRODUCTOSITEMS> GetAll()
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

        public IList<GE_TPRODUCTOSITEMS> GetAllGridViewXprod(int idProducto)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from prit in context.GE_TPRODUCTOSITEMS
                                   join prod in context.GE_TPRODUCTOS on prit.prit_producto equals prod.prod_consecutivo
                                   where prod.prod_consecutivo == idProducto
                                   join cuenta in context.GE_TCUENTAS on prit.prit_cuenta equals cuenta.cuen_consecutivo
                                   select new
                                   {
                                       prit_consecutivo = prit.prit_consecutivo,
                                       prit_item = prit.prit_item,
                                       prit_activo = prit.prit_activo,
                                       prit_cuenta = prit.prit_cuenta,
                                       cuenta_auxiliar = cuenta.cuen_auxiliar,
                                       cuenta_descrip = cuenta.cuen_descripcion,
                                       prit_producto = prit.prit_producto,
                                       prit_tipo = prit.prit_tipo,
                                       prod_nombre = prod.prod_descripcion

                                   };
                    IList<GE_TPRODUCTOSITEMS> listaProductos = new List<GE_TPRODUCTOSITEMS>();
                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOSITEMS pr = new GE_TPRODUCTOSITEMS();
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TCUENTAS c = new GE_TCUENTAS();
                        pr.prit_activo = item.prit_activo;
                        pr.prit_consecutivo = item.prit_consecutivo;
                        pr.prit_item = item.prit_item;
                        pr.prit_tipo = item.prit_tipo;

                        p.prod_consecutivo = item.prit_producto;
                        p.prod_descripcion = item.prod_nombre;
                        
                        c.cuen_consecutivo = (int)item.prit_cuenta;
                        c.cuen_auxiliar = item.cuenta_auxiliar;
                        c.cuen_descripcion = item.cuenta_auxiliar + "-" + item.cuenta_descrip;

                        pr.GE_TPRODUCTOS = p;
                        pr.GE_TCUENTAS = c;
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

        public IList<GE_TPRODUCTOSITEMS> GetAllGridView()
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from prit in context.GE_TPRODUCTOSITEMS
                                   join prod in context.GE_TPRODUCTOS on prit.prit_producto equals prod.prod_consecutivo
                                   join cuenta in context.GE_TCUENTAS on prit.prit_cuenta equals cuenta.cuen_consecutivo
                                   select new
                                   {
                                       prit_consecutivo = prit.prit_consecutivo,
                                       prit_item = prit.prit_item,
                                       prit_activo = prit.prit_activo,
                                       prit_cuenta = prit.prit_cuenta,
                                       cuenta_auxiliar = cuenta.cuen_auxiliar,
                                       cuenta_descrip = cuenta.cuen_descripcion,
                                       prit_producto = prit.prit_producto,
                                       prod_nombre = prod.prod_descripcion

                                   };
                    IList<GE_TPRODUCTOSITEMS> listaProductos = new List<GE_TPRODUCTOSITEMS>();
                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOSITEMS pr = new GE_TPRODUCTOSITEMS();
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TCUENTAS c = new GE_TCUENTAS();
                        pr.prit_activo = item.prit_activo;
                        pr.prit_consecutivo = item.prit_consecutivo;
                        pr.prit_item = item.prit_item;
                        p.prod_consecutivo = item.prit_producto;
                        p.prod_descripcion = item.prod_nombre;
                        c.cuen_consecutivo = (int)item.prit_cuenta;
                        c.cuen_auxiliar = item.cuenta_auxiliar;
                        c.cuen_descripcion = item.cuenta_auxiliar + "-" + item.cuenta_descrip;

                        pr.GE_TPRODUCTOS = p;
                        pr.GE_TCUENTAS = c;
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

        public IEnumerable<GE_TPRODUCTOSITEMS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat, int inCeco, int inProd)
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
                                   && pr.prod_consecutivo == inProd
                                   select new
                                   {
                                       prod_consecutivo = prit.prit_consecutivo,
                                       prod_descripcion = prit.prit_item
                                   };

                    IList<GE_TPRODUCTOSITEMS> listaProductos = new List<GE_TPRODUCTOSITEMS>();

                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOSITEMS p = new GE_TPRODUCTOSITEMS();
                        p.prit_consecutivo = item.prod_consecutivo;
                        p.prit_item = item.prod_descripcion;
                        listaProductos.Add(p);
                    }

                    return listaProductos.Distinct(new ProductoItemComparer());
                }
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TPRODUCTOSITEMS[] objeto)
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

        public void Update(params GE_TPRODUCTOSITEMS[] objeto)
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

        public GE_TPRODUCTOSITEMS GetItemxNombre(string strNombre)
        {
            try
            {
                GE_TPRODUCTOSITEMS opc = CRUD.GetSingle(i => i.prit_item == strNombre);
                return opc;
            }
            catch
            {
                throw;
            }
        }
    }
}
