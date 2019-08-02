using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    internal class PeriodoTransComparer : IEqualityComparer<GE_TPERIODOTRANSACCIONES>
    {
        public bool Equals(GE_TPERIODOTRANSACCIONES x, GE_TPERIODOTRANSACCIONES y)
        {
            if (string.Equals(x.petr_consecutivo.ToString(), y.petr_consecutivo.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GE_TPERIODOTRANSACCIONES obj)
        {
            return obj.petr_consecutivo.GetHashCode();
        }
    }
    
    public class CPeriodoTransacciones : Interfase.IPeriodoTransacciones
    {
        private readonly IPeriodoTransacciones CRUD;

        public CPeriodoTransacciones()
        {
            CRUD = new PeriodoTransacciones();   
        }

        public IList<GE_TPERIODOTRANSACCIONES> GetAll()
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

        public IList<GE_TPERIODOTRANSACCIONES> GetById(int inConsecutivo)
        {
            try
            {
                return CRUD.GetList(i =>i.petr_consecutivo == inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TPERIODOTRANSACCIONES[] objeto)
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

        public void Update(params GE_TPERIODOTRANSACCIONES[] objeto)
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

        public IEnumerable<GE_TPERIODOTRANSACCIONES> GetAllGridView(string strPersona, string strSubCateg)
        {
            try
            {
                int mPeriodo = new CPeriodoPresupuesto().GetPeriodoActivo().peri_consecutivo;
                using (var context = new Entities())
                {
                    var consulta = from pet in context.GE_TPERIODOTRANSACCIONES
                                   join prit in context.GE_TPRODUCTOSITEMS on pet.petr_producto_item equals prit.prit_consecutivo
                                   join prod in context.GE_TPRODUCTOS on prit.prit_producto equals prod.prod_consecutivo
                                   join pers in context.GE_TPERSONAS on pet.petr_persona equals pers.pers_consecutivo
                                   /*join cucl in context.GE_TCUENTASCLASIFICACION
                                   on new { cuenta = prit.prit_cuenta, ceco = pet.petr_centrocosto } equals new { cuenta = cucl.cucl_cuenta, ceco = cucl.cucl_ccosto }
                                   join param in context.GE_TPARAMETROS on cucl.cucl_subcategoria equals param.parm_consecutivo*/
                                   join ceco in context.GE_TCENTROSCOSTOS on pet.petr_centrocosto equals ceco.cost_consecutivo
                                   join comp in context.GE_TCOMPANIAS on ceco.cost_empresa equals comp.comp_consecutivo
                                   join para in context.GE_TPARAMETROS on pet.petr_moneda equals para.parm_consecutivo
                                   where pet.petr_periodo == mPeriodo && pers.pers_usudom == strPersona && pet.petr_activo == 1 && pet.petr_tipo == strSubCateg  //&& param.parm_descripcion == strSubCateg
                                   select new
                                   {
                                       petr_consecutivo = pet.petr_consecutivo,
                                       petr_centrocosto = pet.petr_centrocosto,
                                       petr_producto_item = pet.petr_producto_item,
                                       prod_consecutivo = prod.prod_consecutivo,
                                       cost_descripcion = ceco.cost_descripcion,
                                       cost_empresa = ceco.cost_empresa,
                                       comp_nombre = comp.comp_nombre,
                                       prod_descripcion = prod.prod_descripcion,
                                       prit_item = prit.prit_item,
                                       petr_activo = pet.petr_activo,
                                       petr_cantidad = pet.petr_cantidad,
                                       petr_mes = pet.petr_mes,
                                       petr_moneda = pet.petr_moneda,
                                       petr_observacion = pet.petr_observacion,
                                       petr_periodo = pet.petr_periodo,
                                       petr_persona = pet.petr_persona,
                                       petr_proveedor = pet.petr_proveedor,
                                       petr_tipo_viaje = pet.petr_tipo_viaje,
                                       petr_trm = pet.petr_trm,
                                       petr_valor = pet.petr_valor,
                                       petr_valor_amortizar = pet.petr_valor_amortizar,
                                       petr_amortizar = pet.petr_amortizar,
                                       petr_desc_mon = para.parm_codigo,
                                       petr_meses_amortizar = pet.petr_meses_amortizar

                                   };
                    IList<GE_TPERIODOTRANSACCIONES> listaTransacc = new List<GE_TPERIODOTRANSACCIONES>();
                    foreach (var item in consulta)
                    {
                        GE_TCOMPANIAS comp = new GE_TCOMPANIAS();
                        GE_TPRODUCTOSITEMS pr = new GE_TPRODUCTOSITEMS();
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TCENTROSCOSTOS c = new GE_TCENTROSCOSTOS();
                        GE_TPARAMETROS pm = new GE_TPARAMETROS();
                        GE_TPERIODOTRANSACCIONES pt = new GE_TPERIODOTRANSACCIONES();
                        pt.petr_activo = item.petr_activo;
                        pt.petr_amortizar = item.petr_amortizar;
                        pt.petr_cantidad = item.petr_cantidad;

                        comp.comp_nombre = item.comp_nombre;
                        c.GE_TCOMPANIAS = comp;
                        c.cost_consecutivo = item.petr_centrocosto;
                        c.cost_descripcion = item.cost_descripcion;
                        c.cost_empresa = item.cost_empresa;

                        pt.petr_consecutivo = item.petr_consecutivo;
                        pt.petr_mes = item.petr_mes;
                        pt.petr_moneda = item.petr_moneda;
                        pt.petr_observacion = item.petr_observacion;
                        pt.petr_periodo = item.petr_periodo;
                        pt.petr_persona = item.petr_persona;
                        pr.prit_consecutivo = item.petr_producto_item;
                        pr.prit_item = item.prit_item;
                        pt.petr_proveedor = item.petr_proveedor;
                        pt.petr_tipo_viaje = item.petr_tipo_viaje;
                        pt.petr_trm = item.petr_trm;
                        pt.petr_valor = item.petr_valor;
                        pt.petr_valor_amortizar = item.petr_valor_amortizar;
                        pt.petr_meses_amortizar = item.petr_meses_amortizar;
                        pt.petr_amortizar = item.petr_amortizar;
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        pm.parm_codigo = item.petr_desc_mon;
                        pm.parm_consecutivo = item.petr_moneda;

                        pt.GE_TPRODUCTOSITEMS = pr;
                        pt.GE_TPRODUCTOSITEMS.GE_TPRODUCTOS = p;
                        pt.GE_TCENTROSCOSTOS = c;
                        pt.GE_TPARAMETROS = pm;
                        listaTransacc.Add(pt);
                    }
                    return listaTransacc.Distinct(new PeriodoTransComparer());
                }
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERIODOTRANSACCIONES> GetAllActive()
        {
            try
            {
                return CRUD.GetList(i => i.petr_activo == 1);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERIODOTRANSACCIONES> GetAllGridViewViaje(string strPersona, string strSubCateg, string strTipo)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pet in context.GE_TPERIODOTRANSACCIONES
                                   join prit in context.GE_TPRODUCTOSITEMS on pet.petr_producto_item equals prit.prit_consecutivo
                                   join prod in context.GE_TPRODUCTOS on prit.prit_producto equals prod.prod_consecutivo
                                   join pers in context.GE_TPERSONAS on pet.petr_persona equals pers.pers_consecutivo
                                   join cucl in context.GE_TCUENTASCLASIFICACION
                                   on new { cuenta = prit.prit_cuenta, ceco = pet.petr_centrocosto } equals new { cuenta = cucl.cucl_cuenta, ceco = cucl.cucl_ccosto }
                                   join param in context.GE_TPARAMETROS on cucl.cucl_subcategoria equals param.parm_consecutivo
                                   join ceco in context.GE_TCENTROSCOSTOS on pet.petr_centrocosto equals ceco.cost_consecutivo
                                   join para in context.GE_TPARAMETROS on pet.petr_moneda equals para.parm_consecutivo
                                   where pers.pers_usudom == strPersona && param.parm_descripcion == strSubCateg && pet.petr_tipo == strTipo
                                   select new
                                   {
                                       petr_consecutivo = pet.petr_consecutivo,
                                       petr_centrocosto = pet.petr_centrocosto,
                                       petr_producto_item = pet.petr_producto_item,
                                       prod_consecutivo = prod.prod_consecutivo,
                                       cost_descripcion = ceco.cost_descripcion,
                                       prod_descripcion = prod.prod_descripcion,
                                       prit_item = prit.prit_item,
                                       petr_activo = pet.petr_activo,
                                       petr_cantidad = pet.petr_cantidad,
                                       petr_mes = pet.petr_mes,
                                       petr_moneda = pet.petr_moneda,
                                       petr_observacion = pet.petr_observacion,
                                       petr_periodo = pet.petr_periodo,
                                       petr_persona = pet.petr_persona,
                                       petr_proveedor = pet.petr_proveedor,
                                       petr_tipo_viaje = pet.petr_tipo_viaje,
                                       petr_trm = pet.petr_trm,
                                       petr_valor = pet.petr_valor,
                                       petr_valor_amortizar = pet.petr_valor_amortizar,
                                       petr_amortizar = pet.petr_amortizar,
                                       petr_desc_mon = para.parm_codigo,
                                       petr_meses_amortizar = pet.petr_meses_amortizar

                                   };
                    IList<GE_TPERIODOTRANSACCIONES> listaTransacc = new List<GE_TPERIODOTRANSACCIONES>();
                    foreach (var item in consulta)
                    {
                        GE_TPRODUCTOSITEMS pr = new GE_TPRODUCTOSITEMS();
                        GE_TPRODUCTOS p = new GE_TPRODUCTOS();
                        GE_TCENTROSCOSTOS c = new GE_TCENTROSCOSTOS();
                        GE_TPARAMETROS pm = new GE_TPARAMETROS();
                        GE_TPERIODOTRANSACCIONES pt = new GE_TPERIODOTRANSACCIONES();
                        pt.petr_activo = item.petr_activo;
                        pt.petr_amortizar = item.petr_amortizar;
                        pt.petr_cantidad = item.petr_cantidad;
                        c.cost_consecutivo = item.petr_centrocosto;
                        c.cost_descripcion = item.cost_descripcion;
                        pt.petr_consecutivo = item.petr_consecutivo;
                        pt.petr_mes = item.petr_mes;
                        pt.petr_moneda = item.petr_moneda;
                        pt.petr_observacion = item.petr_observacion;
                        pt.petr_periodo = item.petr_periodo;
                        pt.petr_persona = item.petr_persona;
                        pr.prit_consecutivo = item.petr_producto_item;
                        pr.prit_item = item.prit_item;
                        pt.petr_proveedor = item.petr_proveedor;
                        pt.petr_tipo_viaje = item.petr_tipo_viaje;
                        pt.petr_trm = item.petr_trm;
                        pt.petr_valor = item.petr_valor;
                        pt.petr_valor_amortizar = item.petr_valor_amortizar;
                        pt.petr_meses_amortizar = item.petr_meses_amortizar;
                        pt.petr_amortizar = item.petr_amortizar;
                        p.prod_consecutivo = item.prod_consecutivo;
                        p.prod_descripcion = item.prod_descripcion;
                        pm.parm_codigo = item.petr_desc_mon;
                        pm.parm_consecutivo = item.petr_moneda;

                        pt.GE_TPRODUCTOSITEMS = pr;
                        pt.GE_TPRODUCTOSITEMS.GE_TPRODUCTOS = p;
                        pt.GE_TCENTROSCOSTOS = c;
                        pt.GE_TPARAMETROS = pm;
                        listaTransacc.Add(pt);
                    }
                    return listaTransacc.Distinct(new PeriodoTransComparer());
                }
            }
            catch
            {
                throw;
            }
        }

        public int LoadTransactions(int inConsecutivo)
        {
            try
            {
                using (var context = new Entities())
                {
                    return context.sp_CargarPeriodoTransaccion(inConsecutivo);
                }
            }
            catch
            {
                throw;
            }
        }

        public int LoadTransactionsPersons(int inConsecutivo)
        {
            try
            {
                using (var context = new Entities())
                {
                    return context.sp_CargarPeriodoTransaccionPersona(inConsecutivo);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
