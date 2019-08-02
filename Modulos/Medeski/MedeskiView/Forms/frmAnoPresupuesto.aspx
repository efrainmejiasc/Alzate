<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmAnoPresupuesto.aspx.cs" Inherits="MedeskiView.Forms.frmAnoPresupuesto" %>

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
                <li class="breadcrumb-item active">Lista de Años Presupuesto</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Año Presupuesto<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click" ></dx:ASPxButton>
        <br/><br/><br/> 

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
                KeyFieldName="peri_consecutivo"
                OnCustomButtonCallback="IdGrid_CustomButtonCallback"
                OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText">
                <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <SettingsBehavior AllowFocusedRow="True" />                    
                <Settings GridLines="Horizontal" />
                <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />

                <Columns>
                    <dx:GridViewDataColumn Caption="CONSECUTIVO" FieldName="peri_consecutivo" VisibleIndex="1" Visible="false" />
                    <dx:GridViewDataColumn Caption="Año" FieldName="peri_ano" VisibleIndex="2" />
                    <dx:GridViewDataColumn Caption="Versión" FieldName="peri_paso" VisibleIndex="3" />
                    <%-- <dx:GridViewDataColumn Caption="ETAPA" FieldName="peri_etapa" VisibleIndex="4" /> --%>
                    <dx:GridViewDataColumn Caption="Estado" FieldName="peri_activo" Visible="false" VisibleIndex="6"/>
                    <dx:GridViewDataColumn Caption="Estado" FieldName="peri_estadoStr" Visible="true" VisibleIndex="7" CellStyle-HorizontalAlign="Right"><Settings AutoFilterCondition="BeginsWith"/></dx:GridViewDataColumn>
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
