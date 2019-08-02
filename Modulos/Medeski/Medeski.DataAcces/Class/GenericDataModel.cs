using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Medeski.DataAcces.Class
{
    class GenericDataModel{ }
    public class Centrocosto : GenericDataRepository<GE_TCENTROSCOSTOS>, ICentrocosto { }
    public class Claseparametros : GenericDataRepository<GE_TCLASESPARAMETROS>, IClaseparametros { }
    public class Cuentas : GenericDataRepository<GE_TCUENTAS>, ICuentas { }
    public class CuentasClasificacion : GenericDataRepository<GE_TCUENTASCLASIFICACION>, ICuentasClasificacion { }
    public class Dominios : GenericDataRepository<GE_TDOMINIOS>, IDominios { }
    public class OpcionesMenu : GenericDataRepository<GE_TOPCIONESMENU>, IOpcionesMenu { }
    public class OpcionesMenuxRol : GenericDataRepository<GE_TOPCIONESMENUXROL>, IOpcionesMenuxRol { }
    public class Parametros : GenericDataRepository<GE_TPARAMETROS>, IParametros { }
    public class PeriodoPresupuesto : GenericDataRepository<GE_TPERIODOPRESUPUESTO>, IPeriodoPresupuesto { }
    public class PeriodoTransacciones : GenericDataRepository<GE_TPERIODOTRANSACCIONES>, IPeriodoTransacciones { }
    public class PeriodoTransaccPersonas : GenericDataRepository<GE_TPERIODOTRANSACCPERSONAS>, IPeriodoTransaccPersonas { }
    public class Personas : GenericDataRepository<GE_TPERSONAS>, IPersonas { }
    public class Productos : GenericDataRepository<GE_TPRODUCTOS>, IProductos { }
    public class ProductoItems : GenericDataRepository<GE_TPRODUCTOSITEMS>, IProductoItems { }
    public class Proveedores : GenericDataRepository<GE_TPROVEEDORES>, IProveedores { }
    public class Roles : GenericDataRepository<GE_TROLES>, IRoles { }
    public class Usuarios : GenericDataRepository<GE_TUSUARIOS>, IUsuarios { }
    public class UsuariosAccesos : GenericDataRepository<GE_TUSUARIOS_ACCESOS>, IUsuariosAccesos { }
    public class UsuariosxRol : GenericDataRepository<GE_TUSUARIOSXROL>, IUsuariosxRol { }
    public class VareConomicas : GenericDataRepository<GE_TVARECONOMICAS>, IVareConomicas { }
    public class ParametrosGrales : GenericDataRepository<PARAMETROSGRALES>, IParametrosGrales { }
    public class Vlrprmgrales : GenericDataRepository<VLRSPRMGRALES>, IVlrprmgrales { }
    public class CargueArchivosLaboral : GenericDataRepository<GE_TCARGUEARCHIVOSLABORAL>, ICargueArchivosLaboral { }
    public class CargueArchivos : GenericDataRepository<GE_TCARGUEARCHIVOS>, ICargueArchivos { }
    public class GastosViaje : GenericDataRepository<GE_TCALCULOGASTOSVIAJE>, IGastosViaje { }
    public class Servidores : GenericDataRepository<GE_TSERVIDORES>, IServidores { }
    public class CargueGastosArea : GenericDataRepository<GE_TDISTRIBUCIONCARGUEGA>, IDistriGastoArea { }
    public class DistribIntermedios : GenericDataRepository<GE_TDISTRIBUCIONINTERMEDIOS>, IDistribIntermedios { }
    public class DistribInfraest : GenericDataRepository<GE_TDISTRIBUCIONINFRAESTRUCTURA>, IDistribInfraest { }
	public class CargueDrivers : GenericDataRepository<GE_TCARGUEDRIVERS>, ICargueDrivers { }
    public class CentroOperacion : GenericDataRepository<GE_TCENTROSOPERACION>, ICentroOperacion { }
    public class CargueGente : GenericDataRepository<GE_TGENTE>, ICargueGente { }
    public class DistribucionDedicacionPersonas : GenericDataRepository<GE_TDISTRIBUCIONDEDICACIONPERSONA>, IDistribucionDedicacionPersonas { }
    public class DistribMASProcesos : GenericDataRepository<GE_TDISTRIBUCIONMASPROCESOS>, IDistribMAS { }
    public class Redistribucion : GenericDataRepository<GE_TREDISTRIBUCION>, IRedistribucion { }
    public class VwGenteTecnica : GenericDataRepository<VW_GENTE_TECNICA>, IvwGenteTecnica { }
    public class VwPresupuesto : GenericDataRepository<VW_PRESUPUESTO>, IVwPresupuesto { }
    public class VwSalidaPresupuesto : GenericDataRepository<VW_SALIDA_PRESUPUESTO>, IVwSalidaPresupuesto { }
    public class VwVlrDataCenterPrd : GenericDataRepository<VW_VLR_DATACENTER_PRD>, IVwVlrDataCenterPrd { }
    public class VwVlrDedicacionGente : GenericDataRepository<VW_VLR_DEDICACION_GENTE>, IVwVlrDedicacionGente { }
    public class VwVlrDistribInterm : GenericDataRepository<VW_VLR_DISTRIB_INTERM>, IVwVlrDistrinInterm { }
    public class VwVlrDistribMAS : GenericDataRepository<VW_VLR_DISTRIB_MAS>, IVwVlrDistribMAS { }
    public class VwVlrEncabDistribMAS : GenericDataRepository<VW_VLR_ENCABEZ_DISTRIB_MAS>, IVwEncabDistribMAS { }
    public class VwVlrGenteTecnicaProd : GenericDataRepository<VW_VLR_GENTE_TECNICA_PROD>, IVwVlrGenteTecnProd { }
    public class VwVlrItemsDataCenter : GenericDataRepository<VW_VLR_ITEMS_DATACENTER>, IVwVlrItemsDataCenter { }
    public class VwVlrItemsInfr : GenericDataRepository<VW_VLR_ITEMS_INFRAESTRUCTURA>, IVwVlrItemsInf { }
    public class VwVlrPresupuesto : GenericDataRepository<VW_VLR_PRESUPUESTO>, IVwVlrPresupuesto { }
    public class VwVlrProdDataCenter : GenericDataRepository<VW_VLR_PROD_DATACENTER>, IVwVlrProdDataCenter { }
    public class VwVlrProdInfr : GenericDataRepository<VW_VLR_PROD_INFRAESTRUCTURA>, IVwVlrProdInf { }
    public class VwVlrRedistribucion : GenericDataRepository<VW_VLR_REDISTRIBUCION>, IVwVlrRedistribucion { }
    public class VwVlrServDataCenter : GenericDataRepository<VW_VLR_SERV_DATACENTER>, IVwVlrServDataCenter { }

    public class Companias : GenericDataRepository<GE_TCOMPANIAS>, ICompanias { }
    public class Drivers : GenericDataRepository<GE_TDRIVERS>, IDrivers { }
    public class VwCuadroServicio : GenericDataRepository<VW_VLR_CUADRO_SERVICIO>, IVwCuadroServicio { }
    public class VwCuadroServicioTotal : GenericDataRepository<VW_VLR_CUADRO_SERVICIO_TOTAL>, IVwCuadroServicioTotal { }
    public class Delegados : GenericDataRepository<GE_TDELEGADOS>, IDelegados { }
    public class Gente : GenericDataRepository<GE_TGENTE>, IGente { }
    public class FamiliasProductos : GenericDataRepository<GE_TFAMILIAS_PRODUCTOS>, IFamiliasProductos{ }

    public class VwProductosDirectos : GenericDataRepository<VW_PRODUCTOS_DIRECTOS>, IVwProductosDirectos { }
    public class CargueDistribucion : GenericDataRepository<GE_TCARGUEDISTRIBUCION>, ICargueDistribucion { }
    public class RedistribucionDrivers : GenericDataRepository<GE_TREDISTRIBUCION_DRIVERS>, IRedistribucionDrivers { }

    public class VWReporteRedistribucion : GenericDataRepository<VW_REPORTE_REDISTRIBUCION>, IVWReporteRedistribucion { }
    public class VWCuadroVentasPYG : GenericDataRepository<VW_CUADRO_VENTAS_PYG>, IVWCuadroVentasPYG { }
    public class HistoricoPYG : GenericDataRepository<GE_THISTORICOPYG>, IHistoricoPYG { }

    public class PorcentajesPYG : GenericDataRepository<GE_TPORCENTAJESPYG>, IPorcentajesPYG { }
    public class SalidaPresupuesto : GenericDataRepository<GE_TSALIDAPRESUPUESTO>, ISalidaPresupuesto { }

    public class VWProductosSinPpto : GenericDataRepository<VW_PRODUCTOS_SIN_PPTO>, IVWProductosSinPpto { }

    public class VlrCuadroServicio : GenericDataRepository<GE_TVLR_CUADRO_SERVICIO>, IVlrCuadroServicio { }
    public class VlrCuadroServicioDetalle : GenericDataRepository<GE_TVLR_CUADRO_SERVICIO_DETALLE>, IVlrCuadroServicioDetalle { }

}
