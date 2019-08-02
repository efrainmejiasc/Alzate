using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CUsuariosxRol : Interfase.IUsuariosxRol
    {
        private readonly IUsuariosxRol CRUD;
        private readonly IParametros CRUDPARAMS;

        public CUsuariosxRol()
        {
            CRUD = new UsuariosxRol();
            CRUDPARAMS = new Parametros();
        }

        public IList<GE_TUSUARIOSXROL> GetUsuariosXRol(GE_TUSUARIOS user)
        {
            try
            {
                IList<GE_TUSUARIOSXROL> rolUserRetorno = CRUD.GetList(i => i.usua_usuario == user.USUA_USUARIO);
                return rolUserRetorno;
            }
            catch
            {
                throw;
            }
        }

        public void DeleteRolXUsuario(GE_TUSUARIOSXROL usuarioXRol)
        {
            try
            {
                var userRol = CRUD.GetSingle(i => i.usxr_consecutivo == usuarioXRol.usxr_consecutivo);
                CRUD.Remove(userRol);
            }
            catch
            {
                throw;
            }
        }

        public int InsertarUsuarioXrol(List<string> grupos, GE_TUSUARIOS usuario)
        {
            try
            {
                CRoles roles = new CRoles();
                CDominios obnerGruposDALP = new CDominios();
                CVlrsparamgrales obtenerParametroDALP = new CVlrsparamgrales();
                CUsuarios user = new CUsuarios();

                int retorno = -2;
                IList<GE_TROLES> rolList = roles.GetRoles();
                GE_TUSUARIOS insertaUser = user.GetUsuarios().Where(x => x.USUA_USERNAME.ToUpper().Equals(usuario.USUA_USERNAME.ToUpper())).FirstOrDefault();
                VLRSPRMGRALES validarParametro = obtenerParametroDALP.GetByClase("GRUPLDAP");

                if (validarParametro.vhpg_valor == "1")
                {
                    int resultadoCargueGrupos = obnerGruposDALP.InsertAll();
                }

                foreach (var rol in rolList)
                {
                    foreach (string nombreGrupo in grupos)
                    {
                        GE_TPARAMETROS objParam = CRUDPARAMS.GetAll().Where(x => x.parm_descripcion.ToUpper().Equals(nombreGrupo.ToUpper())).FirstOrDefault();

                        if (objParam != null)
                        {
                            if (rol.rolm_grupo == objParam.parm_consecutivo)
                            {

                                if (insertaUser == null)
                                {
                                    usuario.USUA_CLAVE = Encrypt(usuario.USUA_CLAVE);
                                    user.Add(usuario);
                                }
                                else
                                {
                                    insertaUser.USUA_CLAVE = Encrypt(usuario.USUA_CLAVE);
                                    user.Update(insertaUser);
                                }
                                
                                GE_TUSUARIOSXROL usuarioXrol = new GE_TUSUARIOSXROL();
                                usuarioXrol.rolm_consecutivo = rol.rolm_consecutivo;
                                usuarioXrol.usua_usuario = usuario.USUA_USUARIO;
                                usuarioXrol.usxr_fecha = DateTime.Today;
                                usuarioXrol.usxr_fecha_act = DateTime.Today;
                                usuarioXrol.usxr_estado = 1;

                                Add(usuarioXrol);
                                retorno = 1;
                            }
                        }
                    }


                }

                return retorno;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private string Encrypt(string clearText)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TUSUARIOSXROL[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }
    }
}
