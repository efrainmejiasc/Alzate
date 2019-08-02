using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrPorcentajesPYG : ApiController
    {
        private IPorcentajesPYG CRUD = new CPorcentajesPYG();
        private IHistoricoPYG CRUDHISTO = new CHistoricoPYG();
        private ICentroOperacion CRUDCEOP = new CCentroOperacion();

        public IList<GE_TPORCENTAJESPYG> Recalcular(string usuario)
        {
            try
            {
                IList<GE_TPORCENTAJESPYG> lista = new List<GE_TPORCENTAJESPYG>();
                IList<GE_THISTORICOPYG> listaHist = CRUDHISTO.GetAllActive();
                IList<GE_TCENTROSOPERACION> listaCeOp = CRUDCEOP.GetAll();

                decimal suma = Convert.ToDecimal(listaHist.Where(x => x.vent_tipo == "PPTO").Sum(x => x.vent_valor_ventas));
                decimal fore_suma = Convert.ToDecimal(listaHist.Where(x => x.vent_tipo == "FORE").Sum(x => x.vent_valor_ventas));

                foreach (var item in listaHist)
                {
                    GE_TCENTROSOPERACION itemCeOp = listaCeOp.Any(x => x.ceop_consecutivo == item.vent_ceop) == true ? 
                        listaCeOp.Where(x => x.ceop_consecutivo == item.vent_ceop).First() : null;
                    
                    item.GE_TCENTROSOPERACION = itemCeOp;

                    GE_TPORCENTAJESPYG itemPorc = lista.Where(x => x.GE_THISTORICOPYG.GE_TCENTROSOPERACION == itemCeOp).FirstOrDefault();
                    if (itemPorc == null)
                    {
                        itemPorc = new GE_TPORCENTAJESPYG();
                        itemPorc.GE_THISTORICOPYG = item;


                        itemPorc.hipo_historico_id = item.vent_consecutivo;
                        itemPorc.hipo_gastos_totales = Convert.ToDecimal(item.vent_valor_directos + item.vent_valor_indirectos);

                        // PPTO
                        try
                        {
                            itemPorc.hipo_porc_ventas = item.vent_valor_ventas / suma;
                        }
                        catch (Exception ex)
                        {
                            itemPorc.hipo_porc_ventas = 0;
                        }

                        try
                        {
                            itemPorc.hipo_porc_directos = item.vent_valor_directos / item.vent_valor_ventas;
                        }
                        catch (Exception ex)
                        {
                            itemPorc.hipo_porc_directos = 0;
                        }

                        try
                        {
                            itemPorc.hipo_porc_indirectos = item.vent_valor_indirectos / item.vent_valor_ventas;
                        }
                        catch (Exception ex)
                        {
                            itemPorc.hipo_porc_indirectos = 0;
                        }

                        itemPorc.hipo_porc_total = itemPorc.hipo_porc_directos + itemPorc.hipo_porc_indirectos;
                        itemPorc.hipo_usuario = usuario;
                        itemPorc.hipo_fecha = DateTime.Now;
                        itemPorc.hipo_usuario_act = usuario;
                        itemPorc.hipo_fecha_act = DateTime.Now;
                        itemPorc.hipo_activo = 1;
                        lista.Add(itemPorc);
                    }
                    else
                    {
                        lista.Remove(itemPorc);

                        itemPorc.hipo_gastos_fore_totales = Convert.ToDecimal(item.vent_valor_directos + item.vent_valor_indirectos);
                        // FORECAST
                        try
                        {
                            itemPorc.hipo_porc_fore_ventas = item.vent_valor_ventas / fore_suma;
                        }
                        catch (Exception ex)
                        {
                            itemPorc.hipo_porc_fore_ventas = 0;
                        }

                        try
                        {
                            itemPorc.hipo_porc_fore_directos = item.vent_valor_directos / item.vent_valor_ventas;
                        }
                        catch (Exception ex)
                        {
                            itemPorc.hipo_porc_fore_directos = 0;
                        }

                        try
                        {
                            itemPorc.hipo_porc_fore_indirectos = item.vent_valor_indirectos / item.vent_valor_ventas;
                        }
                        catch (Exception ex)
                        {
                            itemPorc.hipo_porc_fore_indirectos = 0;
                        }

                        itemPorc.hipo_porc_fore_total = itemPorc.hipo_porc_fore_directos + itemPorc.hipo_porc_fore_indirectos;
                        
                        lista.Add(itemPorc);
                    }
                    
                    
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        
        public IList<GE_TPORCENTAJESPYG> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IList<GE_TPORCENTAJESPYG> GetAllActive()
        {
            try
            {
                return CRUD.GetAllActive();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IHttpActionResult Add(GE_TPORCENTAJESPYG objeto)
        {
            try
            {
                CRUD.Add(objeto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IHttpActionResult Update(GE_TPORCENTAJESPYG objeto)
        {
            try
            {
                CRUD.Update(objeto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}