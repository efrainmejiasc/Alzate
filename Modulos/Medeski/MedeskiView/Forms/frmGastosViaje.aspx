<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmGastosViaje.aspx.cs" Inherits="MedeskiView.Forms.frmGastosViaje" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-dollar d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Presupuesto<br /></h3>
			</div>           
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Lista de Gastos Viaje</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Lista de Gastos Viaje<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="nuevo boton-formulariosintsec"></dx:ASPxButton>                   
        <br/><br/><br/> 

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
                <dx:GridViewDataColumn Caption="petr_amortizar" FieldName="petr_amortizar" VisibleIndex="9" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_meses_amortizar" FieldName="petr_meses_amortizar" VisibleIndex="10" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_trm" FieldName="petr_trm" VisibleIndex="10" Visible="false" />
                <dx:GridViewDataColumn Caption="petr_tipo_viaje" FieldName="petr_tipo_viaje" VisibleIndex="10" Visible="false" />
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="GE_TCENTROSCOSTOS.cost_descripcion" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center">
                        <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Producto" FieldName="GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_descripcion" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Item" FieldName="GE_TPRODUCTOSITEMS.prit_item" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Moneda" FieldName="GE_TPARAMETROS.parm_codigo" VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Valor" FieldName="petr_valor" VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" >
                <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn VisibleIndex="16" Caption="Acción">
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
