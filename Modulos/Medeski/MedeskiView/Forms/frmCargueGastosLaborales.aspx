﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCargueGastosLaborales.aspx.cs" Inherits="MedeskiView.Forms.frmCargueGastosLaborales"  %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var fileNumber = 0;
        var fileName = "";
        var startDate = null;

        function OnFileUploadComplete(s, e) {
            gvPpto.PerformCallback();
        }

        function UploadControl_OnFileUploadStart() {
            startDate = new Date();
            ClearProgressInfo();
            pcProgress.Show();
        }
        function UploadControl_OnFilesUploadComplete(e) {
            pcProgress.Hide();
            if (e.errorText)
                ShowMessage(e.errorText);
            else if (e.callbackData == "success")
                ShowMessage("El archivo se cargó satisfactoriamente.");
        }
        function ShowMessage(message) {
            window.setTimeout(function () { window.alert(message); }, 0);
        }
        // Progress Dialog
        function UploadControl_OnUploadingProgressChanged(args) {
            if (!pcProgress.IsVisible())
                return;
            if (args.currentFileName != fileName) {
                fileName = args.currentFileName;
                fileNumber++;
            }
            SetCurrentFileUploadingProgress(args.currentFileName, args.currentFileUploadedContentLength, args.currentFileContentLength);
            progress1.SetPosition(args.currentFileProgress);
            SetTotalUploadingProgress(fileNumber, args.fileCount, args.uploadedContentLength, args.totalContentLength);
            progress2.SetPosition(args.progress);
            UpdateProgressStatus(args.uploadedContentLength, args.totalContentLength);
        }
        function SetCurrentFileUploadingProgress(fileName, uploadedLength, fileLength) {
            lblFileName.SetText("Current File Progress: " + fileName);
            lblFileName.GetMainElement().title = fileName;
            lblCurrentUploadedFileLength.SetText(GetContentLengthString(uploadedLength) + " / " + GetContentLengthString(fileLength));
        }
        function SetTotalUploadingProgress(number, count, uploadedLength, totalLength) {
            lblUploadedFiles.SetText("Total Progress: " + number + ' of ' + count + " file(s)");
            lblUploadedFileLength.SetText(GetContentLengthString(uploadedLength) + " / " + GetContentLengthString(totalLength));
        }
        function ClearProgressInfo() {
            SetCurrentFileUploadingProgress("", 0, 0);
            progress1.SetPosition(0);
            SetTotalUploadingProgress(0, 0, 0, 0);
            progress2.SetPosition(0);
            lblProgressStatus.SetText('Elapsed time: 00:00:00 &ensp; Estimated time: 00:00:00 &ensp; Speed: ' + GetContentLengthString(0) + '/s');
            fileNumber = 0;
            fileName = "";
        }
        function UpdateProgressStatus(uploadedLength, totalLength) {
            var currentDate = new Date();
            var elapsedDateMilliseconds = currentDate - startDate;
            var speed = uploadedLength / (elapsedDateMilliseconds / 1000);
            var elapsedDate = new Date(elapsedDateMilliseconds);
            var elapsedTime = GetTimeString(elapsedDate);
            var estimatedMilliseconds = Math.floor((totalLength - uploadedLength) / speed) * 1000;
            var estimatedDate = new Date(estimatedMilliseconds);
            var estimatedTime = GetTimeString(estimatedDate);
            var speed = uploadedLength / (elapsedDateMilliseconds / 1000);
            lblProgressStatus.SetText('Elapsed time: ' + elapsedTime + ' &ensp; Estimated time: ' + estimatedTime + ' &ensp; Speed: ' + GetContentLengthString(speed) + '/s');
        }
        function GetContentLengthString(contentLength) {
            var sizeDimensions = ['bytes', 'KB', 'MB', 'GB', 'TB'];
            var index = 0;
            var length = contentLength;
            var postfix = sizeDimensions[index];
            while (length > 1024) {
                length = length / 1024;
                postfix = sizeDimensions[++index];
            }
            var numberRegExpPattern = /[-+]?[0-9]*(?:\.|\,)[0-9]{0,2}|[0-9]{0,2}/;
            var results = numberRegExpPattern.exec(length);
            length = results ? results[0] : Math.floor(length);
            return length.toString() + ' ' + postfix;
        }
        function GetTimeString(date) {
            var timeRegExpPattern = /\d{1,2}:\d{1,2}:\d{1,2}/;
            var results = timeRegExpPattern.exec(date.toUTCString());
            return results ? results[0] : "00:00:00";
        }
    </script>


    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono">
                <i class="la la-file-excel-o d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i>
            </div>
            <div id="divtitulo">
                <h3>Cargues<br /></h3>
            </div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i>Inicio</a></li>
                <li class="breadcrumb-item active">Cargue de Gastos Laborales</li>
            </ol>
        </div>
        <div id="SubTitulos" class="titulo2">
            <h6>Cargue de Gastos Laborales<br /></h6>
        </div>
        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbSubcategoria" DropDownStyle="DropDownList" CssClass="form-control labelinterno" ToolTip="Seleccione Subcategoria" NullText="Seleccione Subcategoria" />
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" Width="100%"
                        CssClass="form-control"
                        NullText="Haga clic aquí para ver los archivos..." UploadMode="Advanced" AutoStartUpload="True"
                        OnFilesUploadComplete="UploadControl_FilesUploadComplete" BrowseButton-Text="Examinar">
                        <AdvancedModeSettings EnableMultiSelect="False" EnableDragAndDrop="True" />
                        <ValidationSettings MaxFileSize="5000000" ShowErrors="false">
                        </ValidationSettings>
                        <ClientSideEvents FilesUploadStart="function(s, e) { UploadControl_OnFileUploadStart(); }"
                            FilesUploadComplete="function(s, e) { UploadControl_OnFilesUploadComplete(e); gvPpto.PerformCallback();} "
                            UploadingProgressChanged="function(s, e) { UploadControl_OnUploadingProgressChanged(e); }" />
                    </dx:ASPxUploadControl>
                    <p class="Note">
                        <br /><b>Nota</b>: 
                        <br />Tamaño límite del archivo a cargar es de 5 MB.
                        <br />Llenar campos vacíos con Cero (0) 
                    </p>
                </div>
            </div>
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar boton-formulariosintprim" >
            </dx:ASPxButton>
            <dx:ASPxButton ID="btnPlantilla" OnClick="btnTemplate_Click" runat="server" AutoPostBack="False" Text="Plantilla" ClientInstanceName="btnPlantilla" CssClass="boton-formulariosintsec download">
                <ClientSideEvents Click="function(s, e) { UploadControl.Cancel(); return false; }" />
            </dx:ASPxButton>
            <br/>
            <br/>
            <br/>
        </div>
        <dx:ASPxGridView ID="gvPpto" runat="server" AutoGenerateColumns="False" CssClass="gridview"
             Width ="100%"
            ClientInstanceName="gvPpto" EnableRowsCache="False" KeyFieldName="carl_consecutivo">
            <Settings GridLines="Horizontal" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataColumn Caption="Consecutivo" FieldName="carl_consecutivo" VisibleIndex="0" Visible="false" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Periodo" FieldName="carl_periodo" VisibleIndex="1" Visible="false" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Categoría" FieldName="carl_categoria" VisibleIndex="2" Visible="false" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Subcategoría" FieldName="carl_subcategoria" VisibleIndex="3" Visible="false" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Empresa" FieldName="carl_empresa" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Centro de Costos" FieldName="carl_ccostos" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Producto" FieldName="carl_producto" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Item" FieldName="carl_item" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains"/>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Valor" FieldName="carl_valor" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Moneda" FieldName="carl_moneda" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Observaciones" FieldName="carl_observaciones" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
            </Columns>
        </dx:ASPxGridView>
        <div>
            <uc1:VentanaValidaciones ID="VentanaValidaciones1" runat="server" />
        </div>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="pcProgress" Modal="True" HeaderText="Uploading"
            PopupAnimationType="None" CloseAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="460px"
            AllowDragging="true" ShowPageScrollbarWhenModal="True" ShowCloseButton="False" ShowFooter="True">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">
                                <div style="overflow: hidden; width: 280px;">
                                    <dx:ASPxLabel ID="lblFileName" runat="server" ClientInstanceName="lblFileName" Text=""
                                        Wrap="False">
                                    </dx:ASPxLabel>
                                </div>
                            </td>
                            <td class="NoWrap" style="text-align: right">
                                <dx:ASPxLabel ID="lblCurrentUploadedFileLength" runat="server" ClientInstanceName="lblCurrentUploadedFileLength"
                                    Text="" Wrap="False">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="TopPadding">
                                <dx:ASPxProgressBar ID="ASPxProgressBar1" runat="server" Height="21px" Width="100%"
                                    ClientInstanceName="progress1">
                                </dx:ASPxProgressBar>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="Spacer" style="height: 12px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <dx:ASPxLabel ID="lblUploadedFiles" runat="server" ClientInstanceName="lblUploadedFiles" Text=""
                                    Wrap="False">
                                </dx:ASPxLabel>
                            </td>
                            <td class="NoWrap" style="text-align: right">
                                <dx:ASPxLabel ID="lblUploadedFileLength" runat="server" ClientInstanceName="lblUploadedFileLength"
                                    Text="" Wrap="False">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="TopPadding">
                                <dx:ASPxProgressBar ID="ASPxProgressBar2" runat="server" CssClass="BottomMargin" Height="21px" Width="100%"
                                    ClientInstanceName="progress2">
                                </dx:ASPxProgressBar>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="Spacer" style="height: 12px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <dx:ASPxLabel ID="lblProgressStatus" runat="server" ClientInstanceName="lblProgressStatus" Text=""
                                    Wrap="False">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <FooterTemplate>
                <div style="overflow: hidden;">
                    <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" Text="Cancel" ClientInstanceName="btnCancel" Width="100px" Style="float: right">
                        <ClientSideEvents Click="function(s, e) { UploadControl.Cancel(); }" />
                    </dx:ASPxButton>
                </div>
            </FooterTemplate>
            <FooterStyle>
            <Paddings Padding="5px" PaddingRight="10px" />
            </FooterStyle>
        </dx:ASPxPopupControl>
    </div>
</asp:Content>
