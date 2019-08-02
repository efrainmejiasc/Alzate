<%@ Page Language="C#"  MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCentroCostos_form.aspx.cs" Inherits="MedeskiView.Forms.frmCentroCostos_form" %>

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
                <li class="breadcrumb-item"><a href="frmCompanias.aspx">Lista de Compa&ntilde;ias</a></li>
                <li class="breadcrumb-item"><a href="frmCentroCostos.aspx">Lista de Centros de Costos</a></li>
                <li class="breadcrumb-item active">Formulario de Centros de Costos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Centros de Costos<br /></h6>
	    </div>
        
                
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />
                
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
			    <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtCcosto" ClientInstanceName="txtCcosto" ToolTip="Codigo" NullText="Codigo" CssClass="form-control" ></dx:ASPxTextBox>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripcion" NullText="Descripcion" CssClass="form-control"  />
                </div>
            </div>

            <div class="form-row">
			    <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbCentroOperaciones" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbCentroOperaciones" ToolTip="Seleccionar Centro de Operaciones" NullText="Seleccionar Centro de Operaciones">                        
                    </dx:ASPxComboBox>
                    <div>
                        <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
                    </div>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtResponsable" ClientInstanceName="txtResponsable" ToolTip="Responsable" NullText="Responsable" CssClass="form-control"></dx:ASPxTextBox>
                </div>
            </div>
            
            <div class="form-row">
			    <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbTipoCliente" runat="server" DropDownStyle="DropDownList"
                                AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbTipoCliente" ToolTip="Seleccionar Tipo de Cliente" NullText="Seleccionar Tipo de Cliente">                        
                    </dx:ASPxComboBox>
                    <div>
                        <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones3" />
                    </div>
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
			    <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbTipoDistri" runat="server" DropDownStyle="DropDownList"
                                AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbTipoDistri" 
                                ToolTip="Seleccionar Tipo de Distribucion de Facturacion" NullText="Seleccionar Tipo de Distribucion de Facturacion">                        
                        </dx:ASPxComboBox>
                        <div>
                            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones2" />
                        </div>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbCompania" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbCompania" 
                            ToolTip="Seleccionar Compañia" NullText="Seleccionar Compañia">                        
                    </dx:ASPxComboBox>                        
                </div>
            </div>

            <br />
                <dx:ASPxHiddenField ID="txtConsecutivo" runat="server" ClientInstanceName="txtConsecutivo"></dx:ASPxHiddenField>                        
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="guardar boton-formulariosintprim" OnClick="GuardarClicked"></dx:ASPxButton>
            <br /><br />
            <br /><br />

        </div>
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones1" />
        </div>
     </div>      
</asp:Content>