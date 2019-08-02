using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using DevExpress.Web.ASPxTreeList;

namespace MedeskiView.Controllers
{
    public class CtrUtilidades
    {
        IParametros Iparametros = new CParametros();
        IUsuarios Iusuarios = new CUsuarios();
        IUsuariosxRol Iusuariosxrol = new CUsuariosxRol();
        IUtilidades Iutilidades = new CUtilidades();

        public void ScrollGrid(ASPxGridView grid)
        {
            grid.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            grid.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
            grid.SettingsBehavior.AllowEllipsisInText = true;
            grid.EditFormLayoutProperties.SettingsAdaptivity.AdaptivityMode = FormLayoutAdaptivityMode.SingleColumnWindowLimit;
            grid.EditFormLayoutProperties.SettingsAdaptivity.SwitchToSingleColumnAtWindowInnerWidth = 600;
            grid.Styles.Cell.Wrap = DevExpress.Utils.DefaultBoolean.True;
            grid.Settings.VerticalScrollBarMode = (ScrollBarMode)Enum.Parse(typeof(ScrollBarMode), "Auto");
            grid.Settings.ShowFilterRow = (System.Web.HttpContext.Current.Session["GridFilterRow"].ToString() == "1") ? true : false;
            grid.SettingsEditing.BatchEditSettings.ShowConfirmOnLosingChanges = false;
            
        }

        public void ConfigurarGrid(ASPxGridView grid)
        {
            /*Configuration GRID*/
            
            string [] pagerTam = { "5", "10", "20", "50", "100" };

            grid.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            grid.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
            grid.SettingsBehavior.AllowEllipsisInText = true;
            grid.EditFormLayoutProperties.SettingsAdaptivity.AdaptivityMode = FormLayoutAdaptivityMode.SingleColumnWindowLimit;
            grid.EditFormLayoutProperties.SettingsAdaptivity.SwitchToSingleColumnAtWindowInnerWidth = 600;
            grid.Styles.Cell.Wrap = DevExpress.Utils.DefaultBoolean.True;
            grid.SettingsEditing.BatchEditSettings.ShowConfirmOnLosingChanges = false;

            // grid.SettingsPager.PageSize = Convert.ToInt32(System.Web.HttpContext.Current.Session["GridTamano"].ToString());
            grid.Settings.ShowFilterRow = true;// (System.Web.HttpContext.Current.Session["GridFilterRow"].ToString() == "1") ? true : false;
            grid.Styles.Header.BackColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridHdBackColor"].ToString());
            grid.Styles.Header.ForeColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridHdForeColor"].ToString());
            grid.Styles.Header.Font.Bold = (System.Web.HttpContext.Current.Session["GridHdFontBold"].ToString() == "1") ? true : false;
            grid.Styles.Header.HorizontalAlign = (System.Web.HttpContext.Current.Session["GridHdHorAlign"].ToString() == "CENTER") ? HorizontalAlign.Center : (System.Web.HttpContext.Current.Session["GridHdHorAlign"].ToString() == "LEFT") ? HorizontalAlign.Left : HorizontalAlign.Right;
            grid.Styles.FocusedRow.BackColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridFrBackColor"].ToString());
            grid.Styles.FocusedRow.ForeColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridFrForeColor"].ToString());
            grid.SettingsPager.Position = PagerPosition.Bottom;
            grid.SettingsPager.PageSizeItemSettings.Visible = true; // false;
            grid.SettingsPager.PageSizeItemSettings.Items = pagerTam;
            grid.SettingsPager.PageSizeItemSettings.Caption = "";
            grid.SettingsPager.Summary.Visible = false;
            grid.SettingsBehavior.AllowFocusedRow = (System.Web.HttpContext.Current.Session["GridAllowFocuseRow"].ToString() == "1") ? true : false;
            grid.SettingsBehavior.AllowSelectByRowClick = (System.Web.HttpContext.Current.Session["GridAllowFocuseRow"].ToString() == "1") ? true : false;
            grid.SettingsBehavior.AllowSelectSingleRowOnly = (System.Web.HttpContext.Current.Session["GridAllowFocuseRow"].ToString() == "1") ? true : false;
            grid.Styles.AlternatingRow.Enabled = DevExpress.Utils.DefaultBoolean.True;
            grid.Styles.AlternatingRow.BackColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridAlternativeRowColor"].ToString());
        }

        public void ConfigurarTreeList(ASPxTreeList p_treeList)
        {
            p_treeList.Styles.Header.BackColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridHdBackColor"].ToString());
            p_treeList.Styles.Header.ForeColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridHdForeColor"].ToString());
            p_treeList.Styles.Header.Font.Bold = (System.Web.HttpContext.Current.Session["GridHdFontBold"].ToString() == "1") ? true : false;
            p_treeList.Styles.FocusedNode.BackColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridFrBackColor"].ToString());
            p_treeList.Styles.FocusedNode.ForeColor = System.Drawing.ColorTranslator.FromHtml(System.Web.HttpContext.Current.Session["GridFrForeColor"].ToString());
        }

        public bool UsuarioNoValidar(string strUsuario)
        {
            bool blRetorno = false;
            try
            {
                IEnumerable<GE_TPARAMETROS> lParam = Iparametros.GetListbyClase("USUARIOS_NO_VALIDAR");

                foreach (GE_TPARAMETROS p in lParam)
                {
                    if (p.parm_descripcion == strUsuario)
                    {
                        blRetorno = true;
                        break;
                    }
                }

            }
            catch
            {
                throw;
            }

            return blRetorno;
        }

        public bool IngresoUsuarioNoValidar(string strUsuario, string strContrasena)
        {
            bool blRetorno = false;

            try
            {
                if (UsuarioNoValidar(strUsuario))
                {
                    GE_TUSUARIOS user = new GE_TUSUARIOS();
                    user.USUA_USERNAME = strUsuario;
                    GE_TUSUARIOS us = Iusuarios.ObtenerUsuario(user);
                    List<string> grupos = new List<string>();
                    CtrCParametros param = new CtrCParametros();
                    
                    grupos.Add("GIN-DESARROLLO TI");

                    if (us == null)
                    {
                        GE_TUSUARIOS usuario = new GE_TUSUARIOS();
                        usuario.USUA_USERNAME = strUsuario;
                        usuario.USUA_CLAVE = "MEDESKI";
                        usuario.USUA_DOMINIO = "FANALCASA";
                        usuario.USUA_ESTADO = 1;
                        Iusuariosxrol.InsertarUsuarioXrol(grupos, usuario);
                        blRetorno = true;
                    }
                    else
                    {
                        GE_TUSUARIOS usuario = new GE_TUSUARIOS();
                        usuario.USUA_USERNAME = strUsuario;
                        usuario.USUA_CLAVE = Encrypt(strContrasena);
                        usuario.USUA_DOMINIO = "FANALCASA";
                        usuario.USUA_ESTADO = 1;
                        GE_TUSUARIOS gUsuario = Iusuarios.ObtenerUsuarioConContrasena(usuario);

                        if (gUsuario != null)
                            blRetorno = true;
                    }
                }
            }
            catch
            {
                throw;
            }

            return blRetorno;
        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public DataTable ExceltoDataTable(int pHojaIndex, String pRutaArchivo)
        {
            DataTable dtSalida = new DataTable();
            try
            {

                dtSalida = Iutilidades.ExceltoDataTable(pHojaIndex, pRutaArchivo);
            }
            catch
            {
                throw;
            }

            return dtSalida;
        }

        public IList<DTOCuadroServicio> GenerarCuadroServicio()
        {
            try
            {
                return Iutilidades.GenerarCuadroServicio();
            }
            catch
            {
                throw;
            }
        }

        public DataTable CalcularValorItemServidor()
        {
            try
            {
                return Iutilidades.CalcularValorItemServidor();
            }
            catch
            {
                throw;
            }
        }
    }
}