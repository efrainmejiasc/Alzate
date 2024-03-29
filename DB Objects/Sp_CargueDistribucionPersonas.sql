USE [Medeski]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CargueDistribucionPersonas]    Script Date: 02/08/2019 10:08:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CargueDistribucionPersonas] 
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
														   AND dper_periodo = @dper_periodo
														   AND dper_persona = @dper_persona) 
             IF(@Bandera >= 1)
			     Begin
				     UPDATE GE_TDISTRIBUCIONDEDICACIONPERSONA SET  dper_valor = @dper_valor	
					                                          WHERE dper_producto = @dper_producto 
															  OR dper_servidor = @dper_servidor
														      AND dper_periodo = @dper_periodo
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
