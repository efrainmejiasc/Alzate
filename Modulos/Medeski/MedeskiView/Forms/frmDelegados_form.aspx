<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDelegados_form.aspx.cs" Inherits="MedeskiView.Forms.frmDelegados_form" %>

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
                <li class="breadcrumb-item"><a href="frmDelegados.aspx">Lista de Delegados</a></li>
                <li class="breadcrumb-item active">Formulario de Delegados</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Delegados<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox ID="cmbParametros" runat="server" DropDownStyle="DropDownList"
                        AutoPostBack="true" CssClass="form-control"  ClientInstanceName="cmbParametros" 
                        ToolTip="Seleccionar Fase" NullText="Seleccionar Fase" 
                        OnSelectedIndexChanged="cmbParametros_SelectedIndexChanged" />                    
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxCheckBoxList ID="cbLPersonas" CssClass="form-control delegados" runat="server" ValueField="pers_consecutivo" TextField="pers_nombres" RepeatColumns="2" RepeatLayout="OrderedList" Caption="" RepeatDirection="Vertical" Theme ="MetropolisBlue">
                        <CaptionSettings Position="Top" />
                    </dx:ASPxCheckBoxList>
                </div>
            </div>

            <br />
            <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>             
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" UseSubmitBehavior="True"></dx:ASPxButton>
            <br /><br /><br />

        </div>

        <div>
            <uc1:ventanavalidaciones runat="server" id="VentanaValidaciones" />
        </div>               
    </div>
</asp:Content>
