using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class COpcionesMenuRol : Interfase.IOpcionesMenuRol
    {
        private readonly IOpcionesMenuxRol CRUD;

        public COpcionesMenuRol()
        {
            CRUD = new OpcionesMenuxRol();
        }


        public IList<GE_TOPCIONESMENUXROL> GetOpciones(int idRol)
        {
            try
            {
                return CRUD.GetList(i => i.rolm_consecutivo == idRol);
            }
            catch
            {
                throw;
            }
        }


        public void deleteOpcionesUsuario(int rol)
        {
            try
            {
                CRUD.DeleteWhere(x => x.rolm_consecutivo == rol);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void Add(GE_TOPCIONESMENUXROL[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
