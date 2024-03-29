﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VentanaValidaciones.ascx.cs" Inherits="MedeskiView.UserControl.VentanaValidaciones" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<script type="text/javascript">
    function popup_Shown(s, e) {
        timeOut = 6000;
        timer.SetEnabled(true);
    }

    function popup_Closing(s, e) {
        timer.SetEnabled(false);
    }

    function popup_MouseOver() {
        timer.SetEnabled(false);
    }

    function popup_MouseOut() {
        timeOut = 6000;
        timer.SetEnabled(true);
    }

    function timer_Tick(s, e) {
        timeOut -= s.GetInterval();
        if (timeOut <= 0) {
            ventanaValid.Hide();
        }
    }
</script>
<div class="grid-6 alpha">
    <dx:ASPxPopupControl ID="ventanaValid" runat="server" ClientInstanceName="ventanaValid"
        PopupHorizontalAlign="WindowCenter" Width="400" PopupVerticalAlign="Above">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <div class="grid-6 alpha">
                    <dx:ASPxLabel ID="lblMensaje" runat="server" Text="lblMensaje"></dx:ASPxLabel>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents Shown="popup_Shown" Closing="popup_Closing" />
    </dx:ASPxPopupControl>
    <dx:ASPxTimer ID="tmHidePopup" runat="server" Interval="30" ClientInstanceName="timer" Enabled="False">
        <ClientSideEvents Tick="timer_Tick" />
    </dx:ASPxTimer>
</div>