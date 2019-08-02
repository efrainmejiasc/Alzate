<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmOpcionesDeMenu.aspx.cs" Inherits="MedeskiView.Forms.frmOpcionesDeMenu" %>
<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagName="VentanaValidaciones" TagPrefix="uc1"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-terminal d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Administración<br /></h3>
			</div>
        </div>
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
			    <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbRol" ClientInstanceName="cmbRol" DropDownStyle="DropDownList" CssClass="form-control labelinterno" ToolTip="Seleccionar Rol" OnSelectedIndexChanged="cmbRol_Change" AutoPostBack="true" />
			    </div>			    
            </div>
            
            <br />
                <dx:ASPxButton ID="btnGuardar" runat="server" OnClick="GuardarClicked" Text="Guardar" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>
                <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
            <br/><br/> 
            <br/><br />
            
            <div>
                <dx:ASPxHiddenField ID="txtPaso" runat="server" ClientInstanceName="txtPaso"></dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
                <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            </div>
        
            <div class="grid-16 alpha">
                <dx:ASPxTreeView runat="server" ID="treeView" AutoGenerateColumns="False" Theme="MetropolisBlue" 
                    Width="100%"
                    CssClass="gridview"
                    ClientInstanceName="grid" 
                    EnableRowsCache="False" EnableCallBacks="False"
                    AllowSelectNode="true" AllowCheckNodes="true"
                    EnableAnimation="true" CheckNodesRecursive="true"
                    ShowExpandButtons="true" ShowTreeLines="true"
                    EnableHotTrack="true">                    
                </dx:ASPxTreeView>                                   
            </div>                
        </div>
    </div>
</asp:Content>
