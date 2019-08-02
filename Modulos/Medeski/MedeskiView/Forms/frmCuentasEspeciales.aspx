<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCuentasEspeciales.aspx.cs" Inherits="MedeskiView.Forms.frmCuentasEspeciales"   EnableEventValidation="false"%>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        var fieldName;
        function OnBatchEditStartEditing(s, e) {
            fieldName = e.focusedColumn.fieldName;            
        }


        function OnBatchEditEndEditing(s, e) {
            if (fieldName == "valor_a" || fieldName == "valor_b") {
                var retorno = CalculateSummary(s, e.rowValues, e.visibleIndex, false);
                e.rowValues[(s.GetColumnByField(fieldName).index)] = { value: retorno, text: "$" + addCommas(retorno) };
            }            
        }


        function GetPickUpPoints(values) {
            alert(values);
            
        }


        function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {

            var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, fieldName);
            var newValue = rowValues[(grid.GetColumnByField(fieldName).index)].value;

            newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

            if (isNaN(parseInt(txtSuma.Get("txtSuma")))) {
                valor = 0;
            } else {
                valor = txtSuma.Get("txtSuma");
            }
            
            suma = parseInt(valor) - parseInt(originalValue) + parseInt(newValue);
            
            if (txtSvalor.GetValue() == null) {
                alert("El campo valor es requerido");
                return 0;
            }else if (suma > txtSvalor.GetValue()) {
                alert("La suma de meses no debe ser diferente de $" + txtSvalor.GetValue().toFixed());
                return originalValue;
            } else {
                txtSuma.Set("txtSuma", suma);
                return newValue;
            }            
        }


        function OnUpdateClick(s, e) {
            try {
                grid_meses.UpdateEdit();
            }
            catch (err) {
                alert(err.message)
            }
        }


        function checkValues() {
            if (parseFloat(txtSvalor.GetValue()) != parseFloat(txtSvalor.GetValue())) {
                alert("La suma de meses no debe ser diferente de $" + txtSvalor.GetValue().toFixed())
                return false;
            } else {
                return true;
            }
        }

    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-dollar d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Presupuesto<br /></h3>
			</div>           
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Lista de Presupuestos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Presupuesto<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <br /><br /><br />    

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
			    <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbSubcategoria_SelectedIndexChanged" ID="cmbSubcategoria" DropDownStyle="DropDownList" CssClass="form-control labelinterno" ToolTip="Seleccione Subcategoria" NullText="Seleccione Subcategoria" />
			    </div>
		    </div>
        </div>

        <dx:ASPxGridView runat="server" ID="gvPrItems" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            ClientInstanceName="gvPrItems" EnableRowsCache="False" KeyFieldName="petr_consecutivo"
            Width="100%"
            OnCustomButtonCallback="grid_CustomButtonCallback" EnableCallBacks="False">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="petr_consecutivo" FieldName="petr_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_ccosto" FieldName="GE_TCENTROSCOSTOS.cost_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_producto" FieldName="GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo" VisibleIndex="2" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_item" FieldName="GE_TPRODUCTOSITEMS.prit_consecutivo" VisibleIndex="3" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_mes" FieldName="petr_mes" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_activo" FieldName="petr_activo" VisibleIndex="5" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_moneda" FieldName="petr_moneda" VisibleIndex="6" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_cantidad" FieldName="petr_cantidad" VisibleIndex="7" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_observacion" FieldName="petr_observacion" VisibleIndex="8" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_amortizar" FieldName="petr_amortizar" VisibleIndex="9" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_meses_amortizar" FieldName="petr_meses_amortizar" VisibleIndex="10" Visible="false" />
                
                <dx:GridViewDataColumn Caption="EMPRESA" FieldName="GE_TCENTROSCOSTOS.cost_empresa" Visible="false" />
                <dx:GridViewDataColumn Caption="Empresa" FieldName="GE_TCENTROSCOSTOS.GE_TCOMPANIAS.comp_nombre" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="GE_TCENTROSCOSTOS.cost_descripcion" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" >
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                
                <dx:GridViewDataColumn Caption="Producto" FieldName="GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_descripcion" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Item" FieldName="GE_TPRODUCTOSITEMS.prit_item" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Moneda" FieldName="GE_TPARAMETROS.parm_codigo" VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Caption="Valor" FieldName="petr_valor" VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="N0"/>
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn VisibleIndex="16" Caption="Acción" >
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png" />
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>        
    </div>
</asp:Content>
