<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmSalidaCuadroServicio.aspx.cs" Inherits="MedeskiView.Forms.frmSalidaCuadroServicio" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-stack-overflow d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Reportes <br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Salida Cuadro Servicio</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Salida Cuadro Servicio<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                </div>
            </div>
        </div>

        <div class="grid-16 alpha">
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />          
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dx:ASPxGridViewExporter>
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="idprod;item">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataTextColumn Caption="idprod" FieldName="idprod" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Producto" FieldName="producto" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Item" FieldName="item" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataSpinEditColumn FieldName="total" Caption="Total" VisibleIndex="3" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_lic" Caption="Licenciamiento" VisibleIndex="4" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_int_lic" Caption="Intermedios Lic." VisibleIndex="5" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_infr" Caption="Infraestructura" VisibleIndex="6" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_datac" Caption="Datacenter" VisibleIndex="7" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_ga" Caption="MAS GA" VisibleIndex="8" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_softw" Caption="MAS Software" VisibleIndex="9" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_infr" Caption="MAS Infraestr." VisibleIndex="10" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_datac" Caption="MAS Datacenter" VisibleIndex="11" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_gente" Caption="MAS Gente Técnica Inf." VisibleIndex="12" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_ga_operaciones" Caption="GA Operaciones" VisibleIndex="13" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_ga_gtecnica" Caption="GA GTécnica" VisibleIndex="14" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_ga_desarrollo" Caption="GA Desarrollo" VisibleIndex="15" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_cdm" Caption="CDM" VisibleIndex="16" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn FieldName="vlr_mas_procesos" Caption="Procesos" VisibleIndex="17" PropertiesSpinEdit-AllowMouseWheel="false" PropertiesSpinEdit-AllowNull="true" 
                    PropertiesSpinEdit-MaxLength="3" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float" PropertiesSpinEdit-DisplayFormatString="C0" 
                    PropertiesSpinEdit-MaxValue="1" PropertiesSpinEdit-SpinButtons-Enabled="false">
                    <PropertiesSpinEdit DisplayFormatString="C0" NumberFormat="Custom" MaxValue="1" NumberType="Float" AllowMouseWheel="False" MinValue="0">
                        <SpinButtons Enabled="False"></SpinButtons>
                    </PropertiesSpinEdit>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataSpinEditColumn>
            </Columns>
            <Settings ShowFilterRow="true" ShowGroupPanel="True" ShowFooter="True" />
            <Settings VerticalScrollableHeight="400" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <GroupSummary>
                <%-- <dx:ASPxSummaryItem FieldName="dto_generic_cantidad" DisplayFormat="Cantidad: {0}" SummaryType="Sum" /> --%>
                <dx:ASPxSummaryItem FieldName="total" DisplayFormat="Total: {0:c0}" Tag="Total" SummaryType="Sum" />
            </GroupSummary>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="total" SummaryType="Sum" Tag="Total"/>
                <dx:ASPxSummaryItem FieldName="total" SummaryType="Count" Tag="No Registros"/>
            </TotalSummary>
        </dx:ASPxGridView>

        <dx:ASPxButton ID="btnDescargar" runat="server" Text="CSV" OnClick="btnDescargar_Click" CssClass="download boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />

    </div>
</asp:Content>
