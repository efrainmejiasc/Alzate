<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCParametros_form.aspx.cs" Inherits="MedeskiView.Forms.frmCParametros_form" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono">
                <i class="la la-terminal d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i>
            </div>
            <div id="divtitulo">
                <h3>Administración<br /></h3>
            </div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i>Inicio</a></li>F4n4Inn0v41920
                <li class="breadcrumb-item"><a href="frmParametros.aspx">Lista de Clases</a></li>
                <li class="breadcrumb-item"><a href="frmCParametros.aspx">Lista de Parámetros</a></li>
                <li class="breadcrumb-item active">Formulario de Parámetros</li>
            </ol>
        </div>
        <div id="SubTitulos" class="titulo2">
            <h6>Formualrio de Parámetros<br /></h6>
        </div>
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click">
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnRegresar" runat="server" Text="Regresar" OnClick="RegresarClicked" CssClass="regresar boton-formulariosintsec">
        </dx:ASPxButton>
        <br />
        <br />
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtCodigo" runat="server" ClientInstanceName="txtCodigo" ToolTip="Identificación parametro" NullText="Identificación parametro" CssClass="form-control" />
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxTextBox ID="txtDescripcion" runat="server" ClientInstanceName="txtDescripcion" ToolTip="Descripción" NullText="Descripción" CssClass="form-control" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxDateEdit ID="dtFechaini" runat="server" CssClass="form-control" ClientInstanceName="clFechaini" ToolTip="Fecha Inicial" NullText="Fecha Inicial">
                        <ClearButton DisplayMode="Never">
                        </ClearButton>
                    </dx:ASPxDateEdit>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxDateEdit ID="dtFechafin" runat="server" EditFormat="Custom" CssClass="form-control" ClientInstanceName="clFechafin" ToolTip="Fecha Final" NullText="Fecha Final"  />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbClase" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbClase" ToolTip="Clase" NullText="Clase">
                    </dx:ASPxComboBox>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox ID="cmbEstado" runat="server" DropDownStyle="DropDownList"
                            AutoPostBack="false" CssClass="form-control" ClientInstanceName="cmbEstado" ToolTip="Estado" NullText="Estado">
                        <Items>
                            <dx:ListEditItem Text="Seleccionar Estado"/>
                            <dx:ListEditItem Text="Activo" Value = 1 />
                            <dx:ListEditItem Text="Inactivo" Value = 0 />
                        </Items>
                    </dx:ASPxComboBox>
                </div>
            </div>
            <br />
            <dx:ASPxHiddenField ID="txtParametro" runat="server" ClientInstanceName="txtParametro">
            </dx:ASPxHiddenField>
            <dx:ASPxButton ID="btnGuardar" runat="server" OnClick="GuardarClicked" Text="Guardar" CssClass="guardar boton-formulariosintprim">
            </dx:ASPxButton>
            <br />
            <br />
            <br />
            <br />
        </div>
        <div>
            <uc1:VentanaValidaciones runat="server" id="VentanaValidaciones" />
        </div>
    </div>
</asp:Content>
