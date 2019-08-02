using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrCargueGente : ApiController
    {
        ICargueGente ICargueGente = new CCargueGente();
        IPersonas IPersonas = new CPersonas();
        ICentroCosto ICentroCosto = new CCentroCosto();
        ICentroOperacion ICentroOperacion = new CCentroOperacion();
        IPeriodoPresupuesto IperiodoPresupuesto = new CPeriodoPresupuesto();
        CtrValidador validador = new CtrValidador();

        public void Guardar(IList<GE_TGENTE> p_lstGente)
        {
            try
            {
                ICargueGente.guardar(p_lstGente);
            }
            catch
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> organizaGridView(DataTable p_dtGente)
        {
            int v_idLinea = 1;
            try
            {
                IList<DTOgenericoCargueArchivos> lstGente = new List<DTOgenericoCargueArchivos>();

                //Cargo el Periodo Activo
                GE_TPERIODOPRESUPUESTO objPeriodoPresupuesto = new GE_TPERIODOPRESUPUESTO();
                objPeriodoPresupuesto = IperiodoPresupuesto.GetAllActive().Where(a => a.peri_activo == 1).FirstOrDefault();

                foreach (DataRow registro in p_dtGente.Rows)
                {
                    v_idLinea++;

                    string mensaje = "";
                    string aux = "";
                    DTOgenericoCargueArchivos dtoCargueGente = new DTOgenericoCargueArchivos();

                    //Paso por el validador los Centros de Costos y Centros de operación.
                    aux = validador.validaCentrosDeCosto(registro[0].ToString(), registro[5].ToString());
                    mensaje += aux == "" ? "" : aux + " " + validador.validaCentrosDeOperacion(registro[2].ToString());

                    //Paso por el validador de las personas.
                    aux = validador.validaPersona(registro[3].ToString());
                    mensaje += aux == "" ? "" : aux;

                    dtoCargueGente.dto_generic_ccostos = registro[0].ToString();
                    dtoCargueGente.dto_generic_descripcion_ccostos = registro[1].ToString();
                    dtoCargueGente.dto_generic_centro_operacion = registro[2].ToString();

                    int myInt;
                    bool tryDoc = int.TryParse(registro[3].ToString(), out myInt);
                    if (tryDoc) 
                    {
                        dtoCargueGente.dto_generic_numero_cedula = registro[3].ToString() == "" ? 0 : Convert.ToInt32(registro[3].ToString());
                    }
                    else
                    {
                        mensaje += "El Documento no es numerico";
                    }
                    
                    
                    dtoCargueGente.dto_generic_descripcion_cargo = registro[4].ToString();
                    dtoCargueGente.dto_generic_empresa = registro[5].ToString();

                    decimal myValue;
                    tryDoc = decimal.TryParse(registro[6].ToString(), out myValue);
                    if (tryDoc)
                    {
                        dtoCargueGente.dto_generic_valor_colaborador = registro[6].ToString() == "" ? 0 : Convert.ToDecimal(registro[6].ToString());
                    }
                    else
                    {
                        mensaje += "El Valor del Colaborador no es numerico";
                    }

                    tryDoc = decimal.TryParse(registro[7].ToString(), out myValue);
                    if (tryDoc)
                    {
                        dtoCargueGente.dto_generic_porcentaje_colaborador = registro[7].ToString() == "" ? 0 : Convert.ToDecimal(registro[7].ToString());
                    }
                    else
                    {
                        mensaje += "El Porcentaje del Colaborador no es numerico";
                    }

                    
                    dtoCargueGente.dto_generic_observaciones = mensaje;
                    dtoCargueGente.dto_generic_id_consecutivo_presupuesto = objPeriodoPresupuesto.peri_consecutivo;
                    dtoCargueGente.dto_generic_estado = 1;

                    lstGente.Add(dtoCargueGente);
                }

                return lstGente;
            }
            catch(Exception ex)
            {
                throw new Exception("El Archivo presenta inconsistencias " + ex.Message + " favor revisar la linea: " + v_idLinea + " del Excel");
            }
        }

        public IList<GE_TGENTE> genteOk(IList<DTOgenericoCargueArchivos> p_lstGente, String p_usuario)
        {
            try
            {
                IList<GE_TGENTE> lstGente = new List<GE_TGENTE>();

                foreach (var item in p_lstGente)
                {
                    GE_TGENTE objGente = new GE_TGENTE();

                    objGente.gent_ccostos = ICentroCosto.GetSingle(item.dto_generic_ccostos.Trim()).cost_consecutivo;
                    objGente.gent_periodo = item.dto_generic_id_consecutivo_presupuesto; 
                    objGente.gent_descripcion_ccostos = item.dto_generic_descripcion_ccostos.Trim().ToString() == null ? "" : item.dto_generic_descripcion_ccostos.Trim().ToString();
                    objGente.gent_ceop = ICentroOperacion.GetSingle(item.dto_generic_centro_operacion.Trim()).ceop_consecutivo;
                    objGente.gent_nombre_cargo = item.dto_generic_descripcion_cargo.ToString().Trim();
                    objGente.gent_empresa = item.dto_generic_empresa.Trim().ToString();
                    objGente.gent_costo_colaborador = item.dto_generic_valor_colaborador;
                    objGente.gent_persona = IPersonas.GetSingle(item.dto_generic_numero_cedula.ToString()).pers_consecutivo;
                    objGente.gent_usuario_carga = p_usuario;
                    objGente.gent_fecha_cargue = DateTime.Now;
                    objGente.gent_estado = item.dto_generic_estado;
                    objGente.gent_porcentaje_manual_dedicacion = item.dto_generic_porcentaje_colaborador;

                    lstGente.Add(objGente);

                }

                return lstGente;
            }
            catch
            {
                throw new Exception("Se presento error creando el la lista de Gente Definitiva");
            }
        }

    }
}