

SELECT * FROM [dbo].[GE_TDISTRIBUCIONDEDICACIONPERSONA] where dper_consecutivo > = 1002

SELECT * FROM GE_TPERSONAS WHERE pers_consecutivo = 4 OR pers_consecutivo = 70 OR pers_consecutivo = 58

  SELECT A.*, B.* ,C.*, D.* FROM  GE_TCARGUEDRIVERS A  
								   INNER JOIN  GE_TCENTROSCOSTOS B ON  A.carg_ccosto = B.cost_consecutivo
								   INNER JOIN  GE_TCENTROSOPERACION C ON B.cost_centro_operacion = C.ceop_codigo
								   INNER JOIN  GE_TREDISTRIBUCION_DRIVERS  D ON A.carg_consecutivo = D.care_cargue_driver  

								   SELECT * FROM GE_TCUENTAS
SELECT * FROM GE_TPERIODOTRANSACCIONES

