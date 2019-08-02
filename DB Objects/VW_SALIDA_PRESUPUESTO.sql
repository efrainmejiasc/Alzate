USE [Medeski]
GO

/****** Object:  View [dbo].[VW_SALIDA_PRESUPUESTO]    Script Date: 07/24/2017 11:37:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[VW_SALIDA_PRESUPUESTO] AS
SELECT	sal.[sali_consecutivo],
		pers.pers_identificacion identificacion, pers.pers_nombres nombre,  pr.prod_descripcion producto, 
		prit.prit_item item, cuenta.cuen_auxiliar + '-' + cuenta.cuen_descripcion cuenta,
		ceco.cost_codigo ceco, per.peri_ano ano, sal.sali_mes mes, parm.parm_descripcion, 
		par.parm_codigo moneda, 
		sal.sali_valor valor, sal.[sali_tipo], pers.[pers_usudom]
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
INNER JOIN [dbo].[GE_TCLASESPARAMETROS] cpar
ON cpar.[clap_nombre] = 'MESES'
INNER JOIN [dbo].[GE_TPARAMETROS] parm
ON cpar.[clap_clase] = parm.[clap_clase]
AND parm.[parm_codigo] = sal.sali_mes


GO


