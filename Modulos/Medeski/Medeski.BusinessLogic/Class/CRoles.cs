using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CRoles : Interfase.IRoles
    {
        private readonly IRoles CRUD;

        public CRoles()
        {
            CRUD = new Roles();   
        }
        
        public IList<GE_TROLES> GetRoles()
        {
            try
            {
                using (var context = new Entities())
                {
                    var query = from roles in context.GE_TROLES
                                join param in context.GE_TPARAMETROS on roles.rolm_grupo equals param.parm_consecutivo
                                select new
                                {
                                    roles.rolm_consecutivo,
                                    param.parm_descripcion,
                                    roles.rolm_grupo,
                                    roles.rolm_nombre,
                                    roles.rolm_descripcion,
                                    roles.rolm_estado
                                };
                    
                    IList<GE_TROLES> lista = new List<GE_TROLES>();

                    foreach (var item in query)
                    {
                        GE_TPARAMETROS param = new GE_TPARAMETROS();
                        param.parm_descripcion = item.parm_descripcion;

                        GE_TROLES rol = new GE_TROLES();
                        rol.GE_TPARAMETROS = param;
                        rol.rolm_descripcion = item.rolm_descripcion;
                        rol.rolm_consecutivo = item.rolm_consecutivo;
                        rol.rolm_nombre = item.rolm_nombre;
                        rol.rolm_estado = item.rolm_estado;
                        rol.rolm_grupo = item.rolm_grupo;
                        if (item.rolm_estado == 1)
                            rol.rolm_estadoStr = "Activo";
                        else
                            rol.rolm_estadoStr = "Inactivo";

                        lista.Add(rol);
                    }

                    return lista;
                };

            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TROLES> GetRolesActivos()
        {
            try
            {
                return CRUD.GetList(x => x.rolm_estado == 1);
            }
            catch
            {
                throw;
            }
        }

        public void Add(GE_TROLES[] objeto)
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

        public void Update(GE_TROLES[] objeto)
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
    }
}
