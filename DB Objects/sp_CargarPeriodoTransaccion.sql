USE [Medeski]
GO

/****** Object:  StoredProcedure [dbo].[sp_CargarPeriodoTransaccion]    Script Date: 07/24/2017 1:10:57 AM ******/
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


