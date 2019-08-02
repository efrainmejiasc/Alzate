<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCuentasEspeciales_form.aspx.cs" Inherits="MedeskiView.Forms.frmCuentasEspeciales_form"   EnableEventValidation="false"%>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        var fieldName;
        function OnBatchEditStartEditing(s, e) {
            fieldName = e.focusedColumn.fieldName;

            if (e.focusedColumn.fieldName != 'valor_a' && e.focusedColumn.fieldName != 'valor_b') {
                e.cancel = true;
                s.batchEditApi.EndEdit();
                window.setTimeout(function () { s.batchEditApi.StartEdit(e.visibleIndex + 1, 0); }, 0);
            }
        }


        function OnBatchEditEndEditing(s, e) {

            if (fieldName == "valor_a" || fieldName == "valor_b") {
                var retorno = CalculateSummary(s, e.rowValues, e.visibleIndex, false);
                e.rowValues[(s.GetColumnByField(fieldName).index)] = { value: retorno, text: "$" + addCommas(retorno) };
            }

            if (fieldName == "valor_a" && s.GetColumnByField("trm_a").visible != false)
            {

                trm = parseInt(e.rowValues[(s.GetColumnByField('trm_a').index)].value);
                equiv = parseFloat(retorno) / parseFloat(trm);
                s.batchEditApi.SetCellValue(e.visibleIndex, "equiv_trm_a", equiv, null, true);

            }
            else if (fieldName == "valor_b" && s.GetColumnByField("trm_b").visible != false)
            {

                trm = parseInt(e.rowValues[(s.GetColumnByField('trm_b').index)].value);
                equiv = parseFloat(retorno) / parseFloat(trm);
                s.batchEditApi.SetCellValue(e.visibleIndex, "equiv_trm_b", equiv, null, true);

            }
        }


        function GetPickUpPoints(values) {
            alert(values);
            
        }


        function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {

            var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, fieldName);
            var newValue = rowValues[(grid.GetColumnByField(fieldName).index)].value;

            // trm = grid.batchEditApi.GetCellValue(visibleIndex, 'trm_a');
            // alert(trm)
            //trm = parseInt(e.rowValues[(s.GetColumnByField('trm_a').index)].value);
            //equiv = parseDouble(retorno) / parseDouble(trm);
            //e.rowValues[(s.GetColumnByField('equiv_trm_a').index)] = { value: equiv, text: "$" + addCommas(equiv) };


            newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

            if (isNaN(parseInt(txtSuma.Get("txtSuma")))) {
                valor = 0;
            } else {
                valor = txtSuma.Get("txtSuma");
            }
            
            suma = parseInt(valor) - parseInt(originalValue) + parseInt(newValue);

            if (txtSvalor.GetValue() == null) {
                errado("Error", "El campo valor en el formulario es requerido");
                return 0;
            } else if (suma > txtSvalor.GetValue()) {
                errado("Error", "La suma de meses no debe ser mayor a $" + txtSvalor.GetValue().toFixed());
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
                errado("Error", "La suma de meses no debe ser diferente de $" + txtSvalor.GetValue().toFixed())
                return false;
            } else {
                return true;
            }
        }

        function validaNumericos(event) {
            if (event.charCode >= 48 && event.charCode <= 57) {
                return true;
            }
            return false;
        }

        function DiferenteCeroTxtCantidad() {
            var j = $('#ctl00_ctl00_MainPane_Content_MainContent_txtCantidad_I').val();
            console.log(j);
            if (j == 0)
                $('#ctl00_ctl00_MainPane_Content_MainContent_txtCantidad_I').val('');

        }

        function DiferenteCeroTxtSValor() {
            
            var j = $('#ctl00_ctl00_MainPane_Content_MainContent_txtSvalor_I').val();
            console.log(j);
            if (j == 0)
                $('#ctl00_ctl00_MainPane_Content_MainContent_txtSvalor_I').val('');
        }


    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-dollar d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Presupuesto<br /></h3>
			</div><ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>            
                <li class="breadcrumb-item"><a href="frmCuentasEspeciales.aspx">Lista de Presupuestos</a></li>
                <li class="breadcrumb-item active">Formulario de Presupuesto</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Presupuesto<br /></h6>
	    </div>
        
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec"></dx:ASPxButton>        
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />
        
        <div id="ContenedorFormulario" class="">
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbEmpresa"  ClientInstanceName="cmbEmpresa" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Centro Costo" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged" AutoPostBack="true" />
                </div>

                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCCosto"  ClientInstanceName="cmbCCosto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Centro Costo" OnSelectedIndexChanged="cmbCCosto_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbProducto" ClientInstanceName="cmbProducto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbItem"  ClientInstanceName="cmbItem" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Item" OnSelectedIndexChanged="cmbItem_SelectedIndexChanged" AutoPostBack="true" placeholder="Seleccionar Item"/>
                </div>
            </div>

            <div class="form-row">                
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtSvalor" runat="server" 
                        DisplayFormatString="C0"
                        MaxLength="20"
                        Theme="MetropolisBlue" ClientInstanceName="txtSvalor" 
                        MinValue="1"  MaxValue="99999999999999999999" CssClass="form-control" ToolTip ="Valor" NullText ="Valor"
                        AllowMouseWheel="False" HorizontalAlign="Right"  >
                        <SpinButtons Enabled="False" />
                            <ClientSideEvents KeyPress="function(s, e) {
                            if(e.htmlEvent.keyCode == 13) {
                                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                            }}" />
                    </dx:ASPxSpinEdit>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbMoneda_SelectedIndexChanged" ID="cmbMoneda" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Moneda" onkeypress="return validaNumericos(event)" />
                </div>                
            </div>
             
            <div class="form-row">
                
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtCantidad"  Name ="txtCantidad" ClientInstanceName="txtCantidad" CssClass="form-control" NullText="Cantidad" ToolTip="Seleccionar Cantidad" AutoCompleteType="Disabled" onkeypress="return validaNumericos(event)" onkeyup="DiferenteCeroTxtCantidad()">
       
                    </dx:ASPxTextBox> 
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo"/>
                </div>
            </div>

            <div class="form-row">
               <div class="form-group col-md-6">
                    <dx:ASPxRadioButton GroupName="amortizar" AutoPostBack="True" CssClass="form-control" runat="server" ID="chkAmortizar" ClientInstanceName="chkAmortizar" Text="Amortizar en todos los meses" ToolTip="Amortizar en todos los meses" Enabled="true" OnCheckedChanged="amortizarClicked"/>
               </div>
                <div class="form-group col-md-6">
                    <dx:ASPxRadioButton GroupName="amortizar" AutoPostBack="True" CssClass="form-control" runat="server" ID="chkAmortEntre" ClientInstanceName="chkAmortEntre" Text="Amortizar en rango de meses" ToolTip="Amortizar en rango de meses" Enabled="true" OnCheckedChanged="amortizarClicked"/>

               </div>
            </div>

            <div class="form-row">
               <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMesIni"  ClientInstanceName="cmbMesIni" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Mes Inicial" OnSelectedIndexChanged="cmbMesIni_SelectedIndexChanged" AutoPostBack="true" />
               </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMesFin"  ClientInstanceName="cmbMesFin" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Mes Final"  OnSelectedIndexChanged="cmbMesFin_SelectedIndexChanged" AutoPostBack="true">
                        
                    </dx:ASPxComboBox>
               </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxMemo ID="txtObservacion" Rows="5" MaxLength="1000" runat="server" ClientInstanceName="txtObservacion" ToolTip="Observación" NullText="Observación" CssClass="form-control">
                        <ClientSideEvents KeyPress="function(s, e) {
                        if(e.htmlEvent.keyCode == 13) {
                            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                        }}" />
                    </dx:ASPxMemo>
                </div>
            </div>            
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
    
        <dx:ASPxGridView ID="grid_meses" ClientInstanceName="grid_meses" runat="server"
            Visible="false"
            AutoGenerateColumns="False" Theme="MetropolisBlue"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="mes_a" Width="100%"
            OnDataBinding="grid_DataBinding" 
            CssClass="gridview"
            OnRowUpdating="grid_RowUpdating">
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Settings GridLines="Horizontal" />

            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="consec_a" FieldName="consec_a" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataColumn Caption="consec_b" FieldName="consec_b" VisibleIndex="0" Visible="false" />
                
            <%-- Columna A --%>
                <dx:GridViewDataColumn Caption="Mes" FieldName="mes_a" EditFormSettings-Visible="False" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                    <Settings AutoFilterCondition="Contains" />                                    
                </dx:GridViewDataColumn>

                <dx:GridViewDataTextColumn Width="10%"  Caption="TRM" FieldName="trm_a" Visible="false" VisibleIndex="2" ReadOnly="true">
                    <PropertiesTextEdit DisplayFormatString="C0"  ClientInstanceName="trm_a"/>
                </dx:GridViewDataTextColumn>
                
                <dx:GridViewDataTextColumn Caption="Valor en Pesos" CellStyle-CssClass="cell-edit" FieldName="valor_a" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />                                    
                </dx:GridViewDataTextColumn>
                
                <dx:GridViewDataTextColumn  Caption="Valor Equivalente" FieldName="equiv_trm_a" Visible="false" VisibleIndex="4">
                    <PropertiesTextEdit DisplayFormatString="C0"/>
                </dx:GridViewDataTextColumn>
                
            <%-- Columna B --%>

                <dx:GridViewDataColumn Caption="Mes" FieldName="mes_b" EditFormSettings-Visible="False" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center"  ReadOnly="true">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                
                <dx:GridViewDataTextColumn Width="10%"  Caption="TRM" FieldName="trm_b" Visible="false" VisibleIndex="12" ReadOnly="true">
                   <PropertiesTextEdit DisplayFormatString="C0" />             
                </dx:GridViewDataTextColumn>
                
                <dx:GridViewDataTextColumn Caption="Valor en Pesos" CellStyle-CssClass="cell-edit" FieldName="valor_b" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />                            
                </dx:GridViewDataTextColumn>      

                <dx:GridViewDataTextColumn  Caption="Valor Equivalente" FieldName="equiv_trm_b" Visible="false" VisibleIndex="14" ReadOnly="true">
                    <PropertiesTextEdit DisplayFormatString="C0" />             
                </dx:GridViewDataTextColumn>
                
                                          
            </Columns>
            <Settings ShowStatusBar="Hidden"/>
            <SettingsEditing Mode="Batch" />            
            <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" BatchEditStartEditing="OnBatchEditStartEditing" />            
        </dx:ASPxGridView>
              
              
        <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="txtSuma" runat="server" ClientInstanceName="txtSuma"></dx:ASPxHiddenField>        
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar boton-formulariosintprim">
            <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = checkValues(); }" />
        </dx:ASPxButton>
        <br /><br /><br />  

    </div>
</asp:Content>
