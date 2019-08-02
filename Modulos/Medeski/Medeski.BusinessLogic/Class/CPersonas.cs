using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CPersonas : Interfase.IPersonas
    {
        private readonly IPersonas CRUD;

        public CPersonas()
        {
            CRUD = new Personas();   
        }

        public IList<GE_TPERSONAS> GetAll()
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

        public IList<GE_TPERSONAS> GetAllActive()
        {
            try
            {
                return CRUD.GetList(i => i.pers_activo == 1).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERSONAS> GetAllJefe()
        {
            try
            {
                return CRUD.GetList(i => i.pers_consec_jefe == 1 && i.pers_activo == 1).ToList();
            }
            catch
            {
                throw;
            }
        }

        public GE_TPERSONAS GetbyUsuario(string strUsuario)
        {
            try
            {
                return CRUD.GetSingle(i => i.pers_usudom == strUsuario.ToUpper());
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERSONAS> GetAllActiveOrderBy()
        {
            try
            {
                return CRUD.GetList(i => i.pers_activo == 1).ToList().OrderBy(x => x.pers_nombres);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERSONAS> GetAllActiveJefeOrderBy(string strUsuario)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = (from pe in context.GE_TPERSONAS
                                    join pe1 in context.GE_TPERSONAS on pe.pers_consec_jefe equals pe1.pers_consecutivo
                                    where pe.pers_activo == 1 && pe1.pers_activo == 1 && pe1.pers_usudom == strUsuario
                                    select pe).ToList().OrderBy(x => x.pers_nombres);

                    return consulta;
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERSONAS> GetAllActivexArea(string strUsuario)
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = (from pe in context.GE_TPERSONAS
                                    join pe1 in context.GE_TPERSONAS on pe.pers_consec_jefe equals pe1.pers_consecutivo
                                    where pe.pers_activo == 1 && pe1.pers_activo == 1 && pe1.pers_usudom == strUsuario
                                    select pe).ToList()
                                    .Union(from pe in context.GE_TPERSONAS
                                           where pe.pers_activo == 1 && pe.pers_usudom == strUsuario
                                           select pe).ToList()
                                    .OrderBy(x => x.pers_nombres);

                    return consulta;
                }
            }
            catch
            {
                throw;
            }

        }

        public IList<GE_TPERSONAS> GetMisFuncionarios(int id)
        {
            try
            {
                using(var context = new Entities() ){
                    var consulta = from personas in context.GE_TPERSONAS
                                   where personas.pers_consec_jefe == id
                                   select new { personas };
                    IList<GE_TPERSONAS> lstPersonas = new List<GE_TPERSONAS>();
                    foreach (var item in consulta)
                    {
                        lstPersonas.Add(item.personas);
                    }

                    // return CRUD.GetAll(x => x.pers_consec_jefe == id);
                    return lstPersonas;
                }
                
            }
            catch
            {
                throw;
            }
        }

        public GE_TPERSONAS GetbyConsecutivo(int inConsecutivo)
        {
            try
            {
                return CRUD.GetSingle(i => i.pers_consecutivo == inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public GE_TPERSONAS GetSingle(String p_identificacionPersona)
        {
            try
            {
                return CRUD.GetSingle(a => a.pers_identificacion == p_identificacionPersona);
            }
            catch
            {
                
                throw;
            }
        }

        public void Add(params GE_TPERSONAS[] persona)
        {
            try
            {
                CRUD.Add(persona);
            }
            catch
            {
                throw;
            }
        }

        public void Update(params GE_TPERSONAS[] persona)
        {
            try
            {
                CRUD.Update(persona);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERSONAS> GetAllInfo()
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from pers in context.GE_TPERSONAS
                                   join pers1 in context.GE_TPERSONAS on pers.pers_consec_jefe equals pers1.pers_consecutivo
                                   join param1 in context.GE_TPARAMETROS on pers.pers_tipo_contrato equals param1.parm_consecutivo
                                   join param2 in context.GE_TPARAMETROS on pers.pers_metodo_distrib equals param2.parm_consecutivo
                                   join param3 in context.GE_TPARAMETROS on pers.pers_cargo equals param3.parm_consecutivo
                                   join param4 in context.GE_TPARAMETROS on pers.pers_grupo equals param4.parm_consecutivo
                                   join param5 in context.GE_TPARAMETROS on pers.pers_empresa equals param5.parm_consecutivo
                                   
                                   join param6 in context.GE_TPARAMETROS on pers.pers_nombre_area equals param6.parm_consecutivo into tmpArea
                                   from param6 in tmpArea.DefaultIfEmpty()

                                   join ccostos in context.GE_TCENTROSCOSTOS on pers.pers_ccosto equals ccostos.cost_consecutivo
                                   select new 
                                   {
                                       consec = pers.pers_consecutivo,
                                       nombres = pers.pers_nombres,
                                       nombre = pers.pers_nombre,
                                       apellido = pers.pers_apellido,
                                       tipoDoc = pers.pers_tipodoc,
                                       doc = pers.pers_identificacion,
                                       usuario = pers.pers_usudom,
                                       area = param6.parm_descripcion,
                                       nombre_busq = pers.pers_nombre_busq,
                                       activo = pers.pers_activo,
                                       jefe = pers1.pers_nombres,
                                       tipoContrato = param1.parm_descripcion,
                                       metodoDistri = param2.parm_descripcion,
                                       cargo = param3.parm_descripcion,
                                       grupo = param4.parm_descripcion,
                                       empresa = param5.parm_descripcion,
                                       ccosto = ccostos.cost_codigo,                                       
                                   };
                    
                    IList<GE_TPERSONAS> lstPersonas = new List<GE_TPERSONAS>();
                    foreach (var item in consulta)
                    {
                        GE_TPERSONAS pers = new GE_TPERSONAS();
                        pers.pers_consecutivo = item.consec;
                        pers.pers_nombres = item.nombres;
                        pers.pers_nombre = item.nombre;
                        pers.pers_apellido = item.apellido;
                        pers.pers_tipodoc = item.tipoDoc;
                        pers.pers_identificacion = item.doc;
                        pers.pers_usudom = item.usuario;

                        GE_TPARAMETROS param6 = new GE_TPARAMETROS();
                        param6.parm_descripcion = item.area;
                        pers.GE_TPARAMETROS5 = param6;
                        
                        pers.pers_nombre_busq = item.nombre_busq;
                        pers.pers_activo = item.activo;

                        GE_TPERSONAS jefe = new GE_TPERSONAS();
                        jefe.pers_nombres = item.jefe;
                        pers.GE_TPERSONAS2 = jefe;

                        GE_TPARAMETROS param1 = new GE_TPARAMETROS();
                        param1.parm_descripcion = item.tipoContrato;
                        pers.GE_TPARAMETROS = param1;

                        GE_TPARAMETROS param2 = new GE_TPARAMETROS();
                        param2.parm_descripcion = item.metodoDistri;
                        pers.GE_TPARAMETROS1 = param2;

                        GE_TPARAMETROS param3 = new GE_TPARAMETROS();
                        param3.parm_descripcion = item.cargo ;
                        pers.GE_TPARAMETROS2 = param3;

                        GE_TPARAMETROS param4 = new GE_TPARAMETROS();
                        param4.parm_descripcion = item.grupo;
                        pers.GE_TPARAMETROS3 = param4;

                        GE_TPARAMETROS param5 = new GE_TPARAMETROS();
                        param5.parm_descripcion = item.empresa;
                        pers.GE_TPARAMETROS4 = param5;
                        
                        GE_TCENTROSCOSTOS ccostos = new GE_TCENTROSCOSTOS();
                        ccostos.cost_codigo = item.ccosto;
                        pers.GE_TCENTROSCOSTOS2 = ccostos;

                        lstPersonas.Add(pers);
                    }

                    foreach(var I in lstPersonas)
                    {
                        if (I.pers_activo == 1)
                            I.pers_estadoStr = "Activo";
                        else
                            I.pers_estadoStr = "Inactivo";

                    }

                    return lstPersonas;
                }                 
            }
            catch
            {
                throw;
            }

        }
    }
}
