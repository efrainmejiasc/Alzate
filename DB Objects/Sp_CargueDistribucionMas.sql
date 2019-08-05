USE [Medeski]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CargueDistribucionMas] 
(
  @dmas_periodo INT,
  @dmas_producto INT,
  @dmas_valor DECIMAL,
  @dmas_usuario VARCHAR(50),
  @dmas_fecha DATETIME
)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Bandera INT
	         SET @dmas_periodo = (SELECT peri_consecutivo FROM GE_TPERIODOPRESUPUESTO WHERE peri_Activo = 1)

	         SET @Bandera = (SELECT dmas_consecutivo FROM GE_TDISTRIBUCIONMASPROCESOS
			                                         WHERE dmas_producto = @dmas_producto 
														   AND dmas_periodo = @dmas_periodo)
														   
             IF(@Bandera >= 1)
			     Begin
				     UPDATE GE_TDISTRIBUCIONMASPROCESOS SET  dmas_valor = @dmas_valor	
					                                          WHERE dmas_producto = @dmas_producto 
														      AND dmas_periodo = @dmas_periodo                                            
				 End
             ELSE
                  Begin
				      INSERT INTO 
					        GE_TDISTRIBUCIONMASPROCESOS (dmas_periodo,dmas_producto,dmas_valor,dmas_usuario,dmas_fecha)  
					                                          VALUES 
														     (@dmas_periodo, @dmas_producto,@dmas_valor,@dmas_usuario,@dmas_fecha) 
					                
				 End

END
