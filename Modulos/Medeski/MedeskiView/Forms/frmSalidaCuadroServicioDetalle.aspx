<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmSalidaCuadroServicioDetalle.aspx.cs" Inherits="MedeskiView.Forms.frmSalidaCuadroServicioDetalle" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes <br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Salida Detalle Cuadro Servicio</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Salida Detalle Cuadro Servicio<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                </div>
            </div>
        </div>

        <div class="grid-16 alpha">
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="Consecutivo">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="idprod" FieldName="idprod" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Servicio" FieldName="Servicio" VisibleIndex="1">
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Producto" FieldName="Producto" VisibleIndex="2">
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Tipo" Caption="Tipo" VisibleIndex="3">                    
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SubTipo" Caption="SubTipo" VisibleIndex="4">
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ServPersona" Caption="ServPersona" VisibleIndex="5">
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Item" Caption="Item" VisibleIndex="6">
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Valor" Caption="Valor" VisibleIndex="7">
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataTextColumn>                
            </Columns>
            <Settings ShowFilterRow="true" ShowGroupPanel="True" ShowFooter="True" />
            <Settings VerticalScrollableHeight="400" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <GroupSummary>
                <%-- <dx:ASPxSummaryItem FieldName="dto_generic_cantidad" DisplayFormat="Cantidad: {0}" SummaryType="Sum" /> --%>
                <dx:ASPxSummaryItem FieldName="Valor" DisplayFormat="Total: {0:c0}" Tag="Total" SummaryType="Sum" />
            </GroupSummary>            
        </dx:ASPxGridView>
       
        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />

    </div>
</asp:Content>
