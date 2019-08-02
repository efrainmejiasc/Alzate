<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionCuadroServicios.aspx.cs" Inherits="MedeskiView.Forms.frmCuadroServicios" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-pie-chart d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro de Servicios<br /></h3>
			</div>
        </div>
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox ID="cmbParametros" runat="server" DropDownStyle="DropDownList"
                        AutoPostBack="false" CssClass="form-control"  ClientInstanceName="cmbParametros" 
                        ToolTip="Seleccionar Fase" NullText="Seleccionar Fase" />                    
                </div>
            </div>
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="gvSalida" AutoGenerateColumns="True" Theme="MetropolisBlue" CssClass="gridview"
            ClientInstanceName="gvSalida" EnableRowsCache="False" EnableCallBacks="False" KeyFieldName="idProducto">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <Columns>
                <dx:GridViewDataColumn Caption="Producto" FieldName="prod_codigo" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Valor Directo" FieldName="vlr_directo" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Valor Infraestructura" FieldName="vlr_infraest" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Valor Intermedio" FieldName="vlr_interm" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Valor Gastos Area" FieldName="vlr_ga" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>               
    </div>
</asp:Content>
