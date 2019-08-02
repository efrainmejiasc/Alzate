<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCostoGenteTecnica.aspx.cs" Inherits="MedeskiView.Forms.frmCostoGenteTecnica" %>

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
                <li class="breadcrumb-item active">Costo Personal Técnico</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Costo Personal Técnico<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbTecnicos" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Tecnico" OnSelectedIndexChanged="cmbTecnicos_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox runat="server" ID="txtCostoColaborador" ClientInstanceName="txtCostoColaborador" ToolTip="Costo Colaborador" NullText="Costo Colaborador" CssClass="form-control" ReadOnly="true" DisplayFormatString="C0">                        
                    </dx:ASPxTextBox>                    
                </div>
            </div>
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />            
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>
        
        <div class="grid-16 alpha">
            <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" 
                CssClass="gridview"
                Width="100%"
                ClientInstanceName="grid" EnableRowsCache="False" 
                EnableCallBacks="False"
                KeyFieldName="NOMBRE_SERVIDOR;NOMBRE_PRODUCTO">
                <Settings GridLines="Horizontal" />
                <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Servidor" FieldName="NOMBRE_SERVIDOR" VisibleIndex="0" Visible="true" HeaderStyle-HorizontalAlign="Center"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="producto" FieldName="NOMBRE_PRODUCTO" VisibleIndex="1" Visible="true" HeaderStyle-HorizontalAlign="Center"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tipo Distribución" FieldName="TIPO_DISTRIBUCION" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Porcentaje Dedicación" FieldName="PORCENTAJE_DEDICACION" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="p"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Costo Distribución" FieldName="COSTO_DISTIBUCION" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" PropertiesTextEdit-DisplayFormatString="c"></dx:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="true" />
                <SettingsPager EnableAdaptivity="true" />
                <Styles Header-Wrap="True" />
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="PORCENTAJE_DEDICACION" SummaryType="Sum"/>
                </TotalSummary>
            </dx:ASPxGridView>

            <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
            <br /><br /><br />

        </div>  
    </div>
</asp:Content>
