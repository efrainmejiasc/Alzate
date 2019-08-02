using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MedeskiView.Controllers
{
    public class CtrCargueDistribucion : ApiController
    {
        ICargueDistribucion cargue = new CCargueDistribucion();
        ICentroOperacion Cceop = new CCentroOperacion();

        public IEnumerable<DTOgenericoCargueArchivos> LeerExcel(string hoja, string archivo)
        {
            try
            {
                IList<DTOgenericoCargueArchivos> lstArchivos = cargue.informacionExcelPoi(hoja, archivo);
                return lstArchivos;
            }
            catch
            {
                throw;
            }
        }

        public void Guardar(IList<DTOgenericoCargueArchivos> p_lstCargue, string usuario)
        {

            try
            {
                IList<GE_TCARGUEDISTRIBUCION> listaCargue = new List<GE_TCARGUEDISTRIBUCION>();

                foreach (var item in p_lstCargue)
                {
                    GE_TCARGUEDISTRIBUCION registro = new GE_TCARGUEDISTRIBUCION();
                    registro.cadi_activo = 1;
                    registro.cadi_co_origen = item.dto_generic_id_consecutivo;
                    registro.cadi_co_destino = Convert.ToInt32(item.dto_generic_codigo);
                    registro.cadi_porcentaje = item.dto_generic_valor;
                    registro.cadi_fecha = DateTime.Now;
                    registro.cadi_usuario = usuario; 
                    registro.cadi_fecha_act = DateTime.Now;
                    registro.cadi_usuario_act = usuario;

                    listaCargue.Add(registro);
                }
                cargue.Guardar(listaCargue);
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public IList<DTOgenericoCargueArchivos> GetAllActive()
        {
            try
            {
                return cargue.GetAllActive();
            }
            catch
            {
                throw;
            }
        }


        public IList<DTOgenericoCargueArchivos> OrganizarGrid(IList<DTOgenericoCargueArchivos> cargue)
        {
            IDictionary<string, DTOgenericoCargueArchivos> dictionary = new Dictionary<string, DTOgenericoCargueArchivos>();
            IList<DTOgenericoCargueArchivos> retorno = new List<DTOgenericoCargueArchivos>();
            IList<GE_TCENTROSOPERACION> lstCeOp = new List<GE_TCENTROSOPERACION>();
            lstCeOp = Cceop.GetAll();
            
            try
            {
                foreach (DTOgenericoCargueArchivos item in cargue)
                {
                    if (String.IsNullOrEmpty(item.dto_generic_descripcion_a))
                    {
                        continue;
                    }
                    
                    DTOgenericoCargueArchivos objCargue = new DTOgenericoCargueArchivos();
                    objCargue = item;

                    IEnumerable<GE_TCENTROSOPERACION> centroOrigen = lstCeOp.Where(x => x.ceop_codigo == item.dto_generic_descripcion_a);
                    if (centroOrigen.Count() != 0)
                    {
                        if (centroOrigen.First().ceop_activo != 1)
                        {                            
                            objCargue.dto_generic_observaciones += "Error! "+Environment.NewLine+" Centro de Operaciones Origen Inactivo";
                        }
                        else
                        {
                            objCargue.dto_generic_id_consecutivo = centroOrigen.First().ceop_consecutivo;
                            objCargue.dto_generic_observaciones = "OK";
                        }
                    }
                    else
                    {
                        objCargue.dto_generic_observaciones = "Error! " + Environment.NewLine + " Centro de Operaciones Origen NO Existe";
                    }

                    IEnumerable<GE_TCENTROSOPERACION> centroDestino = lstCeOp.Where(x => x.ceop_codigo == item.dto_generic_descripcion_b);
                    if (centroDestino.Count() != 0)
                    {
                        if (centroDestino.First().ceop_activo != 1)
                        {
                            objCargue.dto_generic_observaciones += "Error! " + Environment.NewLine + " Centro de Operaciones Destino Inactivo";
                        }
                        else
                        {
                            objCargue.dto_generic_codigo = centroDestino.First().ceop_consecutivo.ToString();
                            if(objCargue.dto_generic_observaciones.Equals("OK"))
                                objCargue.dto_generic_observaciones = "OK";
                            else
                                objCargue.dto_generic_observaciones += " OK";
                        }
                    }
                    else
                    {
                        objCargue.dto_generic_observaciones = "Error! " + Environment.NewLine + " Centro de Operaciones Destino NO Existe";
                    }


                    if (dictionary.ContainsKey(objCargue.dto_generic_descripcion_a.ToUpper()))
                    {
                        var objDic = dictionary[objCargue.dto_generic_descripcion_a.ToUpper()];
                        objDic.dto_generic_valor_suma = Convert.ToDecimal(objDic.dto_generic_valor_suma) + Convert.ToDecimal(objCargue.dto_generic_valor);
                        dictionary[objCargue.dto_generic_descripcion_a.ToUpper()] = objDic;
                    }
                    else
                    {
                        objCargue.dto_generic_valor_suma = objCargue.dto_generic_valor;
                        dictionary.Add(objCargue.dto_generic_descripcion_a.ToUpper(), objCargue);
                    }

                    retorno.Add(objCargue);
                }

                foreach (var itemDic in dictionary)
                {
                    try
                    {
                        if(Convert.ToDouble(itemDic.Value.dto_generic_valor_suma * 100) != Convert.ToDouble(100))
                        {
                            List<DTOgenericoCargueArchivos> listObj = retorno.Where(x => x.dto_generic_descripcion_a == itemDic.Value.dto_generic_descripcion_a).ToList<DTOgenericoCargueArchivos>();
                            foreach (var x in listObj)
                            {
                                DTOgenericoCargueArchivos objDrivers = x;
                                objDrivers.dto_generic_observaciones += Environment.NewLine + " La suma de estos valores (" + (itemDic.Value.dto_generic_valor_suma.ToString()) + "%) es diferente de 100%.";
                                retorno.Remove(x);
                                retorno.Add(objDrivers);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                }

                return retorno;
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}