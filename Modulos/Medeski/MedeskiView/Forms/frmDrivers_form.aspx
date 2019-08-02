<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDrivers_form.aspx.cs" Inherits="MedeskiView.Forms.frmDrivers_form" %>

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
                <li class="breadcrumb-item"><a href="frmDrivers.aspx">Lista de Drivers</a></li>
                <li class="breadcrumb-item active">Formulario de Drivers</li>
            </ol>
        </div>
         
        <div id="SubTitulos" class="titulo2">
		    <h6>Drivers<br /></h6>
	    </div>
        
         <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec" />                     
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtNombre" runat="server" ClientInstanceName="txtNombre" ToolTip="Nombre" NullText="Nombre" CssClass="form-control" />
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripcion" NullText="Descripcion" CssClass="form-control" />
                </div>
            </div>  

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbTipoCobro" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Tipo de Cobro" />
                </div>                
                <div class="form-group col-md-3">
                    <dx:ASPxComboBox runat="server" ID="cmbAplicaSede" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Aplica Sede?"/>
                </div>
                <div class="form-group col-md-3">
                    <dx:ASPxComboBox runat="server" ID="cmbAplicaValor" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Aplica Valor?"/>
                </div>
            </div>  

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbAplicaProv" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Aplica proveedor?" />
                </div>
            </div>

            <br />
                <dx:ASPxHiddenField ID="txtConsecutivo" runat="server" ClientInstanceName="txtConsecutivo"></dx:ASPxHiddenField>
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked"  CssClass="guardar boton-formulariosintprim" ></dx:ASPxButton>                
            <br /><br />
            <br /><br />
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>
    </div>                                   
</asp:Content>
