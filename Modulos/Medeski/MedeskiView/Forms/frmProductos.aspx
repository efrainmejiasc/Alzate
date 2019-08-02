<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="MedeskiView.Forms.frmProductos" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>
<%@ Register Src="~/UserControl/VentanaEliminar.ascx" TagPrefix="uc1" TagName="VentanaEliminar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-cog d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Configuración<br /></h3>
			</div>            
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Lista de Productos</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Productos<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Nuevo" OnClick="NuevoClicked" CssClass="nuevo boton-formulariosintsec"></dx:ASPxButton>
        <br /><br /><br />
        
        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView runat="server" ID="gvProductos" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
            ClientInstanceName="gvProductos" EnableRowsCache="False" KeyFieldName="prod_consecutivo"
            Width="100%"
            OnCustomButtonCallback="grid_CustomButtonCallback" EnableCallBacks="False">
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />            
            <Settings GridLines="Horizontal" />
            
            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="500" />
            <SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="true"></SettingsAdaptivity>
            <SettingsBehavior AllowEllipsisInText="true"/>
            <EditFormLayoutProperties>
                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="1500" />
            </EditFormLayoutProperties>
            <SettingsEditing Mode="EditForm"></SettingsEditing>
            <Styles>
                <Cell Wrap="False" />
            </Styles>

            <Columns>
                <dx:GridViewDataColumn Caption="prod_consecutivo" FieldName="prod_consecutivo" VisibleIndex="0" Visible="false"/>
                <dx:GridViewDataColumn Caption="prod_intermedio" FieldName="prod_intermedio" VisibleIndex="1" Visible="false"/>
                <dx:GridViewDataColumn Caption="prod_contrato" FieldName="prod_contrato" VisibleIndex="2" Visible="false"/>
                <dx:GridViewDataColumn Caption="prod_tipo_licencia" FieldName="prod_tipo_licencia" VisibleIndex="3" Visible="false" />
                <dx:GridViewDataColumn Caption="prod_criterio" FieldName="prod_criterio" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataColumn Caption="prod_componente" FieldName="prod_componente" VisibleIndex="5" Visible="false"/>
                <dx:GridViewDataColumn Caption="prod_activo" FieldName="prod_activo" VisibleIndex="6" Visible="false"/>
                <dx:GridViewDataColumn Caption="prod_responsable" FieldName="prod_responsable" VisibleIndex="7" Visible="false" />
                <dx:GridViewDataTextColumn Caption="Cod. Producto" FieldName="prod_codigo" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center"  >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Descripción" FieldName="prod_descripcion" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center"   >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Responsable" FieldName="GE_TPERSONAS.pers_nombres" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center"  >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Componente" FieldName="GE_TPARAMETROS.parm_descripcion" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center"  >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tipo de Producto" FieldName="GE_TPARAMETROS2.parm_descripcion" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center"  >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn VisibleIndex="12" Caption="Acciones">
                    <CustomButtons>
                        
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png" />
                        </dx:GridViewCommandColumnCustomButton>

                        <dx:GridViewCommandColumnCustomButton ID="btnDetalles" Text=" ">
                            <Image ToolTip="Items" Url="../Content/Imagenes/consultar.png" />
                        </dx:GridViewCommandColumnCustomButton>

                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn Caption="prod_redistribuir" FieldName="prod_redistribuir" VisibleIndex="13" Visible="false" />
            </Columns>                    
        </dx:ASPxGridView>

        <asp:Button ID="btnConfirm" CssClass="redirect" runat="server" onclick="btnConfirm_Click"/>

    </div>
</asp:Content>
