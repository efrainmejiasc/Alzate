<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmGastosViaje_form.aspx.cs" Inherits="MedeskiView.Forms.frmGastosViaje_form" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-dollar d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Presupuesto<br /></h3>
			</div>           
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item"><a href="frmGastosViaje.aspx">Lista de Gastos Viaje</a></li>
                <li class="breadcrumb-item active">Formulario de Gastos Viaje</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Gastos Viaje<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="btnNuevo_Click" CssClass="limpiar boton-formulariosintsec"></dx:ASPxButton>                   
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCCosto"  ClientInstanceName="cmbCCosto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Centro Costo" /> 
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbTipoViaje"  ClientInstanceName="cmbTipoViaje" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Tipo Viaje" AutoPostBack="true" OnSelectedIndexChanged="cmbTipoViaje_SelectedIndexChanged" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbDestino"  ClientInstanceName="cmbDestino" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Destino" /> 
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMoneda" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Moneda" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMes" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Mes" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtSvalor" runat="server"
                        MaxLength="20" DisplayFormatString="N0" 
                        Theme="MetropolisBlue" ClientInstanceName="txtSvalor" 
                            MinValue="-9999999999999999999" MaxValue="99999999999999999999" CssClass="form-control" ToolTip ="Valor" NullText ="Valor"
                            AllowMouseWheel="False" HorizontalAlign="Right">
                        <SpinButtons Enabled="False">
                        </SpinButtons>
                    </dx:ASPxSpinEdit>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCantidad" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Cantidad Días" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxMemo ID="txtObservacion" Rows="5" MaxLength="1000" runat="server" ClientInstanceName="txtObservacion" ToolTip="Observación" NullText="Observación" CssClass="form-control">
                        <ClientSideEvents KeyPress="function(s, e) {
                        if(e.htmlEvent.keyCode == 13) {
                            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                        }}" />
                    </dx:ASPxMemo>
                </div>
            </div>  

            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxCheckBoxList ID="cbLPersonas" runat="server" CssClass="form-control delegados" ValueField="pers_consecutivo" TextField="pers_nombres" RepeatColumns="2" RepeatLayout="OrderedList" Caption="Personas viaje" RepeatDirection="Vertical" Theme ="MetropolisBlue">
                        <CaptionSettings Position="Top" />
                    </dx:ASPxCheckBoxList>
                </div>
            </div>
            
            <br />                             
                <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>                
            <br /><br />
            <br /><br />
        </div>

        <div>                        
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

    </div>
</asp:Content>
