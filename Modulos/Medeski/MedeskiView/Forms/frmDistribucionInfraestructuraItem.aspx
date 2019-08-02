<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionInfraestructuraItem.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionInfraestructuraItem" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-pie-chart d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Valor Item x Servidor<br /></h3>
			</div>
        </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxGridView runat="server" ID="gvSalida" AutoGenerateColumns="True" Theme="MetropolisBlue" CssClass="gridview"
                        Width="100%"
                        ClientInstanceName="gvSalida" EnableRowsCache="False" EnableCallBacks="False" KeyFieldName="servidor;item">
                        <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                        <Settings GridLines="Horizontal" />
                    </dx:ASPxGridView>
                </div>
            </div>
        </div>
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <h1 class="titulo1"></h1>
        </div>    
    </div>    
</asp:Content>
