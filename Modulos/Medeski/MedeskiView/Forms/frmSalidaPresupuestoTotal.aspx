<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmSalidaPresupuestoTotal.aspx.cs" Inherits="MedeskiView.Forms.frmSalidaPresupuestoTotal" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Salida Presupuesto</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Salida Presupuesto<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                </div>
            </div>
        </div>
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>

        <dx:ASPxGridView runat="server" ID="gvSalida" AutoGenerateColumns="True" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="gvSalida" EnableRowsCache="False" EnableCallBacks="False" KeyFieldName="sali_consecutivo">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <Columns>
                <dx:GridViewDataColumn Caption="Producto" FieldName="producto" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Item" FieldName="item" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Cuenta" FieldName="cuenta" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="ceco" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Año Ppto." FieldName="ano" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" Visible="false">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Mes Ppto" FieldName="descrip_mes" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Moneda" FieldName="moneda" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" Visible="false">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Valor" FieldName="valor" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Responsable" FieldName="nombre" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Tipo Mvto." FieldName="sali_tipo" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
            </Columns>
            <Settings ShowFooter="True" ShowGroupPanel="true" />
            <GroupSummary>
                <%-- <dx:ASPxSummaryItem FieldName="dto_generic_cantidad" DisplayFormat="Cantidad: {0}" SummaryType="Sum" /> --%>
                <dx:ASPxSummaryItem FieldName="valor" DisplayFormat="Total: {0:c0}" Tag="Total" SummaryType="Sum" />
            </GroupSummary>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="valor" SummaryType="Sum" Tag="Total"/>
                <%-- <dx:ASPxSummaryItem FieldName="valor" SummaryType="Count" Tag="No Registros"/> --%>
            </TotalSummary>
        </dx:ASPxGridView>

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />  

    </div>
</asp:Content>

