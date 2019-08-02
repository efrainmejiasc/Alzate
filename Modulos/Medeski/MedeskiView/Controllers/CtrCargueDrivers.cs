using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace MedeskiView.Controllers
{
    public class CtrCargueDrivers : ApiController
    {
        ICargueDrivers ICargueDrivers = new CCargueDrives();
        CtrPeriodoPresupuesto periodo = new CtrPeriodoPresupuesto();
        CtrVwCuadroServicioTotal CuadroServicio = new CtrVwCuadroServicioTotal();

        public IEnumerable<DTOgenericoCargueArchivos> LeerExcel(string hoja, string archivo, string usuario)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstArchivos = ICargueDrivers.informacionExcelPoi(hoja, archivo, usuario);
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
                                                       .GroupBy(a => a.dto_generic_codigo ).ToList();

                            for (int m = 0; m < lst_Usuario.Count; m++)
                            {
                                //Valido que la cantidad sea mayor que 0 para que lo tenga en cuenta en la pantalla.
                                int cantidad = Convert.ToInt32(p_lstCarg.Where(a => a.dto_generic_empresa == lst_Empresas[i].Key
                                                                                     && a.dto_generic_ccostos == lst_Ccos[j].Key
                                                                && a.dto_generic_centro_operacion == lst_Cop[k].Key
                                                                                     && a.dto_generic_productos == lst_Prod[l].Key
                                                                                     && a.dto_generic_codigo == lst_Usuario[m].Key)
                                                                             .Sum(a => Convert.ToInt32(a.dto_generic_cantidad)).ToString());

                                if (cantidad > 0)
                                {
                                    DTOgenericoCargueArchivos objUsu = new DTOgenericoCargueArchivos();
                                    objUsu.dto_generic_id_consecutivo = lstTree.Count;
                                    objUsu.dto_generic_id_consecutivo_padre = prd;
                                    objUsu.dto_generic_cantidad = cantidad.ToString();
                                    objUsu.dto_generic_codigo = lst_Usuario[m].Key;
                                    lstTree.Add(objUsu);
                                }
                            }
                        }
                    }
                }
            }
            return lstTree;
        }

        public IList<DTOgenericoCargueArchivos> organizaGridview(IList<DTOgenericoCargueArchivos> p_lstCarg, string usuario)
        {
            //Cargo la lista de los productos directos
            IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
            lstProductos = ICargueDrivers.obtenerProductos();

            //Cargo la lista de los productos directos
            IList<VW_PRODUCTOS_DIRECTOS> lstProductosDir = new List<VW_PRODUCTOS_DIRECTOS>();
            lstProductosDir = ICargueDrivers.obtenerProductosDirectos();

            IList<GE_TDRIVERS> listaDrivers = new List<GE_TDRIVERS>();
            listaDrivers = ICargueDrivers.obtenerDrivers();

            //Cargo la lista de Empresas
            IList<GE_TCOMPANIAS> lstEmpresas = new List<GE_TCOMPANIAS>();
            GE_TCOMPANIAS empr;
            lstEmpresas = ICargueDrivers.obtenerCompanias();

            //Cargo la lista de los centros de costos
            IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
            lstCentroCostos = ICargueDrivers.obtenerCCostos();

            //Cargo la lista de los centros de operacion
            IList<GE_TCENTROSOPERACION> lstCentroOperacion = new List<GE_TCENTROSOPERACION>();
            lstCentroOperacion = ICargueDrivers.obtenerCOperacion();

            IList<DTOgenericoCargueArchivos> lstGrid = new List<DTOgenericoCargueArchivos>();
                
            IDictionary<string, DTOgenericoCargueArchivos> dictionary = new Dictionary<string, DTOgenericoCargueArchivos>();

            Char delimiter = ';';
            string[] strUsuario = null;
            HttpContext context = HttpContext.Current;
            strUsuario = (context.Session["Usuario"]).ToString().Split(delimiter);




            GE_TPERSONAS persona = new CPersonas().GetbyUsuario(usuario);
            GE_TPERSONAS jefe = new CPersonas().GetbyConsecutivo(persona.pers_consec_jefe);

            string v_observaciones = "";
            string[] v_split;
            string v_remplazar = "";
            int v_consecutivo = 1;
            string v_existeEmpresa = "";
            string v_existeDriver = "";
            string v_existeCentroCostos = "";
            string v_estaActivoCentroCostos = "";
            string v_existeCentroDeOperacion = "";
            string v_estaActivoCentroOperacion = "";
            string v_existeProducto = "";
            string v_estaActivoProducto = "";
            string v_estaActivoEmpresa = "";
            string v_estaActivoDriver = "";
            string v_driverAplicaSede = "";
            string v_driverAplicaValor = "";
            string v_driverAplicaProv = "";
            string v_empresaUsaCO = "";
            int usaCo = 0;

            foreach (var item in p_lstCarg)
            {
                if (String.IsNullOrEmpty(item.dto_generic_productos.ToUpper().ToString()))
                {
                    continue;
                }
                DTOgenericoCargueArchivos objDrivers = new DTOgenericoCargueArchivos();

                objDrivers.dto_generic_id_consecutivo = v_consecutivo;
                objDrivers.dto_generic_empresa = item.dto_generic_empresa;
                objDrivers.dto_generic_sede = item.dto_generic_sede;

                v_existeEmpresa = lstEmpresas.Any(c => c.comp_nombre.Equals(item.dto_generic_empresa.ToString().ToUpper())) == true ? "Existe" : "No Existe";
                // v_existeDriver = listaDrivers.Any(c => c.driv_nombre == item.dto_generic_driver) == true ? "Existe" : "No Existe";

                if (v_existeEmpresa.Equals("Existe"))
                {
                    // Aplica Centro de Costos?
                    empr = lstEmpresas.Where(a => a.comp_nombre == item.dto_generic_empresa).First();
                    usaCo = Convert.ToInt32(empr.comp_usa_co);
                    
                    if (usaCo == 1)
                    {                        
                        v_empresaUsaCO = item.dto_generic_ccostos != "" == true ? "" : "Empresa Requiere Centro de Costos,";
                    }

                    // Empresa Activa?
                    if (Convert.ToInt32(empr.comp_activo) != 1)
                    {
                        v_estaActivoEmpresa = "Empresa Inactiva,";
                    }

                    v_existeEmpresa = "";

                }
                else
                {
                    v_existeEmpresa = " Empresa No Existe,";
                }


                if(v_empresaUsaCO == "" && usaCo == 1)
                {
                    v_existeCentroCostos = lstCentroCostos.Any(a => a.cost_codigo == item.dto_generic_ccostos) == true ? "Existe" : "No Existe";
                
                    if (v_existeCentroCostos.Equals("Existe"))
                    {
                        v_estaActivoCentroCostos = lstCentroCostos.Any(d => d.cost_codigo == item.dto_generic_ccostos && d.cost_activo == 1) == true ? "" : "Centro de costos Inactivo,";
                        v_existeCentroCostos = "";
                    }
                    else
                    {
                        v_existeCentroCostos = " Centro de Costos No Existe,";
                    }
                }

                
                // v_existeCentroDeOperacion = lstCentroOperacion.Any(b => b.ceop_codigo == item.dto_generic_centro_operacion) == true ? "Existe" : "No Existe";
                v_existeProducto = lstProductos.Any(c => c.prod_codigo == item.dto_generic_productos.ToUpper()) == true ? "Existe" : "No Existe";
                                
                if (v_existeProducto.Equals("Existe"))
                {
                    // v_estaActivoProducto = lstProductos.Any(f => (f.prod_responsable == persona.pers_consecutivo || f.prod_responsable == jefe.pers_consecutivo) && f.prod_codigo == item.dto_generic_productos.ToUpper() && f.prod_activo == 1) == true ? "" : "Producto No Existe, o no Está a Su Cargo";
                    v_estaActivoProducto = lstProductos.Any(f => f.prod_codigo == item.dto_generic_productos.ToUpper() && f.prod_activo == 1) == true ? "" : "Producto No Existe, o no Está a Su Cargo";
                    v_existeProducto = "";
                }
                else
                {
                    v_existeProducto = " Producto no Existe, ";
                }

                v_observaciones =   v_existeEmpresa + v_estaActivoEmpresa + v_empresaUsaCO +
                                    v_existeDriver + v_estaActivoDriver + v_driverAplicaSede + v_driverAplicaValor + v_driverAplicaProv +
                                    v_existeCentroCostos + v_estaActivoCentroCostos + 
                                    v_existeCentroDeOperacion + v_estaActivoCentroOperacion + 
                                    v_existeProducto + v_estaActivoProducto;

                v_split = v_observaciones.Split(',');
                v_remplazar = v_split.Count() > 2 ? v_split[v_split.Count() - 2] : v_split[v_split.Count() - 1];
                v_observaciones = v_observaciones.Replace(v_remplazar + ",", v_remplazar + ".");
                v_observaciones = (v_observaciones.Length != 0) ? "Error: " + v_observaciones : "OK";
                

                objDrivers.dto_generic_ccostos = item.dto_generic_ccostos;
                objDrivers.dto_generic_productos = item.dto_generic_productos.ToUpper();
                objDrivers.dto_generic_codigo = item.dto_generic_codigo;
                objDrivers.dto_generic_cantidad = item.dto_generic_cantidad;
                objDrivers.dto_generic_valor = item.dto_generic_valor;
                objDrivers.dto_generic_proveedor = item.dto_generic_proveedor;
                
                objDrivers.dto_generic_observaciones = v_observaciones;                
                lstGrid.Add(objDrivers);

                // diccionario
                if(dictionary.ContainsKey(objDrivers.dto_generic_productos.ToUpper())){
                    var objDic = dictionary[objDrivers.dto_generic_productos.ToUpper()];
                    objDic.dto_generic_cantidad_total = (Convert.ToDecimal(objDic.dto_generic_cantidad_total) + Convert.ToDecimal(objDrivers.dto_generic_cantidad)).ToString();
                    objDic.dto_generic_valor_suma = Convert.ToDecimal(objDic.dto_generic_valor_suma) + Convert.ToDecimal(objDrivers.dto_generic_valor);
                    dictionary[objDrivers.dto_generic_productos.ToUpper()] = objDic;
                }
                else
                {
                    objDrivers.dto_generic_valor_suma = objDrivers.dto_generic_valor;
                    objDrivers.dto_generic_cantidad_total = objDrivers.dto_generic_cantidad;
                    objDrivers.dto_generic_valor_adicional = objDrivers.dto_generic_valor_adicional;
                    dictionary.Add(objDrivers.dto_generic_productos.ToUpper(), objDrivers);
                }
                
                v_consecutivo++;

                v_observaciones = "";
                v_remplazar = "";
                v_consecutivo = 1;
                v_existeEmpresa = "";
                v_existeDriver = "";
                v_existeCentroCostos = "";
                v_estaActivoCentroCostos = "";
                v_existeCentroDeOperacion = "";
                v_estaActivoCentroOperacion = "";
                v_existeProducto = "";
                v_estaActivoProducto = "";
                v_estaActivoEmpresa = "";
                v_estaActivoDriver = "";
                v_driverAplicaSede = "";
                v_driverAplicaValor = "";
                v_driverAplicaProv = "";
                v_empresaUsaCO = "";
                usaCo = 0;
            }

            IList<VW_VLR_CUADRO_SERVICIO_TOTAL> Cuadro = CuadroServicio.GetAll();
            IEnumerable<VW_VLR_CUADRO_SERVICIO_TOTAL> itemsCuadro;
            GE_TPRODUCTOS producto;
            VW_PRODUCTOS_DIRECTOS prodDir;
            // VW_PRODUCTOS_DIRECTOS producto;
            int sumaHijos = 0;

            foreach(var itemDic in dictionary)
            {
                try
                {

                    itemsCuadro = Cuadro.Where(obj => obj.producto == itemDic.Value.dto_generic_productos.ToUpper());
                    if (itemsCuadro.Count() == 0)
                    {
                        prodDir = (lstProductosDir.Any(x => x.prod_codigo == itemDic.Value.dto_generic_productos.ToUpper()) == true) ? lstProductosDir.Where(x => x.prod_codigo == itemDic.Value.dto_generic_productos.ToUpper()).First() : null;
                        // producto = new CVwProductosDirectos().GetByProducto(itemDic.Value.dto_generic_productos.ToUpper());

                        if (prodDir == null)
                        {
                            List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x == itemDic.Value).ToList<DTOgenericoCargueArchivos>();
                            foreach (var x in listObj)
                            {
                                DTOgenericoCargueArchivos objDrivers = x;
                                objDrivers.dto_generic_observaciones += " Este Producto no Existe.";
                                lstGrid.Remove(x);
                                lstGrid.Add(objDrivers);
                            }

                            continue;
                        }

                        CtrFamiliasProductos objFamilia = new CtrFamiliasProductos();
                        GE_TFAMILIAS_PRODUCTOS hijo = objFamilia.GetByHijo(prodDir.prod_consecutivo);
                        if (hijo != null)
                        {
                            GE_TPRODUCTOS padre = new CProductos().GetSingle(Convert.ToInt32(hijo.fam_padre));
                            itemsCuadro = Cuadro.Where(obj => obj.producto == padre.prod_codigo);

                            IList<GE_TFAMILIAS_PRODUCTOS> listaHijos = objFamilia.GetByPadre(padre.prod_consecutivo);
                            sumaHijos = listaHijos.Count();
                        }
                        else
                        {
                            List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x == itemDic.Value).ToList<DTOgenericoCargueArchivos>();
                            foreach (var x in listObj)
                            {
                                DTOgenericoCargueArchivos objDrivers = x;
                                objDrivers.dto_generic_observaciones += " Este Producto no tiene distribución en Cuadro de Servicios.";
                                lstGrid.Remove(x);
                                lstGrid.Add(objDrivers);
                            }

                            continue;
                        }
                        
                    }
                
                    VW_VLR_CUADRO_SERVICIO_TOTAL itemCuadro = itemsCuadro.First();

                    decimal totalCuadroServ = Convert.ToDecimal(itemCuadro.Total);
                    // producto = new CVwProductosDirectos().GetByProducto(itemDic.Value.dto_generic_productos.ToUpper());
                    prodDir = lstProductosDir.Where(x => x.prod_codigo == itemDic.Value.dto_generic_productos.ToUpper()).First();

                    GE_TDRIVERS driver = (listaDrivers.Any(x => x.driv_consecutivo == prodDir.prod_driver1) == true) ?  listaDrivers.Where(x => x.driv_consecutivo == prodDir.prod_driver1).First() :  null; //new CDrivers().GetSingle(Convert.ToInt32(prodDir.prod_driver1));
                    
                    if (driver == null)
                    {
                        List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x == itemDic.Value).ToList<DTOgenericoCargueArchivos>();
                        foreach (var x in listObj)
                        {
                            DTOgenericoCargueArchivos objDrivers = x;
                            objDrivers.dto_generic_observaciones += " Este Producto no tiene Configurado un Driver Principal.";
                            lstGrid.Remove(x);
                            lstGrid.Add(objDrivers);
                        }
                    }
                    else if (driver.driv_tipo_cobro == "T")  // Productos Total
                    {
                        List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x.dto_generic_productos == itemDic.Value.dto_generic_productos.ToUpper()).ToList<DTOgenericoCargueArchivos>();
                        foreach (var x in listObj)
                        {
                            DTOgenericoCargueArchivos objDrivers = x;
                            objDrivers.dto_generic_valor_distribuidos = totalCuadroServ / Convert.ToDecimal(itemDic.Value.dto_generic_cantidad_total) * Convert.ToDecimal(objDrivers.dto_generic_cantidad);
                            lstGrid.Remove(x);
                            lstGrid.Add(objDrivers);
                        }                    
                    }
                    else if (driver.driv_tipo_cobro == "U") // Productos Uno a Uno
                    {
                        if (totalCuadroServ != Convert.ToInt32(Decimal.Round(Convert.ToDecimal(itemDic.Value.dto_generic_valor_suma), 0)))
                        {
                            List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x.dto_generic_productos == itemDic.Value.dto_generic_productos.ToUpper()).ToList<DTOgenericoCargueArchivos>();
                            foreach (var x in listObj)
                            {
                                DTOgenericoCargueArchivos objDrivers = x;
                                objDrivers.dto_generic_observaciones += " Suma de Valores != Total Cuadro de Servicios ($" + Convert.ToInt32(totalCuadroServ) + "):";
                                lstGrid.Remove(x);
                                lstGrid.Add(objDrivers);
                            }

                        }
                        else
                        {
                            List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x.dto_generic_productos == itemDic.Value.dto_generic_productos.ToUpper()).ToList<DTOgenericoCargueArchivos>();
                            foreach (var x in listObj)
                            {
                                DTOgenericoCargueArchivos objDrivers = x;
                                objDrivers.dto_generic_valor_distribuidos = totalCuadroServ / Convert.ToDecimal(itemDic.Value.dto_generic_cantidad_total) * Convert.ToDecimal(objDrivers.dto_generic_cantidad);
                                lstGrid.Remove(x);
                                lstGrid.Add(objDrivers);
                            }
                        }
                    }
                    else  // Productos Equipos
                    {
                        List<DTOgenericoCargueArchivos> listObj = lstGrid.Where(x => x.dto_generic_productos == itemDic.Value.dto_generic_productos.ToUpper()).ToList<DTOgenericoCargueArchivos>();
                        decimal adicional; 
                    
                        if(sumaHijos > 0)
                            adicional = (totalCuadroServ - Convert.ToDecimal(itemDic.Value.dto_generic_valor)) / ( Convert.ToDecimal(listObj.Count) *sumaHijos );
                        else
                            adicional = (totalCuadroServ - Convert.ToDecimal(itemDic.Value.dto_generic_valor)) / Convert.ToDecimal(listObj.Count);

                        foreach (var x in listObj)
                        {
                            DTOgenericoCargueArchivos objDrivers = x;
                            objDrivers.dto_generic_valor_adicional = adicional;
                            lstGrid.Remove(x);
                            lstGrid.Add(objDrivers);
                        }

                        
                    }
                    sumaHijos = 0;                   
                }
                catch(Exception ex)
                {
                    
                }                
            }


            return lstGrid;
        }

        public IList<GE_TCARGUEDRIVERS> getDriversOk(IList<DTOgenericoCargueArchivos> p_lstCarg, String p_usuario)
        {
            //Cargo la lista de los productos
            IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
            lstProductos = ICargueDrivers.obtenerProductos();

            //Cargo la lista de los drivers
            IList<GE_TDRIVERS> listaDrivers = new List<GE_TDRIVERS>();
            listaDrivers = ICargueDrivers.obtenerDrivers();

            //Cargo la lista de Empresas
            IList<GE_TCOMPANIAS> lstEmpresas = new List<GE_TCOMPANIAS>();
            lstEmpresas = ICargueDrivers.obtenerCompanias();

            //Cargo la lista de los centros de costos
            IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
            lstCentroCostos = ICargueDrivers.obtenerCCostos();

            //Cargo la lista de los centros de operacion
            IList<GE_TCENTROSOPERACION> lstCentroOperacion = new List<GE_TCENTROSOPERACION>();
            lstCentroOperacion = ICargueDrivers.obtenerCOperacion();

            //Guardo la lista Ok del cargue que se realizó.
            List<GE_TCARGUEDRIVERS> lstDrivers = new List<GE_TCARGUEDRIVERS>();

            GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
            IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodo.GetAllActive();
            objPeriodo = lstPeriodo.Where(x => x.peri_activo == 1).First();

            int? nullValue = null;
            decimal? nullDecimal = null;

            try{

                foreach (var item in p_lstCarg)
                {
                    if(item.dto_generic_observaciones != "OK")
                    {
                        continue;
                    }
                    GE_TCARGUEDRIVERS objDriversOk = new GE_TCARGUEDRIVERS();

                    objDriversOk.carg_periodo = objPeriodo.peri_consecutivo;
                    objDriversOk.carg_producto = item.dto_generic_productos.ToUpper() == "" ? nullValue : Convert.ToInt32(lstProductos.Where(a => a.prod_codigo == item.dto_generic_productos.ToUpper()).Select(b => b.prod_consecutivo).First());
                    objDriversOk.carg_compania = item.dto_generic_empresa == "" ? nullValue : Convert.ToInt32(lstEmpresas.Where(a => a.comp_nombre == item.dto_generic_empresa).Select(b => b.comp_consecutivo).First());
                    objDriversOk.carg_ccosto = item.dto_generic_ccostos == "0" ? nullValue : Convert.ToInt32(lstCentroCostos.Where(a => a.cost_codigo == item.dto_generic_ccostos).Select(b => b.cost_consecutivo).First());
                    objDriversOk.carg_cantidad = item.dto_generic_cantidad == "" ? nullDecimal : Convert.ToDecimal(item.dto_generic_cantidad);
                    objDriversOk.carg_valor_distribucion = (item.dto_generic_valor_distribuidos == null) ? nullDecimal : Convert.ToDecimal(item.dto_generic_valor_distribuidos);
                    objDriversOk.carg_valor_adicional = (item.dto_generic_valor_adicional == null) ? nullDecimal : Convert.ToDecimal(item.dto_generic_valor_adicional);
                    objDriversOk.carg_usuario = p_usuario;
                    objDriversOk.carg_fecha = DateTime.Now;
                    objDriversOk.carg_usuario_act = p_usuario;
                    objDriversOk.carg_fecha_act = DateTime.Now;
                    objDriversOk.carg_activo = 1;
                    objDriversOk.carg_valor = item.dto_generic_valor == null ? nullValue : Convert.ToInt32(item.dto_generic_valor);
                    // objDriversOk.carg_driver = item.dto_generic_driver == "" ? nullValue : Convert.ToInt32(listaDrivers.Where(a => a.driv_nombre == item.dto_generic_driver).Select(b => b.driv_consecutivo).First());
                    objDriversOk.carg_sede = item.dto_generic_sede;
                    objDriversOk.carg_proveedor = item.dto_generic_proveedor;

                    GE_TPRODUCTOS producto = lstProductos.Where(x => x.prod_consecutivo == Convert.ToInt32(objDriversOk.carg_producto)).First();
                    GE_TDRIVERS driver = listaDrivers.Where(x => x.driv_consecutivo == producto.prod_driver1).First();

                    if (driver.driv_tipo_cobro == "T")
                    {
                        objDriversOk.carg_valor_total = Decimal.Round(Convert.ToDecimal(item.dto_generic_valor), 0);

                    }
                    /*
                     * else if (item.driv_tipo_cobro == "U")
                    {
                        objGeneric.dto_generic_valor_total = objGeneric.dto_generic_valor_adicional + objGeneric.dto_generic_valor_distribuidos;
                        
                    }
                     * */
                    else
                    {
                        objDriversOk.carg_valor_total = Decimal.Round(Convert.ToDecimal(item.dto_generic_valor_adicional + item.dto_generic_valor_distribuidos), 0);
                    }
                    
                    // objDriversOk.facd_cebe = Convert.ToInt32(lstCentroOperacion.Where(a => a.ceop_codigo == item.dto_generic_centro_operacion).Select(b => b.ceop_consecutivo).First());
                
                    lstDrivers.Add(objDriversOk);
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return lstDrivers;
        }

        public void Guardar(IList<GE_TCARGUEDRIVERS> p_lstDrivers, string usuario)
        {

            try
            {
                ICargueDrivers.guardar(p_lstDrivers, usuario);
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


        public IList<DTOgenericoCargueArchivos> GetAllActive(string usuario)
        {
            try
            {
                return new CCargueDrives().GetAllActive(usuario);
            }
            catch(Exception ex)
            {

                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> GetPendientes(IList<DTOgenericoCargueArchivos> cargue, string usuario)
        {
            try
            {
                return new CCargueDrives().listarPendientes(cargue, usuario);
            }
            catch
            {

                throw;
            }
        }
    }
}