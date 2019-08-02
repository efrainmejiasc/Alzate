using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;
using System.Data.OleDb;
using System.Data;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueCuentasEspeciales : Interfase.ICargueCuentasEspeciales
    {
        private readonly ICargueArchivos _CRUD;
        
        public CCargueCuentasEspeciales()
        {
            _CRUD = new CargueArchivos();            
        }

        public IList<GE_TCARGUEARCHIVOS> GetAll()
        {
            return _CRUD.GetAll();
        }

        public IList<GE_TCARGUEARCHIVOS> GetAllProd(String strProducto)
        {
            try
            {
                IList<GE_TCARGUEARCHIVOS> lstArchivos = new List<GE_TCARGUEARCHIVOS>();
                lstArchivos = _CRUD.GetList(x => x.carg_producto == int.Parse(strProducto));
                return lstArchivos;
            }
            catch
            {
                throw;
            }
           
        }

        public IList<GE_TCARGUEARCHIVOS> leerExcel(String strHoja, String strArchivo)
        {
            try
            {
                String consultaDatos = "Select * from [" + strHoja + "$] where Empresa is not null ";
                string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + strArchivo + "';Extended Properties=Excel 12.0;";
                OleDbConnection conex = new OleDbConnection(cadenaConexionArchivoExcel);
                conex.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(consultaDatos, conex);
                DataSet dtSet = new DataSet();
                dataAdapter.Fill(dtSet, strHoja);
                conex.Close();
                List<GE_TCARGUEARCHIVOS> lstArchivos = new List<GE_TCARGUEARCHIVOS>();
                foreach (DataRow row in dtSet.Tables[strHoja].Rows)
                {
                        GE_TCARGUEARCHIVOS cArchivos = new GE_TCARGUEARCHIVOS();
                        cArchivos.carg_empresa = (row["Empresa"] == null) ? null : row["Empresa"].ToString();
                        cArchivos.carg_ccosto = (row["CCostos"] == null) ? null : row["CCostos"].ToString();
                        cArchivos.carg_usuario = (row["Usuario"] == null) ? null : row["Usuario"].ToString(); 
                        cArchivos.carg_item = (row["Item"] == null) ? null : row["Item"].ToString();
                        cArchivos.carg_equipo = (row["SerialEquipo"] == null) ? null : row["SerialEquipo"].ToString();
                        cArchivos.carg_leasing = (row["Leasing"] == null) ? null : row["Leasing"].ToString();
                        cArchivos.carg_papel = (row["PapelSN"] == null) ? null : row["PapelSN"].ToString();
                        cArchivos.carg_mes = (row["Mes"] == null) ? null : row["Mes"].ToString();
                        cArchivos.carg_proveedor = (row["Proveedor"] == null) ? null : row["Proveedor"].ToString();
                        cArchivos.carg_cantidad = (row["Cantidad"] == null) ? null : row["Cantidad"].ToString();
                        cArchivos.carg_valor = (row["Valor"] == null) ? (decimal?)null : Convert.ToDecimal(row["Valor"].ToString());
                        cArchivos.carg_observacion = (row["Observacion"] == null) ? null : row["Observacion"].ToString();
                        lstArchivos.Add(cArchivos);
                }

                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TCARGUEARCHIVOS> leerDatos(int pHojaIndex, String pRutaArchivo)
        {
            try
            {
                CUtilidades util = new CUtilidades();

                List<GE_TCARGUEARCHIVOS> lstArchivos = new List<GE_TCARGUEARCHIVOS>();
                foreach (DataRow row in util.ExceltoDataTable(pHojaIndex, pRutaArchivo).Rows)
                {
                    if (!row.IsNull(0))
                    {
                        GE_TCARGUEARCHIVOS cArchivos = new GE_TCARGUEARCHIVOS();
                        cArchivos.carg_empresa = (row["Empresa"] == null) ? null : row["Empresa"].ToString();
                        cArchivos.carg_ccosto = (row["CCostos"] == null) ? null : row["CCostos"].ToString();
                        cArchivos.carg_usuario = (row["Usuario"] == null) ? null : row["Usuario"].ToString();
                        cArchivos.carg_item = (row["Item"] == null) ? null : row["Item"].ToString();
                        cArchivos.carg_equipo = (row["SerialEquipo"] == null) ? null : row["SerialEquipo"].ToString();
                        cArchivos.carg_leasing = (row["Leasing"] == null) ? null : row["Leasing"].ToString();
                        cArchivos.carg_papel = (row["PapelSN"] == null) ? null : row["PapelSN"].ToString();
                        cArchivos.carg_mes = (row["Mes"] == null) ? null : row["Mes"].ToString();
                        cArchivos.carg_proveedor = (row["Proveedor"] == null) ? null : row["Proveedor"].ToString();
                        cArchivos.carg_cantidad = (row["Cantidad"] == null) ? null : row["Cantidad"].ToString();
                        cArchivos.carg_valor = (row["Valor"] == null) ? (decimal?)null : Convert.ToDecimal(row["Valor"].ToString());
                        cArchivos.carg_observacion = (row["Observacion"] == null) ? null : row["Observacion"].ToString();
                        lstArchivos.Add(cArchivos); 
                    }
                }

                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }

        public void Guardar(IList<GE_TCARGUEARCHIVOS> lstPpto, String strUsr, String strProducto)
        {
            try
            {
                DateTime dtFecha = DateTime.Now;
                CPeriodoPresupuesto periodoPPTO = new CPeriodoPresupuesto();
                IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodoPPTO.GetAllActive();
                int nPediodoPPTO = 0;
                foreach (GE_TPERIODOPRESUPUESTO ppto in lstPeriodo)
                {
                    nPediodoPPTO = ppto.peri_consecutivo;
                }
                // Se eliminan todos los registros del periodo activo para
                // cargarlos de nuevo.
                _CRUD.DeleteWhere(t => t.carg_periodo.Equals(nPediodoPPTO));

                foreach (GE_TCARGUEARCHIVOS row in lstPpto)
                {
                    GE_TCARGUEARCHIVOS cArchivos = new GE_TCARGUEARCHIVOS();

                    cArchivos.carg_empresa = row.carg_empresa.ToString();
                    cArchivos.carg_ccosto = row.carg_ccosto.ToString();
                    cArchivos.carg_usuario = row.carg_usuario.ToString();
                    cArchivos.carg_item = row.carg_item.ToString();
                    cArchivos.carg_equipo = row.carg_equipo.ToString();
                    cArchivos.carg_leasing = row.carg_leasing.ToString();
                    cArchivos.carg_papel = row.carg_papel.ToString();
                    cArchivos.carg_mes = row.carg_mes.ToString();
                    cArchivos.carg_proveedor = row.carg_proveedor.ToString();
                    cArchivos.carg_cantidad = row.carg_cantidad.ToString();
                    cArchivos.carg_valor = Convert.ToDecimal(row.carg_valor.ToString());
                    cArchivos.carg_observacion = row.carg_observacion.ToString();
                    cArchivos.carg_periodo = nPediodoPPTO;
                    cArchivos.carg_usuario = strUsr.Trim();
                    cArchivos.carg_producto = int.Parse(strProducto);
                    _CRUD.Add(cArchivos);
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
