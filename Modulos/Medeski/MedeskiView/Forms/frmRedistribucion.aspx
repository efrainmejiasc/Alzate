<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmRedistribucion.aspx.cs" Inherits="MedeskiView.Forms.frmRedistribucion" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">

      function ShowLoginWindow() {
          pcLogin.Show();
      }

      function OnUpdateClick(s, e) {
          try {
              if (grid.batchEditApi.HasChanges()) {
                  grid.UpdateEdit();
              }
          }
          catch (err) {
              alert(err.message)
          }
      }

      function OnBatchEditEndEditing(s, e) {
          var retorno = CalculateSummary(s, e.rowValues, e.visibleIndex, false);

          if (retorno == 0 ) {
              window.setTimeout(function () {
                  s.batchEditApi.SetCellValue(e.visibleIndex, "redi_valor", retorno, null, true);
             }, 0);
          }
          else {

              if (fieldName == "redi_valor") {
                  var price = parseFloat(valorItem.GetText());
                     s.batchEditApi.SetCellValue(e.visibleIndex, "redi_valor_asignado", retorno * price, null, true);
              }
          }
      }

      function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
          var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "redi_valor");
          var newValue = rowValues[(grid.GetColumnByField("redi_valor").index)].value;
          var dif = isDeleting ? -newValue : newValue - originalValue;
          var retorno;
          //var dif1 = dif * 100;
          var dif1 = dif;
       
          if ((parseFloat(labelSum.GetValue()) + dif1) <= 100) {
              labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif1).toFixed(1));
              retorno = newValue;
          }
          else {
              alert("La suma de los porcentajes no puede ser mayor a 100");
              retorno = 0;

          }

          return retorno;
      }

      var fieldName;
      function OnBatchEditStartEditing(s, e) {

            fieldName = e.focusedColumn.fieldName;

            if (e.focusedColumn.fieldName == 'redi_valor_asignado') {
                e.cancel = true;
                s.batchEditApi.EndEdit();
                window.setTimeout(function () { s.batchEditApi.StartEdit(e.visibleIndex + 1, 0); }, 0);
            }
      }
    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-briefcase d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro de Servicios<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Redistribución</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Redistribución<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        
        <dx:ASPxButton ID="btShowModal" runat="server" Text="Detalle" CssClass="nuevo boton-formulariosintsec" AutoPostBack="False" UseSubmitBehavior="false">
            <ClientSideEvents Click="function(s, e) { ShowLoginWindow(); }" />
        </dx:ASPxButton>
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbProducto" ClientInstanceName="cmbProducto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" AutoPostBack="true" />
                </div>            
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" DisplayFormatString="N2" ID="valorItem" ClientInstanceName="valorItem" CssClass="form-control" ToolTip="Valor Producto" ReadOnly="true"  NullText="Valor Producto"/>
                </div>
            </div>
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
        
        <dx:ASPxPopupControl ID="pcLogin" runat="server" Width="80%" CssClass="modal" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Detalle Redistribución" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxGridView runat="server" ID="gridenc" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
                    Width="100%"
                    ClientInstanceName="gridenc" EnableRowsCache="False" KeyFieldName="Item" OnDataBinding="gridenc_DataBinding" Visible="false">
                    <Settings GridLines="Horizontal" />
                    <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                    <Columns>
                        <dx:GridViewDataSpinEditColumn FieldName="total" Caption="Total" VisibleIndex="1" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataTextColumn Caption="Item" FieldName="item" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" ></dx:GridViewDataTextColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_lic" Caption="Licenciamiento" VisibleIndex="3" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_lic_interm" Caption="Licen. intermedio" VisibleIndex="4" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_infr" Caption="Infraestructura" VisibleIndex="5" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_datac" Caption="Datacenter" VisibleIndex="6" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_ga" Caption="MAS GA" VisibleIndex="7" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_softw" Caption="MAS Soft." VisibleIndex="9" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_infr" Caption="MAS Infraest." VisibleIndex="13" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_datac" Caption="MAS Datacenter" VisibleIndex="14" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_gente" Caption="MAS Gente Infra." VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_ga_operaciones" Caption="GA Operaciones" VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_ga_gtecnica" Caption="GA GTecnica" VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_ga_desarrollo" Caption="GA Desarrollo" VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_cdm" Caption="CDM" VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_procesos" Caption="Procesos" VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                    </Columns>
                    <Settings ShowFooter="True" />
            
                    <Settings VerticalScrollableHeight="400" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="total" SummaryType="Sum" Tag="Total"/>
                    </TotalSummary>
                </dx:ASPxGridView>
            </dx:PopupControlContentControl>
        </ContentCollection>
        </dx:ASPxPopupControl>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="redi_producto_dist"
            OnCustomButtonCallback="grid_CustomButtonCallback" OnDataBinding="grid_DataBinding" OnRowUpdating="grid_RowUpdating" Visible ="false">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="redi_producto_dist" FieldName="redi_producto_dist" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="redi_consecutivo" FieldName="redi_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataTextColumn Caption="redi_producto_orig" FieldName="redi_producto_orig" VisibleIndex="2" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Producto Directo" EditFormSettings-Visible="False" FieldName="GE_TPRODUCTOS.prod_descripcion" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="redi_valor" CellStyle-CssClass="cell-edit"  Caption="Distribución" VisibleIndex="5" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2" 
                    PropertiesSpinEdit-MaxValue="100" PropertiesSpinEdit-SpinButtons-Enabled="false" >
                    <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MinValue="0" MaxValue="100" NumberType="Float" AllowMouseWheel="False" >
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                    <FooterTemplate>
                        TPorcentaje = 
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="labelSum" Text='<%# string.Format("{0:N2}",GetTotalSummaryValue()) %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="redi_valor_asignado" Caption="Valor Distribución" VisibleIndex="6" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="20" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2" ReadOnly ="true"
                    PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="redi_valor_producto" EditFormSettings-Visible="False" Caption="Valor Producto" VisibleIndex="7" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="20" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2" ReadOnly ="true"
                    PropertiesSpinEdit-SpinButtons-Enabled="false" Visible="false">
                    <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
            </Columns>
            <Settings ShowStatusBar="Hidden"/>
            <SettingsEditing Mode="Batch" />
            
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Settings VerticalScrollableHeight="350" />

            <Settings ShowFooter="true" />
                <TotalSummary>
                    <dx:ASPxSummaryItem SummaryType="Sum" FieldName="redi_valor" Tag="TPorcentaje" DisplayFormat="N2"/>
                </TotalSummary>
            <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" BatchEditStartEditing="OnBatchEditStartEditing" />
        </dx:ASPxGridView>
        
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" AutoPostBack="False" Enabled="false">
            <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = true;}" />
        </dx:ASPxButton>
        <br /><br />
        <br /><br />

    </div>               
</asp:Content>
