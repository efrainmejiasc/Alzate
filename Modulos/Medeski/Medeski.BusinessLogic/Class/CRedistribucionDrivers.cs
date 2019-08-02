using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CRedistribucionDrivers : Interfase.IRedistribucionDrivers
    {
        private readonly IRedistribucionDrivers CRUDDISTRI = new RedistribucionDrivers();

        public void guardar(IList<GE_TREDISTRIBUCION_DRIVERS> redis)
        {
            try
            {
                // CRUDDISTRI.Add(redis.ToArray());
                
                // IList<GE_TREDISTRIBUCION_DRIVERS> array = new List<GE_TREDISTRIBUCION_DRIVERS>();
                
                foreach (GE_TREDISTRIBUCION_DRIVERS redi in redis)
                {
                    CRUDDISTRI.Add(redi);
                }
                
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> GetAllActive()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueDrivers = new List<DTOgenericoCargueArchivos>();
                
                var lstCopes = new CCargueDistribucion().GetAll().Select(x => x.cadi_co_origen).Union(new CCargueDistribucion().GetAll().Select(x => x.cadi_co_destino)).ToArray();
                
                using (var context = new Entities())
                {
                    var consulta = from drivers in context.GE_TCARGUEDRIVERS
                                   join ccosto in context.GE_TCENTROSCOSTOS on drivers.carg_ccosto equals ccosto.cost_consecutivo
                                   join cope in context.GE_TCENTROSOPERACION on ccosto.cost_centro_operacion equals cope.ceop_codigo
                                   join redi in context.GE_TREDISTRIBUCION_DRIVERS on drivers.carg_consecutivo equals redi.care_cargue_driver into tmpRedi
                                   from redis in tmpRedi.DefaultIfEmpty()

                                   where lstCopes.Contains(cope.ceop_consecutivo)                                   
                                   where drivers.GE_TPERIODOPRESUPUESTO.peri_activo == 1
                                   where drivers.carg_activo == 1
                                   
                                   select new
                                   {
                                       drivers.carg_consecutivo,
                                       drivers.GE_TPRODUCTOS.prod_codigo,
                                       drivers.carg_usuario,
                                       drivers.GE_TCOMPANIAS.comp_nombre,
                                       drivers.GE_TCENTROSCOSTOS.cost_codigo,
                                       drivers.GE_TCENTROSCOSTOS.GE_TPARAMETROS1.parm_consecutivo,
                                       drivers.GE_TCENTROSCOSTOS.cost_centro_operacion,
                                       drivers.carg_cantidad,
                                       drivers.carg_valor_adicional,
                                       drivers.carg_valor,
                                       drivers.carg_valor_distribucion,
                                       drivers.carg_valor_total,
                                       drivers.carg_sede,
                                       cope.ceop_descripcion,
                                       care_valor = redis.care_valor == null ? 0 : redis.care_valor
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();
                        objGeneric.dto_generic_id_consecutivo = item.carg_consecutivo;
                        objGeneric.dto_generic_id_parametro = item.parm_consecutivo;
                        objGeneric.dto_generic_coperaciones = item.ceop_descripcion;
                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_codigo = item.carg_usuario;
                        objGeneric.dto_generic_empresa = item.comp_nombre;
                        objGeneric.dto_generic_sede = item.carg_sede;
                        objGeneric.dto_generic_ccostos = item.cost_codigo;
                        objGeneric.dto_generic_centro_operacion = item.cost_centro_operacion;
                        objGeneric.dto_generic_cantidad = item.carg_cantidad.ToString();
                        objGeneric.dto_generic_valor = item.carg_valor;
                        objGeneric.dto_generic_valor_adicional = item.carg_valor_adicional == null ? 0 : item.carg_valor_adicional;
                        objGeneric.dto_generic_valor_distribuidos = item.carg_valor_distribucion == null ? 0 : item.carg_valor_distribucion;
                        objGeneric.dto_generic_valor_total = item.carg_valor_total;
                        objGeneric.dto_generic_valor_suma = item.care_valor;
                        
                        lstCargueDrivers.Add(objGeneric);
                    }                   
                }
                return lstCargueDrivers;
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> GetAllRedistribuidos()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueDrivers = new List<DTOgenericoCargueArchivos>();

                var lstCopes = new CCargueDistribucion().GetAll().Select(x => x.cadi_co_origen).Union(new CCargueDistribucion().GetAll().Select(x => x.cadi_co_destino)).ToArray();

                using (var context = new Entities())
                {
                    var consulta = from drivers in context.GE_TCARGUEDRIVERS
                                   join ccosto in context.GE_TCENTROSCOSTOS on drivers.carg_ccosto equals ccosto.cost_consecutivo
                                   join cope in context.GE_TCENTROSOPERACION on ccosto.cost_centro_operacion equals cope.ceop_codigo
                                   join redis in context.GE_TREDISTRIBUCION_DRIVERS on drivers.carg_consecutivo equals redis.care_cargue_driver
                                   
                                   where lstCopes.Contains(cope.ceop_consecutivo)
                                   where drivers.GE_TPERIODOPRESUPUESTO.peri_activo == 1
                                   where drivers.carg_activo == 1

                                   select new
                                   {
                                       drivers.carg_consecutivo,
                                       drivers.GE_TPRODUCTOS.prod_codigo,
                                       drivers.carg_usuario,
                                       drivers.GE_TCOMPANIAS.comp_nombre,
                                       drivers.GE_TCENTROSCOSTOS.cost_codigo,
                                       drivers.GE_TCENTROSCOSTOS.cost_centro_operacion,
                                       drivers.carg_cantidad,
                                       drivers.carg_valor_adicional,
                                       drivers.carg_valor,
                                       drivers.carg_valor_distribucion,
                                       drivers.carg_valor_total,
                                       drivers.carg_sede,
                                       cope.ceop_descripcion,
                                       care_valor = redis.care_valor == null ? 0 : redis.care_valor
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();
                        objGeneric.dto_generic_id_consecutivo = item.carg_consecutivo;
                        objGeneric.dto_generic_coperaciones = item.ceop_descripcion;
                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_codigo = item.carg_usuario;
                        objGeneric.dto_generic_empresa = item.comp_nombre;
                        objGeneric.dto_generic_sede = item.carg_sede;
                        objGeneric.dto_generic_ccostos = item.cost_codigo;
                        objGeneric.dto_generic_centro_operacion = item.cost_centro_operacion;
                        objGeneric.dto_generic_cantidad = item.carg_cantidad.ToString();
                        objGeneric.dto_generic_valor = item.carg_valor;
                        objGeneric.dto_generic_valor_adicional = item.carg_valor_adicional == null ? 0 : item.carg_valor_adicional;
                        objGeneric.dto_generic_valor_distribuidos = item.carg_valor_distribucion == null ? 0 : item.carg_valor_distribucion;
                        objGeneric.dto_generic_valor_total = item.carg_valor_total;
                        objGeneric.dto_generic_valor_suma = item.care_valor;

                        lstCargueDrivers.Add(objGeneric);
                    }
                }
                return lstCargueDrivers;
            }
            catch
            {
                throw;
            }
        }
 

        public void eliminarActivos(IList<GE_TCARGUEDRIVERS> cargue)
        {
            try
            {
                IList<GE_TREDISTRIBUCION_DRIVERS> listaDistri = new List<GE_TREDISTRIBUCION_DRIVERS>();
                
                foreach (var item in cargue)
                {
                    GE_TREDISTRIBUCION_DRIVERS itemDistri = CRUDDISTRI.GetSingle(x => x.care_cargue_driver == item.carg_consecutivo);
                    itemDistri.care_activo = "0";
                    listaDistri.Add(itemDistri);
                }

                CRUDDISTRI.Update(listaDistri.ToArray());

            }
            catch
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> GetResumenDistribucion()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueDrivers = new List<DTOgenericoCargueArchivos>();

                var lstCopes = new CCargueDistribucion().GetAll().Select(x => x.cadi_co_origen).Union(new CCargueDistribucion().GetAll().Select(x => x.cadi_co_destino)).ToArray();

                using (var context = new Entities())
                {
                    var consulta = from drivers in context.GE_TCARGUEDRIVERS
                                   join ccosto in context.GE_TCENTROSCOSTOS on drivers.carg_ccosto equals ccosto.cost_consecutivo
                                   join cope in context.GE_TCENTROSOPERACION on ccosto.cost_centro_operacion equals cope.ceop_codigo
                                   join redis in context.GE_TREDISTRIBUCION_DRIVERS on drivers.carg_consecutivo equals redis.care_cargue_driver

                                   where lstCopes.Contains(cope.ceop_consecutivo)
                                   where drivers.GE_TPERIODOPRESUPUESTO.peri_activo == 1
                                   where drivers.carg_activo == 1

                                   select new
                                   {
                                       drivers.carg_consecutivo,
                                       drivers.GE_TPRODUCTOS.prod_codigo,
                                       drivers.carg_usuario,
                                       drivers.GE_TCOMPANIAS.comp_nombre,
                                       drivers.GE_TCENTROSCOSTOS.cost_codigo,
                                       drivers.GE_TCENTROSCOSTOS.cost_centro_operacion,
                                       drivers.carg_cantidad,
                                       drivers.carg_valor_adicional,
                                       drivers.carg_valor,
                                       drivers.carg_valor_distribucion,
                                       drivers.carg_valor_total,
                                       drivers.carg_sede,
                                       cope.ceop_descripcion,
                                       care_valor = redis.care_valor == null ? 0 : redis.care_valor
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();
                        objGeneric.dto_generic_id_consecutivo = item.carg_consecutivo;
                        objGeneric.dto_generic_coperaciones = item.ceop_descripcion;
                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_codigo = item.carg_usuario;
                        objGeneric.dto_generic_empresa = item.comp_nombre;
                        objGeneric.dto_generic_sede = item.carg_sede;
                        objGeneric.dto_generic_ccostos = item.cost_codigo;
                        objGeneric.dto_generic_centro_operacion = item.cost_centro_operacion;
                        objGeneric.dto_generic_cantidad = item.carg_cantidad.ToString();
                        objGeneric.dto_generic_valor = item.carg_valor;
                        objGeneric.dto_generic_valor_adicional = item.carg_valor_adicional == null ? 0 : item.carg_valor_adicional;
                        objGeneric.dto_generic_valor_distribuidos = item.carg_valor_distribucion == null ? 0 : item.carg_valor_distribucion;
                        objGeneric.dto_generic_valor_total = item.carg_valor_total;
                        objGeneric.dto_generic_valor_suma = item.care_valor;

                        lstCargueDrivers.Add(objGeneric);
                    }
                }
                return lstCargueDrivers;
            }
            catch
            {
                throw;
            }
        }

    }
}
