<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmGente_form.aspx.cs" Inherits="MedeskiView.Forms.frmGente_form" %>

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
                <li class="breadcrumb-item"><a href="frmPersonas.aspx">Lista de Personas</a></li>
                <li class="breadcrumb-item active">Formulario de Costo de Personas por Periodo Activo</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Costo de Personas por Periodo Activo<br /></h6>
	    </div>

        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec"></dx:ASPxButton>            
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtNombre" runat="server" ClientInstanceName="txtNombre" ToolTip="Nombre" NullText="Nombre" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtApellido" runat="server" ClientInstanceName="txtApellido" ToolTip="Apellidos" NullText="Apellidos" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtCentroCostos" runat="server" ClientInstanceName="txtCentroCostos" ToolTip="Centro de Costos" NullText="Centro de Costos" ReadOnly="true" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtPorcentaje" runat="server" ClientInstanceName="txtPorcentaje" ToolTip="Porcentaje Dedicacion" NullText="Porcentaje Dedicacion" CssClass="form-control" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtCostoColaborador" runat="server" DisplayFormatString="C0" ClientInstanceName="txtCostoColaborador" ToolTip="Costo Colaborador" NullText="Costo Colaborador" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
            </div>

            <br />
            <dx:ASPxHiddenField ID="txtPeriodo" runat="server" ClientInstanceName="txtPeriodo"></dx:ASPxHiddenField>

            <dx:ASPxHiddenField ID="txtIdPersona" runat="server" ClientInstanceName="txtIdPersona"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="txtIdentificacion" runat="server" ClientInstanceName="txtIdentificacion"></dx:ASPxHiddenField>

            <dx:ASPxHiddenField ID="txtIdCcostos" runat="server" ClientInstanceName="txtIdCcostos"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="txtDescCcostos" runat="server" ClientInstanceName="txtDescCcostos"></dx:ASPxHiddenField>

            <dx:ASPxHiddenField ID="txtCargo" runat="server" ClientInstanceName="txtCargo"></dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="txtEmpresa" runat="server" ClientInstanceName="txtEmpresa"></dx:ASPxHiddenField>

            <dx:ASPxHiddenField ID="txtConsecutivo" runat="server" ClientInstanceName="txtConsecutivo"></dx:ASPxHiddenField>

            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>
            <br />
            <br />
            <br />
            <br />                  
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>
    </div>                                    
</asp:Content>
