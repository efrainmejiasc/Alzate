<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCompanias.aspx.cs" Inherits="MedeskiView.Forms.frmCompanias" %>
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
                <li class="breadcrumb-item active">Lista de Compa&ntilde;ias</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Compa&ntilde;ias<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="NuevoClicked" ></dx:ASPxButton>
        <br/><br/><br/> 

        <div class="grid-16 alpha">
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>
        
        
        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" 
            EnableRowsCache="False" 
            KeyFieldName="comp_consecutivo"
            OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
            OnCustomButtonCallback="grid_CustomButtonCallback" 
            EnableCallBacks="False">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="Consecutivo" FieldName="comp_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Nombre" FieldName="comp_nombre" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Descripción" FieldName="comp_descripcion" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" Caption="Utliza Centro de Costos" FieldName="comp_usa_co" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="comp_activo" Visible="false" VisibleIndex="4" />
                  <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="comp_estadoStr" VisibleIndex="4" ><Settings AutoFilterCondition="BeginsWith"/></dx:GridViewDataColumn>
                <dx:GridViewCommandColumn VisibleIndex="5" Caption="Acción" Name="accion">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificiar" Url="../Content/Imagenes/modificar.png" />
                        </dx:GridViewCommandColumnCustomButton>
                       
                        <dx:GridViewCommandColumnCustomButton ID="btnDetalles" Text=" ">
                            <Image ToolTip="Centros de Costo" Url="../Content/Imagenes/consultar.png" />
                        </dx:GridViewCommandColumnCustomButton>

                    </CustomButtons>
                </dx:GridViewCommandColumn>                               
            </Columns>
        </dx:ASPxGridView>
            
        <asp:Button ID="btnConfirm" CssClass="redirect" runat="server" onclick="btnConfirm_Click"/>
        
    </div>
</asp:Content>
