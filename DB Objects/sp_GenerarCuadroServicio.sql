USE [Medeski]
GO
/****** Object:  StoredProcedure [dbo].[sp_GenerarCuadroServicio]    Script Date: 09/25/2017 8:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mauricio Escobar
-- Create date: 2017-09-25
-- Description:	Generación Cuadro de Servicios
-- =============================================
ALTER PROCEDURE [dbo].[sp_GenerarCuadroServicio]
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
