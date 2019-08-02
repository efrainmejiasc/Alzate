<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmAnoPresupuesto_form.aspx.cs" Inherits="MedeskiView.Forms.frmAnoPresupuesto_form" %>
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
                <li class="breadcrumb-item"><a href="frmAnoPresupuesto.aspx">Lista de Años Presupuesto</a></li>
                <li class="breadcrumb-item active">Formulario de Años Presupuesto</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Años Presupuesto<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
			    <div class="form-group col-md-4">
                    <dx:ASPxComboBox runat="server" ID="cmbAno" DropDownStyle="DropDownList" CssClass="form-control labelinterno" ToolTip="Seleccionar Año" />				    
			    </div>
			    <div class="form-group col-md-4">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
			    </div>
			    <div class="form-group col-md-4">
                    <dx:ASPxCheckBox runat="server" ID="chkDuplicar" CssClass="form-control"  ClientInstanceName="chkDuplicar" Text="Duplicar Parámetros?" EnableViewState="false" ToolTip="Duplicar Parámetros?" OnCheckedChanged="chkDuplicar_CheckedChanged">
                        <ClientSideEvents CheckedChanged="function(s, e) {e.processOnServer = true;}" />
                    </dx:ASPxCheckBox>
                </div>
            </div>

            <div class="form-row">        
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbCopiarAno" ClientInstanceName="cmbCopiarAno" NullText="Seleccionar Año y Versión" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Año y Versión" />
                    <p class="Note">
                        <b>Nota</b>: 
                        <br />Por seguridad, sólo se permite duplicar cuando se crea un nuevo registro.
                    </p>
                </div>                  
            </div>            
            
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>            
            <br/><br/> 
            <br/><br />
            
            <div>
                <dx:ASPxHiddenField ID="txtPaso" runat="server" ClientInstanceName="txtPaso"></dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
                <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            </div>
       
        </div>
    </div>
</asp:Content>

