<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmItems_form.aspx.cs" Inherits="MedeskiView.Forms.frmItems_form" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-cog d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Configuración<br /></h3>
			</div>            
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item"><a href="frmProductos.aspx">Lista de Productos</a></li>
                <li class="breadcrumb-item"><a href="frmItems.aspx">Lista de Items</a></li>
                <li class="breadcrumb-item active">Formulario de Items</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Items<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec"></dx:ASPxButton>                                
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>
        <br /><br />    
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbProducto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtItem" runat="server" ClientInstanceName="txtItem" ToolTip="Item" NullText="Item" CssClass="form-control" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCuentaAux" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" />
                </div>
                
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbTipo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Tipo de Item" />
                </div>

            </div>            

            <div class="form-row">                
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
            </div>            

            <br />
            <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>
            <br /><br /><br />
        </div>    

        <div>                        
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
    </div>
</asp:Content>
