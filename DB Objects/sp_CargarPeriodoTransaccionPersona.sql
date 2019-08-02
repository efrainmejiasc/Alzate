USE [Medeski]
GO

/****** Object:  StoredProcedure [dbo].[sp_CargarPeriodoTransaccionPersona]    Script Date: 07/24/2017 1:10:22 AM ******/
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


