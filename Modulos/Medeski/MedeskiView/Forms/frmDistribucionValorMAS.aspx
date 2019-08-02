<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionValorMAS.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionValorMAS" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Valor Distribución MAS</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Valor Distribución MAS<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Detalle" OnClick="btnNuevo_Click" CssClass="nuevo boton-formulariosintsec" UseSubmitBehavior="True"></dx:ASPxButton>
        <br /><br /><br />

        <dx:ASPxGridView runat="server" ID="grdencab" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grdencab" EnableRowsCache="False" KeyFieldName="Item">
            <Settings GridLines="Horizontal" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="Item" FieldName="Item" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="Total" Caption="Valor" VisibleIndex="2" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
            </Columns>
        </dx:ASPxGridView>

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />
                         
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />   
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>
    </div>
</asp:Content>
