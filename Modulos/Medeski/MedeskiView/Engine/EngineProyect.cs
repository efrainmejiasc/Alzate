using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MedeskiView.Engine
{
    public class EngineProyect
    {
        private decimal sumServCdm = 0;
        private decimal sumServDesarrollo = 0;
        private decimal sumServGerenciaTecnica = 0;
        private decimal sumServInfraestructura = 0;
        private decimal sumServJefatura = 0;
        private decimal sumServOperaciones = 0;

        private decimal sumProdCdm = 0;
        private decimal sumProdDesarrollo = 0;
        private decimal sumProdGerenciaTecnica = 0;
        private decimal sumProdInfraestructura = 0;
        private decimal sumProdJefatura = 0;
        private decimal sumProdOperaciones = 0;



        public Dictionary<string, Decimal> ValidarDistribucion (DataTable dt)
        {
            Dictionary<string, Decimal> Sumatoria = new Dictionary<string, Decimal>(); 
            foreach (DataRow r in dt.Rows)
            {
                if (r["CriterioDistribucion"] != null)
                {
                    if (r["CriterioDistribucion"].ToString().ToUpper() == "SERVIDORES")
                    {
                        if (r["Area"] != null)
                        {
                            if (r["Area"].ToString().ToUpper() == "CDM")
                            {
                                sumServCdm = sumServCdm + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "DESARROLLO")
                            {
                                sumServDesarrollo = sumServDesarrollo + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "GERENCIA TECNICA")
                            {
                                sumServGerenciaTecnica = sumServGerenciaTecnica + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "INFRAESTRUCTURA")
                            {
                                sumServInfraestructura = sumServInfraestructura + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "JEFATURA")
                            {
                                sumServJefatura = sumServJefatura + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "OPERACIONES")
                            {
                                sumServOperaciones = sumServOperaciones + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                        }

                    }
                    else if (r["CriterioDistribucion"].ToString().ToUpper() == "PRODUCTOS")
                    {
                        if (r["Area"] != null)
                        {
                            if (r["Area"].ToString().ToUpper() == "CDM")
                            {
                                sumProdCdm = sumProdCdm + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "DESARROLLO")
                            {
                                sumProdDesarrollo = sumProdDesarrollo + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "GERENCIA TECNICA")
                            {
                                sumProdGerenciaTecnica = sumProdGerenciaTecnica + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "INFRAESTRUCTURA")
                            {
                                sumProdInfraestructura = sumProdInfraestructura + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "JEFATURA")
                            {
                                sumProdJefatura = sumProdJefatura + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }
                            if (r["Area"].ToString().ToUpper() == "OPERACIONES")
                            {
                                sumProdOperaciones = sumProdOperaciones + Convert.ToDecimal(r["PorcentajeDistribucion"]);
                            }

                        }
                    }
                } 
            }

            Sumatoria.Add("Productos Cdm", sumProdCdm);
            Sumatoria.Add("Productos Desarrollo",sumProdDesarrollo);
            Sumatoria.Add("Productos Gerencia Tecnica", sumProdGerenciaTecnica);
            Sumatoria.Add("Productos Infraestructura", sumProdInfraestructura);
            Sumatoria.Add("Productos Jefatura", sumProdJefatura);
            Sumatoria.Add("Productos Operaciones", sumProdOperaciones);

            Sumatoria.Add("Servidores Cdm", sumServCdm);
            Sumatoria.Add("Servidores Desarrollo", sumServDesarrollo);
            Sumatoria.Add("Servidores Gerencia Tecnica", sumServGerenciaTecnica);
            Sumatoria.Add("Servidores Infraestructura", sumServInfraestructura);
            Sumatoria.Add("Servidores Jefatura", sumServJefatura);
            Sumatoria.Add("Servidores Operaciones", sumServOperaciones);

            return Sumatoria;
        }


        public DataTable PorcentajeCantidadesMas(DataTable dt,decimal sumatoria)
        {
            decimal porcentaje = 0;
            HttpContext.Current.Session["SumatoriaMas"] = sumatoria;
            foreach (DataRow r in dt.Rows)
            {
                porcentaje = 0;
                if (r["Cantidad"] != null && r["Observacion"].ToString () == string.Empty)
                {
                    porcentaje = Convert.ToDecimal(r["Cantidad"]) * 100 / sumatoria;
                    r["porcentaje"] = porcentaje.ToString("N3");
                }
            }
            return dt;
        }
    }
}