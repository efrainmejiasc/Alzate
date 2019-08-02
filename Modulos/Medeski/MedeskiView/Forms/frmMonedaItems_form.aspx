<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmMonedaItems_form.aspx.cs" Inherits="MedeskiView.Forms.frmMonedaItems_form" %>
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
                <li class="breadcrumb-item"><a href="frmMoneda.aspx">Cambio de Moneda por Año</a></li>
                <li class="breadcrumb-item"><a href="frmMonedaItems.aspx">Valor de Moneda por Meses</a></li>
                <li class="breadcrumb-item active">Formulario de Valor de Moneda por Meses</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Valor de Moneda por Meses<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
			    <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMeses" DropDownStyle="DropDownList" CssClass="form-control labelinterno" ToolTip="Seleccionar Mes" />
			    </div>
			    
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtValor" CssClass="form-control" ClientInstanceName="txtValor" ToolTip="Valor" NullText="Valor" DisplayFormatString="C0" AutoCompleteType="Disabled" />
			    </div>
			</div>

            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
			    </div>
            </div>

            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>            
            <br/><br/> 
            <br/><br />
            
            <div>
                <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="txtMoneda" runat="server" ClientInstanceName="txtMoneda"></dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="txtAno" runat="server" ClientInstanceName="txtAno"></dx:ASPxHiddenField>
                <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            </div>
       
        </div>
    </div>
</asp:Content>

