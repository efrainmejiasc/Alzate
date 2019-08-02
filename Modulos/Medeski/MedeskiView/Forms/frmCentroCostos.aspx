<%@ Page Language="C#"  MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCentroCostos.aspx.cs" Inherits="MedeskiView.Forms.frmCentroCostos" %>

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
                <li class="breadcrumb-item active">Centros de Costos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Centros de Costos<br /></h6>
	    </div>

        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br /><br />
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
                    
        <div class="grid-16 alpha">
            <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" 
                Width="100%"
                CssClass="gridview"
                ClientInstanceName="grid" 
                EnableRowsCache="False"
                EnableCallBacks="False" 
                KeyFieldName="cost_codigo"
                OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
                OnCustomButtonCallback="IdGrid_CustomButtonCallback">
                <SettingsBehavior AllowFocusedRow="True" />
                <Settings GridLines="Horizontal" />
                <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
                <Columns>
                    <dx:GridViewDataColumn Caption="Consecutivo" FieldName="cost_consecutivo" VisibleIndex="1" Visible="false" />
                                
                    <dx:GridViewDataColumn Caption="Empresa" FieldName="GE_TCOMPANIAS.comp_nombre" VisibleIndex="2" />
                    <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="cost_codigo" VisibleIndex="2" />
                    <dx:GridViewDataColumn Caption="Descripción" FieldName="cost_descripcion" VisibleIndex="3" />
                    <dx:GridViewDataColumn Caption="Centro de Operación" FieldName="cost_centro_operacion" VisibleIndex="4" />
                    <dx:GridViewDataColumn Caption="Responsable" FieldName="cost_responsable" VisibleIndex="5" />
                    <dx:GridViewDataColumn Caption="Tipo de Cliente" FieldName="GE_TPARAMETROS.parm_descripcion" VisibleIndex="6" />
                    <dx:GridViewDataColumn Caption="Tipo de Distribución" FieldName="GE_TPARAMETROS2.parm_descripcion" VisibleIndex="6" />
                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="cost_activo" VisibleIndex="7" />
                    <dx:GridViewCommandColumn VisibleIndex="8" Caption="Acción">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                                <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png"/>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>                    
        </div>
     </div>      
</asp:Content>