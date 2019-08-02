<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmRoles.aspx.cs" Inherits="MedeskiView.Forms.frmRoles" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-terminal d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Administración<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Lista de Roles</li>
            </ol>
        </div>

        <div id="SubTitulos" class="titulo2">
		    <h6>Roles<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click" ></dx:ASPxButton>
        <br/><br/><br/> 

        <div>
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView ID="IdGrid" runat="server" Theme="MetropolisBlue"
            CssClass="gridview"
            EnableCallBacks="False"
            ClientInstanceName="IdGrid"
            KeyFieldName="rolm_consecutivo" Width="100%"
            AutoGenerateColumns="False" 
            EnableRowsCache="False"
            OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
            OnCustomButtonCallback="IdGrid_CustomButtonCallback">
            <SettingsBehavior AllowFocusedRow="True" />
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />

            <Columns>
                <dx:GridViewDataColumn FieldName="rolm_consecutivo" Caption="Clae" VisibleIndex="0" Visible="false" />
                <dx:GridViewDataColumn FieldName="GE_TPARAMETROS.parm_descripcion" Caption="Grupo" VisibleIndex="1" > <Settings AutoFilterCondition="Contains" /></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="rolm_nombre" Caption="Rol" VisibleIndex="2" ><Settings AutoFilterCondition="Contains" /></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="rolm_descripcion" Caption="Descripción" VisibleIndex="3" ><Settings AutoFilterCondition="Contains" /></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="rolm_estadoStr" VisibleIndex="6" Caption="Estado"  ><Settings AutoFilterCondition="BeginsWith"/></dx:GridViewDataColumn>
                <dx:GridViewCommandColumn VisibleIndex="7" Caption="Acción">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png"/>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <%--<SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="true"/>
                <Settings ShowFilterRow="True" />
                <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" Summary-Visible="false" PageSizeItemSettings-Caption="" PageSize="20">
                    <PageSizeItemSettings Items="5, 10, 20, 50, 100" />
                </SettingsPager>
                    <Styles>
                <Header HorizontalAlign="Center" BackColor="#2C5183" ForeColor="White" Font-Bold="False"></Header>
                <FocusedRow BackColor ="#d5eaff" ForeColor="Black"></FocusedRow>
            </Styles>
            <FormatConditions>
            </FormatConditions>--%>
        </dx:ASPxGridView>
    </div>
</asp:Content>
