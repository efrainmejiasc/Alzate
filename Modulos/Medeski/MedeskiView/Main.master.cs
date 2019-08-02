using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MedeskiView {
    public partial class MainMaster : System.Web.UI.MasterPage {

        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        IList<GE_TOPCIONESMENU> opcM;

        protected void Page_Load(object sender, EventArgs e)
        {
            Cargar_Menu();            
        }
        
        #region Eventos

        protected void ASPxNavBar1_ItemClick(object source, DevExpress.Web.NavBarItemEventArgs e)
        {
            if (Session["usuario"] == null)
            {
                string strUrl = "Forms/" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;
                Response.Redirect(strUrl);
            }
        }
        
        #endregion


        #region Metodos

        private HtmlGenericControl addChilds(IList<GE_TOPCIONESMENU> lista, string titulo)
        {
            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.Attributes.Add("class", "collapse list-unstyled");
            ul.Attributes.Add("id", titulo.Replace(" ", "_"));
                    
            foreach (var hijo in lista)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");

                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.InnerText = hijo.opcm_titulo;
                
                IList<GE_TOPCIONESMENU> nietos = opcM.Where(x => x.opcm_idpadre == hijo.opcm_consecutivo).ToList();
                
                if (nietos.Count != 0)
                {
                    anchor.Attributes.Add("href", "#" + hijo.opcm_consecutivo + "_" + hijo.opcm_titulo.ToString().Replace(" ", "_"));
                    anchor.Attributes.Add("data-toggle", "collapse");
                    anchor.Attributes.Add("aria-expanded", "false");

                    li.Controls.Add(anchor);
                    li.Controls.Add(addChilds(nietos, hijo.opcm_consecutivo + "_" + hijo.opcm_titulo.ToString()));
                }
                else
                {
                    anchor.Attributes.Add("href", "../" + hijo.opcm_url);
                    // anchor.Attributes.Add("onclick", "loadPanel()");
                    li.Controls.Add(anchor);
                }
                
                ul.Controls.Add(li);
            }

            return ul;
        }


        private void AddMenuItem(IList<GE_TOPCIONESMENU> op)
        {
            foreach (var item in op)
            {
                if (item.opcm_idpadre == 0)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "#" + item.opcm_consecutivo + "_" + item.opcm_titulo.ToString().Replace(" ", "_"));
                    anchor.Attributes.Add("data-toggle", "collapse");
                    anchor.Attributes.Add("aria-expanded", "false");

                    HtmlGenericControl icon = new HtmlGenericControl("i");
                    icon.Attributes.Add("class", item.opcm_ruta_imagen);
                    anchor.Controls.Add(icon);

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerText = item.opcm_titulo;
                    anchor.Controls.Add(span);

                    li.Controls.Add(anchor);

                    IList<GE_TOPCIONESMENU> hijos = opcM.Where(x => x.opcm_idpadre == item.opcm_consecutivo)
                                                        .Where(x => x.opcm_tipo == "MENU")
                                                        .ToList();

                    li.Controls.Add(addChilds(hijos, item.opcm_consecutivo + "_" + item.opcm_titulo.ToString()));
                    menuSidebar.Controls.Add(li);
                } // if
            } // foreach
        } 


        public void Cargar_Menu()
        {
            Char delimiter = ';';
            string[] strUsuario = null;
            strUsuario = Session["usuario"].ToString().Split(delimiter);
            CtrOpcionesMenu ctrOpc = new CtrOpcionesMenu();
            opcM = ctrOpc.GetOpcionesMenuxUser(strUsuario[0].ToString().ToUpper())
                            .Where(x => x.opcm_tipo == "MENU")
                            .OrderBy(x => x.opcm_idpadre)
                            .ThenBy(x => x.opcm_idonma)
                            .ToList();

            if (opcM.Count > 0)
            {     
                AddMenuItem(opcM);
            }
        }
        #endregion


    }
}