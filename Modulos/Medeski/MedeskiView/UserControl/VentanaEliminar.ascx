<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VentanaEliminar.ascx.cs" Inherits="MedeskiView.UserControl.VentanaEliminar" %>
<div class="grid-4 alpha">
    <div class="grid-4">
        <dx:ASPxLabel ID="lblMensaje" runat="server" Text="lblMensaje"></dx:ASPxLabel>
    </div>
    <div class="grid-4">
        <dx:ASPxLabel ID="lblId" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
    </div>
    <div class="grid-4">
        <dx:ASPxLabel ID="lblDescripcion" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
    </div>
    <div class="grid-4">&nbsp;</div>
    <div class="grid-4">
        <div class="grid-2 alpha">
            <dx:ASPxButton ID="btnAceptar" runat="server" AutoPostBack="false" Text="Si" OnClick="okClick" CssClass="opcion-si">
            </dx:ASPxButton>
        </div>
        <div class="grid-2 omega">
            <dx:ASPxButton ID="btnCancelar" runat="server" Text="No" AutoPostBack="false" ClientInstanceName="btnCancelar" CssClass="opcion-no">
                <ClientSideEvents Click="function(s, e) { ventana.Hide(); }" />
            </dx:ASPxButton>
        </div>
    </div>
</div>