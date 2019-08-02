using System.Collections.Generic;
using Medeski.DataAcces.Class;
using System;
using Medeski.DataAcces;
using System.Collections;

namespace Medeski.BusinessLogic.Class
{
    public class CVwVlrCuadroServicioTotal : Interfase.IVwVlrCuadroServicioTotal
    {
        private readonly IVwCuadroServicioTotal CRUD;

        public CVwVlrCuadroServicioTotal()
        {
            CRUD = new VwCuadroServicioTotal();
        }

        public IList<VW_VLR_CUADRO_SERVICIO_TOTAL> GetAll()
        {
            try
            {
                IList<VW_VLR_CUADRO_SERVICIO_TOTAL> vw = CRUD.GetAll();
                return vw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public VW_VLR_CUADRO_SERVICIO_TOTAL GetByProducto(string producto)
        {
            try
            {
                VW_VLR_CUADRO_SERVICIO_TOTAL vw = CRUD.GetSingle(x => x.producto == producto);
                return vw;
            }
            catch
            {
                throw;
            }
        }

        private void sp_ActualizarCuadroBPC()
        {
            try
            {
                using(var context = new Entities())
                {
                    context.sp_ActualizarCuadroBPC();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public void UpdateCuadros()
        {

            try
            {
                using(var context = new Entities())
                {
                    context.sp_ActualizarCuadroServicio();
                    context.sp_ActualizarCuadroServicioDetalle();

                    // context.sp_ActualizarCuadroBPC();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
