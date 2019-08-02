<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmOtrosGastosLaborales.aspx.cs" Inherits="MedeskiView.Forms.frmOtrosGastosLaborales" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-pie-chart d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Otros Gastos Laborales<br /></h3>
			</div>
        </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCCosto"  ClientInstanceName="cmbCCosto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Centro Costo" OnSelectedIndexChanged="cmbCCosto_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbProducto" ClientInstanceName="cmbProducto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbItem"  ClientInstanceName="cmbItem" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Item" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMoneda" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Moneda" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbMes" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Mes" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxSpinEdit ID="txtSvalor" runat="server"
                            MaxLength="20" Width="100%" DisplayFormatString="N0" 
                            Theme="MetropolisBlue" ClientInstanceName="txtSvalor" 
                             MinValue="-9999999999999999999" MaxValue="99999999999999999999" CssClass="form-control" ToolTip ="Valor" NullText ="Valor"
                             AllowMouseWheel="False" HorizontalAlign="Right">
                            <SpinButtons Enabled="False">
                            </SpinButtons>
                        </dx:ASPxSpinEdit>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCantidad" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Cantidad" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbActivo" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Activo" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxTextBox ID="txtObservacion" runat="server" ClientInstanceName="txtObservacion" ToolTip="Observación" NullText="Observación" CssClass="form-control" />
                </div>                
            </div>

            <br />
                <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>                
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>
                <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" OnClick="NuevoClicked" CssClass="boton-formulariosintsec"></dx:ASPxButton>            
            <br /><br />
            <br /><br />
        </div>
        
        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
                    
        <dx:ASPxGridView runat="server" ID="gvPrItems" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="gvPrItems" EnableRowsCache="False" KeyFieldName="petr_consecutivo"
            OnCustomButtonCallback="grid_CustomButtonCallback" EnableCallBacks="False">
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataColumn Caption="petr_consecutivo" FieldName="petr_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_ccosto" FieldName="GE_TCENTROSCOSTOS.cost_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_producto" FieldName="GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo" VisibleIndex="2" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_item" FieldName="GE_TPRODUCTOSITEMS.prit_consecutivo" VisibleIndex="3" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_mes" FieldName="petr_mes" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_activo" FieldName="petr_activo" VisibleIndex="5" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_moneda" FieldName="petr_moneda" VisibleIndex="6" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_cantidad" FieldName="petr_cantidad" VisibleIndex="7" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_observacion" FieldName="petr_observacion" VisibleIndex="8" Visible="false" />
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="GE_TCENTROSCOSTOS.cost_descripcion" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Producto" FieldName="GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_descripcion" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Item" FieldName="GE_TPRODUCTOSITEMS.prit_item" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Moneda" FieldName="GE_TPARAMETROS.parm_codigo" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Valor" FieldName="petr_valor" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="N0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn VisibleIndex="14" Caption="Acción">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png" />
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <%-- <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="true"/>
            <Settings ShowFilterRow="True" />
            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" Summary-Visible="false" PageSizeItemSettings-Caption="" PageSize="20">
                <PageSizeItemSettings Items="5, 10, 20, 50, 100" />
            </SettingsPager>
                <Styles>
            <Header HorizontalAlign="Center" BackColor="#2C5183" ForeColor="White" Font-Bold="False"></Header>
            <FocusedRow BackColor ="#d5eaff" ForeColor="Black"></FocusedRow>
        </Styles>--%>
        </dx:ASPxGridView>
    </div>
</asp:Content>
