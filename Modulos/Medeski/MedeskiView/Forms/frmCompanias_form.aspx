<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCompanias_form.aspx.cs" Inherits="MedeskiView.Forms.frmCompanias_form" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-cog d-inline-block jjustify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Configuración<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item"><a href="frmCompanias.aspx">Lista de Compa&ntilde;ias</a></li>
                <li class="breadcrumb-item active">Formulario de Compa&ntilde;ias</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Compa&ntilde;ias<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec" ></dx:ASPxButton>                
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />   
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtNombre" runat="server" ClientInstanceName="txtNombre" ToolTip="Nombre" NullText="Nombre" CssClass="form-control"/>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripcion" NullText="Descripcion" CssClass="form-control"/>
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbUsaCO" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Utiliza Centro de Costos?" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
            </div>
            
            <br />            
                <dx:ASPxHiddenField ID="txtConsecutivo" runat="server" ClientInstanceName="txtConsecutivo"></dx:ASPxHiddenField>
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" ></dx:ASPxButton>                
            <br /><br /><br />

        </div>
                
        <div class="grid-16 alpha">
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>   
              
    </div>
</asp:Content>
