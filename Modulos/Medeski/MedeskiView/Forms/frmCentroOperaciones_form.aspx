<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCentroOperaciones_form.aspx.cs" Inherits="MedeskiView.Forms.frmCentroOperaciones_form" %>

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
                <li class="breadcrumb-item"><a href="frmCentroOperaciones.aspx">Lista de Centros de Operaciones</a></li>
                <li class="breadcrumb-item active">Formulario de Centros de Operaciones</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Centros de Operaciones<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtCodigo" ClientInstanceName="txtCodigo" ToolTip="Codigo" NullText="Codigo" CssClass="form-control"></dx:ASPxTextBox>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripcion" NullText="Descripcion" CssClass="form-control" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbVicepresidencia" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbVicepresidencia" ToolTip="Es Vicepresidencia?" NullText="Es Vicepresidencia?">
                        <Items>
                            <dx:ListEditItem Text="SI" Value = S />
                            <dx:ListEditItem Text="NO" Value = N />
                        </Items>
                    </dx:ASPxComboBox>
                </div>

                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbEstado" runat="server" DropDownStyle="DropDownList"
                            CssClass="form-control"
                            AutoPostBack="false" ClientInstanceName="cmbEstado" ToolTip="Estado" NullText="Estado">
                        <Items>
                            <dx:ListEditItem Text="Activo" Value = 1 />
                            <dx:ListEditItem Text="Inactivo" Value = 0 />
                        </Items>
                    </dx:ASPxComboBox>
                    <div>
                        <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
                    </div>
                </div>
                <div class="form-group col-md-6">
                </div>
            </div>

            <br />
            <dx:ASPxHiddenField ID="txtConsecutivo" runat="server" ClientInstanceName="txtConsecutivo"></dx:ASPxHiddenField>                
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="guardar boton-formulariosintprim" OnClick="GuardarClicked"></dx:ASPxButton>
            <br /><br /><br />

        </div>
            
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones1" />
        </div>
                    
    </div>
</asp:Content>
