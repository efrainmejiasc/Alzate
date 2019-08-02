<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmVentasPYG.aspx.cs" Inherits="MedeskiView.Forms.frmVentasPYG" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagName="VentanaValidaciones" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        var fieldName;
        function OnBatchEditStartEditing(s, e) {
            fieldName = e.focusedColumn.fieldName;
        }


        function OnBatchEditEndEditing(s, e) {
            if (fieldName != "ceop_codigo") {
                var retorno = CalculateSummary(s, e.rowValues, e.visibleIndex, false);
                column = s.GetColumnByField(fieldName);
                e.rowValues[(column.index)] = { value: retorno, text: "$" + addCommas(retorno) };
            }
        }

        function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
            var retorno;

            // FORECAST
            if (fieldName == "forecast_ventas") { // suma forecast_ventas

                var originalValue = parseInt(grid.batchEditApi.GetCellValue(visibleIndex, "forecast_ventas"));
                var newValue = rowValues[(grid.GetColumnByField("forecast_ventas").index)].value;
                newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

                var dif1 = isDeleting ? -newValue : newValue - originalValue;
                var totalValue = labelForecastVentas.GetValue().replace("$", "").split('.').join("")

                labelForecastVentas.SetValue("$" + addCommas(parseFloat(totalValue) + parseFloat(dif1)));
                retorno = newValue;

            } else if (fieldName == "forecast_directos") { // suma forecast_directos

                var originalValue = parseInt(grid.batchEditApi.GetCellValue(visibleIndex, "forecast_directos"));
                var newValue = rowValues[(grid.GetColumnByField("forecast_directos").index)].value;
                newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

                var dif1 = isDeleting ? -newValue : newValue - originalValue;
                var totalValue = labelForecastDirectos.GetValue().replace("$", "").split('.').join("")

                labelForecastDirectos.SetValue("$" + addCommas(parseFloat(totalValue) + parseFloat(dif1)));
                retorno = newValue;

            } else if (fieldName == "forecast_indirectos") { // suma forecast_indirectos

                var originalValue = parseInt(grid.batchEditApi.GetCellValue(visibleIndex, "forecast_indirectos"));
                var newValue = rowValues[(grid.GetColumnByField("forecast_indirectos").index)].value;
                newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

                var dif1 = isDeleting ? -newValue : newValue - originalValue;
                var totalValue = labelForecastIndirectos.GetValue().replace("$", "").split('.').join("")

                labelForecastIndirectos.SetValue("$" + addCommas(parseFloat(totalValue) + parseFloat(dif1)));
                retorno = newValue;

            } // PPTO
            else if (fieldName == "ventas") { // suma ventas

                var originalValue = parseInt(grid.batchEditApi.GetCellValue(visibleIndex, "ventas"));
                var newValue = rowValues[(grid.GetColumnByField("ventas").index)].value;
                newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

                var dif1 = isDeleting ? -newValue : newValue - originalValue;
                var totalValue = labelVentas.GetValue().replace("$", "").split('.').join("")

                labelVentas.SetValue("$" + addCommas(parseFloat(totalValue) + parseFloat(dif1)));
                retorno = newValue;

            } else if (fieldName == "directos") { // suma directos

                var originalValue = parseInt(grid.batchEditApi.GetCellValue(visibleIndex, "directos"));
                var newValue = rowValues[(grid.GetColumnByField("directos").index)].value;
                newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);
                
                var dif1 = isDeleting ? - newValue : newValue - originalValue;
                var totalValue = labelDirectos.GetValue().replace("$", "").split('.').join("")

                if (parseFloat(totalValue) + parseFloat(dif1) <= parseFloat(txtSumaDirectos.Get("txtSumaDirectos"))) {

                    labelDirectos.SetValue("$" + addCommas(parseFloat(totalValue) + parseFloat(dif1)));
                    lblSumaDirectos.Set("lblSumaDirectos", (parseFloat(totalValue) + parseFloat(dif1)).toFixed());
                    retorno = newValue;
                }
                else {
                    swal("Errado", "La suma de los Directos no puede ser mayor a " + txtSumaDirectos.Get("txtSumaDirectos"));
                    retorno = originalValue;
                }

            } else if (fieldName == "indirectos") { // suma indirectos
                var originalValue = parseInt(grid.batchEditApi.GetCellValue(visibleIndex, "indirectos"));
                var newValue = rowValues[(grid.GetColumnByField("indirectos").index)].value;
                newValue = newValue == null || isNaN(parseInt(newValue)) ? 0 : parseInt(newValue);

                var dif1 = isDeleting ? -newValue : newValue - originalValue;
                var totalValue = labelIndirectos.GetValue().replace("$", "").split('.').join("")

                if (parseFloat(totalValue) + parseFloat(dif1) <= parseFloat(txtSumaIndirectos.Get("txtSumaIndirectos"))) {

                    labelIndirectos.SetValue("$" + addCommas(parseFloat(totalValue) + parseFloat(dif1)));
                    lblSumaIndirectos.Set("lblSumaIndirectos", (parseFloat(totalValue) + parseFloat(dif1)).toFixed());
                    retorno = newValue;
                }
                else {
                    swal("Errado", "La suma de los Indirectos no puede ser mayor a " + "$" + addCommas(txtSumaIndirectos.Get("txtSumaIndirectos")));
                    retorno = originalValue;
                }
            }

            return retorno == null ? 0 : retorno;
        }


        function OnUpdateClick(s, e) {
            try {
                grid_Driver.UpdateEdit();
            }
            catch (err) {
                alert(err.message)
            }
        }

        function checkValues() {
            if (parseFloat(txtSumaDirectos.Get("txtSumaDirectos")) != parseFloat(lblSumaDirectos.Get("lblSumaDirectos"))) {
                swal("Errado", "Los valores Directos No coinciden con $ " + txtSumaDirectos.Get("txtSumaDirectos"))
                return false;
            } else if (parseFloat(txtSumaIndirectos.Get("txtSumaIndirectos")) != parseFloat(lblSumaIndirectos.Get("lblSumaIndirectos"))) {
                swal("Errado", "Los valores Indirectos No coinciden con $" + txtSumaIndirectos.Get("txtSumaIndirectos"))
                return false;
            } else {
                return true;
            }
        }

    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-area-chart d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>PyG<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Cuadro de Ventas PyG</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Cuadro de Ventas PyG<br /></h6>
	    </div>

         <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">                    
                </div>
            </div>                
        </div>
        <dx:ASPxCallbackPanel ID="cbpProgreso" ClientInstanceName="cbpProgreso" runat="server">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <div class="grid-16 alpha">
                        <!-- Hiddens  Reales -->
                        
                        <dx:ASPxHiddenField ID="txtSumaDirectos" runat="server" ClientInstanceName="txtSumaDirectos"></dx:ASPxHiddenField>
                        <dx:ASPxHiddenField ID="txtSumaIndirectos" runat="server" ClientInstanceName="txtSumaIndirectos"></dx:ASPxHiddenField>

                        <!-- Hiddens  Recalculados -->
                        <dx:ASPxHiddenField ID="lblSumaDirectos" runat="server" ClientInstanceName="lblSumaDirectos"></dx:ASPxHiddenField>
                        <dx:ASPxHiddenField ID="lblSumaIndirectos" runat="server" ClientInstanceName="lblSumaIndirectos"></dx:ASPxHiddenField>                                                            
                    </div>
                    <div>
                        <uc1:VentanaValidaciones ID="VentanaValidaciones" runat="server" />
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

        <dx:ASPxGridView ID="grid_Driver" ClientInstanceName="grid_Driver" runat="server"
            AutoGenerateColumns="False" Theme="MetropolisBlue"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="ceop_codigo" Width="100%"
            OnDataBinding="grid_DataBinding" 
            CssClass="gridview"
            OnRowUpdating="grid_RowUpdating">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn FieldName="ceop_codigo" EditFormSettings-Visible="False" Caption="CEOP" VisibleIndex="1" ReadOnly="true">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>

                <%-- Forecast --%>
                <dx:GridViewDataTextColumn CellStyle-CssClass="cell-edit"  FieldName="forecast_ventas" Caption="Forecast Ventas" VisibleIndex="2">
                        <Settings AutoFilterCondition="Contains" />  
                        <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Forecast Ventas = 
                        <dx:ASPxLabel ID="labelForecastVentas" runat="server" ClientInstanceName="labelForecastVentas" Text='<%# string.Format("{0:C0}",GetTotalesXfila("Forecast_Ventas")) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn CellStyle-CssClass="cell-edit"  FieldName="forecast_directos" Caption="Forecast Directos" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Forecast Directos = 
                        <dx:ASPxLabel ID="labelForecastDirectos" runat="server" ClientInstanceName="labelForecastDirectos" Text='<%# string.Format("{0:C0}",GetTotalesXfila("Forecast_Directos")) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn CellStyle-CssClass="cell-edit"  FieldName="forecast_indirectos" Caption="Forecast Indirectos" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Forecast Indirectos = 
                        <dx:ASPxLabel ID="labelForecastIndirectos" runat="server" ClientInstanceName="labelForecastIndirectos" Text='<%# string.Format("{0:C0}",GetTotalesXfila("Forecast_Indirectos")) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                
                <%-- PPTO --%>
                <dx:GridViewDataTextColumn CellStyle-CssClass="cell-edit"  FieldName="ventas" Caption="Ventas" VisibleIndex="5">
                        <Settings AutoFilterCondition="Contains" />  
                        <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Ventas = 
                        <dx:ASPxLabel ID="labelVentas" runat="server" ClientInstanceName="labelVentas" Text='<%# string.Format("{0:C0}",GetTotalesXfila("Ventas")) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn CellStyle-CssClass="cell-edit"  FieldName="directos" Caption="Directos" VisibleIndex="6">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Directos = 
                        <dx:ASPxLabel ID="labelDirectos" runat="server" ClientInstanceName="labelDirectos" Text='<%# string.Format("{0:C0}",GetTotalesXfila("Directos")) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn CellStyle-CssClass="cell-edit"  FieldName="indirectos" Caption="Indirectos" VisibleIndex="7 ">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Indirectos = 
                        <dx:ASPxLabel ID="labelIndirectos" runat="server" ClientInstanceName="labelIndirectos" Text='<%# string.Format("{0:C0}",GetTotalesXfila("Indirectos")) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>

            </Columns>  
            <Settings ShowStatusBar="Hidden"/>
            <SettingsEditing Mode="Batch" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Settings VerticalScrollableHeight="350" />

            <Settings ShowFooter="True" />
                <TotalSummary>
                    
                    <dx:ASPxSummaryItem FieldName="forecast_ventas" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Forecast_Ventas"/>
                    <dx:ASPxSummaryItem FieldName="forecast_directos" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Forecast_Directos"/>
                    <dx:ASPxSummaryItem FieldName="forecast_indirectos" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Forecast_Indirectos"/>

                    <dx:ASPxSummaryItem FieldName="ventas" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Ventas"/>
                    <dx:ASPxSummaryItem FieldName="directos" DisplayFormat="Total: {0:0}" SummaryType="Sum" Tag="Directos"/>
                    <dx:ASPxSummaryItem FieldName="indirectos"  DisplayFormat="Total: {0:0}" SummaryType="Sum" Tag="Indirectos"/>

                </TotalSummary>    
            <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" BatchEditStartEditing="OnBatchEditStartEditing" />
        </dx:ASPxGridView>
        
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar boton-formulariosintprim">
            <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = checkValues(); }" />
        </dx:ASPxButton>
        <br /><br />
        <br /><br />
    </div>
</asp:Content>
