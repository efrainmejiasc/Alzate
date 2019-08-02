<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionServidoresDataCenter.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionServidoresDataCenter" %>
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
                <li class="breadcrumb-item active">Valor Distribución Servidores - Productos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Valor Distribución Servidores - Productos<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbServidor" ClientInstanceName="cmbServidor" DropDownStyle="DropDownList"  CssClass="form-control" ToolTip="Seleccionar Servidor" OnSelectedIndexChanged="cmbServidor_SelectedIndexChanged" AutoPostBack="true" />
                </div>                
            </div>               
        </div>        
         
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
                ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="drel_item_datacenter"
                OnDataBinding="grid_DataBinding">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Item Datacenter" FieldName="prit_item" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn CellStyle-HorizontalAlign="Left" FieldName="valor" Caption="Porcentaje" VisibleIndex="2" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                        PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="P3" 
                        PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                        <PropertiesSpinEdit DisplayFormatString="P3" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                            <SpinButtons Enabled="False"></SpinButtons>
                        </PropertiesSpinEdit>
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataSpinEditColumn>
                </Columns>
            </dx:ASPxGridView>      

            <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
            <br /><br /><br />

        </div>
</asp:Content>
