<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCParametros.aspx.cs" Inherits="MedeskiView.Forms.frmCParametros" %>

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
                <li class="breadcrumb-item"><a href="frmParametros.aspx">Lista de Clases</a></li>
                <li class="breadcrumb-item active">Lista de Parámetros</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Parámetros<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="nuevo boton-formulariosintsec" OnClick="btnNuevo_Click" ></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br/><br/><br/> 

        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>

        <dx:ASPxGridView ID="gvParametros" runat="server" Theme="MetropolisBlue"
            CssClass="gvParametros gridview"
            EnableCallBacks="False"            
            ClientInstanceName="gvParametros"
            KeyFieldName="clap_clase" Width="100%"
            AutoGenerateColumns="False"
            OnCustomColumnDisplayText ="gvParametros_CustomColumnDisplayText" 
            OnCustomButtonCallback ="gvParametros_CustomButtonCallback"
            EnableRowsCache="False">
            <Settings GridLines="Horizontal" />

            <SettingsBehavior AllowFocusedRow="True" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn FieldName="parm_consecutivo" Caption="ID" VisibleIndex="1" Visible="false" />
                <dx:GridViewDataColumn FieldName="GE_TCLASESPARAMETROS.clap_descripcion" Caption="Clase" VisibleIndex="2" Visible="true" />
                <dx:GridViewDataColumn FieldName="parm_descripcion" Caption="Descripción" VisibleIndex="3" Visible="true" />
                <dx:GridViewDataColumn FieldName="parm_fechadesde" VisibleIndex="4" Visible="false" />
                <dx:GridViewDataColumn FieldName="parm_fechahasta" VisibleIndex="5" Visible="false" />
                <dx:GridViewDataColumn FieldName="parm_codigo" Caption="Parámetro" VisibleIndex="6" Visible="true" />
                <dx:GridViewDataColumn CellStyle-HorizontalAlign="Left" FieldName="parm_estado" Caption="Estado" VisibleIndex="7" Visible="true" />                
                <dx:GridViewDataColumn FieldName="clap_clase" VisibleIndex="8" Visible="false" />
                <dx:GridViewCommandColumn VisibleIndex="7" Caption="Acción">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                            <Image ToolTip="Modificar" Url="../Content/Imagenes/modificar.png"/>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>    
</asp:Content>
