using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CGente: Interfase.IGente
    {
        private readonly IGente CRUD;
        
        public CGente()
        {
            CRUD = new Gente();
        }

        public IList<GE_TGENTE> GetAllInfo(string usuario)
        {
            try
            {
                using (var context = new Entities())
                {

                    var consulta = from persona in context.GE_TPERSONAS.DefaultIfEmpty()
                                   join pers in context.GE_TPERSONAS on persona.pers_consec_jefe equals pers.pers_consecutivo
                                   where pers.pers_usudom == usuario
                                   join param1 in context.GE_TPARAMETROS on persona.pers_empresa equals param1.parm_consecutivo
                                   join param2 in context.GE_TPARAMETROS on persona.pers_cargo equals param2.parm_consecutivo
                                   join ccostos in context.GE_TCENTROSCOSTOS on persona.pers_ccosto equals ccostos.cost_consecutivo

                                   join gente in
                                       (
                                        from gente2 in context.GE_TGENTE
                                        join periodo in context.GE_TPERIODOPRESUPUESTO on gente2.gent_periodo equals periodo.peri_consecutivo
                                        where periodo.peri_activo == 1
                                        select new
                                        {
                                            gent_consecutivo = gente2.gent_consecutivo,
                                            gent_costo_colaborador = gente2.gent_costo_colaborador,
                                            gent_porcentaje_manual_dedicacion = gente2.gent_porcentaje_manual_dedicacion,
                                            gent_estado = gente2.gent_estado,
                                            gent_persona = gente2.gent_persona,
                                            idPeriodo = periodo.peri_consecutivo,
                                            periodo = periodo.peri_ano,
                                            paso = periodo.peri_paso,
                                        }
                                   ) on persona.pers_consecutivo equals gente.gent_persona into personaGente2
                                   from gente in personaGente2.DefaultIfEmpty()
                        
                                   select new
                                   {
                                       consec = gente == null ? 0 : gente.gent_consecutivo,
                                       costo_col = gente == null ? 0 : gente.gent_costo_colaborador,
                                       porcentaje = gente == null ? 0 : gente.gent_porcentaje_manual_dedicacion,
                                       activo = gente == null ? 0 : gente.gent_estado,

                                       idPeriodo = gente == null ? 0 : gente.idPeriodo ,
                                       periodo = gente == null ? 0 : gente.periodo,

                                       idPersona = persona.pers_consecutivo,
                                       documento = persona.pers_identificacion,
                                       nombre = persona.pers_nombre,
                                       apellido = persona.pers_apellido,
                                       area = persona.pers_nombre_area,

                                       codEmpresa = param1.parm_consecutivo,
                                       empresa = param1.parm_descripcion,

                                       codCargo = param2.parm_consecutivo,
                                       cargo = param2.parm_descripcion,


                                       paso = gente == null ? 0 : gente.paso,

                                       idCcosto = ccostos.cost_consecutivo,
                                       ccosto = ccostos.cost_codigo,
                                       descCcosto = ccostos.cost_descripcion
                                   };
                    IList<GE_TGENTE> lstGentes = new List<GE_TGENTE>();

                    foreach(var item in consulta)
                    {
                        GE_TGENTE gente = new GE_TGENTE();
                        gente.gent_consecutivo = item.consec;
                        gente.gent_costo_colaborador = item.costo_col;
                        gente.gent_porcentaje_manual_dedicacion = item.porcentaje;
                        gente.gent_estado = item.activo;

                        GE_TPERSONAS p1 = new GE_TPERSONAS();
                        p1.pers_consecutivo = item.idPersona;
                        p1.pers_identificacion = item.documento;
                        p1.pers_nombre = item.nombre;
                        p1.pers_apellido = item.apellido;
                        p1.pers_nombre_area = item.area;

                        GE_TPARAMETROS para1 = new GE_TPARAMETROS();
                        para1.parm_consecutivo = item.codEmpresa;
                        para1.parm_descripcion = item.empresa;
                        p1.GE_TPARAMETROS = para1;

                        GE_TPARAMETROS para2 = new GE_TPARAMETROS();
                        para2.parm_consecutivo = item.codCargo;
                        para2.parm_descripcion = item.cargo;
                        p1.GE_TPARAMETROS1 = para2;
                        
                        gente.GE_TPERSONAS = p1;

                        GE_TPERIODOPRESUPUESTO pr = new GE_TPERIODOPRESUPUESTO();
                        pr.peri_consecutivo = item.idPeriodo; 
                        pr.peri_ano = item.periodo;
                        pr.peri_paso = item.paso;
                        gente.GE_TPERIODOPRESUPUESTO = pr;

                        GE_TCENTROSCOSTOS ccosto = new GE_TCENTROSCOSTOS();
                        ccosto.cost_consecutivo = item.idCcosto;
                        ccosto.cost_codigo = item.ccosto;
                        ccosto.cost_descripcion = item.descCcosto;
                        gente.GE_TCENTROSCOSTOS = ccosto;

                        lstGentes.Add(gente);
                    }
                    
                    return lstGentes;
                }                
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TGENTE> GetAll()
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

        public GE_TGENTE GetSingle(int consecutivo)
        {
            try
            {
                return CRUD.GetSingle(x => x.gent_consecutivo == consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TGENTE[] objeto)
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

        public void Update(params GE_TGENTE[] objeto)
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

        public GE_TGENTE getByPersonaId(int id){
            try
            {
                using (var context = new Entities())
                {

                    var consulta = from persona in context.GE_TPERSONAS.DefaultIfEmpty()
                                   join pers in context.GE_TPERSONAS on persona.pers_consec_jefe equals pers.pers_consecutivo
                                   join param1 in context.GE_TPARAMETROS on persona.pers_empresa equals param1.parm_consecutivo
                                   join param2 in context.GE_TPARAMETROS on persona.pers_cargo equals param2.parm_consecutivo
                                   join ccostos in context.GE_TCENTROSCOSTOS on persona.pers_ccosto equals ccostos.cost_consecutivo

                                   join gente in
                                       (
                                        from gente2 in context.GE_TGENTE
                                        join periodo in context.GE_TPERIODOPRESUPUESTO on gente2.gent_periodo equals periodo.peri_consecutivo
                                        where periodo.peri_activo == 1
                                        select new
                                        {
                                            gent_consecutivo = gente2.gent_consecutivo,
                                            gent_costo_colaborador = gente2.gent_costo_colaborador,
                                            gent_porcentaje_manual_dedicacion = gente2.gent_porcentaje_manual_dedicacion,
                                            gent_estado = gente2.gent_estado,
                                            gent_persona = gente2.gent_persona,
                                            idPeriodo = periodo.peri_consecutivo,
                                            periodo = periodo.peri_ano,
                                            paso = periodo.peri_paso,
                                        }
                                   ) on persona.pers_consecutivo equals gente.gent_persona into personaGente2
                                   from gente in personaGente2.DefaultIfEmpty()

                                   where persona.pers_consecutivo == id

                                   select new
                                   {
                                       consec = gente == null ? 0 : gente.gent_consecutivo,
                                       costo_col = gente == null ? 0 : gente.gent_costo_colaborador,
                                       porcentaje = gente == null ? 0 : gente.gent_porcentaje_manual_dedicacion,
                                       activo = gente == null ? 0 : gente.gent_estado,

                                       idPeriodo = gente == null ? 0 : gente.idPeriodo,
                                       periodo = gente == null ? 0 : gente.periodo,

                                       idPersona = persona.pers_consecutivo,
                                       documento = persona.pers_identificacion,
                                       nombre = persona.pers_nombre,
                                       apellido = persona.pers_apellido,
                                       area = persona.pers_nombre_area,

                                       codEmpresa = param1.parm_consecutivo,
                                       empresa = param1.parm_descripcion,

                                       codCargo = param2.parm_consecutivo,
                                       cargo = param2.parm_descripcion,


                                       paso = gente == null ? 0 : gente.paso,

                                       idCcosto = ccostos.cost_consecutivo,
                                       ccosto = ccostos.cost_codigo,
                                       descCcosto = ccostos.cost_descripcion
                                   };

                    GE_TGENTE retorno = new GE_TGENTE();

                    foreach (var item in consulta)
                    {
                        GE_TGENTE gente = new GE_TGENTE();
                        gente.gent_consecutivo = item.consec;
                        gente.gent_costo_colaborador = item.costo_col;
                        gente.gent_porcentaje_manual_dedicacion = item.porcentaje;
                        gente.gent_estado = item.activo;

                        GE_TPERSONAS p1 = new GE_TPERSONAS();
                        p1.pers_consecutivo = item.idPersona;
                        p1.pers_identificacion = item.documento;
                        p1.pers_nombre = item.nombre;
                        p1.pers_apellido = item.apellido;
                        p1.pers_nombre_area = item.area;

                        GE_TPARAMETROS para1 = new GE_TPARAMETROS();
                        para1.parm_consecutivo = item.codEmpresa;
                        para1.parm_descripcion = item.empresa;
                        p1.GE_TPARAMETROS = para1;

                        GE_TPARAMETROS para2 = new GE_TPARAMETROS();
                        para2.parm_consecutivo = item.codCargo;
                        para2.parm_descripcion = item.cargo;
                        p1.GE_TPARAMETROS1 = para2;

                        gente.GE_TPERSONAS = p1;

                        GE_TPERIODOPRESUPUESTO pr = new GE_TPERIODOPRESUPUESTO();
                        pr.peri_consecutivo = item.idPeriodo;
                        pr.peri_ano = item.periodo;
                        pr.peri_paso = item.paso;
                        gente.GE_TPERIODOPRESUPUESTO = pr;

                        GE_TCENTROSCOSTOS ccosto = new GE_TCENTROSCOSTOS();
                        ccosto.cost_consecutivo = item.idCcosto;
                        ccosto.cost_codigo = item.ccosto;
                        ccosto.cost_descripcion = item.descCcosto;
                        gente.GE_TCENTROSCOSTOS = ccosto;

                        retorno = gente;
                    }

                    return retorno;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
