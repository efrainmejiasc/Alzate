<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmServidores.aspx.cs" Inherits="MedeskiView.Forms.frmServidores" %>

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
                <li class="breadcrumb-item active">Lista de Servidores</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Servidores<br /></h6>
	    </div>

        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="NuevoClicked" CssClass="nuevo boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
     
        <dx:ASPxGridView runat="server" ID="gvServidores" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            ClientInstanceName="gvServidores" EnableRowsCache="False" KeyFieldName="serv_consecutivo"
            Width="100%"
            OnCustomButtonCallback="grid_CustomButtonCallback" EnableCallBacks="False">
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <Columns>
                <dx:GridViewDataColumn Caption="serv_consecutivo" FieldName="serv_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Servidor" FieldName="serv_nombre" VisibleIndex="1" Visible="true" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn  Caption="Cantidad Core" FieldName="serv_core" VisibleIndex="2" Visible="true" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn  Caption="Memoria" FieldName="serv_memoria" VisibleIndex="3" Visible="true" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn  Caption="Disco Duro" FieldName="serv_disco_duro" VisibleIndex="4" Visible="true" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn VisibleIndex="5" Caption="Acción" >
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png" />
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>                    
    </div>
</asp:Content>
