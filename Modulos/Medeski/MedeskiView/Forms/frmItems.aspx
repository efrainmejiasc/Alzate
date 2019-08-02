<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmItems.aspx.cs" Inherits="MedeskiView.Forms.frmItems" %>

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
                <li class="breadcrumb-item"><a href="frmProductos.aspx">Lista de Productos</a></li>
                <li class="breadcrumb-item active">Lista de Items</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Items<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="NuevoClicked" CssClass="nuevo boton-formulariosintsec"></dx:ASPxButton>     
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />

        <div>                        
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="gvItems" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            ClientInstanceName="gvItems" EnableRowsCache="False" KeyFieldName="prit_consecutivo"
            Width="100%"
            OnCustomButtonCallback="grid_CustomButtonCallback" EnableCallBacks="False">
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <Columns>
                <dx:GridViewDataColumn Caption="prit_consecutivo" FieldName="prit_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataColumn Caption="prit_producto" FieldName="GE_TPRODUCTOS.prod_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="prit_cuenta" FieldName="GE_TCUENTAS.cuen_consecutivo" VisibleIndex="2" Visible="false" />
                <dx:GridViewDataColumn Caption="prit_tipo" FieldName="prit_tipo" VisibleIndex="2" />                
                <dx:GridViewDataColumn Caption="Item" FieldName="prit_item" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Producto" FieldName="GE_TPRODUCTOS.prod_descripcion" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Cuenta" FieldName="GE_TCUENTAS.cuen_descripcion" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn VisibleIndex="6" Caption="Acción">
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
