<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionValorMAS_Detalle.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionValorMAS_Detalle" %>
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
                <li class="breadcrumb-item"><a href="frmDistribucionValorMAS.aspx">Valor Distribución MAS</a></li>
                <li class="breadcrumb-item active">Valor Distribución MAS - Detalle</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Valor Distribución MAS - Detalle<br /></h6>
	    </div>

        
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="regresar boton-formulariosintsec" UseSubmitBehavior="True"></dx:ASPxButton>
        <br /><br /><br />

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="prod_consecutivo"
            OnDataBinding="grid_DataBinding">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="prod_consecutivo" FieldName="prod_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Producto" FieldName="prod_codigo" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="casos" Caption="Cantidad" VisibleIndex="2" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Integer" PropertiesSpinEdit-DisplayFormatString="N0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" MaxValue="1" NumberType="Integer" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="porc" Caption="Porcentaje" VisibleIndex="3" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="P2" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="P2" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="software" Caption="MAS Software" VisibleIndex="4" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="infraestructura" Caption="MAS Infraestructura" VisibleIndex="5" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="datacenter" Caption="MAS Datacenter" VisibleIndex="6" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="gente" Caption="Gente Técnica Inf." VisibleIndex="7" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="ga" Caption="MAS GA" VisibleIndex="8" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="cdm" Caption="CDM" VisibleIndex="9" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="procesos" Caption="Procesos" VisibleIndex="10" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="total" Caption="Total" VisibleIndex="11" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>
</asp:Content>
