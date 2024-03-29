USE [Medeski]
GO
/****** Object:  StoredProcedure [dbo].[Sp_DatosGentePersona]    Script Date: 31/07/2019 11:25:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_DatosGentePersona]

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
