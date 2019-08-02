using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CUsuarios : Interfase.IUsuarios
    {
        private readonly IUsuarios CRUD;

        public CUsuarios()
        {
            CRUD = new Usuarios();
        }

        public IList<GE_TUSUARIOS> GetUsuarios()
        {
            try
            {
                IList<GE_TUSUARIOS> users = CRUD.GetAll();
                return users;
            }
            catch
            {
                throw;
            }
        }


        public GE_TUSUARIOS LoginUsuario(GE_TUSUARIOS usuario)
        {
            try
            {
                string dominio = (String.IsNullOrEmpty(usuario.USUA_DOMINIO)) ? "NO" : usuario.USUA_DOMINIO;

                GE_TUSUARIOS user = CRUD.GetSingle(i => i.USUA_USERNAME == usuario.USUA_USERNAME
                                             && i.USUA_CLAVE == usuario.USUA_CLAVE
                                             && i.USUA_DOMINIO == dominio
                                             && i.USUA_ESTADO == 1);
                return user;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Metodo que consulta el usuario en la base de datos, a partir del usuario y dominio
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns>usuario</returns>
        public GE_TUSUARIOS LoginUsuarioBD(GE_TUSUARIOS usuario)
        {
            try
            {
                string dominio = (String.IsNullOrEmpty(usuario.USUA_DOMINIO)) ? "NO" : usuario.USUA_DOMINIO;

                GE_TUSUARIOS user = CRUD.GetSingle(i => i.USUA_USERNAME == usuario.USUA_USERNAME
                                             && i.USUA_DOMINIO == dominio
                                             && i.USUA_ESTADO == 1
                                              );
                return user;
            }
            catch
            {
                throw;
            }
        }

        public GE_TUSUARIOS GetUsuarioID(GE_TUSUARIOS usuario)
        {
            try
            {
                GE_TUSUARIOS user = CRUD.GetSingle(i => i.USUA_USERNAME == usuario.USUA_USERNAME
                                        && i.USUA_DOMINIO == usuario.USUA_DOMINIO
                                               );

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public GE_TUSUARIOS GetUsuario(GE_TUSUARIOS usuario)
        {
            try
            {
                GE_TUSUARIOS user = CRUD.GetSingle(i => i.USUA_USUARIO == usuario.USUA_USUARIO);

                return user;
            }
            catch
            {
                throw;
            }
        }

        public GE_TUSUARIOS ObtenerUsuario(GE_TUSUARIOS usuario)
        {
            try
            {
                GE_TUSUARIOS user = CRUD.GetSingle(u => u.USUA_USERNAME == usuario.USUA_USERNAME && u.USUA_ESTADO == 1);

                return user;
            }
            catch
            {
                throw;
            }

        }

        public GE_TUSUARIOS ObtenerUsuarioConContrasena(GE_TUSUARIOS usuario)
        {
            try
            {
                GE_TUSUARIOS user = CRUD.GetSingle(u => u.USUA_USERNAME == usuario.USUA_USERNAME
                                               && u.USUA_DOMINIO == usuario.USUA_DOMINIO
                                               && u.USUA_CLAVE == usuario.USUA_CLAVE
                                  );

                return user;
            }
            catch
            {
                throw;
            }
        }

        public int GetNextSequenceValue(string nombreSequencia)
        {
            try
            {
                return CRUD.GetNextSequenceValue(nombreSequencia);
            }
            catch
            {
                throw;
            }
        }

        public GE_TUSUARIOS ObtenerUsuarioXID(int codigoUsuario)
        {
            try
            {
                GE_TUSUARIOS usuario = CRUD.GetSingle(u => u.USUA_USUARIO == codigoUsuario);

                return usuario;
            }
            catch
            {
                throw;
            }

        }

        public void Add(params GE_TUSUARIOS[] objeto)
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

        public void Update(params GE_TUSUARIOS[] objeto)
        {
            try
            {
                CRUD.Update(objeto);
            }
            catch
            {
                throw;
            }
        }

        public void Remove(params GE_TUSUARIOS[] objeto)
        {
            try
            {
                CRUD.Remove(objeto);
            }
            catch
            {
                throw;
            }
        }
    }
}
