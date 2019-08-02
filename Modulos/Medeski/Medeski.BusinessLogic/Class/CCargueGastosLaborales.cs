using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;
using System.Data.OleDb;
using System.Data;
using Medeski.BusinessLogic.Interfase;

namespace Medeski.BusinessLogic.Class
{
    public class CCargueGastosLaborales : Interfase.ICargueGastosLaborales
    {
        private readonly ICargueArchivosLaboral _CRUD;
        private readonly Medeski.DataAcces.Class.IPeriodoTransacciones _CRUDCMP;
        private readonly Medeski.DataAcces.Class.ISalidaPresupuesto _CRUDAMORT;
        private readonly Medeski.DataAcces.Class.IPersonas _CRUDPER;
        private readonly Medeski.DataAcces.Class.IParametros _CRUDPARAM;
        private readonly IClaseparametros _CRUDCLASES;
        private readonly IProductoItems _CRUDITEMS;
        private readonly ICentrocosto _CRUDCCTOS;
        
        public CCargueGastosLaborales()
        {
            _CRUD = new CargueArchivosLaboral();
            _CRUDCMP = new PeriodoTransacciones();
            _CRUDAMORT = new SalidaPresupuesto();
            _CRUDPER = new Personas();
            _CRUDPARAM = new Parametros();
            _CRUDITEMS = new ProductoItems();
            _CRUDCCTOS = new Centrocosto();
            _CRUDCLASES = new Claseparametros();
        }

        public IList<GE_TCARGUEARCHIVOSLABORAL> GetAll()
        {
            return _CRUD.GetAll();
        }

        public IList<GE_TCARGUEARCHIVOSLABORAL> leerExcel(string subCat, String strHoja, String strArchivo)
        {
            try
            {
                String consultaDatos = "Select * from [" + strHoja + "$] where Categoria is not null ";
                string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + strArchivo + "';Extended Properties=Excel 12.0;";
                OleDbConnection conex = new OleDbConnection(cadenaConexionArchivoExcel);
                conex.Open();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(consultaDatos, conex);
                DataSet dtSet = new DataSet();
                dataAdapter.Fill(dtSet, strHoja);
                conex.Close();
                List<GE_TCARGUEARCHIVOSLABORAL> lstLineEnsa = new List<GE_TCARGUEARCHIVOSLABORAL>();
                foreach (DataRow row in dtSet.Tables[strHoja].Rows)
                {
                    if (!row.IsNull(0))
                    {
                        
                            GE_TCARGUEARCHIVOSLABORAL cArchivos = new GE_TCARGUEARCHIVOSLABORAL();
                            string mTipo = subCat.Equals("CE") ? "CE" : "GA";
                            cArchivos.carl_categoria = mTipo;
                            cArchivos.carl_subcategoria = subCat;
                            cArchivos.carl_ccostos = row["Centro Costos"].ToString();
                            cArchivos.carl_item = row["Item"].ToString();
                            cArchivos.carl_moneda = row["Moneda"].ToString();
                            cArchivos.carl_valor = Convert.ToInt32(row["Valor"].ToString());
                            cArchivos.carl_cantidad = Convert.ToInt32(row["Cantidad"].ToString());
                            cArchivos.carl_periodo = new CPeriodoPresupuesto().GetPeriodoActivo().peri_consecutivo;

                            cArchivos.carl_enero = row["Enero"].ToString();
                            cArchivos.carl_febrero = row["Febrero"].ToString();
                            cArchivos.carl_marzo = row["Marzo"].ToString();
                            cArchivos.carl_abril = row["Abril"].ToString();
                            cArchivos.carl_mayo = row["Mayo"].ToString();
                            cArchivos.carl_junio = row["Junio"].ToString();
                            cArchivos.carl_julio = row["Julio"].ToString();
                            cArchivos.carl_agosto = row["Agosto"].ToString();
                            cArchivos.carl_septiembre = row["Septiembre"].ToString();
                            cArchivos.carl_octubre = row["Octubre"].ToString();
                            cArchivos.carl_noviembre = row["Noviembre"].ToString();
                            cArchivos.carl_diciembre = row["Diciembre"].ToString();

                            lstLineEnsa.Add(cArchivos);
                        
                    }
                    
                }

                return lstLineEnsa;
            }
            catch
            {
                throw;
            }
        }

        public void add(params GE_TCARGUEARCHIVOSLABORAL[] objeto)
        {
            try
            {
                _CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TCARGUEARCHIVOSLABORAL> leerDatos(string subCat, int pHojaIndex, String pRutaArchivo, String usuario)
        {
            try
            {
                CUtilidades util = new CUtilidades();
                
                decimal vlr = 1;

                IVarEconomicas varEc = new CVarEconomicas();

                IList<GE_TPERSONAS> CPersonas = new CPersonas().GetAll();
                IList<GE_TCOMPANIAS> CCompanias = new CCompanias().GetAll();
                IList<GE_TCENTROSCOSTOS> CCostos = new CCentroCosto().GetAll();
                IList<GE_TPRODUCTOS> CProductos = new CProductos().GetAll();
                IList<GE_TPRODUCTOSITEMS> CItems = new CProductosItems().GetAll();
                IList<GE_TPARAMETROS> CtrParametros = new CParametros().GetAll();
                IList<GE_TPARAMETROS> pMeses = new CParametros().GetListbyClaseOrdenada("MESES").OrderBy(x => Convert.ToInt32(x.parm_codigo)).ToList();
                
                GE_TPERIODOPRESUPUESTO periodo = new CPeriodoPresupuesto().GetPeriodoActivo();

                string mTipo = subCat.Equals("CE") ? "CE" : "GA";

                // IList<GE_TCENTROSCOSTOS> CCostoPers = new CCentroCosto().GetAllUsuarioCentros(usuario, mTipo);

                List<GE_TCARGUEARCHIVOSLABORAL> lstLineEnsa = new List<GE_TCARGUEARCHIVOSLABORAL>();
                DataTable tabla = util.ExceltoDataTable(pHojaIndex, pRutaArchivo);
                foreach (DataRow row in tabla.Rows)
                {
                    if (!row.IsNull(0))
                    {
                        
                        GE_TCARGUEARCHIVOSLABORAL cArchivos = new GE_TCARGUEARCHIVOSLABORAL();
                        cArchivos.carl_categoria = mTipo;
                        cArchivos.carl_subcategoria = subCat;

                        // Compañias y Centro de Costos
                        cArchivos.carl_empresa = row["Empresa"].ToString();
                        cArchivos.carl_ccostos = row["Centro Costos"].ToString();
                            
                        GE_TCOMPANIAS esComp = CCompanias.Where(x => x.comp_nombre.ToLower().Equals(row["Empresa"].ToString().ToLower())).FirstOrDefault();                        
                        if (esComp == null)
                        {
                            cArchivos.carl_observaciones += "Empresa No Existe. ";
                        }
                        else if(esComp.comp_activo == 0)
                        {
                            cArchivos.carl_observaciones += "Empresa Inactiva. ";
                        }
                        else
                        {
                            // Centro de Costos
                            GE_TCENTROSCOSTOS esCCosto = CCostos.Where(x => x.cost_codigo.ToLower() == row["Centro Costos"].ToString().ToLower() && x.cost_empresa == esComp.comp_consecutivo).FirstOrDefault();

                            if (esCCosto == null)
                            {
                                cArchivos.carl_observaciones += "Centro de Costos No Existe. ";
                            }
                            else if (esCCosto.cost_activo == 0)
                            {
                                cArchivos.carl_observaciones += "Centro de Costos Inactivo. ";
                            }
                            else if (esCCosto.cost_empresa != esComp.comp_consecutivo)
                            {
                                cArchivos.carl_observaciones += "Centro de Costos No pertenece a la Empresa. ";
                            }
                            else
                            {
                                bool isUser = CPersonas.Any(x => x.pers_usudom != null && x.pers_usudom.ToUpper().Equals(row["Usuario"].ToString().ToUpper()));
                                if (!isUser)
                                {
                                    cArchivos.carl_observaciones += "La persona NO Existe. ";
                                }
                                else
                                {
                                    IList<GE_TCENTROSCOSTOS> CCostoPers = new CCentroCosto().GetAllUsuarioCentros(row["Usuario"].ToString(), mTipo);

                                    bool myCcosto = CCostoPers.Any(x => x.cost_codigo.ToLower().Equals(row["Centro Costos"].ToString().ToLower()) && x.GE_TCOMPANIAS.comp_consecutivo == esComp.comp_consecutivo);
                                    if (myCcosto)
                                    {
                                        cArchivos.carl_ccostos = row["Centro Costos"].ToString();
                                    }
                                    else
                                    {
                                        cArchivos.carl_observaciones += "No puede presupuestar en este Centro de Costo para esta Categoría. ";
                                    }
                                }
                            }
                        }

                        
                        // Productos e Items
                        cArchivos.carl_producto = row["Producto"].ToString();
                        cArchivos.carl_item = row["Item"].ToString();

                        GE_TPRODUCTOS esProd = CProductos.Where(x => x.prod_codigo.ToLower().Equals(row["Producto"].ToString().ToLower())).FirstOrDefault();
                        if (esProd == null)
                        {
                            cArchivos.carl_observaciones += "Producto No existe. ";

                        }
                        else if (esProd.prod_activo == 0)
                        {
                            cArchivos.carl_observaciones += "Producto Inactivo. ";
                        }
                        else
                        {
                            
                            GE_TPRODUCTOSITEMS esItem = CItems.Where(x => x.prit_item.ToLower() == row["Item"].ToString().ToLower() && x.prit_producto == esProd.prod_consecutivo).FirstOrDefault();

                            if (esItem == null)
                            {
                                cArchivos.carl_observaciones += "Item No Existe. ";
                            }
                            else if (esItem.prit_activo == 0)
                            {
                                cArchivos.carl_observaciones += "Item Inactivo. ";
                            }
                            else
                            {
                                cArchivos.carl_item = row["Item"].ToString();
                            }
                        }
                        

                        // Parámetro Moneda
                        cArchivos.carl_moneda = row["Moneda"].ToString();

                        GE_TPARAMETROS esMoneda = CtrParametros.Where(x => x.parm_codigo == row["Moneda"].ToString().ToUpper()).FirstOrDefault();
                        if (esMoneda == null)
                        {
                            cArchivos.carl_observaciones += "Moneda No Existe. ";
                        }
                        else 
                        {
                            if (esMoneda.parm_estado == 0)
                            {
                                cArchivos.carl_observaciones += "Moneda Inactiva. ";
                            }
                            else
                            {
                                cArchivos.carl_moneda = row["Moneda"].ToString();
                            }
                        }

                        
                        // valor
                        double a;
                        if (row["Valor"].ToString() == "" || !Double.TryParse(row["Valor"].ToString(), out a))
                        {
                            cArchivos.carl_observaciones += "El valor no es un numero. ";
                            cArchivos.carl_valor = 0;
                        }
                        else
                        {                       
                            cArchivos.carl_valor =  Convert.ToInt32(Convert.ToDouble(row["Valor"].ToString()));
                        }


                        // cantidad
                        if (row["Cantidad"].ToString() == "" || !Double.TryParse(row["Cantidad"].ToString(), out a))
                        {
                            cArchivos.carl_observaciones += "La cantidad no es un numero. ";
                            cArchivos.carl_cantidad  = 0;
                        }
                        else
                        {                       
                            cArchivos.carl_cantidad = Convert.ToInt32(Convert.ToDouble(row["Cantidad"].ToString()));
                        }


                        cArchivos.carl_periodo = periodo.peri_consecutivo;


                        double n;
                        // Enero
                        if(row["Enero"].ToString() == "" || !Double.TryParse(row["Enero"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Enero no es un numero. ";
                            cArchivos.carl_enero = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_enero = row["Enero"].ToString();
                        }

                        // Febrero
                        if(row["Febrero"].ToString() == "" || !Double.TryParse(row["Febrero"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Febrero no es un numero. ";
                            cArchivos.carl_febrero = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_febrero = row["Febrero"].ToString();
                        }

                        // Marzo
                        if(row["Marzo"].ToString() == "" || !Double.TryParse(row["Marzo"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Marzo no es un numero. ";
                            cArchivos.carl_marzo = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_marzo = row["Marzo"].ToString();
                        }

                        // Abril
                        if(row["Abril"].ToString() == "" || !Double.TryParse(row["Abril"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Abril no es un numero. ";
                            cArchivos.carl_abril = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_abril = row["Abril"].ToString();
                        }

                        // Mayo
                        if(row["Mayo"].ToString() == "" || !Double.TryParse(row["Mayo"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Mayo no es un numero. ";
                            cArchivos.carl_mayo = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_mayo = row["Mayo"].ToString();
                        }

                        // Junio
                        if(row["Junio"].ToString() == "" || !Double.TryParse(row["Junio"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Junio no es un numero. ";
                            cArchivos.carl_junio = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_junio = row["Junio"].ToString();
                        }

                        // Julio
                        if(row["Julio"].ToString() == "" || !Double.TryParse(row["Julio"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Julio no es un numero. ";
                            cArchivos.carl_julio = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_julio = row["Julio"].ToString();
                        }
                        
                        // Agosto
                        if(row["Agosto"].ToString() == "" || !Double.TryParse(row["Agosto"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Agosto no es un numero. ";
                            cArchivos.carl_agosto = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_agosto = row["Agosto"].ToString();
                        }

                        // Septiembre
                        if(row["Septiembre"].ToString() == "" || !Double.TryParse(row["Septiembre"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Septiembre no es un numero. ";
                            cArchivos.carl_septiembre = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_septiembre = row["Septiembre"].ToString();
                        }

                        // Octubre
                        if(row["Octubre"].ToString() == "" || !Double.TryParse(row["Octubre"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Octubre no es un numero. ";
                            cArchivos.carl_octubre = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_octubre = row["Octubre"].ToString();
                        }

                        // Noviembre
                        if(row["Noviembre"].ToString() == "" || !Double.TryParse(row["Noviembre"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Noviembre no es un numero. ";
                            cArchivos.carl_noviembre = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_noviembre = row["Noviembre"].ToString();
                        }

                        // Diciembre
                        if(row["Diciembre"].ToString() == "" || !Double.TryParse(row["Diciembre"].ToString(), out n))
                        {
                            cArchivos.carl_observaciones += "El valor de Enero no es un numero. ";
                            cArchivos.carl_diciembre = "0";
                        }
                        else
                        {                       
                            cArchivos.carl_diciembre = row["Diciembre"].ToString();
                        }

                        var suma = Convert.ToDouble(cArchivos.carl_enero) + Convert.ToDouble(cArchivos.carl_febrero) + Convert.ToDouble(cArchivos.carl_marzo) +
                                    Convert.ToDouble(cArchivos.carl_abril) + Convert.ToDouble(cArchivos.carl_mayo) + Convert.ToDouble(cArchivos.carl_junio) +
                                    Convert.ToDouble(cArchivos.carl_julio) + Convert.ToDouble(cArchivos.carl_agosto) + Convert.ToDouble(cArchivos.carl_septiembre) +
                                    Convert.ToDouble(cArchivos.carl_octubre) + Convert.ToDouble(cArchivos.carl_noviembre) + Convert.ToDouble(cArchivos.carl_diciembre);


                        if (esMoneda != null)
                        {
                            if (Convert.ToInt32(suma) != Convert.ToInt32(cArchivos.carl_valor))
                            {
                                cArchivos.carl_observaciones += "El valor total no coincide con la suma de valores de los meses. ";
                            }
                            else
                            {
                                // Enero
                                GE_TPARAMETROS param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("ENERO")).FirstOrDefault();
                                GE_TVARECONOMICAS var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_enero = (Convert.ToDecimal(cArchivos.carl_enero) * vlr).ToString();
                                }

                                // FEBRERO
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("FEBRERO")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_febrero = (Convert.ToDecimal(cArchivos.carl_febrero) * vlr).ToString();
                                }

                                // MARZO
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("MARZO")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_marzo = (Convert.ToDecimal(cArchivos.carl_marzo) * vlr).ToString();
                                }

                                // ABRIL
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("ABRIL")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_abril = (Convert.ToDecimal(cArchivos.carl_abril) * vlr).ToString();
                                }

                                // MAYO
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("MAYO")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_mayo = (Convert.ToDecimal(cArchivos.carl_mayo) * vlr).ToString();
                                }

                                // JUNIO
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("JUNIO")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_junio = (Convert.ToDecimal(cArchivos.carl_junio) * vlr).ToString();
                                }

                                // JULIO
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("JULIO")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_julio = (Convert.ToDecimal(cArchivos.carl_julio) * vlr).ToString();
                                }

                                // AGOSTO
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("AGOSTO")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_agosto = (Convert.ToDecimal(cArchivos.carl_agosto) * vlr).ToString();
                                }

                                // SEPTIEMBRE
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("SEPTIEMBRE")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_septiembre = (Convert.ToDecimal(cArchivos.carl_septiembre) * vlr).ToString();
                                }

                                // OCTUBRE
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("OCTUBRE")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_octubre = (Convert.ToDecimal(cArchivos.carl_octubre) * vlr).ToString();
                                }

                                // NOVIEMBRE
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("NOVIEMBRE")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_noviembre = (Convert.ToDecimal(cArchivos.carl_noviembre) * vlr).ToString();
                                }

                                // DICIEMBRE
                                param = pMeses.Where(x => x.parm_descripcion.ToUpper().Equals("DICIEMBRE")).FirstOrDefault();
                                var = varEc.GetByAnoMes(param.parm_consecutivo, esMoneda.parm_consecutivo, periodo.peri_ano);

                                if (var != null)
                                {
                                    vlr = var.vari_valor;
                                    cArchivos.carl_diciembre = (Convert.ToDecimal(cArchivos.carl_diciembre) * vlr).ToString();
                                }
                            }
                        }
                        
                        // cArchivos.carl_usuario = usuario;
                        cArchivos.carl_usuario = row["Usuario"].ToString();
                        cArchivos.carl_fecha = DateTime.Now;

                        lstLineEnsa.Add(cArchivos);                            
                        
                    }
                }

                return lstLineEnsa;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void Guardar(IList<GE_TCARGUEARCHIVOSLABORAL> lstPpto, String strUsr)
        {
            try
            {
                DateTime dtFecha = DateTime.Now;
                CPeriodoPresupuesto periodoPPTO = new CPeriodoPresupuesto();
                GE_TPERIODOPRESUPUESTO ppto = periodoPPTO.GetPeriodoActivo();                    
                // Se eliminan todos los registros del periodo activo para
                // cargarlos de nuevo.

                /*
                _CRUD.DeleteWhere(t => t.carl_periodo == ppto.peri_consecutivo && t.carl_usuario.Equals(strUsr));                
                _CRUDAMORT.DeleteWhere(x => x.sali_periodo == ppto.peri_consecutivo && x.sali_usuario.ToLower().Equals(strUsr.ToLower()));
                */

                GE_TCARGUEARCHIVOSLABORAL first = lstPpto[0];

                // _CRUDCMP.DeleteWhere(x => x.petr_periodo == ppto.peri_consecutivo && x.petr_usuario.Equals(strUsr) && x.petr_tipo.ToUpper().Equals(first.carl_subcategoria));

                IList<GE_TPARAMETROS> lstParams = new CParametros().GetAll();
                IList<GE_TPRODUCTOSITEMS> lstItems = new CProductosItems().GetAll();
                IList<GE_TCENTROSCOSTOS> lstCCtos = new CCentroCosto().GetAll();
                GE_TCLASESPARAMETROS clase = _CRUDCLASES.GetSingle(x => x.clap_nombre.ToUpper() == "MESES");
                IList<GE_TPERSONAS> lstPersonas = new CPersonas().GetAll();

                IList<String> usuarios = lstPpto.Select(x => x.carl_usuario).Distinct().ToList();

                foreach (var mUser in usuarios)
                {
                    _CRUD.DeleteWhere(t => t.carl_periodo == ppto.peri_consecutivo 
                                            && t.carl_usuario.ToLower().Equals(mUser.ToLower()));

                    _CRUDAMORT.DeleteWhere(x => x.sali_periodo == ppto.peri_consecutivo 
                                                && x.sali_usuario.ToLower().Equals(mUser.ToLower()) 
                                                && x.sali_tipo.ToUpper().Equals(first.carl_subcategoria));

                    _CRUDCMP.DeleteWhere(x => x.petr_periodo == ppto.peri_consecutivo 
                                            && x.petr_usuario.ToLower().Equals(mUser.ToLower()) 
                                            && x.petr_tipo.ToUpper().Equals(first.carl_subcategoria));
                }
                

                foreach (GE_TCARGUEARCHIVOSLABORAL row in lstPpto)
                {
                    GE_TPERSONAS p = lstPersonas.Where(x => x.pers_usudom != null && x.pers_usudom.ToUpper().Equals(row.carl_usuario.ToUpper())).FirstOrDefault();

                      GE_TCARGUEARCHIVOSLABORAL cArchivos = new GE_TCARGUEARCHIVOSLABORAL();
                        
                      cArchivos.carl_categoria = row.carl_categoria;
                      cArchivos.carl_subcategoria = row.carl_subcategoria;
                      cArchivos.carl_empresa = row.carl_empresa;
                      cArchivos.carl_ccostos = row.carl_ccostos;
                      cArchivos.carl_producto = row.carl_producto;
                      cArchivos.carl_item = row.carl_item;
                      cArchivos.carl_moneda = row.carl_moneda;
                      cArchivos.carl_valor = row.carl_valor;
                      cArchivos.carl_cantidad = row.carl_cantidad;
                      cArchivos.carl_periodo = row.carl_periodo;

                      cArchivos.carl_enero = row.carl_enero;
                      cArchivos.carl_febrero = row.carl_febrero;
                      cArchivos.carl_marzo = row.carl_marzo;
                      cArchivos.carl_abril = row.carl_abril;
                      cArchivos.carl_mayo = row.carl_mayo;
                      cArchivos.carl_junio = row.carl_junio;
                      cArchivos.carl_julio = row.carl_julio;
                      cArchivos.carl_agosto = row.carl_agosto;
                      cArchivos.carl_septiembre = row.carl_septiembre;
                      cArchivos.carl_octubre = row.carl_octubre;
                      cArchivos.carl_noviembre = row.carl_noviembre;
                      cArchivos.carl_diciembre = row.carl_diciembre;
                      cArchivos.carl_observaciones = row.carl_observaciones;

                      // cArchivos.carl_usuario = strUsr;
                      cArchivos.carl_usuario = row.carl_usuario;
                      cArchivos.carl_fecha = DateTime.Now;

                      this.add(cArchivos);

                      GE_TPERIODOTRANSACCIONES pt = new GE_TPERIODOTRANSACCIONES();
                      pt.petr_activo = 1;
                      pt.petr_amortizar = 0;
                      pt.petr_cantidad = Convert.ToInt32(row.carl_cantidad);

                      GE_TCENTROSCOSTOS ccto = _CRUDCCTOS.GetSingle(x => x.cost_codigo == row.carl_ccostos && x.GE_TCOMPANIAS.comp_nombre.ToUpper() == row.carl_empresa.ToUpper());
                      pt.petr_centrocosto = Convert.ToInt32(ccto.cost_consecutivo);
                    
                      pt.petr_fecha = DateTime.Now;
                      pt.petr_fecha_act = DateTime.Now;
                      pt.petr_mes = 1;
                      pt.petr_observacion = "";
                      pt.petr_periodo = row.carl_periodo;

                      pt.petr_persona = p.pers_consecutivo;

                      GE_TPARAMETROS param = _CRUDPARAM.GetSingle(x => x.parm_codigo.ToUpper() == row.carl_moneda.ToUpper() && x.parm_estado == 1);
                      pt.petr_moneda = param.parm_consecutivo;

                      GE_TPRODUCTOSITEMS item = _CRUDITEMS.GetSingle(x => x.prit_item.ToUpper() == row.carl_item.ToUpper());
                      pt.petr_producto_item = item.prit_consecutivo;

                      pt.petr_proveedor = 1;
                      pt.petr_tipo_viaje = null;
                      pt.petr_trm = 1;
                      pt.petr_valor = Convert.ToDecimal(row.carl_valor);
                      pt.petr_valor_amortizar = 0;

                    /* 
                      pt.petr_usuario = strUsr;
                      pt.petr_usuario_act = strUsr;
                    */

                      pt.petr_usuario = row.carl_usuario;
                      pt.petr_usuario_act = row.carl_usuario; 

                      pt.petr_tipo = row.carl_subcategoria;
                      pt.petr_meses_amortizar = 1;
                      
                      _CRUDCMP.Add(pt);
                      
                      IList<GE_TPARAMETROS> meses = lstParams.Where(x => x.clap_clase == clase.clap_clase).ToList();

                      foreach (var mes in meses)
                      {

                          GE_TSALIDAPRESUPUESTO salida1 = new GE_TSALIDAPRESUPUESTO();

                          salida1.sali_periodo_transacc = pt.petr_consecutivo;
                          salida1.sali_persona = pt.petr_persona;
                          salida1.sali_centrocosto = pt.petr_centrocosto;
                          salida1.sali_producto_item = pt.petr_producto_item;
                          salida1.sali_moneda = pt.petr_moneda;
                          salida1.sali_mes = mes.parm_consecutivo;

                          switch (mes.parm_descripcion)
                          {
                              case "ENERO":
                                  salida1.sali_valor = Convert.ToDecimal(row.carl_enero != "" ? row.carl_enero : "0");
                                break;
                              case "FEBRERO":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_febrero != null ? row.carl_febrero : "0");
                                break;
                              case "MARZO":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_marzo != null ? row.carl_marzo : "0");
                                break;
                              case "ABRIL":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_abril != null ? row.carl_abril : "0");
                                break;
                              case "MAYO":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_mayo != null ? row.carl_mayo : "0");
                                break;
                              case "JUNIO":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_junio != null ? row.carl_junio : "0");
                                break;
                              case "JULIO":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_julio != null ? row.carl_julio : "0");
                                break;
                              case "AGOSTO":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_agosto != null ? row.carl_agosto : "0");
                                break;
                              case "SEPTIEMBRE":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_septiembre != null ? row.carl_septiembre : "0");
                                break;
                              case "OCTUBRE":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_octubre != null ? row.carl_octubre : "0");
                                break;
                              case "NOVIEMBRE":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_noviembre != null ? row.carl_noviembre : "0");
                                break;
                              case "DICIEMBRE":
                                salida1.sali_valor = Convert.ToDecimal(row.carl_diciembre != null ? row.carl_diciembre : "0");
                                break;
                          }
                                                    
                          // salida1.sali_usuario = strUsr;
                          salida1.sali_usuario = row.carl_usuario;
                          
                          salida1.sali_fecha = pt.petr_fecha;
                          salida1.sali_tipo = row.carl_subcategoria;
                          salida1.sali_periodo = pt.petr_periodo;

                          _CRUDAMORT.Add(salida1);
                      }                    
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
