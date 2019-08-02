<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmProductosSinPpto.aspx.cs" Inherits="MedeskiView.Forms.frmProductosSinPpto" %>
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
                <li class="breadcrumb-item active">Productos No Presupuestados</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Productos No Presupuestados<br /></h6>
	    </div>
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                </div>
            </div>
        </div>

        <div>
            <uc1:ventanavalidaciones runat="server" id="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>

        <dx:ASPxGridView runat="server" ID="grid"
            Width="100%"
            AutoGenerateColumns="False" 
            Theme="MetropolisBlue" 
            CssClass="gridview"
            ClientInstanceName="gvPrItems" 
            EnableRowsCache="False" 
            KeyFieldName="prod_codigo"
            EnableCallBacks="False">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="Producto" FieldName="prod_codigo" VisibleIndex="1"/>                
                <dx:GridViewDataColumn Caption="Item" FieldName="prit_item" VisibleIndex="1"/>                                
            </Columns>
        </dx:ASPxGridView> 

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />  

    </div>
</asp:Content>
