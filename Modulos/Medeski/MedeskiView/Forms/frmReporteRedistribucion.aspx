<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmReporteRedistribucion.aspx.cs" Inherits="MedeskiView.Forms.frmReporteRedistribucion" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagName="VentanaValidaciones" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Reporte Redistribución - Fase 2</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Reporte Redistribución - Fase 2<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                </div>
            </div>
        </div>
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>
                    
        <dx:ASPxGridView ID="grid_Driver" ClientInstanceName="grid_Driver" runat="server"
            AutoGenerateColumns="False" Theme="MetropolisBlue"
            OnCustomCallback="grid_CustomCallback"
            CssClass="gridview"
            OnCustomUnboundColumnData="grid_CustomUnboundColumnData"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="ceop_codigo" Width="100%">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="ceop_codigo" Caption="CeOp. Intermedio" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left"  FieldName="cost_centro_operacion" Caption="Centro de Operaciones " VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left"  FieldName="ceop_descripcion" Caption="Descripción CO" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="Expr1" Caption="Valor" VisibleIndex="8">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>                            
            </Columns>
            <Settings ShowGroupPanel="True" ShowFooter="true" />
                <GroupSummary>
                <dx:ASPxSummaryItem FieldName="Expr1" DisplayFormat="Total: {0:c0}" SummaryType="Sum" />
            </GroupSummary>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Expr1" DisplayFormat="Total: {0:c0}" SummaryType="Sum" />
            </TotalSummary>
        </dx:ASPxGridView>  

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />  
                  
    </div>
</asp:Content>
