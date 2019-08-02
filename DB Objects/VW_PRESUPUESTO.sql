USE [Medeski]
GO

/****** Object:  View [dbo].[VW_PRESUPUESTO]    Script Date: 07/24/2017 1:08:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[VW_PRESUPUESTO] AS
SELECT	pers.pers_identificacion identificacion, pers.pers_nombres nombre,  pr.prod_descripcion producto, prit.prit_item item, v.[vhpg_valor] + cuenta.cuen_auxiliar cuenta,
		v.[vhpg_valor] + ceco.cost_codigo ceco, per.peri_ano ano, sal.sali_mes mes, par.parm_codigo moneda, sal.sali_valor valor
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
UNION
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





GO


