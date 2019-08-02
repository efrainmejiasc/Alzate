using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class COpcionesMenu : Interfase.IOpcionesMenu 
    {
        private readonly IOpcionesMenu CRUD;

        public COpcionesMenu()
        {
            CRUD = new OpcionesMenu();   
        }
        
        public IList<GE_TOPCIONESMENU> GetOpcionesMenu(int id)
        {
            try
            {
                IList<GE_TOPCIONESMENU> opc = CRUD.GetList(i => i.opcm_idpadre == id && i.opcm_estado == 1);
                return opc;
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TOPCIONESMENU> GetOpcionesMenuxUser(string strUser)
        {
            try
            {
                using (var contEnt = new Entities())
                {
                    var query = (from opc in contEnt.GE_TOPCIONESMENU
                                 join opcrol in contEnt.GE_TOPCIONESMENUXROL on opc.opcm_consecutivo equals opcrol.opcm_consecutivo
                                 join usrol in contEnt.GE_TUSUARIOSXROL on opcrol.rolm_consecutivo equals usrol.rolm_consecutivo
                                 join us in contEnt.GE_TUSUARIOS on usrol.usua_usuario equals us.USUA_USUARIO
                                 where us.USUA_USERNAME == strUser && opc.opcm_estado == 1 && usrol.usxr_estado == 1
                                 select opc).Distinct().ToList();
                    return query;
                }
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TOPCIONESMENU> GetAllOpcionesMenu()
        {
            try
            {
                return CRUD.GetList(x => x.opcm_estado == 1);                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
