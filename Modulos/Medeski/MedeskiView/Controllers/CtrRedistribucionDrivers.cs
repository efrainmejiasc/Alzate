using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrRedistribucionDrivers : ApiController
    {
        public IList<DTOgenericoCargueArchivos> GetAllActive()
        {
            try
            {
                return new CRedistribucionDrivers().GetAllActive();
            }
            catch
            {

                throw;
            }
        }

        public IList<DTOgenericoCargueArchivos> GetAllRedistribuidos()
        {
            try
            {
                return new CRedistribucionDrivers().GetAllRedistribuidos();
            }
            catch
            {

                throw;
            }
        }
        

        public void Guardar(IList<DTOgenericoCargueArchivos> listaGrid, string usuario)
        {
            try
            {
                var Ok = new List<string>();

                IList<DTOgenericoCargueArchivos> lstRedis = new CCargueDistribucion().GetAllActive();
                IList<GE_TREDISTRIBUCION_DRIVERS> lstGuardar = new List<GE_TREDISTRIBUCION_DRIVERS>();
                IList<GE_TCENTROSOPERACION> lstCeOp = new CCentroOperacion().GetAll();

                foreach(var item in listaGrid)
                {
                    string ceop = item.dto_generic_centro_operacion;
                    bool redis = lstRedis.Any(x => x.dto_generic_descripcion_a == ceop) ? true : false;
                    if (redis && !Ok.Contains(ceop) && item.dto_generic_id_parametro == 529)
                    {
                        Ok.Add(ceop);

                        int ceop_id = lstCeOp.Where(x => x.ceop_codigo == ceop).Select(x => x.ceop_consecutivo).First();

                        decimal suma = Convert.ToDecimal(listaGrid.Where(x => x.dto_generic_centro_operacion == ceop).Sum(x => x.dto_generic_valor_total));
                        var lstDestinos = lstRedis.Where(x => x.dto_generic_descripcion_a == ceop).Select(x => x.dto_generic_descripcion_b).ToArray();
                        foreach(var destino in lstDestinos)
                        {
                            decimal porc = Convert.ToDecimal(lstRedis.Where(x => x.dto_generic_descripcion_a == ceop && x.dto_generic_descripcion_b == destino).Select(x => x.dto_generic_valor).First());
                            IEnumerable<DTOgenericoCargueArchivos> buscar = listaGrid.Where(x => x.dto_generic_centro_operacion == destino && x.dto_generic_id_parametro == 524);
                            
                            int count = buscar.Count();
                            if(count == 0)
                            {
                                continue;
                            }
                                

                            decimal subporc = porc / count;

                            foreach (var dest in buscar)
                            {
                                decimal redi = Convert.ToDecimal(item.dto_generic_valor_total * subporc);

                                GE_TREDISTRIBUCION_DRIVERS objRedis = new GE_TREDISTRIBUCION_DRIVERS();
                                objRedis.care_ceop_id = ceop_id;
                                objRedis.care_cargue_driver = dest.dto_generic_id_consecutivo;
                                objRedis.care_valor = redi;
                                objRedis.care_activo = "1";
                                objRedis.care_usuario = usuario;
                                objRedis.care_fecha = DateTime.Now;
                                objRedis.care_usuario_act = usuario;
                                objRedis.care_fecha_act = DateTime.Now;

                                lstGuardar.Add(objRedis);
                            }    
                        }
                        
                    }
                }

                new CRedistribucionDrivers().guardar(lstGuardar);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}