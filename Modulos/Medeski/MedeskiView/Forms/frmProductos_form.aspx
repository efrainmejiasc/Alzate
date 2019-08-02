<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmProductos_form.aspx.cs" Inherits="MedeskiView.Forms.frmProductos_form" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<%@ Register Src="~/UserControl/VentanaEliminar.ascx" TagPrefix="uc1" TagName="VentanaEliminar" %>

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
                <li class="breadcrumb-item active">Formulario de Productos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Productos<br /></h6>
	    </div>

        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtCodigo" runat="server" ClientInstanceName="txtCodigo" ToolTip="Codigo" NullText="Codigo" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripcion" NullText="Descripcion" CssClass="form-control" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbResponsable" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Responsable" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbComponente" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Componente" OnSelectedIndexChanged="cmbComponente_SelectedIndexChanged" AutoPostBack="true"/>
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCriterio" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Criterio" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbTipoLicencia" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Tipo Licencia" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbContrato" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Contrato" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbReDistribucion" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Se Redistribuye?" />
                </div>                
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbServicio" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Servicio" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbTipoProd" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Tipo de Producto" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxCheckBox runat="server" ID="chkDistribucionDTC" ClientInstanceName="chkDistribucionDTC" CssClass="form-control" Text="Se Distribuye en DataCenter?" EnableViewState="false" ToolTip="Se Distribuye en DataCenter?" Enabled="false"/>
                </div>                
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbDriver1" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Driver Principal" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbDriver2" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Driver Opcional" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbIntermedio" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Intermedio" OnSelectedIndexChanged="cmbIntermedio_SelectedIndexChanged" AutoPostBack="true"/>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxCheckBox runat="server" ID="chkIntermedioNoDistribuible" ClientInstanceName="chkIntermedioNoDistribuible" CssClass="form-control" Text="Intermedio Distribuible" EnableViewState="false" ToolTip="Intermedio Distribuible" Enabled="false"/>
                </div>
            </div>

            <br />
                <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>                
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>                
            <br /><br /><br />
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />            
        </div>                   
    </div>
</asp:Content>
