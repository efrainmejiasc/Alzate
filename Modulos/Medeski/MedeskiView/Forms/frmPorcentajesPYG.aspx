<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmPorcentajesPYG.aspx.cs" Inherits="MedeskiView.Forms.frmPorcentajesPYG" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagName="VentanaValidaciones" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Porcentajes de PyG</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Porcentajes de PyG<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnCalcular" runat="server" Text="Recalcular" CssClass="limpiar boton-formulariosintsec" OnClick="CalcularClicked">
            <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
        </dx:ASPxButton>
        <br /><br /><br />
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>                    
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" 
            Width="100%"
            CssClass="gridview"
            ClientInstanceName="grid" 
            EnableRowsCache="False"
            EnableCallBacks="False" 
            KeyFieldName="hipo_consecutivo">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="CONSECUTIVO" FieldName="hipo_consecutivo" VisibleIndex="1" Visible="false" />
                
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Right" Caption="CEOP" FieldName="GE_THISTORICOPYG.GE_TCENTROSOPERACION.ceop_codigo" VisibleIndex="1" />
                
                <%-- FORECAST --%>
                <dx:GridViewDataTextColumn Caption="Gastos Totales Forecast" FieldName="hipo_gastos_fore_totales" VisibleIndex="2">
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% de Ventas Totales Forecast" FieldName="hipo_porc_fore_ventas" VisibleIndex="3">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% Directos Forecast" FieldName="hipo_porc_fore_directos" VisibleIndex="4">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% Indirectos Forecast" FieldName="hipo_porc_fore_indirectos" VisibleIndex="5">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% Total Forecast" FieldName="hipo_porc_fore_total" VisibleIndex="6">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>

                <%-- PPTO --%>
                <dx:GridViewDataTextColumn Caption="Gastos Totales" FieldName="hipo_gastos_totales" VisibleIndex="7">
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% de Ventas Totales" FieldName="hipo_porc_ventas" VisibleIndex="8">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% Directos" FieldName="hipo_porc_directos" VisibleIndex="9">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% Indirectos" FieldName="hipo_porc_indirectos" VisibleIndex="10">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="% Total" FieldName="hipo_porc_total" VisibleIndex="11">
                    <PropertiesTextEdit DisplayFormatString="P2" />
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>  

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />  
                  
    </div>
</asp:Content>
