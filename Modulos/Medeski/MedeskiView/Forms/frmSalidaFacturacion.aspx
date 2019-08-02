<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmSalidaFacturacion.aspx.cs" Inherits="MedeskiView.Forms.frmSalidaFacturacion" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-pie-chart d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Salida Facturación<br /></h3>
			</div>
        </div>

         <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                </div>
            </div>
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView ID="gvSalidaFacturacion" runat="server" AutoGenerateColumns="False" CssClass="gridview"
            Width="100%"
            ClientInstanceName="gvSalidaFacturacion" EnableRowsCache="False" KeyFieldName="dto_generic_productos">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataColumn Caption="Producto" FieldName="dto_generic_productos" VisibleIndex="0" Visible="true" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Empresa" FieldName="dto_generic_ccostos" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Centro Op." FieldName="dto_generic_valor" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="dto_generic_usuario_carga" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Valor de Prd. Directo" FieldName="dto_generic_observaciones" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Valor de Prd. Distribuido" FieldName="dto_generic_observaciones" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Cant. de Prd. Directo" FieldName="dto_generic_observaciones" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Cant. de Prd. Distribuido" FieldName="dto_generic_observaciones" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Facturación" FieldName="dto_generic_observaciones" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
            </Columns>
        </dx:ASPxGridView>                    
    </div>
</asp:Content>