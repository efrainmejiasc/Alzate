using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Medeski.DataAcces.Class
{
    interface IGenericDataModel{ }
    public interface ICentrocosto : IGenericDataRepository<GE_TCENTROSCOSTOS> { }
    public interface IClaseparametros : IGenericDataRepository<GE_TCLASESPARAMETROS> { }
    public interface ICuentas : IGenericDataRepository<GE_TCUENTAS> { }
    public interface ICuentasClasificacion : IGenericDataRepository<GE_TCUENTASCLASIFICACION> { }
    public interface IDominios : IGenericDataRepository<GE_TDOMINIOS> { }
    public interface IOpcionesMenu : IGenericDataRepository<GE_TOPCIONESMENU> { }
    public interface IOpcionesMenuxRol : IGenericDataRepository<GE_TOPCIONESMENUXROL> { }
    public interface IParametros : IGenericDataRepository<GE_TPARAMETROS> { }
    public interface IPeriodoPresupuesto : IGenericDataRepository<GE_TPERIODOPRESUPUESTO> { }
    public interface IPeriodoTransacciones : IGenericDataRepository<GE_TPERIODOTRANSACCIONES> { }
    public interface IPeriodoTransaccPersonas : IGenericDataRepository<GE_TPERIODOTRANSACCPERSONAS> { }
    public interface IPersonas : IGenericDataRepository<GE_TPERSONAS> { }
    public interface IProductos : IGenericDataRepository<GE_TPRODUCTOS> { }
    public interface IProductoItems : IGenericDataRepository<GE_TPRODUCTOSITEMS> { }
    public interface IProveedores : IGenericDataRepository<GE_TPROVEEDORES> { }
    public interface IRoles : IGenericDataRepository<GE_TROLES> { }
    public interface IUsuarios : IGenericDataRepository<GE_TUSUARIOS> { }
    public interface IUsuariosAccesos : IGenericDataRepository<GE_TUSUARIOS_ACCESOS> { }
    public interface IUsuariosxRol : IGenericDataRepository<GE_TUSUARIOSXROL> { }
    public interface IVareConomicas : IGenericDataRepository<GE_TVARECONOMICAS> { }
    public interface IParametrosGrales : IGenericDataRepository<PARAMETROSGRALES> { }
    public interface IVlrprmgrales : IGenericDataRepository<VLRSPRMGRALES> { }
    public interface ICargueArchivosLaboral : IGenericDataRepository<GE_TCARGUEARCHIVOSLABORAL> { }
    public interface ICargueArchivos : IGenericDataRepository<GE_TCARGUEARCHIVOS> { }
    public interface IGastosViaje : IGenericDataRepository<GE_TCALCULOGASTOSVIAJE> { }
    public interface IServidores : IGenericDataRepository<GE_TSERVIDORES> { }
    public interface IDistriGastoArea : IGenericDataRepository<GE_TDISTRIBUCIONCARGUEGA> { }
    public interface IDistribIntermedios : IGenericDataRepository<GE_TDISTRIBUCIONINTERMEDIOS> { }
    public interface IDistribInfraest : IGenericDataRepository<GE_TDISTRIBUCIONINFRAESTRUCTURA> { }
    public interface ICargueDrivers : IGenericDataRepository<GE_TCARGUEDRIVERS> { }
    public interface ICentroOperacion : IGenericDataRepository<GE_TCENTROSOPERACION> { }
    public interface ICargueGente : IGenericDataRepository<GE_TGENTE> { }
    public interface IDistribucionDedicacionPersonas : IGenericDataRepository<GE_TDISTRIBUCIONDEDICACIONPERSONA> { }
    public interface IDistribMAS : IGenericDataRepository<GE_TDISTRIBUCIONMASPROCESOS> { }
    public interface IRedistribucion : IGenericDataRepository<GE_TREDISTRIBUCION> { }
    public interface IvwGenteTecnica : IGenericDataRepository<VW_GENTE_TECNICA> { }
    public interface IVwPresupuesto : IGenericDataRepository<VW_PRESUPUESTO> { }
    public interface IVwSalidaPresupuesto : IGenericDataRepository<VW_SALIDA_PRESUPUESTO> { }
    public interface IVwVlrDataCenterPrd : IGenericDataRepository<VW_VLR_DATACENTER_PRD> { }
    public interface IVwVlrDedicacionGente : IGenericDataRepository<VW_VLR_DEDICACION_GENTE> { }
    public interface IVwVlrDistrinInterm : IGenericDataRepository<VW_VLR_DISTRIB_INTERM> { }
    public interface IVwVlrDistribMAS : IGenericDataRepository<VW_VLR_DISTRIB_MAS> { }
    public interface IVwEncabDistribMAS : IGenericDataRepository<VW_VLR_ENCABEZ_DISTRIB_MAS> { }
    public interface IVwVlrGenteTecnProd : IGenericDataRepository<VW_VLR_GENTE_TECNICA_PROD> { }
    public interface IVwVlrItemsDataCenter : IGenericDataRepository<VW_VLR_ITEMS_DATACENTER> { }
    public interface IVwVlrItemsInf : IGenericDataRepository<VW_VLR_ITEMS_INFRAESTRUCTURA> { }
    public interface IVwVlrPresupuesto : IGenericDataRepository<VW_VLR_PRESUPUESTO> { }
    public interface IVwVlrProdDataCenter : IGenericDataRepository<VW_VLR_PROD_DATACENTER> { }
    public interface IVwVlrProdInf : IGenericDataRepository<VW_VLR_PROD_INFRAESTRUCTURA> { }
    public interface IVwVlrRedistribucion : IGenericDataRepository<VW_VLR_REDISTRIBUCION> { }
    public interface IVwVlrServDataCenter : IGenericDataRepository<VW_VLR_SERV_DATACENTER> { }

    public interface ICompanias : IGenericDataRepository<GE_TCOMPANIAS> { }
    public interface IDrivers : IGenericDataRepository<GE_TDRIVERS> { }
    public interface IVwCuadroServicio : IGenericDataRepository<VW_VLR_CUADRO_SERVICIO> { }
    public interface IVwCuadroServicioTotal : IGenericDataRepository<VW_VLR_CUADRO_SERVICIO_TOTAL> { }
    public interface IDelegados : IGenericDataRepository<GE_TDELEGADOS> { }
    public interface IGente : IGenericDataRepository<GE_TGENTE> { }
    public interface IFamiliasProductos: IGenericDataRepository<GE_TFAMILIAS_PRODUCTOS> { }
    public interface IVwProductosDirectos : IGenericDataRepository<VW_PRODUCTOS_DIRECTOS> { }
    public interface ICargueDistribucion : IGenericDataRepository<GE_TCARGUEDISTRIBUCION> { }
    public interface IRedistribucionDrivers : IGenericDataRepository<GE_TREDISTRIBUCION_DRIVERS> { }

    public interface IVWReporteRedistribucion : IGenericDataRepository<VW_REPORTE_REDISTRIBUCION> { }

    public interface IVWCuadroVentasPYG : IGenericDataRepository<VW_CUADRO_VENTAS_PYG> { }
    public interface IHistoricoPYG : IGenericDataRepository<GE_THISTORICOPYG> { }

    public interface IPorcentajesPYG : IGenericDataRepository<GE_TPORCENTAJESPYG> { }
    public interface ISalidaPresupuesto : IGenericDataRepository<GE_TSALIDAPRESUPUESTO> { }

    public interface IVWProductosSinPpto : IGenericDataRepository<VW_PRODUCTOS_SIN_PPTO> { }

    public interface IVlrCuadroServicio : IGenericDataRepository<GE_TVLR_CUADRO_SERVICIO> { }
    public interface IVlrCuadroServicioDetalle : IGenericDataRepository<GE_TVLR_CUADRO_SERVICIO_DETALLE> { }
}
