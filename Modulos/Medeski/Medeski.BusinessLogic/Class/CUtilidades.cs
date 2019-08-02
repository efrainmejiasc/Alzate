using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Medeski.BusinessLogic.Class
{
    public static class tabla
    {
        public static DataTable ToPivotTable<T, TColumn, TRow, TData>(
    this IEnumerable<T> source,
    Func<T, TColumn> columnSelector,
    Expression<Func<T, TRow>> rowSelector,
    Func<IEnumerable<T>, TData> dataSelector)
        {
            DataTable table = new DataTable();
            var rowName = ((MemberExpression)rowSelector.Body).Member.Name;
            table.Columns.Add(new DataColumn(rowName));
            var columns = source.Select(columnSelector).Distinct();

            foreach (var column in columns)
                table.Columns.Add(new DataColumn(column.ToString()));

            var rows = source.GroupBy(rowSelector.Compile())
                             .Select(rowGroup => new
                             {
                                 Key = rowGroup.Key,
                                 Values = columns.GroupJoin(
                                     rowGroup,
                                     c => c,
                                     r => columnSelector(r),
                                     (c, columnGroup) => dataSelector(columnGroup))
                             });

            foreach (var row in rows)
            {
                var dataRow = table.NewRow();
                var items = row.Values.Cast<object>().ToList();
                items.Insert(0, row.Key);
                dataRow.ItemArray = items.ToArray();
                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
    
    public class CUtilidades : Interfase.IUtilidades
    {
         public CUtilidades()
         {}

         public DataTable ExceltoDataTable(int pHojaIndex, String pRutaArchivo)
         {
             DataTable Tabla = null;
             try
             {
                 if (System.IO.File.Exists(pRutaArchivo))
                 {

                     IWorkbook workbook = null;  //IWorkbook determina si es xls o xlsx              
                     ISheet worksheet = null;
                     string first_sheet_name = "";

                     using (FileStream FS = new FileStream(pRutaArchivo, FileMode.Open, FileAccess.Read))
                     {
                         workbook = WorkbookFactory.Create(FS);          //Abre tanto XLS como XLSX
                         worksheet = workbook.GetSheetAt(pHojaIndex);    //Obtener Hoja por indice
                         first_sheet_name = worksheet.SheetName;         //Obtener el nombre de la Hoja

                         Tabla = new DataTable(first_sheet_name);
                         Tabla.Rows.Clear();
                         Tabla.Columns.Clear();

                         // Leer Fila por fila desde la primera
                         for (int rowIndex = 0; rowIndex <= worksheet.LastRowNum; rowIndex++)
                         {
                             bool isEmpty = false; //Si todas las celdas de la fila estan vacias este parametro cambia
                             DataRow NewReg = null;
                             IRow row = worksheet.GetRow(rowIndex);
                             IRow row2 = null;
                             IRow row3 = null;

                             if (rowIndex == 0)
                             {
                                 row2 = worksheet.GetRow(rowIndex + 1); //Si es la Primera fila, obtengo tambien la segunda para saber el tipo de datos
                                 row3 = worksheet.GetRow(rowIndex + 2); //Y la tercera tambien por las dudas
                             }

                             if (row != null) //null is when the row only contains empty cells 
                             {
                                 if (rowIndex > 0) NewReg = Tabla.NewRow();

                                 int colIndex = 0;
                                 //Leer cada Columna de la fila
                                 foreach (ICell cell in row.Cells)
                                 {
                                     object valorCell = null;
                                     string cellType = "";
                                     string[] cellType2 = new string[2];

                                     if (rowIndex == 0) //Asumo que la primera fila contiene los titlos:
                                     {
                                         for (int i = 0; i < 2; i++)
                                         {
                                             ICell cell2 = null;
                                             if (i == 0) { cell2 = row2 != null ? row2.GetCell(cell.ColumnIndex) : null; }
                                             else { cell2 = row3 != null ? row3.GetCell(cell.ColumnIndex) : null; }

                                             if (cell2 != null)
                                             {
                                                 switch (cell2.CellType)
                                                 {
                                                     case CellType.Blank: break;
                                                     case CellType.Boolean: cellType2[i] = "System.Boolean"; break;
                                                     case CellType.String: cellType2[i] = "System.String"; break;
                                                     case CellType.Numeric:
                                                         if (HSSFDateUtil.IsCellDateFormatted(cell2)) { cellType2[i] = "System.DateTime"; }
                                                         else
                                                         {
                                                             cellType2[i] = "System.Double";  //valorCell = cell2.NumericCellValue;
                                                         }
                                                         break;

                                                     case CellType.Formula:
                                                         bool continuar = true;
                                                         switch (cell2.CachedFormulaResultType)
                                                         {
                                                             case CellType.Boolean: cellType2[i] = "System.Boolean"; break;
                                                             case CellType.String: cellType2[i] = "System.String"; break;
                                                             case CellType.Numeric:
                                                                 if (HSSFDateUtil.IsCellDateFormatted(cell2)) { cellType2[i] = "System.DateTime"; }
                                                                 else
                                                                 {
                                                                     try
                                                                     {
                                                                         //DETERMINAR SI ES BOOLEANO
                                                                         if (cell2.CellFormula == "TRUE()") { cellType2[i] = "System.Boolean"; continuar = false; }
                                                                         if (continuar && cell2.CellFormula == "FALSE()") { cellType2[i] = "System.Boolean"; continuar = false; }
                                                                         if (continuar) { cellType2[i] = "System.Double"; continuar = false; }
                                                                     }
                                                                     catch { }
                                                                 } break;
                                                         }
                                                         break;
                                                     default:
                                                         cellType2[i] = "System.String"; break;
                                                 }
                                             }
                                         }

                                         //Resolver las diferencias de Tipos
                                         if (cellType2[0] == cellType2[1]) { cellType = cellType2[0]; }
                                         else
                                         {
                                             if (cellType2[0] == null) cellType = cellType2[1];
                                             if (cellType2[1] == null) cellType = cellType2[0];
                                             if (cellType == "") cellType = "System.String";
                                         }

                                         //Obtener el nombre de la Columna
                                         string colName = "Column_{0}";
                                         try { colName = cell.StringCellValue; }
                                         catch { colName = string.Format(colName, colIndex); }

                                         //Verificar que NO se repita el Nombre de la Columna
                                         foreach (DataColumn col in Tabla.Columns)
                                         {
                                             if (col.ColumnName == colName) colName = string.Format("{0}_{1}", colName, colIndex);
                                         }

                                         //Agregar el campos de la tabla:
                                         if (cellType != null)
                                         {
                                             DataColumn codigo = new DataColumn(colName, System.Type.GetType(cellType));
                                             Tabla.Columns.Add(codigo); colIndex++;
                                         }
                                         
                                     }
                                     else
                                     {
                                         //Las demas filas son registros:
                                         switch (cell.CellType)
                                         {
                                             case CellType.Blank: valorCell = DBNull.Value; isEmpty = true; break;
                                             case CellType.Boolean: valorCell = cell.BooleanCellValue; break;
                                             case CellType.String: valorCell = cell.StringCellValue; break;
                                             case CellType.Numeric:
                                                 if (HSSFDateUtil.IsCellDateFormatted(cell)) { valorCell = cell.DateCellValue; }
                                                 else { valorCell = cell.NumericCellValue; } break;
                                             case CellType.Formula:
                                                 switch (cell.CachedFormulaResultType)
                                                 {
                                                     case CellType.Blank: valorCell = DBNull.Value; break;
                                                     case CellType.String: valorCell = cell.StringCellValue; break;
                                                     case CellType.Boolean: valorCell = cell.BooleanCellValue; break;
                                                     case CellType.Numeric:
                                                         if (HSSFDateUtil.IsCellDateFormatted(cell)) { valorCell = cell.DateCellValue; }
                                                         else { valorCell = cell.NumericCellValue; }
                                                         break;
                                                 }
                                                 break;
                                             default: valorCell = cell.StringCellValue; break;
                                         }
                                         //Agregar el nuevo Registro
                                         if (cell.ColumnIndex <= Tabla.Columns.Count - 1) NewReg[cell.ColumnIndex] = valorCell;
                                     }
                                 }
                             }
                             //Si las celdas de las filas no estan vacias la agrego a la tabla
                             if (rowIndex > 0 && !isEmpty) Tabla.Rows.Add(NewReg);
                         }
                         Tabla.AcceptChanges();
                     }
                 }
                 else
                 {
                     throw new Exception("ERROR 404: El archivo especificado NO existe.");
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return Tabla;
         }

         public IList<DTOCuadroServicio> GenerarCuadroServicio()
         {
             try
             {
                 using (var context = new Entities())
                 {
                     return context.Database.SqlQuery<DTOCuadroServicio>("sp_GenerarCuadroServicio").ToList();
                 }
             }
             catch
             {
                 throw;
             }
         }

         public DataTable CalcularValorItemServidor ()
         {
             try
             {
                 using (var context = new Entities())
                 {
                     DataTable myDataTable = new DataTable();
                     myDataTable.Columns.AddRange(new DataColumn[4] { new DataColumn("servidor", typeof(string)), new DataColumn("core", typeof(decimal)), new DataColumn("item", typeof(string)), new DataColumn("vlritem", typeof(decimal)) });
                     IList<DTOServidorItem> list = context.Database.SqlQuery<DTOServidorItem>("sp_CalcularValorItemServidor").ToList();
                     
                     foreach(DTOServidorItem l in list)
                     {
                         DataRow r = myDataTable.NewRow();
                         r["servidor"] = l.servidor;
                         r["core"] = l.core;
                         r["item"] = l.item;
                         r["vlritem"] = l.vlritem;
                         myDataTable.Rows.Add(r);
                        
                     }


                     var data2 = myDataTable.AsEnumerable().Select(x => new
                     {
                         Servidor = x.Field<String>("servidor"),
                         Core = x.Field<decimal>("core"),
                         Item = x.Field<string>("item"),
                         VlrItem = x.Field<decimal>("vlritem")
                     });

                     DataTable pivotDataTable = tabla.ToPivotTable( data2,
                    item => item.Item,
                    item => item.Servidor,
                    items => items.Any() ? items.Sum(x => x.VlrItem) : 0);

                     return pivotDataTable;

                     //return myDataTable;
                 }
             }
             catch
             {
                 throw;
             }
         }


/*
         public static DataTable ToPivotTable<T, TColumn, TRow, TData>(
             this IEnumerable<T> source,
             Func<T, TColumn> columnSelector,
             Expression<Func<T, TRow>> rowSelector,
             Func<IEnumerable<T>, TData> dataSelector)
         {
             DataTable table = new DataTable();
             var rowName = ((MemberExpression)rowSelector.Body).Member.Name;
             table.Columns.Add(new DataColumn(rowName));
             var columns = source.Select(columnSelector).Distinct();

             foreach (var column in columns)
                 table.Columns.Add(new DataColumn(column.ToString()));

             var rows = source.GroupBy(rowSelector.Compile())
                              .Select(rowGroup => new
                              {
                                  Key = rowGroup.Key,
                                  Values = columns.GroupJoin(
                                      rowGroup,
                                      c => c,
                                      r => columnSelector(r),
                                      (c, columnGroup) => dataSelector(columnGroup))
                              });

             foreach (var row in rows)
             {
                 var dataRow = table.NewRow();
                 var items = row.Values.Cast<object>().ToList();
                 items.Insert(0, row.Key);
                 dataRow.ItemArray = items.ToArray();
                 table.Rows.Add(dataRow);
             }

             return table;
         }*/
    }
}
