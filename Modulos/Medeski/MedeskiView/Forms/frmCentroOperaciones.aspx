<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCentroOperaciones.aspx.cs" Inherits="MedeskiView.Forms.frmCentroOperaciones" %>

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
                <li class="breadcrumb-item active">Lista de Centros de Operaciones</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Centros de Operaciones<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
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
                KeyFieldName="ceop_codigo"
                OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
                OnCustomButtonCallback="IdGrid_CustomButtonCallback">
                <SettingsBehavior AllowFocusedRow="True" />
                <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <Settings GridLines="Horizontal" />
                <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
                <Columns>
                    <dx:GridViewDataColumn Caption="Consecutivo" FieldName="ceop_consecutivo" VisibleIndex="1" Visible="false" />
                                
                    <dx:GridViewDataColumn Caption="Centro de Operación" FieldName="ceop_codigo" VisibleIndex="2" />
                    <dx:GridViewDataColumn Caption="Descripción" FieldName="ceop_descripcion" VisibleIndex="3" />
                    <dx:GridViewDataColumn Caption="Vicepresidencia" FieldName="ceop_vicepresidencia" VisibleIndex="4" />
                    <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="ceop_activo" VisibleIndex="5" Visible ="false" /> 
                     <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="ceop_estadoStr" VisibleIndex="5" ><Settings AutoFilterCondition="BeginsWith" /></dx:GridViewDataColumn> 
                    <dx:GridViewCommandColumn VisibleIndex="6" Caption="Acción">
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
