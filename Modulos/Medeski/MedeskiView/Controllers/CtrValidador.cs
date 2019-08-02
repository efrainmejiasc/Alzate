using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedeskiView.Controllers
{
    /// <summary>
    /// Clase que se crea con el fin de realizar validaciones que se requieran en el sistema sobre tablas de la BD
    /// Diseña y Desarrolla : Jhon Alexis Ramirez Triana.
    /// Fecha: 30-Oct-2017.
    /// Fanalca
    /// </summary>
    public class CtrValidador
    {
        ICentroCosto IcentroCosto = new CCentroCosto();
        ICentroOperacion IcentrosOperacion = new CCentroOperacion();
        IProductos Iproductos = new CProductos();
        IPersonas Ipersonas = new CPersonas();
        ICompanias Icompanias = new CCompanias();

        public String validaCentrosDeCosto(String p_item)
        {
            try
            {
                //Obtengo el Centro de Costos
                GE_TCENTROSCOSTOS objCentroDeCostos = null;
                objCentroDeCostos = IcentroCosto.GetSingle(p_item.Trim());
                string v_respuesta = "";

                if (objCentroDeCostos != null)
                {
                    v_respuesta = objCentroDeCostos.cost_activo == 1 ? "" : "Centro de Costos Inactivo. ";
                }
                else
                {
                    v_respuesta = "Centro de Costos No existe. ";
                }
                return v_respuesta;
            }
            catch
            {
                throw;
            }
        }

        public String validaCentrosDeCosto(String p_item, String p_empresa)
        {
            try
            {
                GE_TCOMPANIAS empresa = Icompanias.GetAll().Where(x => x.comp_nombre.ToUpper().Equals(p_empresa.ToUpper())).FirstOrDefault();
                
                string v_respuesta = "";

                if (empresa != null)
                {
                    v_respuesta = empresa.comp_activo == 1 ? "" : "Empresa Inactiva. ";
                    
                    //Obtengo el Centro de Costos                
                    GE_TCENTROSCOSTOS objCentroDeCostos = null;
                    objCentroDeCostos = IcentroCosto.GetSingle(p_item, empresa.comp_consecutivo);

                    if (objCentroDeCostos != null)
                    {
                        v_respuesta = objCentroDeCostos.cost_activo == 1 ? "" : "Centro de Costos Inactivo. ";
                    }
                    else
                    {
                        v_respuesta = "Centro de Costos No existe. ";
                    }
                }
                else
                {
                    v_respuesta = "Empresa No existe. ";
                }
                
                return v_respuesta;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public String validaProductos(String p_item)
        {
            try
            {
                //Obtengo los productos
                GE_TPRODUCTOS objproductos = null;
                objproductos = Iproductos.GetSingle(p_item.Trim());
                string v_respuesta = "";

                if (objproductos != null)
                {
                    v_respuesta = objproductos.prod_activo == 1 ? "" : "Producto Inactivo. ";
                }
                else
                {
                    v_respuesta = "Producto No Existe. ";
                }
                return v_respuesta;
            }
            catch
            {

                throw;
            }
        }

        public String validaCentrosDeOperacion(String p_item)
        {
            try
            {
                //Obtengo Centros de Operación
                string v_respuesta = "";
                GE_TCENTROSOPERACION objCentrosOperacion = null;
                objCentrosOperacion = IcentrosOperacion.GetSingle(p_item.Trim());

                if (objCentrosOperacion != null)
                {
                    v_respuesta = objCentrosOperacion.ceop_activo == 1 ? "" : "Centro de Operación Inactivo. ";
                }
                else
                {
                    v_respuesta = "Centro de Operación No existe. ";
                }

                return v_respuesta;
            }
            catch
            {
                throw;
            }
        }

        public String validaPersona(String p_item)
        {
            try
            {
                //Obtengo las Personas
                string v_respuesta = "";
                GE_TPERSONAS objPersonas = null;
                objPersonas = Ipersonas.GetSingle(p_item);

                if (objPersonas != null)
                {
                    v_respuesta = objPersonas.pers_activo == 1 ? "" : "Persona Inactiva en el Sistema. ";
                }
                else
                {
                    v_respuesta = "Persona no Existe en el Maestro. ";
                }

                return v_respuesta;
            }
            catch
            {
                throw;
            }
        }

    }
}