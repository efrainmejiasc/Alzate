using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedeskiView.Controllers
{
    public class CtrDistribucionPersonas
    {
        ICargueGente ICargueGente = new CCargueGente();
        IServidores IServidores = new CServidores();
        IProductos IProductos = new CProductos();
        IPeriodoPresupuesto IPeriodoPresupuesto = new CPeriodoPresupuesto();
        IDistribucionDedicacionPersonas IDistribucionDedicacionPersonas = new CDistribucionDedicacionPersonas();

        //Cargo la gente que se cargo desde el formulario de
        //cargue de Gente.
        public IList<GE_TGENTE> cargarPersonas()
        {
            try
            {
                return ICargueGente.getAllPeriodoActivo();
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOdistribucionPersonas> organizaGridProductos(Int32 p_consecutivoPersona)
        {
            try
            {
                //Cargo el Periodo Activo
                GE_TPERIODOPRESUPUESTO objPeriodoPresupuesto = new GE_TPERIODOPRESUPUESTO();
                objPeriodoPresupuesto = IPeriodoPresupuesto.GetAllActive().Where(a => a.peri_activo == 1).FirstOrDefault();

                //Cargo las distribuciones por el periodo activo
                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstDedicacionPersonas = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();
                lstDedicacionPersonas = IDistribucionDedicacionPersonas.getAllDedicacionPersona(p_consecutivoPersona, objPeriodoPresupuesto.peri_consecutivo);

                //Cargo los Productos Activos a la fecha
                IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
                lstProductos = IProductos.GetAllActive().Where(a => a.prod_distrib_serv == 0).ToList();

                //Lista a Retornar
                IList<DTOdistribucionPersonas> lstDistribuidos = new List<DTOdistribucionPersonas>();
                int consecutivo = 1;
                //Armo la lista de los que se van a mostrar
                foreach (var item in lstProductos)
                {

                    DTOdistribucionPersonas objProductos = new DTOdistribucionPersonas();
                    GE_TDISTRIBUCIONDEDICACIONPERSONA objDedicacion = lstDedicacionPersonas.Where(a => a.dper_producto == item.prod_consecutivo && a.dper_estado == 1).FirstOrDefault();

                    if (objDedicacion != null)
                    {
                        objProductos.consecutivo = consecutivo;
                        objProductos.consecutivoItem = item.prod_consecutivo;
                        objProductos.descripcion = item.prod_codigo;
                        objProductos.vlrDistribucion = objDedicacion.dper_valor * 1;
                        objProductos.tipo_distribucion = "Productos";
                    }
                    else
                    {
                        objProductos.consecutivo = consecutivo;
                        objProductos.consecutivoItem = item.prod_consecutivo;
                        objProductos.descripcion = item.prod_codigo;
                        objProductos.vlrDistribucion = 0;
                        objProductos.tipo_distribucion = "Productos";
                    }
                    lstDistribuidos.Add(objProductos);
                    consecutivo++;
                }

                return lstDistribuidos.OrderByDescending(b => b.vlrDistribucion).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<DTOdistribucionPersonas> organizaGridInfraestructura(Int32 p_consecutivoPersona)
        {
            try
            {
                //Cargo el Periodo Activo
                GE_TPERIODOPRESUPUESTO objPeriodoPresupuesto = new GE_TPERIODOPRESUPUESTO();
                objPeriodoPresupuesto = IPeriodoPresupuesto.GetAllActive().Where(a => a.peri_activo == 1).FirstOrDefault();

                //Cargo las distribuciones por el periodo activo
                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstDedicacionPersonas = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();
                lstDedicacionPersonas = IDistribucionDedicacionPersonas.getAllServidoresFindPeriodo(objPeriodoPresupuesto.peri_consecutivo, p_consecutivoPersona);

                //Cargo los Servidores Activos a la fecha
                IList<GE_TSERVIDORES> lstServidores = new List<GE_TSERVIDORES>();
                lstServidores = IServidores.GetAllActive();

                //Cargo los Productos que se realizan mantenimiento en Datacenter Activos a la fecha
                IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
                lstProductos = IProductos.GetAllActive().Where(a => a.prod_distrib_serv == 1).ToList();

                //Lista a Retornar
                IList<DTOdistribucionPersonas> lstDistribuidos = new List<DTOdistribucionPersonas>();

                int consecutivo = 1;

                //Armo la lista de los que se van a mostrar a nivel de servidores
                foreach (var item in lstServidores)
                {
                    DTOdistribucionPersonas objServidores = new DTOdistribucionPersonas();
                    GE_TDISTRIBUCIONDEDICACIONPERSONA objDedicacion = lstDedicacionPersonas.Where(a => a.dper_servidor == item.serv_consecutivo && a.dper_estado == 1).FirstOrDefault();

                    if (objDedicacion != null)
                    {
                        objServidores.consecutivo = consecutivo;
                        objServidores.consecutivoItem = item.serv_consecutivo;
                        objServidores.descripcion = item.serv_nombre;
                        objServidores.vlrDistribucion = objDedicacion.dper_valor;
                        objServidores.tipo_distribucion = "Infraestructura";
                    }
                    else
                    {
                        objServidores.consecutivo = consecutivo;
                        objServidores.consecutivoItem = item.serv_consecutivo;
                        objServidores.descripcion = item.serv_nombre;
                        objServidores.vlrDistribucion = 0;
                        objServidores.tipo_distribucion = "Infraestructura";
                    }
                    lstDistribuidos.Add(objServidores);
                    consecutivo++;
                }

                //Armo los que voy a mostrar a nivel de productos de Infraestructura.
                foreach (var item in lstProductos)
                {
                    DTOdistribucionPersonas objServidores = new DTOdistribucionPersonas();
                    GE_TDISTRIBUCIONDEDICACIONPERSONA objDedicacion = lstDedicacionPersonas.Where(a => a.dper_producto == item.prod_consecutivo && a.dper_estado == 1).FirstOrDefault();

                    if (objDedicacion != null)
                    {
                        objServidores.consecutivo = consecutivo;
                        objServidores.consecutivoItem = item.prod_consecutivo;
                        objServidores.descripcion = item.prod_codigo;
                        objServidores.vlrDistribucion = objDedicacion.dper_valor;
                        objServidores.tipo_distribucion = "Producto-Infraestructura";
                    }
                    else
                    {
                        objServidores.consecutivo = consecutivo;
                        objServidores.consecutivoItem = item.prod_consecutivo;
                        objServidores.descripcion = item.prod_codigo;
                        objServidores.vlrDistribucion = 0;
                        objServidores.tipo_distribucion = "Producto-Infraestructura";
                    }
                    lstDistribuidos.Add(objServidores);
                    consecutivo++;
                }

                return lstDistribuidos.OrderByDescending(b => b.vlrDistribucion).ToList();
            }
            catch
            {
                throw;
            }
        }

        public void organizaDistribucion(IList<DTOdistribucionPersonas> p_lstDistribucion, Int32 p_consecutivoPersona, String p_tipoDistribucion)
        {
            try
            {
                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstDistribucionPersonas = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();
                //Cargo el Periodo Activo
                GE_TPERIODOPRESUPUESTO objPeriodoPresupuesto = new GE_TPERIODOPRESUPUESTO();
                objPeriodoPresupuesto = IPeriodoPresupuesto.GetAllActive().Where(a => a.peri_activo == 1).FirstOrDefault();

                if (p_tipoDistribucion.Equals("PRODUCTOS"))
                {
                    foreach (var item in p_lstDistribucion)
                    {
                        GE_TDISTRIBUCIONDEDICACIONPERSONA objDistribucionPersona = new GE_TDISTRIBUCIONDEDICACIONPERSONA();

                        objDistribucionPersona.dper_periodo = objPeriodoPresupuesto.peri_consecutivo;
                        objDistribucionPersona.dper_tipo = item.tipo_distribucion;
                        objDistribucionPersona.dper_persona = p_consecutivoPersona;
                        objDistribucionPersona.dper_producto = item.consecutivoItem;
                        objDistribucionPersona.dper_valor = item.vlrDistribucion;
                        objDistribucionPersona.dper_estado = 1;
                        objDistribucionPersona.dper_usuario = item.usuarioSesion;
                        objDistribucionPersona.dper_fecha = DateTime.Now;

                        lstDistribucionPersonas.Add(objDistribucionPersona);
                    }
                }
                else if (p_tipoDistribucion.Equals("INFRAESTRUCTURA"))
                {
                    IList<GE_TSERVIDORES> lstServidores = new List<GE_TSERVIDORES>();
                    lstServidores = IServidores.GetAllActive();

                    IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
                    lstProductos = IProductos.GetAllActive().Where(a => a.prod_distrib_serv == 1).ToList();

                    foreach (var item in p_lstDistribucion)
                    {
                        GE_TDISTRIBUCIONDEDICACIONPERSONA objDistribucionPersona = new GE_TDISTRIBUCIONDEDICACIONPERSONA();

                        objDistribucionPersona.dper_periodo = objPeriodoPresupuesto.peri_consecutivo;
                        objDistribucionPersona.dper_tipo = item.tipo_distribucion;
                        objDistribucionPersona.dper_persona = p_consecutivoPersona;

                        if (item.tipo_distribucion.Equals("Infraestructura"))
                        {
                            objDistribucionPersona.dper_servidor = Convert.ToInt32(lstServidores.Where(b => b.serv_consecutivo == item.consecutivoItem).Select(b => b.serv_consecutivo).SingleOrDefault());
                        }
                        else
                        {
                            objDistribucionPersona.dper_producto = Convert.ToInt32(lstProductos.Where(b => b.prod_consecutivo == item.consecutivoItem).Select(b => b.prod_consecutivo).SingleOrDefault());
                        }

                        objDistribucionPersona.dper_valor = item.vlrDistribucion;
                        objDistribucionPersona.dper_estado = 1;
                        objDistribucionPersona.dper_usuario = item.usuarioSesion;
                        objDistribucionPersona.dper_fecha = DateTime.Now;

                        lstDistribucionPersonas.Add(objDistribucionPersona);
                    }

                }
                //Envío la lista de distribución lista y organizada para que se realice la inserción o la actualización
                guardarDistribucion(lstDistribucionPersonas, p_consecutivoPersona, objPeriodoPresupuesto.peri_consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public void guardarDistribucion(IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> p_lstDistribucionPersonas, Int32 p_consecutivoPersona, Int32 p_consecutivoPeriodoActivo)
        {
            try
            {
                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstInfoDedicacionColaborador = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();
                lstInfoDedicacionColaborador = IDistribucionDedicacionPersonas.getAllDedicacionPersona(p_consecutivoPersona, p_consecutivoPeriodoActivo);

                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstModificar = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();
                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstGuardar = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();

                GE_TPERIODOPRESUPUESTO periodo = new CPeriodoPresupuesto().GetPeriodoActivo();

                foreach (var item in p_lstDistribucionPersonas)
                {
                    GE_TDISTRIBUCIONDEDICACIONPERSONA objBaseDatos = new GE_TDISTRIBUCIONDEDICACIONPERSONA();
                    if (item.dper_servidor != null || item.dper_tipo.Equals("Producto-Infraestructura"))
                    {
                        if (item.dper_tipo.Equals("Producto-Infraestructura"))
                        {
                            objBaseDatos = lstInfoDedicacionColaborador.Where(a => a.dper_persona == item.dper_persona && a.dper_producto == item.dper_producto && a.dper_periodo == periodo.peri_consecutivo && a.dper_estado == 1)
                                                                       .SingleOrDefault();
                        }
                        else
                        {
                            objBaseDatos = lstInfoDedicacionColaborador.Where(a => a.dper_persona == item.dper_persona && a.dper_servidor == item.dper_servidor && a.dper_periodo == periodo.peri_consecutivo && a.dper_estado == 1)
                                                                       .SingleOrDefault();
                        }

                    }
                    else
                    {
                        objBaseDatos = lstInfoDedicacionColaborador.Where(a => a.dper_persona == item.dper_persona && a.dper_producto == item.dper_producto && a.dper_periodo == periodo.peri_consecutivo && a.dper_estado == 1)
                                                                   .SingleOrDefault();
                    }


                    if (objBaseDatos == null)
                    {
                        GE_TDISTRIBUCIONDEDICACIONPERSONA objNuevo = new GE_TDISTRIBUCIONDEDICACIONPERSONA();
                        objNuevo.dper_periodo = item.dper_periodo;
                        objNuevo.dper_tipo = item.dper_tipo;
                        objNuevo.dper_persona = item.dper_persona;
                        objNuevo.dper_servidor = item.dper_servidor == null ? null : item.dper_servidor;
                        objNuevo.dper_producto = item.dper_producto == null ? null : item.dper_producto;
                        objNuevo.dper_valor = item.dper_valor;
                        objNuevo.dper_estado = item.dper_estado;
                        objNuevo.dper_usuario = item.dper_usuario;
                        objNuevo.dper_fecha = item.dper_fecha;

                        lstGuardar.Add(objNuevo);

                    }
                    else
                    {
                        GE_TDISTRIBUCIONDEDICACIONPERSONA objModificar = new GE_TDISTRIBUCIONDEDICACIONPERSONA();
                        objModificar.dper_consecutivo = objBaseDatos.dper_consecutivo;
                        objModificar.dper_periodo = item.dper_periodo;
                        objModificar.dper_tipo = item.dper_tipo;
                        objModificar.dper_persona = item.dper_persona;
                        objModificar.dper_servidor = item.dper_servidor == null ? null : item.dper_servidor;
                        objModificar.dper_producto = item.dper_producto == null ? null : item.dper_producto;
                        objModificar.dper_valor = item.dper_valor;
                        objModificar.dper_estado = item.dper_valor == 0 ? 0 : 1;
                        objModificar.dper_usuario = item.dper_usuario;
                        objModificar.dper_fecha = item.dper_fecha;

                        lstModificar.Add(objModificar);
                    }
                }
                IDistribucionDedicacionPersonas.guardar(lstGuardar);
                IDistribucionDedicacionPersonas.actualizar(lstModificar);
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public bool validaDistribucion(String p_codProducto)
        {
            try
            {
                //Cargo los Productos Activos a la fecha
                IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
                lstProductos = IProductos.GetAllActive().Where(a => a.prod_activo == 1).ToList();

                GE_TPRODUCTOS objProducto = lstProductos.Where(a => a.prod_codigo == p_codProducto).SingleOrDefault();

                bool contieneDistribucion = false;
                return contieneDistribucion = IDistribucionDedicacionPersonas.validaDistribucion(objProducto.prod_consecutivo);
            }
            catch
            {
                throw;
            }
        }
    }
}