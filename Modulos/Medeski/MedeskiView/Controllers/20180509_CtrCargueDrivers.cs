using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrCargueDrivers_20180509 : ApiController
    {
        ICargueDrivers ICargueDrivers = new CCargueDrives();
        CtrPeriodoPresupuesto periodo = new CtrPeriodoPresupuesto();

        public IEnumerable<DTOgenericoCargueArchivos> LeerExcel(string hoja, string archivo)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstArchivos = ICargueDrivers.informacionExcelPoi(hoja, archivo);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> organizaTreeList(IList<DTOgenericoCargueArchivos> p_lstCarg)
        {

            //Cargo la lista de los productos
            IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
            lstProductos = ICargueDrivers.obtenerProductos();

            //Cargo la lista de los centros de costos
            IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
            lstCentroCostos = ICargueDrivers.obtenerCCostos();

            //Cargo la lista de los centros de operacion
            IList<GE_TCENTROSOPERACION> lstCentroOperacion = new List<GE_TCENTROSOPERACION>();
            lstCentroOperacion = ICargueDrivers.obtenerCOperacion();

            IList<DTOgenericoCargueArchivos> lstTree = new List<DTOgenericoCargueArchivos>();

            int empresa = 0;
            int ccos = 0;
            int cop = 0;
            int prd = 0;

            var lst_Empresas = p_lstCarg.GroupBy(a => a.dto_generic_empresa).ToList();

            for (int i = 0; i < lst_Empresas.Count; i++)
            {
                DTOgenericoCargueArchivos objEmpresa = new DTOgenericoCargueArchivos();
                empresa = lstTree.Count;
                objEmpresa.dto_generic_id_consecutivo = empresa;
                objEmpresa.dto_generic_id_consecutivo_padre = -1;
                objEmpresa.dto_generic_empresa = lst_Empresas[i].Key;
                lstTree.Add(objEmpresa);

                var lst_Ccos = p_lstCarg.Where(a => a.dto_generic_empresa == lst_Empresas[i].Key).GroupBy(a => a.dto_generic_ccostos).ToList();
                for (int j = 0; j < lst_Ccos.Count; j++)
                {
                    DTOgenericoCargueArchivos objCcos = new DTOgenericoCargueArchivos();
                    ccos = lstTree.Count;
                    objCcos.dto_generic_id_consecutivo = ccos;
                    objCcos.dto_generic_id_consecutivo_padre = empresa;
                    objCcos.dto_generic_ccostos = lst_Ccos[j].Key;
                    objCcos.dto_generic_observaciones = lstCentroCostos.Any(a => a.cost_codigo.Contains(lst_Ccos[j].Key.ToString())) == true ? "" : "No existe Centro de Costos";
                    lstTree.Add(objCcos);

                    var lst_Cop = p_lstCarg.Where(a => a.dto_generic_ccostos == lst_Ccos[j].Key
                                                  && a.dto_generic_empresa == lst_Empresas[i].Key)
                                           .GroupBy(b => b.dto_generic_centro_operacion).ToList();

                    for (int k = 0; k < lst_Cop.Count; k++)
                    {
                        DTOgenericoCargueArchivos objCop = new DTOgenericoCargueArchivos();
                        cop = lstTree.Count;
                        objCop.dto_generic_id_consecutivo = cop;
                        objCop.dto_generic_id_consecutivo_padre = ccos;
                        objCop.dto_generic_centro_operacion = lst_Cop[k].Key;
                        objCop.dto_generic_observaciones = lstCentroOperacion.Any(a => a.ceop_codigo.Contains(lst_Cop[k].Key.ToString())) == true ? "" : "No existe Centro de Operación";
                        lstTree.Add(objCop);

                        var lst_Prod = p_lstCarg.Where(a => a.dto_generic_empresa == lst_Empresas[i].Key
                                && a.dto_generic_ccostos == lst_Ccos[j].Key
                                && a.dto_generic_centro_operacion == lst_Cop[k].Key)
                                .GroupBy(a => a.dto_generic_productos).ToList();

                        for (int l = 0; l < lst_Prod.Count; l++)
                        {
                            DTOgenericoCargueArchivos objProd = new DTOgenericoCargueArchivos();
                            prd = lstTree.Count;
                            objProd.dto_generic_id_consecutivo = prd;
                            objProd.dto_generic_id_consecutivo_padre = cop;
                            objProd.dto_generic_productos = lst_Prod[l].Key;
                            objProd.dto_generic_observaciones = lstProductos.Any(a => a.prod_codigo.Contains(lst_Prod[l].Key.ToString())) == true ? "" : "No existe Producto";
                            lstTree.Add(objProd);

                            var lst_Usuario = p_lstCarg.Where(a => a.dto_generic_empresa == lst_Empresas[i].Key
                                                                && a.dto_generic_ccostos == lst_Ccos[j].Key
                                                                && a.dto_generic_centro_operacion == lst_Cop[k].Key
                                                                                     && a.dto_generic_productos == lst_Prod[l].Key)
                                                       .GroupBy(a => a.dto_generic_usuario_carga).ToList();

                            for (int m = 0; m < lst_Usuario.Count; m++)
                            {
                                //Valido que la cantidad sea mayor que 0 para que lo tenga en cuenta en la pantalla.
                                int cantidad = Convert.ToInt32(p_lstCarg.Where(a => a.dto_generic_empresa == lst_Empresas[i].Key
                                                                                     && a.dto_generic_ccostos == lst_Ccos[j].Key
                                                                && a.dto_generic_centro_operacion == lst_Cop[k].Key
                                                                                     && a.dto_generic_productos == lst_Prod[l].Key
                                                                                     && a.dto_generic_usuario_carga == lst_Usuario[m].Key)
                                                                             .Sum(a => Convert.ToInt32(a.dto_generic_cantidad)).ToString());

                                if (cantidad > 0)
                                {
                                    DTOgenericoCargueArchivos objUsu = new DTOgenericoCargueArchivos();
                                    objUsu.dto_generic_id_consecutivo = lstTree.Count;
                                    objUsu.dto_generic_id_consecutivo_padre = prd;
                                    objUsu.dto_generic_cantidad = cantidad.ToString();
                                    objUsu.dto_generic_usuario_carga = lst_Usuario[m].Key;
                                    lstTree.Add(objUsu);
                                }
                            }
                        }
                    }
                }
            }
            return lstTree;
        }

        public IList<DTOgenericoCargueArchivos> organizaGridview(IList<DTOgenericoCargueArchivos> p_lstCarg)
        {
            //Cargo la lista de los productos
            IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
            lstProductos = ICargueDrivers.obtenerProductos();

            //Cargo la lista de los centros de costos
            IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
            lstCentroCostos = ICargueDrivers.obtenerCCostos();

            //Cargo la lista de los centros de operacion
            IList<GE_TCENTROSOPERACION> lstCentroOperacion = new List<GE_TCENTROSOPERACION>();
            lstCentroOperacion = ICargueDrivers.obtenerCOperacion();

            IList<DTOgenericoCargueArchivos> lstGrid = new List<DTOgenericoCargueArchivos>();


            foreach (var item in p_lstCarg)
            {
                string v_observaciones = "";
                string[] v_split;
                string v_remplazar = "";
                int v_consecutivo= 1;
                string v_existeCentroCostos = "";
                string v_estaActivoCentroCostos = "";
                string v_existeCentroDeOperacion = "";
                string v_estaActivoCentroOperacion = "";
                string v_existeProducto = "";
                string v_estaActivoProducto = "";

                DTOgenericoCargueArchivos objDrivers = new DTOgenericoCargueArchivos();

                objDrivers.dto_generic_id_consecutivo = v_consecutivo;
                objDrivers.dto_generic_empresa = item.dto_generic_empresa;

                v_existeCentroCostos = lstCentroOperacion.Any(a => a.ceop_codigo == item.dto_generic_ccostos) == true ? "Existe" : "No Existe";
                v_existeCentroDeOperacion = lstCentroOperacion.Any(b => b.ceop_codigo == item.dto_generic_centro_operacion) == true ? "Existe" : "No Existe";
                v_existeProducto = lstProductos.Any(c => c.prod_codigo == item.dto_generic_productos) == true ? "Existe" : "No Existe";

                if (v_existeCentroCostos.Equals("Existe"))
                {
                    v_estaActivoCentroCostos = lstCentroCostos.Any(d => d.cost_codigo == item.dto_generic_ccostos && d.cost_activo == 1) == true ? "" : "Centro de costos Inactivo,";
                    v_existeCentroCostos = "";

                }
                else
                {
                    v_existeCentroCostos = " Centro de Costos No Existe,";
                }

                if (v_existeCentroDeOperacion.Equals("Existe"))
                {
                    v_estaActivoCentroOperacion = lstCentroOperacion.Any(e => e.ceop_codigo == item.dto_generic_centro_operacion && e.ceop_activo == 1) == true ? "" : "Centro de Operación Inactivo,";
                    v_existeCentroDeOperacion = "";
                }
                else
                {
                    v_existeCentroDeOperacion = " Centro de Operación No existe,";
                }


                if (v_existeProducto.Equals("Existe"))
                {
                    v_estaActivoProducto = lstProductos.Any(f => f.prod_codigo == item.dto_generic_productos && f.prod_activo == 1) == true ? "" : "Producto No Existe,";
                    v_existeProducto = "";
                }
                else
                {
                    v_existeProducto = " Producto no Existe,";
                }

                v_observaciones = v_existeCentroCostos + v_estaActivoCentroCostos + v_existeCentroDeOperacion + v_estaActivoCentroOperacion + v_existeProducto + v_estaActivoProducto;
                v_split = v_observaciones.Split(',');
                v_remplazar = v_split[v_split.Count() - 2];
                v_observaciones = v_observaciones.Replace(v_remplazar + ",", v_remplazar + ".");

                objDrivers.dto_generic_ccostos = item.dto_generic_ccostos;
                objDrivers.dto_generic_centro_operacion = item.dto_generic_centro_operacion;
                objDrivers.dto_generic_productos = item.dto_generic_productos;
                objDrivers.dto_generic_usuario_carga = item.dto_generic_usuario_carga;
                objDrivers.dto_generic_cantidad = item.dto_generic_cantidad;
                objDrivers.dto_generic_observaciones = v_observaciones;
                lstGrid.Add(objDrivers);
                v_consecutivo++;
            }
            return lstGrid;
        }

        public IList<GE_TFACTURACIONCARGUEDRIVERS> getDriversOk(IList<DTOgenericoCargueArchivos> p_lstCarg, String p_usuario)
        {
            //Cargo la lista de los productos
            IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
            lstProductos = ICargueDrivers.obtenerProductos();

            //Cargo la lista de los centros de costos
            IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
            lstCentroCostos = ICargueDrivers.obtenerCCostos();

            //Cargo la lista de los centros de operacion
            IList<GE_TCENTROSOPERACION> lstCentroOperacion = new List<GE_TCENTROSOPERACION>();
            lstCentroOperacion = ICargueDrivers.obtenerCOperacion();

            //Guardo la lista Ok del cargue que se realizó.
            List<GE_TFACTURACIONCARGUEDRIVERS> lstDrivers = new List<GE_TFACTURACIONCARGUEDRIVERS>();

            GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
            objPeriodo = periodo.GetAllActive()[0];

            foreach (var item in p_lstCarg)
            {
                GE_TFACTURACIONCARGUEDRIVERS objDriversOk = new GE_TFACTURACIONCARGUEDRIVERS();

                objDriversOk.facd_periodo = objPeriodo.peri_consecutivo;
                objDriversOk.facd_producto = Convert.ToInt32(lstProductos.Where(a => a.prod_codigo == item.dto_generic_productos).Select(b => b.prod_consecutivo).First());
                objDriversOk.facd_usuario_cargue = item.dto_generic_usuario_carga;
                objDriversOk.facd_empresa = item.dto_generic_empresa;
                objDriversOk.facd_ceco = Convert.ToInt32(lstCentroCostos.Where(a => a.cost_codigo == item.dto_generic_ccostos).Select(b => b.cost_consecutivo).First());
                objDriversOk.facd_cebe = Convert.ToInt32(lstCentroOperacion.Where(a => a.ceop_codigo == item.dto_generic_centro_operacion).Select(b => b.ceop_consecutivo).First());
                objDriversOk.facd_cantidad = Convert.ToInt32(item.dto_generic_cantidad);
                objDriversOk.facd_usuario = p_usuario;
                objDriversOk.facd_fecha = DateTime.Now;

                lstDrivers.Add(objDriversOk);
            }
            return lstDrivers;
        }

        public void Guardar(IList<GE_TFACTURACIONCARGUEDRIVERS> p_lstDrivers)
        {

            try
            {
                ICargueDrivers.guardar(p_lstDrivers);
            }
            catch
            {
                throw;
            }

        }

        public IList<DTOgenericoCargueArchivos> cargarDriversActivos()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstActivos = new List<DTOgenericoCargueArchivos>();
                return lstActivos = organizaTreeList(ICargueDrivers.cargarDriversActivos());
            }
            catch 
            {
                
                throw;
            }
        }
    }
}