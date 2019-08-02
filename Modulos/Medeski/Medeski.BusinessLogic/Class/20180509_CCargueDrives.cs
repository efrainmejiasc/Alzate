using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueDrives_20180509 : Interfase.ICargueDrivers_20180509
    {

        //Interfaz de Productos.
        private readonly IProductos _CRUDPRD;

        //Interfaz en Centros de Costos.
        private readonly ICentrocosto _CRUDCCO;

        //Interfaz de Centro de Operación.
        private readonly ICentroOperacion _CRUDCEO;

        //Interfaz de Centro de Operación.
        private readonly ICargueDrivers _CRUDDRI;

        public CCargueDrives_20180509()
        {
            _CRUDCCO = new Centrocosto();
            _CRUDPRD = new Productos();
            _CRUDCEO = new CentroOperacion();
            _CRUDDRI = new CargueDrivers();
        }
        public IList<DTOgenericoCargueArchivos> validaInformacionExcel(String p_hoja, String p_archivo)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueArchivo = informacionExcelPoi(p_hoja, p_archivo);

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
                    objDto.dto_generic_usuario_carga = item.dto_generic_usuario_carga;
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
        public IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo)
        {
            cargarDriversActivos();
            try
            {
                OPCPackage pkg = OPCPackage.Open(p_archivo);
                XSSFWorkbook wb = new XSSFWorkbook(pkg);

                int encabezado = 0;
                int[] columnas = new int[6];
                ISheet sheet = wb.GetSheet(p_hoja);
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null)
                    {
                        for (int col = 0; col <= sheet.GetRow(row).LastCellNum; col++)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null && sheet.GetRow(row).GetCell(col).CellType.ToString().Equals("String"))
                            {
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Producto"))
                                {
                                    encabezado = row;
                                    columnas[0] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Usuario"))
                                {
                                    columnas[1] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Empresa"))
                                {
                                    columnas[2] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Centro Costos"))
                                {
                                    columnas[3] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Centro Operacion"))
                                {
                                    columnas[4] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Equals("Cantidad"))
                                {
                                    columnas[5] = col;
                                    continue;
                                }
                                if (columnas[0] != 0 && columnas[1] != 0 && columnas[2] != 0 &&
                                    columnas[3] != 0 && columnas[4] != 0 && columnas[5] != 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                IList<DTOgenericoCargueArchivos> lstGastosArea = new List<DTOgenericoCargueArchivos>();
                for (int row = encabezado + 1; row <= sheet.LastRowNum; row++)
                {
                    DTOgenericoCargueArchivos gastoArea = new DTOgenericoCargueArchivos();

                    gastoArea.dto_generic_productos = sheet.GetRow(row).GetCell(columnas[0]).StringCellValue;
                    gastoArea.dto_generic_usuario_carga = sheet.GetRow(row).GetCell(columnas[1]).StringCellValue;
                    gastoArea.dto_generic_empresa = sheet.GetRow(row).GetCell(columnas[2]).StringCellValue;
                    gastoArea.dto_generic_ccostos = Int32.Parse(sheet.GetRow(row).GetCell(columnas[3]).NumericCellValue.ToString()).ToString();
                    //gastoArea.dto_generic_ccostos = sheet.GetRow(row).GetCell(columnas[3]).StringCellValue;
                    gastoArea.dto_generic_centro_operacion = sheet.GetRow(row).GetCell(columnas[4]).StringCellValue;
                    gastoArea.dto_generic_cantidad = Int32.Parse(sheet.GetRow(row).GetCell(columnas[5]).NumericCellValue.ToString()).ToString();
                    //gastoArea.dto_generic_cantidad = sheet.GetRow(row).GetCell(columnas[5]).StringCellValue;

                    lstGastosArea.Add(gastoArea);
                }
                pkg.Close();
                return lstGastosArea;
            }
            catch
            {
                throw;
            }
        }
        public void guardar(IList<GE_TFACTURACIONCARGUEDRIVERS> p_lstDrivers)
        {
            try
            {
                eliminarActivos();
                _CRUDDRI.Add(p_lstDrivers.ToArray());
            }
            catch
            {
                throw;
            }
        }
        public void eliminarActivos()
        {
            try
            {
                CPeriodoPresupuesto periodo = new CPeriodoPresupuesto();
                GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
                IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodo.GetAllActive();
                objPeriodo = periodo.GetAllActive()[0];

                _CRUDDRI.DeleteWhere(a => a.facd_periodo == objPeriodo.peri_consecutivo);
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
                    var consulta = from drivers in context.GE_TFACTURACIONCARGUEDRIVERS
                                   where drivers.GE_TPERIODOPRESUPUESTO.peri_activo == 1
                                   select new
                                   {
                                       drivers.GE_TPRODUCTOS.prod_codigo,
                                       drivers.facd_usuario_cargue,
                                       drivers.facd_empresa,
                                       drivers.GE_TCENTROSCOSTOS.cost_codigo,
                                       drivers.GE_TCENTROSOPERACION.ceop_codigo,
                                       drivers.facd_cantidad
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objGeneric = new DTOgenericoCargueArchivos();

                        objGeneric.dto_generic_productos = item.prod_codigo;
                        objGeneric.dto_generic_usuario_carga = item.facd_usuario_cargue;
                        objGeneric.dto_generic_empresa = item.facd_empresa;
                        objGeneric.dto_generic_ccostos = item.cost_codigo;
                        objGeneric.dto_generic_centro_operacion = item.ceop_codigo;
                        objGeneric.dto_generic_cantidad = item.facd_cantidad.ToString();

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
    }
}
