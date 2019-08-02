using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Data;
using System.Collections;
using System.Security.Cryptography;
using System.IO;

namespace MedeskiView.Controllers
{
    public class CtrUsuarios : ApiController
    {
        IUsuarios users = new CUsuarios();
        IVlrsPrmgrales IVlrsPrmgrales = new CVlrsparamgrales();
        IUsuariosxRol IUsuariosxRol = new CUsuariosxRol();

        public string LoginUsuarioDominio2()
        {

            try
            {
                int estado = IVlrsPrmgrales.GetByClase("PATHLDAP").vhpg_estado;

                if (estado == 1)
                {
                    return "true";
                }
                else
                {

                    return "false";
                }
            }
            catch
            {
                throw;

            }

            //  return false;
        }


        public bool LoginUsuarioDominio()
        {

            try
            {

                int estado = IVlrsPrmgrales.GetByClase("PATHLDAP").vhpg_estado;

                if (estado == 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception)
            {

            }

            return false;
        }

        public IList<GE_TUSUARIOS> GetUsuarios()
        {
            return users.GetUsuarios();
        }

        public GE_TUSUARIOS LoginUsuario(string usuarioName, string clave)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USERNAME = usuarioName;
            user.USUA_CLAVE = clave;

            return users.LoginUsuario(user);
        }

        public int GetUsuarioID(string usuarioName, string dominio)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USERNAME = usuarioName;
            user.USUA_DOMINIO = dominio;
            try
            {

                return users.GetUsuarioID(user).USUA_USUARIO;
            }
            catch
            {
                return 0;
            }

        }

        public GE_TUSUARIOS GetUsuario(int usuario)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USUARIO = usuario;
            return users.GetUsuario(user);
        }

        public GE_TUSUARIOS ObtenerUsuario(string usuario)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USERNAME = usuario;


            return users.ObtenerUsuario(user);

        }

        public GE_TUSUARIOS ObtenerUsuario(string usuario, string contrasena, string dominio)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USERNAME = usuario;
            user.USUA_CLAVE = Encrypt(contrasena);
            user.USUA_DOMINIO = dominio;

            return users.ObtenerUsuario(user);
        }

        public GE_TUSUARIOS ObtenerUsuario(string usuario, string dominio)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USERNAME = usuario;
            user.USUA_DOMINIO = dominio;

            return users.ObtenerUsuario(user);
        }


        public void Insert(GE_TUSUARIOS user)
        {
            //if(user.USUA_DOMINIO=="" || user.USUA_DOMINIO ==null ){
            //    user.USUA_DOMINIO = "NO";
            //}

            user.USUA_CLAVE = Encrypt(user.USUA_CLAVE);
            user.USUA_FECHA = DateTime.Today;
            user.USUA_FECHA_ACT = DateTime.Today;
            // user.USUA_ESTADO = 1;
            users.Add(user);
        }

        public void Update(GE_TUSUARIOS user)
        {

            if (user.USUA_DOMINIO == "" || user.USUA_DOMINIO == null)
            {
                user.USUA_DOMINIO = "NO";
            }
            user.USUA_CLAVE = Encrypt(user.USUA_CLAVE);
            // user.USUA_FECHA = DateTime.Today;
            user.USUA_FECHA_ACT = DateTime.Today;
            user.USUA_ESTADO = 1;

            users.Update(user);
        }

        public void UpdateEstado(GE_TUSUARIOS user)
        {
            user.USUA_FECHA_ACT = DateTime.Today;
            users.Update(user);
        }

        public void Delete(int usuario)
        {
            GE_TUSUARIOS user = new GE_TUSUARIOS();
            user.USUA_USUARIO = usuario;
            users.Remove(user);

        }

        public string AutenticarUsuarioActiveDirectory(string dominio, string usuario, string password)
        {
            try
            {
                GE_TUSUARIOS usua = new GE_TUSUARIOS();
                usua.USUA_ESTADO = 1;
                usua.USUA_USERNAME = usuario;
                usua.USUA_CLAVE = password;
                usua.USUA_DOMINIO = dominio;
                usua.USUA_USUARIO = GetUsuarioID(usua.USUA_USERNAME, usua.USUA_DOMINIO);
                return AutenticarUsuarioDA(usua);
            }
            catch (Exception)
            {
                return "Tiempo agotado en la conexión";

            }

        }

        public string AutenticarUsuarioDA(GE_TUSUARIOS user)
        {
            string respuesta = "";

            try
            {
                //Armamos la cadena completa de dominio y usuario
                string domainAndUsername = user.USUA_DOMINIO + @"\" + user.USUA_USERNAME;
                string path = IVlrsPrmgrales.GetByClase("PATHLDAP").vhpg_valor;

                DirectoryEntry entry = new DirectoryEntry(path, domainAndUsername, user.USUA_CLAVE);


                DirectorySearcher search = new DirectorySearcher(entry);

                //search.Filter = "(&(objectClass=user)(l=" + user.USUA_USERNAME + "))";
                SearchResultCollection resultado = search.FindAll();

                //if (search.PropertiesToLoad.Contains())
                //{
                //    return 0;
                //}
                //else
                //{
                respuesta = "Existe";
                //}



            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error de inicio de sesión"))
                {

                    respuesta = "Error de inicio de sesión";
                }
                else if (ex.Message.Contains("El nombre de usuario o la contraseña no son correctos"))
                {
                    respuesta = "El nombre de usuario o la contraseña no son correctos";
                }
                else
                {
                    respuesta = "El nombre de usuario o la contraseña no son correctos, tiempo agotado para la conexión";
                }


            }

            return respuesta;
        }

        public bool AutenticarUsuarioBd(string usuario, string dominio)
        {
            try
            {
                GE_TUSUARIOS user = new GE_TUSUARIOS();
                user.USUA_ESTADO = 1;
                user.USUA_USERNAME = usuario;
                user.USUA_DOMINIO = dominio;

                return (users.LoginUsuarioBD(user).USUA_USERNAME).Length > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;

            }

        }


        public bool AutenticarUsuarioBd(string usuario, string password, string dominio)
        {
            try
            {
                GE_TUSUARIOS user = new GE_TUSUARIOS();
                user.USUA_ESTADO = 1;
                user.USUA_USERNAME = usuario;
                user.USUA_CLAVE = Encrypt(password);
                user.USUA_DOMINIO = dominio;

                return (users.LoginUsuario(user).USUA_USERNAME).Length > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;

            }

        }

        public IList<GE_TUSUARIOSXROL> ObtenerRolXUsuariosBD(GE_TUSUARIOS user)
        {
            CtrUsuariosxRol CtrUsuariosxRol = new CtrUsuariosxRol();
            return CtrUsuariosxRol.GetUsuariosXRol(user);

        }

        public void InhabilitarUsuario(GE_TUSUARIOS user)
        {
            user.USUA_ESTADO = 0;

            UpdateEstado(user);
        }

        public void HabilitarUsuario(GE_TUSUARIOS user)
        {
            user.USUA_ESTADO = 1;
            UpdateEstado(user);
        }

        public bool DeleteRolXusuario(GE_TUSUARIOSXROL rolAsignados)
        {
            try
            {
                IUsuariosxRol.DeleteRolXUsuario(rolAsignados);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<string> GruposDirectorioActivoXUsuario(string path, string domainAndUsername, string usuarioName, string pass)
        {
            List<string> result = new List<string>();

            using (DirectoryEntry adsRoot = new DirectoryEntry(path, domainAndUsername, pass))
            {
                using (DirectorySearcher adsSearch = new DirectorySearcher(adsRoot))
                {


                    String strUsr = null;
                    strUsr = usuarioName;

                    // Ponemos como filtro que busque el usuario actual
                    adsSearch.Filter = "samAccountName=" + strUsr; //+ Environment.GetEnvironmentVariable("USERNAME");

                    // Extraemos la primera coincidencia
                    SearchResult oResult;
                    oResult = adsSearch.FindOne();
                    // Creamos una variable StringBuilder donde ir añadiendo los SID para crear un filtro de búsqueda
                    StringBuilder sids = new StringBuilder();
                    using (DirectoryEntry usuario = oResult.GetDirectoryEntry())
                    {
                        usuario.RefreshCache(new string[] { "tokenGroups" });
                        sids.Append("(|");
                        foreach (byte[] sid in usuario.Properties["tokenGroups"])
                        {
                            sids.Append("(objectSid=");
                            for (int indice = 0; indice < sid.Length; indice++)
                            {
                                sids.AppendFormat("\\{0}", sid[indice].ToString("X2"));
                            }
                            sids.AppendFormat(")");
                        }
                        sids.Append(")");
                    }

                    // Creamos un objeto DirectorySearcher con el filtro antes generado y buscamos todas la coincidencias
                    using (DirectorySearcher ds = new DirectorySearcher(adsRoot, sids.ToString()))
                    {
                        SearchResultCollection src = ds.FindAll();
                        // Recorremos toda la lista de grupos devueltos

                        foreach (SearchResult sr in src)
                        {
                            result.Add(((String)sr.Properties["samAccountName"][0]));

                        }
                    }
                }
            }


            return result;
        }

        public string ValidarGruposRolXGruposDA(GE_TUSUARIOS usuario)
        {
            //se desencripta la clave del usuario 
            usuario.USUA_CLAVE = Decrypt(usuario.USUA_CLAVE);

            // se valida los grupos del rol contra los grupos asociados en el direcotio activo
            int resultado = ValidarGruposRolXGruposAD(usuario);//--cambiar metodo string 
            if (resultado == 1)
            {
                HabilitarUsuario(usuario);
                return "sesionOk";
            }
            else if (resultado == 3)
            {
                return "El nombre de usuario o la contraseña no son correctos";
            }
            else if (resultado == -1)
            {
                return "Tiempo agotado para la conexión";
            } 
            else if (resultado == -2)
            {
                return "El usuario no tiene al menos un rol asociado.";
            }
            else
            {
                return "Error iniciando sesión";
            }

        }

        public int ValidarGruposRolXGruposAD(GE_TUSUARIOS user)
        {
            //Armamos la cadena completa de dominio y usuario
            string domainAndUsername = user.USUA_DOMINIO + @"\" + user.USUA_USERNAME;

            //Creamos un objeto DirectoryEntry al cual le pasamos el URL, dominio/usuario y la contraseña
            string path = IVlrsPrmgrales.GetByClase("PATHLDAP").vhpg_valor;

            try
            {

                List<string> listaUsuarioGrupos = GruposDirectorioActivoXUsuario(path, domainAndUsername, user.USUA_USERNAME, user.USUA_CLAVE);

                //se valida si el usuario tiene grupos asignados, si no tiene quiere decir que no no exise
                if (listaUsuarioGrupos.Count() >= 1)
                {
                    return IUsuariosxRol.InsertarUsuarioXrol(listaUsuarioGrupos, user);
                    // return 0;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error de inicio de sesión"))
                {
                    return 0;
                }
                else if (ex.Message.Contains("El nombre de usuario o la contraseña no son correctos"))
                {
                    return 3;
                }

                return -1;
            }

        }

        public string ValidarUsuarioDA(string dominio, string usuario, string password)
        {
            string validarUsuario;
            string respues = "";
            IList<GE_TUSUARIOSXROL> rolesAsignados = null;
            //GE_TUSUARIOS usuarioBD = new GE_TUSUARIOS();

            try
            {
                //se valida si esta creado en la base de datos el Path del servidor LDAP
                if (LoginUsuarioDominio())
                {
                    //se valida si el usuario se encuentra en el DA
                    validarUsuario = AutenticarUsuarioActiveDirectory(dominio, usuario, password); //--cambiar metodo a string 

                    if (validarUsuario == "Existe")
                    {
                        // se obtiene el usuario de la base de datos
                        GE_TUSUARIOS usuarioBD = ObtenerUsuario(usuario, password, dominio);

                        //se valida si el usuario se debe validar contra el directorio, de ser asi, se cambia la contraseña de la bd
                        //por la que el usuario ingreso en el login
                        if (usuarioBD != null && usuarioBD.USUA_VALIDALDAP == "S")
                        {
                            usuarioBD.USUA_CLAVE = Encrypt(password);
                        }

                        //si el usuario no existe en la base de datos, tampoco tiene roles asignados
                        if (usuarioBD != null)
                        {
                            //se valida si el usuario tiene roles asignados en la BD
                            rolesAsignados = ObtenerRolXUsuariosBD(usuarioBD);
                        }

                        if (rolesAsignados != null)
                        {
                            if (rolesAsignados.Count() >= 1)
                            {
                                string sincronizarRol = IVlrsPrmgrales.GetByClase("SINCROL").vhpg_valor;

                                if (sincronizarRol == "S")
                                {
                                    //Se inhabilita el usuario 
                                    InhabilitarUsuario(usuarioBD);

                                    //Se eliminan los roles asignados
                                    foreach (GE_TUSUARIOSXROL rol in rolesAsignados)
                                    {
                                        DeleteRolXusuario(rol);
                                    }

                                    return ValidarGruposRolXGruposDA(usuarioBD);
                                    //int   resultado = users.ValidarGruposRolXGruposDA(usuarioBD);//--cambiar metodo string 
                                    //if (resultado == 1)
                                    //{
                                    //     HabilitarUsuario(usuarioBD);
                                    //     return respues = "sesionOk";

                                    //}
                                    //else
                                    //{
                                    //    return respues = "Usuario no existe en el directorio";
                                    //}

                                }

                                else
                                {
                                    return respues = "sesionOk";
                                }
                            }
                            else
                            {
                                if (usuarioBD == null)
                                {
                                    GE_TUSUARIOS usuarioDA = new GE_TUSUARIOS();
                                    usuarioDA.USUA_USERNAME = usuario;
                                    usuarioDA.USUA_CLAVE = Encrypt(password);
                                    usuarioDA.USUA_DOMINIO = dominio;

                                    return ValidarGruposRolXGruposDA(usuarioDA);
                                }
                                else
                                {
                                    return ValidarGruposRolXGruposDA(usuarioBD);
                                }

                            }
                        }
                        else
                        {
                            if (usuarioBD == null)
                            {
                                GE_TUSUARIOS usuarioDA = new GE_TUSUARIOS();
                                usuarioDA.USUA_USERNAME = usuario;
                                usuarioDA.USUA_CLAVE = Encrypt(password);
                                usuarioDA.USUA_DOMINIO = dominio;

                                return ValidarGruposRolXGruposDA(usuarioDA);
                            }
                            else
                            {
                                return ValidarGruposRolXGruposDA(usuarioBD);
                            }

                        }
                    }

                    return validarUsuario;
                }

                return respues = "No se encuentra creado el path del servidor, por favor valide el parametro e intente nuevamente";

            }
            catch (Exception ex)
            {
                return respues = ex.ToString();
            }


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

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
