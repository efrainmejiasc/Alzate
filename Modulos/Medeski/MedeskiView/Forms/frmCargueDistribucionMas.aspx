<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmCargueDistribucionMas.aspx.cs" Inherits="MedeskiView.Forms.frmCargueDistribucionMas" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var fileNumber = 0;
        var fileName = "";
        var startDate = null;

        function OnFileUploadComplete(s, e) {
            grid_gente.PerformCallback();
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
        function actualizarProgreso() {
            cbpProgreso.PerformCallback();
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
                  <li class="breadcrumb-item active">Cargue Distribucion Personas</li>
              </ol>
          </div>
          <div id="SubTitulos" class="titulo2">
              <h6>Cargue Distribucion Personas<br /></h6>
          </div>
          <div id="ContenedorFormulario" class="">
        
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
                          <br /><b>&nbsp;&nbsp;&nbsp;Nota</b>: 
                          <br/>&nbsp;&nbsp;&nbsp;Tamaño límite del archivo a cargar es de 5 MB.
                          <br/>&nbsp;&nbsp;&nbsp;Llenar campos vacíos con Cero (0) 
                      </p>
                  </div>
              </div>
              <dx:ASPxCallbackPanel ID="cbpProgreso" ClientInstanceName="cbpProgreso" runat="server">
                  <PanelCollection>
                      <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                          <div class="grid-16 alpha">
                              <dx:ASPxButton ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="guardar boton-formulariosintprim" runat="server" Text="Guardar">
                              </dx:ASPxButton>
                              <dx:ASPxButton ID="btnPlantilla" OnClick="btnTemplate_Click" runat="server" AutoPostBack="False" Text="Plantilla" ClientInstanceName="btnPlantilla" CssClass="download boton-formulariosintsec">
                                  <ClientSideEvents Click="function(s, e) { UploadControl.Cancel(); }" />
                              </dx:ASPxButton>
                          </div>
                          <div>
                              <uc1:VentanaValidaciones ID="VentanaValidaciones" runat="server" />
                          </div>
                      </dx:PanelContent>
                  </PanelCollection>
              </dx:ASPxCallbackPanel>
              <div>
                  <uc1:VentanaValidaciones ID="VentanaValidaciones1" runat="server" />
              </div>
              <dx:ASPxGridView ID="gvPpto" runat="server" AutoGenerateColumns="False" CssClass="gridview" Width="100%" ClientInstanceName="gvPpto" EnableRowsCache="False">
                  <Settings GridLines="Horizontal" />
                  <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                  <Columns>
                      <dx:GridViewDataColumn Caption="Producto Directo" FieldName="ProductoDirecto" VisibleIndex="0" Visible="true" HeaderStyle-HorizontalAlign="Left" > </dx:GridViewDataColumn>

                      <dx:GridViewDataColumn Caption="Codigo Producto" FieldName="CodigoProducto" VisibleIndex="1" Visible="true" HeaderStyle-HorizontalAlign="Left" > </dx:GridViewDataColumn>

                      <dx:GridViewDataColumn Caption="Cantidad" FieldName ="Cantidad" VisibleIndex="2" Visible="true" HeaderStyle-HorizontalAlign="Left"> 
                          <FooterTemplate>
                        Total = 
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="labelSum" Text='<%# GetSumatoriaMas() %>'>
                        </dx:ASPxLabel>
                    </FooterTemplate>
                      </dx:GridViewDataColumn>

                      <dx:GridViewDataColumn Caption="Porcentaje" FieldName ="Porcentaje" VisibleIndex="3" Visible="true" HeaderStyle-HorizontalAlign="Left"> </dx:GridViewDataColumn>
  
                      <dx:GridViewDataColumn Caption="Observacion" FieldName ="Observacion" VisibleIndex="4" Visible="true" HeaderStyle-HorizontalAlign="Left"> </dx:GridViewDataColumn>

                       <dx:GridViewDataColumn Caption="Usuario" FieldName ="Usuario"  Visible="false" HeaderStyle-HorizontalAlign="Left"> </dx:GridViewDataColumn>
  
                      <dx:GridViewDataColumn Caption="Fecha" FieldName ="Fecha"  Visible="false" HeaderStyle-HorizontalAlign="Left"> </dx:GridViewDataColumn>


                  </Columns>
              </dx:ASPxGridView>
              <div class="grid-4">
                  <dx:ASPxPopupControl ID="ASPxPopupControl" runat="server" ClientInstanceName="pcProgress" Modal="True" HeaderText="Uploading"
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
                  <br />
                  <br />
              </div>
          </div>
    </div>

</asp:Content>

