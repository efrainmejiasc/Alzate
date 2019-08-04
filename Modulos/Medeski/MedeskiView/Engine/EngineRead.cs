using ExcelDataReader;
using Medeski.DataAcces;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace MedeskiView.Engine
{
    public class EngineRead
    {
        //******************************************************************
        private string colaborador = string.Empty;
        private string cedula = string.Empty;
        private string criterioDistribucion = string.Empty;
        private string tipoDistribucion = string.Empty;
        private string area = string.Empty;
        private string codigoArea = string.Empty;
        private string descripcion = string.Empty;
        private string codigoDescripcion = string.Empty;
        private string porcentajeDistribucion = string.Empty;
        private string observacion = string.Empty;
        private string personaConsecutivo = string.Empty;
        //*********************************************************************
        private string productoDirecto = string.Empty;
        private int codigoProducto =  0;
        private decimal cantidad = 0;
        private decimal sumatoria = 0;

        private CtrDistribMAS Cdist;

        public DataTable ReadExcelDistribucionPersonas(string filePath,string usuario)
        {
            if (filePath == string.Empty || filePath == null)
                return null;
            try
            {
                EngineDb Metodo = new EngineDb();
                List<PersonaGente> personas = Metodo.GetDataForCargueDistribucionPersona();
                PersonaGente empleado = new PersonaGente();
                List<GE_TPRODUCTOS> productos = Productos();
                GE_TPRODUCTOS producto = new GE_TPRODUCTOS();
                List<GE_TSERVIDORES> servidores = Servidores();
                GE_TSERVIDORES servidor = new GE_TSERVIDORES();
                DataTable dt = new DataTable();
                DataTable dt2 = AgregarColumnasDistribucionPersonas(dt);

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        dt = result.Tables[0];
                        int n = 0;
                        foreach (DataRow r in dt.Rows)
                        {
                            if (n > 0)
                            {
                                this.observacion = string.Empty;
                                this.colaborador = string.Empty;
                                this.criterioDistribucion = string.Empty;
                                this.tipoDistribucion = string.Empty;
                                this.area = string.Empty;
                                this.descripcion = string.Empty;
                                this.porcentajeDistribucion = "0.00";
                                this.codigoDescripcion = string.Empty;
                                this.personaConsecutivo = string.Empty;
                                if (r[0] != null)
                                    colaborador = r[0].ToString().ToUpper();
                                if (r[1] != null)
                                    cedula = r[1].ToString();
                                try
                                {
                                    empleado = personas.Where(s => s.pers_nombres == colaborador.Trim() || s.pers_identificacion == cedula).FirstOrDefault();
                                    colaborador = empleado.pers_nombres;
                                    cedula = empleado.pers_identificacion;
                                    personaConsecutivo = empleado.pers_consecutivo;
                                }
                                catch
                                {
                                    observacion = "Error: Verifique cedula o nombre del colaborador. ";
                                }

                                if (r[2] != null)
                                    criterioDistribucion = r[2].ToString().ToUpper();
                                if (criterioDistribucion.ToUpper() == "PRODUCTOS")
                                    tipoDistribucion = "Productos";
                                else if (criterioDistribucion.ToUpper() == "SERVIDORES")
                                    tipoDistribucion = "Infraestructura";

                                if (r[4] != null)
                                    area = r[4].ToString();

                                if (r[5] != null)
                                    descripcion = r[5].ToString();
                                if (r[6] != null)
                                    codigoDescripcion = r[6].ToString();
                                if (criterioDistribucion == "PRODUCTOS")
                                {
                                    try
                                    {
                                        producto = productos.Where(s => s.prod_codigo == descripcion || s.prod_consecutivo == Convert.ToInt32(codigoDescripcion)).FirstOrDefault();
                                        codigoDescripcion = producto.prod_consecutivo.ToString();
                                        descripcion = producto.prod_descripcion;
                                        codigoDescripcion = producto.prod_consecutivo.ToString();
                                    }
                                    catch
                                    {
                                        observacion = observacion + " " + descripcion + " No existe el producto.";
                                    }
                                }
                                else if (criterioDistribucion == "SERVIDORES")
                                {
                                    try
                                    {
                                        servidor = servidores.Where(s => s.serv_nombre == descripcion || s.serv_consecutivo == Convert.ToInt32(codigoDescripcion)).FirstOrDefault();
                                        codigoDescripcion = servidor.serv_consecutivo.ToString();
                                        descripcion = servidor.serv_nombre;
                                        codigoDescripcion = servidor.serv_consecutivo.ToString();
                                    }
                                    catch
                                    {
                                        observacion = observacion + " " + descripcion + " No existe el servidor.";
                                    }

                                }
                                else
                                {
                                    observacion = observacion + " " + " El criterio de la distribucion debe ser PRODUCTOS / SERVIDORES.";
                                }

                                if (r[7] != null)
                                    porcentajeDistribucion = r[7].ToString();


                                dt2.Rows.Add(colaborador, cedula, criterioDistribucion, tipoDistribucion, codigoArea, area, descripcion, codigoDescripcion, porcentajeDistribucion, observacion.Trim(), DateTime.Now, 1, usuario, 1, personaConsecutivo);
                            }
                            n++;
                        }

                    }
                }
                return dt2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable AgregarColumnasDistribucionPersonas(DataTable dt)
        {
            dt.Columns.Add("Colaborador");//0
            dt.Columns.Add("Cedula");//1
            dt.Columns.Add("CriterioDistribucion");//2
            dt.Columns.Add("TipoDistribucion");//3
            dt.Columns.Add("CodigoArea");//4
            dt.Columns.Add("Area");//5
            dt.Columns.Add("Descripcion");//6
            dt.Columns.Add("CodigoDescripcion");//7
            dt.Columns.Add("PorcentajeDistribucion");//8
            dt.Columns.Add("Observacion");//9
            dt.Columns.Add("Fecha");//10
            dt.Columns.Add("Periodo");//11
            dt.Columns.Add("Usuario");//12
            dt.Columns.Add("Estado");//13
            dt.Columns.Add("PersonaConsecutivo");//14
            return dt;
        }



        public List<GE_TPERSONAS> Personas()
        {
            using (var contEnt = new Entities())
            {
                return contEnt.GE_TPERSONAS.ToList();
            }
        }

        public List<GE_TPRODUCTOS> Productos()
        {
            using (var contEnt = new Entities())
            {
                return contEnt.GE_TPRODUCTOS.ToList();
            }
        }

        public List<GE_TSERVIDORES> Servidores()
        {
            using (var contEnt = new Entities())
            {
                return contEnt.GE_TSERVIDORES.ToList();
            }
        }


        //**************************************************************************************************************************************************************

        public DataTable ReadExcelDistribucionMas(string filePath, string usuario)
        {
            if (filePath == string.Empty || filePath == null)
                return null;

            try
            {

                IList<GE_TDISTRIBUCIONMASPROCESOS> productos = ProductosMas();
                GE_TDISTRIBUCIONMASPROCESOS producto = new GE_TDISTRIBUCIONMASPROCESOS();

                DataTable dt = new DataTable();
                DataTable dt2 = AgregarColumnasDistribucionMas(dt);

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        dt = result.Tables[0];
                        int n = 0;
                        foreach (DataRow r in dt.Rows)
                        {
                            if (n > 0)
                            {
                                this.productoDirecto = string.Empty;
                                this.observacion = string.Empty;
                                this.cantidad = 0;
                                this.codigoProducto = 0;

                                if (r[0] != null)
                                {
                                    try
                                    {
                                        this.productoDirecto = r[0].ToString();
                                        producto = productos.Where(s => s.GE_TPRODUCTOS.prod_descripcion == productoDirecto.Trim()).FirstOrDefault();
                                        codigoProducto = Convert.ToInt32(producto.GE_TPRODUCTOS.prod_consecutivo);
                                    }
                                    catch
                                    {
                                        observacion = "El producto " + producto + " no esta registrado, verifique q este bien escrito.";
                                    }
                                }

                                if (r[1] != null && r[0] != null)
                                    this.cantidad = Convert.ToDecimal(r[1]);
                                else
                                    this.observacion = "Producto y Cantidad requeridos";

                                if (r[1] != null && r[0] != null)
                                    sumatoria = sumatoria + cantidad;

                                dt2.Rows.Add(productoDirecto, codigoProducto, cantidad.ToString("N3"), 0, observacion,usuario,DateTime.Now);
                            }
                            n++;
                        }

                    }
                }
                EngineProyect Funcion = new EngineProyect();
                dt2 = Funcion.PorcentajeCantidadesMas(dt2, sumatoria);
                return dt2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable AgregarColumnasDistribucionMas(DataTable dt)
        {
            dt.Columns.Add("ProductoDirecto");//0
            dt.Columns.Add("CodigoProducto"); //1
            dt.Columns.Add("Cantidad");//2
            dt.Columns.Add("Porcentaje");//3
            dt.Columns.Add("Observacion");//4
            dt.Columns.Add("Usuario");//5
            dt.Columns.Add("Fecha");//6
            return dt;
        }

        public IList<GE_TDISTRIBUCIONMASPROCESOS> ProductosMas()
        {
            Cdist = new CtrDistribMAS();
            int periodo = Convert.ToInt32(HttpContext.Current.Session["periodo"]);
            IList<GE_TDISTRIBUCIONMASPROCESOS> productos = Cdist.GetAllProductosDistrib(periodo);
            return productos;
        }

    }
}