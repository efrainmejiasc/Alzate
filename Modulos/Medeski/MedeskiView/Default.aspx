<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="MedeskiView._Default" %>



<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <link rel="stylesheet" href="../assets/css/graficos.css">
        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
        <script type="text/javascript" src="../assets/js/graficos.js"></script>

    
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-dashboard d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-right:5px;"></i></div>
            <div id="divtitulo">
                <h3>Dashboard</h3>
            </div>
        </div>

        <dx:ASPxButton ID="btnSync" runat="server" Text="Sincronizar" CssClass="limpiar boton-formulariosintsec" OnClick="btnSync_Click"></dx:ASPxButton>
        
        <div id="ContenedorFormulario">
            <div class="row">
                <div class="col-lg-6">
                    <div class="col" id="donna_div"></div>
                </div>
                <div class="col-lg-6">
                    <div class="col" id="bars_div"></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="col card_div">
                        <a href="#"><i class="la la-pie-chart" style="font-size:28px;padding-top:4px;"></i> Reporte 1</a>
                    </div>
                    <div class="col card_div">
                        <a href="#"><i class="la la-pie-chart" style="font-size:28px;padding-top:4px;"></i> Reporte 2</a>                    
                    </div>                    
                </div>
                <div class="col-lg-6">
                    <div class="col card_div">                    
                        <a href="#"><i class="la la-pie-chart" style="font-size:28px;padding-top:4px;"></i> Reporte 3</a>
                    </div>
                    <div class="col card_div">                    
                        <a href="#"><i class="la la-pie-chart" style="font-size:28px;padding-top:4px;"></i> Reporte 4</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>