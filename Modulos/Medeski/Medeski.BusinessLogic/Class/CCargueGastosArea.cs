using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using NPOI.HSSF.UserModel;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueGastosArea : Interfase.ICargueGastosArea
    {
        private readonly IProductos _CRUD;
        private readonly ICentrocosto _CRUDCC;
        private readonly IDistriGastoArea _CRUDGA;
        private readonly IPeriodoPresupuesto _CRUDPER;

        public CCargueGastosArea()
        {
            _CRUD = new Productos();
            _CRUDCC = new Centrocosto();
            _CRUDGA = new CargueGastosArea();
            _CRUDPER = new PeriodoPresupuesto();

        }

        public IList<DTOgenericoCargueArchivos> validaInformacionExcel(String p_hoja, String p_archivo)
        {
            //Obtengo la información del Excel.
            
            try
            {
                IList<DTOgenericoCargueArchivos> lstExcel = informacionExcelPoi(p_hoja, p_archivo);

                //Listo los productos para comparar con el Excel
                IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
                lstProductos = obtenerProductos();

                //Listo los centros de costo para comparar con el Excel
                IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
                lstCentroCostos = obtenerCCostos();

                //Cargo los productos
                IList<DTOgenericoCargueArchivos> lstNoexistentes = new List<DTOgenericoCargueArchivos>();

                string aux = "";

                foreach (var item in lstExcel)
                {
                    DTOgenericoCargueArchivos objDto = new DTOgenericoCargueArchivos();

                    var consultaProd = from lista in lstProductos
                                       where lista.prod_codigo == item.dto_generic_productos
                                       select lista;

                    var consultaCC = from lista in lstCentroCostos
                                     where lista.cost_codigo == item.dto_generic_ccostos
                                     select lista;

                    if (consultaProd.Count() < 1)
                    {
                        objDto.dto_generic_productos = item.dto_generic_productos;
                        objDto.dto_generic_observaciones = "No existe el producto";
                    }
                    else
                    {
                        objDto.dto_generic_productos = item.dto_generic_productos;
                    }

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
                    objDto.dto_generic_valor = item.dto_generic_valor;
                    objDto.dto_generic_codigo = item.dto_generic_codigo;

                    lstNoexistentes.Add(objDto);

                }
                return lstNoexistentes;
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
                return _CRUD.GetAll();
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
                return _CRUDCC.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> informacionExcel(String p_hoja, String p_archivo)
        {
            IList<DTOgenericoCargueArchivos> lstGastosArea = new List<DTOgenericoCargueArchivos>();
            try
            {
                String consultaDatos = "Select * from [" + p_hoja + "$] where Empresa is not null ";
                string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + p_archivo + "';Extended Properties=Excel 12.0;";
                OleDbConnection conex = new OleDbConnection(cadenaConexionArchivoExcel);
                conex.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(consultaDatos, conex);
                DataSet dtSet = new DataSet();
                dataAdapter.Fill(dtSet, p_hoja);
                conex.Close();
                List<DTOgenericoCargueArchivos> lstArchivos = new List<DTOgenericoCargueArchivos>();

                decimal? nullDecimal = null;

                foreach (DataRow row in dtSet.Tables[p_hoja].Rows)
                {
                    DTOgenericoCargueArchivos cArchivos = new DTOgenericoCargueArchivos();
                    cArchivos.dto_generic_productos = (row["Productos"] == null) ? null : row["Productos"].ToString();
                    cArchivos.dto_generic_ccostos = (row["CeCo"] == null) ? null : row["CeCo"].ToString();
                    cArchivos.dto_generic_valor = (row["Valor"] == null) ? nullDecimal : Convert.ToDecimal(row["Valor"]);
                    lstArchivos.Add(cArchivos);
                }
                return lstGastosArea;
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo)
        {
            try
            {
                OPCPackage pkg = OPCPackage.Open(p_archivo);
                XSSFWorkbook wb = new XSSFWorkbook(pkg);

                int encabezado = 0;
                int[] columnas = new int[3];
                ISheet sheet = wb.GetSheet(p_hoja);
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null)
                    {
                        for (int col = 0; col <= sheet.GetRow(row).LastCellNum; col++)
                        {
                            if (sheet.GetRow(row).GetCell(col) != null && sheet.GetRow(row).GetCell(col).CellType.ToString().Equals("String"))
                            {
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Productos"))
                                {
                                    encabezado = row;
                                    columnas[0] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("CeCo"))
                                {
                                    columnas[1] = col;
                                    continue;
                                }
                                if (sheet.GetRow(row).GetCell(col).StringCellValue.Trim().Equals("Valor"))
                                {
                                    columnas[2] = col;
                                    continue;
                                }
                                if (columnas[0] != 0 && columnas[1] != 0 && columnas[2] != 0)
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
                    gastoArea.dto_generic_ccostos = Int32.Parse(sheet.GetRow(row).GetCell(columnas[1]).NumericCellValue.ToString()).ToString();
                    gastoArea.dto_generic_valor = Convert.ToDecimal(sheet.GetRow(row).GetCell(columnas[2]).NumericCellValue.ToString());

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

        public void guardarGastosArea(IList<GE_TDISTRIBUCIONCARGUEGA> p_lstGastos)
        {
            try
            {
                eliminarGastosArea();
                _CRUDGA.Add(p_lstGastos.ToArray());
            }
            catch
            {
                throw;
            }
           
        }

        public void eliminarGastosArea()
        {
            try
            {
                CPeriodoPresupuesto periodo = new CPeriodoPresupuesto();
                GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
                IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodo.GetAllActive();
                objPeriodo = periodo.GetAllActive()[0];

                _CRUDGA.DeleteWhere(a => a.card_periodo == objPeriodo.peri_consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> obtenerActuales()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstdrivers = new List<DTOgenericoCargueArchivos>();

                using (var contex = new Entities())
                {
                    var consulta = from gastos in contex.GE_TDISTRIBUCIONCARGUEGA
                                   where gastos.GE_TPERIODOPRESUPUESTO.peri_activo == 1
                                   select new
                                   {
                                       gastos.card_consecutivo,
                                       gastos.GE_TPERIODOPRESUPUESTO.peri_ano,
                                       gastos.GE_TPRODUCTOS.prod_codigo,
                                       gastos.GE_TCENTROSCOSTOS.cost_codigo,
                                       gastos.card_valor,
                                       gastos.card_usuario
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos objDto = new DTOgenericoCargueArchivos();
        
                        objDto.dto_generic_productos = item.prod_codigo;
                        objDto.dto_generic_ccostos = item.cost_codigo;
                        objDto.dto_generic_valor = item.card_valor;
                        objDto.dto_generic_codigo = item.card_usuario;

                        lstdrivers.Add(objDto);
                    }
                    
                }
                return lstdrivers;
            }
            catch 
            {
                
                throw;
            }
        }
    }
}
