<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDrivers.aspx.cs" Inherits="MedeskiView.Forms.frmDrivers" %>

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
                <li class="breadcrumb-item active">Lista de Drivers</li>
            </ol>
        </div>
         
        <div id="SubTitulos" class="titulo2">
		    <h6>Drivers<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="NuevoClicked" CssClass="nuevo boton-formulariosintsec" ></dx:ASPxButton>
        <br /><br /><br />
        
         <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" 
            EnableRowsCache="False" 
            KeyFieldName="driv_consecutivo"
            OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
            OnCustomButtonCallback="grid_CustomButtonCallback" 
            EnableCallBacks="False">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="Consecutivo" FieldName="driv_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Nombre" FieldName="driv_nombre" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Descripción" FieldName="driv_descripcion" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Tipo Cobro" FieldName="driv_tipo_cobro" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Aplica Sede?" FieldName="driv_aplica_sede" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Aplica Valor?" FieldName="driv_aplica_valor" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Aplica Proveedor?" FieldName="driv_aplica_proveedor" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Estado" FieldName="driv_activo" VisibleIndex="7" visible ="false"/>
                 <dx:GridViewDataColumn Caption="Estado" FieldName="driv_estadoStr" VisibleIndex="7" visible ="true"><Settings AutoFilterCondition="BeginsWith"/></dx:GridViewDataColumn>
                <dx:GridViewCommandColumn VisibleIndex="8" Caption="Acción">
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
