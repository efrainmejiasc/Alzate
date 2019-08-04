using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedeskiView.Engine
{
    public class EngineDb
    {
        private string Conexion = ConfigurationManager.ConnectionStrings["Alzate"].ToString();
        private int periodo = 0;
        private string tipo = string.Empty;
        private int persona = 0;
        private int servidor = 0;
        private int producto = 0;
        private decimal valor = 0;
        private int estado = 0;
        private string usuario = string.Empty;
        private DateTime fecha = DateTime.Now;

        public List<PersonaGente> GetDataForCargueDistribucionPersona()
        {
            List<PersonaGente> dataList = new List<PersonaGente>();
            using (SqlConnection Cnx = new SqlConnection(Conexion))
            {
                Cnx.Open();
                SqlCommand command = new SqlCommand("dbo.Sp_DatosGentePersona", Cnx);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    PersonaGente data = new PersonaGente();
                    data.pers_consecutivo = lector.GetInt32(0).ToString();
                    data.gent_ccostos = lector.GetInt32(1).ToString();
                    try { data.gent_ceop = lector.GetInt32(2).ToString(); }
                    catch {data.gent_ceop = string.Empty;}
                    data.gent_consecutivo = lector.GetInt32(3).ToString();
                    try {data.gent_costo_colaborador = lector.GetDecimal(4).ToString("N2");}
                    catch { data.gent_costo_colaborador = "0.00"; }
                    data.gent_descripcion_ccostos = lector.GetString(5);
                    data.gent_empresa = lector.GetString(6);
                    try {data.gent_estado = lector.GetInt32(7).ToString(); }
                    catch { data.gent_estado = string.Empty; }     
                    data.gent_nombre_cargo = lector.GetString(8);
                    data.gent_periodo = lector.GetInt32(9).ToString();
                    try { data.gent_porcentaje_manual_dedicacion = lector.GetDecimal(10).ToString("N2"); }
                    catch { data.gent_porcentaje_manual_dedicacion = "0,00"; }
                    try { data.gent_persona = lector.GetInt32(11).ToString(); }
                    catch { data.gent_persona = "0"; }
                    try { data.pers_nombres = lector.GetString(12); }
                    catch { data.pers_nombres = "0"; }
                    data.pers_nombre_area = lector.GetInt32(13).ToString();
                    data.pers_identificacion = lector.GetString(14); 
                    dataList.Insert(n, data);
                    n++;
                    }
                lector.Close();
                Cnx.Close();
            }
            return dataList;

        }

        public bool InsertDistribucionPersona(DataTable dt)
        {
            bool resultado = false;
            using (SqlConnection Cnx = new SqlConnection(Conexion))
            {
                SqlCommand command = new SqlCommand("Sp_CargueDistribucionPersonas", Cnx);
                command.CommandType = CommandType.StoredProcedure;
                foreach (DataRow m in dt.Rows)
                {
                    if (m[9].ToString() == string.Empty)
                    {
                        try
                        {
                            this.periodo = Convert.ToInt32(m[11]);
                            this.tipo = m[3].ToString();
                            this.persona = Convert.ToInt32(m[14]);
                            this.servidor = Convert.ToInt32(m[7]);
                            this.producto = Convert.ToInt32(m[7]);
                            this.valor = Convert.ToDecimal(m[8]);
                            this.estado = Convert.ToInt32(m[13]);
                            this.usuario = m[12].ToString();
                            this.fecha = Convert.ToDateTime(m[10]);

                            Cnx.Open();
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@dper_periodo", this.periodo);
                            command.Parameters.AddWithValue("@dper_tipo", this.tipo);
                            command.Parameters.AddWithValue("@dper_persona", this.persona);
                            if (m[3].ToString() == "Infraestructura")
                                command.Parameters.AddWithValue("@dper_servidor", this.servidor);
                            else
                                command.Parameters.AddWithValue("@dper_servidor", 0);
                            if (m[3].ToString() == "Productos")
                                command.Parameters.AddWithValue("@dper_producto", this.producto);
                            else
                                command.Parameters.AddWithValue("@dper_producto", 0);
                            command.Parameters.AddWithValue("@dper_valor", this.valor);
                            command.Parameters.AddWithValue("@dper_estado", this.estado);
                            command.Parameters.AddWithValue("@dper_usuario", this.usuario);
                            command.Parameters.AddWithValue("@dper_fecha", this.fecha);
                            command.ExecuteNonQuery();
                            resultado = true;
                            Cnx.Close();
                        }
                        catch(Exception ex )
                        {
                            Cnx.Close();
                        }
                    }
                }


            }
            return resultado;
        }

        public bool InsertDistribucionMas(DataTable dt)
        {
            bool resultado = false;
            using (SqlConnection Cnx = new SqlConnection(Conexion))
            {
                SqlCommand command = new SqlCommand("Sp_CargueDistribucionMas", Cnx);
                command.CommandType = CommandType.StoredProcedure;
                foreach (DataRow m in dt.Rows)
                {
                    if (m[9].ToString() == string.Empty)
                    {
                        try
                        {
                            this.periodo = 1;
                            this.producto = Convert.ToInt32(m[1]);
                            this.valor = Convert.ToDecimal(m[2]);
                            this.usuario = m[5].ToString();
                            this.fecha = Convert.ToDateTime(m[6]);

                            Cnx.Open();
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@dmas_periodo", this.periodo);
                            command.Parameters.AddWithValue("@dmas_producto", this.producto);
                            command.Parameters.AddWithValue("@dmas_valor", this.valor);
                            command.Parameters.AddWithValue("@dmas_usuario", this.usuario);
                            command.Parameters.AddWithValue("@dmas_fecha", this.fecha);
                            command.ExecuteNonQuery();
                            resultado = true;
                            Cnx.Close();
                        }
                        catch (Exception ex)
                        {
                            Cnx.Close();
                        }
                    }
                }


            }
            return resultado;
        }



        public string  InsertarEnTasasSqlBulk(DataTable dt , string tableName)
        {
            string  resultado = "true";
            try
            {
                using (SqlConnection destinationConnection = new SqlConnection(Conexion))
                {
                    destinationConnection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection.ConnectionString))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(dt);
                    }
                    destinationConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return resultado;
        }
    }
}