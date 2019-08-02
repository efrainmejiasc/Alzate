using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueGente : Interfase.ICargueGente
    {
        //Interfaz de Centro de Operación.
        private readonly ICargueGente _CRUDGENTE;

        public CCargueGente()
        {
            _CRUDGENTE = new CargueGente();
        }

        //Guardo la Gente
        public void guardar(IList<GE_TGENTE> p_lstGente)
        {
            try
            {
                int periodo = new CPeriodoPresupuesto().GetPeriodoActivo().peri_consecutivo;
                foreach(GE_TGENTE item in p_lstGente){
                    GE_TGENTE tmp = _CRUDGENTE.GetSingle(x => x.gent_periodo == periodo && x.gent_persona == item.gent_persona && x.gent_estado == 1);
                    if (tmp != null)
                    {
                        tmp.gent_estado = 0;
                        _CRUDGENTE.Update(tmp);
                    }
                }
                _CRUDGENTE.Add(p_lstGente.ToArray());
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IList<GE_TGENTE> getAll()
        {
            try
            {
               return _CRUDGENTE.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TGENTE> getAllPeriodoActivo()
        {
            try
            {
                GE_TPERIODOPRESUPUESTO periodo = new CPeriodoPresupuesto().GetPeriodoActivo();

                using (var context = new Entities())
                {
                    var consulta = from personas in context.GE_TPERSONAS
                                   join gente in context.GE_TGENTE on personas.pers_consecutivo equals gente.gent_persona
                                   where gente.gent_periodo == periodo.peri_consecutivo && gente.gent_estado == 1                                   
                                   select new
                                   {
                                       gente.gent_ccostos,
                                       gente.gent_ceop,
                                       gente.gent_consecutivo,
                                       gente.gent_costo_colaborador,
                                       gente.gent_descripcion_ccostos,
                                       gente.gent_empresa,
                                       gente.gent_estado,
                                       gente.gent_nombre_cargo,
                                       gente.gent_periodo,
                                       gente.gent_porcentaje_manual_dedicacion,
                                       gente.gent_persona,
                                       personas.pers_nombres,
                                       personas.pers_nombre_area
                                   };

                    IList<GE_TGENTE> lstGente = new List<GE_TGENTE>();

                    foreach (var item in consulta)
                    {
                        GE_TGENTE gente = new GE_TGENTE();
                        gente.gent_ccostos = item.gent_ccostos;
                        gente.gent_ceop = item.gent_ceop;
                        gente.gent_consecutivo = item.gent_consecutivo;
                        gente.gent_costo_colaborador = item.gent_costo_colaborador;
                        gente.gent_descripcion_ccostos = item.gent_descripcion_ccostos;
                        gente.gent_empresa = item.gent_empresa;
                        gente.gent_estado = item.gent_estado;
                        gente.gent_nombre_cargo = item.gent_nombre_cargo;
                        gente.gent_periodo = item.gent_periodo;
                        gente.gent_persona = item.gent_persona;
                        gente.gent_porcentaje_manual_dedicacion = item.gent_porcentaje_manual_dedicacion;
                        
                        GE_TPERSONAS persona = new GE_TPERSONAS();
                        persona.pers_nombres = item.pers_nombres;
                        persona.pers_nombre_area = item.pers_nombre_area;

                        gente.GE_TPERSONAS = persona;

                        lstGente.Add(gente);
                    }

                    return lstGente;
                }                
            }
            catch
            {
                throw;
            }
        }
    }
}
