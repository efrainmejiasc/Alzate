<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmRoles_form.aspx.cs" Inherits="MedeskiView.Forms.frmRoles_form" %>

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
                <li class="breadcrumb-item"><a href="frmRoles.aspx">Lista de Roles</a></li>
                <li class="breadcrumb-item active">Formulario de Roles</li>
            </ol>
        </div>

        <div id="SubTitulos" class="titulo2">
		    <h6>Formulario de Roles<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec"></dx:ASPxButton>                                
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtNombre" runat="server" ClientInstanceName="txtNombre" ToolTip="Nombre" NullText="Nombre" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripcion" NullText="Descripcion" CssClass="form-control" />
                </div>
            </div>
            
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbGrupos" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbNombreArea" ToolTip="Seleccionar Grupo" NullText="Seleccionar Grupo">
                    </dx:ASPxComboBox>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="lbxEstado" runat="server" DropDownStyle="DropDownList"
                             AutoPostBack="false" CssClass="form-control" ClientInstanceName="lbxEstado" ToolTip="Estado" NullText="Estado">
                            <Items>
                                <dx:ListEditItem Text="Activo" Value = 1 />
                                <dx:ListEditItem Text="Inactivo" Value = 0 />
                            </Items>
                        </dx:ASPxComboBox>
                </div>
            </div>
            
            <br/>

            <div id="" class="titulo2">
		        <h6>Permisos<br /></h6>
	        </div>

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

            <br/>
            <dx:ASPxHiddenField ID="txtRol" runat="server" ClientInstanceName="txtRol"></dx:ASPxHiddenField >
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="guardar boton-formulariosintprim" OnClick="GuardarClicked"></dx:ASPxButton>
            <br/><br/>
            <br/><br/>
        </div>

        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>

    </div>
</asp:Content>
