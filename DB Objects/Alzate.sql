USE [Medeski]
GO
/****** Object:  User [usrMedeski]    Script Date: 02/08/2019 17:09:57 ******/
CREATE USER [usrMedeski] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [usrMedeski]
GO
/****** Object:  UserDefinedTableType [dbo].[TMP_CARGUEDRIVERS]    Script Date: 02/08/2019 17:09:57 ******/
CREATE TYPE [dbo].[TMP_CARGUEDRIVERS] AS TABLE(
	[carg_periodo] [int] NULL,
	[carg_producto] [int] NULL,
	[carg_compania] [int] NULL,
	[carg_sede] [varchar](100) NULL,
	[carg_ccosto] [int] NULL,
	[carg_driver] [int] NULL,
	[carg_cantidad] [decimal](38, 10) NULL,
	[carg_valor] [int] NULL,
	[carg_valor_distribucion] [decimal](38, 10) NULL,
	[carg_valor_adicional] [decimal](38, 10) NULL,
	[carg_proveedor] [varchar](50) NULL,
	[carg_usuario] [varchar](50) NULL,
	[carg_fecha] [datetime] NULL,
	[carg_usuario_act] [varchar](50) NULL,
	[carg_fecha_act] [datetime] NULL,
	[carg_activo] [int] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[get_MSSQL_CLIENT]    Script Date: 02/08/2019 17:09:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[get_MSSQL_CLIENT]() returns nvarchar(3) 
 as 
 begin 
    return (rtrim(substring(cast(context_info() as nvarchar(64)), 1, 3))) 
 end 

GO
/****** Object:  UserDefinedFunction [dbo].[get_MSSQL_LANGU]    Script Date: 02/08/2019 17:09:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[get_MSSQL_LANGU]() returns nvarchar(1) 
 as 
 begin 
    return (rtrim(substring(cast(context_info() as nvarchar(64)), 16, 1))) 
 end 

GO
/****** Object:  UserDefinedFunction [dbo].[get_mssql_session_id]    Script Date: 02/08/2019 17:09:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[get_mssql_session_id]() returns smallint 
 as 
 begin 
    return ISNULL(cast(substring(context_info(), 33, 2) as smallint),@@SPID) 
 end 

GO
/****** Object:  UserDefinedFunction [dbo].[get_MSSQL_SYSTEM_DATE]    Script Date: 02/08/2019 17:09:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[get_MSSQL_SYSTEM_DATE]() returns nvarchar(8) 
 as 
 begin 
    return (rtrim(substring(cast(context_info() as nvarchar(64)), 18, 8))) 
 end 

GO
/****** Object:  UserDefinedFunction [dbo].[get_MSSQL_USER]    Script Date: 02/08/2019 17:09:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[get_MSSQL_USER]() returns nvarchar(12) 
 as 
 begin 
    return (rtrim(substring(cast(context_info() as nvarchar(64)), 4, 12))) 
 end 

GO
/****** Object:  Table [dbo].[GE_TPRODUCTOS]    Script Date: 02/08/2019 17:09:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPRODUCTOS](
	[prod_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[prod_codigo] [varchar](50) NOT NULL,
	[prod_descripcion] [varchar](500) NOT NULL,
	[prod_responsable] [int] NOT NULL,
	[prod_tipo_licencia] [int] NOT NULL,
	[prod_intermedio] [int] NOT NULL,
	[prod_contrato] [int] NOT NULL,
	[prod_componente] [int] NOT NULL,
	[prod_criterio] [int] NOT NULL,
	[prod_activo] [int] NOT NULL,
	[prod_usuario] [varchar](30) NOT NULL,
	[prod_fecha] [datetime] NOT NULL,
	[prod_usuario_act] [varchar](30) NOT NULL,
	[prod_fecha_act] [datetime] NOT NULL,
	[prod_serv_venta] [int] NULL,
	[prod_driver1] [int] NULL,
	[prod_driver2] [int] NULL,
	[prod_categ_serv] [int] NULL,
	[prod_tipo] [int] NULL,
	[prod_redistribuir] [int] NULL,
	[prod_interm_no_distrib] [int] NULL,
	[prod_distrib_serv] [int] NULL,
	[prod_tipo_producto] [int] NULL,
	[prod_directo_agrupado] [int] NULL,
 CONSTRAINT [PK_GE_TPRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[prod_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_GE_TPRODUCTOS] UNIQUE NONCLUSTERED 
(
	[prod_codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_GE_TPRODUCTOS_2] UNIQUE NONCLUSTERED 
(
	[prod_descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPRODUCTOSITEMS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPRODUCTOSITEMS](
	[prit_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[prit_producto] [int] NOT NULL,
	[prit_item] [varchar](200) NOT NULL,
	[prit_cuenta] [int] NOT NULL,
	[prit_tipo] [varchar](10) NULL,
	[prit_activo] [int] NOT NULL,
	[prit_usuario] [varchar](30) NOT NULL,
	[prit_fecha] [datetime] NOT NULL,
	[prit_usuario_act] [varchar](30) NOT NULL,
	[prit_fecha_act] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TPRODUCTOSITEMS] PRIMARY KEY CLUSTERED 
(
	[prit_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_BPC_ITEMS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_BPC_ITEMS]
AS
SELECT     t.prit_consecutivo, t.prit_producto, t.prit_item
FROM         dbo.GE_TPRODUCTOSITEMS AS t INNER JOIN
                      dbo.GE_TPRODUCTOS AS p ON t.prit_producto = p.prod_consecutivo
WHERE     (t.prit_activo = 1) AND (p.prod_descripcion IN ('INFRAESTRUCTURA', 'DATACENTER'))
GO
/****** Object:  Table [dbo].[GE_TPERSONAS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPERSONAS](
	[pers_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[pers_tipodoc] [varchar](10) NOT NULL,
	[pers_identificacion] [varchar](50) NOT NULL,
	[pers_nombre] [varchar](100) NULL,
	[pers_apellido] [varchar](100) NULL,
	[pers_nombres] [varchar](200) NOT NULL,
	[pers_consec_jefe] [int] NOT NULL,
	[pers_tipo_contrato] [int] NOT NULL,
	[pers_metodo_distrib] [int] NOT NULL,
	[pers_cargo] [int] NOT NULL,
	[pers_grupo] [int] NOT NULL,
	[pers_activo] [int] NOT NULL,
	[pers_empresa] [int] NULL,
	[pers_ccosto] [int] NULL,
	[pers_usudom] [varchar](30) NULL,
	[pers_nombre_area] [int] NULL,
	[pers_nombre_busq] [varchar](200) NULL,
	[pers_ceop] [int] NULL,
	[pers_usuario] [varchar](30) NOT NULL,
	[pers_usuario_act] [varchar](30) NOT NULL,
	[pers_fecha_act] [datetime] NOT NULL,
	[pers_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TPERSONAS] PRIMARY KEY CLUSTERED 
(
	[pers_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_BPC_PERSONAS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_BPC_PERSONAS]
AS
SELECT     pers_consecutivo, 'PE_'+Convert(varchar(100),pers_consecutivo) as codigo, pers_nombres
FROM         dbo.GE_TPERSONAS

GO
/****** Object:  Table [dbo].[GE_TSERVIDORES]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TSERVIDORES](
	[serv_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[serv_nombre] [varchar](50) NOT NULL,
	[ser_marca] [varchar](50) NULL,
	[ser_modelo] [varchar](80) NULL,
	[serv_funcion] [varchar](100) NULL,
	[serv_estado] [varchar](80) NULL,
	[serv_ip] [varchar](20) NULL,
	[ser_sistema_operativo] [varchar](100) NULL,
	[serv_numero_bits] [varchar](5) NULL,
	[serv_memoria] [decimal](18, 3) NULL,
	[serv_procesadores] [varchar](150) NULL,
	[serv_core] [decimal](18, 3) NULL,
	[serv_disco_duro] [decimal](18, 3) NULL,
	[serv_descripcion_disco_duro] [varchar](100) NULL,
	[serv_aplicaciones_instaladas] [varchar](500) NULL,
	[serv_virtualizado] [int] NULL,
	[serv_software_virtualizacion] [varchar](80) NULL,
	[serv_granja_virtual] [varchar](80) NULL,
	[serv_ubicacion_fisica] [varchar](80) NULL,
	[serv_activo_fijo] [varchar](50) NULL,
	[serv_depreciable] [int] NULL,
	[serv_activo] [int] NOT NULL,
	[serv_usuario] [varchar](50) NOT NULL,
	[serv_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TSERVIDORES] PRIMARY KEY CLUSTERED 
(
	[serv_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_GE_TSERVIDORES] UNIQUE NONCLUSTERED 
(
	[serv_nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_BPC_SERVIDORES]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_BPC_SERVIDORES]
AS
SELECT     serv_consecutivo, 'SE_'+Convert(varchar(100),serv_consecutivo) as codigo, serv_nombre
FROM         dbo.GE_TSERVIDORES

GO
/****** Object:  Table [dbo].[GE_TVLR_CUADRO_SERVICIO_DETALLE]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TVLR_CUADRO_SERVICIO_DETALLE](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Idservicio] [int] NULL,
	[Servicio] [varchar](100) NULL,
	[IdProducto] [int] NULL,
	[Producto] [varchar](100) NULL,
	[Tipo] [varchar](100) NULL,
	[SubTipo] [varchar](100) NULL,
	[ServPersona] [varchar](500) NULL,
	[Item] [varchar](500) NULL,
	[Valor] [decimal](20, 0) NULL,
	[Usuario] [varchar](50) NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [PK_GE_TVLR_CUADRO_SERVICIO_DETALLE] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TVW_BPC_SALIDA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TVW_BPC_SALIDA](
	[bpc_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[bpc_id] [varchar](50) NULL,
	[bpc_prod_desc] [varchar](200) NULL,
	[bpc_padre] [varchar](50) NULL,
	[bpc_tipo_cuenta] [varchar](50) NULL,
 CONSTRAINT [PK_GE_VW_BPC_SALIDA] PRIMARY KEY CLUSTERED 
(
	[bpc_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_BPC_SALIDA_FINAL]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_BPC_SALIDA_FINAL]
AS
SELECT     T2.bpc_id, T3.codigo, T4.prit_consecutivo as Item, T1.Valor, 'COP' AS Moneda
FROM         dbo.GE_TVLR_CUADRO_SERVICIO_DETALLE AS T1 INNER JOIN
                      dbo.GE_TVW_BPC_SALIDA AS T2 ON T1.Producto + '_' + T1.SubTipo = T2.bpc_prod_desc INNER JOIN
                          (SELECT     pers_consecutivo, codigo, pers_nombres
                            FROM          dbo.VW_BPC_PERSONAS
                            UNION
                            SELECT     serv_consecutivo, codigo, serv_nombre
                            FROM         dbo.VW_BPC_SERVIDORES) AS T3 ON T1.ServPersona = T3.pers_nombres LEFT OUTER JOIN
                      dbo.VW_BPC_ITEMS AS T4 ON T1.Item = T4.prit_item

GO
/****** Object:  Table [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA](
	[dper_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[dper_periodo] [int] NOT NULL,
	[dper_tipo] [varchar](50) NOT NULL,
	[dper_persona] [int] NOT NULL,
	[dper_servidor] [int] NULL,
	[dper_producto] [int] NULL,
	[dper_valor] [decimal](20, 10) NOT NULL,
	[dper_estado] [int] NOT NULL,
	[dper_usuario] [varchar](50) NOT NULL,
	[dper_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TDISTRIBUCIONDEDICACIONPERSONA] PRIMARY KEY CLUSTERED 
(
	[dper_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TGENTE]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TGENTE](
	[gent_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[gent_periodo] [int] NULL,
	[gent_ccostos] [int] NULL,
	[gent_descripcion_ccostos] [varchar](80) NULL,
	[gent_ceop] [int] NULL,
	[gent_nombre_cargo] [varchar](100) NULL,
	[gent_empresa] [varchar](80) NULL,
	[gent_costo_colaborador] [numeric](18, 2) NULL,
	[gent_persona] [int] NULL,
	[gent_porcentaje_manual_dedicacion] [numeric](18, 2) NULL,
	[gent_usuario_carga] [varchar](50) NULL,
	[gent_fecha_cargue] [date] NULL,
	[gent_estado] [int] NULL,
 CONSTRAINT [PK_GE_TGENTE] PRIMARY KEY CLUSTERED 
(
	[gent_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPERIODOPRESUPUESTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPERIODOPRESUPUESTO](
	[peri_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[peri_ano] [int] NOT NULL,
	[peri_paso] [int] NOT NULL,
	[peri_activo] [int] NOT NULL,
	[peri_usuario] [varchar](30) NOT NULL,
	[peri_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TPERIODOPRESUPUESTO] PRIMARY KEY CLUSTERED 
(
	[peri_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_GENTE_TECNICA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



















--- Vista que calcula lo que cuesta la persona de infraestructura teniendo en cuenta el porcentaje de Dedicación
--- que se capturo en la pantala de Distribución de Dedicación de personas en Medeski

-- ** Formula **
-- Valor = (% del valor Distribución / suma % de la dedicacion ) * (Valor del colaborador 'Gente' * Porcentaje a ojimetro de la tabla gente)

-- Desarrolla : Jhon Alexis Ramirez
-- Fecha 22 - Nov - 2017

-- Modificacion : Mauricio Escobar
-- Fecha: Agosto 3/2018 - Se adiciona validación de porcentaje total > 0

CREATE VIEW [dbo].[VW_GENTE_TECNICA]
AS
SELECT
	
  PERSONAS.pers_identificacion AS NUMERO_CEDULA,
  PERSONAS.pers_apellido + ' ' + PERSONAS.pers_nombre AS NOMBRE,
  DISTRIBUCION.DPER_TIPO AS TIPO_DISTRIBUCION,
  ISNULL(SERVIDORES.SERV_CONSECUTIVO, 0) AS IDSERVIDOR,
  ISNULL(SERVIDORES.SERV_NOMBRE, '-') AS NOMBRE_SERVIDOR,
  ISNULL(PRODUCTOS.prod_consecutivo, 0) AS IDPRODUCTO,
  ISNULL(PRODUCTOS.PROD_CODIGO, '-') AS NOMBRE_PRODUCTO,
  DISTRIBUCION.DPER_VALOR AS PORCENTAJE_DEDICACION,
  GENTE.GENT_COSTO_COLABORADOR AS COSTO_COLABORADOR,
  CASE PORCENTAJE.TOTAL
  WHEN 0 THEN 0
  ELSE ROUND((DISTRIBUCION.DPER_VALOR / PORCENTAJE.TOTAL) * (GENTE.GENT_COSTO_COLABORADOR * GENTE.GENT_PORCENTAJE_MANUAL_DEDICACION),0) 
  END COSTO_DISTIBUCION,
  PORCENTAJE.TOTAL AS TOTAL_PERSONA,
  PRESUPUESTO.PERI_ANO AS PERIODO_ACTUAL
FROM GE_TDISTRIBUCIONDEDICACIONPERSONA DISTRIBUCION
INNER JOIN GE_TPERSONAS PERSONAS
	ON DISTRIBUCION.dper_persona = PERSONAS.pers_consecutivo

INNER JOIN GE_TGENTE GENTE
  ON PERSONAS.pers_consecutivo = GENTE.gent_persona
  
LEFT JOIN GE_TSERVIDORES SERVIDORES
  ON SERVIDORES.SERV_CONSECUTIVO = DISTRIBUCION.DPER_SERVIDOR
LEFT JOIN GE_TPRODUCTOS PRODUCTOS
  ON PRODUCTOS.PROD_CONSECUTIVO = DISTRIBUCION.DPER_PRODUCTO
INNER JOIN (SELECT
  DISTRIBUCION.DPER_PERSONA AS PERSONA,
  SUM(DISTRIBUCION.DPER_VALOR) AS TOTAL
FROM GE_TDISTRIBUCIONDEDICACIONPERSONA DISTRIBUCION
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.[peri_consecutivo] = DISTRIBUCION.dper_periodo
WHERE p.[peri_activo] = 1 AND DISTRIBUCION.dper_estado = 1
GROUP BY DISTRIBUCION.DPER_PERSONA) PORCENTAJE
  ON DISTRIBUCION.DPER_PERSONA = PORCENTAJE.PERSONA
INNER JOIN GE_TPERIODOPRESUPUESTO PRESUPUESTO
  ON PRESUPUESTO.PERI_CONSECUTIVO = DISTRIBUCION.DPER_PERIODO
  AND PRESUPUESTO.PERI_CONSECUTIVO = GENTE.GENT_PERIODO
  AND PRESUPUESTO.PERI_ACTIVO = 1
WHERE 1 = 1 AND GENTE.gent_estado = 1
AND DISTRIBUCION.DPER_ESTADO = 1 
AND DISTRIBUCION.DPER_VALOR > 0










GO
/****** Object:  Table [dbo].[GE_TSALIDAPRESUPUESTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TSALIDAPRESUPUESTO](
	[sali_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[sali_periodo_transacc] [int] NOT NULL,
	[sali_persona] [int] NOT NULL,
	[sali_centrocosto] [int] NOT NULL,
	[sali_producto_item] [int] NOT NULL,
	[sali_mes] [int] NOT NULL,
	[sali_moneda] [int] NOT NULL,
	[sali_valor] [decimal](18, 6) NOT NULL,
	[sali_usuario] [varchar](30) NOT NULL,
	[sali_fecha] [datetime] NOT NULL,
	[sali_tipo] [varchar](50) NULL,
	[sali_periodo] [int] NULL,
 CONSTRAINT [PK_GE_TSALIDAPRESUPUESTO] PRIMARY KEY CLUSTERED 
(
	[sali_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCENTROSCOSTOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCENTROSCOSTOS](
	[cost_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[cost_codigo] [varchar](50) NOT NULL,
	[cost_descripcion] [varchar](500) NOT NULL,
	[cost_centro_operacion] [varchar](50) NOT NULL,
	[cost_consec_responsable] [int] NOT NULL,
	[cost_ppto_interno] [int] NOT NULL,
	[cost_cuenta_especial] [int] NOT NULL,
	[cost_consec_resp_ppto] [int] NOT NULL,
	[cost_consec_categoria] [int] NOT NULL,
	[cost_activo] [int] NOT NULL,
	[cost_usuario] [varchar](30) NOT NULL,
	[cost_fecha] [datetime] NOT NULL,
	[cost_usuario_act] [varchar](30) NOT NULL,
	[cost_fecha_act] [datetime] NOT NULL,
	[cost_empresa] [int] NULL,
	[cost_responsable] [varchar](100) NULL,
	[cost_tipo_cliente] [int] NULL,
	[cost_tipo_distribucion] [int] NULL,
 CONSTRAINT [PK_GE_TCENTROSCOSTOS] PRIMARY KEY CLUSTERED 
(
	[cost_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_GE_TCENTROSCOSTOS] UNIQUE NONCLUSTERED 
(
	[cost_codigo] ASC,
	[cost_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCOMPANIAS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCOMPANIAS](
	[comp_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[comp_nombre] [varchar](100) NULL,
	[comp_descripcion] [varchar](500) NULL,
	[comp_activo] [int] NULL,
	[comp_usa_co] [int] NULL,
	[comp_usuario] [varchar](100) NULL,
	[comp_fecha] [datetime] NULL,
 CONSTRAINT [PK_GE_TCOMPANIAS] PRIMARY KEY CLUSTERED 
(
	[comp_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCUENTAS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCUENTAS](
	[cuen_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[cuen_auxiliar] [varchar](50) NOT NULL,
	[cuen_descripcion] [varchar](500) NOT NULL,
	[cuen_mayor] [varchar](50) NOT NULL,
	[cuen_activo] [int] NOT NULL,
	[cuen_usuario] [varchar](30) NOT NULL,
	[cuen_fecha] [datetime] NOT NULL,
	[cuen_amortizar] [int] NULL,
 CONSTRAINT [PK_GE_TCUENTAS] PRIMARY KEY CLUSTERED 
(
	[cuen_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPARAMETROS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPARAMETROS](
	[parm_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[clap_clase] [int] NOT NULL,
	[parm_descripcion] [varchar](500) NULL,
	[parm_fechadesde] [date] NOT NULL,
	[parm_fechahasta] [date] NULL,
	[parm_estado] [int] NOT NULL,
	[parm_infoadicional] [varchar](100) NULL,
	[parm_codigo] [varchar](50) NULL,
 CONSTRAINT [PK_PARM_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[parm_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_GE_TPARAMETROS] UNIQUE NONCLUSTERED 
(
	[clap_clase] ASC,
	[parm_codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPERIODOTRANSACCIONES]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPERIODOTRANSACCIONES](
	[petr_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[petr_periodo] [int] NOT NULL,
	[petr_persona] [int] NOT NULL,
	[petr_centrocosto] [int] NOT NULL,
	[petr_producto_item] [int] NOT NULL,
	[petr_moneda] [int] NOT NULL,
	[petr_valor] [decimal](18, 6) NOT NULL,
	[petr_valor_amortizar] [decimal](18, 6) NOT NULL,
	[petr_mes] [int] NOT NULL,
	[petr_proveedor] [int] NOT NULL,
	[petr_trm] [decimal](18, 6) NOT NULL,
	[petr_cantidad] [int] NOT NULL,
	[petr_tipo_viaje] [int] NULL,
	[petr_amortizar] [int] NOT NULL,
	[petr_observacion] [varchar](1000) NOT NULL,
	[petr_activo] [int] NOT NULL,
	[petr_usuario] [varchar](30) NOT NULL,
	[petr_fecha] [datetime] NOT NULL,
	[petr_usuario_act] [varchar](30) NOT NULL,
	[petr_fecha_act] [datetime] NOT NULL,
	[petr_meses_amortizar] [int] NULL,
	[petr_tipo] [varchar](50) NULL,
 CONSTRAINT [PK_GE_TPERIODOTRANSACCIONES] PRIMARY KEY CLUSTERED 
(
	[petr_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_SALIDA_PRESUPUESTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO















CREATE VIEW [dbo].[VW_SALIDA_PRESUPUESTO] AS
SELECT	sal.[sali_consecutivo],
		pers.pers_identificacion identificacion, pers.pers_nombres nombre,  pr.prod_descripcion producto, 
		prit.prit_item item, cuenta.cuen_auxiliar + '-' + cuenta.cuen_descripcion cuenta,
		CONVERT(VARCHAR(20),ceco.cost_codigo) + '-' + CONVERT(VARCHAR(20),empr.comp_nombre) ceco, 
		par1.parm_codigo mes,
		par1.parm_descripcion descrip_mes, 
		CASE LEN(par1.parm_codigo)
			WHEN 1 THEN  CAST(per.peri_ano AS VARCHAR(4)) + '.0' + par1.parm_codigo
			ELSE CAST(per.peri_ano AS VARCHAR(4)) + '.' + par1.parm_codigo
		END ano_mes,
		par.parm_codigo moneda, 
		sal.sali_valor valor, 
		sal.[sali_tipo], 
		pers.[pers_usudom],
		prit.prit_consecutivo iditem,
		pr.prod_consecutivo idprod
FROM [dbo].[GE_TSALIDAPRESUPUESTO] sal

INNER JOIN [dbo].[GE_TPERIODOTRANSACCIONES] tra
ON sal.sali_periodo_transacc = tra.petr_consecutivo

INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] per
ON sal.sali_periodo = per.peri_consecutivo
INNER JOIN [dbo].[GE_TPERSONAS] pers
ON pers.pers_consecutivo = sal.sali_persona
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] prit
ON prit.prit_consecutivo = sal.sali_producto_item
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON pr.prod_consecutivo = prit.prit_producto
INNER JOIN [dbo].[GE_TCENTROSCOSTOS] ceco
ON sal.sali_centrocosto = ceco.cost_consecutivo
INNER JOIN [dbo].[GE_TCOMPANIAS] empr
ON empr.comp_consecutivo = ceco.cost_empresa
INNER JOIN [dbo].[GE_TPARAMETROS] par
ON par.parm_consecutivo = sal.sali_moneda
INNER JOIN [dbo].[GE_TCUENTAS] cuenta
ON prit.prit_cuenta = cuenta.cuen_consecutivo
INNER JOIN [dbo].[GE_TPARAMETROS] par1
ON sal.sali_mes = par1.parm_consecutivo
WHERE per.peri_activo = 1 
AND tra.petr_activo = 1












GO
/****** Object:  Table [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA](
	[dinf_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[dinf_periodo] [int] NOT NULL,
	[dinf_tipo] [varchar](50) NOT NULL,
	[dinf_producto] [int] NOT NULL,
	[dinf_producto_item] [int] NULL,
	[dinf_servidor] [int] NOT NULL,
	[dinf_valor] [decimal](18, 6) NOT NULL,
	[dinf_estado] [int] NOT NULL,
	[dinf_usuario] [varchar](50) NOT NULL,
	[dinf_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TDISTRIBUCIONINFRAESTRUCTURA] PRIMARY KEY CLUSTERED 
(
	[dinf_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_VLR_ITEMS_INFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VW_VLR_ITEMS_INFRAESTRUCTURA] AS
SELECT	dinf.[dinf_servidor], s.serv_nombre,it.prit_item, SUM(dinf.dinf_valor) valor_ind, sal.valor vlritem, porc_item.valor valor_total, 
		ROUND(((CAST(SUM(dinf.dinf_valor) AS DECIMAL(18,10))/CAST(porc_item.valor AS DECIMAL(18,10))) * sal.valor),0) vlritem_serv
FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] dinf
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
ON dinf.dinf_producto_item = it.prit_consecutivo
INNER JOIN
(SELECT iditem, SUM(VALOR) valor FROM [dbo].[VW_SALIDA_PRESUPUESTO] GROUP BY [iditem]) sal
ON sal.iditem = it.prit_consecutivo
INNER JOIN 
(SELECT dinf_producto_item, SUM(dinf_valor) valor FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
WHERE p.peri_activo = 1 /*AND p.peri_etapa='CSER'*/ AND dinf_estado = 1 AND dinf_tipo = 'INFRAESTRUCTURA' GROUP BY dinf_producto_item) porc_item
ON porc_item.dinf_producto_item = dinf.dinf_producto_item
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON s.serv_consecutivo = dinf.dinf_servidor
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = dinf.dinf_periodo
WHERE p.peri_activo = 1 /*AND p.peri_etapa='CSER'*/ AND  dinf.dinf_estado = 1 AND dinf.dinf_tipo = 'INFRAESTRUCTURA'
GROUP BY s.serv_nombre, dinf.[dinf_servidor], it.prit_consecutivo, it.prit_item, sal.valor, porc_item.valor



GO
/****** Object:  View [dbo].[VW_VLR_PROD_INFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VW_VLR_PROD_INFRAESTRUCTURA] AS
SELECT	s.serv_nombre, d.[dinf_servidor], 
		pr.prod_codigo, pr.[prod_consecutivo],
		d.dinf_valor vlrprod, serv.valor vlserv,
		ROUND((CAST(d.dinf_valor AS DECIMAL(18,10)) /CAST(porc.valor AS DECIMAL(18,10))) * CAST(serv.valor AS DECIMAL(18,10)),0) vlrservprod
FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON d.dinf_producto = pr.prod_consecutivo
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
ON pe.[peri_activo] = 1
/*AND pe.peri_etapa='CSER'*/

AND d.dinf_periodo = pe.peri_consecutivo
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON s.serv_consecutivo = d.dinf_servidor
INNER JOIN
(
SELECT dinf_servidor, SUM(dinf_valor) valor FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
WHERE p.peri_activo = 1 /*AND p.peri_etapa='CSER'*/ AND dinf_estado = 1 AND dinf_tipo = 'DIRECTO' GROUP BY dinf_servidor
) porc
ON porc.dinf_servidor = d.[dinf_servidor]
INNER JOIN
(
SELECT [dinf_servidor]
      ,SUM([vlritem_serv]) valor
  FROM [Medeski].[dbo].[VW_VLR_ITEMS_INFRAESTRUCTURA]
  GROUP BY [dinf_servidor]
) serv
ON serv.[dinf_servidor] = d.[dinf_servidor]
WHERE pe.peri_activo = 1 /*AND pe.peri_etapa='CSER'*/ AND pr.prod_activo = 1 AND d.dinf_tipo = 'DIRECTO'



GO
/****** Object:  View [dbo].[VW_VLR_GENTE_TECNICA_PROD]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




  CREATE view [dbo].[VW_VLR_GENTE_TECNICA_PROD] as
  select v.dinf_servidor,
		 v.serv_nombre, 
		 v.prod_codigo, 
		 v.prod_consecutivo,
		round((cast(v.vlrprod as decimal(18,10)) / cast(t.valor as decimal(18,10))) * cast(g.valor as decimal(18,10)),0) vlrt
  from [dbo].[vw_vlr_prod_infraestructura] v
  inner join
  (
    select dinf_servidor, sum(vlrprod) valor
	from [dbo].[vw_vlr_prod_infraestructura]
	group by dinf_servidor
  ) t
  on v.dinf_servidor = t.dinf_servidor
  inner join 
  (
	select [idservidor]
     ,sum([costo_distibucion]) valor
	from [dbo].[vw_gente_tecnica]
	where idservidor > 0
	group by idservidor
  )g
  on g.idservidor = v.dinf_servidor 
  group by v.dinf_servidor, v.serv_nombre, v.prod_codigo, v.prod_consecutivo, v.vlrprod, t.valor, g.valor


GO
/****** Object:  View [dbo].[VW_VLR_DATACENTER_PRD]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VW_VLR_DATACENTER_PRD] AS
SELECT [prod_consecutivo],[prod_codigo],
 ROUND((SUM([vlrt])/gente.valor * datacenter.valor),0) vlrdatacenter
FROM [dbo].[VW_VLR_GENTE_TECNICA_PROD]
CROSS JOIN
(
	SELECT SUM([vlrt]) valor
	FROM [dbo].[VW_VLR_GENTE_TECNICA_PROD]
)gente
CROSS JOIN
(
	SELECT SUM([COSTO_DISTIBUCION]) valor
	FROM [dbo].[VW_GENTE_TECNICA]
	WHERE idproducto > 0 AND [NOMBRE_PRODUCTO] = 'DATACENTER'
) datacenter
GROUP BY [prod_consecutivo],[prod_codigo], gente.valor, datacenter.valor
GO
/****** Object:  View [dbo].[VW_VLR_GA_GENTE_TECNICA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VW_VLR_GA_GENTE_TECNICA] AS
SELECT total.prod_codigo, total.prod_consecutivo, SUM(total.valor) valor
FROM
(SELECT [prod_consecutivo], [prod_codigo], SUM([vlrt]) valor
FROM VW_VLR_GENTE_TECNICA_PROD
GROUP BY [prod_consecutivo], [prod_codigo]
UNION ALL
SELECT [prod_consecutivo], [prod_codigo], SUM(vlrdatacenter) valor
FROM [dbo].[VW_VLR_DATACENTER_PRD]
GROUP BY [prod_consecutivo], [prod_codigo]

UNION ALL
SELECT t.IDPRODUCTO, t.NOMBRE_PRODUCTO,SUM([PORCENTAJE_DEDICACION] * [COSTO_COLABORADOR]) valor
  FROM [Medeski].[dbo].[VW_GENTE_TECNICA] t
  WHERE t.TIPO_DISTRIBUCION = 'Producto-Infraestructura' --AND t.NOMBRE_PRODUCTO <> 'DATACENTER'
  GROUP BY t.IDPRODUCTO, t.NOMBRE_PRODUCTO
) total
GROUP BY total.prod_codigo, total.prod_consecutivo
GO
/****** Object:  Table [dbo].[GE_TRELITEMSDATACENTERPROD]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TRELITEMSDATACENTERPROD](
	[drel_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[drel_periodo] [int] NOT NULL,
	[drel_item_datacenter] [int] NOT NULL,
	[drel_producto] [int] NOT NULL,
	[drel_activo] [int] NOT NULL,
	[drel_usuario] [varchar](50) NOT NULL,
	[drel_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TRELITEMSDATACENTERPROD] PRIMARY KEY CLUSTERED 
(
	[drel_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_VLR_ITEMS_DATACENTER]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_VLR_ITEMS_DATACENTER] AS
SELECT dinf.[dinf_servidor], s.serv_nombre, rel.drel_item_datacenter, prit.prit_item, SUM(dinf_valor) valor
FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] dinf
INNER JOIN [dbo].[GE_TRELITEMSDATACENTERPROD] rel
ON dinf.dinf_periodo = rel.drel_periodo
AND dinf.dinf_producto = rel.drel_producto
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON s.[serv_consecutivo] =  dinf.dinf_servidor
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] prit
ON prit.prit_consecutivo = rel.drel_item_datacenter
INNER JOIN 
(SELECT pp.[peri_consecutivo] FROM [dbo].[GE_TPERIODOPRESUPUESTO] pp WHERE pp.[peri_activo] = 1 /*AND pp.peri_etapa='CSER'*/ ) ppto
ON ppto.[peri_consecutivo] = dinf.dinf_periodo
WHERE dinf.[dinf_estado] = 1 AND dinf.dinf_tipo = 'DIRECTO' AND rel.drel_activo = 1
GROUP BY dinf.[dinf_servidor], s.serv_nombre, rel.drel_item_datacenter, prit.prit_item


GO
/****** Object:  View [dbo].[VW_VLR_SERV_DATACENTER]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_VLR_SERV_DATACENTER] AS
SELECT [dinf_servidor], [serv_nombre],v.[drel_item_datacenter], v.prit_item,
ROUND(((CAST(v.[valor] AS DECIMAL(18,10))/CAST(t.valor AS DECIMAL(18,10))) * sal.valor),0) vlritemdatacenter
FROM [dbo].[VW_VLR_ITEMS_DATACENTER] v
INNER JOIN (
SELECT [drel_item_datacenter], SUM(valor) valor
FROM [Medeski].[dbo].[VW_VLR_ITEMS_DATACENTER]
GROUP BY [drel_item_datacenter] ) t
ON v.[drel_item_datacenter] = t.[drel_item_datacenter]
INNER JOIN
(SELECT iditem, SUM(VALOR) valor FROM [dbo].[VW_SALIDA_PRESUPUESTO] GROUP BY [iditem]) sal
ON sal.iditem = v.drel_item_datacenter
GROUP BY [dinf_servidor], [serv_nombre], v.[drel_item_datacenter], v.prit_item, v.[valor], t.valor, sal.valor
GO
/****** Object:  View [dbo].[VW_VLR_PROD_DATACENTER]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


 CREATE VIEW [dbo].[VW_VLR_PROD_DATACENTER] AS
 SELECT t.dinf_servidor, t.serv_nombre, pr.dinf_producto, pr.prod_codigo,
 ROUND(((CAST(pr.dinf_valor AS DECIMAL(18,10)) /CAST(serv.valor AS DECIMAL(18,10))) * CAST(t.valor AS DECIMAL(18,10))), 0) vlrprd
 FROM
(SELECT dinf_servidor, d.dinf_producto, dinf_valor, pr.prod_codigo FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON d.dinf_producto = pr.prod_consecutivo
WHERE p.peri_activo = 1 /*AND p.peri_etapa='CSER'*/ AND dinf_estado = 1 AND dinf_tipo = 'DIRECTO') pr
INNER JOIN
(SELECT dinf_servidor, SUM(dinf_valor) valor FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
WHERE p.peri_activo = 1 /*AND p.peri_etapa='CSER'*/ AND dinf_estado = 1 AND dinf_tipo = 'DIRECTO' GROUP BY dinf_servidor)serv
ON pr.dinf_servidor = serv.dinf_servidor
INNER JOIN
(SELECT [dinf_servidor], [serv_nombre],SUM([vlritemdatacenter]) valor
  FROM [Medeski].[dbo].[VW_VLR_SERV_DATACENTER]
  GROUP BY [dinf_servidor], [serv_nombre])t
ON t.dinf_servidor = pr.dinf_servidor
WHERE serv.valor > 0

GO
/****** Object:  View [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS] AS
SELECT 'INFRESTRUCTURA' Item,SUM([vlrservprod]) Total, 'INFRAESTRUCTURA' Tipo
FROM [dbo].[VW_VLR_PROD_INFRAESTRUCTURA]
WHERE prod_codigo IN ('SERVICE DESK', 'DYALOGO')
UNION ALL
SELECT 'DATACENTER' Item, SUM([vlrprd]) Total, 'DATACENTER' Tipo
FROM [dbo].[VW_VLR_PROD_DATACENTER]
WHERE prod_codigo IN ('SERVICE DESK', 'DYALOGO')
UNION ALL
SELECT 'GENTE TECNICA INFRAESTRUCTURA', SUM([vlrt]) Total, 'GENTE' Tipo
FROM [dbo].[VW_VLR_GENTE_TECNICA_PROD]
WHERE prod_codigo IN ('SERVICE DESK', 'DYALOGO')
UNION ALL
SELECT  'GENTE TECNICA DATACENTER', SUM([vlrdatacenter]) Total, 'GENTE' Tipo
FROM [dbo].[VW_VLR_DATACENTER_PRD]
WHERE prod_codigo IN ('SERVICE DESK', 'DYALOGO')
UNION ALL
SELECT 'GENTE', SUM(g.gent_costo_colaborador) Total, 'GA' Tipo
FROM [dbo].[GE_TGENTE] g
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON g.gent_periodo = p.peri_consecutivo
AND p.peri_activo = 1
/*AND p.peri_etapa='CSER'*/
AND g.[gent_estado] = 1
INNER JOIN [dbo].[GE_TCENTROSCOSTOS] c
ON c.cost_consecutivo = g.gent_ccostos
WHERE c.cost_codigo IN ('31051106')
UNION ALL
SELECT item, SUM(valor), 'SOFTWARE' Tipo
FROM [dbo].[VW_SALIDA_PRESUPUESTO]
WHERE producto IN ('SERVICE DESK', 'DYALOGO')
GROUP BY item



GO
/****** Object:  Table [dbo].[GE_TDISTRIBUCIONMASPROCESOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDISTRIBUCIONMASPROCESOS](
	[dmas_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[dmas_periodo] [int] NOT NULL,
	[dmas_producto] [int] NOT NULL,
	[dmas_valor] [decimal](18, 6) NOT NULL,
	[dmas_usuario] [varchar](50) NOT NULL,
	[dmas_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TDISTRIBUCIONMASPROCESOS] PRIMARY KEY CLUSTERED 
(
	[dmas_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_VLR_DISTRIB_MAS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VW_VLR_DISTRIB_MAS] AS
SELECT pr.prod_consecutivo, pr.prod_codigo, dist.[dmas_valor] casos, 
	   CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) porc,
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * soft.Total),0) software,
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * infr.Total),0) infraestructura,
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * datac.Total),0) datacenter,
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * gente.Total),0) gente,
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * ga.Total),0) ga,
	   0 cdm,
	   0 procesos,
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * soft.Total),0) +
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * infr.Total),0) +
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * datac.Total),0) +
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * gente.Total),0) +
	   ROUND((CAST(dist.[dmas_valor] AS DECIMAL(18,6))/CAST(total.valor AS DECIMAL(18,6)) * ga.Total),0) total
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONMASPROCESOS] dist
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
ON pe.peri_consecutivo = dist.dmas_periodo
AND pe.peri_activo = 1
/*AND pe.peri_etapa='CSER'*/
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON pr.prod_consecutivo = dist.dmas_producto
CROSS JOIN
(SELECT SUM(dmas_valor) valor FROM [Medeski].[dbo].[GE_TDISTRIBUCIONMASPROCESOS] mas 
INNER JOIN  [dbo].[GE_TPERIODOPRESUPUESTO] pp
ON pp.peri_consecutivo = mas.dmas_periodo
AND pp.peri_activo = 1
/*AND pp.peri_etapa='CSER'*/
) total
CROSS JOIN
(SELECT SUM(Total) Total
FROM [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS]
WHERE Tipo = 'SOFTWARE') soft
CROSS JOIN
(SELECT SUM(Total) Total
FROM [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS]
WHERE Tipo = 'INFRAESTRUCTURA') infr
CROSS JOIN
(SELECT SUM(Total) Total
FROM [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS]
WHERE Tipo = 'DATACENTER') datac
CROSS JOIN
(SELECT SUM(Total) Total
FROM [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS]
WHERE Tipo = 'GENTE') gente
CROSS JOIN
(SELECT SUM(Total) Total
FROM [dbo].[VW_VLR_ENCABEZ_DISTRIB_MAS]
WHERE Tipo = 'GA') ga





GO
/****** Object:  Table [dbo].[GE_TDISTRIBUCIONINTERMEDIOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS](
	[dint_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[dint_periodo] [int] NOT NULL,
	[dint_producto_intermedio] [int] NOT NULL,
	[dint_item_intermedio] [int] NOT NULL,
	[dint_producto_directo] [int] NOT NULL,
	[dint_valor] [decimal](18, 6) NOT NULL,
	[dint_estado] [int] NOT NULL,
	[dint_usuario] [varchar](30) NOT NULL,
	[dint_fecha] [datetime] NOT NULL,
	[dint_valor_asignado] [decimal](18, 6) NULL,
	[dint_valor_item] [decimal](18, 6) NULL,
 CONSTRAINT [PK_GE_TDISTRIBUCIONINTERMEDIOS] PRIMARY KEY CLUSTERED 
(
	[dint_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_VLR_DISTRIB_INTERM]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VW_VLR_DISTRIB_INTERM] AS
SELECT	pr.prod_consecutivo idprod, 
		pr.prod_codigo producto, 
		it.prit_consecutivo iditem,
		it.[prit_item] item, 
		pr1.prod_consecutivo idproddist, 
		pr1.prod_codigo producto_distrib,
		ROUND((CAST(dinf.dint_valor AS DECIMAL(18,10)) * total.vlr),0) vlrdistrib
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONINTERMEDIOS] dinf
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON dinf.[dint_producto_directo] = pr.prod_consecutivo
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
ON dinf.[dint_item_intermedio] = it.[prit_consecutivo]
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
ON pe.[peri_consecutivo] = dinf.[dint_periodo]
AND pe.[peri_activo] = 1
/*AND pe.peri_etapa='CSER'*/
INNER JOIN [dbo].[GE_TPRODUCTOS] pr1
ON dinf.[dint_producto_intermedio] = pr1.[prod_consecutivo]
INNER JOIN
(SELECT [iditem], [item], SUM(valor) vlr
FROM  [dbo].[VW_SALIDA_PRESUPUESTO] GROUP BY [iditem], [item]) total
ON total.[iditem] = it.[prit_consecutivo]
WHERE pr.[prod_redistribuir] = 1

GO
/****** Object:  View [dbo].[VW_VLR_PRESUPUESTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_VLR_PRESUPUESTO] AS
SELECT idprod, producto,[iditem], [item], SUM(valor) vlr
FROM  [dbo].[VW_SALIDA_PRESUPUESTO] 
GROUP BY idprod, producto, [iditem], [item]
GO
/****** Object:  View [dbo].[VW_VLR_DEDICACION_GENTE]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[VW_VLR_DEDICACION_GENTE] AS
SELECT	pr.[prod_consecutivo],
		pr.prod_codigo,
		par.[parm_descripcion] as pers_nombre_area,
		SUM(g.[gent_costo_colaborador] * d.[dper_valor]) valor
  FROM [Medeski].[dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA] d
  INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
  ON d.[dper_periodo] = pe.peri_consecutivo
  AND pe.peri_activo = 1
  INNER JOIN [dbo].[GE_TPERSONAS] per
  ON per.[pers_consecutivo] = d.dper_persona
  
  INNER JOIN [dbo].[GE_TPARAMETROS] par
  ON per.[pers_nombre_area] = par.parm_consecutivo
  
  INNER JOIN [dbo].[GE_TGENTE] g
  ON g.[gent_persona] = per.pers_consecutivo
    AND g.gent_periodo = pe.peri_consecutivo
  INNER JOIN [dbo].[GE_TPRODUCTOS] pr
  ON d.dper_producto = pr.[prod_consecutivo]
  WHERE d.dper_tipo = 'Producto' AND d.dper_estado = 1 AND g.[gent_estado] = 1
  GROUP BY pr.[prod_consecutivo], pr.prod_codigo, par.[parm_descripcion]




GO
/****** Object:  View [dbo].[VW_VLR_REDISTRIBUCION]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VW_VLR_REDISTRIBUCION] AS
SELECT	pr.prod_consecutivo idprod,
		pr.prod_codigo producto,
		presup.item,
		ROUND((CAST(ISNULL(presup.vlr,0) AS DECIMAL(18,0))),0) vlr_lic,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,0))),0) vlr_lic_interm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_ga,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_softw,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_gente,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_ga_operaciones,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_ga_gtecnica,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_ga_desarrollo,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_cdm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_procesos,
		ROUND((CAST(ISNULL(presup.vlr,0) AS DECIMAL(18,0))),0) total
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].[VW_VLR_PRESUPUESTO] presup
ON presup.idprod = pr.prod_consecutivo
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1 AND pr.[prod_intermedio] = 0
UNION
SELECT	prred.prod_consecutivo idprod,
		prred.prod_codigo producto,
		interm.item item_inter,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,0))),0) vlr_lic,
		ROUND((CAST(ISNULL(interm.vlrdistrib,0) AS DECIMAL(18,0))),0) vlr_lic_interm,
		ROUND((CAST(ISNULL(inf.valor,0) AS DECIMAL(18,10))),0) vlr_infr,
		ROUND((CAST(ISNULL(data.valor,0) AS DECIMAL(18,10))),0) vlr_datac,
		ROUND((CAST(ISNULL(dmas.ga,0) AS DECIMAL(18,10))),0) vlr_mas_ga,
		ROUND((CAST(ISNULL(dmas.software,0) AS DECIMAL(18,10))),0) vlr_mas_softw,
		ROUND((CAST(ISNULL(dmas.infraestructura,0) AS DECIMAL(18,10))),0) vlr_mas_infr,
		ROUND((CAST(ISNULL(dmas.datacenter,0) AS DECIMAL(18,10))),0) vlr_mas_datac,
		ROUND((CAST(ISNULL(dmas.gente,0) AS DECIMAL(18,10))),0) vlr_mas_gente,
		ROUND((CAST(ISNULL(dedop.valor,0) AS DECIMAL(20,10))),0) vlr_ga_operaciones,
		ROUND((CAST(ISNULL(dedgtec.valor,0) AS DECIMAL(18,10))),0) vlr_ga_gtecnica,
		ROUND((CAST(ISNULL(deddes.valor,0) AS DECIMAL(20,10))),0) vlr_ga_desarrollo,
		ROUND((CAST(ISNULL(dmas.cdm,0) AS DECIMAL(18,10))),0) vlr_mas_cdm,
		ROUND((CAST(ISNULL(dmas.procesos,0) AS DECIMAL(18,10))),0) vlr_mas_procesos,
		(ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,0))),0) +
		ROUND((CAST(ISNULL(interm.vlrdistrib,0) AS DECIMAL(18,0))),0) +
		ROUND((CAST(ISNULL(inf.valor,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(data.valor,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dmas.ga,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dmas.software,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dmas.infraestructura,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dmas.datacenter,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dmas.gente,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dedop.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dedgtec.valor,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(deddes.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.cdm,0) AS DECIMAL(18,10))),0) +
		ROUND((CAST(ISNULL(dmas.procesos,0) AS DECIMAL(18,10))),0)) total
FROM
(SELECT pr.[prod_consecutivo]
		,pr.[prod_codigo]
FROM [dbo].[GE_TPRODUCTOS] pr
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1) prred
LEFT OUTER JOIN
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		dint.iditem,
		dint.item,
		dint.vlrdistrib
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN  
(
SELECT dinte.idprod, dinte.iditem, item,vlrdistrib  FROM [Medeski].[dbo].[VW_VLR_DISTRIB_INTERM] dinte
INNER JOIN
(SELECT idprod, MIN(iditem) iditem
FROM [Medeski].[dbo].[VW_VLR_DISTRIB_INTERM]
GROUP BY idprod) t
ON dinte.iditem = t.iditem
AND dinte.idprod = t.idprod
)dint
ON dint.idprod = pr.prod_consecutivo
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1
)interm
ON interm.prod_consecutivo = prred.prod_consecutivo
LEFT OUTER JOIN
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		inf.[vlrservprod] valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_INFRAESTRUCTURA inf
ON inf.prod_consecutivo = pr.prod_consecutivo
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1
)inf
ON inf.prod_consecutivo = prred.prod_consecutivo
LEFT OUTER JOIN
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		inf.[vlrprd] valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_DATACENTER inf
ON inf.dinf_producto = pr.prod_consecutivo
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1
)data
ON data.prod_consecutivo = prred.prod_consecutivo
LEFT OUTER JOIN
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		d.cdm,
		d.[datacenter],
		d.[software],
		d.infraestructura,
		d.ga,
		d.gente,
		d.procesos
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d
ON d.prod_consecutivo = pr.prod_consecutivo
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1
)dmas
ON dmas.prod_consecutivo = prred.prod_consecutivo

LEFT OUTER JOIN
(
SELECT [prod_consecutivo]
      ,[prod_codigo]
      ,[pers_nombre_area]
      ,[valor]
FROM [Medeski].[dbo].[VW_VLR_DEDICACION_GENTE]
WHERE [pers_nombre_area] = 'DESARROLLO'
)deddes
ON deddes.[prod_consecutivo] = prred.prod_consecutivo

LEFT OUTER JOIN
(
SELECT [prod_consecutivo]
      ,[prod_codigo]
      ,[pers_nombre_area]
      ,[valor]
FROM [Medeski].[dbo].[VW_VLR_DEDICACION_GENTE]
WHERE [pers_nombre_area] = 'OPERACIONES'
)dedop
ON dedop.[prod_consecutivo] = prred.prod_consecutivo

LEFT OUTER JOIN
(/*SELECT [prod_codigo]
      ,[prod_consecutivo]
      ,SUM([vlrt]) valor
  FROM [Medeski].[dbo].[VW_VLR_GENTE_TECNICA_PROD]
  GROUP BY [prod_codigo],[prod_consecutivo]*/
  SELECT prod_codigo, prod_consecutivo, valor FROM VW_VLR_GA_GENTE_TECNICA
) dedgtec
ON dedgtec.prod_consecutivo = prred.prod_consecutivo
UNION
SELECT	pr.prod_consecutivo idprod,
		pr.prod_codigo producto,
		dint.item item_interm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,0))),0) vlr_lic,
		ROUND((CAST(ISNULL(dint.vlrdistrib,0) AS DECIMAL(18,0))),0) vlr_lic_interm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_ga,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_softw,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_gente,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_ga_operaciones,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_ga_gtecnica,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_ga_desarrollo,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_cdm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(18,10))),0) vlr_mas_procesos,
		ROUND((CAST(ISNULL(dint.vlrdistrib,0) AS DECIMAL(18,0))),0) total
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN  
(
SELECT dinte.idprod, dinte.iditem, item,vlrdistrib  FROM [Medeski].[dbo].[VW_VLR_DISTRIB_INTERM] dinte
INNER JOIN
((SELECT idprod, iditem
FROM [Medeski].[dbo].[VW_VLR_DISTRIB_INTERM]
GROUP BY idprod, iditem)
EXCEPT
(SELECT idprod, MIN(iditem) iditem
FROM [Medeski].[dbo].[VW_VLR_DISTRIB_INTERM]
GROUP BY idprod)) t
ON dinte.iditem = t.iditem
AND dinte.idprod = t.idprod
)dint
ON dint.idprod = pr.prod_consecutivo
WHERE pr.prod_activo = 1 AND pr.prod_redistribuir = 1


GO
/****** Object:  View [dbo].[VW_PRODUCTOS_DIRECTOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[VW_PRODUCTOS_DIRECTOS] AS
SELECT  pr.prod_consecutivo, pr.prod_codigo, pr.prod_responsable, pr.prod_driver1, pr.prod_activo, pa.parm_descripcion, pa1.parm_descripcion servicio, pa1.parm_consecutivo idservicio
  FROM [Medeski].[dbo].[GE_TPRODUCTOS] pr
  INNER JOIN [dbo].[GE_TPARAMETROS] pa
  ON pr.prod_componente = pa.parm_consecutivo
  LEFT OUTER JOIN [dbo].[GE_TPARAMETROS] pa1
  ON pr.prod_serv_venta = pa1.parm_consecutivo
  WHERE pa.parm_descripcion = 'DIRECTO' AND pr.prod_intermedio = 0
  AND pr.[prod_redistribuir] = 0




GO
/****** Object:  View [dbo].[VW_VLR_PROD_DATACENTER_MODIF]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_VLR_PROD_DATACENTER_MODIF] AS 
SELECT total.prod_codigo, total.prod_consecutivo dinf_producto, SUM(total.valor) [vlrprd]
FROM
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		SUM(ISNULL(inf.[vlrprd],0)) valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_DATACENTER inf
ON inf.dinf_producto = pr.prod_consecutivo
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON p.[prod_consecutivo] = pr.[prod_consecutivo]
AND inf.[dinf_producto] = p.[prod_consecutivo]
WHERE pr.prod_activo = 1
GROUP BY pr.prod_consecutivo, pr.prod_codigo
UNION ALL
SELECT	prn.prod_consecutivo,
		prn.prod_codigo,
		SUM(ISNULL(inf.[vlrprd],0)) valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_DATACENTER inf
ON inf.[dinf_producto] = pr.prod_consecutivo
CROSS JOIN
(SELECT pr.prod_consecutivo, pr.prod_codigo FROM [dbo].[GE_TPRODUCTOS] pr WHERE pr.prod_codigo = 'ADMINISTRACION USUARIOS/AUDITORIA') prn
WHERE pr.prod_codigo = 'ADSECURITY'
GROUP BY prn.prod_consecutivo, prn.prod_codigo
)total
GROUP BY total.prod_codigo, total.prod_consecutivo
GO
/****** Object:  View [dbo].[VW_VLR_PROD_INFRAESTRUCTURA_MODIF]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_VLR_PROD_INFRAESTRUCTURA_MODIF] AS 
SELECT total.prod_codigo, total.prod_consecutivo, SUM(total.valor) [vlrservprod]
FROM
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		SUM(ISNULL(inf.[vlrservprod],0)) valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_INFRAESTRUCTURA inf
ON inf.prod_consecutivo = pr.prod_consecutivo
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON p.[prod_consecutivo] = pr.[prod_consecutivo]
AND inf.prod_consecutivo = p.[prod_consecutivo]
WHERE pr.prod_activo = 1
GROUP BY pr.prod_consecutivo, pr.prod_codigo
UNION ALL
SELECT	prn.prod_consecutivo,
		prn.prod_codigo,
		SUM(ISNULL(inf.[vlrservprod],0)) valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_INFRAESTRUCTURA inf
ON inf.prod_consecutivo = pr.prod_consecutivo
CROSS JOIN
(SELECT pr.prod_consecutivo, pr.prod_codigo FROM [dbo].[GE_TPRODUCTOS] pr WHERE pr.prod_codigo = 'ADMINISTRACION USUARIOS/AUDITORIA') prn
WHERE pr.prod_codigo = 'ADSECURITY'
GROUP BY prn.prod_consecutivo, prn.prod_codigo
)total
GROUP BY total.prod_codigo, total.prod_consecutivo
GO
/****** Object:  View [dbo].[VW_PERIODO_ACTIVO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_PERIODO_ACTIVO] AS
SELECT *
FROM [dbo].[GE_TPERIODOPRESUPUESTO]
WHERE peri_activo = 1


GO
/****** Object:  View [dbo].[VW_VLR_GA_GENTE_TECNICA_GSUITE]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[VW_VLR_GA_GENTE_TECNICA_GSUITE] AS
SELECT  pr.prod_codigo, pr.prod_consecutivo, (costo_total - vlrcosto) valor
FROM
(
(SELECT pr.prod_codigo, pr.prod_consecutivo FROM dbo.ge_tproductos pr WHERE pr.prod_codigo = 'G-SUITE') pr
 CROSS JOIN 
(SELECT SUM(g.gent_costo_colaborador) costo_total 
	FROM dbo.GE_TGENTE g
	INNER JOIN [dbo].[VW_PERIODO_ACTIVO] p ON g.gent_periodo = p.peri_consecutivo
	INNER JOIN [dbo].[GE_TPERSONAS] pers ON g.gent_persona = pers.pers_consecutivo
	
	WHERE [pers].pers_identificacion = 94526142) costo_serv
	CROSS JOIN
	(SELECT SUM(COSTO_DISTIBUCION) vlrcosto
		FROM [dbo].[VW_GENTE_TECNICA]
		WHERE NUMERO_CEDULA = 94526142) costo
)
GO
/****** Object:  View [dbo].[VW_VLR_GA_GENTE_TECNICA_MODIF]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
/*SELECT TOP (1000) [prod_codigo]
      ,[prod_consecutivo]
      ,[valor]
  FROM [Medeski].[dbo].[VW_VLR_GA_GENTE_TECNICA] t
  WHERE t.prod_codigo IN ('ADSECURITY', 'ADMINISTRACION USUARIOS/AUDITORIA');*/

CREATE VIEW [dbo].[VW_VLR_GA_GENTE_TECNICA_MODIF] AS
SELECT total.prod_codigo, total.prod_consecutivo, SUM(total.valor) valor
FROM
(SELECT [prod_codigo]
      ,[prod_consecutivo]
      ,[valor]
FROM [dbo].[VW_VLR_GA_GENTE_TECNICA] t
UNION ALL
(SELECT prn.[prod_codigo]
      ,prn.[prod_consecutivo]
      ,t.[valor]
FROM [dbo].[VW_VLR_GA_GENTE_TECNICA] t
CROSS JOIN
(SELECT pr.prod_consecutivo, pr.prod_codigo FROM [dbo].[GE_TPRODUCTOS] pr WHERE pr.prod_codigo = 'ADMINISTRACION USUARIOS/AUDITORIA') prn
WHERE t.prod_codigo IN ('ADSECURITY'))

UNION ALL
SELECT [prod_codigo], [prod_consecutivo], [valor] FROM [dbo].[VW_VLR_GA_GENTE_TECNICA_GSUITE] 
) total
GROUP BY total.prod_codigo, total.prod_consecutivo
GO
/****** Object:  View [dbo].[VW_PRODUCTOS_SIN_PPTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_PRODUCTOS_SIN_PPTO]
AS
SELECT VW_PRODUCTOS_DIRECTOS.prod_codigo, GE_TPRODUCTOSITEMS.prit_item, GE_TPERSONAS.pers_usudom
FROM VW_PRODUCTOS_DIRECTOS
JOIN GE_TPRODUCTOSITEMS ON GE_TPRODUCTOSITEMS.prit_producto = VW_PRODUCTOS_DIRECTOS.prod_consecutivo
JOIN GE_TPERSONAS ON GE_TPERSONAS.pers_consecutivo = VW_PRODUCTOS_DIRECTOS.prod_responsable

EXCEPT
(
	SELECT VW_SALIDA_PRESUPUESTO.producto, VW_SALIDA_PRESUPUESTO.item, VW_SALIDA_PRESUPUESTO.pers_usudom
	FROM VW_SALIDA_PRESUPUESTO	
)
GO
/****** Object:  View [dbo].[VW_CS_VLR_SERV_ITEM_INFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_CS_VLR_SERV_ITEM_INFRAESTRUCTURA]
AS
SELECT	s.serv_nombre, d.[dinf_servidor], 
		pr.prod_codigo, pr.[prod_consecutivo],
		d.dinf_valor vlrprod, serv.valor vlserv,
		serv.prit_item,
		ROUND((CAST(d.dinf_valor AS DECIMAL(18,10)) /CAST(porc.valor AS DECIMAL(18,10))) * CAST(serv.valor AS DECIMAL(18,10)),0) vlritem
FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON d.dinf_producto = pr.prod_consecutivo
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
ON pe.[peri_activo] = 1
AND d.dinf_periodo = pe.peri_consecutivo
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON s.serv_consecutivo = d.dinf_servidor
INNER JOIN
(
SELECT dinf_servidor, SUM(dinf_valor) valor FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
WHERE p.peri_activo = 1 AND dinf_estado = 1 AND dinf_tipo = 'DIRECTO' GROUP BY dinf_servidor
) porc
ON porc.dinf_servidor = d.[dinf_servidor]
INNER JOIN
(
SELECT [dinf_servidor]
	   ,[prit_item]
      ,SUM([vlritem_serv]) valor
  FROM [dbo].[VW_VLR_ITEMS_INFRAESTRUCTURA]
  GROUP BY [dinf_servidor],[prit_item]
) serv
ON serv.[dinf_servidor] = d.[dinf_servidor]
WHERE pe.peri_activo = 1 AND pr.prod_activo = 1 AND d.dinf_tipo = 'DIRECTO'
GO
/****** Object:  View [dbo].[VW_CS_VLR_SERV_ITEM_DATACENTER]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE VIEW [dbo].[VW_CS_VLR_SERV_ITEM_DATACENTER]
 AS
 SELECT t.dinf_servidor, t.serv_nombre, pr.dinf_producto, pr.prod_codigo,t.[prit_item],
 ROUND(((CAST(pr.dinf_valor AS DECIMAL(18,10)) /CAST(serv.valor AS DECIMAL(18,10))) * CAST(t.valor AS DECIMAL(18,10))), 0) vlrprd
 FROM
(SELECT dinf_servidor, d.dinf_producto, dinf_valor, pr.prod_codigo FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON d.dinf_producto = pr.prod_consecutivo
WHERE p.peri_activo = 1  AND dinf_estado = 1 AND dinf_tipo = 'DIRECTO') pr
INNER JOIN
(SELECT dinf_servidor, SUM(dinf_valor) valor FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] d
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_consecutivo = d.dinf_periodo
WHERE p.peri_activo = 1  AND dinf_estado = 1 AND dinf_tipo = 'DIRECTO' GROUP BY dinf_servidor)serv
ON pr.dinf_servidor = serv.dinf_servidor
INNER JOIN
(SELECT [dinf_servidor], [serv_nombre], [prit_item], SUM([vlritemdatacenter]) valor
  FROM [dbo].[VW_VLR_SERV_DATACENTER]
  GROUP BY [dinf_servidor], [serv_nombre],[prit_item] )t
ON t.dinf_servidor = pr.dinf_servidor
WHERE serv.valor > 0
GO
/****** Object:  View [dbo].[VW_CS_VLR_MAS_GA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_CS_VLR_MAS_GA] AS
SELECT v.prod_consecutivo, v.prod_codigo, per.pers_apellido + ' ' +  per.pers_nombre persona,
      ROUND((CAST(v.porc AS DECIMAL(18,6)) * CAST(g.gent_costo_colaborador AS DECIMAL(18,6))),0) costo
FROM [dbo].[GE_TGENTE] g

INNER JOIN [dbo].[GE_TPERSONAS] per
ON g.gent_persona = per.pers_consecutivo

INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON g.gent_periodo = p.peri_consecutivo
AND p.peri_activo = 1
AND g.[gent_estado] = 1
INNER JOIN [dbo].[GE_TCENTROSCOSTOS] c
ON c.cost_consecutivo = g.gent_ccostos
CROSS JOIN [dbo].[VW_VLR_DISTRIB_MAS] v
WHERE c.cost_codigo IN ('31051106')
GO
/****** Object:  View [dbo].[VW_CS_VLR_MAS_SOFTWARE]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_CS_VLR_MAS_SOFTWARE] AS
SELECT	v.prod_consecutivo,  v.prod_codigo, it.item
		,ROUND((CAST(it.valor AS DECIMAL(18,6)) * CAST(v.porc AS DECIMAL(18,6))),0) valor
FROM
(SELECT item, SUM(valor) valor
FROM [dbo].[VW_SALIDA_PRESUPUESTO]
WHERE producto IN ('SERVICE DESK', 'DYALOGO')
GROUP BY item
) it
CROSS JOIN [dbo].[VW_VLR_DISTRIB_MAS] v
GO
/****** Object:  View [dbo].[VW_CS_VLR_MAS_INFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[VW_CS_VLR_MAS_INFRAESTRUCTURA] AS
SELECT v.prod_codigo producto,
	   v.prod_consecutivo idproducto,
	   [serv_nombre]
      ,[dinf_servidor]
      ,t.[prod_codigo]
      ,t.[prod_consecutivo]
      ,ROUND((CAST(t.[vlrservprod] AS DECIMAL(18,6)) * CAST(v.porc AS DECIMAL(18,6))), 0) valor 
  FROM [dbo].[VW_VLR_PROD_INFRAESTRUCTURA] t
  CROSS JOIN [dbo].[VW_VLR_DISTRIB_MAS] v
  WHERE t.prod_codigo IN ('SERVICE DESK', 'DYALOGO')
GO
/****** Object:  View [dbo].[VW_CS_VLR_MAS_DATACENTER]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[VW_CS_VLR_MAS_DATACENTER] AS
SELECT v.prod_codigo producto,
	   v.prod_consecutivo idproducto,
	   d.[dinf_servidor]
      ,d.[serv_nombre]
      ,d.[dinf_producto]
      ,d.[prod_codigo]
	  ,ROUND((CAST(d.[vlrprd] AS DECIMAL(18,6)) * CAST(v.porc AS DECIMAL(18,6))), 0) valor 
  FROM [Medeski].[dbo].[VW_VLR_PROD_DATACENTER] d
  CROSS JOIN [dbo].[VW_VLR_DISTRIB_MAS] v
  WHERE d.prod_codigo IN ('DYALOGO','SERVICE DESK')
GO
/****** Object:  View [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_INF]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/

CREATE VIEW [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_INF] AS
SELECT v.prod_codigo producto,
	   v.prod_consecutivo idproducto,
	   t.[dinf_servidor]
      ,t.[serv_nombre]
      ,t.[prod_codigo]
      ,t.[prod_consecutivo]
	  ,ROUND((CAST(t.[vlrt] AS DECIMAL(18,6)) * CAST(v.porc AS DECIMAL(18,6))), 0) valor 
  FROM [dbo].[VW_VLR_GENTE_TECNICA_PROD] t
  CROSS JOIN [dbo].[VW_VLR_DISTRIB_MAS] v
  WHERE t.prod_codigo IN ('SERVICE DESK', 'DYALOGO')
  
GO
/****** Object:  View [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_DATAC]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_DATAC] AS
SELECT v.prod_codigo producto,
	   v.prod_consecutivo idproducto
      ,t.[prod_codigo]
      ,t.[prod_consecutivo]
	  ,ROUND((CAST(t.[vlrdatacenter] AS DECIMAL(18,6)) * CAST(v.porc AS DECIMAL(18,6))), 0) valor 
  FROM [dbo].[VW_VLR_DATACENTER_PRD] t
  CROSS JOIN [dbo].[VW_VLR_DISTRIB_MAS] v
  WHERE t.prod_codigo IN ('SERVICE DESK', 'DYALOGO')
GO
/****** Object:  View [dbo].[VW_CS_VLR_GA_GTECNICA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VW_CS_VLR_GA_GTECNICA] AS
SELECT	serv.prod_consecutivo,serv.prod_codigo,
		gente.IDSERVIDOR, gente.NOMBRE_SERVIDOR, 
		gente.persona, 
		(gente.valor * serv_prod.valor) valor
		
FROM
(
SELECT t.IDSERVIDOR, t.NOMBRE_SERVIDOR, t.[NOMBRE] persona, 
g.[gent_porcentaje_manual_dedicacion], (g.[gent_porcentaje_manual_dedicacion] * [PORCENTAJE_DEDICACION] * [COSTO_COLABORADOR]) valor
FROM [dbo].[VW_GENTE_TECNICA] t

INNER JOIN  [dbo].[GE_TPERSONAS] per
ON t.numero_cedula = per.pers_identificacion

INNER JOIN [dbo].[GE_TGENTE] g
ON g.gent_persona = per.pers_consecutivo

INNER JOIN [dbo].[VW_PERIODO_ACTIVO] p
ON p.[peri_consecutivo] = g.[gent_periodo]
WHERE t.TIPO_DISTRIBUCION = 'Infraestructura'
) gente
INNER JOIN 
(SELECT t.dinf_servidor, t.serv_nombre, [prod_consecutivo], [prod_codigo], [vlrt] valor
FROM VW_VLR_GENTE_TECNICA_PROD t) serv
ON gente.IDSERVIDOR = serv.dinf_servidor
INNER JOIN
(
SELECT inf.prod_consecutivo, inf.prod_codigo, inf.dinf_servidor, inf.serv_nombre
      ,SUM([vlrprod]) valor
  FROM [dbo].[VW_VLR_PROD_INFRAESTRUCTURA] inf
  GROUP BY inf.prod_consecutivo, inf.prod_codigo, inf.dinf_servidor, inf.serv_nombre
) serv_prod
ON serv_prod.dinf_servidor = gente.IDSERVIDOR
AND serv.[prod_codigo] = serv_prod.prod_codigo

UNION ALL

SELECT  [prod_consecutivo],
		[prod_codigo],
		dinf_servidor,
		serv_nombre,
		'DATACENTER',
		ROUND((([vlrt])/gente.valor * datacenter.valor),0) valor
FROM [dbo].[VW_VLR_GENTE_TECNICA_PROD] gt
CROSS JOIN
(
	SELECT SUM([vlrt]) valor
	FROM [dbo].[VW_VLR_GENTE_TECNICA_PROD]
)gente
CROSS JOIN
(
	/*SELECT SUM([COSTO_DISTIBUCION]) valor
	FROM [dbo].[VW_GENTE_TECNICA]
	WHERE idproducto > 0 AND [NOMBRE_PRODUCTO] = 'DATACENTER'*/
	SELECT SUM([PORCENTAJE_DEDICACION] * [COSTO_COLABORADOR]) valor
  FROM [Medeski].[dbo].[VW_GENTE_TECNICA] t
  WHERE t.TIPO_DISTRIBUCION = 'Producto-Infraestructura' AND t.NOMBRE_PRODUCTO = 'DATACENTER'
  GROUP BY t.IDPRODUCTO, t.NOMBRE_PRODUCTO
) datacenter
--WHERE [prod_codigo] = 'UNOEE'

GO
/****** Object:  View [dbo].[VW_REPORTE_DISTRIB_INTERM_CS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_REPORTE_DISTRIB_INTERM_CS] AS
SELECT	pr.prod_consecutivo idprod, 
		pr.prod_codigo producto, 
		it.prit_consecutivo iditem,
		it.[prit_item] item, 
		pr1.prod_consecutivo idproddist, 
		pr1.prod_codigo producto_distrib,
		ROUND((CAST(dinf.dint_valor AS DECIMAL(18,10)) * total.vlr),0) vlrdistrib
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONINTERMEDIOS] dinf
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON dinf.[dint_producto_directo] = pr.prod_consecutivo
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
ON dinf.[dint_item_intermedio] = it.[prit_consecutivo]
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
ON pe.[peri_consecutivo] = dinf.[dint_periodo]
AND pe.[peri_activo] = 1
/*AND pe.peri_etapa='CSER'*/

INNER JOIN [dbo].[GE_TPRODUCTOS] pr1
ON dinf.[dint_producto_intermedio] = pr1.[prod_consecutivo]
INNER JOIN
(SELECT [iditem], [item], SUM(valor) vlr
FROM  [dbo].[VW_SALIDA_PRESUPUESTO] GROUP BY [iditem], [item]) total
ON total.[iditem] = it.[prit_consecutivo]






GO
/****** Object:  View [dbo].[VW_CS_VLR_GA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_CS_VLR_GA] AS
SELECT	pr.[prod_consecutivo],
		pr.prod_codigo,
		params.[parm_descripcion] pers_nombre_area,
		per.pers_nombres,
		(g.[gent_costo_colaborador] * d.[dper_valor]) valor
  FROM [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA] d
  INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
  ON d.[dper_periodo] = pe.peri_consecutivo
  AND pe.peri_activo = 1
  INNER JOIN [dbo].[GE_TPERSONAS] per
  ON per.[pers_consecutivo] = d.dper_persona
  
  INNER JOIN [dbo].[GE_TPARAMETROS] params
  ON params.[parm_consecutivo] = per.pers_nombre_area
  
  INNER JOIN [dbo].[GE_TGENTE] g
  ON g.[gent_persona] = per.pers_consecutivo
    AND g.gent_periodo = pe.peri_consecutivo
  INNER JOIN [dbo].[GE_TPRODUCTOS] pr
  ON d.dper_producto = pr.[prod_consecutivo]
  WHERE d.dper_tipo = 'Productos' AND d.dper_estado = 1 AND g.[gent_estado] = 1 --AND per.[pers_nombre_area] = 'OPERACIONES'
  AND d.[dper_valor] > 0


GO
/****** Object:  Table [dbo].[GE_TREDISTRIBUCION]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TREDISTRIBUCION](
	[redi_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[redi_periodo] [int] NOT NULL,
	[redi_producto_orig] [int] NOT NULL,
	[redi_producto_dist] [int] NOT NULL,
	[redi_valor] [decimal](18, 6) NOT NULL,
	[redi_usuario] [varchar](50) NOT NULL,
	[redi_fecha] [datetime] NOT NULL,
	[redi_valor_asignado] [decimal](18, 6) NULL,
	[redi_valor_producto] [decimal](18, 6) NULL,
 CONSTRAINT [PK_GE_TREDISTRIBUCION] PRIMARY KEY CLUSTERED 
(
	[redi_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_REDISTRIBUCION_SAP]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[VW_REDISTRIBUCION_SAP] AS
SELECT	red.redi_producto_dist,
		pr_dist.prod_codigo,
		total.item,
		SUM(total.vlr * red.redi_valor) valor
  FROM [Medeski].[dbo].[GE_TREDISTRIBUCION] red 
  INNER JOIN VW_PERIODO_ACTIVO per
  ON red.redi_periodo = per.[peri_consecutivo]
  INNER JOIN [dbo].[GE_TPRODUCTOS] pr_orig
  ON pr_orig.prod_consecutivo = red.redi_producto_orig
  INNER JOIN [dbo].[GE_TPRODUCTOS] pr_dist
  ON pr_dist.prod_consecutivo = red.redi_producto_dist
  INNER JOIN
(SELECT [idprod]
      ,[producto]
      ,CASE
	  WHEN [item] IS NULL  THEN 'LICENCIAMIENTO ' + [producto]
	  ELSE [item]
	  END
	  item
      ,SUM([total]) vlr
  FROM [Medeski].[dbo].[VW_VLR_REDISTRIBUCION]
  GROUP BY  [idprod]
      ,[producto]
      ,[item]) total
ON total.idprod = red.redi_producto_orig
GROUP BY redi_producto_dist, pr_dist.prod_codigo, total.item
  
GO
/****** Object:  View [dbo].[VW_BPC_TIPOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VW_BPC_TIPOS]
AS
SELECT  v.idprod Idproducto, 		
		v.[producto]  + '_' + 'Licenciamiento' Tipo,
		v.[producto]  + '_' + 'Directo'  SubTipo
FROM [dbo].[VW_SALIDA_PRESUPUESTO] v
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON v.idprod = p.[prod_consecutivo]

GROUP BY p.idservicio, p.servicio, v.idprod, v.[producto]

UNION
SELECT	total.[idprod] Idproducto, 
		total.[producto] + '_' + 'Licenciamiento' Tipo,
		total.[producto] + '_' + 'Intermedios'  SubTipo
FROM
(SELECT [idprod],[producto], [item], [vlrdistrib] valor
  FROM [Medeski].[dbo].[VW_REPORTE_DISTRIB_INTERM_CS]
UNION
SELECT  [redi_producto_dist], [prod_codigo], [item], [valor]
FROM [dbo].[VW_REDISTRIBUCION_SAP]
) total
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON total.idprod = p.[prod_consecutivo]

GROUP BY p.idservicio, p.servicio,total.[idprod], total.[producto]

UNION

SELECT 	distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Infraestructura' Tipo,
		p.prod_codigo + '_' + 'Infraestructura' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_SERV_ITEM_INFRAESTRUCTURA] v
ON p.prod_consecutivo = v.[prod_consecutivo]


UNION

SELECT 	distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Infraestructura' Tipo,
		p.prod_codigo + '_' + 'Datacenter' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_SERV_ITEM_DATACENTER] v
ON p.prod_consecutivo = v.dinf_producto

UNION

SELECT 	distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'MAS GA' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_GA] v
ON p.prod_consecutivo = v.prod_consecutivo


UNION

SELECT 	distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo  + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'MAS Software' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_SOFTWARE] v
ON p.prod_consecutivo = v.prod_consecutivo


UNION

SELECT  distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'MAS Infraestructura' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_INFRAESTRUCTURA] v
ON p.prod_consecutivo = v.idproducto


UNION

SELECT  distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'MAS Datacenter' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_DATACENTER] v
ON p.prod_consecutivo = v.idproducto


UNION

SELECT  distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'MAS GenteTecnica' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_INF] v
ON p.prod_consecutivo = v.idproducto


UNION


SELECT  distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'MAS GenteTecnica' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_DATAC] v
ON p.prod_consecutivo = v.idproducto


UNION

SELECT  distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'GA Operaciones' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_GA] v
ON p.prod_consecutivo = v.[prod_consecutivo]
WHERE v.[pers_nombre_area] = 'OPERACIONES' 

UNION

SELECT  distinct
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo,
		p.prod_codigo + '_' + 'GA Desarrollo' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_GA] v
ON p.prod_consecutivo = v.[prod_consecutivo]
WHERE v.[pers_nombre_area] = 'DESARROLLO' 


UNION

SELECT  distinct 
		p.prod_consecutivo IdProducto,
		p.prod_codigo + '_' + 'Equipo Interno' Tipo ,
		p.prod_codigo + '_' + 'GA GTecnica' SubTipo
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_GA_GTECNICA] v
ON p.prod_consecutivo = v.[prod_consecutivo]





GO
/****** Object:  View [dbo].[VW_BPC_TIPOS_GROUP]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** Script para el comando SelectTopNRows de SSMS  ******/
CREATE VIEW [dbo].[VW_BPC_TIPOS_GROUP] AS
SELECT
	ROW_NUMBER () 
	OVER(order by [Tipo]) idTipo
    	,[Tipo]
  FROM [dbo].[VW_BPC_TIPOS]
  GROUP BY Tipo
 

GO
/****** Object:  View [dbo].[VW_VLR_CUADRO_SERVICIO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[VW_VLR_CUADRO_SERVICIO] AS
/*LICENCIAS*/
SELECT *
FROM
(SELECT  v.idprod idprod, 
		v.[producto] producto, 
		v.item item, 
		ROUND((CAST(ISNULL(SUM(v.valor),0) AS DECIMAL(20,0))),0) vlr_lic,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,0))),0) vlr_int_lic,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_ga,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_softw,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_gente,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_ga_operaciones,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_ga_gtecnica,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_ga_desarrollo,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_cdm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_procesos,
		ROUND((CAST(ISNULL(SUM(v.valor),0) AS DECIMAL(20,0))),0) total
FROM [dbo].[VW_SALIDA_PRESUPUESTO] v
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON v.idprod = p.[prod_consecutivo]
GROUP BY v.idprod, v.[producto], v.item

UNION ALL

/*INTERMEDIO*/
SELECT	total.[idprod] idprod, 
		total.[producto] producto, 
		total.[item] item, 
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,0))),0) vlr_lic,
		ROUND((CAST(ISNULL(SUM(total.valor),0) AS DECIMAL(20,0))),0) vlr_int_lic,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_ga,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_softw,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_infr,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_datac,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_gente,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_ga_operaciones,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_ga_gtecnica,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_ga_desarrollo,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_cdm,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,10))),0) vlr_mas_procesos,
		ROUND((CAST(ISNULL(SUM(total.valor),0) AS DECIMAL(20,0))),0) total
FROM
(SELECT [idprod],[producto], [item], [vlrdistrib] valor
  FROM [Medeski].[dbo].[VW_REPORTE_DISTRIB_INTERM_CS]
UNION ALL
SELECT  [redi_producto_dist], [prod_codigo], [item], [valor]
FROM [dbo].[VW_REDISTRIBUCION_SAP]
) total
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON total.idprod = p.[prod_consecutivo]
GROUP BY total.[idprod], total.[producto], total.[item]


UNION ALL



SELECT	prred.prod_consecutivo idprod,
		prred.prod_codigo producto,
		null item,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,0))),0) vlr_lic,
		ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,0))),0) vlr_int_lic,
		ROUND((CAST(ISNULL(inf.valor,0) AS DECIMAL(20,10))),0) vlr_infr,
		ROUND((CAST(ISNULL(datac.valor,0) AS DECIMAL(20,10))),0) vlr_datac,
		ROUND((CAST(ISNULL(dmas.ga,0) AS DECIMAL(20,10))),0) vlr_mas_ga,
		ROUND((CAST(ISNULL(dmas.software,0) AS DECIMAL(20,10))),0) vlr_mas_softw,
		ROUND((CAST(ISNULL(dmas.infraestructura,0) AS DECIMAL(20,10))),0) vlr_mas_infr,
		ROUND((CAST(ISNULL(dmas.datacenter,0) AS DECIMAL(20,10))),0) vlr_mas_datac,
		ROUND((CAST(ISNULL(dmas.gente,0) AS DECIMAL(20,10))),0) vlr_mas_gente,
		ROUND((CAST(ISNULL(dedop.valor,0) AS DECIMAL(20,10))),0) vlr_ga_operaciones,
		ROUND((CAST(ISNULL(dedgtec.valor,0) AS DECIMAL(20,10))),0) vlr_ga_gtecnica,
		ROUND((CAST(ISNULL(deddes.valor,0) AS DECIMAL(20,10))),0) vlr_ga_desarrollo,
		ROUND((CAST(ISNULL(dmas.cdm,0) AS DECIMAL(20,10))),0) vlr_mas_cdm,
		ROUND((CAST(ISNULL(dmas.procesos,0) AS DECIMAL(20,10))),0) vlr_mas_procesos,
		(ROUND((CAST(ISNULL(0,0) AS DECIMAL(20,0))),0) +
		ROUND((CAST(ISNULL(inf.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(datac.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.ga,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.software,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.infraestructura,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.datacenter,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.gente,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dedop.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dedgtec.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(deddes.valor,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.cdm,0) AS DECIMAL(20,10))),0) +
		ROUND((CAST(ISNULL(dmas.procesos,0) AS DECIMAL(20,10))),0)) total
FROM
(SELECT pr.[prod_consecutivo]
		,pr.[prod_codigo]
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON p.[prod_consecutivo] = pr.[prod_consecutivo]
WHERE pr.prod_activo = 1) prred
LEFT OUTER JOIN
/*INFRAESTRUCTURA*/
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		SUM(ISNULL(inf.[vlrservprod],0)) valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_INFRAESTRUCTURA_MODIF inf
ON inf.prod_consecutivo = pr.prod_consecutivo
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON p.[prod_consecutivo] = pr.[prod_consecutivo]
AND inf.prod_consecutivo = p.[prod_consecutivo]
WHERE pr.prod_activo = 1
GROUP BY pr.prod_consecutivo, pr.prod_codigo
) inf
ON inf.prod_consecutivo = prred.prod_consecutivo

LEFT OUTER JOIN
/*DATACENTER*/
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		SUM(ISNULL(inf.[vlrprd],0)) valor
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].VW_VLR_PROD_DATACENTER_MODIF inf
ON inf.dinf_producto = pr.prod_consecutivo
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON p.[prod_consecutivo] = pr.[prod_consecutivo]
AND inf.[dinf_producto] = p.[prod_consecutivo]
WHERE pr.prod_activo = 1
GROUP BY pr.prod_consecutivo, pr.prod_codigo
) datac
ON datac.prod_consecutivo = prred.prod_consecutivo

LEFT OUTER JOIN

/*MAS*/
(SELECT	pr.prod_consecutivo,
		pr.prod_codigo,
		d.cdm,
		d.[datacenter],
		d.[software],
		d.infraestructura,
		d.ga,
		d.gente,
		d.procesos
FROM [dbo].[GE_TPRODUCTOS] pr
INNER JOIN [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d
ON d.prod_consecutivo = pr.prod_consecutivo
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON p.[prod_consecutivo] = pr.[prod_consecutivo]
AND d.prod_consecutivo = p.[prod_consecutivo]
WHERE pr.prod_activo = 1) dmas
ON dmas.prod_consecutivo = prred.prod_consecutivo

LEFT OUTER JOIN
/*DESARROLLO*/
(SELECT [prod_consecutivo]
      ,[prod_codigo]
      ,[pers_nombre_area]
      ,[valor]
FROM [Medeski].[dbo].[VW_VLR_DEDICACION_GENTE]
WHERE [pers_nombre_area] = 'DESARROLLO'
) deddes
ON deddes.[prod_consecutivo] = prred.prod_consecutivo

LEFT OUTER JOIN
/*OPERACIONES*/
(SELECT [prod_consecutivo]
      ,[prod_codigo]
      ,[pers_nombre_area]
      ,[valor]
FROM [Medeski].[dbo].[VW_VLR_DEDICACION_GENTE]
WHERE [pers_nombre_area] = 'OPERACIONES'
)dedop
ON dedop.[prod_consecutivo] = prred.prod_consecutivo

LEFT OUTER JOIN
/*INFRA*/
(/*SELECT [prod_codigo]
      ,[prod_consecutivo]
      ,SUM([vlrt]) valor
  FROM [Medeski].[dbo].[VW_VLR_GENTE_TECNICA_PROD] 
  GROUP BY [prod_codigo],[prod_consecutivo]*/
  SELECT prod_codigo, prod_consecutivo, valor FROM VW_VLR_GA_GENTE_TECNICA_MODIF
  )
  dedgtec
ON dedgtec.prod_consecutivo = prred.prod_consecutivo
)definitivo
WHERE definitivo.total > 0
--ORDER BY definitivo.producto
GO
/****** Object:  View [dbo].[VW_BPC_TIPOS_SALIDA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_BPC_TIPOS_SALIDA] AS 
SELECT
	ROW_NUMBER () 
	OVER(order by [SubTipo]) IdSubTipo,
	t1.idTipo, t2.Idproducto, t2.Tipo, t2.SubTipo
  FROM [dbo].[VW_BPC_TIPOS_GROUP] t1
  INNER JOIN VW_BPC_TIPOS t2 ON  t1.Tipo = t2.Tipo
GO
/****** Object:  View [dbo].[VW_BPC_PRODUCTOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_BPC_PRODUCTOS]
AS
SELECT     dbo.GE_TPRODUCTOS.prod_consecutivo, dbo.GE_TPRODUCTOS.prod_descripcion, dbo.GE_TPARAMETROS.parm_consecutivo
FROM         dbo.GE_TPRODUCTOS INNER JOIN
                      dbo.GE_TPARAMETROS ON dbo.GE_TPRODUCTOS.prod_serv_venta = dbo.GE_TPARAMETROS.parm_consecutivo
GO
/****** Object:  Table [dbo].[GE_TCLASESPARAMETROS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCLASESPARAMETROS](
	[clap_clase] [int] IDENTITY(1,1) NOT NULL,
	[clap_nombre] [varchar](100) NOT NULL,
	[clap_descripcion] [varchar](300) NOT NULL,
	[clap_fechaini] [date] NOT NULL,
	[clap_fechafin] [date] NULL,
	[clap_estado] [int] NOT NULL,
 CONSTRAINT [PK_CLAP_CLASE] PRIMARY KEY CLUSTERED 
(
	[clap_clase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UN_GE_TCLASESPARAMETROS] UNIQUE NONCLUSTERED 
(
	[clap_nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_BPC_SERVICIOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_BPC_SERVICIOS]
AS
SELECT     dbo.GE_TPARAMETROS.parm_consecutivo, dbo.GE_TPARAMETROS.parm_descripcion, dbo.GE_TCLASESPARAMETROS.clap_clase
FROM         dbo.GE_TCLASESPARAMETROS INNER JOIN
                      dbo.GE_TPARAMETROS ON dbo.GE_TCLASESPARAMETROS.clap_clase = dbo.GE_TPARAMETROS.clap_clase
WHERE     (dbo.GE_TCLASESPARAMETROS.clap_nombre = 'SERV_VENTA')
GO
/****** Object:  View [dbo].[VW_BPC_SALIDA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_BPC_SALIDA] AS 
SELECT 
	'T_PROD' ID, 
	'Total Producto' DESCRIPCION, 
	null PADRE, 
	'EXP' TipoCuenta
UNION  ALL
SELECT 'SR_' + CAST(parm_consecutivo as VARCHAR(30)),
		parm_descripcion,
		'T_PROD',
		'EXP'
FROM VW_BPC_SERVICIOS
UNION  ALL
SELECT 'PR_' + CAST(prod_consecutivo as VARCHAR(30)),
		prod_descripcion,
		'SR_' + CAST(parm_consecutivo as VARCHAR(30)),
		'EXP'
FROM VW_BPC_PRODUCTOS
UNION  ALL
SELECT 'PR_' + CAST(prod_consecutivo as VARCHAR(30)),
		prod_descripcion,
		'SR_' + CAST(parm_consecutivo as VARCHAR(30)),
		'EXP'
FROM VW_BPC_PRODUCTOS
UNION  ALL
SELECT 'TP_' + CAST(Idproducto as VARCHAR(30))+ '_' + CAST(idTipo as VARCHAR(30)),
		Tipo,
		'PR_' + CAST(Idproducto as VARCHAR(30)),
		'EXP'
FROM VW_BPC_TIPOS_SALIDA
UNION ALL
SELECT 'ST_' + CAST(idTipo as VARCHAR(30)) + '_' + CAST(IdSubTipo as VARCHAR(30)),
		SubTipo,
		'TP_' + CAST(idTipo as VARCHAR(30)),
		'EXP'
FROM VW_BPC_TIPOS_SALIDA
; 


GO
/****** Object:  Table [dbo].[GE_TVLR_CUADRO_SERVICIO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TVLR_CUADRO_SERVICIO](
	[cuad_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[idprod] [int] NULL,
	[producto] [varchar](500) NULL,
	[item] [varchar](200) NULL,
	[vlr_lic] [decimal](20, 0) NULL,
	[vlr_int_lic] [decimal](20, 0) NULL,
	[vlr_infr] [decimal](20, 10) NULL,
	[vlr_datac] [decimal](20, 10) NULL,
	[vlr_mas_ga] [decimal](20, 10) NULL,
	[vlr_mas_softw] [decimal](20, 10) NULL,
	[vlr_mas_infr] [decimal](20, 10) NULL,
	[vlr_mas_datac] [decimal](20, 10) NULL,
	[vlr_mas_gente] [decimal](20, 10) NULL,
	[vlr_ga_operaciones] [decimal](20, 10) NULL,
	[vlr_ga_gtecnica] [decimal](20, 10) NULL,
	[vlr_ga_desarrollo] [decimal](20, 10) NULL,
	[vlr_mas_cdm] [decimal](20, 10) NULL,
	[vlr_mas_procesos] [decimal](20, 10) NULL,
	[total] [decimal](38, 10) NULL,
 CONSTRAINT [PK_GE_TVLR_CUADRO_SERVICIO] PRIMARY KEY CLUSTERED 
(
	[cuad_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_VLR_CUADRO_SERVICIO_TOTAL]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_VLR_CUADRO_SERVICIO_TOTAL] AS
SELECT	cs.cuad_consecutivo,
		servicio,
		cs.idprod,
		CASE 
		WHEN cs.idprod < 10 THEN 'PR_0' + CAST(cs.idprod AS varchar(10))
	    ELSE 'PR_' + CAST(cs.idprod AS varchar(10))
		END PRODPC,
		cs.producto,
		SUM(cs.total) Total,
		SUM(cs.vlr_lic + cs.vlr_int_lic) Licenciamiento,
		SUM(cs.vlr_infr + cs.vlr_datac) Infraestructura,
		SUM(cs.vlr_ga_desarrollo + cs.vlr_ga_gtecnica + cs.vlr_ga_operaciones + cs.vlr_mas_cdm + cs.vlr_mas_datac + cs.vlr_mas_ga + cs.vlr_mas_gente + cs.vlr_mas_infr + cs.vlr_mas_procesos + cs.vlr_mas_softw) Equipo
FROM [dbo].[GE_TVLR_CUADRO_SERVICIO] cs
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] pr
ON cs.idprod = pr.prod_consecutivo
GROUP BY cs.cuad_consecutivo, servicio, cs.idprod, cs.producto


GO
/****** Object:  View [dbo].[VW_VLR_CUADRO_SERVICIO_BPC]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_VLR_CUADRO_SERVICIO_BPC]
AS
/*LICENCIAS*/ SELECT p.servicio SERVICIO, CASE WHEN p.prod_consecutivo < 10 THEN 'PR_0' + CAST(p.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(p.prod_consecutivo AS varchar(10)) END PRODPC, p.prod_codigo PRODUCTO, 
                         p.prod_codigo + '_LICENCIAS' TIPO, p.prod_codigo + '_LICENCIAMIENTO' SUBTIPO, 'SR_' + CAST(p.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN p.prod_consecutivo < 10 THEN 'TP_0' + CAST(p.prod_consecutivo AS varchar(10)) + '_1' ELSE 'PR_' + CAST(p.prod_consecutivo AS varchar(10)) + '_1' END IDTIPO, 
                         CASE WHEN p.prod_consecutivo < 10 THEN 'ST_0' + CAST(p.prod_consecutivo AS varchar(10)) + '_1_1' ELSE 'PR_' + CAST(p.prod_consecutivo AS varchar(10)) + '_1_1' END IDSUBTIPO, ROUND((CAST(ISNULL(SUM(v.valor), 0) 
                         AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            [dbo].[VW_SALIDA_PRESUPUESTO] v INNER JOIN
                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON v.idprod = p.[prod_consecutivo]
GROUP BY p.idServicio, p.servicio, p.prod_consecutivo, p.prod_codigo
UNION ALL
/*INTERMEDIO*/ SELECT p.servicio SERVICIO, CASE WHEN p.prod_consecutivo < 10 THEN 'PR_0' + CAST(p.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(p.prod_consecutivo AS varchar(10)) END PRODPC, 
                         p.prod_codigo PRODUCTO, p.prod_codigo + '_LICENCIAS' TIPO, p.prod_codigo + '_INTERMEDIOS_LICENCIAMIENTO' SUBTIPO, 'SR_' + CAST(p.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN p.prod_consecutivo < 10 THEN 'TP_0' + CAST(p.prod_consecutivo AS varchar(10)) + '_1' ELSE 'TP_' + CAST(p.prod_consecutivo AS varchar(10)) + '_1' END IDTIPO, 
                         CASE WHEN p.prod_consecutivo < 10 THEN 'ST_0' + CAST(p.prod_consecutivo AS varchar(10)) + '_1_2' ELSE 'ST_' + CAST(p.prod_consecutivo AS varchar(10)) + '_1_2' END IDSUBTIPO, ROUND((CAST(ISNULL(SUM(total.valor), 
                         0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        [idprod], [producto], [item], [vlrdistrib] valor
                          FROM            [Medeski].[dbo].[VW_REPORTE_DISTRIB_INTERM_CS]
                          UNION ALL
                          SELECT        [redi_producto_dist], [prod_codigo], [item], [valor]
                          FROM            [dbo].[VW_REDISTRIBUCION_SAP]) total INNER JOIN
                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON total.idprod = p.[prod_consecutivo]
GROUP BY p.idServicio, p.servicio, p.prod_consecutivo, p.prod_codigo
UNION ALL
/*INFRAESTRUCTURA*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_INFRAESTRUCTURA' TIPO, prred.prod_codigo + '_INFRAESTRUCTURA' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2_1' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2_1' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(inf.valor), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, SUM(ISNULL(inf.[vlrservprod], 0)) valor
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].VW_VLR_PROD_INFRAESTRUCTURA_MODIF inf ON inf.prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND inf.prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1
                               GROUP BY pr.prod_consecutivo, pr.prod_codigo) inf ON inf.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*DATACENTER*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_INFRAESTRUCTURA' TIPO, prred.prod_codigo + '_DATACENTER' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2_2' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_2_2' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(datac.valor), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, SUM(ISNULL(inf.[vlrprd], 0)) valor
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].VW_VLR_PROD_DATACENTER_MODIF inf ON inf.dinf_producto = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND inf.[dinf_producto] = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1
                               GROUP BY pr.prod_consecutivo, pr.prod_codigo) datac ON datac.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS GA*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_MAS_GA' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_1' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_1' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.ga), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS SOFTWARE*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_MAS_SOFTWARE' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_2' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_2' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.[software]), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS INFRAESTRUCTURA*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) 
                         END PRODPC, prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_MAS_INFRAESTRUCTURA' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_3' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_3' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.infraestructura), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS DATACENTER*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_MAS_DATACENTER' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_4' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_4' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.[datacenter]), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS GENTE INFRAESTRUCTURA*/ SELECT DISTINCT 
                         prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_MAS_GENTE_INFRAESTRUCTURA' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_5' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_5' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.gente), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS CDM*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_CDM' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_6' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_6' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.cdm), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*MAS PROCESOS*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_PROCESOS' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_7' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_7' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dmas.procesos), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        pr.prod_consecutivo, pr.prod_codigo, d .cdm, d .[datacenter], d .[software], d .infraestructura, d .ga, d .gente, d .procesos
                               FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                         [Medeski].[dbo].[VW_VLR_DISTRIB_MAS] d ON d .prod_consecutivo = pr.prod_consecutivo INNER JOIN
                                                         [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo] AND d .prod_consecutivo = p.[prod_consecutivo]
                               WHERE        pr.prod_activo = 1) dmas ON dmas.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*GA DESARROLLO*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_GA_DESARROLLO' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_8' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_8' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(deddes.valor), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (SELECT        [prod_consecutivo], [prod_codigo], [pers_nombre_area], [valor]
                               FROM            [Medeski].[dbo].[VW_VLR_DEDICACION_GENTE]
                               WHERE        [pers_nombre_area] = 'DESARROLLO') deddes ON deddes.[prod_consecutivo] = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*GA OPERACIONES*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_GA_OPERACIONES' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_9' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_9' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dedop.valor), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             /*OPERACIONES*/ (SELECT        [prod_consecutivo], [prod_codigo], [pers_nombre_area], [valor]
                                                                        FROM            [Medeski].[dbo].[VW_VLR_DEDICACION_GENTE]
                                                                        WHERE        [pers_nombre_area] = 'OPERACIONES') dedop ON dedop.[prod_consecutivo] = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
UNION ALL
/*GA INFR*/ SELECT prred.servicio SERVICIO, CASE WHEN prred.prod_consecutivo < 10 THEN 'PR_0' + CAST(prred.prod_consecutivo AS varchar(10)) ELSE 'PR_' + CAST(prred.prod_consecutivo AS varchar(10)) END PRODPC, 
                         prred.prod_codigo PRODUCTO, prred.prod_codigo + '_EQUIPO_INTERNO' TIPO, prred.prod_codigo + '_GA_GTECNICA' SUBTIPO, 'SR_' + CAST(prred.idServicio AS varchar(10)) IDSERVICIO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'TP_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' ELSE 'TP_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3' END IDTIPO, 
                         CASE WHEN prred.prod_consecutivo < 10 THEN 'ST_0' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_10' ELSE 'ST_' + CAST(prred.prod_consecutivo AS varchar(10)) + '_3_10' END IDSUBTIPO, 
                         ROUND((CAST(ISNULL(SUM(dedgtec.valor), 0) AS DECIMAL(20, 0))), 0) VALOR,'A' As LICENCIAS , 'PR_64_1_1_1' as IDLICENCIAS , 'B' as INTERMEDIOS,  'PR_64_1_1_1_1' as IDINTERMEDIOS 
FROM            (SELECT        pr.[prod_consecutivo], pr.[prod_codigo], p.servicio, p.idServicio
                          FROM            [dbo].[GE_TPRODUCTOS] pr INNER JOIN
                                                    [dbo].[VW_PRODUCTOS_DIRECTOS] p ON p.[prod_consecutivo] = pr.[prod_consecutivo]
                          WHERE        pr.prod_activo = 1) prred LEFT OUTER JOIN
                             (/*SELECT [prod_codigo]
      ,[prod_consecutivo]
      ,SUM([vlrt]) valor
  FROM [Medeski].[dbo].[VW_VLR_GENTE_TECNICA_PROD] 
  GROUP BY [prod_codigo],[prod_consecutivo]*/ SELECT prod_codigo, prod_consecutivo, 
                                                         valor
                               FROM            VW_VLR_GA_GENTE_TECNICA_MODIF) dedgtec ON dedgtec.prod_consecutivo = prred.prod_consecutivo
GROUP BY prred.idServicio, prred.servicio, prred.prod_consecutivo, prred.prod_codigo
GO
/****** Object:  Table [dbo].[GE_TREDISTRIBUCION_DRIVERS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TREDISTRIBUCION_DRIVERS](
	[care_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[care_ceop_id] [int] NULL,
	[care_cargue_driver] [int] NULL,
	[care_valor] [decimal](18, 10) NULL,
	[care_usuario] [varchar](50) NULL,
	[care_fecha] [datetime] NULL,
	[care_usuario_act] [varchar](50) NULL,
	[care_fecha_act] [datetime] NULL,
	[care_activo] [varchar](1) NULL,
 CONSTRAINT [PK_GE_TREDISTRIBUCION_DRIVERS] PRIMARY KEY CLUSTERED 
(
	[care_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCARGUEDRIVERS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCARGUEDRIVERS](
	[carg_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[carg_periodo] [int] NULL,
	[carg_producto] [int] NULL,
	[carg_compania] [int] NULL,
	[carg_sede] [varchar](100) NULL,
	[carg_ccosto] [int] NULL,
	[carg_driver] [int] NULL,
	[carg_cantidad] [decimal](38, 10) NULL,
	[carg_valor] [int] NULL,
	[carg_valor_distribucion] [decimal](38, 10) NULL,
	[carg_valor_adicional] [decimal](38, 10) NULL,
	[carg_valor_total] [decimal](38, 10) NULL,
	[carg_proveedor] [varchar](50) NULL,
	[carg_usuario] [varchar](50) NULL,
	[carg_fecha] [datetime] NULL,
	[carg_usuario_act] [varchar](50) NULL,
	[carg_fecha_act] [datetime] NULL,
	[carg_activo] [int] NULL,
 CONSTRAINT [PK_GE_TCARGUEDRIVERS] PRIMARY KEY CLUSTERED 
(
	[carg_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCENTROSOPERACION]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCENTROSOPERACION](
	[ceop_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[ceop_codigo] [varchar](50) NOT NULL,
	[ceop_descripcion] [varchar](500) NOT NULL,
	[ceop_activo] [int] NOT NULL,
	[ceop_usuario] [varchar](50) NOT NULL,
	[ceop_fecha] [datetime] NOT NULL,
	[ceop_vicepresidencia] [varchar](1) NULL,
 CONSTRAINT [PK_GE_TCENTROSOPERACION] PRIMARY KEY CLUSTERED 
(
	[ceop_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_CODIGO] UNIQUE NONCLUSTERED 
(
	[ceop_codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_REPORTE_REDISTRIBUCION]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[VW_REPORTE_REDISTRIBUCION]
AS
SELECT 'INDIRECTOS' as ceop_codigo, 
	cost_centro_operacion,	 
	ceop_descripcion,
	SUM(carg_valor_total) AS Expr1 
from 
GE_TCARGUEDRIVERS 

INNER JOIN dbo.GE_TCENTROSCOSTOS 
	ON dbo.GE_TCARGUEDRIVERS.carg_ccosto = dbo.GE_TCENTROSCOSTOS.cost_consecutivo 
INNER JOIN dbo.GE_TCENTROSOPERACION 
	ON dbo.GE_TCENTROSOPERACION.ceop_codigo = dbo.GE_TCENTROSCOSTOS.cost_centro_operacion	
WHERE     dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = 528
and dbo.GE_TCARGUEDRIVERS.carg_activo = 1
GROUP BY cost_centro_operacion, ceop_descripcion
	
UNION 

SELECT 'EXTERNOS' as ceop_codigo, 
	cost_centro_operacion, 
	ceop_descripcion,
	SUM(carg_valor_total) AS Expr1 
from 
GE_TCARGUEDRIVERS 

INNER JOIN dbo.GE_TCENTROSCOSTOS 
	ON dbo.GE_TCARGUEDRIVERS.carg_ccosto = dbo.GE_TCENTROSCOSTOS.cost_consecutivo 
INNER JOIN dbo.GE_TCENTROSOPERACION 
	ON dbo.GE_TCENTROSOPERACION.ceop_codigo = dbo.GE_TCENTROSCOSTOS.cost_centro_operacion	
WHERE     dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = 527
and dbo.GE_TCARGUEDRIVERS.carg_activo = 1
GROUP BY cost_centro_operacion, ceop_descripcion
	
UNION 

SELECT 'DIRECTOS' as ceop_codigo, 
	cost_centro_operacion,
	ceop_descripcion,
	SUM(carg_valor_total) AS Expr1 
from 
GE_TCARGUEDRIVERS 

INNER JOIN dbo.GE_TCENTROSCOSTOS 
	ON dbo.GE_TCARGUEDRIVERS.carg_ccosto = dbo.GE_TCENTROSCOSTOS.cost_consecutivo 
INNER JOIN dbo.GE_TCENTROSOPERACION 
	ON dbo.GE_TCENTROSOPERACION.ceop_codigo = dbo.GE_TCENTROSCOSTOS.cost_centro_operacion	
WHERE dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = 524
GROUP BY cost_centro_operacion, ceop_descripcion

UNION

SELECT TOP (100) PERCENT 
	ceop2.ceop_codigo,
	dbo.GE_TCENTROSCOSTOS.cost_centro_operacion, 
	ceop2.ceop_descripcion, 
	SUM(dbo.GE_TREDISTRIBUCION_DRIVERS.care_valor) AS Expr1
FROM dbo.GE_TCARGUEDRIVERS 
INNER JOIN dbo.GE_TCENTROSCOSTOS 
	ON dbo.GE_TCARGUEDRIVERS.carg_ccosto = dbo.GE_TCENTROSCOSTOS.cost_consecutivo 
INNER JOIN dbo.GE_TREDISTRIBUCION_DRIVERS 
	ON dbo.GE_TCARGUEDRIVERS.carg_consecutivo = dbo.GE_TREDISTRIBUCION_DRIVERS.care_cargue_driver 
INNER JOIN dbo.GE_TCENTROSOPERACION ceop2
	ON dbo.GE_TREDISTRIBUCION_DRIVERS.care_ceop_id = ceop2.ceop_consecutivo
WHERE     dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = 524
and dbo.GE_TCARGUEDRIVERS.carg_activo = 1
GROUP BY ceop2.ceop_codigo, dbo.GE_TCENTROSCOSTOS.cost_centro_operacion, ceop_descripcion




GO
/****** Object:  Table [dbo].[GE_THISTORICOPYG]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_THISTORICOPYG](
	[vent_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[vent_periodo] [int] NULL,
	[vent_ceop] [int] NULL,
	[vent_valor_ventas] [decimal](21, 10) NULL,
	[vent_valor_directos] [decimal](21, 10) NULL,
	[vent_valor_indirectos] [decimal](21, 10) NULL,
	[vent_tipo] [char](4) NULL,
	[vent_usuario] [varchar](50) NULL,
	[vent_fecha] [datetime] NULL,
	[vent_usuario_act] [varchar](50) NULL,
	[vent_fecha_act] [datetime] NULL,
	[vent_activo] [int] NULL,
 CONSTRAINT [PK_GE_THISTORICOPYG] PRIMARY KEY CLUSTERED 
(
	[vent_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_CUADRO_VENTAS_PYG]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_CUADRO_VENTAS_PYG]
AS


SELECT ceop.[ceop_codigo]
	  ,ppto.[vent_valor_ventas] as ventas
      ,ppto.[vent_valor_indirectos] as indirectos
      ,ppto.[vent_valor_directos] as directos
      ,ppto.[vent_tipo] as tipo
      ,forecast.[vent_valor_ventas] as forecast_ventas
      ,forecast.[vent_valor_indirectos] as forecast_indirectos
      ,forecast.[vent_valor_directos] as forecast_directos
      ,forecast.[vent_tipo] as forecast_tipo
  FROM [Medeski].[dbo].[GE_THISTORICOPYG] ppto
  JOIN [Medeski].[dbo].[GE_THISTORICOPYG] forecast
  ON ppto.vent_ceop = forecast.vent_ceop
  JOIN GE_TCENTROSOPERACION ceop
  ON ppto.vent_ceop = ceop.ceop_consecutivo
  where ppto.vent_activo = 1  
  and ppto.vent_periodo = (select peri_consecutivo from VW_PERIODO_ACTIVO /*where peri_etapa = 'CSER'*/)
  and ppto.vent_tipo = 'PPTO'
  and forecast.vent_periodo = (select peri_consecutivo from VW_PERIODO_ACTIVO /*where peri_etapa = 'CSER'*/)
  and forecast.vent_tipo = 'FORE'
UNION 


SELECT  
	dbo.GE_TCENTROSOPERACION.ceop_codigo, 
	0.0 as ventas, 
	ISNULL(SUM(ind.Expr1), 0.0) as indirectos, 
	ISNULL(SUM(dir.Expr1), 0.0) as directos,
	NULL,
	0.0 as forecast_ventas, 
	0.0 as forecast_indirectos, 
	0.0 as forecast_directos,
	NULL
FROM dbo.GE_TCENTROSOPERACION 

INNER JOIN
    dbo.GE_TCENTROSCOSTOS ON dbo.GE_TCENTROSCOSTOS.cost_centro_operacion = dbo.GE_TCENTROSOPERACION.ceop_codigo 
INNER JOIN
    dbo.GE_TPARAMETROS ON dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = dbo.GE_TPARAMETROS.parm_consecutivo
LEFT JOIN 
	VW_REPORTE_REDISTRIBUCION dir ON dir.cost_centro_operacion = dbo.GE_TCENTROSOPERACION.ceop_codigo
	AND dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = 524

LEFT JOIN VW_REPORTE_REDISTRIBUCION ind ON ind.cost_centro_operacion = dbo.GE_TCENTROSOPERACION.ceop_codigo
	AND dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = 528
WHERE dbo.GE_TCENTROSOPERACION.ceop_codigo NOT IN (
	SELECT [ceop_codigo]
	FROM [Medeski].[dbo].[GE_THISTORICOPYG]
	JOIN GE_TCENTROSOPERACION
	ON GE_THISTORICOPYG.vent_ceop = GE_TCENTROSOPERACION.ceop_consecutivo
	where vent_activo = 1	
	and vent_periodo = (select peri_consecutivo from VW_PERIODO_ACTIVO /*where peri_etapa = 'CSER'*/)
)
AND dbo.GE_TCENTROSOPERACION.ceop_activo = 1
GROUP BY dbo.GE_TCENTROSOPERACION.ceop_codigo


GO
/****** Object:  View [dbo].[VW_CS_PRODUCTOS_INFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VW_CS_PRODUCTOS_INFRAESTRUCTURA]
AS
SELECT 
                      pa1.parm_consecutivo AS idservicio,
                      pa1.parm_descripcion AS servicio, 
                      pr.prod_consecutivo, pr.prod_codigo, 'Equipo Interno' as Tipo,
                      'GA GTECNICA' as SubTipo, 
                      per.pers_nombres as ServPersona,
                      NULL as Item,
                      (pinf.dper_valor * gente.gent_costo_colaborador) as Valor,
                      'developer' AS Usuario ,
                      GETDATE()  AS Fecha
                      
FROM         dbo.GE_TPRODUCTOS AS pr 

INNER JOIN
  dbo.GE_TDISTRIBUCIONDEDICACIONPERSONA AS pinf ON pr.prod_consecutivo = pinf.dper_producto 
INNER JOIN
  dbo.GE_TPERSONAS AS per ON per.pers_consecutivo = pinf.dper_persona 
INNER JOIN
  dbo.GE_TGENTE AS gente ON gente.gent_persona = per.pers_consecutivo 
INNER JOIN
  dbo.GE_TPARAMETROS AS pa ON pr.prod_componente = pa.parm_consecutivo 
LEFT OUTER JOIN
  dbo.GE_TPARAMETROS AS pa1 ON pr.prod_serv_venta = pa1.parm_consecutivo
WHERE pinf.dper_tipo = 'Producto-Infraestructura'
AND pinf.dper_periodo = ( SELECT peri_consecutivo FROM GE_TPERIODOPRESUPUESTO WHERE peri_activo = 1 )

GO
/****** Object:  Table [dbo].[VLRSPRMGRALES]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VLRSPRMGRALES](
	[vhpg_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[pmgr_parametro] [varchar](50) NOT NULL,
	[vhpg_fecdesde] [date] NOT NULL,
	[vhpg_fechasta] [date] NULL,
	[vhpg_estado] [int] NOT NULL,
	[vhpg_valor] [varchar](300) NOT NULL,
 CONSTRAINT [PK_VHPG_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[vhpg_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_PRESUPUESTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[VW_PRESUPUESTO] AS
SELECT	ROW_NUMBER() OVER(ORDER BY pers.pers_identificacion, pers.pers_nombres ASC) AS NFila,
		pers.pers_identificacion identificacion, pers.pers_nombres nombre,  pr.prod_descripcion producto, prit.prit_item item, v.[vhpg_valor] + cuenta.cuen_auxiliar cuenta,
		v.[vhpg_valor] + ceco.cost_codigo ceco, per.peri_ano ano, sal.sali_mes mes, 
		CASE LEN(par1.parm_codigo)
		WHEN 1 THEN  CAST(per.peri_ano AS VARCHAR(4)) + '.0' + par1.parm_codigo
		ELSE CAST(per.peri_ano AS VARCHAR(4)) + '.' + par1.parm_codigo
		END ano_mes,
		par.parm_codigo moneda, 
		sal.sali_valor valor,
		CASE 
		WHEN pr.prod_consecutivo < 10 THEN 'PR_0' + CAST(pr.prod_consecutivo AS varchar(10))
	    ELSE 'PR_' + CAST(pr.prod_consecutivo AS varchar(10))
		END IDPROD,
		CASE 
		WHEN prit.prit_consecutivo < 10 THEN 'IT_0' + CAST(prit.prit_consecutivo AS varchar(10))
	    ELSE 'IT_' + CAST(prit.prit_consecutivo AS varchar(10))
		END IDIT
FROM [dbo].[GE_TSALIDAPRESUPUESTO] sal
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] per
ON sal.sali_periodo = per.peri_consecutivo
INNER JOIN [dbo].[GE_TPERSONAS] pers
ON pers.pers_consecutivo = sal.sali_persona
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] prit
ON prit.prit_consecutivo = sal.sali_producto_item
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON pr.prod_consecutivo = prit.prit_producto
INNER JOIN [dbo].[GE_TCENTROSCOSTOS] ceco
ON sal.sali_centrocosto = ceco.cost_consecutivo
INNER JOIN [dbo].[GE_TPARAMETROS] par
ON par.parm_consecutivo = sal.sali_moneda
INNER JOIN [dbo].[GE_TCUENTAS] cuenta
ON prit.prit_cuenta = cuenta.cuen_consecutivo
INNER JOIN [dbo].[VLRSPRMGRALES] v
ON v.pmgr_parametro = 'PREFCARGUE'
INNER JOIN [dbo].[GE_TPARAMETROS] par1
ON sal.sali_mes = par1.parm_consecutivo
/*UNION
/*CARGUE ARCHIVOS LABORALES*/
SELECT pe.[pers_identificacion], pe.[pers_nombres], pr.[prod_descripcion],
it.[prit_item],  v.[vhpg_valor] + c.[carl_auxiliar],  v.[vhpg_valor] + c.[carl_ccosto], p.[peri_ano], 
c.[carl_mes], 'COP', c.[carl_valor]
FROM [dbo].[GE_TCARGUEARCHIVOSLABORAL] c
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
ON it.[prit_item] = 'LABORALES'
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON pr.[prod_consecutivo] = it.[prit_producto]
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.[peri_activo] = 1
INNER JOIN [dbo].[GE_TPARAMETROS] pa
ON pa.[parm_descripcion] = 'PESOS'
INNER JOIN [dbo].[GE_TPERSONAS] pe
ON pr.[prod_responsable] = pe.pers_consecutivo
 INNER JOIN dbo.VLRSPRMGRALES AS v 
 ON v.pmgr_parametro = 'PREFCARGUE'
WHERE [carl_valor] > 0

UNION
/*CARGUE ARCHIVOS*/
SELECT pe.pers_identificacion, pe.pers_nombres, pr.[prod_descripcion], ca.carg_item, v.[vhpg_valor] + cu.cuen_auxiliar,
 CASE LEN(ca.[carg_ccosto])
	WHEN 0 THEN ca.carg_empresa
	ELSE v.[vhpg_valor] + ca.carg_ccosto
 END,
 p.peri_ano, pa1.parm_codigo, pa.[parm_codigo], ca.[carg_valor]
FROM [Medeski].[dbo].[GE_TCARGUEARCHIVOS] ca
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON ca.carg_producto = pr.prod_consecutivo
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
ON it.prit_item = UPPER(ca.[carg_item])
INNER JOIN [dbo].[GE_TPERSONAS] pe
ON pr.prod_responsable = pe.pers_consecutivo
INNER JOIN [dbo].[GE_TCUENTAS] cu
ON cu.cuen_consecutivo = it.prit_cuenta
INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] p
ON p.peri_activo = 1
INNER JOIN [dbo].[GE_TPARAMETROS] pa
ON pa.[parm_descripcion] = 'PESOS'
INNER JOIN [dbo].[GE_TPARAMETROS] pa1
ON pa1.parm_descripcion = UPPER(ca.carg_mes)
 INNER JOIN dbo.VLRSPRMGRALES AS v 
 ON v.pmgr_parametro = 'PREFCARGUE'
 */






GO
/****** Object:  View [dbo].[VW_PRODUCTOS_INFRAESTRUCTURA]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_PRODUCTOS_INFRAESTRUCTURA]
AS
SELECT 
                      pr.prod_consecutivo, pr.prod_codigo, pr.prod_responsable, pr.prod_driver1, pr.prod_activo, pa.parm_descripcion, pa1.parm_descripcion AS servicio, pa1.parm_consecutivo AS idservicio
FROM         dbo.GE_TPRODUCTOS AS pr INNER JOIN
                      dbo.GE_TDISTRIBUCIONDEDICACIONPERSONA AS pinf ON pr.prod_consecutivo = pinf.dper_producto INNER JOIN
                      dbo.GE_TPARAMETROS AS pa ON pr.prod_componente = pa.parm_consecutivo LEFT OUTER JOIN
                      dbo.GE_TPARAMETROS AS pa1 ON pr.prod_serv_venta = pa1.parm_consecutivo
WHERE     (pinf.dper_tipo = 'Producto-Infraestructura')

GO
/****** Object:  View [dbo].[VW_PRODUCTOS_PRESUPUESTADOS]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_PRODUCTOS_PRESUPUESTADOS]
AS
SELECT     TOP (1000) dbo.GE_TPERIODOTRANSACCIONES.petr_consecutivo, dbo.GE_TPERIODOPRESUPUESTO.peri_ano, dbo.GE_TPERIODOPRESUPUESTO.peri_paso,
                      dbo.GE_TPERSONAS.pers_nombres, dbo.GE_TCENTROSCOSTOS.cost_codigo, dbo.GE_TPRODUCTOS.prod_codigo, dbo.GE_TPRODUCTOSITEMS.prit_item, dbo.GE_TCUENTAS.cuen_auxiliar, 
                      p1.parm_codigo, dbo.GE_TPERIODOTRANSACCIONES.petr_valor, dbo.GE_TPERIODOTRANSACCIONES.petr_cantidad, dbo.GE_TPERIODOTRANSACCIONES.petr_observacion, 
                      dbo.GE_TPERIODOTRANSACCIONES.petr_tipo, dbo.GE_TSALIDAPRESUPUESTO.sali_valor, p2.parm_descripcion AS mes
FROM         dbo.GE_TPERIODOTRANSACCIONES INNER JOIN
                      dbo.GE_TPERSONAS ON dbo.GE_TPERIODOTRANSACCIONES.petr_persona = dbo.GE_TPERSONAS.pers_consecutivo INNER JOIN
                      dbo.GE_TPERIODOPRESUPUESTO ON dbo.GE_TPERIODOTRANSACCIONES.petr_periodo = dbo.GE_TPERIODOPRESUPUESTO.peri_consecutivo INNER JOIN
                      dbo.GE_TPRODUCTOSITEMS ON dbo.GE_TPERIODOTRANSACCIONES.petr_producto_item = dbo.GE_TPRODUCTOSITEMS.prit_consecutivo INNER JOIN
                      dbo.GE_TPRODUCTOS ON dbo.GE_TPRODUCTOSITEMS.prit_producto = dbo.GE_TPRODUCTOS.prod_consecutivo INNER JOIN
                      dbo.GE_TSALIDAPRESUPUESTO ON dbo.GE_TPERIODOTRANSACCIONES.petr_consecutivo = dbo.GE_TSALIDAPRESUPUESTO.sali_periodo_transacc INNER JOIN
                      dbo.GE_TCENTROSCOSTOS ON dbo.GE_TPERIODOTRANSACCIONES.petr_centrocosto = dbo.GE_TCENTROSCOSTOS.cost_consecutivo INNER JOIN
                      dbo.GE_TPARAMETROS AS p1 ON dbo.GE_TPERIODOTRANSACCIONES.petr_moneda = p1.parm_consecutivo INNER JOIN
                      dbo.GE_TCUENTAS ON dbo.GE_TPRODUCTOSITEMS.prit_cuenta = dbo.GE_TCUENTAS.cuen_consecutivo INNER JOIN
                      dbo.GE_TPARAMETROS AS p2 ON dbo.GE_TSALIDAPRESUPUESTO.sali_mes = p2.parm_consecutivo
WHERE     (dbo.GE_TPERIODOPRESUPUESTO.peri_activo = 1) AND (dbo.GE_TPERIODOTRANSACCIONES.petr_activo = 1)

GO
/****** Object:  View [dbo].[VW_VLR_DISTRIBUCION_CCOSTO]    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VW_VLR_DISTRIBUCION_CCOSTO]
AS
SELECT  dbo.GE_TCENTROSOPERACION.ceop_codigo,
		dbo.GE_TCENTROSCOSTOS.cost_codigo, 
		dbo.GE_TPRODUCTOS.prod_descripcion, 
		dbo.GE_TCARGUEDRIVERS.carg_valor_total	
FROM    
		dbo.GE_TCARGUEDRIVERS 
INNER JOIN
		dbo.GE_TCENTROSCOSTOS 
ON 
		dbo.GE_TCARGUEDRIVERS.carg_ccosto = dbo.GE_TCENTROSCOSTOS.cost_consecutivo 
INNER JOIN
		dbo.GE_TCENTROSOPERACION
ON 
		dbo.GE_TCENTROSCOSTOS.cost_centro_operacion = dbo.GE_TCENTROSOPERACION.ceop_codigo 
INNER JOIN
		dbo.GE_TPARAMETROS 
ON 
	dbo.GE_TCENTROSCOSTOS.cost_tipo_cliente = dbo.GE_TPARAMETROS.parm_consecutivo 
INNER JOIN
		dbo.GE_TPRODUCTOS 
ON dbo.GE_TCARGUEDRIVERS.carg_producto = dbo.GE_TPRODUCTOS.prod_consecutivo
GROUP BY
		dbo.GE_TCENTROSOPERACION.ceop_codigo,
		dbo.GE_TCENTROSCOSTOS.cost_codigo, 
		dbo.GE_TPRODUCTOS.prod_descripcion, 
		dbo.GE_TCARGUEDRIVERS.carg_valor_total	
		


GO
/****** Object:  Table [dbo].['BASE DATOS COBROS$']    Script Date: 02/08/2019 17:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['BASE DATOS COBROS$'](
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [nvarchar](255) NULL,
	[EMPRESA] [nvarchar](255) NULL,
	[Sede] [nvarchar](255) NULL,
	[AREA] [nvarchar](255) NULL,
	[Grupo] [nvarchar](255) NULL,
	[Nombre del Servicio] [nvarchar](255) NULL,
	[PRODUCTO] [nvarchar](255) NULL,
	[Nuevo] [nvarchar](255) NULL,
	[Centro Costo] [nvarchar](255) NULL,
	[Nombre Centro de Costo] [nvarchar](255) NULL,
	[alias] [nvarchar](255) NULL,
	[Usuario] [nvarchar](255) NULL,
	[Tipo de Negociacion] [nvarchar](255) NULL,
	[Cantidad] [float] NULL,
	[Direccion] [nvarchar](255) NULL,
	[PROVEEDOR] [nvarchar](255) NULL,
	[C#O# Real] [nvarchar](255) NULL,
	[Nombre C#O Real] [nvarchar](255) NULL,
	[VICEPRESIDENCIA] [nvarchar](255) NULL,
	[TIPO GASTO] [nvarchar](255) NULL,
	[TOTAL] [money] NULL,
	[ENERO] [money] NULL,
	[FEBRERO] [money] NULL,
	[MARZO] [money] NULL,
	[ABRIL] [money] NULL,
	[MAYO] [money] NULL,
	[JUNIO] [money] NULL,
	[JULIO] [money] NULL,
	[AGOSTO] [money] NULL,
	[SEPTIEMBRE] [money] NULL,
	[OCTUBRE] [money] NULL,
	[NOVIEMBRE] [money] NULL,
	[DICIEMBRE] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['CS_Cuadro Servicios$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['CS_Cuadro Servicios$'](
	[TIPO DE SERVICIO] [nvarchar](255) NULL,
	[PRODUCTO] [nvarchar](255) NULL,
	[ITEM] [nvarchar](255) NULL,
	[DESCRIPCION] [nvarchar](255) NULL,
	[Cantidad] [float] NULL,
	[CS 2017] [money] NULL,
	[CS 2017 AJUSTADO] [money] NULL,
	[$ Licenciamiento] [money] NULL,
	[$ Intermedios Licenc#] [money] NULL,
	[$ Infraestructura] [money] NULL,
	[$ Datacenter] [money] NULL,
	[$ MAS G#A#] [money] NULL,
	[$ MAS Soft#] [money] NULL,
	[$ MAS  Infraest#] [money] NULL,
	[$ MAS Datacenter] [money] NULL,
	[$ MAS Gente Infraest#] [money] NULL,
	[$ GA Operaciones] [money] NULL,
	[$ GA G# Tecnica] [money] NULL,
	[$ GA  Desarrollo] [money] NULL,
	[$ CDM] [money] NULL,
	[$ PROCESOS] [money] NULL,
	[$ TOTAL] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['CS_Dist# Infraestructura$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['CS_Dist# Infraestructura$'](
	[F1] [nvarchar](255) NULL,
	[PPTO 2018] [nvarchar](255) NULL,
	[F3] [nvarchar](255) NULL,
	[F4] [nvarchar](255) NULL,
	[F5] [nvarchar](255) NULL,
	[F6] [nvarchar](255) NULL,
	[F7] [nvarchar](255) NULL,
	[F8] [nvarchar](255) NULL,
	[F9] [float] NULL,
	[F10] [float] NULL,
	[F11] [nvarchar](255) NULL,
	[F12] [float] NULL,
	[F13] [nvarchar](255) NULL,
	[F14] [nvarchar](255) NULL,
	[F15] [nvarchar](255) NULL,
	[F16] [nvarchar](255) NULL,
	[F17] [nvarchar](255) NULL,
	[F18] [nvarchar](max) NULL,
	[F19] [nvarchar](255) NULL,
	[F20] [nvarchar](255) NULL,
	[F21] [nvarchar](255) NULL,
	[F22] [nvarchar](255) NULL,
	[F23] [nvarchar](255) NULL,
	[F24] [money] NULL,
	[F25] [money] NULL,
	[F26] [money] NULL,
	[F27] [money] NULL,
	[F28] [money] NULL,
	[F29] [money] NULL,
	[F30] [money] NULL,
	[F31] [money] NULL,
	[F32] [money] NULL,
	[F33] [money] NULL,
	[F34] [money] NULL,
	[F35] [money] NULL,
	[F36] [money] NULL,
	[F37] [money] NULL,
	[F38] [money] NULL,
	[F39] [money] NULL,
	[F40] [money] NULL,
	[F41] [money] NULL,
	[F42] [money] NULL,
	[F43] [money] NULL,
	[F44] [money] NULL,
	[F45] [nvarchar](255) NULL,
	[F46] [money] NULL,
	[F47] [money] NULL,
	[F48] [money] NULL,
	[F49] [money] NULL,
	[F50] [money] NULL,
	[F51] [money] NULL,
	[F52] [nvarchar](255) NULL,
	[F53] [money] NULL,
	[F54] [money] NULL,
	[F55] [money] NULL,
	[F56] [money] NULL,
	[F57] [money] NULL,
	[F58] [money] NULL,
	[F59] [money] NULL,
	[F60] [money] NULL,
	[F61] [money] NULL,
	[F62] [money] NULL,
	[F63] [money] NULL,
	[F64] [money] NULL,
	[F65] [money] NULL,
	[F66] [money] NULL,
	[F67] [money] NULL,
	[F68] [money] NULL,
	[F69] [money] NULL,
	[F70] [money] NULL,
	[F71] [money] NULL,
	[F72] [money] NULL,
	[F73] [money] NULL,
	[F74] [money] NULL,
	[F75] [money] NULL,
	[F76] [money] NULL,
	[F77] [money] NULL,
	[F78] [money] NULL,
	[F79] [money] NULL,
	[F80] [money] NULL,
	[F81] [money] NULL,
	[F82] [money] NULL,
	[F83] [money] NULL,
	[F84] [money] NULL,
	[F85] [money] NULL,
	[F86] [money] NULL,
	[F87] [money] NULL,
	[F88] [money] NULL,
	[F89] [money] NULL,
	[F90] [money] NULL,
	[F91] [money] NULL,
	[F92] [money] NULL,
	[F93] [money] NULL,
	[F94] [money] NULL,
	[F95] [money] NULL,
	[F96] [money] NULL,
	[F97] [money] NULL,
	[F98] [money] NULL,
	[F99] [money] NULL,
	[F100] [money] NULL,
	[F101] [money] NULL,
	[F102] [money] NULL,
	[F103] [money] NULL,
	[F104] [money] NULL,
	[F105] [money] NULL,
	[F106] [nvarchar](255) NULL,
	[F107] [money] NULL,
	[F108] [money] NULL,
	[F109] [money] NULL,
	[F110] [money] NULL,
	[F111] [money] NULL,
	[F112] [money] NULL,
	[F113] [money] NULL,
	[F114] [money] NULL,
	[F115] [money] NULL,
	[F116] [money] NULL,
	[F117] [money] NULL,
	[F118] [money] NULL,
	[F119] [money] NULL,
	[F120] [money] NULL,
	[F121] [money] NULL,
	[F122] [money] NULL,
	[F123] [money] NULL,
	[F124] [money] NULL,
	[F125] [money] NULL,
	[F126] [money] NULL,
	[F127] [money] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CS_distribucionOperaciones$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CS_distribucionOperaciones$](
	[F1] [nvarchar](255) NULL,
	[SAP ERP] [float] NULL,
	[SAP CRM 2#0] [float] NULL,
	[UNOEE] [float] NULL,
	[SLF] [float] NULL,
	[SAP PY (NOMINA)] [float] NULL,
	[SIPRES] [float] NULL,
	[SAP TL - EMPLOYEE CENTRAL] [float] NULL,
	[SAP PORTAL/KM] [float] NULL,
	[SAP BO/BW] [float] NULL,
	[SALES FORCE] [float] NULL,
	[SIM] [float] NULL,
	[DOCUWARE] [float] NULL,
	[SICLHO] [float] NULL,
	[SICS] [float] NULL,
	[SITRAZ (FN-MS)] [float] NULL,
	[VTEK (RENTING)] [float] NULL,
	[APLINSA] [float] NULL,
	[DARUMA 4] [float] NULL,
	[SSC] [float] NULL,
	[MES] [float] NULL,
	[SAP TL - OBJETIVOS Y DESEMPENO] [float] NULL,
	[SAP TL - INCORPORACION] [float] NULL,
	[SAP TL - RECLUTAMIENTO] [float] NULL,
	[SAP BPC] [float] NULL,
	[SOLIDO] [float] NULL,
	[SAP WPB] [float] NULL,
	[SISV] [float] NULL,
	[ESBIRRO MU] [float] NULL,
	[SAP TL - SUCESION Y DESARROLLO] [float] NULL,
	[INFOMANTE] [float] NULL,
	[SAP TL - APRENDIZAJE] [float] NULL,
	[SAP TL - COMPENSACION] [float] NULL,
	[SMART ACCESS] [float] NULL,
	[SAP TL - INFORMES] [float] NULL,
	[NOMINA SUPERTEX] [float] NULL,
	[BIABLE] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CS_Gente$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CS_Gente$](
	[C#costo] [float] NULL,
	[Descripción Centro de Costo] [nvarchar](255) NULL,
	[CO] [nvarchar](255) NULL,
	[Cedula] [float] NULL,
	[Primer y segundo apellido] [nvarchar](255) NULL,
	[Nombres] [nvarchar](255) NULL,
	[Nombre] [nvarchar](255) NULL,
	[Nombre del puesto] [nvarchar](255) NULL,
	[TOTAL FINAL COLABORADOR] [money] NULL,
	[F10] [nvarchar](255) NULL,
	[F11] [float] NULL,
	[F12] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CS_Licencias$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CS_Licencias$](
	[Ítem] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [float] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Licenciamiento] [money] NULL,
	[F8] [nvarchar](255) NULL,
	[F9] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CS_Servicios$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CS_Servicios$](
	[PRODUCTO] [nvarchar](255) NULL,
	[SERVICIOS] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DEDICACION$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEDICACION$](
	[SERVIDOR] [nvarchar](255) NULL,
	[AVILA SOTO ALEJANDRO] [float] NULL,
	[ERAZO CAICEDO HERNANDO] [float] NULL,
	[SALAZAR MARIN MILTON] [float] NULL,
	[JARAMILLO NARANJO JUAN PABLO] [float] NULL,
	[URIBE CASTRO ALEJANDRO] [float] NULL,
	[BALBUENA DUQUE JOVANNI] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Desarrollo$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Desarrollo$](
	[APLICACIÓN] [nvarchar](255) NULL,
	[SAP ERP] [float] NULL,
	[ANGELNET] [float] NULL,
	[BIABLE] [float] NULL,
	[SAP CRM 20] [float] NULL,
	[DOCUWARE] [float] NULL,
	[ECOMMERCE ARENA] [float] NULL,
	[ESPORA] [float] NULL,
	[LOGHO] [float] NULL,
	[PSE] [float] NULL,
	[SAP BO/BW] [float] NULL,
	[SAP INTEGRACIONES] [float] NULL,
	[SIM] [float] NULL,
	[SIPRES] [float] NULL,
	[SIR HERO] [float] NULL,
	[SITRAZ (FN-MS)] [float] NULL,
	[SIUX] [float] NULL,
	[SLF] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Distrib-Intermedios$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Distrib-Intermedios$'](
	[Servicios de Venta] [nvarchar](255) NULL,
	[Tipo] [nvarchar](255) NULL,
	[Componente] [nvarchar](255) NULL,
	[Ítem] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [float] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Intermedios] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DistribMASProcesos$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistribMASProcesos$](
	[PRODUCTO] [nvarchar](255) NULL,
	[Cant# Casos] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCALCULOGASTOSVIAJE]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCALCULOGASTOSVIAJE](
	[tari_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[tari_grupo] [int] NOT NULL,
	[tari_destino] [int] NOT NULL,
	[tari_hotel] [decimal](18, 6) NOT NULL,
	[tari_alimentacion] [decimal](18, 6) NOT NULL,
	[tari_estado] [int] NOT NULL,
	[tari_usuario] [varchar](30) NOT NULL,
	[tari_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TCALCULOGASTOSVIAJE] PRIMARY KEY CLUSTERED 
(
	[tari_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCARGUEARCHIVOS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCARGUEARCHIVOS](
	[carg_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[carg_producto] [int] NOT NULL,
	[carg_periodo] [int] NOT NULL,
	[carg_empresa] [varchar](100) NULL,
	[carg_ccosto] [varchar](50) NULL,
	[carg_user] [varchar](50) NULL,
	[carg_item] [varchar](50) NULL,
	[carg_equipo] [varchar](50) NULL,
	[carg_leasing] [varchar](50) NULL,
	[carg_papel] [varchar](50) NULL,
	[carg_mes] [varchar](50) NULL,
	[carg_proveedor] [varchar](50) NULL,
	[carg_cantidad] [varchar](50) NULL,
	[carg_valor] [decimal](18, 0) NULL,
	[carg_observacion] [varchar](500) NULL,
	[carg_usuario] [varchar](50) NULL,
	[carg_fecha] [datetime] NULL,
 CONSTRAINT [PK_GE_TCARGUEARCHIVOS] PRIMARY KEY CLUSTERED 
(
	[carg_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCARGUEARCHIVOSLABORAL]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCARGUEARCHIVOSLABORAL](
	[carl_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[carl_periodo] [int] NOT NULL,
	[carl_categoria] [varchar](50) NULL,
	[carl_subcategoria] [varchar](50) NULL,
	[carl_empresa] [varchar](150) NULL,
	[carl_ccostos] [varchar](50) NULL,
	[carl_producto] [varchar](200) NULL,
	[carl_item] [varchar](200) NULL,
	[carl_moneda] [varchar](50) NULL,
	[carl_valor] [int] NULL,
	[carl_cantidad] [int] NULL,
	[carl_enero] [varchar](50) NULL,
	[carl_febrero] [varchar](50) NULL,
	[carl_marzo] [varchar](50) NULL,
	[carl_abril] [varchar](50) NULL,
	[carl_mayo] [varchar](50) NULL,
	[carl_junio] [varchar](50) NULL,
	[carl_julio] [varchar](50) NULL,
	[carl_agosto] [varchar](50) NULL,
	[carl_septiembre] [varchar](50) NULL,
	[carl_octubre] [varchar](50) NULL,
	[carl_noviembre] [varchar](50) NULL,
	[carl_diciembre] [varchar](50) NULL,
	[carl_observaciones] [varchar](250) NULL,
	[carl_usuario] [varchar](50) NULL,
	[carl_fecha] [datetime] NULL,
 CONSTRAINT [PK_GE_TCARGUEARCHIVOSLABORAL] PRIMARY KEY CLUSTERED 
(
	[carl_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCARGUEDISTRIBUCION]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCARGUEDISTRIBUCION](
	[cadi_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[cadi_co_origen] [int] NULL,
	[cadi_co_destino] [int] NULL,
	[cadi_porcentaje] [decimal](18, 10) NULL,
	[cadi_usuario] [varchar](50) NULL,
	[cadi_fecha] [datetime] NULL,
	[cadi_usuario_act] [varchar](50) NULL,
	[cadi_fecha_act] [datetime] NULL,
	[cadi_activo] [int] NULL,
 CONSTRAINT [PK_GE_TCARGUEDISTRIBUCION] PRIMARY KEY CLUSTERED 
(
	[cadi_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCENTROCOSTOPERSONA]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCENTROCOSTOPERSONA](
	[cepe_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[cepe_ceco] [int] NOT NULL,
	[cepe_pers] [int] NOT NULL,
	[cepe_activo] [int] NOT NULL,
	[cepe_tipo] [varchar](5) NULL,
	[cepe_usuario] [varchar](30) NOT NULL,
	[cepe_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TCENTROCOSTOPERSONA] PRIMARY KEY CLUSTERED 
(
	[cepe_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TCUENTASCLASIFICACION]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TCUENTASCLASIFICACION](
	[cucl_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[cucl_cuenta] [int] NOT NULL,
	[cucl_ccosto] [int] NOT NULL,
	[cucl_subcategoria] [int] NOT NULL,
	[cucl_tipo] [int] NOT NULL,
	[cucl_usuario] [varchar](30) NOT NULL,
	[cucl_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TCUENTASCLASIFICACION] PRIMARY KEY CLUSTERED 
(
	[cucl_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TDELEGADOS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDELEGADOS](
	[dele_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[dele_jefe] [int] NOT NULL,
	[dele_delegado] [int] NOT NULL,
	[dele_usuario_crea] [varchar](50) NULL,
	[dele_usuario_act] [varchar](50) NULL,
	[dele_fecha] [datetime] NULL,
	[dele_fecha_act] [datetime] NULL,
	[dele_activo] [int] NULL,
	[dele_fase_parm] [int] NULL,
 CONSTRAINT [PK_GE_TDELEGADOS] PRIMARY KEY CLUSTERED 
(
	[dele_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TDISTRIBUCIONCARGUEGA]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA](
	[card_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[card_periodo] [int] NOT NULL,
	[card_producto] [int] NOT NULL,
	[card_ccosto] [int] NOT NULL,
	[card_valor] [decimal](18, 6) NOT NULL,
	[card_usuario] [varchar](50) NOT NULL,
	[card_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TDISTRIBUCIONCARGUEGA] PRIMARY KEY CLUSTERED 
(
	[card_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TDOMINIOS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDOMINIOS](
	[domi_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[domi_nombre] [varchar](100) NOT NULL,
	[domi_estado] [int] NOT NULL,
	[domi_fecha] [date] NULL,
	[domi_grupo] [varchar](200) NULL,
 CONSTRAINT [PK_DOMI_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[domi_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TDRIVERS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TDRIVERS](
	[driv_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[driv_nombre] [varchar](50) NULL,
	[driv_descripcion] [varchar](500) NULL,
	[driv_tipo_cobro] [varchar](1) NULL,
	[driv_aplica_sede] [varchar](1) NULL,
	[driv_aplica_valor] [varchar](1) NULL,
	[driv_aplica_proveedor] [varchar](1) NULL,
	[driv_usuario] [varchar](50) NULL,
	[driv_fecha] [date] NULL,
	[driv_activo] [varchar](1) NULL,
 CONSTRAINT [PK_GE_TDRIVERS] PRIMARY KEY CLUSTERED 
(
	[driv_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TFAMILIAS_PRODUCTOS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TFAMILIAS_PRODUCTOS](
	[fami_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[fam_producto] [int] NULL,
	[fam_padre] [int] NULL,
	[fam_fecha] [datetime] NULL,
	[fam_usuario] [varchar](50) NULL,
	[fam_estado] [int] NULL,
 CONSTRAINT [PK_GE_TFAMILIAS_PRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[fami_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TOPCIONESMENU]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TOPCIONESMENU](
	[opcm_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[opcm_idpadre] [int] NOT NULL,
	[opcm_idonma] [int] NOT NULL,
	[opcm_idpadreonma] [int] NOT NULL,
	[opcm_idobjetoonma] [int] NOT NULL,
	[opcm_titulo] [varchar](200) NOT NULL,
	[opcm_descripcion] [varchar](500) NULL,
	[opcm_url] [varchar](1000) NULL,
	[opcm_ruta_imagen] [varchar](1000) NULL,
	[opcm_estado] [int] NOT NULL,
	[opcm_fecha] [date] NOT NULL,
	[opcm_fecha_act] [date] NOT NULL,
	[opcm_tipo] [varchar](20) NOT NULL,
	[opcm_usuario] [varchar](50) NULL,
	[opcm_usuario_act] [varchar](50) NULL,
 CONSTRAINT [PK_OPCM_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[opcm_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TOPCIONESMENUXROL]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TOPCIONESMENUXROL](
	[opcr_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[rolm_consecutivo] [int] NOT NULL,
	[opcm_consecutivo] [int] NOT NULL,
	[opcr_fecha] [date] NULL,
	[opcr_usuario] [varchar](50) NULL,
 CONSTRAINT [PK_OPCR_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[opcr_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPERIODOTRANSACCPERSONAS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPERIODOTRANSACCPERSONAS](
	[ptrp_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[ptrp_periodo_transacc] [int] NOT NULL,
	[ptrp_persona] [int] NOT NULL,
	[ptrp_cantidad_dias] [int] NOT NULL,
	[ptrp_tarifa_hotel] [decimal](18, 6) NOT NULL,
	[ptrp_tarifa_alimentacion] [decimal](18, 6) NOT NULL,
	[ptrp_mes] [int] NOT NULL,
	[ptrp_smmlv] [decimal](18, 6) NOT NULL,
	[ptrp_usuario] [varchar](30) NOT NULL,
	[ptrp_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TPERIODOTRANSACCPERSONAS] PRIMARY KEY CLUSTERED 
(
	[ptrp_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPORCENTAJESPYG]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPORCENTAJESPYG](
	[hipo_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[hipo_historico_id] [int] NULL,
	[hipo_gastos_totales] [decimal](21, 10) NULL,
	[hipo_porc_ventas] [decimal](18, 10) NULL,
	[hipo_porc_directos] [decimal](18, 10) NULL,
	[hipo_porc_indirectos] [decimal](18, 10) NULL,
	[hipo_porc_total] [decimal](18, 10) NULL,
	[hipo_gastos_fore_totales] [decimal](21, 10) NULL,
	[hipo_porc_fore_ventas] [decimal](18, 10) NULL,
	[hipo_porc_fore_directos] [decimal](18, 10) NULL,
	[hipo_porc_fore_indirectos] [decimal](18, 10) NULL,
	[hipo_porc_fore_total] [decimal](18, 10) NULL,
	[hipo_usuario] [varchar](50) NULL,
	[hipo_fecha] [datetime] NULL,
	[hipo_usuario_act] [varchar](50) NULL,
	[hipo_fecha_act] [datetime] NULL,
	[hipo_activo] [int] NULL,
 CONSTRAINT [PK_GE_TPORCENTAJESPYG] PRIMARY KEY CLUSTERED 
(
	[hipo_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TPROVEEDORES]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TPROVEEDORES](
	[prov_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[prov_nombre] [varchar](500) NOT NULL,
	[prov_descripcion] [varchar](50) NOT NULL,
	[prov_activo] [int] NOT NULL,
	[prov_usuario] [varchar](30) NOT NULL,
	[prov_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TPROVEEDORES] PRIMARY KEY CLUSTERED 
(
	[prov_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TROLES]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TROLES](
	[rolm_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[rolm_nombre] [varchar](60) NOT NULL,
	[rolm_descripcion] [varchar](100) NULL,
	[rolm_estado] [int] NOT NULL,
	[rolm_fecha] [date] NOT NULL,
	[rolm_fecha_act] [date] NOT NULL,
	[rolm_usuario] [varchar](50) NULL,
	[rolm_usuario_act] [varchar](50) NULL,
	[rolm_grupo] [int] NULL,
	[rolm_dominio] [varchar](100) NULL,
 CONSTRAINT [PK_ROLM_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[rolm_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TUSUARIOS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TUSUARIOS](
	[USUA_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[USUA_USERNAME] [varchar](50) NULL,
	[USUA_CLAVE] [varchar](64) NULL,
	[USUA_DOMINIO] [varchar](100) NULL,
	[USUA_FECHA] [date] NULL,
	[USUA_HORA] [varchar](8) NULL,
	[USUA_ESTADO] [int] NOT NULL,
	[USUA_ENCRIPTA] [char](1) NULL,
	[USUA_FECHAVENCE] [date] NULL,
	[USUA_FECHACAMBIO] [date] NULL,
	[USUA_FECHA_ACT] [date] NULL,
	[USUA_USUARIO_INS] [varchar](50) NULL,
	[USUA_USUARIO_ACT] [varchar](50) NULL,
	[PARM_TIPOUSUARIO] [int] NULL,
	[USUA_VALIDALDAP] [char](1) NULL,
 CONSTRAINT [PK__USUA_USUARIO] PRIMARY KEY CLUSTERED 
(
	[USUA_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UX_GE_TUSUARIOS] UNIQUE NONCLUSTERED 
(
	[USUA_DOMINIO] ASC,
	[USUA_USERNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TUSUARIOS_ACCESOS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TUSUARIOS_ACCESOS](
	[usac_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[usua_usuario] [varchar](50) NOT NULL,
	[usac_fecha_ingreso] [date] NOT NULL,
	[usac_fecha_salida] [date] NULL,
	[usac_estado] [int] NOT NULL,
	[usac_tipo_salida] [varchar](50) NULL,
	[usac_hostname] [varchar](100) NULL,
	[usac_ip] [varchar](30) NULL,
	[usac_browser] [varchar](100) NULL,
	[usac_usuario_desbloqueo] [varchar](50) NULL,
	[usac_guid] [varchar](100) NULL,
	[usac_ipantesproxy] [varchar](100) NULL,
 CONSTRAINT [PK_USAC_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[usac_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TUSUARIOSXROL]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TUSUARIOSXROL](
	[usxr_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[rolm_consecutivo] [int] NOT NULL,
	[usua_usuario] [int] NOT NULL,
	[usxr_estado] [int] NOT NULL,
	[usxr_fecha] [date] NOT NULL,
	[usxr_fecha_act] [date] NOT NULL,
	[usxr_usuario] [varchar](50) NULL,
	[usxr_usuario_act] [varchar](50) NULL,
 CONSTRAINT [PK_USXR_CONSECUTIVO] PRIMARY KEY CLUSTERED 
(
	[usxr_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GE_TVARECONOMICAS]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GE_TVARECONOMICAS](
	[vari_consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[vari_tipo_moneda] [int] NOT NULL,
	[vari_ano] [int] NOT NULL,
	[vari_mes] [int] NOT NULL,
	[vari_valor] [decimal](16, 8) NOT NULL,
	[vari_activo] [int] NOT NULL,
	[vari_usuario] [varchar](50) NOT NULL,
	[vari_fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_GE_TVARECONOMICAS] PRIMARY KEY CLUSTERED 
(
	[vari_consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operaciones$]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operaciones$](
	[NOMBRES] [nvarchar](255) NULL,
	[SAP ERP] [float] NULL,
	[SAP CRM 2.0] [float] NULL,
	[UNOEE] [float] NULL,
	[SLF] [float] NULL,
	[SAP PY (NOMINA)] [float] NULL,
	[SIPRES] [float] NULL,
	[SAP TL - EMPLOYEE CENTRAL] [float] NULL,
	[SAP PORTAL/KM] [float] NULL,
	[SAP BO/BW] [float] NULL,
	[SALES FORCE] [float] NULL,
	[SIM] [float] NULL,
	[DOCUWARE] [float] NULL,
	[SICLHO] [float] NULL,
	[SICS] [float] NULL,
	[SITRAZ (FN-MS)] [float] NULL,
	[VTEK (RENTING)] [float] NULL,
	[APLINSA] [float] NULL,
	[DARUMA 4] [float] NULL,
	[SSC] [float] NULL,
	[MES] [float] NULL,
	[SAP TL - OBJETIVOS Y DESEMPEÑO] [float] NULL,
	[SAP TL - INCORPORACION] [float] NULL,
	[SAP TL - RECLUTAMIENTO] [float] NULL,
	[SAP BPC] [float] NULL,
	[SOLIDO] [float] NULL,
	[SAP WPB] [float] NULL,
	[SISV] [float] NULL,
	[ESBIRRO MU] [float] NULL,
	[SAP TL - SUCESION Y DESARROLLO] [float] NULL,
	[INFOMANTE] [float] NULL,
	[SAP TL - APRENDIZAJE] [float] NULL,
	[SAP TL - COMPENSACION] [float] NULL,
	[SMART ACCESS] [float] NULL,
	[SAP TL - INFORMES] [float] NULL,
	[NOMINA SUPERTEX] [float] NULL,
	[BIABLE] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Otros Gastos_V18$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Otros Gastos_V18$'](
	[Servicios de Venta] [nvarchar](255) NULL,
	[Tipo] [nvarchar](255) NULL,
	[Componente] [nvarchar](255) NULL,
	[Item] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [float] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Compañía] [varchar](50) NULL,
	[Centro de Costos] [float] NULL,
	[Moneda] [nvarchar](255) NULL,
	[Valor Presupuesto 2018] [money] NULL,
	[Cantidad] [money] NULL,
	[Mes Factura] [money] NULL,
	[Enero] [money] NULL,
	[Febrero] [money] NULL,
	[Marzo] [money] NULL,
	[Abril] [money] NULL,
	[Mayo] [money] NULL,
	[Junio] [money] NULL,
	[Julio] [money] NULL,
	[Agosto] [money] NULL,
	[Septiembre] [money] NULL,
	[Octubre] [money] NULL,
	[Noviembre] [money] NULL,
	[Diciembre] [money] NULL,
	[NumeroFila] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Otros GastosV20$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Otros GastosV20$'](
	[NumeroFila] [float] NULL,
	[Servicios de Venta] [nvarchar](255) NULL,
	[Tipo] [nvarchar](255) NULL,
	[Componente] [nvarchar](255) NULL,
	[Item] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [float] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Compañía] [nvarchar](255) NULL,
	[Centro de Costos] [float] NULL,
	[Moneda] [nvarchar](255) NULL,
	[Valor Presupuesto 2018] [money] NULL,
	[Cantidad] [float] NULL,
	[Mes Factura] [money] NULL,
	[Enero] [money] NULL,
	[Febrero] [money] NULL,
	[Marzo] [money] NULL,
	[Abril] [money] NULL,
	[Mayo] [money] NULL,
	[Junio] [money] NULL,
	[Julio] [money] NULL,
	[Agosto] [money] NULL,
	[Septiembre] [money] NULL,
	[Octubre] [money] NULL,
	[Noviembre] [money] NULL,
	[Diciembre] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PARAMETROSGRALES]    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PARAMETROSGRALES](
	[pmgr_parametro] [varchar](50) NOT NULL,
	[pmgr_descripcion] [varchar](200) NOT NULL,
	[pmgr_estado] [int] NOT NULL,
	[parm_grupo] [int] NOT NULL,
	[parm_tipodato] [int] NOT NULL,
 CONSTRAINT [PK_PMGR_PARAMETRO] PRIMARY KEY CLUSTERED 
(
	[pmgr_parametro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Productos presupuestados$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Productos presupuestados$'](
	[CATEGORIA DEL SERVICIO] [nvarchar](255) NULL,
	[Tipo] [nvarchar](255) NULL,
	[Componente] [nvarchar](255) NULL,
	[Ítem] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [float] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Compañía] [nvarchar](255) NULL,
	[Centro de Costos] [float] NULL,
	[Moneda] [nvarchar](255) NULL,
	[Valor Presupuesto 2017] [nvarchar](255) NULL,
	[Datos PPTO] [money] NULL,
	[Cantidad] [float] NULL,
	[Mes Factura] [nvarchar](255) NULL,
	[Enero] [money] NULL,
	[Febrero] [money] NULL,
	[Marzo] [money] NULL,
	[Abril] [money] NULL,
	[Mayo] [money] NULL,
	[Junio] [money] NULL,
	[Julio] [money] NULL,
	[Agosto] [money] NULL,
	[Septiembre] [money] NULL,
	[Octubre] [money] NULL,
	[Noviembre] [money] NULL,
	[Diciembre] [money] NULL,
	[tt] [nvarchar](255) NULL,
	[Ppto 2018] [money] NULL,
	[FC 2017] [money] NULL,
	[Variac] [money] NULL,
	[Ppto 2017] [money] NULL,
	[Variac1] [money] NULL,
	[F35] [nvarchar](255) NULL,
	[Servicios de Venta] [nvarchar](255) NULL,
	[Tipo1] [nvarchar](255) NULL,
	[Componente1] [nvarchar](255) NULL,
	[NumeroFila] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Productos presupuestados_V18$']    Script Date: 02/08/2019 17:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Productos presupuestados_V18$'](
	[CATEGORIA DEL SERVICIO] [nvarchar](255) NULL,
	[Tipo] [nvarchar](255) NULL,
	[Componente] [nvarchar](255) NULL,
	[Ítem] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [money] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Compañía] [nvarchar](255) NULL,
	[Centro de Costos] [money] NULL,
	[Moneda] [nvarchar](255) NULL,
	[Valor Presupuesto 2017] [money] NULL,
	[Datos PPTO] [money] NULL,
	[Cantidad] [money] NULL,
	[Mes Factura] [money] NULL,
	[Enero] [money] NULL,
	[Febrero] [money] NULL,
	[Marzo] [money] NULL,
	[Abril] [money] NULL,
	[Mayo] [money] NULL,
	[Junio] [money] NULL,
	[Julio] [money] NULL,
	[Agosto] [money] NULL,
	[Septiembre] [money] NULL,
	[Octubre] [money] NULL,
	[Noviembre] [money] NULL,
	[Diciembre] [money] NULL,
	[Ppto 2018] [money] NULL,
	[FC 2017] [money] NULL,
	[Variac] [money] NULL,
	[Ppto 2017] [money] NULL,
	[Variac1] [money] NULL,
	[F34] [nvarchar](255) NULL,
	[Servicios de Venta] [nvarchar](255) NULL,
	[Tipo1] [nvarchar](255) NULL,
	[Componente1] [nvarchar](255) NULL,
	[NumeroFila] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Productos presupuestadosV20$']    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Productos presupuestadosV20$'](
	[NumeroFila] [float] NULL,
	[CATEGORIA DEL SERVICIO] [nvarchar](255) NULL,
	[Categoria] [nvarchar](255) NULL,
	[Sub Categoria] [nvarchar](255) NULL,
	[Cecos] [float] NULL,
	[Tipo] [nvarchar](255) NULL,
	[Componente] [nvarchar](255) NULL,
	[Ítem] [nvarchar](255) NULL,
	[Producto] [nvarchar](255) NULL,
	[Cuenta] [nvarchar](255) NULL,
	[Auxiliar] [float] NULL,
	[Desc# Auxiliar] [nvarchar](255) NULL,
	[Responsable] [nvarchar](255) NULL,
	[Compañía] [nvarchar](255) NULL,
	[Centro de Costos] [float] NULL,
	[Moneda] [nvarchar](255) NULL,
	[Valor Presupuesto 2017] [nvarchar](255) NULL,
	[Datos PPTO] [money] NULL,
	[Cantidad] [float] NULL,
	[Mes Factura] [nvarchar](255) NULL,
	[Enero] [money] NULL,
	[Febrero] [money] NULL,
	[Marzo] [money] NULL,
	[Abril] [money] NULL,
	[Mayo] [money] NULL,
	[Junio] [money] NULL,
	[Julio] [money] NULL,
	[Agosto] [money] NULL,
	[Septiembre] [money] NULL,
	[Octubre] [money] NULL,
	[Noviembre] [money] NULL,
	[Diciembre] [money] NULL,
	[Ppto 2018] [money] NULL,
	[FC 2017] [money] NULL,
	[Variac] [money] NULL,
	[Ppto 2017] [money] NULL,
	[Variac1] [money] NULL,
	[F38] [nvarchar](255) NULL,
	[Servicios de Venta] [nvarchar](255) NULL,
	[Tipo1] [nvarchar](255) NULL,
	[Componente1] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Servidores Fisicos$']    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Servidores Fisicos$'](
	[F1] [nvarchar](255) NULL,
	[PPTO 2018] [nvarchar](255) NULL,
	[F3] [nvarchar](255) NULL,
	[F4] [nvarchar](255) NULL,
	[F5] [nvarchar](255) NULL,
	[F6] [nvarchar](255) NULL,
	[F7] [nvarchar](255) NULL,
	[F8] [nvarchar](255) NULL,
	[F9] [float] NULL,
	[F10] [float] NULL,
	[F11] [nvarchar](255) NULL,
	[F12] [float] NULL,
	[F13] [nvarchar](255) NULL,
	[F14] [nvarchar](255) NULL,
	[F15] [nvarchar](255) NULL,
	[F16] [nvarchar](255) NULL,
	[F17] [nvarchar](255) NULL,
	[F18] [nvarchar](max) NULL,
	[F19] [nvarchar](255) NULL,
	[F20] [nvarchar](255) NULL,
	[F21] [nvarchar](255) NULL,
	[F22] [nvarchar](255) NULL,
	[F23] [nvarchar](255) NULL,
	[F24] [nvarchar](255) NULL,
	[F25] [float] NULL,
	[F26] [float] NULL,
	[F27] [float] NULL,
	[F28] [nvarchar](255) NULL,
	[F29] [nvarchar](255) NULL,
	[F30] [nvarchar](255) NULL,
	[F31] [nvarchar](255) NULL,
	[F32] [nvarchar](255) NULL,
	[F33] [nvarchar](255) NULL,
	[F34] [nvarchar](255) NULL,
	[F35] [nvarchar](255) NULL,
	[F36] [nvarchar](255) NULL,
	[F37] [nvarchar](255) NULL,
	[F38] [nvarchar](255) NULL,
	[F39] [nvarchar](255) NULL,
	[F40] [nvarchar](255) NULL,
	[F41] [nvarchar](255) NULL,
	[F42] [float] NULL,
	[F43] [float] NULL,
	[F44] [nvarchar](255) NULL,
	[F45] [nvarchar](255) NULL,
	[F46] [float] NULL,
	[F47] [float] NULL,
	[F48] [float] NULL,
	[F49] [float] NULL,
	[F50] [float] NULL,
	[F51] [float] NULL,
	[F52] [nvarchar](255) NULL,
	[F53] [float] NULL,
	[F54] [float] NULL,
	[F55] [float] NULL,
	[F56] [float] NULL,
	[F57] [float] NULL,
	[F58] [float] NULL,
	[F59] [float] NULL,
	[F60] [float] NULL,
	[F61] [float] NULL,
	[F62] [float] NULL,
	[F63] [float] NULL,
	[F64] [float] NULL,
	[F65] [float] NULL,
	[F66] [float] NULL,
	[F67] [float] NULL,
	[F68] [float] NULL,
	[F69] [float] NULL,
	[F70] [float] NULL,
	[F71] [float] NULL,
	[F72] [float] NULL,
	[F73] [float] NULL,
	[F74] [float] NULL,
	[F75] [float] NULL,
	[F76] [float] NULL,
	[F77] [float] NULL,
	[F78] [float] NULL,
	[F79] [float] NULL,
	[F80] [float] NULL,
	[F81] [float] NULL,
	[F82] [float] NULL,
	[F83] [float] NULL,
	[F84] [float] NULL,
	[F85] [float] NULL,
	[F86] [float] NULL,
	[F87] [float] NULL,
	[F88] [float] NULL,
	[F89] [float] NULL,
	[F90] [float] NULL,
	[F91] [float] NULL,
	[F92] [float] NULL,
	[F93] [float] NULL,
	[F94] [float] NULL,
	[F95] [float] NULL,
	[F96] [float] NULL,
	[F97] [float] NULL,
	[F98] [float] NULL,
	[F99] [float] NULL,
	[F100] [float] NULL,
	[F101] [float] NULL,
	[F102] [float] NULL,
	[F103] [float] NULL,
	[F104] [float] NULL,
	[F105] [float] NULL,
	[F106] [nvarchar](255) NULL,
	[F107] [float] NULL,
	[F108] [float] NULL,
	[F109] [float] NULL,
	[F110] [float] NULL,
	[F111] [float] NULL,
	[F112] [float] NULL,
	[F113] [float] NULL,
	[F114] [float] NULL,
	[F115] [float] NULL,
	[F116] [float] NULL,
	[F117] [float] NULL,
	[F118] [float] NULL,
	[F119] [float] NULL,
	[F120] [float] NULL,
	[F121] [float] NULL,
	[F122] [float] NULL,
	[F123] [float] NULL,
	[F124] [float] NULL,
	[F125] [float] NULL,
	[F126] [float] NULL,
	[F127] [float] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Servidores-Datacenter$']    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Servidores-Datacenter$'](
	[Servidor] [nvarchar](255) NULL,
	[SERVICIO DE ENERGIA] [float] NULL,
	[SISTEMA CONTRA INCENDIO] [float] NULL,
	[SERVICIO TECNICO DE AIRE/UPS] [float] NULL,
	[SERVICIO DE CUSTODIA CINTAS] [float] NULL,
	[GARANTIA EXTENDIDA EQUIPOS DE COMUNICACIÓN CISCO] [float] NULL,
	[SEGURIDAD PERIMETRAL-FORTINET, BLUE COAT] [float] NULL,
	[ETHICAL HACKING] [float] NULL,
	[DEPREC#EQUIPO DE OFICINA] [float] NULL,
	[MANT# Y REPARAC# AIRE ACONDICIONADO] [float] NULL,
	[GTOS REPRES#ATENCIONES,PERIODICO] [float] NULL,
	[ACPM PLANTA ELECTRICA] [float] NULL,
	[OTRAS ADECUACIONES E INSTALACIONES] [float] NULL,
	[AMORT#SEGURO DE DAÑOS MATERIALES COMBINA] [float] NULL,
	[IMPUESTO DE ALUMBRADO PUBLICO] [float] NULL,
	[IMPUESTO INDUSTRIA Y COMERCIO] [float] NULL,
	[LICENCIAMIENTO GENERAL MICROSOFT (DATACENTER)] [float] NULL,
	[RESPALDO FISICO Y SOPORTE FIREWALL, BLUE COAT (PROXY)] [float] NULL,
	[TARJETA CONTROLADORA WIFI] [float] NULL,
	[SERVICIO GARANTIA EXTENDIDA EQUIPOS DE HP] [float] NULL,
	[CANON ENLACES DEDICADOS (DATACENTER)] [float] NULL,
	[AMORT# OTROS SEGURO DE EQUIPO ELECTRONICO] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Servidores-Infraestructura$']    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Servidores-Infraestructura$'](
	[Servidor] [nvarchar](255) NULL,
	[LICENCIAMIENTO GENERAL MICROSOFT (WINDOWS STANDARD)] [float] NULL,
	[SOPORTE MICROSOFT] [float] NULL,
	[LICENCIAS DE EXTERNAL CONECTOR] [float] NULL,
	[LICENCIAS AD AUDIT] [float] NULL,
	[LICENCIAMIENTO GENERAL MICROSOFT (RDP)] [float] NULL,
	[SOPORTE LINUX] [float] NULL,
	[LICENCIAS ORACLE (8)] [float] NULL,
	[AMORTIZACION INFRAESTRUCTURA DATACENTER NP1357] [float] NULL,
	[LICENCIAMIENTO ORACLE STANDARD ONE X 2] [float] NULL,
	[LICENCIAS IGNITE SOLARWINDS MONITOREO BDD] [float] NULL,
	[LICENCIAMIENTO TOAD BDD] [float] NULL,
	[LICENCIAMIENTO WEBLOGIC] [float] NULL,
	[LICENCIAMIENTO ORACLE LINUX 6.5] [float] NULL,
	[MCAFEE LIC. SERVER] [float] NULL,
	[LICENCIAS WAS (IBM WEBSHERE APPLICATION SERVER 8)] [float] NULL,
	[LICENCIAMIENTO VMWARE] [float] NULL,
	[LICENCIAMIENTO THIN PRINT] [float] NULL,
	[LICENCIAMIENTO PRTG NETWORK MONITOR] [float] NULL,
	[EQUIPO DE COMPUTO DATACENTER] [float] NULL,
	[GARANTIA EXTENDIDA EQUIPOS DATACENTER (HP)] [float] NULL,
	[LICENCIAMIENTO HYENA] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].['Servidores-Productos$']    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].['Servidores-Productos$'](
	[Servidor] [nvarchar](255) NULL,
	[Wsus - Actualizaciones SO] [float] NULL,
	[SIR HERO] [float] NULL,
	[ECOMMERCE ARENA] [float] NULL,
	[LOGHO] [float] NULL,
	[ANTIVIRUS] [float] NULL,
	[Administracion Usuarios/Auditoria Supertex] [float] NULL,
	[SSC] [float] NULL,
	[INTRANET TALENTO HUMANO] [float] NULL,
	[SLF] [float] NULL,
	[IMPRESIÓN] [float] NULL,
	[MES] [float] NULL,
	[ADSecurity] [float] NULL,
	[G-SUITE] [float] NULL,
	[SISV] [float] NULL,
	[LISTAS RESTRICTIVAS] [float] NULL,
	[EMPLEADO DEL SEMESTRE] [float] NULL,
	[SMART ACCESS] [float] NULL,
	[SAP INTEGRACIONES] [float] NULL,
	[ALFASIS] [float] NULL,
	[SITRAZ (FN-MS)] [float] NULL,
	[SICS] [float] NULL,
	[SIUX] [float] NULL,
	[APLINSA] [float] NULL,
	[7_2] [float] NULL,
	[8_5] [float] NULL,
	[UNOEE] [float] NULL,
	[APLICACIONES SUPERTEX] [float] NULL,
	[Administracion Usuarios/Auditoria] [float] NULL,
	[DYALOGO] [float] NULL,
	[CARPETAS COMPARTIDAS] [float] NULL,
	[MODULA WMS] [float] NULL,
	[POS] [float] NULL,
	[ESBIRRO MU] [float] NULL,
	[SIM] [float] NULL,
	[SIPRES] [float] NULL,
	[PSE] [float] NULL,
	[FTPS] [float] NULL,
	[SUMA SUPERTEX] [float] NULL,
	[QLIK VIEW] [float] NULL,
	[SOLIDO] [float] NULL,
	[INFOMANTE] [float] NULL,
	[DMS] [float] NULL,
	[QUERIX SRH] [float] NULL,
	[VTEK (Renting)] [float] NULL,
	[DOCUWARE] [float] NULL,
	[DARUMA 4] [float] NULL,
	[ESPORA] [float] NULL,
	[SERVICE DESK] [float] NULL,
	[SICLHO] [float] NULL,
	[APP PROGRESER] [float] NULL,
	[NOMINA SUPERTEX] [float] NULL,
	[QLICK SENSE] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbldedicacion]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbldedicacion](
	[idproducto] [int] NULL,
	[idpersona] [int] NULL,
	[valor] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestCargue]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCargue](
	[Documento] [varchar](50) NULL,
	[Nombres] [varchar](50) NULL,
	[Jefe] [varchar](50) NULL,
	[TipoContrato] [varchar](50) NULL,
	[Empresa] [varchar](50) NULL,
	[CentroCosto] [varchar](50) NULL,
	[UsuarioDominio] [varchar](50) NULL,
	[Area] [varchar](50) NULL,
	[NombreBusqueda] [varchar](50) NULL,
	[Estado] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[valores$]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[valores$](
	[Nombres] [nvarchar](255) NULL,
	[SAP ERP] [float] NULL,
	[SAP CRM 2#0] [float] NULL,
	[UNOEE] [float] NULL,
	[SLF] [float] NULL,
	[SAP PY (NOMINA)] [float] NULL,
	[SIPRES] [float] NULL,
	[SAP TL - EMPLOYEE CENTRAL] [float] NULL,
	[SAP PORTAL/KM] [float] NULL,
	[SAP BO/BW] [float] NULL,
	[SALES FORCE] [float] NULL,
	[SIM] [float] NULL,
	[DOCUWARE] [float] NULL,
	[SICLHO] [float] NULL,
	[SICS] [float] NULL,
	[SITRAZ (FN-MS)] [float] NULL,
	[VTEK (RENTING)] [float] NULL,
	[APLINSA] [float] NULL,
	[DARUMA 4] [float] NULL,
	[SSC] [float] NULL,
	[MES] [float] NULL,
	[SAP TL - OBJETIVOS Y DESEMPEÑO] [float] NULL,
	[SAP TL - INCORPORACION] [float] NULL,
	[SAP TL - RECLUTAMIENTO] [float] NULL,
	[SAP BPC] [float] NULL,
	[SOLIDO] [float] NULL,
	[SAP WPB] [float] NULL,
	[SISV] [float] NULL,
	[ESBIRRO MU] [float] NULL,
	[SAP TL - SUCESION Y DESARROLLO] [float] NULL,
	[INFOMANTE] [float] NULL,
	[SAP TL - APRENDIZAJE] [float] NULL,
	[SAP TL - COMPENSACION] [float] NULL,
	[SMART ACCESS] [float] NULL,
	[SAP TL - INFORMES] [float] NULL,
	[NOMINA SUPERTEX] [float] NULL,
	[BIABLE] [float] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GE_TCALCULOGASTOSVIAJE] ADD  CONSTRAINT [DF_GE_TCALCULOGASTOSVIAJE_tari_estado]  DEFAULT ((1)) FOR [tari_estado]
GO
ALTER TABLE [dbo].[GE_TCALCULOGASTOSVIAJE] ADD  CONSTRAINT [DF_GE_TCALCULOGASTOSVIAJE_tari_fecha]  DEFAULT (getdate()) FOR [tari_fecha]
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOS] ADD  CONSTRAINT [DF_GE_TCARGUEARCHIVOS_carg_fecha]  DEFAULT (getdate()) FOR [carg_fecha]
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOSLABORAL] ADD  CONSTRAINT [DF_GE_TCARGUEARCHIVOSLABORAL_carl_fecha]  DEFAULT (getdate()) FOR [carl_marzo]
GO
ALTER TABLE [dbo].[GE_TCENTROCOSTOPERSONA] ADD  CONSTRAINT [DF_GE_TCENTROCOSTOPERSONA_cepe_fecha]  DEFAULT (getdate()) FOR [cepe_fecha]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] ADD  CONSTRAINT [DF_GE_TCENTROSCOSTOS_cost_activo]  DEFAULT ((1)) FOR [cost_activo]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] ADD  CONSTRAINT [DF_GE_TCENTROSCOSTOS_cost_fecha]  DEFAULT (getdate()) FOR [cost_fecha]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] ADD  CONSTRAINT [DF_GE_TCENTROSCOSTOS_cost_fecha_act]  DEFAULT (getdate()) FOR [cost_fecha_act]
GO
ALTER TABLE [dbo].[GE_TCLASESPARAMETROS] ADD  DEFAULT ((1)) FOR [clap_estado]
GO
ALTER TABLE [dbo].[GE_TCUENTAS] ADD  CONSTRAINT [DF_GE_TCUENTAS_cuen_activo]  DEFAULT ((1)) FOR [cuen_activo]
GO
ALTER TABLE [dbo].[GE_TCUENTAS] ADD  CONSTRAINT [DF_GE_TCUENTAS_cuen_fecha]  DEFAULT (getdate()) FOR [cuen_fecha]
GO
ALTER TABLE [dbo].[GE_TCUENTAS] ADD  CONSTRAINT [DF_GE_TCUENTAS_cuen_amortizar]  DEFAULT ((0)) FOR [cuen_amortizar]
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION] ADD  CONSTRAINT [DF_GE_TCUENTASCLASIFICACION_cucl_fecha]  DEFAULT (getdate()) FOR [cucl_fecha]
GO
ALTER TABLE [dbo].[GE_TDOMINIOS] ADD  DEFAULT ((1)) FOR [domi_estado]
GO
ALTER TABLE [dbo].[GE_TOPCIONESMENU] ADD  DEFAULT ((1)) FOR [opcm_estado]
GO
ALTER TABLE [dbo].[GE_TPARAMETROS] ADD  DEFAULT ((1)) FOR [parm_estado]
GO
ALTER TABLE [dbo].[GE_TPERIODOPRESUPUESTO] ADD  CONSTRAINT [DF_GE_TPERIODOPRESUPUESTO_peri_fecha]  DEFAULT (getdate()) FOR [peri_fecha]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] ADD  CONSTRAINT [DF_GE_TPERIODOTRANSACCIONES_petr_activo]  DEFAULT ((1)) FOR [petr_activo]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] ADD  CONSTRAINT [DF_GE_TPERIODOTRANSACCIONES_petr_fecha]  DEFAULT (getdate()) FOR [petr_fecha]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] ADD  CONSTRAINT [DF_GE_TPERIODOTRANSACCIONES_petr_fecha_act]  DEFAULT (getdate()) FOR [petr_fecha_act]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCPERSONAS] ADD  CONSTRAINT [DF_GE_TPERIODOTRANSACCPERSONAS_ptrp_fecha]  DEFAULT (getdate()) FOR [ptrp_fecha]
GO
ALTER TABLE [dbo].[GE_TPERSONAS] ADD  CONSTRAINT [DF_GE_TPERSONAS_pers_activo]  DEFAULT ((1)) FOR [pers_activo]
GO
ALTER TABLE [dbo].[GE_TPERSONAS] ADD  CONSTRAINT [DF_GE_TPERSONAS_pers_fecha_act]  DEFAULT (getdate()) FOR [pers_fecha_act]
GO
ALTER TABLE [dbo].[GE_TPERSONAS] ADD  CONSTRAINT [DF_GE_TPERSONAS_pers_fecha]  DEFAULT (getdate()) FOR [pers_fecha]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] ADD  CONSTRAINT [DF_GE_TPRODUCTOS_prod_activo]  DEFAULT ((1)) FOR [prod_activo]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] ADD  CONSTRAINT [DF_GE_TPRODUCTOS_prod_fecha]  DEFAULT (getdate()) FOR [prod_fecha]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] ADD  CONSTRAINT [DF_GE_TPRODUCTOS_prod_fecha_act]  DEFAULT (getdate()) FOR [prod_fecha_act]
GO
ALTER TABLE [dbo].[GE_TPROVEEDORES] ADD  CONSTRAINT [DF_GE_TPROVEEDORES_prov_activo]  DEFAULT ((1)) FOR [prov_activo]
GO
ALTER TABLE [dbo].[GE_TPROVEEDORES] ADD  CONSTRAINT [DF_GE_TPROVEEDORES_prov_fecha]  DEFAULT (getdate()) FOR [prov_fecha]
GO
ALTER TABLE [dbo].[GE_TROLES] ADD  CONSTRAINT [DF__GE_TROLES__rolm___1A14E395]  DEFAULT ((1)) FOR [rolm_estado]
GO
ALTER TABLE [dbo].[GE_TROLES] ADD  CONSTRAINT [DF_GE_TROLES_rolm_fecha]  DEFAULT (getdate()) FOR [rolm_fecha]
GO
ALTER TABLE [dbo].[GE_TROLES] ADD  CONSTRAINT [DF_GE_TROLES_rolm_fecha_act]  DEFAULT (getdate()) FOR [rolm_fecha_act]
GO
ALTER TABLE [dbo].[GE_TUSUARIOS] ADD  CONSTRAINT [DF_GE_TUSUARIOS_USUA_HORA]  DEFAULT ('00:00:00') FOR [USUA_HORA]
GO
ALTER TABLE [dbo].[GE_TUSUARIOS] ADD  CONSTRAINT [DF_GE_TUSUARIOS_USUA_ESTADO]  DEFAULT ((1)) FOR [USUA_ESTADO]
GO
ALTER TABLE [dbo].[GE_TUSUARIOS] ADD  CONSTRAINT [DF_GE_TUSUARIOS_USUA_ENCRIPTA]  DEFAULT (N'S') FOR [USUA_ENCRIPTA]
GO
ALTER TABLE [dbo].[GE_TUSUARIOS] ADD  CONSTRAINT [DF_GE_TUSUARIOS_USUA_VALIDALDAP]  DEFAULT ('S') FOR [USUA_VALIDALDAP]
GO
ALTER TABLE [dbo].[GE_TUSUARIOS_ACCESOS] ADD  DEFAULT ((1)) FOR [usac_estado]
GO
ALTER TABLE [dbo].[GE_TUSUARIOSXROL] ADD  DEFAULT ((1)) FOR [usxr_estado]
GO
ALTER TABLE [dbo].[GE_TVARECONOMICAS] ADD  CONSTRAINT [DF_GE_TVARECONOMICAS_vari_activo]  DEFAULT ((1)) FOR [vari_activo]
GO
ALTER TABLE [dbo].[GE_TVARECONOMICAS] ADD  CONSTRAINT [DF_GE_TVARECONOMICAS_vari_fecha]  DEFAULT (getdate()) FOR [vari_fecha]
GO
ALTER TABLE [dbo].[PARAMETROSGRALES] ADD  DEFAULT ((1)) FOR [pmgr_estado]
GO
ALTER TABLE [dbo].[GE_TCALCULOGASTOSVIAJE]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCALCULOGASTOSVIAJE_DESTINO] FOREIGN KEY([tari_destino])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCALCULOGASTOSVIAJE] CHECK CONSTRAINT [FK_GE_TCALCULOGASTOSVIAJE_DESTINO]
GO
ALTER TABLE [dbo].[GE_TCALCULOGASTOSVIAJE]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCALCULOGASTOSVIAJE_GRUPO] FOREIGN KEY([tari_grupo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCALCULOGASTOSVIAJE] CHECK CONSTRAINT [FK_GE_TCALCULOGASTOSVIAJE_GRUPO]
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEARCHIVOS_PERIODO] FOREIGN KEY([carg_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOS] CHECK CONSTRAINT [FK_GE_TCARGUEARCHIVOS_PERIODO]
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEARCHIVOS_PRODUCTO] FOREIGN KEY([carg_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOS] CHECK CONSTRAINT [FK_GE_TCARGUEARCHIVOS_PRODUCTO]
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOSLABORAL]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEARCHIVOSLABORAL_PRESUPUESTO] FOREIGN KEY([carl_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEARCHIVOSLABORAL] CHECK CONSTRAINT [FK_GE_TCARGUEARCHIVOSLABORAL_PRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TCARGUEDISTRIBUCION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDISTRIBUCION_GE_TCARGUEDISTRIBUCION_2] FOREIGN KEY([cadi_co_destino])
REFERENCES [dbo].[GE_TCENTROSOPERACION] ([ceop_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDISTRIBUCION] CHECK CONSTRAINT [FK_GE_TCARGUEDISTRIBUCION_GE_TCARGUEDISTRIBUCION_2]
GO
ALTER TABLE [dbo].[GE_TCARGUEDISTRIBUCION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDISTRIBUCION_GE_TCENTROSOPERACION_1] FOREIGN KEY([cadi_co_origen])
REFERENCES [dbo].[GE_TCENTROSOPERACION] ([ceop_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDISTRIBUCION] CHECK CONSTRAINT [FK_GE_TCARGUEDISTRIBUCION_GE_TCENTROSOPERACION_1]
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TCENTROCOSTOS] FOREIGN KEY([carg_ccosto])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS] CHECK CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TCENTROCOSTOS]
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TCOMPANIAS] FOREIGN KEY([carg_compania])
REFERENCES [dbo].[GE_TCOMPANIAS] ([comp_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS] CHECK CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TCOMPANIAS]
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TDRIVERS] FOREIGN KEY([carg_driver])
REFERENCES [dbo].[GE_TDRIVERS] ([driv_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS] CHECK CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TDRIVERS]
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TPERIODO] FOREIGN KEY([carg_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS] CHECK CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TPERIODO]
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TPRODUCTOS] FOREIGN KEY([carg_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCARGUEDRIVERS] CHECK CONSTRAINT [FK_GE_TCARGUEDRIVERS_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_CATEGORIA] FOREIGN KEY([cost_consec_categoria])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_CATEGORIA]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TCENTROSCOSTOS] FOREIGN KEY([cost_consecutivo])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TCENTROSCOSTOS]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TCOMPANIAS] FOREIGN KEY([cost_empresa])
REFERENCES [dbo].[GE_TCOMPANIAS] ([comp_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TCOMPANIAS]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TPARAMETROS] FOREIGN KEY([cost_tipo_cliente])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TPARAMETROS]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TPARAMETROS2] FOREIGN KEY([cost_tipo_distribucion])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_GE_TPARAMETROS2]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_PERSONAS] FOREIGN KEY([cost_consec_responsable])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_PERSONAS]
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCENTROSCOSTOS_RESP_PPTO] FOREIGN KEY([cost_consec_resp_ppto])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCENTROSCOSTOS] CHECK CONSTRAINT [FK_GE_TCENTROSCOSTOS_RESP_PPTO]
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_CENTROCOSTO] FOREIGN KEY([cucl_ccosto])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION] CHECK CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_CENTROCOSTO]
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_CUENTA] FOREIGN KEY([cucl_cuenta])
REFERENCES [dbo].[GE_TCUENTAS] ([cuen_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION] CHECK CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_CUENTA]
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_SUBCATEGORIA] FOREIGN KEY([cucl_subcategoria])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION] CHECK CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_SUBCATEGORIA]
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_TIPO] FOREIGN KEY([cucl_tipo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TCUENTASCLASIFICACION] CHECK CONSTRAINT [FK_GE_TCUENTASCLASIFICACION_TIPO]
GO
ALTER TABLE [dbo].[GE_TDELEGADOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDELEGADOS_GE_TPARAMETROS] FOREIGN KEY([dele_fase_parm])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDELEGADOS] CHECK CONSTRAINT [FK_GE_TDELEGADOS_GE_TPARAMETROS]
GO
ALTER TABLE [dbo].[GE_TDELEGADOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDELEGADOS_GE_TPERSONAS_DELEGADO] FOREIGN KEY([dele_delegado])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDELEGADOS] CHECK CONSTRAINT [FK_GE_TDELEGADOS_GE_TPERSONAS_DELEGADO]
GO
ALTER TABLE [dbo].[GE_TDELEGADOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDELEGADOS_GE_TPERSONAS_JEFE] FOREIGN KEY([dele_jefe])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDELEGADOS] CHECK CONSTRAINT [FK_GE_TDELEGADOS_GE_TPERSONAS_JEFE]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONCARGUEGA_GE_TCENTROSCOSTOS] FOREIGN KEY([card_ccosto])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONCARGUEGA_GE_TCENTROSCOSTOS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONCARGUEGA_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([card_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONCARGUEGA_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONCARGUEGA_GE_TPRODUCTOS] FOREIGN KEY([card_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONCARGUEGA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONCARGUEGA_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONDEDICACIONPERSONA_GE_TDISTRIBUCIONDEDICACIONPERSONA] FOREIGN KEY([dper_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONDEDICACIONPERSONA_GE_TDISTRIBUCIONDEDICACIONPERSONA]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONDEDICACIONPERSONA_GE_TPERSONAS] FOREIGN KEY([dper_persona])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONDEDICACIONPERSONA_GE_TPERSONAS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONDEDICACIONPERSONA_GE_TPRODUCTOS] FOREIGN KEY([dper_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONDEDICACIONPERSONA_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINFRAESTRUCTURA_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([dinf_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINFRAESTRUCTURA_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINFRAESTRUCTURA_GE_TPRODUCTOS] FOREIGN KEY([dinf_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINFRAESTRUCTURA_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINFRAESTRUCTURA_GE_TPRODUCTOSITEMS] FOREIGN KEY([dinf_producto_item])
REFERENCES [dbo].[GE_TPRODUCTOSITEMS] ([prit_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINFRAESTRUCTURA_GE_TPRODUCTOSITEMS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([dint_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_GE_TPRODUCTOS_INTERM] FOREIGN KEY([dint_producto_intermedio])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_GE_TPRODUCTOS_INTERM]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_GE_TPRODUCTOSDIR] FOREIGN KEY([dint_producto_directo])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_GE_TPRODUCTOSDIR]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_ITEM_INTERMEDIO] FOREIGN KEY([dint_item_intermedio])
REFERENCES [dbo].[GE_TPRODUCTOSITEMS] ([prit_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONINTERMEDIOS] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONINTERMEDIOS_ITEM_INTERMEDIO]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONMASPROCESOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONMASPROCESOS_GE_TDISTRIBUCIONMASPROCESOS] FOREIGN KEY([dmas_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONMASPROCESOS] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONMASPROCESOS_GE_TDISTRIBUCIONMASPROCESOS]
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONMASPROCESOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TDISTRIBUCIONMASPROCESOS_GE_TPRODUCTOS] FOREIGN KEY([dmas_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TDISTRIBUCIONMASPROCESOS] CHECK CONSTRAINT [FK_GE_TDISTRIBUCIONMASPROCESOS_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TFAMILIAS_PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TFAMILIAS_PRODUCTOS_GE_TPRODUCTOS] FOREIGN KEY([fam_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TFAMILIAS_PRODUCTOS] CHECK CONSTRAINT [FK_GE_TFAMILIAS_PRODUCTOS_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TFAMILIAS_PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TFAMILIAS_PRODUCTOS_GE_TPRODUCTOS1] FOREIGN KEY([fam_padre])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TFAMILIAS_PRODUCTOS] CHECK CONSTRAINT [FK_GE_TFAMILIAS_PRODUCTOS_GE_TPRODUCTOS1]
GO
ALTER TABLE [dbo].[GE_TGENTE]  WITH CHECK ADD  CONSTRAINT [FK_GE_TGENTE_GE_TCENTROSCOSTOS] FOREIGN KEY([gent_ccostos])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TGENTE] CHECK CONSTRAINT [FK_GE_TGENTE_GE_TCENTROSCOSTOS]
GO
ALTER TABLE [dbo].[GE_TGENTE]  WITH CHECK ADD  CONSTRAINT [FK_GE_TGENTE_GE_TCENTROSOPERACION] FOREIGN KEY([gent_ceop])
REFERENCES [dbo].[GE_TCENTROSOPERACION] ([ceop_consecutivo])
GO
ALTER TABLE [dbo].[GE_TGENTE] CHECK CONSTRAINT [FK_GE_TGENTE_GE_TCENTROSOPERACION]
GO
ALTER TABLE [dbo].[GE_TGENTE]  WITH CHECK ADD  CONSTRAINT [FK_GE_TGENTE_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([gent_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TGENTE] CHECK CONSTRAINT [FK_GE_TGENTE_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TGENTE]  WITH CHECK ADD  CONSTRAINT [FK_GE_TGENTE_GE_TPERSONAS] FOREIGN KEY([gent_persona])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TGENTE] CHECK CONSTRAINT [FK_GE_TGENTE_GE_TPERSONAS]
GO
ALTER TABLE [dbo].[GE_THISTORICOPYG]  WITH CHECK ADD  CONSTRAINT [FK_GE_THISTORICOPYG_GE_TCENTROSOPERACION] FOREIGN KEY([vent_ceop])
REFERENCES [dbo].[GE_TCENTROSOPERACION] ([ceop_consecutivo])
GO
ALTER TABLE [dbo].[GE_THISTORICOPYG] CHECK CONSTRAINT [FK_GE_THISTORICOPYG_GE_TCENTROSOPERACION]
GO
ALTER TABLE [dbo].[GE_THISTORICOPYG]  WITH CHECK ADD  CONSTRAINT [FK_GE_THISTORICOPYG_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([vent_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_THISTORICOPYG] CHECK CONSTRAINT [FK_GE_THISTORICOPYG_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TOPCIONESMENUXROL]  WITH CHECK ADD  CONSTRAINT [FK_OPCR_OPCM_CONSECUTIVO] FOREIGN KEY([opcm_consecutivo])
REFERENCES [dbo].[GE_TOPCIONESMENU] ([opcm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TOPCIONESMENUXROL] CHECK CONSTRAINT [FK_OPCR_OPCM_CONSECUTIVO]
GO
ALTER TABLE [dbo].[GE_TOPCIONESMENUXROL]  WITH CHECK ADD  CONSTRAINT [FK_OPCR_ROLM_CONSECUTIVO] FOREIGN KEY([rolm_consecutivo])
REFERENCES [dbo].[GE_TROLES] ([rolm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TOPCIONESMENUXROL] CHECK CONSTRAINT [FK_OPCR_ROLM_CONSECUTIVO]
GO
ALTER TABLE [dbo].[GE_TPARAMETROS]  WITH CHECK ADD  CONSTRAINT [FK_PARM_CLAP_CLASE] FOREIGN KEY([clap_clase])
REFERENCES [dbo].[GE_TCLASESPARAMETROS] ([clap_clase])
GO
ALTER TABLE [dbo].[GE_TPARAMETROS] CHECK CONSTRAINT [FK_PARM_CLAP_CLASE]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_CENTROCOSTO] FOREIGN KEY([petr_centrocosto])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_CENTROCOSTO]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_MONEDA] FOREIGN KEY([petr_moneda])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_MONEDA]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PERIODO] FOREIGN KEY([petr_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PERIODO]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PERSONA] FOREIGN KEY([petr_persona])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PERSONA]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PRODUCTOITEM] FOREIGN KEY([petr_producto_item])
REFERENCES [dbo].[GE_TPRODUCTOSITEMS] ([prit_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PRODUCTOITEM]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PROVEEDOR] FOREIGN KEY([petr_proveedor])
REFERENCES [dbo].[GE_TPROVEEDORES] ([prov_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_PROVEEDOR]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_TIPOVIAJE] FOREIGN KEY([petr_tipo_viaje])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCIONES] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCIONES_TIPOVIAJE]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCPERSONAS_PERIODOTRANS] FOREIGN KEY([ptrp_periodo_transacc])
REFERENCES [dbo].[GE_TPERIODOTRANSACCIONES] ([petr_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCPERSONAS] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCPERSONAS_PERIODOTRANS]
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERIODOTRANSACCPERSONAS_PERSONAS] FOREIGN KEY([ptrp_persona])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERIODOTRANSACCPERSONAS] CHECK CONSTRAINT [FK_GE_TPERIODOTRANSACCPERSONAS_PERSONAS]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_CARGO] FOREIGN KEY([pers_cargo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_CARGO]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_CCOSTO] FOREIGN KEY([pers_ccosto])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_CCOSTO]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_EMPRESA] FOREIGN KEY([pers_empresa])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_EMPRESA]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_GE_TCENTROSOPERACION] FOREIGN KEY([pers_ceop])
REFERENCES [dbo].[GE_TCENTROSOPERACION] ([ceop_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_GE_TCENTROSOPERACION]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_GE_TPARAMETROS] FOREIGN KEY([pers_nombre_area])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_GE_TPARAMETROS]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_GRUPO] FOREIGN KEY([pers_grupo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_GRUPO]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_JEFE] FOREIGN KEY([pers_consec_jefe])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_JEFE]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_METODO] FOREIGN KEY([pers_metodo_distrib])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_METODO]
GO
ALTER TABLE [dbo].[GE_TPERSONAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPERSONAS_TIPOCONT] FOREIGN KEY([pers_tipo_contrato])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPERSONAS] CHECK CONSTRAINT [FK_GE_TPERSONAS_TIPOCONT]
GO
ALTER TABLE [dbo].[GE_TPORCENTAJESPYG]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPORCENTAJESPYG_GE_THISTORICOPYG1] FOREIGN KEY([hipo_historico_id])
REFERENCES [dbo].[GE_THISTORICOPYG] ([vent_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPORCENTAJESPYG] CHECK CONSTRAINT [FK_GE_TPORCENTAJESPYG_GE_THISTORICOPYG1]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_CATEG_SERV] FOREIGN KEY([prod_categ_serv])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_CATEG_SERV]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_COMPONENTE] FOREIGN KEY([prod_componente])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_COMPONENTE]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_CRITERIO] FOREIGN KEY([prod_criterio])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_CRITERIO]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_DRIVER1] FOREIGN KEY([prod_driver1])
REFERENCES [dbo].[GE_TDRIVERS] ([driv_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_DRIVER1]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_DRIVER2] FOREIGN KEY([prod_driver2])
REFERENCES [dbo].[GE_TDRIVERS] ([driv_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_DRIVER2]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_GE_TPARAMETROS] FOREIGN KEY([prod_tipo_producto])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_GE_TPARAMETROS]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_RESPONSABLE] FOREIGN KEY([prod_responsable])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_RESPONSABLE]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_SERV_VENTA] FOREIGN KEY([prod_serv_venta])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_SERV_VENTA]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_TIPO] FOREIGN KEY([prod_tipo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_TIPO]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOS_TIPO_LICENCIA] FOREIGN KEY([prod_tipo_licencia])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOS] CHECK CONSTRAINT [FK_GE_TPRODUCTOS_TIPO_LICENCIA]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOSITEMS]  WITH NOCHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOSITEMS_CUENTAS] FOREIGN KEY([prit_cuenta])
REFERENCES [dbo].[GE_TCUENTAS] ([cuen_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOSITEMS] NOCHECK CONSTRAINT [FK_GE_TPRODUCTOSITEMS_CUENTAS]
GO
ALTER TABLE [dbo].[GE_TPRODUCTOSITEMS]  WITH NOCHECK ADD  CONSTRAINT [FK_GE_TPRODUCTOSITEMS_PRODUCTO] FOREIGN KEY([prit_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TPRODUCTOSITEMS] NOCHECK CONSTRAINT [FK_GE_TPRODUCTOSITEMS_PRODUCTO]
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TREDISTRIBUCION_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([redi_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION] CHECK CONSTRAINT [FK_GE_TREDISTRIBUCION_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TREDISTRIBUCION_GE_TPRODUCTOS] FOREIGN KEY([redi_producto_orig])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION] CHECK CONSTRAINT [FK_GE_TREDISTRIBUCION_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION]  WITH CHECK ADD  CONSTRAINT [FK_GE_TREDISTRIBUCION_GE_TPRODUCTOS1] FOREIGN KEY([redi_producto_dist])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION] CHECK CONSTRAINT [FK_GE_TREDISTRIBUCION_GE_TPRODUCTOS1]
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION_DRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TREDISTRIBUCION_DRIVERS_GE_TCARGUEDRIVERS] FOREIGN KEY([care_cargue_driver])
REFERENCES [dbo].[GE_TCARGUEDRIVERS] ([carg_consecutivo])
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION_DRIVERS] CHECK CONSTRAINT [FK_GE_TREDISTRIBUCION_DRIVERS_GE_TCARGUEDRIVERS]
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION_DRIVERS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TREDISTRIBUCION_DRIVERS_GE_TCENTROSOPERACION] FOREIGN KEY([care_ceop_id])
REFERENCES [dbo].[GE_TCENTROSOPERACION] ([ceop_consecutivo])
GO
ALTER TABLE [dbo].[GE_TREDISTRIBUCION_DRIVERS] CHECK CONSTRAINT [FK_GE_TREDISTRIBUCION_DRIVERS_GE_TCENTROSOPERACION]
GO
ALTER TABLE [dbo].[GE_TRELITEMSDATACENTERPROD]  WITH CHECK ADD  CONSTRAINT [FK_GE_TRELITEMSDATACENTERPROD_GE_TPERIODOPRESUPUESTO] FOREIGN KEY([drel_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TRELITEMSDATACENTERPROD] CHECK CONSTRAINT [FK_GE_TRELITEMSDATACENTERPROD_GE_TPERIODOPRESUPUESTO]
GO
ALTER TABLE [dbo].[GE_TRELITEMSDATACENTERPROD]  WITH CHECK ADD  CONSTRAINT [FK_GE_TRELITEMSDATACENTERPROD_GE_TPRODUCTOS] FOREIGN KEY([drel_producto])
REFERENCES [dbo].[GE_TPRODUCTOS] ([prod_consecutivo])
GO
ALTER TABLE [dbo].[GE_TRELITEMSDATACENTERPROD] CHECK CONSTRAINT [FK_GE_TRELITEMSDATACENTERPROD_GE_TPRODUCTOS]
GO
ALTER TABLE [dbo].[GE_TRELITEMSDATACENTERPROD]  WITH CHECK ADD  CONSTRAINT [FK_GE_TRELITEMSDATACENTERPROD_GE_TPRODUCTOSITEMS] FOREIGN KEY([drel_item_datacenter])
REFERENCES [dbo].[GE_TPRODUCTOSITEMS] ([prit_consecutivo])
GO
ALTER TABLE [dbo].[GE_TRELITEMSDATACENTERPROD] CHECK CONSTRAINT [FK_GE_TRELITEMSDATACENTERPROD_GE_TPRODUCTOSITEMS]
GO
ALTER TABLE [dbo].[GE_TROLES]  WITH CHECK ADD  CONSTRAINT [FK_GE_TROLES_GE_TPARAMETROS] FOREIGN KEY([rolm_grupo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TROLES] CHECK CONSTRAINT [FK_GE_TROLES_GE_TPARAMETROS]
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO]  WITH CHECK ADD  CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_CCOSTO] FOREIGN KEY([sali_centrocosto])
REFERENCES [dbo].[GE_TCENTROSCOSTOS] ([cost_consecutivo])
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO] CHECK CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_CCOSTO]
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO]  WITH CHECK ADD  CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_PERIODO] FOREIGN KEY([sali_periodo])
REFERENCES [dbo].[GE_TPERIODOPRESUPUESTO] ([peri_consecutivo])
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO] CHECK CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_PERIODO]
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO]  WITH CHECK ADD  CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_PERSONA] FOREIGN KEY([sali_persona])
REFERENCES [dbo].[GE_TPERSONAS] ([pers_consecutivo])
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO] CHECK CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_PERSONA]
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO]  WITH CHECK ADD  CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_PRODUCTOITEM] FOREIGN KEY([sali_producto_item])
REFERENCES [dbo].[GE_TPRODUCTOSITEMS] ([prit_consecutivo])
GO
ALTER TABLE [dbo].[GE_TSALIDAPRESUPUESTO] CHECK CONSTRAINT [FK_GE_TSALIDAPRESUPUESTO_PRODUCTOITEM]
GO
ALTER TABLE [dbo].[GE_TUSUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_USUA_PARM_TIPOUSUARIO] FOREIGN KEY([PARM_TIPOUSUARIO])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TUSUARIOS] CHECK CONSTRAINT [FK_USUA_PARM_TIPOUSUARIO]
GO
ALTER TABLE [dbo].[GE_TUSUARIOSXROL]  WITH CHECK ADD  CONSTRAINT [FK_USXR_ROLM_CONSECUTIVO] FOREIGN KEY([rolm_consecutivo])
REFERENCES [dbo].[GE_TROLES] ([rolm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TUSUARIOSXROL] CHECK CONSTRAINT [FK_USXR_ROLM_CONSECUTIVO]
GO
ALTER TABLE [dbo].[GE_TUSUARIOSXROL]  WITH CHECK ADD  CONSTRAINT [FK_USXR_USUA_USUARIO] FOREIGN KEY([usua_usuario])
REFERENCES [dbo].[GE_TUSUARIOS] ([USUA_USUARIO])
GO
ALTER TABLE [dbo].[GE_TUSUARIOSXROL] CHECK CONSTRAINT [FK_USXR_USUA_USUARIO]
GO
ALTER TABLE [dbo].[GE_TVARECONOMICAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TVARECONOMICAS_MES] FOREIGN KEY([vari_mes])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TVARECONOMICAS] CHECK CONSTRAINT [FK_GE_TVARECONOMICAS_MES]
GO
ALTER TABLE [dbo].[GE_TVARECONOMICAS]  WITH CHECK ADD  CONSTRAINT [FK_GE_TVARECONOMICAS_MONEDA] FOREIGN KEY([vari_tipo_moneda])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[GE_TVARECONOMICAS] CHECK CONSTRAINT [FK_GE_TVARECONOMICAS_MONEDA]
GO
ALTER TABLE [dbo].[PARAMETROSGRALES]  WITH CHECK ADD  CONSTRAINT [FK_PMGR_PARM_GRUPO] FOREIGN KEY([parm_grupo])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[PARAMETROSGRALES] CHECK CONSTRAINT [FK_PMGR_PARM_GRUPO]
GO
ALTER TABLE [dbo].[PARAMETROSGRALES]  WITH CHECK ADD  CONSTRAINT [FK_PMGR_PARM_TIPODATO] FOREIGN KEY([parm_tipodato])
REFERENCES [dbo].[GE_TPARAMETROS] ([parm_consecutivo])
GO
ALTER TABLE [dbo].[PARAMETROSGRALES] CHECK CONSTRAINT [FK_PMGR_PARM_TIPODATO]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarCuadroBPC]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JLopez>
-- Create date: <17/08/2018>
-- Description:	<Procedimiento para actualizar cuadro BPC>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ActualizarCuadroBPC]
AS
BEGIN
	DELETE FROM GE_TVW_BPC_SALIDA;

	INSERT INTO GE_TVW_BPC_SALIDA(
		bpc_id,
		bpc_prod_desc,
		bpc_padre,
		bpc_tipo_cuenta
	)
	(SELECT	
		'T_PROD' ID, 
		'Total Producto' DESCRIPCION, 
		null PADRE, 
		'EXP' TipoCuenta
	);	

	INSERT INTO GE_TVW_BPC_SALIDA(
		bpc_id,
		bpc_prod_desc,
		bpc_padre,
		bpc_tipo_cuenta
	)
	(SELECT 
			'SR_' + CAST(parm_consecutivo as VARCHAR(30)),
			parm_descripcion,
			'T_PROD',
			'EXP'
	FROM VW_BPC_SERVICIOS
	);	

	INSERT INTO GE_TVW_BPC_SALIDA(
		bpc_id,
		bpc_prod_desc,
		bpc_padre,
		bpc_tipo_cuenta
	)
	(SELECT 
			'PR_' + CAST(prod_consecutivo as VARCHAR(30)),
			prod_descripcion,
			'SR_' + CAST(parm_consecutivo as VARCHAR(30)),
			'EXP'
	FROM VW_BPC_PRODUCTOS
	);	

	INSERT INTO GE_TVW_BPC_SALIDA(
		bpc_id,
		bpc_prod_desc,
		bpc_padre,
		bpc_tipo_cuenta
	)
	(SELECT 
			'PR_' + CAST(prod_consecutivo as VARCHAR(30)),
			prod_descripcion,
			'SR_' + CAST(parm_consecutivo as VARCHAR(30)),
			'EXP'
	FROM VW_BPC_PRODUCTOS
	);	

	INSERT INTO GE_TVW_BPC_SALIDA(
		bpc_id,
		bpc_prod_desc,
		bpc_padre,
		bpc_tipo_cuenta
	)
	(SELECT DISTINCT
			'TP_' + CAST(Idproducto as VARCHAR(30))+ '_' + CAST(idTipo as VARCHAR(30)),
			Tipo,
			'PR_' + CAST(Idproducto as VARCHAR(30)),
			'EXP'
	FROM VW_BPC_TIPOS_SALIDA
	);	

	INSERT INTO GE_TVW_BPC_SALIDA(
		bpc_id,
		bpc_prod_desc,
		bpc_padre,
		bpc_tipo_cuenta
	)
	(SELECT 		
			'ST_' + CAST(idTipo as VARCHAR(30)) + '_' + CAST(IdSubTipo as VARCHAR(30)),
			SubTipo,
			'TP_' + CAST(Idproducto as VARCHAR(30)) + '_' + CAST(idTipo as VARCHAR(30)),
			'EXP'
	FROM VW_BPC_TIPOS_SALIDA
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarCuadroServicio]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jonathan López
-- Create date: 01 Ago, 2018
-- Description:	Procedimiento que guarda la vista VW_VLR_CUADRO_SERVICIO 
--				en la tabla GE_TVLR_CUADRO_SERVICIO (Para Reporte "Salida Cuadro Servicio")
-- =============================================
CREATE PROCEDURE [dbo].[sp_ActualizarCuadroServicio]
AS
BEGIN
	DELETE FROM GE_TVLR_CUADRO_SERVICIO;
	
	INSERT INTO GE_TVLR_CUADRO_SERVICIO(
		idprod
		  ,producto
		  ,item
		  ,vlr_lic
		  ,vlr_int_lic
		  ,vlr_infr
		  ,vlr_datac
		  ,vlr_mas_ga
		  ,vlr_mas_softw
		  ,vlr_mas_infr
		  ,vlr_mas_datac
		  ,vlr_mas_gente
		  ,vlr_ga_operaciones
		  ,vlr_ga_gtecnica
		  ,vlr_ga_desarrollo
		  ,vlr_mas_cdm
		  ,vlr_mas_procesos
		  ,total
	) 
	(
		SELECT idprod
		  ,producto
		  ,item
		  ,vlr_lic
		  ,vlr_int_lic
		  ,vlr_infr
		  ,vlr_datac
		  ,vlr_mas_ga
		  ,vlr_mas_softw
		  ,vlr_mas_infr
		  ,vlr_mas_datac
		  ,vlr_mas_gente
		  ,vlr_ga_operaciones
		  ,vlr_ga_gtecnica
		  ,vlr_ga_desarrollo
		  ,vlr_mas_cdm
		  ,vlr_mas_procesos
		  ,total
		  FROM VW_VLR_CUADRO_SERVICIO
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarCuadroServicioDetalle]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Mauricio Escobar>
-- Create date: <2018-08-03>
-- Description:	<Actualizar Cuadro Servicio Detalle>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ActualizarCuadroServicioDetalle]
	
AS
BEGIN
	DELETE FROM [dbo].[GE_TVLR_CUADRO_SERVICIO_DETALLE];
	
	INSERT INTO [dbo].[GE_TVLR_CUADRO_SERVICIO_DETALLE]
           ([Idservicio]
           ,[Servicio]
           ,[IdProducto]
           ,[Producto]
           ,[Tipo]
           ,[SubTipo]
           ,[ServPersona]
           ,[Item]
           ,[Valor]
           ,[Usuario]
           ,[Fecha])

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		v.idprod Idproducto, 
		v.[producto] Producto,
		'Licenciamiento' Tipo,
		'Directo'  SubTipo,
	     null [Servidor/Persona],
		 v.item Item,
		 ROUND((CAST(ISNULL(SUM(v.valor),0) AS DECIMAL(20,0))),0) Valor,
		 'gmescobar',
		 GETDATE()
FROM [dbo].[VW_SALIDA_PRESUPUESTO] v
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON v.idprod = p.[prod_consecutivo]
/*WHERE p.prod_codigo = 'UNOEE'*/
GROUP BY p.idservicio, p.servicio, v.idprod, v.[producto], v.item

UNION ALL
SELECT	p.idservicio IdServicio,
		p.servicio Servicio,
		total.[idprod] Idproducto, 
		total.[producto] Producto,
		'Licenciamiento' Tipo,
		'Intermedios'  SubTipo, 
		null [Servidor/Persona],
		total.[item] Item, 
		ROUND((CAST(ISNULL(SUM(total.valor),0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM
(SELECT [idprod],[producto], [item], [vlrdistrib] valor
  FROM [Medeski].[dbo].[VW_REPORTE_DISTRIB_INTERM_CS]
UNION ALL
SELECT  [redi_producto_dist], [prod_codigo], [item], [valor]
FROM [dbo].[VW_REDISTRIBUCION_SAP]
) total
INNER JOIN [dbo].[VW_PRODUCTOS_DIRECTOS] p
ON total.idprod = p.[prod_consecutivo]
/*WHERE p.prod_codigo = 'UNOEE'*/
GROUP BY p.idservicio, p.servicio,total.[idprod], total.[producto], total.[item]

UNION ALL

SELECT p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Infraestructura' Tipo,
		'Infraestructura' SubTipo,
		v.[serv_nombre] [Servidor/Persona],
		v.[prit_item] Item,
		ROUND((CAST(ISNULL(v.[vlritem],0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_SERV_ITEM_INFRAESTRUCTURA] v
ON p.prod_consecutivo = v.[prod_consecutivo]
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Infraestructura' Tipo,
		'Datacenter' SubTipo,
		v.[serv_nombre] [Servidor/Persona],
		v.[prit_item] Item,
		ROUND((CAST(ISNULL(v.[vlrprd],0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_SERV_ITEM_DATACENTER] v
ON p.prod_consecutivo = v.dinf_producto
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'MAS GA' SubTipo,
		v.persona [Servidor/Persona],
		null Item,
		ROUND((CAST(ISNULL(v.costo,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_GA] v
ON p.prod_consecutivo = v.prod_consecutivo
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'MAS Software' SubTipo,
		null [Servidor/Persona],
		v.item Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_SOFTWARE] v
ON p.prod_consecutivo = v.prod_consecutivo
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'MAS Infraestructura' SubTipo,
		v.serv_nombre [Servidor/Persona],
		v.prod_codigo Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_INFRAESTRUCTURA] v
ON p.prod_consecutivo = v.idproducto
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'MAS Datacenter' SubTipo,
		v.serv_nombre [Servidor/Persona],
		v.prod_codigo Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_DATACENTER] v
ON p.prod_consecutivo = v.idproducto
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'MAS GenteTecnica' SubTipo,
		v.serv_nombre [Servidor/Persona],
		v.prod_codigo Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_INF] v
ON p.prod_consecutivo = v.idproducto
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL


SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'MAS GenteTecnica' SubTipo,
		 NULL [Servidor/Persona],
		 v.prod_codigo Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_MAS_GENTE_TECNICA_DATAC] v
ON p.prod_consecutivo = v.idproducto
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'GA Operaciones' SubTipo,
		 v.[pers_nombres] [Servidor/Persona],
		 Null Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_GA] v
ON p.prod_consecutivo = v.[prod_consecutivo]
WHERE v.[pers_nombre_area] = 'OPERACIONES' /*AND p.prod_codigo = 'UNOEE'*/

UNION ALL

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'GA Desarrollo' SubTipo,
		 v.[pers_nombres] [Servidor/Persona],
		 Null Item,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_GA] v
ON p.prod_consecutivo = v.[prod_consecutivo]
WHERE v.[pers_nombre_area] = 'DESARROLLO' /*AND p.prod_codigo = 'UNOEE'*/


UNION ALL

SELECT  p.idservicio IdServicio,
		p.servicio Servicio,
		p.prod_consecutivo IdProducto,
		p.prod_codigo Producto,
		'Equipo Interno' Tipo,
		'GA GTecnica' SubTipo,
		 v.persona [Servidor/Persona],
		 v.nombre_servidor,
		ROUND((CAST(ISNULL(v.valor,0) AS DECIMAL(20,0))),0) Valor,
		'gmescobar',
		 GETDATE()
FROM [dbo].[VW_PRODUCTOS_DIRECTOS] p
INNER JOIN [dbo].[VW_CS_VLR_GA_GTECNICA] v
ON p.prod_consecutivo = v.[prod_consecutivo]
/*WHERE p.prod_codigo = 'UNOEE'*/

UNION ALL 

SELECT * FROM VW_CS_PRODUCTOS_INFRAESTRUCTURA

END






GO
/****** Object:  StoredProcedure [dbo].[sp_CalcularValorItemServidor]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mauricio Escobar
-- Create date: 2017-09-26
-- Description:	Calculo de valor de item x servidor
-- =============================================
CREATE PROCEDURE [dbo].[sp_CalcularValorItemServidor]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

        DECLARE @inPeriodo INT

	/*PARAMETROS*/
	SELECT @inPeriodo= [peri_consecutivo] FROM [dbo].[GE_TPERIODOPRESUPUESTO] WHERE [peri_activo] = 1

	/*PRESUPUESTO*/
SELECT	pr.prod_consecutivo idProducto, it.[prit_consecutivo] idItem, 
		v.producto, v.item, pr.[prod_intermedio] intermedio, pa.[parm_descripcion] tipo, SUM(v.valor) vlr_presup
  INTO #tmpPresupuesto
  FROM [Medeski].[dbo].[VW_PRESUPUESTO] v
  INNER JOIN [dbo].[GE_TPRODUCTOS] pr
  ON v.producto = pr.[prod_descripcion]
  INNER JOIN [dbo].[GE_TPARAMETROS] pa
  ON pr.[prod_componente] = pa.[parm_consecutivo]
  INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
  ON it.[prit_item] = v.item
  INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
  ON pe.peri_ano = v.ano
  WHERE pe.[peri_consecutivo] = @inPeriodo
  GROUP BY pr.prod_consecutivo, it.[prit_consecutivo], producto, item, 
  pr.[prod_intermedio], pa.[parm_descripcion];

/*INFRAESTRUCTURA*/
SELECT dinf_servidor, dinf_producto, dinf_producto_item, SUM(dinf_valor) vlr_inf
INTO #tmpInfraestructura
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]
WHERE dinf_tipo = 'INFRAESTRUCTURA' AND dinf_estado = 1 AND  dinf_periodo = @inPeriodo
GROUP BY dinf_servidor, dinf_producto, dinf_producto_item

/*SERVIDORES*/
SELECT dinf_servidor, ISNULL(s.[serv_core],0) core, SUM(tp.vlr_presup * tf.vlr_inf) vlr
INTO #tmpServidores
FROM #tmpPresupuesto tp
INNER JOIN  #tmpInfraestructura tf
ON tp.idProducto = tf.dinf_producto
AND tp.idItem = tf.dinf_producto_item
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON tf.dinf_servidor = s.[serv_consecutivo]
GROUP BY dinf_servidor, ISNULL(s.[serv_core],0)

/*ITEM X SERV*/
SELECT ts.dinf_servidor,ts.core, tf.dinf_producto, tf.dinf_producto_item, tf.vlr_inf,
tp.producto, tp.item, tp.vlr_presup
INTO #tmpServxItem
FROM #tmpServidores ts
INNER JOIN #tmpInfraestructura tf
ON ts.dinf_servidor = tf.dinf_servidor
INNER JOIN #tmpPresupuesto tp
ON tp.idProducto = tf.dinf_producto
AND tp.idItem = tf.dinf_producto_item

/*VLR ITEM X SERV*/
SELECT ts.dinf_servidor, ts.core, ts.dinf_producto, ts.dinf_producto_item, 
tmpCore.vlr_core * ts.core vlritem
INTO #tmpVlrItemSev
FROM #tmpServxItem ts
INNER JOIN (
SELECT dinf_producto, dinf_producto_item,SUM(core) core, vlr_presup/SUM(core) vlr_core
FROM #tmpServxItem 
GROUP BY dinf_producto, dinf_producto_item, vlr_presup) tmpCore 
ON ts.dinf_producto = tmpCore.dinf_producto
AND ts.dinf_producto_item = tmpCore.dinf_producto_item

/*VLR SERVIDOR*/
SELECT dinf_servidor, SUM(vlritem) vlr_serv
INTO #tmpVlrServ
FROM #tmpVlrItemSev
GROUP BY dinf_servidor

/*VLR ITEM  SERVIDOR*/
SELECT v.dinf_servidor idServidor, s.[serv_nombre] Servidor, s.serv_core #Core, v.dinf_producto idProducto, pr.prod_codigo Producto, 
v.dinf_producto_item idItem, it.prit_item item, v.vlritem
INTO #tmp
FROM #tmpVlrItemSev v
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON v.dinf_producto = pr.prod_consecutivo
INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
ON v.dinf_producto_item = it.prit_consecutivo
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON s.serv_consecutivo = v.dinf_servidor

/*
DECLARE @columns varchar(MAX);
DECLARE @sql nvarchar(max)

SET @columns = STUFF(
 (
 SELECT
   ',' + QUOTENAME(LTRIM(item))
 FROM
   (SELECT DISTINCT item
    FROM #tmp
   ) AS T
 ORDER BY
 item
 FOR XML PATH('')
 ), 1, 1, '');


 SET @sql = N'
 SELECT
   Servidor, #Core, ' + @columns + ' 
  FROM
  (  
  SELECT  Servidor, #Core, item, vlritem
  FROM #tmp
  ) AS T
  PIVOT   
  (
  SUM(vlritem)
  FOR item IN (' + @columns + N')
  ) AS P;'; 

EXEC sp_executesql @sql;
*/

SELECT  Servidor servidor, #Core core, item, ISNULL(vlritem,0) vlritem
  FROM #tmp

DROP TABLE #tmp
DROP TABLE #tmpVlrServ;
DROP TABLE #tmpVlrItemSev;
DROP TABLE #tmpServxItem;
DROP TABLE #tmpServidores;
DROP TABLE #tmpPresupuesto;
DROP TABLE #tmpInfraestructura
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CargarPeriodoTransaccion]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Mauricio Escobar>
-- Create date: <2017-07-11>
-- Description:	<Periodo Transacciones>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CargarPeriodoTransaccion]
	@inConsecutivo INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @inAmortizar INT,
			@vcMoneda VARCHAR(50),
			@deValor DECIMAL(18,6),
			@vcMes VARCHAR(50),
			@deValord DECIMAL(18,6),
			@deTrm DECIMAL(18,6),
			@inMonedaBase INT,
			@inPeriodo INT,
			@inMes INT,
			@inCont	INT,
			@inCantidadMeses INT,
			@vcTipo	VARCHAR(50),
			@inCantidad INT,
			@inActivo INT

	/*Parametros*/
	SELECT @inMonedaBase = [parm_consecutivo] FROM [dbo].[GE_TPARAMETROS] WHERE [parm_descripcion] = 'PESOS' and parm_estado = 1
	SELECT @inPeriodo= [peri_consecutivo] FROM [dbo].[GE_TPERIODOPRESUPUESTO] WHERE [peri_activo] = 1

	/*Selecciono datos amortización*/
	SELECT @inAmortizar = petr_amortizar, @deValor = petr_valor, @vcMes = p.parm_codigo, @vcMoneda = p1.parm_codigo, @deTrm = [petr_trm],
	@inCantidadMeses = ISNULL(p2.parm_codigo,0), @vcTipo = petr_tipo, @inCantidad = petr_cantidad, @inActivo = petr_activo
	FROM [dbo].[GE_TPERIODOTRANSACCIONES] t
	INNER JOIN  [dbo].[GE_TPARAMETROS] p
	ON t.petr_mes = p.parm_consecutivo
	INNER JOIN [dbo].[GE_TPARAMETROS] p1
	ON t.petr_moneda = p1.parm_consecutivo
	LEFT OUTER JOIN [dbo].[GE_TPARAMETROS] p2
	ON p2.parm_consecutivo = t.petr_meses_amortizar
	WHERE t.petr_consecutivo = @inConsecutivo

	/*Borro datos generados*/
	DELETE FROM [dbo].[GE_TSALIDAPRESUPUESTO] 
	WHERE [sali_periodo_transacc] = @inConsecutivo AND [sali_tipo] = @vcTipo AND [sali_periodo] = @inPeriodo

	SET @inMes = CAST(@vcMes AS INT)
	SET @inCont = @inMes

	IF (@vcTipo = 'VIAJE') 
	BEGIN
		SET @inCantidad = 1
	END

	IF (@inActivo = 1)
	BEGIN
		IF (@inAmortizar = 1)
		BEGIN
	
			SET @deValord = (@deValor *  @deTrm * @inCantidad)/ ((@inCantidadMeses -  @inMes) + 1)

			WHILE (@inCont <= @inCantidadMeses)
			BEGIN
		
					INSERT INTO [dbo].[GE_TSALIDAPRESUPUESTO]
								([sali_periodo_transacc]
								,[sali_persona]
								,[sali_centrocosto]
								,[sali_producto_item]
								,[sali_mes]
								,[sali_moneda]
								,[sali_valor]
								,[sali_usuario]
								,[sali_fecha]
								,[sali_tipo]
								,[sali_periodo])
					SELECT t.[petr_consecutivo], t.petr_persona, t.petr_centrocosto, t.petr_producto_item, @inCont, @inMonedaBase, @deValord, t.petr_usuario, GETDATE(), 
					@vcTipo, t.petr_periodo
					FROM [dbo].[GE_TPERIODOTRANSACCIONES] t
					WHERE t.petr_consecutivo = @inConsecutivo

					SET @inCont = @inCont + 1
			END
			END
			ELSE
			BEGIN
				SET @deValord = (@deValor *  @deTrm)
		
					INSERT INTO [dbo].[GE_TSALIDAPRESUPUESTO]
								([sali_periodo_transacc]
								,[sali_persona]
								,[sali_centrocosto]
								,[sali_producto_item]
								,[sali_mes]
								,[sali_moneda]
								,[sali_valor]
								,[sali_usuario]
								,[sali_fecha]
								,[sali_tipo]
								,[sali_periodo])
					SELECT t.[petr_consecutivo], t.petr_persona, t.petr_centrocosto, t.petr_producto_item, @inMes, @inMonedaBase, (@deValord * @inCantidad), t.petr_usuario, GETDATE(), 
					@vcTipo, t.petr_periodo
					FROM [dbo].[GE_TPERIODOTRANSACCIONES] t
					WHERE t.petr_consecutivo = @inConsecutivo
			END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CargarPeriodoTransaccionPersona]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Mauricio Escobar>
-- Create date: <2017-07-17>
-- Description:	<Periodo Transacciones Personas>
-- =============================================
CREATE PROCEDURE [dbo].[sp_CargarPeriodoTransaccionPersona]
	@inConsecutivo INT
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @inMoneda INT,
			@vcItem VARCHAR(50),
			@inMonedaBase INT,
			@inPeriodo INT,
			@inActIvo INT,
			@inItem INT,
			@deValor DECIMAL(18,6),
			@vcMes VARCHAR(50),
			@inMes INT

	/*Parametros*/
	SELECT @inMonedaBase = [parm_consecutivo] FROM [dbo].[GE_TPARAMETROS] WHERE [parm_descripcion] = 'PESOS' and parm_estado = 1
	SELECT @inPeriodo= [peri_consecutivo] FROM [dbo].[GE_TPERIODOPRESUPUESTO] WHERE [peri_activo] = 1

	/*DATOS*/
	SELECT  @inActivo = t.petr_activo, @inMoneda = t.petr_moneda, @vcMes = p.parm_codigo
	FROM [dbo].[GE_TPERIODOTRANSACCIONES]  t
	INNER JOIN  [dbo].[GE_TPARAMETROS] p
	ON t.petr_mes = p.parm_consecutivo
	WHERE t.[petr_consecutivo] = @inConsecutivo

	/*Borro datos generados*/
	DELETE FROM [dbo].[GE_TSALIDAPRESUPUESTO] 
	WHERE [sali_periodo_transacc] = @inConsecutivo AND [sali_tipo] = 'ALOJAMIENTO' AND [sali_periodo] = @inPeriodo

	IF (@inActivo = 1)
	BEGIN
	
	IF (@inMoneda = @inMonedaBase)
	 SET @vcItem = 'ITEM_ALOJAMIENTO_NAL'
	ELSE
	 SET @vcItem = 'ITEM_ALOJAMIENTO_EXT'

	 SET @inMes = CAST(@vcMes AS INT)

	 SELECT @inItem = prit_consecutivo
	FROM [dbo].[GE_TPRODUCTOSITEMS] it
	INNER JOIN [dbo].[VLRSPRMGRALES] v
	ON it.[prit_item] = v.[vhpg_valor]
	WHERE pmgr_parametro = @vcItem

	SELECT @deValor = SUM(((t.ptrp_tarifa_hotel * ptrp_smmlv) + (t.ptrp_tarifa_alimentacion * ptrp_smmlv)) * ptrp_cantidad_dias)
	FROM [Medeski].[dbo].[GE_TPERIODOTRANSACCPERSONAS] t
	WHERE t.[ptrp_periodo_transacc] = @inConsecutivo
	
	INSERT INTO [dbo].[GE_TSALIDAPRESUPUESTO]
			   ([sali_periodo_transacc]
			   ,[sali_persona]
			   ,[sali_centrocosto]
			   ,[sali_producto_item]
			   ,[sali_mes]
			   ,[sali_moneda]
			   ,[sali_valor]
			   ,[sali_usuario]
			   ,[sali_fecha]
			   ,[sali_tipo]
			   ,[sali_periodo])

		SELECT @inConsecutivo, t1.[petr_persona],  t1.[petr_centrocosto], @inItem, @inMes, 
		@inMonedaBase,@deValor,
		t1.[petr_usuario],  GETDATE(), 'ALOJAMIENTO', @inPeriodo
		FROM [Medeski].[dbo].[GE_TPERIODOTRANSACCIONES] t1
		WHERE t1. [petr_consecutivo]= @inConsecutivo
END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CargueDistribucionPersonas]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CargueDistribucionPersonas] 
(
  @dper_periodo INT,
  @dper_tipo VARCHAR(50),
  @dper_persona INT,
  @dper_servidor INT,
  @dper_producto INT,
  @dper_valor DECIMAL,
  @dper_estado INT,
  @dper_usuario VARCHAR(50),
  @dper_fecha DATETIME
)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Bandera INT
	         SET @dper_periodo = (SELECT peri_consecutivo FROM GE_TPERIODOPRESUPUESTO WHERE peri_Activo = 1)

	         SET @Bandera = (SELECT dper_consecutivo FROM GE_TDISTRIBUCIONDEDICACIONPERSONA 
			                                         WHERE dper_producto = @dper_producto 
													       OR dper_servidor = @dper_servidor
														   AND dper_periodo = 1
														   AND dper_persona = @dper_persona) 
             IF(@Bandera >= 1)
			     Begin
				     UPDATE GE_TDISTRIBUCIONDEDICACIONPERSONA SET  dper_valor = @dper_valor	
					                                          WHERE dper_producto = @dper_producto 
															  OR dper_servidor = @dper_servidor
														      AND dper_periodo = 1
														      AND dper_persona = @dper_persona                                             
				 End
             ELSE

			 IF(@dper_producto = 0)
			     Begin
				  SET @dper_producto = null
				  INSERT INTO 
					        GE_TDISTRIBUCIONDEDICACIONPERSONA (dper_periodo, dper_tipo,dper_persona,dper_servidor,dper_valor,dper_estado,dper_usuario,dper_fecha)  
					                                          VALUES 
														     (@dper_periodo, @dper_tipo,@dper_persona,@dper_servidor,@dper_valor,@dper_estado,@dper_usuario,@dper_fecha) 
					                
				 End
				 ELSE IF (@dper_servidor = 0)
				   Begin
				   SET @dper_servidor = null
				    INSERT INTO 
					        GE_TDISTRIBUCIONDEDICACIONPERSONA (dper_periodo, dper_tipo,dper_persona,dper_producto,dper_valor,dper_estado,dper_usuario,dper_fecha)  
					                                          VALUES 
														     (@dper_periodo, @dper_tipo,@dper_persona,@dper_producto,@dper_valor,@dper_estado,@dper_usuario,@dper_fecha) 
					                
				 End

END
GO
/****** Object:  StoredProcedure [dbo].[SP_CARGUEDRIVERS]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CARGUEDRIVERS]
@INFO_ARRAY AS dbo.TMP_CARGUEDRIVERS READONLY
AS
BEGIN
    INSERT INTO GE_TCARGUEDRIVERS(
		carg_periodo,
		carg_producto,
		carg_compania,
		carg_sede,
		carg_ccosto,
		carg_driver,
		carg_cantidad,
		carg_valor,
		carg_valor_distribucion,
		carg_valor_adicional,
		carg_proveedor,
		carg_usuario,
		carg_fecha,
		carg_usuario_act,
		carg_fecha_act,
		carg_activo)
    
    SELECT * FROM @INFO_ARRAY
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DatosGentePersona]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_DatosGentePersona]

AS
BEGIN
	SET NOCOUNT ON;

 SELECT DISTINCT
        [Extent1].[pers_consecutivo] AS [pers_consecutivo], 
        [Extent2].[gent_ccostos] AS [gent_ccostos], 
        [Extent2].[gent_ceop] AS [gent_ceop], 
        [Extent2].[gent_consecutivo] AS [gent_consecutivo], 
        [Extent2].[gent_costo_colaborador] AS [gent_costo_colaborador], 
        [Extent2].[gent_descripcion_ccostos] AS [gent_descripcion_ccostos], 
        [Extent2].[gent_empresa] AS [gent_empresa], 
        [Extent2].[gent_estado] AS [gent_estado], 
        [Extent2].[gent_nombre_cargo] AS [gent_nombre_cargo], 
        [Extent2].[gent_periodo] AS [gent_periodo], 
        [Extent2].[gent_porcentaje_manual_dedicacion] AS [gent_porcentaje_manual_dedicacion], 
        [Extent2].[gent_persona] AS [gent_persona], 
        [Extent1].[pers_nombres] AS [pers_nombres], 
        [Extent1].[pers_nombre_area] AS [pers_nombre_area],
		[Extent1].[pers_identificacion] AS [pers_identificacion]
        FROM  [dbo].[GE_TPERSONAS] AS [Extent1]
        INNER JOIN [dbo].[GE_TGENTE] AS [Extent2] ON [Extent1].[pers_consecutivo] = [Extent2].[gent_persona]
        WHERE (1 = [Extent2].[gent_estado]) ORDER BY [Extent1].[pers_nombres]


END
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarValoresDuplicados]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<JLopez>
-- Create date: <2018-08-10>
-- Description:	<Procedimiento para eliminar valores de períodos duplicados>
-- =============================================

CREATE PROCEDURE [dbo].[sp_EliminarValoresDuplicados]
	@buscar int
AS
BEGIN
	-- GE_TREDISTRIBUCION
	DELETE FROM GE_TREDISTRIBUCION WHERE redi_periodo <> @buscar;
  
	-- GE_TRELITEMSDATACENTERPROD	
	DELETE FROM dbo.GE_TRELITEMSDATACENTERPROD WHERE drel_periodo <> @buscar;
    
	DELETE FROM dbo.GE_TGENTE WHERE gent_periodo <> @buscar;
	
	-- GE_TDISTRIBUCIONINTERMEDIOS
	DELETE FROM dbo.GE_TDISTRIBUCIONINTERMEDIOS WHERE dint_periodo <> @buscar;
		
	-- GE_TDISTRIBUCIONINFRAESTRUCTURA
	DELETE FROM dbo.GE_TDISTRIBUCIONINFRAESTRUCTURA WHERE dinf_periodo <> @buscar;
				
	-- GE_TDISTRIBUCIONMASPROCESOS
	DELETE FROM dbo.GE_TDISTRIBUCIONMASPROCESOS WHERE dmas_periodo <> @buscar;		
		
	-- GE_TDISTRIBUCIONDEDICACIONPERSONA
	DELETE FROM dbo.GE_TDISTRIBUCIONDEDICACIONPERSONA WHERE dper_periodo <> @buscar;
	
	-- GE_TCARGUEDRIVERS
	DELETE FROM dbo.GE_TCARGUEDRIVERS WHERE carg_periodo <> @buscar;
	
	-- GE_TSALIDAPRESUPUESTO
	DELETE FROM dbo.GE_TSALIDAPRESUPUESTO WHERE sali_periodo <> @buscar;
	
	-- GE_TPERIODOTRANSACCIONES
	DELETE FROM dbo.GE_TPERIODOTRANSACCIONES WHERE petr_periodo <> @buscar;
	
	-- GE_TPERIODOPRESUPUESTO
	DELETE FROM dbo.GE_TPERIODOPRESUPUESTO WHERE peri_consecutivo <> @buscar;
		
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerarCuadroServicio]    Script Date: 02/08/2019 17:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mauricio Escobar
-- Create date: 2017-09-25
-- Description:	Generación Cuadro de Servicios
-- =============================================
CREATE PROCEDURE [dbo].[sp_GenerarCuadroServicio]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @inPeriodo INT

	/*PARAMETROS*/
	SELECT @inPeriodo= [peri_consecutivo] FROM [dbo].[GE_TPERIODOPRESUPUESTO] WHERE [peri_activo] = 1

	/*PRESUPUESTO*/
SELECT	pr.prod_consecutivo idProducto, it.[prit_consecutivo] idItem, 
		v.producto, v.item, pr.[prod_intermedio] intermedio, pa.[parm_descripcion] tipo, SUM(v.valor) vlr_presup
  INTO #tmpPresupuesto
  FROM [Medeski].[dbo].[VW_PRESUPUESTO] v
  INNER JOIN [dbo].[GE_TPRODUCTOS] pr
  ON v.producto = pr.[prod_descripcion]
  INNER JOIN [dbo].[GE_TPARAMETROS] pa
  ON pr.[prod_componente] = pa.[parm_consecutivo]
  INNER JOIN [dbo].[GE_TPRODUCTOSITEMS] it
  ON it.[prit_item] = v.item
  INNER JOIN [dbo].[GE_TPERIODOPRESUPUESTO] pe
  ON pe.peri_ano = v.ano
  WHERE pe.[peri_consecutivo] = @inPeriodo
  GROUP BY pr.prod_consecutivo, it.[prit_consecutivo], producto, item, 
  pr.[prod_intermedio], pa.[parm_descripcion];

/*INFRAESTRUCTURA*/
SELECT dinf_servidor, dinf_producto, dinf_producto_item, SUM(dinf_valor) vlr_inf
INTO #tmpInfraestructura
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]
WHERE dinf_tipo = 'INFRAESTRUCTURA' AND dinf_estado = 1 AND  dinf_periodo = @inPeriodo
GROUP BY dinf_servidor, dinf_producto, dinf_producto_item

/*SERVIDORES*/
SELECT dinf_servidor, ISNULL(s.[serv_core],0) core, SUM(tp.vlr_presup * tf.vlr_inf) vlr
INTO #tmpServidores
FROM #tmpPresupuesto tp
INNER JOIN  #tmpInfraestructura tf
ON tp.idProducto = tf.dinf_producto
AND tp.idItem = tf.dinf_producto_item
INNER JOIN [dbo].[GE_TSERVIDORES] s
ON tf.dinf_servidor = s.[serv_consecutivo]
GROUP BY dinf_servidor, ISNULL(s.[serv_core],0)

/*ITEM X SERV*/
SELECT ts.dinf_servidor,ts.core, tf.dinf_producto, tf.dinf_producto_item, tf.vlr_inf,
tp.producto, tp.item, tp.vlr_presup
INTO #tmpServxItem
FROM #tmpServidores ts
INNER JOIN #tmpInfraestructura tf
ON ts.dinf_servidor = tf.dinf_servidor
INNER JOIN #tmpPresupuesto tp
ON tp.idProducto = tf.dinf_producto
AND tp.idItem = tf.dinf_producto_item

/*VLR ITEM X SERV*/
SELECT ts.dinf_servidor, ts.core, ts.dinf_producto, ts.dinf_producto_item, 
tmpCore.vlr_core * ts.core vlritem
INTO #tmpVlrItemSev
FROM #tmpServxItem ts
INNER JOIN (
SELECT dinf_producto, dinf_producto_item,SUM(core) core, vlr_presup/SUM(core) vlr_core
FROM #tmpServxItem 
GROUP BY dinf_producto, dinf_producto_item, vlr_presup) tmpCore 
ON ts.dinf_producto = tmpCore.dinf_producto
AND ts.dinf_producto_item = tmpCore.dinf_producto_item

/*VLR SERVIDOR*/
SELECT dinf_servidor, SUM(vlritem) vlr_serv
INTO #tmpVlrServ
FROM #tmpVlrItemSev
GROUP BY dinf_servidor

/*VLR DISTRIB DIRECTOS*/
SELECT inf.[dinf_servidor], inf.[dinf_producto], (inf.dinf_valor * 100)/s.valor vlrreal
INTO #tmpDistribRealDirectos
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA] inf
INNER JOIN
(SELECT [dinf_servidor], SUM(dinf_valor) valor
FROM [dbo].[GE_TDISTRIBUCIONINFRAESTRUCTURA]
WHERE dinf_tipo = 'DIRECTO' AND dinf_estado = 1 AND dinf_periodo = @inPeriodo
GROUP BY [dinf_servidor]) s
ON inf.[dinf_servidor] = s.[dinf_servidor]
WHERE dinf_tipo = 'DIRECTO' AND dinf_estado = 1 AND dinf_periodo = @inPeriodo

/*DISTRIB INTERMEDIOS*/
SELECT [dint_producto_intermedio], [dint_producto_directo], SUM(dint_valor) vlr
INTO #tmpIntermedios
FROM [Medeski].[dbo].[GE_TDISTRIBUCIONINTERMEDIOS]
WHERE [dint_periodo] = @inPeriodo AND dint_estado = 1
GROUP BY [dint_producto_intermedio], [dint_producto_directo]

/*VLR DISTRIB INFRAESTRUCTURA*/
SELECT r.[dinf_producto] idProducto, SUM(vs.vlr_serv * r.vlrreal /100) vlr_infraest
INTO #tmpDistribInfra
FROM #tmpVlrServ vs
INNER JOIN #tmpDistribRealDirectos r
ON vs.dinf_servidor = r.[dinf_servidor]
GROUP BY r.[dinf_producto]

/*VLR DISTRIB INTERMEDIOS*/
SELECT t.[dint_producto_directo] idProducto, ISNULL(SUM(t.vlr * pres.vlr_p),0) vlr_interm
INTO #tmpDistribInterm
FROM #tmpIntermedios t
LEFT OUTER JOIN  
(SELECT idProducto, SUM(vlr_presup) vlr_p FROM #tmpPresupuesto GROUP BY idProducto) pres
ON t.[dint_producto_intermedio] = pres.idProducto
GROUP BY t.[dint_producto_directo]

/*VLR DISTRIB DIRECTOS*/
SELECT idProducto, SUM(vlr_presup) vlr_directo
INTO #tmpDistribDirect
FROM #tmpPresupuesto
WHERE tipo = 'DIRECTO' AND intermedio = 0
GROUP BY idProducto

/*VLR DISTRIB GA*/
SELECT [card_producto] idProducto, SUM([card_valor]) vlr_ga
INTO #tmpDistribGA
FROM [dbo].[GE_TDISTRIBUCIONCARGUEGA]
WHERE [card_periodo] = @inPeriodo
GROUP BY [card_producto]

SELECT t1.idProducto, pr.prod_codigo, ISNULL(t1.vlr_directo,0) vlr_directo, 
ISNULL(t2.vlr_infraest,0) vlr_infraest, 
ISNULL(t3.vlr_interm,0) vlr_interm, ISNULL(t4.vlr_ga, 0) vlr_ga
FROM #tmpDistribDirect t1
INNER JOIN [dbo].[GE_TPRODUCTOS] pr
ON t1.idProducto = pr.prod_consecutivo
LEFT OUTER JOIN #tmpDistribInfra t2
ON t1.idProducto = t2.idProducto
LEFT OUTER JOIN #tmpDistribInterm t3
ON t1.idProducto = t3.idProducto
LEFT OUTER JOIN #tmpDistribGA t4
ON t1.idProducto = t4.idProducto

DROP TABLE #tmpDistribInfra;
DROP TABLE #tmpDistribInterm;
DROP TABLE #tmpDistribDirect;
DROP TABLE #tmpDistribGA;
DROP TABLE #tmpIntermedios;
DROP TABLE #tmpVlrServ;
DROP TABLE #tmpDistribRealDirectos;
DROP TABLE #tmpVlrItemSev;
DROP TABLE #tmpServxItem;
DROP TABLE #tmpServidores;
DROP TABLE #tmpPresupuesto;
DROP TABLE #tmpInfraestructura
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo dado a una agrupacion de parametros relaciondos entre si (lista)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_clase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del grupo de parametros en nemotecnico' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del grupo de parametros' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha desde la que tiene vigencia el grupo de parametros' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_fechaini'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha hasta la que tiene vigencia el grupo de parametros' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_fechafin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del grupo de parametros (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametros Generales del Sistema (Tipo Lista)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TCLASESPARAMETROS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TDOMINIOS', @level2type=N'COLUMN',@level2name=N'domi_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del dominio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TDOMINIOS', @level2type=N'COLUMN',@level2name=N'domi_nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TDOMINIOS', @level2type=N'COLUMN',@level2name=N'domi_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se inserta el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TDOMINIOS', @level2type=N'COLUMN',@level2name=N'domi_fecha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del grupo del directorio activo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TDOMINIOS', @level2type=N'COLUMN',@level2name=N'domi_grupo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametrizacion de los grupos del directorio activo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TDOMINIOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Opcion padre de la que depende la actual opcion (para el encadenamiento del menu)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_idpadre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la forma o pagina' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_idonma'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador padre de la cual depende la forma' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_idpadreonma'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del objeto que contiene la forma, por ejemplo un boton un un campo de texto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_idobjetoonma'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Titulo que va a contener el menu o forma' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_titulo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion general de la opcion de menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Direccion o URL del formulario al que va a referenciar una vez se oprima la opcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL de la imagen que se va a aplicar a la opcion para visualizar en la barra de menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_ruta_imagen'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creacion del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_fecha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de actualizacion de registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_fecha_act'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de registro (FORMA, OBJETO o MENU)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_tipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que creo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que actualizo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU', @level2type=N'COLUMN',@level2name=N'opcm_usuario_act'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametrizacion de las opciones de menu de la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENU'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENUXROL', @level2type=N'COLUMN',@level2name=N'opcr_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del rol' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENUXROL', @level2type=N'COLUMN',@level2name=N'rolm_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENUXROL', @level2type=N'COLUMN',@level2name=N'opcm_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha inserción registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENUXROL', @level2type=N'COLUMN',@level2name=N'opcr_fecha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que inserta el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENUXROL', @level2type=N'COLUMN',@level2name=N'opcr_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametrizacion de los permisos de los roles a un menu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TOPCIONESMENUXROL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del grupo de parametros al que pertenece el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'clap_clase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del parametro individual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha desde la que tiene vigencia el parametro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_fechadesde'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha hasta la que tiene vigencia el parametro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_fechahasta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del parametro (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Campo comodin utilizado para almacenar informacion adicional multiproposito' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_infoadicional'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Codigo del parametro individual' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS', @level2type=N'COLUMN',@level2name=N'parm_codigo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Detalle de los Parametros Generales del Sistema (Tipo Lista)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TPARAMETROS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del Rol' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_nombre'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion general del rol' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se inserta el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_fecha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se actualiza el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_fecha_act'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del usuario que inserta el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del usuario que actualiza el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_usuario_act'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del grupo al que se le asigna el rol' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_grupo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del dominio del grupo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES', @level2type=N'COLUMN',@level2name=N'rolm_dominio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametrizacion de los roles de la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TROLES'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_USUARIO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_USERNAME'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contrasena encriptada del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_CLAVE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dominio al que pertenece el usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_DOMINIO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se realizo el registro del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_FECHA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora en la que se realizo el registro del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_HORA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado en el que se encuentra el usuario (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_ESTADO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No es esta utilizando, para uso futuro de ser necesario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_ENCRIPTA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que vence de la clave el usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_FECHAVENCE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se cambio la contrasena' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_FECHACAMBIO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se actualizo el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_FECHA_ACT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que inserto el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_USUARIO_INS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realizo actualizacion del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_USUARIO_ACT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de usuario (Administrador, Prueba)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'PARM_TIPOUSUARIO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Campo que indica si el usuario se validara contra el directorio activo o no (S = si, N = No)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS', @level2type=N'COLUMN',@level2name=N'USUA_VALIDALDAP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametrizacion de los usuarios del sistema' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza el ingreso' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usua_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de ingreso a la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_fecha_ingreso'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de salida de la alicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_fecha_salida'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Forma como se realizo la salida de la aplicacion. App = Desde la aplicacion o por Desbloqueo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_tipo_salida'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del host desde el cual se ingresa a la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_hostname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP del host desde donde se ingresa a la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre y Version del Browser desde el cual se ingresa a la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_browser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza el desbloqueo (si aplica)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_usuario_desbloqueo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Patron para identificar la session' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_guid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP del host antes del proxy desde donde se accede a la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS', @level2type=N'COLUMN',@level2name=N'usac_ipantesproxy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla para registrar los accesos de los usuarios en la aplicacion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOS_ACCESOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo que identifica el registro en la tabla' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usxr_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del rol' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'rolm_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del usuario' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usua_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro (1= Activo ; 0= Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usxr_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en la que se inserta el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usxr_fecha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en la que se actualiza el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usxr_fecha_act'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que inserta el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usxr_usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que actualiza el registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL', @level2type=N'COLUMN',@level2name=N'usxr_usuario_act'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Asignacion de un usuario a uno o varios Roles' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GE_TUSUARIOSXROL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la variable que se quiere parametrizar. Nemotecnico' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PARAMETROSGRALES', @level2type=N'COLUMN',@level2name=N'pmgr_parametro'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripcion del parametro general' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PARAMETROSGRALES', @level2type=N'COLUMN',@level2name=N'pmgr_descripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PARAMETROSGRALES', @level2type=N'COLUMN',@level2name=N'pmgr_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica a que grupo de variables pertence el registro. Clase 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PARAMETROSGRALES', @level2type=N'COLUMN',@level2name=N'parm_grupo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica el tipo de dato al que pertenece el parametro. Clase 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PARAMETROSGRALES', @level2type=N'COLUMN',@level2name=N'parm_tipodato'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parametros Generales del Sistema (Tipo Variable)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PARAMETROSGRALES'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consecutivo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES', @level2type=N'COLUMN',@level2name=N'vhpg_consecutivo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del parametro en Nemotecnico' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES', @level2type=N'COLUMN',@level2name=N'pmgr_parametro'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha desde la cual se encuentra disponible el parametro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES', @level2type=N'COLUMN',@level2name=N'vhpg_fecdesde'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha hasta la cual se encuentra disponible el parametro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES', @level2type=N'COLUMN',@level2name=N'vhpg_fechasta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del parametro (1 = Activo, 0 = Inactivo)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES', @level2type=N'COLUMN',@level2name=N'vhpg_estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del parametro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES', @level2type=N'COLUMN',@level2name=N'vhpg_valor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valores de los Parametros Generales del Sistema (Tipo Variable)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VLRSPRMGRALES'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 21
               Left = 437
               Bottom = 172
               Right = 728
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_ITEMS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_ITEMS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TPERSONAS"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 191
               Right = 366
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_PERSONAS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_PERSONAS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TPRODUCTOS"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPARAMETROS"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 191
               Right = 535
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_PRODUCTOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_PRODUCTOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "T1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "T2"
            Begin Extent = 
               Top = 6
               Left = 274
               Bottom = 126
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "T3"
            Begin Extent = 
               Top = 6
               Left = 510
               Bottom = 111
               Right = 708
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "T4"
            Begin Extent = 
               Top = 6
               Left = 746
               Bottom = 111
               Right = 944
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_SALIDA_FINAL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_SALIDA_FINAL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TCLASESPARAMETROS"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPARAMETROS"
            Begin Extent = 
               Top = 6
               Left = 274
               Bottom = 183
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1620
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_SERVICIOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_SERVICIOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TSERVIDORES"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 189
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_SERVIDORES'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_SERVIDORES'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_TIPOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_BPC_TIPOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "pr"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pinf"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 126
               Right = 476
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 6
               Left = 514
               Bottom = 126
               Right = 712
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "gente"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 303
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pa"
            Begin Extent = 
               Top = 6
               Left = 750
               Bottom = 126
               Right = 948
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pa1"
            Begin Extent = 
               Top = 126
               Left = 341
               Bottom = 246
               Right = 539
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 11' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CS_PRODUCTOS_INFRAESTRUCTURA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'70
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CS_PRODUCTOS_INFRAESTRUCTURA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CS_PRODUCTOS_INFRAESTRUCTURA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TCENTROSOPERACION"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TCENTROSCOSTOS"
            Begin Extent = 
               Top = 6
               Left = 290
               Bottom = 126
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPARAMETROS"
            Begin Extent = 
               Top = 6
               Left = 556
               Bottom = 126
               Right = 770
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CUADRO_VENTAS_PYG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_CUADRO_VENTAS_PYG'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TSERVIDORES"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPERSONAS"
            Begin Extent = 
               Top = 6
               Left = 315
               Bottom = 135
               Right = 511
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TDISTRIBUCIONDEDICACIONPERSONA"
            Begin Extent = 
               Top = 6
               Left = 549
               Bottom = 135
               Right = 731
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_GENTE_TECNICA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_GENTE_TECNICA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -951
      End
      Begin Tables = 
         Begin Table = "pr"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pinf"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 126
               Right = 476
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pa"
            Begin Extent = 
               Top = 6
               Left = 514
               Bottom = 126
               Right = 712
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pa1"
            Begin Extent = 
               Top = 6
               Left = 750
               Bottom = 126
               Right = 948
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_INFRAESTRUCTURA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_INFRAESTRUCTURA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TPERIODOTRANSACCIONES"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPERSONAS"
            Begin Extent = 
               Top = 6
               Left = 274
               Bottom = 126
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPERIODOPRESUPUESTO"
            Begin Extent = 
               Top = 6
               Left = 510
               Bottom = 126
               Right = 708
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPRODUCTOSITEMS"
            Begin Extent = 
               Top = 6
               Left = 746
               Bottom = 126
               Right = 944
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPRODUCTOS"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 246
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TSALIDAPRESUPUESTO"
            Begin Extent = 
               Top = 126
               Left = 278
               Bottom = 246
               Right = 476
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TCENTROSCOSTOS"
            Begin Extent = 
               Top = 126
               Left = 514
               Bottom = 246
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_PRESUPUESTADOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'              Right = 726
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p1"
            Begin Extent = 
               Top = 126
               Left = 764
               Bottom = 246
               Right = 962
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TCUENTAS"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 366
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p2"
            Begin Extent = 
               Top = 246
               Left = 274
               Bottom = 366
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_PRESUPUESTADOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_PRESUPUESTADOS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_SIN_PPTO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_PRODUCTOS_SIN_PPTO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TCARGUEDRIVERS"
            Begin Extent = 
               Top = 62
               Left = 47
               Bottom = 182
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "GE_TCENTROSCOSTOS"
            Begin Extent = 
               Top = 144
               Left = 312
               Bottom = 264
               Right = 540
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TREDISTRIBUCION_DRIVERS"
            Begin Extent = 
               Top = 6
               Left = 558
               Bottom = 126
               Right = 772
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TCENTROSOPERACION"
            Begin Extent = 
               Top = 71
               Left = 814
               Bottom = 191
               Right = 1028
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_REPORTE_REDISTRIBUCION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_REPORTE_REDISTRIBUCION'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_VLR_CUADRO_SERVICIO_BPC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_VLR_CUADRO_SERVICIO_BPC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[45] 4[23] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GE_TCARGUEDRIVERS"
            Begin Extent = 
               Top = 0
               Left = 285
               Bottom = 164
               Right = 485
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "GE_TCENTROSCOSTOS"
            Begin Extent = 
               Top = 41
               Left = 7
               Bottom = 241
               Right = 219
            End
            DisplayFlags = 280
            TopColumn = 14
         End
         Begin Table = "GE_TPARAMETROS"
            Begin Extent = 
               Top = 116
               Left = 514
               Bottom = 236
               Right = 712
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GE_TPRODUCTOS"
            Begin Extent = 
               Top = 15
               Left = 804
               Bottom = 216
               Right = 1006
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_VLR_DISTRIBUCION_CCOSTO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_VLR_DISTRIBUCION_CCOSTO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VW_VLR_DISTRIBUCION_CCOSTO'
GO
