<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDelegados.aspx.cs" Inherits="MedeskiView.Forms.frmDelegados" %>

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
                <li class="breadcrumb-item active">Lista de Delegados</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Delegados<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Asignar" OnClick="btnNuevo_Click" CssClass="nuevo boton-formulariosintsec" UseSubmitBehavior="True"></dx:ASPxButton>
        <br /><br /><br />

        <div>
            <uc1:ventanavalidaciones runat="server" id="VentanaValidaciones" />            
        </div>  

        <dx:ASPxGridView runat="server" ID="gvPrItems"
            Width="100%"
            AutoGenerateColumns="False" 
            Theme="MetropolisBlue" 
            CssClass="gridview"
            ClientInstanceName="gvPrItems" 
            EnableRowsCache="False" 
            KeyFieldName="dele_consecutivo"
            OnCustomButtonCallback="grid_CustomButtonCallback" 
            EnableCallBacks="False">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="dele_consecutivo" FieldName="dele_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="Fase" FieldName="GE_TPARAMETROS.parm_descripcion" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Delegado" FieldName="GE_TPERSONAS1.pers_nombres" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn VisibleIndex="16" Caption="Acción">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnQuitar" Text=" ">
                            <Image ToolTip="Quitar" Url="../Content/Imagenes/no.png" />
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>                
    </div>
</asp:Content>
