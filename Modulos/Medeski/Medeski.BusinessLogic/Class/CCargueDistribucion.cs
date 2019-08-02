using Medeski.DataAcces;
using Medeski.BusinessLogic;
using Medeski.DataAcces.Class;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueDistribucion : Interfase.ICargueDistribucion
    {
        private readonly ICargueDistribucion CRUDDRI = new CargueDistribucion();

        public IList<GE_TCARGUEDISTRIBUCION> GetAll()
        {
            try
            {
                return CRUDDRI.GetAll();
            }
            catch
            {
                throw;
            }
        }
        
        
        public void Guardar(IList<GE_TCARGUEDISTRIBUCION> lstCargue)
        {
            try
            {
                eliminarActivos(lstCargue);

                foreach (GE_TCARGUEDISTRIBUCION driver in lstCargue)
                {
                    CRUDDRI.Add(driver);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> GetAllActive()
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstCargueDrivers = new List<DTOgenericoCargueArchivos>();
        
                using(var context = new Entities())
                {

                    var consulta = from cargue in context.GE_TCARGUEDISTRIBUCION
                                   join coo in context.GE_TCENTROSOPERACION on cargue.cadi_co_origen equals coo.ceop_consecutivo
                                   join cod in context.GE_TCENTROSOPERACION on cargue.cadi_co_destino equals cod.ceop_consecutivo
                                   where cargue.cadi_activo == 1 && coo.ceop_activo == 1  && cod.ceop_activo == 1
                                   select new
                                   {
                                       coor = coo.ceop_codigo,
                                       code = cod.ceop_codigo,
                                       porc = cargue.cadi_porcentaje                                       
                                   };

                    foreach (var item in consulta)
                    {
                        DTOgenericoCargueArchivos distr = new DTOgenericoCargueArchivos();
                        distr.dto_generic_centro_operacion = item.coor;
                        distr.dto_generic_descripcion_a = item.coor;
                        distr.dto_generic_coperaciones = item.code;
                        distr.dto_generic_descripcion_b = item.code;
                        distr.dto_generic_valor = item.porc;

                        lstCargueDrivers.Add(distr);
                    }
                }

                return lstCargueDrivers;
            }
            catch
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo)
        {
            // cargarDriversActivos();
            try
            {
                IList<DTOgenericoCargueArchivos> lstGastosArea = new List<DTOgenericoCargueArchivos>();
                decimal? nullDecimal = null;

                OPCPackage pkg = OPCPackage.Open(p_archivo);
                IWorkbook wb = new XSSFWorkbook(pkg);
                
                XSSFSheet sheet = (XSSFSheet)wb.GetSheet(p_hoja);
                int[] columnas = new int[9];
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null)
                    {
                        if (sheet.GetRow(row).GetCell(0) == null)
                        {
                            continue;
                        }

                        DTOgenericoCargueArchivos gastoArea = new DTOgenericoCargueArchivos();

                        for (int col = 0; col <= sheet.GetRow(row).LastCellNum; col++)
                        {

                            if (sheet.GetRow(row).GetCell(col) != null)
                            {
                                if (sheet.GetRow(0).GetCell(col).StringCellValue.Equals("CO Origen"))
                                {
                                    gastoArea.dto_generic_descripcion_a = sheet.GetRow(row).GetCell(col) != null ? sheet.GetRow(row).GetCell(col).ToString() : "";
                                    // columnas[6] = col;
                                }

                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Equals("CO Destino"))
                                {
                                    gastoArea.dto_generic_descripcion_b = sheet.GetRow(row).GetCell(col) != null ? sheet.GetRow(row).GetCell(col).ToString() : "";
                                    // columnas[6] = col;
                                }

                                else if (sheet.GetRow(0).GetCell(col).StringCellValue.Equals("Porcentaje"))
                                {
                                    gastoArea.dto_generic_valor = sheet.GetRow(row).GetCell(col) != null ? Convert.ToDecimal(sheet.GetRow(row).GetCell(col).ToString()) / 100 : nullDecimal;
                                    // columnas[6] = col;
                                }

                            }
                        }
                        // gastoArea.dto_generic_codigo = Environment.UserName;
                        lstGastosArea.Add(gastoArea);
                    }
                }

                pkg.Close();
                
                return lstGastosArea;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void eliminarActivos(IList<GE_TCARGUEDISTRIBUCION> cargue)
        {
            try
            {
                IList<GE_TCARGUEDISTRIBUCION> lstCargue = CRUDDRI.GetAll();

                foreach (var item in cargue)
                {
                    bool existe = lstCargue.Any(x => x.cadi_co_origen == item.cadi_co_origen && x.cadi_co_destino == item.cadi_co_destino) ? true : false;
                    if (existe)
                    {
                        GE_TCARGUEDISTRIBUCION itemCargue = lstCargue.Where(x => x.cadi_co_origen == item.cadi_co_origen && x.cadi_co_destino == item.cadi_co_destino && x.cadi_activo == 1).First();
                        itemCargue.cadi_activo = 0;
                        CRUDDRI.Update(itemCargue);
                    }                    
                }

            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
