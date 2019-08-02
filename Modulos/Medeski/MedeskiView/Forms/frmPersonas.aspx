<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmPersonas.aspx.cs" Inherits="MedeskiView.Forms.frmPersonas" %>

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
                <li class="breadcrumb-item active">Lista de Personas</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Personas<br /></h6>
	    </div>

        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <br /><br /><br />
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
                    
        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" 
            Width="100%"
            CssClass="gridview"
            ClientInstanceName="grid" 
            EnableRowsCache="False"
            EnableCallBacks="False" 
            KeyFieldName="pers_consecutivo"
            OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
            OnCustomButtonCallback="IdGrid_CustomButtonCallback">
            <SettingsBehavior AllowFocusedRow="True" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="Cosecutivo" FieldName="pers_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="Nombre" FieldName="pers_nombre" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="Apellido" FieldName="pers_apellido" VisibleIndex="1" Visible="false" />

                <dx:GridViewDataColumn Caption="Tipo de Documento" FieldName="pers_tipodoc" VisibleIndex="2" Visible="false"/>
                <dx:GridViewDataColumn Caption="Documento" FieldName="pers_identificacion" VisibleIndex="3" />
                <dx:GridViewDataColumn Caption="Nombres" FieldName="pers_nombres" VisibleIndex="4" />
                <dx:GridViewDataColumn Caption="Jefe" FieldName="GE_TPERSONAS2.pers_nombres" VisibleIndex="5" />
                <dx:GridViewDataColumn Caption="Tipo Contrato" FieldName="GE_TPARAMETROS.parm_descripcion" VisibleIndex="6" />
                <dx:GridViewDataColumn Caption="Método de Distribución" FieldName="GE_TPARAMETROS1.parm_descripcion" VisibleIndex="7" Visible="false"/>
                <dx:GridViewDataColumn Caption="Cargo" FieldName="GE_TPARAMETROS2.parm_descripcion" VisibleIndex="8" Visible="false"/>
                <dx:GridViewDataColumn Caption="Grupo" FieldName="GE_TPARAMETROS3.parm_descripcion" VisibleIndex="9" Visible="false"/>
                <dx:GridViewDataColumn Caption="Empresa" FieldName="GE_TPARAMETROS4.parm_descripcion" VisibleIndex="10" />
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="GE_TCENTROSCOSTOS2.cost_codigo" VisibleIndex="11" />
                <dx:GridViewDataColumn Caption="Usuario de Dominio" FieldName="pers_usudom" VisibleIndex="12" />
                <dx:GridViewDataColumn Caption="Area" FieldName="GE_TPARAMETROS5.parm_descripcion" VisibleIndex="13" />
                <dx:GridViewDataColumn Caption="Nombre Búsqueda" FieldName="pers_nombre_busq" VisibleIndex="14" />
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="pers_activo" Visible ="false"  />
                 <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="pers_estadoStr" VisibleIndex="15" ><Settings AutoFilterCondition="BeginsWith" /></dx:GridViewDataColumn> 
                <dx:GridViewCommandColumn VisibleIndex="16" Caption="Acción">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png"/>
                        </dx:GridViewCommandColumnCustomButton>
                        
                        <dx:GridViewCommandColumnCustomButton ID="btnDetalles" Text=" ">
                            <Image ToolTip="Costo por Periodo" Url="../Content/Imagenes/consultar.png" />
                        </dx:GridViewCommandColumnCustomButton>

                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>               
        
        <asp:Button ID="btnConfirm" CssClass="redirect" runat="server" onclick="btnConfirm_Click"/>
             
    </div>
</asp:Content>
