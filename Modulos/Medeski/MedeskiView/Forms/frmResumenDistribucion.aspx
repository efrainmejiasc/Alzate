<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmResumenDistribucion.aspx.cs" Inherits="MedeskiView.Forms.frmResumenDistribucion" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagName="VentanaValidaciones" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Resumen Distribución - Fase 1</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Resumen Distribución - Fase 1<br /></h6>
	    </div>

         <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <asp:PlaceHolder ID="Note" runat="server" Visible="false">
                        <p class="Note">
                            <b>Nota</b>: 
                            <br />Para visualizar información en esta pantalla, debe <a href="frmFacturacionCargueDrivers.aspx"><strong>Cargar Base de Datos de Cobro</strong></a> para el actual periodo activo.
                        </p>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>
                    
        <dx:ASPxGridView ID="grid_Driver" ClientInstanceName="grid_Driver" runat="server"
            AutoGenerateColumns="False" Theme="MetropolisBlue"
            CssClass="gridview"
            OnCustomCallback="grid_CustomCallback"
            OnCustomUnboundColumnData="grid_CustomUnboundColumnData"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="dto_generic_empresa" Width="100%">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataColumn FieldName="dto_generic_productos" Caption="Productos" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_empresa" Caption="Empresa" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_sede" Caption="Sede" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_centro_operacion" Caption="C.O" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_coperaciones" Caption="Descripción C.O" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_descripcion_a" Caption="Tipo C.C." VisibleIndex="5">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_ccostos" Caption="C.C" VisibleIndex="6">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_cantidad" Caption="Cantidad" VisibleIndex="7" Visible="false">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor" Caption="Valor" VisibleIndex="8">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>

                <%-- 
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor_distribuidos" Caption="Valor Distrib" VisibleIndex="9">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor_adicional" Caption="Valor Adicional" VisibleIndex="10">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                --%>

                <dx:GridViewDataTextColumn FieldName="dto_generic_valor_total" Caption="Valor Total" VisibleIndex="11">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowGroupPanel="True" ShowFooter="true" />
            <GroupSummary>
                <%-- <dx:ASPxSummaryItem FieldName="dto_generic_cantidad" DisplayFormat="Cantidad: {0}" SummaryType="Sum" /> --%>
                <dx:ASPxSummaryItem FieldName="dto_generic_valor_total" DisplayFormat="Total: {0:c0}" SummaryType="Sum" />
            </GroupSummary>
            <TotalSummary>
                <%-- <dx:ASPxSummaryItem FieldName="dto_generic_cantidad" DisplayFormat="Cantidad: {0}" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="dto_generic_valor" DisplayFormat="Total Valor: {0:c0}" SummaryType="Sum" />                            
                <dx:ASPxSummaryItem FieldName="dto_generic_valor_distribuidos" DisplayFormat="Total Distr.: {0:c0}" SummaryType="Sum" />                            
                <dx:ASPxSummaryItem FieldName="dto_generic_valor_adicional" DisplayFormat="Total Adicional: {0:c0}" SummaryType="Sum" />
                --%>
                <dx:ASPxSummaryItem FieldName="dto_generic_valor_total" DisplayFormat="Total: {0:c0}" SummaryType="Sum" />                            
                    
            </TotalSummary>
        </dx:ASPxGridView>      

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />  
              
    </div>
</asp:Content>
