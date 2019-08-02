<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmGente.aspx.cs" Inherits="MedeskiView.Forms.frmGente" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-cog d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Configuración<br /></h3>
			</div>            
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Lista de Personas por Periodo Activo</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Personas por Periodo Activo<br /></h6>
	    </div>
        
        <%--
            <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" OnClick="NuevoClicked" CssClass="nuevo boton-formulariosintsec"></dx:ASPxButton>
        --%>

        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                        
        <br /><br /><br />
        
        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            Width="100%"
            ClientInstanceName="grid" 
            EnableRowsCache="False" 
            KeyFieldName="gent_consecutivo"
            OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
            OnCustomButtonCallback="grid_CustomButtonCallback" 
            EnableCallBacks="False">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn Caption="ID_PERIODO" FieldName="gent_consecutivo" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataColumn Caption="ID_PERIODO" FieldName="GE_TPERIODOPRESUPUESTO.peri_consecutivo" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn Caption="ID_PERSONA" FieldName="GE_TPERSONAS.pers_consecutivo" VisibleIndex="2" Visible="false" />
                <dx:GridViewDataColumn Caption="DOC_PERSONA" FieldName="GE_TPERSONAS.pers_identificacion" VisibleIndex="3" Visible="false" />
                <dx:GridViewDataColumn Caption="ID_CCOSTOS" FieldName="GE_TCENTROSCOSTOS.cost_consecutivo" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataColumn Caption="DESC_CCOSTOS" FieldName="GE_TCENTROSCOSTOS.cost_descripcion" VisibleIndex="5" Visible="false" />
                <dx:GridViewDataColumn Caption="Cargo" FieldName="GE_TPERSONAS.GE_TPARAMETROS1.parm_descripcion" VisibleIndex="6" Visible="false" />
                                
                <dx:GridViewDataTextColumn Caption="Empresa" FieldName="GE_TPERSONAS.GE_TPARAMETROS.parm_descripcion" VisibleIndex="7"  HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>                                
                <dx:GridViewDataTextColumn Caption="Nombres" FieldName="GE_TPERSONAS.pers_nombre" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Apellidos" FieldName="GE_TPERSONAS.pers_apellido" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Periodo" FieldName="GE_TPERIODOPRESUPUESTO.peri_ano" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Centro de Costos" FieldName="GE_TCENTROSCOSTOS.cost_codigo" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" Caption="Costo Colaborador" FieldName="gent_costo_colaborador" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" Caption="Porcentaje Dedicación" FieldName="gent_porcentaje_manual_dedicacion" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" Caption="Estado" FieldName="gent_estado" VisibleIndex="14" />
                <dx:GridViewCommandColumn VisibleIndex="15" Caption="Acción">
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
