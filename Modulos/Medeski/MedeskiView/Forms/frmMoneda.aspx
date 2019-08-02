<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmMoneda.aspx.cs" Inherits="MedeskiView.Forms.frmMoneda" %>
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
                <li class="breadcrumb-item active">Cambio de Moneda por Año</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Cambio de Moneda por Año<br /></h6>
	    </div>
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
			    <div class="form-group col-md-4">
			    </div>
            </div>
        </div>

        <div class="grid-16 alpha">
            <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" 
                Width="100%"
                CssClass="gridview"
                ClientInstanceName="grid" 
                EnableRowsCache="False"
                EnableCallBacks="False" 
                KeyFieldName="parm_consecutivo"
                OnCustomButtonCallback="IdGrid_CustomButtonCallback"
                OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText">
                <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <SettingsBehavior AllowFocusedRow="True" />                    
                <Settings GridLines="Horizontal" />
                <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
                <Columns>
                    <dx:GridViewDataColumn Caption="CONSECUTIVO" FieldName="parm_consecutivo" VisibleIndex="1" Visible="false" />
                    <dx:GridViewDataColumn Caption="Moneda" FieldName="parm_descripcion" VisibleIndex="2" />
                    <dx:GridViewDataColumn Caption="Código" FieldName="parm_codigo" VisibleIndex="3" />
                    <%-- <dx:GridViewDataColumn Caption="ETAPA" FieldName="peri_etapa" VisibleIndex="4" /> --%>
                    <dx:GridViewDataColumn Caption="Estado" FieldName="parm_estado" visible="false"/>
                    <dx:GridViewDataColumn Caption="Estado" FieldName="parm_estadoStr" visible="true" VisibleIndex="7"><Settings AutoFilterCondition="BeginsWith" /></dx:GridViewDataColumn> 
                    <dx:GridViewCommandColumn VisibleIndex="8" Caption="Acción">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                                <Image ToolTip="Valores" Url="../Content/Imagenes/consultar.png"/>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>                    
        </div>                
    </div>
</asp:Content>
