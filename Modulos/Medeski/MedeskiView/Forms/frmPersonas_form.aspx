<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmPersonas_form.aspx.cs" Inherits="MedeskiView.Forms.frmPersonas_form" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content id="Content1" contentplaceholderid="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-cog d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Configuración<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item"><a href="frmPersonas.aspx">Lista de Personas</a></li>
                <li class="breadcrumb-item active">Formulario de Personas</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Personas<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>            
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>
        <br /><br />
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtTipoDoc" ClientInstanceName="txtTipoDoc" ToolTip="Tipo de Documento" NullText="Tipo de Documento" CssClass="form-control"></dx:ASPxTextBox>
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDocumento" runat="server" ClientInstanceName="txtDocumento" ToolTip="Documento" NullText="Documento" CssClass="form-control" />
                </div>
            </div> 

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtNombres" runat="server" ClientInstanceName="txtNombres" ToolTip="Nombres" NullText="Nombres" CssClass="form-control" />
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtApellidos" runat="server" ClientInstanceName="txtApellidos" ToolTip="Apellidos" NullText="Apellidos" CssClass="form-control" />
                </div>
            </div> 
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbTipoContrato" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbTipoContrato" ToolTip="Seleccionar Tipo de Contrato" NullText="Seleccionar Tipo de Contrato">
                    </dx:ASPxComboBox>
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbCargo" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbCargo" ToolTip="Seleccionar Cargo" NullText="Seleccionar Cargo">
                    </dx:ASPxComboBox>
                </div>
            </div> 
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbGrupo" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbGrupo" ToolTip="Seleccionar Grupo" NullText="Seleccionar Grupo">
                    </dx:ASPxComboBox>
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbEmpresa" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbEmpresa" ToolTip="Seleccionar Empresa" NullText="Seleccionar Empresa">
                    </dx:ASPxComboBox>
                </div>
            </div> 
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbCentroCostos" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbCentroCostos" ToolTip="Seleccionar Centro de Costos" NullText="Seleccionar Centro de Costos">
                    </dx:ASPxComboBox>
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtUsuDom" ClientInstanceName="txtUsuDom" ToolTip="Usuario de Dominio" NullText="Usuario de Dominio" CssClass="form-control"></dx:ASPxTextBox>
                </div>
            </div> 
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbNombreArea" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbNombreArea" ToolTip="Seleccionar Area" NullText="Seleccionar Area">
                    </dx:ASPxComboBox>
                </div>                    
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtNombreBusqueda" ClientInstanceName="txtNombreBusqueda" ToolTip="Nombre Busqueda" NullText="Nombre Busqueda" CssClass="form-control" ></dx:ASPxTextBox>
                </div>
            </div> 
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbJefe" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="v" ToolTip="Seleccionar Jefe" NullText="Seleccionar Jefe">
                    </dx:ASPxComboBox>
                </div>                
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbEstado" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbEstado" ToolTip="Estado" NullText="Estado">
                        <Items>
                            <dx:ListEditItem Text="Activo" Value = 1 />
                            <dx:ListEditItem Text="Inactivo" Value = 0 />
                        </Items>
                    </dx:ASPxComboBox>
                </div>
            </div> 
            
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox ID="cmbTipoDistribucion" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbTipoDistribucion" ToolTip="Seleccionar Tipo de Distribucion" NullText="Seleccionar Tipo de Distribucion">
                    </dx:ASPxComboBox>
                </div>
            </div> 
            
            <br />      
            <dx:ASPxHiddenField ID="txtConsecutivo" runat="server" ClientInstanceName="txtConsecutivo"></dx:ASPxHiddenField>
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="guardar boton-formulariosintprim" OnClick="GuardarClicked"></dx:ASPxButton>
            <br /><br /><br />
        </div>     

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
                      
    </div>
</asp:Content>
