<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCarguePyG.aspx.cs" Inherits="MedeskiView.Forms.frmCarguePyG" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" >
        var fileNumber = 0;
        var fileName = "";
        var startDate = null;

        function OnFileUploadComplete(s, e) {
            grid_Driver.PerformCallback();
        }

        function UploadControl_OnFileUploadStart() {
            startDate = new Date();
            ClearProgressInfo();
            pcProgress.Show();
        }
        function UploadControl_OnFilesUploadComplete(e) {
            pcProgress.Hide();
            if (e.errorText) {
                ShowMessage(e.errorText);
            }
            else if (e.callbackData == "success") {
                ShowMessage("El archivo se cargó satisfactoriamente.");
            }
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
        function actualizarProgreso() {
            cbpProgreso.PerformCallback();
        }
    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-pie-chart d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cargue PyG<br /></h3>
			</div>
        </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxUploadControl ID="UploadControl" runat="server" ClientInstanceName="UploadControl" Width="100%"
                        CssClass="form-control"
                        NullText="Haga clic aquí para ver los archivos..." UploadMode="Advanced" AutoStartUpload="True"
                        OnFilesUploadComplete="UploadControl_FilesUploadComplete" BrowseButton-Text="Examinar">
                        <AdvancedModeSettings EnableMultiSelect="False" EnableDragAndDrop="True" />
                        <ValidationSettings MaxFileSize="5000000" ShowErrors="false"></ValidationSettings>
                        
                        <ClientSideEvents FilesUploadStart="function(s, e) { UploadControl_OnFileUploadStart(); }"
                            FilesUploadComplete="function(s, e) { UploadControl_OnFilesUploadComplete(e); grid_Driver.PerformCallback(); actualizarProgreso();} "
                            UploadingProgressChanged="function(s, e) { UploadControl_OnUploadingProgressChanged(e); }" />
                                                
                    </dx:ASPxUploadControl>
                    <p class="Note">
                        <b>Nota</b>: 
                        <br />Tamaño límite del archivo a cargar es de 5 MB.
                        <br />Archivo en format XLSX.
                    </p>
                    <dx:ASPxTextBox runat="server" Width="100%" Height="150" ID="txtPendientes" ReadOnly="true" ClientInstanceName="txtPendientes" Visible="false" BackColor="#fcfcfc"></dx:ASPxTextBox>
                        
                </div>                
            </div>
            
                <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"  CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>
                <dx:ASPxButton ID="btnPlantilla" OnClick="btnTemplate_Click" runat="server" AutoPostBack="False" Text="Descargar Plantilla" ClientInstanceName="btnPlantilla" CssClass="boton-formulariosintsec">
                    <ClientSideEvents Click="function(s, e) { UploadControl.Cancel(); }" />
                </dx:ASPxButton>                
            <br/><br/>
            <br/><br/>
                
        </div>
        
        <dx:ASPxCallbackPanel ID="cbpProgreso" ClientInstanceName="cbpProgreso" runat="server">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">                    
                    <div>
                        <uc1:VentanaValidaciones ID="VentanaValidaciones" runat="server" />
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        
        <dx:ASPxGridView ID="grid_Driver" ClientInstanceName="grid_Driver" runat="server"
            AutoGenerateColumns="False" Theme="MetropolisBlue"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="ceop_codigo" Width="100%"
            OnDataBinding="grid_DataBinding" 
            CssClass="gridview"
            OnRowUpdating="grid_RowUpdating">
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Settings GridLines="Horizontal" />
            <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
            <Columns>
                <dx:GridViewDataColumn FieldName="ceop_codigo" Caption="CEOP" VisibleIndex="1" ReadOnly="true">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>

                <%-- Forecast --%>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" FieldName="forecast_ventas" Caption="Forecast Ventas" VisibleIndex="2">
                        <Settings AutoFilterCondition="Contains" />  
                        <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Forecast Ventas = 
                        <dx:ASPxLabel ID="labelForecastVentas" runat="server" ClientInstanceName="labelForecastVentas" Text=''>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" FieldName="forecast_directos" Caption="Forecast Directos" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Forecast Directos = 
                        <dx:ASPxLabel ID="labelForecastDirectos" runat="server" ClientInstanceName="labelForecastDirectos" Text=''>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" FieldName="forecast_indirectos" Caption="Forecast Indirectos" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Forecast Indirectos = 
                        <dx:ASPxLabel ID="labelForecastIndirectos" runat="server" ClientInstanceName="labelForecastIndirectos" Text=''>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                
                <%-- PPTO --%>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" FieldName="ventas" Caption="Ventas" VisibleIndex="5">
                        <Settings AutoFilterCondition="Contains" />  
                        <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Ventas = 
                        <dx:ASPxLabel ID="labelVentas" runat="server" ClientInstanceName="labelVentas" Text=''>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" FieldName="directos" Caption="Directos" VisibleIndex="6">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Directos = 
                        <dx:ASPxLabel ID="labelDirectos" runat="server" ClientInstanceName="labelDirectos" Text=''>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Left" FieldName="indirectos" Caption="Indirectos" VisibleIndex="7 ">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                    <FooterTemplate>
                        Total Indirectos = 
                        <dx:ASPxLabel ID="labelIndirectos" runat="server" ClientInstanceName="labelIndirectos" Text=''>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>

            </Columns>  
            <Settings ShowStatusBar="Hidden"/>
            <SettingsEditing Mode="Batch" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Settings VerticalScrollableHeight="350" />

            <Settings ShowFooter="True" />
                <TotalSummary>
                    
                    <dx:ASPxSummaryItem FieldName="forecast_ventas" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Forecast_Ventas"/>
                    <dx:ASPxSummaryItem FieldName="forecast_directos" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Forecast_Directos"/>
                    <dx:ASPxSummaryItem FieldName="forecast_indirectos" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Forecast_Indirectos"/>

                    <dx:ASPxSummaryItem FieldName="ventas" DisplayFormat="Total: {0}" SummaryType="Sum" Tag="Ventas"/>
                    <dx:ASPxSummaryItem FieldName="directos" DisplayFormat="Total: {0:0}" SummaryType="Sum" Tag="Directos"/>
                    <dx:ASPxSummaryItem FieldName="indirectos"  DisplayFormat="Total: {0:0}" SummaryType="Sum" Tag="Indirectos"/>

                </TotalSummary>    
            <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" BatchEditStartEditing="OnBatchEditStartEditing" />
        </dx:ASPxGridView>
                
        <br /><br />
        <dx:ASPxGridView ID="gridPendientes" ClientInstanceName="gridPendientes" runat="server"
            Theme="MetropolisBlue" Visible="false"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="dto_generic_id_consecutivo" Width="100%">
            <Settings GridLines="Horizontal" />            
            <Columns>
                <dx:GridViewDataColumn FieldName="dto_generic_id_consecutivo" Caption="Consecutivo" Visible="false" VisibleIndex="1" />
                <dx:GridViewDataColumn FieldName="dto_generic_productos" Caption="Producto" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_observaciones" Caption="Observaciones" VisibleIndex="3" />
            </Columns>
        </dx:ASPxGridView>

        <dx:ASPxPopupControl ID="pcProgress" runat="server" ClientInstanceName="pcProgress" Modal="True" HeaderText="Uploading"
            PopupAnimationType="None" CloseAction="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="460px"
            AllowDragging="true" ShowPageScrollbarWhenModal="True" ShowCloseButton="False" ShowFooter="True">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server" SupportsDisabledAttribute="True">
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
                                <div class="Spacer" style="height: 12px;"></div>
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
                                <div class="Spacer" style="height: 12px;"></div>
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
