//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class GE_TCENTROSCOSTOS
{
    public GE_TCENTROSCOSTOS()
    {
        this.GE_TCARGUEDRIVERS = new HashSet<GE_TCARGUEDRIVERS>();
        this.GE_TCUENTASCLASIFICACION = new HashSet<GE_TCUENTASCLASIFICACION>();
        this.GE_TDISTRIBUCIONCARGUEGA = new HashSet<GE_TDISTRIBUCIONCARGUEGA>();
        this.GE_TPERIODOTRANSACCIONES = new HashSet<GE_TPERIODOTRANSACCIONES>();
        this.GE_TSALIDAPRESUPUESTO = new HashSet<GE_TSALIDAPRESUPUESTO>();
        this.GE_TGENTE = new HashSet<GE_TGENTE>();
        this.GE_TPERSONAS2 = new HashSet<GE_TPERSONAS>();
    }

    public int cost_consecutivo { get; set; }
    public string cost_codigo { get; set; }
    public string cost_descripcion { get; set; }
    public string cost_centro_operacion { get; set; }
    public int cost_consec_responsable { get; set; }
    public int cost_ppto_interno { get; set; }
    public int cost_cuenta_especial { get; set; }
    public int cost_consec_resp_ppto { get; set; }
    public int cost_consec_categoria { get; set; }
    public int cost_activo { get; set; }
    public string cost_usuario { get; set; }
    public System.DateTime cost_fecha { get; set; }
    public string cost_usuario_act { get; set; }
    public System.DateTime cost_fecha_act { get; set; }
    public Nullable<int> cost_empresa { get; set; }
    public string cost_responsable { get; set; }
    public Nullable<int> cost_tipo_cliente { get; set; }
    public Nullable<int> cost_tipo_distribucion { get; set; }

    public virtual ICollection<GE_TCARGUEDRIVERS> GE_TCARGUEDRIVERS { get; set; }
    public virtual GE_TPARAMETROS GE_TPARAMETROS { get; set; }
    public virtual GE_TCENTROSCOSTOS GE_TCENTROSCOSTOS1 { get; set; }
    public virtual GE_TCENTROSCOSTOS GE_TCENTROSCOSTOS2 { get; set; }
    public virtual GE_TCOMPANIAS GE_TCOMPANIAS { get; set; }
    public virtual GE_TPARAMETROS GE_TPARAMETROS1 { get; set; }
    public virtual GE_TPARAMETROS GE_TPARAMETROS2 { get; set; }
    public virtual ICollection<GE_TCUENTASCLASIFICACION> GE_TCUENTASCLASIFICACION { get; set; }
    public virtual ICollection<GE_TDISTRIBUCIONCARGUEGA> GE_TDISTRIBUCIONCARGUEGA { get; set; }
    public virtual ICollection<GE_TPERIODOTRANSACCIONES> GE_TPERIODOTRANSACCIONES { get; set; }
    public virtual ICollection<GE_TSALIDAPRESUPUESTO> GE_TSALIDAPRESUPUESTO { get; set; }
    public virtual ICollection<GE_TGENTE> GE_TGENTE { get; set; }
    public virtual GE_TPERSONAS GE_TPERSONAS { get; set; }
    public virtual GE_TPERSONAS GE_TPERSONAS1 { get; set; }
    public virtual ICollection<GE_TPERSONAS> GE_TPERSONAS2 { get; set; }
}
