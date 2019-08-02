<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionIntemedios.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionIntemedios" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">

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
              window.setTimeout(function () {s.batchEditApi.SetCellValue(e.visibleIndex, "dint_valor", retorno, null, true); }, 0);
          }
          else {

               if (fieldName == "dint_valor") {
                   var price = parseFloat(valorItem.GetText());
                   s.batchEditApi.SetCellValue(e.visibleIndex, "dint_valor_asignado", retorno * price, null, true);
              }
          }
      }

      function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
          var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "dint_valor");
          var newValue = rowValues[(grid.GetColumnByField("dint_valor").index)].value;
          var dif = isDeleting ? -newValue : newValue - originalValue;
          var retorno;
          //var dif1 = dif * 100;
          var dif1 = dif;

          if ((parseFloat(labelSum.GetValue()) + dif1) <= 100) {
              labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif1).toFixed(1));
              retorno = newValue ;
          }
          else {
              alert("La suma de los porcentajes no puede ser mayor a 100");
              retorno = 0 ;

          }

          return retorno;
      }

      var fieldName;
      function OnBatchEditStartEditing(s, e) {

          fieldName = e.focusedColumn.fieldName;

          if (e.focusedColumn.fieldName == 'dint_valor_asignado') {
              e.cancel = true;
              s.batchEditApi.EndEdit();
              window.setTimeout(function () { s.batchEditApi.StartEdit(e.visibleIndex + 1, 0); }, 0);
          }
      }
    </script>   

     <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
         <div id="Titulos" class="titulo1">
             <div id="divicono">
                 <i class="la la-briefcase d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i>
             </div>
             <div id="divtitulo">
                 <h3>Cuadro de Servicios 
                     <br /></h3>
             </div>
             <ol class="breadcrumb">
                 <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i>Inicio</a></li>
                 <li class="breadcrumb-item active">Distribución Intermedios</li>
             </ol>
         </div>
         <div id="SubTitulos" class="titulo2">
             <h6>Distribución Intermedios<br /></h6>
         </div>
         <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click">
         </dx:ASPxButton>
         <br />
         <br />
         <div id="ContenedorFormulario" class="">
             <div class="form-row">
                 <div class="form-group col-md-6">
                     <dx:ASPxComboBox runat="server" ID="cmbProducto" ClientInstanceName="cmbProducto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" AutoPostBack="true" />
                 </div>
                 <div class="form-group col-md-6">
                     <dx:ASPxComboBox runat="server" ID="cmbItem" ClientInstanceName="cmbItem" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Item" OnSelectedIndexChanged="cmbItem_SelectedIndexChanged" AutoPostBack="true" />
                 </div>
             </div>
             <div class="form-row">
                 <div class="form-group col-md-12">
                     <dx:ASPxTextBox runat="server" DisplayFormatString="C0" ID="valorItem" ClientInstanceName="valorItem" CssClass="form-control" ToolTip="Valor Item" ReadOnly="true"  NullText="Valor Item"/>
                 </div>
             </div>
         </div>
         <div>
             <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
         </div>
         <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="dint_producto_directo"
            OnCustomButtonCallback="grid_CustomButtonCallback" OnDataBinding="grid_DataBinding" OnRowUpdating="grid_RowUpdating">
             <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
             <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
             <Settings GridLines="Horizontal" />
             <Columns>
                 <dx:GridViewDataTextColumn Caption="dint_producto_directo" FieldName="dint_producto_directo" VisibleIndex="0" Visible="false" /> 
                 <dx:GridViewDataTextColumn Caption="dint_consecutivo" FieldName="dint_consecutivo" VisibleIndex="1" Visible="false" />
                 <dx:GridViewDataTextColumn Caption="dint_producto_intermedio" FieldName="dint_producto_intermedio" VisibleIndex="2" Visible="false" />
                 <dx:GridViewDataTextColumn Caption="dint_item_intermedio" FieldName="dint_item_intermedio" VisibleIndex="3" Visible="false" />
                 <dx:GridViewDataTextColumn Caption="Producto Directo" EditFormSettings-Visible="False" FieldName="GE_TPRODUCTOS.prod_descripcion" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                     <Settings AutoFilterCondition="Contains" />
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataSpinEditColumn CellStyle-CssClass="cell-edit" FieldName="dint_valor" Caption="Distribución" VisibleIndex="5" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2" 
                    PropertiesSpinEdit-MaxValue="100" PropertiesSpinEdit-SpinButtons-Enabled="false">
                     <PropertiesSpinEdit DisplayFormatString="N2"  NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                         <SpinButtons Enabled="False">
                         </SpinButtons>
                     </PropertiesSpinEdit>
                     <Settings AutoFilterCondition="Contains" />
                     <FooterTemplate>
                        TPorcentaje % = 
                         <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="labelSum" Text='<%# string.Format("{0:N2}",GetTotalSummaryValue()) %>'>
                         </dx:ASPxLabel>
                     </FooterTemplate>
                     <Settings AutoFilterCondition="Contains" />
                 </dx:GridViewDataSpinEditColumn>
                 <dx:GridViewDataSpinEditColumn FieldName="dint_valor_asignado" Caption="Valor Distribución" VisibleIndex="6" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="20" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2"
                    PropertiesSpinEdit-SpinButtons-Enabled="false">
                     <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                         <SpinButtons Enabled="False">
                         </SpinButtons>
                     </PropertiesSpinEdit>
                     <Settings AutoFilterCondition="Contains" />
                 </dx:GridViewDataSpinEditColumn>
                 <dx:GridViewDataSpinEditColumn FieldName="dint_valor_item" CellStyle-HorizontalAlign="Left" Caption="Valor Item" VisibleIndex="7" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="20" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2" ReadOnly ="true"
                    PropertiesSpinEdit-SpinButtons-Enabled="false" Visible="false">
                     <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                         <SpinButtons Enabled="False">
                         </SpinButtons>
                     </PropertiesSpinEdit>
                 </dx:GridViewDataSpinEditColumn>
             </Columns>
             <Settings ShowStatusBar="Hidden"/>
             <SettingsPager Mode="ShowAllRecords">
             </SettingsPager>
             <Settings VerticalScrollableHeight="350" />
             <SettingsEditing Mode="Batch" />
             <Settings ShowFooter="true" />
             <TotalSummary>
                 <dx:ASPxSummaryItem SummaryType="Sum" FieldName="dint_valor" Tag="TPorcentaje" DisplayFormat="N2"/>
             </TotalSummary>
             <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" BatchEditStartEditing="OnBatchEditStartEditing" />
         </dx:ASPxGridView>
         <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId">
         </dx:ASPxHiddenField>
         <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" AutoPostBack="False" Enabled ="false">
             <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = true;}" />
         </dx:ASPxButton>
         <br />
         <br />
         <br />
         <br />
     </div>
</asp:Content>
