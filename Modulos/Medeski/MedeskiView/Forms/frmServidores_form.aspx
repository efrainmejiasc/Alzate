<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmServidores_form.aspx.cs" Inherits="MedeskiView.Forms.frmServidores_form" %>

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
                <li class="breadcrumb-item"><a href="frmServidores.aspx">Lista de Servidores</a></li>
                <li class="breadcrumb-item active">Formulario de Servidores</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Servidores<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="NuevoClicked" CssClass="limpiar boton-formulariosintsec"></dx:ASPxButton >
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br /><br />    
        
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtServidor" runat="server" ClientInstanceName="txtServidor" ToolTip="Servidor" NullText="Servidor" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtMarca" runat="server" ClientInstanceName="txtMarca" ToolTip="Marca" NullText="Marca" CssClass="form-control" />
                </div>
            </div>

             <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtModelo" runat="server" ClientInstanceName="txtModelo" ToolTip="Modelo" NullText="Modelo" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtFuncion" runat="server" ClientInstanceName="txtFuncion" ToolTip="Función" NullText="Funcion" CssClass="form-control" />
                </div>
            </div>
             
             <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtEstado" runat="server" ClientInstanceName="txtEstado" ToolTip="Estado" NullText="Estado" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDireccionIP" runat="server" ClientInstanceName="txtDireccionIP" ToolTip="Dirección IP" NullText="Dirección IP" CssClass="form-control" />
                </div>
            </div>
             
             <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtSistemaOperativo" runat="server" ClientInstanceName="txtSistemaOperativo" ToolTip="Sistema Operativo" NullText="Sistema Operativo" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtNumeroBits" runat="server"
                            MaxLength="20" DisplayFormatString="N0"
                            Theme="MetropolisBlue" ClientInstanceName="txtNumeroBits"
                            MinValue="0" MaxValue="99" CssClass="form-control" ToolTip="Número de Bits" NullText="Número de Bits"
                            AllowMouseWheel="False" HorizontalAlign="Right">
                            <SpinButtons Enabled="False">
                            </SpinButtons>
                            <ClientSideEvents KeyPress="function(s, e) {
                            if(e.htmlEvent.keyCode == 13) {
                                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                            }}" />
                        </dx:ASPxSpinEdit>
                </div>
            </div>
             
             <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtMemoria" runat="server"
                        MaxLength="20" DisplayFormatString="N2"
                        Theme="MetropolisBlue" ClientInstanceName="txtMemoria"
                        MinValue="0" MaxValue="99999999999999999999" CssClass="form-control" ToolTip="Memoria" NullText="Memoria"
                        AllowMouseWheel="False" HorizontalAlign="Right">
                        <SpinButtons Enabled="False">
                        </SpinButtons>
                        <ClientSideEvents KeyPress="function(s, e) {
                        if(e.htmlEvent.keyCode == 13) {
                            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                        }}" />
                    </dx:ASPxSpinEdit>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtProcesadores" runat="server" ClientInstanceName="txtProcesadores" ToolTip="Descripción de Procesadores" NullText="Descripción de Procesadores" CssClass="form-control" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtCore" runat="server"
                        MaxLength="20" DisplayFormatString="N0"
                        Theme="MetropolisBlue" ClientInstanceName="txtCore"
                        MinValue="0" MaxValue="99999999999999999999" CssClass="form-control" ToolTip="Número de Core" NullText="Número de Core"
                        AllowMouseWheel="False" HorizontalAlign="Right">
                        <SpinButtons Enabled="False">
                        </SpinButtons>
                        <ClientSideEvents KeyPress="function(s, e) {
                        if(e.htmlEvent.keyCode == 13) {
                            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                        }}" />
                    </dx:ASPxSpinEdit>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtDiscoDuro" runat="server"
                        MaxLength="20" DisplayFormatString="N0"
                        Theme="MetropolisBlue" ClientInstanceName="txtDiscoDuro"
                        MinValue="0" MaxValue="99999999999999999999" CssClass="form-control" ToolTip="Tamaño GB total Disco Duro" NullText="Tamaño GB total Disco Duro"
                        AllowMouseWheel="False" HorizontalAlign="Right">
                        <SpinButtons Enabled="False">
                        </SpinButtons>
                        <ClientSideEvents KeyPress="function(s, e) {
                        if(e.htmlEvent.keyCode == 13) {
                            ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                        }}" />
                    </dx:ASPxSpinEdit>
                </div>
            </div>
              
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcionDD" runat="server" ClientInstanceName="txtDescripcionDD" ToolTip="Descripción de los Discos Duros" NullText="Descripción de los Discos Duros" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtAplicaciones" runat="server" ClientInstanceName="txtAplicaciones" ToolTip="Aplicaciones Instaladas" NullText="Aplicaciones Instaladas" CssClass="form-control" />
                </div>
            </div>
             
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbVirtualizado" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Servidor Virtualizado?" OnValueChanged="cmbVirtualizado_ValueChanged" AutoPostBack="true"/>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtSoftwareVirtualizacion" runat="server" ClientInstanceName="txtSoftwareVirtualizacion" ToolTip="Software de Virtualización" NullText="Software de Virtualización" CssClass="form-control" />
                </div>
            </div>
             
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtGranja" runat="server" ClientInstanceName="txtGranja" ToolTip="Granja" NullText="Granja" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtUbicacionFisica" runat="server" ClientInstanceName="txtUbicacionFisica" ToolTip="Ubicación Fisica" NullText="Ubicación Fisica" CssClass="form-control" />
                </div>
            </div>
             
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtActivoFijo" runat="server" ClientInstanceName="txtActivoFijo" ToolTip="Activo fijo" NullText="Activo Fijo" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
            </div>
             
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbDepreciable" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Depreciable" />
                </div>                
            </div>

            <br />
            <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>                        
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>            
            <br /><br /><br />
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>     
    </div>
</asp:Content>
