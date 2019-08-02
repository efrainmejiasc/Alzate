<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmParametros.aspx.cs" Inherits="MedeskiView.Forms.frmParametros" %>

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
                <li class="breadcrumb-item active">Lista de Clases</li>
            </ol>
        </div>

        <div id="SubTitulos" class="titulo2">
		    <h6>Lista de Clases<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click" ></dx:ASPxButton>
        <br/><br/><br/> 

        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView ID="IdGrid" runat="server" Theme="MetropolisBlue"
            CssClass="gridview"
            EnableCallBacks="False"
            ClientInstanceName="IdGrid"
            KeyFieldName="clap_clase" Width="100%"
            AutoGenerateColumns="False" 
            EnableRowsCache="False"
            OnCustomColumnDisplayText="IdGrid_CustomColumnDisplayText" 
            OnCustomButtonCallback="IdGrid_CustomButtonCallback">
            <SettingsBehavior AllowFocusedRow="True" />
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />

            <Columns>
                <dx:GridViewDataColumn FieldName="clap_clase" Caption="Clae" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn FieldName="clap_nombre" Caption="Nombre" VisibleIndex="2" />
                <dx:GridViewDataColumn FieldName="clap_descripcion" Caption="Descripción" VisibleIndex="3" />
                <dx:GridViewDataColumn FieldName="clap_fechaini" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataColumn FieldName="clap_fechafin" VisibleIndex="5" Visible="false" />
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="clap_estadoStr" VisibleIndex="6" Caption="Estado"/>
                 <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="clap_estado" Visible="false"/>
                <dx:GridViewCommandColumn VisibleIndex="7" Caption="Acción">
                    <CustomButtons>

                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png"/>
                        </dx:GridViewCommandColumnCustomButton>
                        
                        <dx:GridViewCommandColumnCustomButton ID="btnDetalles" Text=" ">
                            <Image ToolTip="Parámetros" Url="../Content/Imagenes/consultar.png" />
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

        <asp:Button ID="btnConfirm" CssClass="redirect" runat="server" onclick="btnConfirm_Click"/>

    </div>
</asp:Content>
