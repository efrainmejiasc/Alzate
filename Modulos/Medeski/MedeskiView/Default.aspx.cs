using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace MedeskiView {
    public partial class _Default : System.Web.UI.Page 
    {
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrVwCuadroServicioTotal Cvwsalida = new CtrVwCuadroServicioTotal();

        protected void Page_Load(object sender, EventArgs e) 
        {
            string strUrl = ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] == null)
            {
                Response.Redirect(strUrl);
            }
            else
            {
                if(!IsPostBack)
                    LoadCharts();
            }
        }

        private void LoadCharts()
        {
            IList<VW_VLR_CUADRO_SERVICIO_TOTAL> salida = Cvwsalida.GetAll();

            List<object> salida1 = salida.GroupBy(x => new { x.servicio }).Select(x => new { servicio = x.Key.servicio, total = x.Sum(y => y.Total) }).ToList<object>();            
            var json = JsonConvert.SerializeObject(salida1);
            ScriptManager.RegisterStartupScript(this, GetType(), "Charts", "printDona('" + json + "');", true);

            double SalidaTotal = Convert.ToDouble(salida.Where(x => x.servicio.Equals("SERVICIO DE APLICACIONES EMPRESARIALES")).Sum(x => x.Total));

            List<object> salida2 = salida.Where(x => x.servicio.Equals("SERVICIO DE APLICACIONES EMPRESARIALES"))
                                        .GroupBy(x => new { x.producto })
                                        .Select(x => new { producto = x.Key.producto, total = x.Sum(y => y.Total), porcentaje = (Convert.ToDouble(x.Sum(y => y.Total)) / SalidaTotal * 100).ToString() + "%" })
                                        .OrderByDescending(x => x.total)
                                        .Take(10)
                                        .ToList<object>();
            var json2 = JsonConvert.SerializeObject(salida2);
            ScriptManager.RegisterStartupScript(this, GetType(), "Charts2", "printBars('" + json2 + "');", true);
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                Cvwsalida.UpdateCuadros();
                LoadCharts();
            }
            catch (Exception ex)
            {                
                throw;
            }
                
        }
        
    }
}