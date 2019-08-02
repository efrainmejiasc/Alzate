<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionDirectos.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionDirectos" %>
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
          }
      }

     function OnBatchEditEndEditing(s, e) {
         var retorno = CalculateSummary(s, e.rowValues, e.visibleIndex, false);

         if (retorno > 0) {
             window.setTimeout(function () {
                 s.batchEditApi.SetCellValue(e.visibleIndex, "dinf_valor", retorno, null, true);
             }, 0);
         }
         else {
             window.setTimeout(function () {
                 s.batchEditApi.SetCellValue(e.visibleIndex, "dinf_valor", retorno, null, true);
             }, 0);
         }
     }
    
     function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
         var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "dinf_valor");
         var newValue = rowValues[(grid.GetColumnByField("dinf_valor").index)].value;
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

    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-briefcase d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro de Servicios<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Valor Distribución Productos Directos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Valor Distribución Productos Directos<br /></h6>
	    </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <dx:ASPxGridView runat="server" ID="gridServidores" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
                    Width="100%"
                    OnCustomButtonCallback="gridServidores_CustomButtonCallback"
                    OnCustomColumnDisplayText="gridServidores_CustomColumnDisplayText"
                    ClientInstanceName="grid" EnableRowsCache="False" 
                    EnableCallBacks="False" 
                    KeyFieldName="serv_consecutivo" >
                    <Settings GridLines="Horizontal" />
                    <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                    <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="serv_consecutivo" FieldName="serv_consecutivo" VisibleIndex="0" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="Servidor" FieldName="serv_nombre" VisibleIndex="1"/>
                        <dx:GridViewDataTextColumn Caption="Estado" FieldName="serv_activo" CellStyle-HorizontalAlign="Left" VisibleIndex="1" Visible="false"/>
                        <dx:GridViewDataTextColumn Caption="Estado" FieldName="serv_estadoStr" CellStyle-HorizontalAlign="Left" VisibleIndex="1">  <Settings AutoFilterCondition="BeginsWith"/></dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn VisibleIndex="8" Caption="Acción">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                                <Image ToolTip="Seleccionar" Url="../Content/Imagenes/seleccionar.png"/>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    </Columns>                    
                </dx:ASPxGridView>
            </div>

            <div class="form-group col-md-6">
                <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
                    Width="100%"
                    ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="dinf_consecutivo"
                    OnCustomButtonCallback="grid_CustomButtonCallback" OnDataBinding="grid_DataBinding" OnRowUpdating="grid_RowUpdating">
                    <Settings GridLines="Horizontal" />
                    <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                    <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="dinf_consecutivo" FieldName="dinf_consecutivo" VisibleIndex="0" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="dinf_producto" FieldName="dinf_producto" VisibleIndex="1" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="Producto Directo" EditFormSettings-Visible="False" FieldName="GE_TPRODUCTOS.prod_descripcion" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataSpinEditColumn CellStyle-CssClass="cell-edit" FieldName="dinf_valor" Caption="Distribución" VisibleIndex="3" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                            PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="N2" 
                            PropertiesSpinEdit-MaxValue="100" PropertiesSpinEdit-SpinButtons-Enabled="false">
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MaxValue="100" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                                <SpinButtons Enabled="False"></SpinButtons>
                            </PropertiesSpinEdit>
                            <Settings AutoFilterCondition="Contains" />
                            <FooterTemplate>
                                TPorcentaje % = 
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="labelSum" Text='<%# string.Format("{0:N2}",GetTotalSummaryValue()) %>'>
                                </dx:ASPxLabel>
                            </FooterTemplate>
                        </dx:GridViewDataSpinEditColumn>
                    </Columns>
                    <Settings ShowStatusBar="Hidden"/>
                    <SettingsEditing Mode="Batch" />
            
                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                    <Settings VerticalScrollableHeight="285" />

                    <Settings ShowFooter="true" />
                            <TotalSummary>
                            <dx:ASPxSummaryItem SummaryType="Sum" FieldName="dinf_valor" Tag="TPorcentaje" />
                            </TotalSummary>
                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" />
                </dx:ASPxGridView>        
            </div>
        </div>
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" AutoPostBack="False" Enabled="false">
            <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = true;}" />
        </dx:ASPxButton>
        <br/><br/>
        <br/><br/>  

    </div>
</asp:Content>
