using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{

    internal class CentroCostoComparer : IEqualityComparer<GE_TCENTROSCOSTOS>
    {
        public bool Equals(GE_TCENTROSCOSTOS x, GE_TCENTROSCOSTOS y)
        {
            if (string.Equals(x.cost_consecutivo.ToString(), y.cost_consecutivo.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GE_TCENTROSCOSTOS obj)
        {
            return obj.cost_consecutivo.GetHashCode();
        }
    }
    public class CCentroCosto : Interfase.ICentroCosto
    {
        private readonly ICentrocosto CRUD;

        public CCentroCosto()
        {
            CRUD = new Centrocosto();
        }

        public void Add(params GE_TCENTROSCOSTOS[] objeto)
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

        public void Update(params GE_TCENTROSCOSTOS[] objeto)
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

        public IList<GE_TCENTROSCOSTOS> GetAll()
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


        public GE_TCENTROSCOSTOS GetSingle(String p_centro_costos)
        {
            try
            {
                return CRUD.GetSingle(x => x.cost_codigo == p_centro_costos);
            }
            catch
            {
                throw;
            }
        }


        public GE_TCENTROSCOSTOS GetSingle(int id)
        {
            try
            {
                return CRUD.GetSingle(x => x.cost_consecutivo == id);
            }
            catch
            {
                throw;
            }
        }

        public GE_TCENTROSCOSTOS GetSingle(String p_centro_costos, int p_empresa)
        {
            try
            {
                return CRUD.GetSingle(x => x.cost_codigo == p_centro_costos && x.cost_empresa == p_empresa);
            }
            catch
            {
                throw;
            }
        }  

        public IList<GE_TCENTROSCOSTOS> GetAllActive()
        {
            try
            {
                return CRUD.GetList(i => i.cost_activo == 1);
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TCENTROSCOSTOS> GetAllUsuarioCentros(string strUsuario, string subcateg)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from ceco in context.GE_TCENTROSCOSTOS
                                   join cepe in context.GE_TCENTROCOSTOPERSONA on ceco.cost_consecutivo equals cepe.cepe_ceco
                                   join pers in context.GE_TPERSONAS on cepe.cepe_pers equals pers.pers_consecutivo
                                   join comp in context.GE_TCOMPANIAS on ceco.cost_empresa equals comp.comp_consecutivo
                                   where pers.pers_usudom == strUsuario.ToUpper() && cepe.cepe_tipo == subcateg 
                                   && ceco.cost_activo == 1 && ceco.cost_activo == 1
                                   && cepe.cepe_activo == 1 && comp.comp_activo == 1
                                   select new
                                   {
                                       ceco.cost_codigo,
                                       ceco.cost_consecutivo,
                                       comp.comp_consecutivo,
                                       comp.comp_nombre
                                   };
                    IList<GE_TCENTROSCOSTOS> mLista = new List<GE_TCENTROSCOSTOS>();
                    foreach (var item in consulta)
                    {
                        GE_TCENTROSCOSTOS c = new GE_TCENTROSCOSTOS();
                        c.cost_codigo = item.cost_codigo;
                        c.cost_consecutivo = item.cost_consecutivo;

                        GE_TCOMPANIAS comp = new GE_TCOMPANIAS();
                        comp.comp_consecutivo = item.comp_consecutivo;
                        comp.comp_nombre = item.comp_nombre;

                        c.GE_TCOMPANIAS = comp;

                        mLista.Add(c);
                    }
                    
                    return mLista;
                }                
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public IEnumerable<GE_TCENTROSCOSTOS> GetAllUsuarioxCuenta(string strUsuario, string strSubCat)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from ceco in context.GE_TCENTROSCOSTOS
                                   join cepe in context.GE_TCENTROCOSTOPERSONA on ceco.cost_consecutivo equals cepe.cepe_ceco
                                   join cccl in context.GE_TCUENTASCLASIFICACION on cepe.cepe_ceco equals cccl.cucl_ccosto
                                   join para in context.GE_TPARAMETROS on cccl.cucl_subcategoria equals para.parm_consecutivo
                                   join pers in context.GE_TPERSONAS on cepe.cepe_pers equals pers.pers_consecutivo
                                   where pers.pers_usudom == strUsuario && para.parm_descripcion == strSubCat && ceco.cost_activo == 1
                                   && cepe.cepe_activo == 1
                                   select new
                                   {
                                       cost_activo = ceco.cost_activo,
                                       cost_centro_operacion = ceco.cost_centro_operacion,
                                       cost_codigo = ceco.cost_codigo,
                                       cost_consec_categoria = ceco.cost_consec_categoria,
                                       cost_consec_resp_ppto = ceco.cost_consec_resp_ppto,
                                       cost_consec_responsable = ceco.cost_consec_responsable,
                                       cost_consecutivo = ceco.cost_consecutivo,
                                       cost_cuenta_especial = ceco.cost_cuenta_especial,
                                       cost_descripcion = ceco.cost_descripcion,
                                       cost_ppto_interno = ceco.cost_ppto_interno
                                   };
                    IList<GE_TCENTROSCOSTOS> listaCentros = new List<GE_TCENTROSCOSTOS>();
                    foreach (var item in consulta)
                    {
                        GE_TCENTROSCOSTOS c = new GE_TCENTROSCOSTOS();
                        c.cost_activo = item.cost_activo;
                        c.cost_centro_operacion = item.cost_centro_operacion;
                        c.cost_codigo = item.cost_codigo;
                        c.cost_consec_categoria = item.cost_consec_categoria;
                        c.cost_consec_resp_ppto = item.cost_consec_resp_ppto;
                        c.cost_consec_responsable = item.cost_consec_responsable;
                        c.cost_consecutivo = item.cost_consecutivo;
                        c.cost_cuenta_especial = item.cost_cuenta_especial;
                        c.cost_descripcion = item.cost_descripcion;
                        c.cost_ppto_interno = item.cost_ppto_interno;
                        listaCentros.Add(c);
                    }

                    return listaCentros.Distinct(new CentroCostoComparer());
                }
            }
            catch
            {
                throw;
            }
        }



        public IEnumerable<GE_TCENTROSCOSTOS> GetAllCuentaxParametros()
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from ceco in context.GE_TCENTROSCOSTOS
                                   join cepa in context.GE_TPARAMETROS on ceco.cost_tipo_cliente equals cepa.parm_consecutivo into tipoCliente
                                   join cepa2 in context.GE_TPARAMETROS on ceco.cost_tipo_distribucion equals cepa2.parm_consecutivo into tipoDistr
                                   join comp in context.GE_TCOMPANIAS on ceco.cost_empresa equals comp.comp_consecutivo into tmpComp
                                   from comp in tmpComp.DefaultIfEmpty()
                                   from cepa in tipoCliente.DefaultIfEmpty()
                                   from cepa2 in tipoDistr.DefaultIfEmpty() 
                                   //where ceco.cost_activo == 1
                                   //&& cepa.parm_estado == 1
                                   select new
                                   {
                                       cost_consecutivo = ceco.cost_consecutivo,
                                       cost_codigo = ceco.cost_codigo,
                                       cost_descripcion = ceco.cost_descripcion,
                                       cost_centro_operacion = ceco.cost_centro_operacion,
                                       cost_consec_responsable = ceco.cost_consec_responsable,
                                       cost_responsable = ceco.cost_responsable,
                                       cost_parametro = cepa.parm_descripcion == null ? "" : cepa.parm_descripcion,
                                       cost_distrib = cepa2.parm_descripcion == null ? "" : cepa2.parm_descripcion,
                                       cost_activo = ceco.cost_activo,
                                       comp_empresa = comp.comp_nombre,
                                   };
                    IList<GE_TCENTROSCOSTOS> listaCentros = new List<GE_TCENTROSCOSTOS>();
                    foreach (var item in consulta)
                    {                        
                        GE_TCENTROSCOSTOS c = new GE_TCENTROSCOSTOS();
                        c.cost_activo = item.cost_activo;
                        c.cost_consecutivo = item.cost_consecutivo;
                        c.cost_codigo = item.cost_codigo;
                        c.cost_descripcion = item.cost_descripcion;
                        c.cost_centro_operacion = item.cost_centro_operacion;
                        c.cost_responsable = item.cost_responsable;

                        GE_TPARAMETROS p = new GE_TPARAMETROS();
                        p.parm_descripcion = item.cost_parametro;
                        c.GE_TPARAMETROS = p;

                        GE_TPARAMETROS p2 = new GE_TPARAMETROS(); 
                        p2.parm_descripcion = item.cost_distrib;
                        c.GE_TPARAMETROS2 = p2;

                        GE_TCOMPANIAS comp = new GE_TCOMPANIAS();
                        comp.comp_nombre = item.comp_empresa;
                        c.GE_TCOMPANIAS = comp;

                        listaCentros.Add(c);
                    }

                    return listaCentros.Distinct(new CentroCostoComparer());
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
