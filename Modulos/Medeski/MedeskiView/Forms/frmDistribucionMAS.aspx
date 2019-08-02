<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionMAS.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionMAS" %>
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
            CalculateSummary(s, e.rowValues, e.visibleIndex, false);        
        }

        function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
            var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "dmas_valor");
            var newValue = rowValues[(grid.GetColumnByField("dmas_valor").index)].value;
            var dif = isDeleting ? -newValue : newValue - originalValue;
            var retorno;

            labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif).toFixed(1));
        }
    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-briefcase  d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro de Servicios<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Distribución MAS</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Distribución MAS<br /></h6>
	    </div>
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="dmas_consecutivo"
            OnCustomButtonCallback="grid_CustomButtonCallback" OnDataBinding="grid_DataBinding" OnRowUpdating="grid_RowUpdating">
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="dmas_consecutivo" FieldName="dmas_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="dmas_producto" FieldName="dmas_producto" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Producto Directo" EditFormSettings-Visible="False" FieldName="GE_TPRODUCTOS.prod_descripcion" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn  CellStyle-CssClass="cell-edit" FieldName="dmas_valor" Caption="Cantidad" VisibleIndex="3" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true"  CellStyle-HorizontalAlign="Left"
                    PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Integer" PropertiesSpinEdit-DisplayFormatString="N0" 
                    PropertiesSpinEdit-MaxValue="100000" PropertiesSpinEdit-SpinButtons-Enabled="false" >
                    <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" MaxValue="100000" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                    <FooterTemplate>
                        TTotal = 
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="labelSum" Text='<%# GetTotalSummaryValue() %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataSpinEditColumn>
            </Columns>
            <Settings ShowStatusBar="Hidden"/>
            <SettingsEditing Mode="Batch" />
            <Settings ShowFooter="true" />

            <Settings VerticalScrollableHeight="350" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            
            <TotalSummary>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="dmas_valor" Tag="TTotal" />
            </TotalSummary>
            <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" />
        </dx:ASPxGridView> 
        
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" AutoPostBack="False" Enabled="false">
            <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = true;}" />
        </dx:ASPxButton>
        <br /><br />
        <br /><br />                  
    </div> 
</asp:Content>
