using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueDrives : Interfase.ICargueDrivers
    {

        //Interfaz de Parametros.
        private readonly ICompanias _CRUDCMP;

        //Interfaz de Drivers.
        private readonly IDrivers _CRUDDVR;
        
        //Interfaz de Productos.
        private readonly IProductos _CRUDPRD;

        //Interfaz de Productos.
        private readonly IVwProductosDirectos _CRUDPRDir;

        //Interfaz en Centros de Costos.
        private readonly ICentrocosto _CRUDCCO;

        //Interfaz de Centro de Operación.
        private readonly ICentroOperacion _CRUDCEO;

        //Interfaz de Centro de Operación.
        private readonly ICargueDrivers _CRUDDRI;

        //Interfaz de Centro de Operación.
        private readonly IRedistribucionDrivers CRUDDISTRI;

        public CCargueDrives()
        {
            _CRUDCCO = new Centrocosto();
            _CRUDPRD = new Productos();
            _CRUDPRDir = new VwProductosDirectos();
            _CRUDCEO = new CentroOperacion();
            _CRUDDRI = new CargueDrivers();
            _CRUDCMP = new Companias();
            _CRUDDVR = new Drivers();
            CRUDDISTRI = new RedistribucionDrivers();
        }
        
        public IList<DTOgenericoCargueArchivos> validaInformacionExcel(String p_hoja, String p_archivo, string usuario)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueArchivo = informacionExcelPoi(p_hoja, p_archivo, usuario);

                //Cargo la lista de los productos
                IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
                lstProductos = obtenerProductos();

                //Cargo la lista de los centros de costos
                IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
                lstCentroCostos = obtenerCCostos();

                //Cargo la lista de los centros de operacion
                IList<GE_TCENTROSOPERACION> lstCentroOperacion = new List<GE_TCENTROSOPERACION>();
                lstCentroOperacion = obtenerCOperacion();

                //Cargo el archivo validado
                IList<DTOgenericoCargueArchivos> lstArchivoValidado = new List<DTOgenericoCargueArchivos>();

                string aux = "";

                foreach (var item in lstCargueArchivo)
                {
                    DTOgenericoCargueArchivos objDto = new DTOgenericoCargueArchivos();

                    //Busco el producto por codigo.
                    var consultaProd = from lista in lstProductos
                                       where lista.prod_codigo == item.dto_generic_productos
                                       select lista;

                    //Busco el Centro de Costos por codigo.
                    var consultaCC = from lista in lstCentroCostos
                                     where lista.cost_codigo == item.dto_generic_ccostos
                                     select lista;

                    //Busco el Centro de operacion por codigo.
                    var consultaCOP = from lista in lstCentroOperacion
                                      where lista.ceop_codigo == item.dto_generic_centro_operacion
                                      select lista;
                    
                    //Valido que exista el Producto.
                    if (consultaProd.Count() < 1)
                    {
                        objDto.dto_generic_productos = item.dto_generic_productos;
                        objDto.dto_generic_observaciones = "No existe el producto";
                    }
                    else if(consultaProd.Any(a => a.prod_activo == 0))
                    {
                        objDto.dto_generic_productos = item.dto_generic_productos;

                        aux = objDto.dto_generic_observaciones;
                        objDto.dto_generic_observaciones = aux + " El producto se encuentra Inactivo";
                    }
                    else
                    {
                        objDto.dto_generic_productos = item.dto_generic_productos;
                    }

                    //Valido que exista el Centro de Costos.
                    if (consultaCC.Count() < 1)
                    {
                        objDto.dto_generic_ccostos = item.dto_generic_ccostos;
                        aux = objDto.dto_generic_observaciones;
                        objDto.dto_generic_observaciones = aux + " No existe CC";
                    }
                    else
                    {
                        objDto.dto_generic_ccostos = item.dto_generic_ccostos;
                    }

                    //Valido que exista el Centro de Operación.
                    if (consultaCOP.Count() < 1)
                    {
                        objDto.dto_generic_centro_operacion = item.dto_generic_centro_operacion;
                        aux = objDto.dto_generic_observaciones;
                        objDto.dto_generic_observaciones = aux + " No existe Centro Operación";
                    }
                    else
                    {
                        objDto.dto_generic_centro_operacion = item.dto_generic_centro_operacion;
                    }

                    //Termino de armar el Objeto.
                    objDto.dto_generic_codigo = item.dto_generic_codigo;
                    objDto.dto_generic_empresa = item.dto_generic_empresa.ToUpper();
                    objDto.dto_generic_cantidad = item.dto_generic_cantidad;

                    lstArchivoValidado.Add(objDto);

                }
                //Retorno la lista con los objetos ya validados y ordenados descendente por la observación.
                return lstArchivoValidado.OrderBy(x => x.dto_generic_id_consecutivo).ToList();
                //lstArchivoValidado
            }
            catch
            {
                throw;
            }

        }
        public IList<GE_TPRODUCTOS> obtenerProductos()
        {
            try
            {
                return _CRUDPRD.GetAll();
            }
            catch
            {
                throw;
            }
        }

        
        public IList<VW_PRODUCTOS_DIRECTOS> obtenerProductosDirectos()
        {
            try
            {
                return _CRUDPRDir.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCOMPANIAS> obtenerCompanias()
        {
            try
            {
                return _CRUDCMP.GetAll();               
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TDRIVERS> obtenerDrivers()
        {
            try
            {
                return _CRUDDVR.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCENTROSCOSTOS> obtenerCCostos()
        {
            try
            {
                return _CRUDCCO.GetAll();
            }
            catch
            {
                throw;
            }
        }
        public IList<GE_TCENTROSOPERACION> obtenerCOperacion()
        {
            try
            {
                return _CRUDCEO.GetAll();
            }
            catch
            {
                throw;
            }
        }
        
        public IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo, string usuario)
        {
            cargarDriversActivos();
            try
            {
                IList<DTOgenericoCargueArchivos> lstGastosArea = new List<DTOgenericoCargueArchivos>();
                decimal? nullDecimal = null;

                OPCPackage pkg = OPCPackage.Open(p_archivo);
                IWorkbook wb = new XSSFWorkbook(pkg);
                int encabezado = 0;

                XSSFSheet sheet = (XSSFSheet)wb.GetSheet(p_hoja);
                int[] columnas = new int[9];
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null)
                    {
                        if(sheet.GetRow(row).GetCell(0) == null)
                        {
                            continue;
                        }
                        
                        DTOgenericoCargueArchivos gastoArea = new DTOgenericoCargueArchivos();
                        
                        for (int col = 0; col <= sheet.GetRow(row).LastCellNum; col++)
                        {
                            

                            
                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(0).GetCell(col).StringCellValue.Trim().Equals("Producto"))
                                {
                                    // encabezado = row;
                                    // columnas[0] = col;
                                    gastoArea.dto_generic_productos = sheet.GetRow(row).GetCell(col) != null ? sheet.GetRow(row).GetCell(col).ToString() : "";
                                }
                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Trim().Equals("Empresa"))
                                {
                                    // columnas[1] = col;
                                    gastoArea.dto_generic_empresa = sheet.GetRow(row).GetCell(col) != null ? sheet.GetRow(row).GetCell(col).ToString().Trim() : "";
                                }
                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Trim().Equals("Sede"))
                                {
                                    gastoArea.dto_generic_sede = sheet.GetRow(row).GetCell(col) != null ? sheet.GetRow(row).GetCell(col).ToString().Trim() : "";
                                    // columnas[2] = col;
                                }
                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Trim().Equals("Centro Costos"))
                                {
                                    gastoArea.dto_generic_ccostos = sheet.GetRow(row).GetCell(col) != null ? ( (sheet.GetRow(row).GetCell(col).CellType == CellType.Numeric) ? Int32.Parse(sheet.GetRow(row).GetCell(col).NumericCellValue.ToString()).ToString() : "0" ) : "0";                        
                                    // columnas[3] = col;
                                }
                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Equals("Cantidad"))
                                {
                                    gastoArea.dto_generic_cantidad = sheet.GetRow(row).GetCell(col) != null ? Decimal.Parse(sheet.GetRow(row).GetCell(col).NumericCellValue.ToString(), System.Globalization.NumberStyles.Float).ToString() : "0";                        
                                    // columnas[4] = col;
                                }
                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Equals("Valor"))
                                {
                                    gastoArea.dto_generic_valor = sheet.GetRow(row).GetCell(col) != null ? Convert.ToDecimal(sheet.GetRow(row).GetCell(col).NumericCellValue.ToString()) : nullDecimal;
                                    // columnas[5] = col;
                                }
                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Equals("Proveedor"))
                                {
                                    gastoArea.dto_generic_proveedor = sheet.GetRow(row).GetCell(col) != null ? sheet.GetRow(row).GetCell(col).ToString() : "";
                                    // columnas[6] = col;
                                }                                
                            }                            
                        }
                        
                        gastoArea.dto_generic_codigo = usuario;
                        lstGastosArea.Add(gastoArea);
                    }
                }

                pkg.Close();
                return lstGastosArea;
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public void guardar(IList<GE_TCARGUEDRIVERS> p_lstDrivers, string usuario)
        {
            try
            {
                eliminarActivos(p_lstDrivers, usuario);
                
                IList<GE_TCARGUEDRIVERS> array = new List<GE_TCARGUEDRIVERS>();
                
                foreach(GE_TCARGUEDRIVERS driver in p_lstDrivers)
                {
                    _CRUDDRI.Add(driver);                    
                } 
                
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public void eliminarActivos(IList<GE_TCARGUEDRIVERS> cargue, string usuario)
        {
            try
            {
                var lstProdsCargue = cargue.Select(item => item.carg_producto).ToArray();

                CPeriodoPresupuesto periodo = new CPeriodoPresupuesto();
                GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
                IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodo.GetAllActive();
                objPeriodo = lstPeriodo.Where(x => x.peri_activo == 1).First();
                
                CPersonas objPersona = new CPersonas();
                GE_TPERSONAS persona = objPersona.GetbyUsuario(usuario);

                CDelegados objDelegados = new CDelegados();
                GE_TDELEGADOS delegado = objDelegados.GetByDelegado(persona.pers_consecutivo);
                
                IList<GE_TCARGUEDRIVERS> lista = new List<GE_TCARGUEDRIVERS>();
                if (delegado != null)
                {
                    CPersonas objJefe = new CPersonas();
                    GE_TPERSONAS jefe = objJefe.GetbyConsecutivo(delegado.dele_jefe);

                    lista = _CRUDDRI.GetList(a => lstProdsCargue.Contains(a.carg_producto) && a.carg_periodo == objPeriodo.peri_consecutivo && (a.carg_usuario == usuario || a.carg_usuario == jefe.pers_usudom));
                }
                else
                {
                    lista = _CRUDDRI.GetList(a => lstProdsCargue.Contains(a.carg_producto) && a.carg_periodo == objPeriodo.peri_consecutivo && a.carg_usuario == usuario);
                }
                
                IList<GE_TCARGUEDRIVERS> drivers = new List<GE_TCARGUEDRIVERS>();
                IList<GE_TREDISTRIBUCION_DRIVERS> listaDistri = new List<GE_TREDISTRIBUCION_DRIVERS>();
                
                foreach (var item in lista)
                {
                    GE_TREDISTRIBUCION_DRIVERS itemDistri = CRUDDISTRI.GetSingle(x => x.care_cargue_driver == item.carg_consecutivo);
                    itemDistri.care_activo = "0";
                    listaDistri.Add(itemDistri);
                    item.carg_activo = 0;
                    drivers.Add(item);
                }
                
                _CRUDDRI.Update(drivers.ToArray());
                CRUDDISTRI.Update(listaDistri.ToArray());

                // _CRUDDRI.Where(a => a.carg_periodo == objPeriodo.peri_consecutivo);
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
                IList<DTOgenericoCargueArchivos> lstDriversActivos = new List<DTOgenericoCargueArchivos>();

                using (var context = new Entities())
                {
                    var consulta = from drivers in context.GE_TCARGUEDRIVERS
                                   where drivers.GE_TPERIODOPRESUPUESTO.peri_activo == 1
                                   select new
                                   {
                                       drivers.GE_TPRODUCTOS.prod_codigo,
                                       drivers.carg_usuario,
                                       drivers.carg_compania,
                                       drivers.GE_TCENTROSCOSTOS.cost_codigo,
                                       drivers.GE_TCENTROSCOSTOS.cost_centro_operacion,
                                       drivers.carg_cantidad
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();

                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_codigo = item.carg_usuario;
                        objGeneric.dto_generic_empresa = item.carg_compania.ToString();
                        objGeneric.dto_generic_ccostos = item.cost_codigo;
                        objGeneric.dto_generic_centro_operacion = item.cost_centro_operacion;
                        objGeneric.dto_generic_cantidad = item.carg_cantidad.ToString();

                        lstDriversActivos.Add(objGeneric);
                    }

                }

            return lstDriversActivos;
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
                IList<DTOgenericoCargueArchivos> lstCargueDrivers = new List<DTOgenericoCargueArchivos>();
                GE_TPERSONAS persona = new CPersonas().GetbyUsuario(usuario);
                string jefeUser = new CPersonas().GetbyConsecutivo(persona.pers_consec_jefe).pers_usudom;
                var misFuncionarios = new CPersonas().GetMisFuncionarios(persona.pers_consecutivo).Select(x => x.pers_usudom);

                CPeriodoPresupuesto periodo = new CPeriodoPresupuesto();
                GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
                IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodo.GetAllActive();
                objPeriodo = lstPeriodo.Where(x => x.peri_activo == 1).First();

                using (var context = new Entities())
                {
                    var consulta = from drivers in context.GE_TCARGUEDRIVERS
                                   join prods in context.GE_TPRODUCTOS on drivers.carg_producto equals prods.prod_consecutivo
                                   join drivs in context.GE_TDRIVERS on prods.prod_driver1 equals drivs.driv_consecutivo

                                   join ccosto in context.GE_TCENTROSCOSTOS on drivers.carg_ccosto equals ccosto.cost_consecutivo into tmpccostos
                                   from ccosto in tmpccostos.DefaultIfEmpty()
                                   
                                   join ceop in context.GE_TCENTROSOPERACION on ccosto.cost_centro_operacion equals ceop.ceop_codigo into tmpceop
                                   from ceop in tmpceop.DefaultIfEmpty()

                                   join param in context.GE_TPARAMETROS on ccosto.cost_tipo_cliente equals param.parm_consecutivo into tmpparam
                                   from param in tmpparam.DefaultIfEmpty()

                                   join empresa in context.GE_TCOMPANIAS on drivers.carg_compania equals empresa.comp_consecutivo
                                   where drivers.GE_TPERIODOPRESUPUESTO.peri_consecutivo == objPeriodo.peri_consecutivo
                                   where drivers.carg_activo == 1

                                   // where drivers.carg_usuario == Environment.UserName.ToUpper() || drivers.carg_usuario == jefeUser.ToUpper() || misFuncionarios.Contains(drivers.carg_usuario)
                                   
                                   select new
                                   {
                                       drivers.GE_TPRODUCTOS.prod_codigo,
                                       drivers.carg_usuario,
                                       drivers.GE_TCOMPANIAS.comp_nombre,
                                       ceop.ceop_codigo,
                                       ceop.ceop_descripcion,
                                       param.parm_descripcion,
                                       drivers.GE_TCENTROSCOSTOS.cost_codigo,
                                       drivers.GE_TCENTROSCOSTOS.cost_centro_operacion,
                                       drivers.carg_cantidad,
                                       drivers.carg_valor_adicional,
                                       drivers.carg_valor,
                                       drivers.carg_valor_distribucion,
                                       drivers.carg_sede,
                                       drivs.driv_tipo_cobro
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();

                        objGeneric.dto_generic_centro_operacion = item.ceop_codigo;
                        objGeneric.dto_generic_coperaciones = item.ceop_descripcion;
                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_codigo = item.carg_usuario;
                        objGeneric.dto_generic_empresa = item.comp_nombre;
                        objGeneric.dto_generic_sede = item.carg_sede;
                        objGeneric.dto_generic_descripcion_a = item.parm_descripcion == null ? "EXTERNOS" : item.parm_descripcion;
                        objGeneric.dto_generic_ccostos = item.cost_codigo;
                        objGeneric.dto_generic_centro_operacion = item.cost_centro_operacion;
                        objGeneric.dto_generic_cantidad = item.carg_cantidad.ToString();
                        objGeneric.dto_generic_valor = item.carg_valor;
                        objGeneric.dto_generic_valor_adicional = item.carg_valor_adicional == null ? 0 : item.carg_valor_adicional;
                        objGeneric.dto_generic_valor_distribuidos = item.carg_valor_distribucion == null ? 0 : item.carg_valor_distribucion;
                        
                        if(item.driv_tipo_cobro == "T")
                        {
                            objGeneric.dto_generic_valor_total = objGeneric.dto_generic_valor;
                        
                        }
                        /*
                         * else if (item.driv_tipo_cobro == "U")
                        {
                            objGeneric.dto_generic_valor_total = objGeneric.dto_generic_valor_adicional + objGeneric.dto_generic_valor_distribuidos;
                        
                        }
                         * */
                        else
                        {
                            objGeneric.dto_generic_valor_total = objGeneric.dto_generic_valor_adicional + objGeneric.dto_generic_valor_distribuidos;
                        }
                        
                        lstCargueDrivers.Add(objGeneric);
                    }                   
                }
                return lstCargueDrivers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> listarPendientes(IList<DTOgenericoCargueArchivos> cargue, string usuario)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueDrivers = new List<DTOgenericoCargueArchivos>();
                GE_TPERSONAS persona = new CPersonas().GetbyUsuario(usuario);

                var lstCargue = cargue.Select(item => item.dto_generic_productos).ToArray();

                using (var context = new Entities())
                { 
                    var consulta = from productos in context.VW_PRODUCTOS_DIRECTOS
                                   where productos.prod_responsable == persona.pers_consecutivo || productos.prod_responsable == persona.pers_consec_jefe
                                   where productos.prod_activo == 1
                                   where (!lstCargue.Contains(productos.prod_codigo))
                                   select productos;

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();

                        objGeneric.dto_generic_id_consecutivo = item.prod_consecutivo;
                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_observaciones = "Producto no se encuentra en el archivo";
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
