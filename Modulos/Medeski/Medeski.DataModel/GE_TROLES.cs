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

public partial class GE_TROLES
{
    public GE_TROLES()
    {
        this.GE_TOPCIONESMENUXROL = new HashSet<GE_TOPCIONESMENUXROL>();
        this.GE_TUSUARIOSXROL = new HashSet<GE_TUSUARIOSXROL>();
    }

    public int rolm_consecutivo { get; set; }
    public string rolm_nombre { get; set; }
    public string rolm_descripcion { get; set; }
    public int rolm_estado { get; set; }
    public string rolm_estadoStr { get; set; }
    public System.DateTime rolm_fecha { get; set; }
    public System.DateTime rolm_fecha_act { get; set; }
    public string rolm_usuario { get; set; }
    public string rolm_usuario_act { get; set; }
    public Nullable<int> rolm_grupo { get; set; }
    public string rolm_dominio { get; set; }

    public virtual ICollection<GE_TOPCIONESMENUXROL> GE_TOPCIONESMENUXROL { get; set; }
    public virtual GE_TPARAMETROS GE_TPARAMETROS { get; set; }
    public virtual ICollection<GE_TUSUARIOSXROL> GE_TUSUARIOSXROL { get; set; }
}
