﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Medeski.DataAcces
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GE_TCALCULOGASTOSVIAJE> GE_TCALCULOGASTOSVIAJE { get; set; }
        public virtual DbSet<GE_TCARGUEARCHIVOS> GE_TCARGUEARCHIVOS { get; set; }
        public virtual DbSet<GE_TCARGUEDISTRIBUCION> GE_TCARGUEDISTRIBUCION { get; set; }
        public virtual DbSet<GE_TCARGUEDRIVERS> GE_TCARGUEDRIVERS { get; set; }
        public virtual DbSet<GE_TCENTROCOSTOPERSONA> GE_TCENTROCOSTOPERSONA { get; set; }
        public virtual DbSet<GE_TCENTROSOPERACION> GE_TCENTROSOPERACION { get; set; }
        public virtual DbSet<GE_TCLASESPARAMETROS> GE_TCLASESPARAMETROS { get; set; }
        public virtual DbSet<GE_TCUENTAS> GE_TCUENTAS { get; set; }
        public virtual DbSet<GE_TCUENTASCLASIFICACION> GE_TCUENTASCLASIFICACION { get; set; }
        public virtual DbSet<GE_TDELEGADOS> GE_TDELEGADOS { get; set; }
        public virtual DbSet<GE_TDISTRIBUCIONCARGUEGA> GE_TDISTRIBUCIONCARGUEGA { get; set; }
        public virtual DbSet<GE_TDISTRIBUCIONDEDICACIONPERSONA> GE_TDISTRIBUCIONDEDICACIONPERSONA { get; set; }
        public virtual DbSet<GE_TDISTRIBUCIONINFRAESTRUCTURA> GE_TDISTRIBUCIONINFRAESTRUCTURA { get; set; }
        public virtual DbSet<GE_TDISTRIBUCIONINTERMEDIOS> GE_TDISTRIBUCIONINTERMEDIOS { get; set; }
        public virtual DbSet<GE_TDISTRIBUCIONMASPROCESOS> GE_TDISTRIBUCIONMASPROCESOS { get; set; }
        public virtual DbSet<GE_TDOMINIOS> GE_TDOMINIOS { get; set; }
        public virtual DbSet<GE_TDRIVERS> GE_TDRIVERS { get; set; }
        public virtual DbSet<GE_TFAMILIAS_PRODUCTOS> GE_TFAMILIAS_PRODUCTOS { get; set; }
        public virtual DbSet<GE_THISTORICOPYG> GE_THISTORICOPYG { get; set; }
        public virtual DbSet<GE_TOPCIONESMENU> GE_TOPCIONESMENU { get; set; }
        public virtual DbSet<GE_TOPCIONESMENUXROL> GE_TOPCIONESMENUXROL { get; set; }
        public virtual DbSet<GE_TPARAMETROS> GE_TPARAMETROS { get; set; }
        public virtual DbSet<GE_TPERIODOPRESUPUESTO> GE_TPERIODOPRESUPUESTO { get; set; }
        public virtual DbSet<GE_TPERIODOTRANSACCIONES> GE_TPERIODOTRANSACCIONES { get; set; }
        public virtual DbSet<GE_TPERIODOTRANSACCPERSONAS> GE_TPERIODOTRANSACCPERSONAS { get; set; }
        public virtual DbSet<GE_TPORCENTAJESPYG> GE_TPORCENTAJESPYG { get; set; }
        public virtual DbSet<GE_TPRODUCTOS> GE_TPRODUCTOS { get; set; }
        public virtual DbSet<GE_TPRODUCTOSITEMS> GE_TPRODUCTOSITEMS { get; set; }
        public virtual DbSet<GE_TPROVEEDORES> GE_TPROVEEDORES { get; set; }
        public virtual DbSet<GE_TREDISTRIBUCION> GE_TREDISTRIBUCION { get; set; }
        public virtual DbSet<GE_TREDISTRIBUCION_DRIVERS> GE_TREDISTRIBUCION_DRIVERS { get; set; }
        public virtual DbSet<GE_TRELITEMSDATACENTERPROD> GE_TRELITEMSDATACENTERPROD { get; set; }
        public virtual DbSet<GE_TSALIDAPRESUPUESTO> GE_TSALIDAPRESUPUESTO { get; set; }
        public virtual DbSet<GE_TSERVIDORES> GE_TSERVIDORES { get; set; }
        public virtual DbSet<GE_TUSUARIOS> GE_TUSUARIOS { get; set; }
        public virtual DbSet<GE_TUSUARIOS_ACCESOS> GE_TUSUARIOS_ACCESOS { get; set; }
        public virtual DbSet<GE_TUSUARIOSXROL> GE_TUSUARIOSXROL { get; set; }
        public virtual DbSet<GE_TVARECONOMICAS> GE_TVARECONOMICAS { get; set; }
        public virtual DbSet<GE_TVLR_CUADRO_SERVICIO> GE_TVLR_CUADRO_SERVICIO { get; set; }
        public virtual DbSet<GE_TVLR_CUADRO_SERVICIO_DETALLE> GE_TVLR_CUADRO_SERVICIO_DETALLE { get; set; }
        public virtual DbSet<PARAMETROSGRALES> PARAMETROSGRALES { get; set; }
        public virtual DbSet<VLRSPRMGRALES> VLRSPRMGRALES { get; set; }
        public virtual DbSet<VW_CS_VLR_GA> VW_CS_VLR_GA { get; set; }
        public virtual DbSet<VW_CS_VLR_MAS_DATACENTER> VW_CS_VLR_MAS_DATACENTER { get; set; }
        public virtual DbSet<VW_CS_VLR_MAS_GA> VW_CS_VLR_MAS_GA { get; set; }
        public virtual DbSet<VW_CS_VLR_MAS_GENTE_TECNICA_DATAC> VW_CS_VLR_MAS_GENTE_TECNICA_DATAC { get; set; }
        public virtual DbSet<VW_CS_VLR_MAS_GENTE_TECNICA_INF> VW_CS_VLR_MAS_GENTE_TECNICA_INF { get; set; }
        public virtual DbSet<VW_CS_VLR_MAS_INFRAESTRUCTURA> VW_CS_VLR_MAS_INFRAESTRUCTURA { get; set; }
        public virtual DbSet<VW_CS_VLR_MAS_SOFTWARE> VW_CS_VLR_MAS_SOFTWARE { get; set; }
        public virtual DbSet<VW_CS_VLR_SERV_ITEM_DATACENTER> VW_CS_VLR_SERV_ITEM_DATACENTER { get; set; }
        public virtual DbSet<VW_CS_VLR_SERV_ITEM_INFRAESTRUCTURA> VW_CS_VLR_SERV_ITEM_INFRAESTRUCTURA { get; set; }
        public virtual DbSet<VW_CUADRO_VENTAS_PYG> VW_CUADRO_VENTAS_PYG { get; set; }
        public virtual DbSet<VW_GENTE_TECNICA> VW_GENTE_TECNICA { get; set; }
        public virtual DbSet<VW_PERIODO_ACTIVO> VW_PERIODO_ACTIVO { get; set; }
        public virtual DbSet<VW_PRESUPUESTO> VW_PRESUPUESTO { get; set; }
        public virtual DbSet<VW_PRODUCTOS_DIRECTOS> VW_PRODUCTOS_DIRECTOS { get; set; }
        public virtual DbSet<VW_PRODUCTOS_SIN_PPTO> VW_PRODUCTOS_SIN_PPTO { get; set; }
        public virtual DbSet<VW_REDISTRIBUCION_SAP> VW_REDISTRIBUCION_SAP { get; set; }
        public virtual DbSet<VW_REPORTE_DISTRIB_INTERM_CS> VW_REPORTE_DISTRIB_INTERM_CS { get; set; }
        public virtual DbSet<VW_REPORTE_REDISTRIBUCION> VW_REPORTE_REDISTRIBUCION { get; set; }
        public virtual DbSet<VW_VLR_CUADRO_SERVICIO> VW_VLR_CUADRO_SERVICIO { get; set; }
        public virtual DbSet<VW_VLR_CUADRO_SERVICIO_BPC> VW_VLR_CUADRO_SERVICIO_BPC { get; set; }
        public virtual DbSet<VW_VLR_DATACENTER_PRD> VW_VLR_DATACENTER_PRD { get; set; }
        public virtual DbSet<VW_VLR_DEDICACION_GENTE> VW_VLR_DEDICACION_GENTE { get; set; }
        public virtual DbSet<VW_VLR_DISTRIB_INTERM> VW_VLR_DISTRIB_INTERM { get; set; }
        public virtual DbSet<VW_VLR_DISTRIB_MAS> VW_VLR_DISTRIB_MAS { get; set; }
        public virtual DbSet<VW_VLR_DISTRIBUCION_CCOSTO> VW_VLR_DISTRIBUCION_CCOSTO { get; set; }
        public virtual DbSet<VW_VLR_ENCABEZ_DISTRIB_MAS> VW_VLR_ENCABEZ_DISTRIB_MAS { get; set; }
        public virtual DbSet<VW_VLR_GA_GENTE_TECNICA> VW_VLR_GA_GENTE_TECNICA { get; set; }
        public virtual DbSet<VW_VLR_GA_GENTE_TECNICA_GSUITE> VW_VLR_GA_GENTE_TECNICA_GSUITE { get; set; }
        public virtual DbSet<VW_VLR_GA_GENTE_TECNICA_MODIF> VW_VLR_GA_GENTE_TECNICA_MODIF { get; set; }
        public virtual DbSet<VW_VLR_GENTE_TECNICA_PROD> VW_VLR_GENTE_TECNICA_PROD { get; set; }
        public virtual DbSet<VW_VLR_ITEMS_DATACENTER> VW_VLR_ITEMS_DATACENTER { get; set; }
        public virtual DbSet<VW_VLR_ITEMS_INFRAESTRUCTURA> VW_VLR_ITEMS_INFRAESTRUCTURA { get; set; }
        public virtual DbSet<VW_VLR_PRESUPUESTO> VW_VLR_PRESUPUESTO { get; set; }
        public virtual DbSet<VW_VLR_PROD_DATACENTER> VW_VLR_PROD_DATACENTER { get; set; }
        public virtual DbSet<VW_VLR_PROD_DATACENTER_MODIF> VW_VLR_PROD_DATACENTER_MODIF { get; set; }
        public virtual DbSet<VW_VLR_PROD_INFRAESTRUCTURA> VW_VLR_PROD_INFRAESTRUCTURA { get; set; }
        public virtual DbSet<VW_VLR_PROD_INFRAESTRUCTURA_MODIF> VW_VLR_PROD_INFRAESTRUCTURA_MODIF { get; set; }
        public virtual DbSet<VW_VLR_REDISTRIBUCION> VW_VLR_REDISTRIBUCION { get; set; }
        public virtual DbSet<VW_VLR_SERV_DATACENTER> VW_VLR_SERV_DATACENTER { get; set; }
        public virtual DbSet<VW_VLR_CUADRO_SERVICIO_TOTAL> VW_VLR_CUADRO_SERVICIO_TOTAL { get; set; }
        public virtual DbSet<GE_TCENTROSCOSTOS> GE_TCENTROSCOSTOS { get; set; }
        public virtual DbSet<GE_TCOMPANIAS> GE_TCOMPANIAS { get; set; }
        public virtual DbSet<GE_TGENTE> GE_TGENTE { get; set; }
        public virtual DbSet<VW_SALIDA_PRESUPUESTO> VW_SALIDA_PRESUPUESTO { get; set; }
        public virtual DbSet<GE_TCARGUEARCHIVOSLABORAL> GE_TCARGUEARCHIVOSLABORAL { get; set; }
        public virtual DbSet<GE_TPERSONAS> GE_TPERSONAS { get; set; }
        public virtual DbSet<GE_TROLES> GE_TROLES { get; set; }
    
        public virtual int sp_CargarPeriodoTransaccion(Nullable<int> inConsecutivo)
        {
            var inConsecutivoParameter = inConsecutivo.HasValue ?
                new ObjectParameter("inConsecutivo", inConsecutivo) :
                new ObjectParameter("inConsecutivo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CargarPeriodoTransaccion", inConsecutivoParameter);
        }
    
        public virtual int sp_CargarPeriodoTransaccionPersona(Nullable<int> inConsecutivo)
        {
            var inConsecutivoParameter = inConsecutivo.HasValue ?
                new ObjectParameter("inConsecutivo", inConsecutivo) :
                new ObjectParameter("inConsecutivo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CargarPeriodoTransaccionPersona", inConsecutivoParameter);
        }
    
        public virtual int sp_GenerarCuadroServicio()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_GenerarCuadroServicio");
        }
    
        public virtual int sp_CalcularValorItemServidor()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_CalcularValorItemServidor");
        }
    
        public virtual int sp_alterdiagram(byte[] definition, string diagramname, Nullable<int> owner_id, Nullable<int> version)
        {
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", definitionParameter, diagramnameParameter, owner_idParameter, versionParameter);
        }
    
        public virtual int sp_creatediagram(byte[] definition, string diagramname, Nullable<int> owner_id, Nullable<int> version)
        {
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", definitionParameter, diagramnameParameter, owner_idParameter, versionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, string new_diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, new_diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int SP_CARGUEDRIVERS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_CARGUEDRIVERS");
        }
    
        public virtual int sp_DuplicarValores(Nullable<int> buscar, Nullable<int> nuevo)
        {
            var buscarParameter = buscar.HasValue ?
                new ObjectParameter("buscar", buscar) :
                new ObjectParameter("buscar", typeof(int));
    
            var nuevoParameter = nuevo.HasValue ?
                new ObjectParameter("nuevo", nuevo) :
                new ObjectParameter("nuevo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_DuplicarValores", buscarParameter, nuevoParameter);
        }
    
        public virtual int sp_ActualizarCuadroServicio()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ActualizarCuadroServicio");
        }
    
        public virtual int sp_ActualizarCuadroBPC()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ActualizarCuadroBPC");
        }
    
        public virtual int sp_ActualizarCuadroServicioDetalle()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ActualizarCuadroServicioDetalle");
        }
    }
}
