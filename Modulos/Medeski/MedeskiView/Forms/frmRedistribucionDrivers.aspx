<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmRedistribucionDrivers.aspx.cs" Inherits="MedeskiView.Forms.frmRedistribucionDriver" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
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
            <div id="divicono"><i class="la la-retweet d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro Redistribución<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Cuadro de Redistribución de Drivers</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Cuadro de Redistribución de Drivers<br /></h6>
	    </div>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <p class="Note">
                        <b>Nota</b>: 
                        <br />Puede presione sobre el botón <strong>Guardar</strong> para recalcular y guardar los valores.
                    </p>
                </div>
            </div>
        
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar boton-formulariosintprim"></dx:ASPxButton>
        <br /><br /><br />
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
            CssClass="gridview"
            EnableRowsCache="False" EnableCallBacks="False"
            KeyFieldName="dto_generic_empresa" Width="100%">
            <Settings GridLines="Horizontal" />
            <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
            <Columns>
                <dx:GridViewDataColumn FieldName="dto_generic_productos" Caption="Productos" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_empresa" Caption="Empresa" VisibleIndex="2">
                    <Settings AutoFilterCondition="BeginsWith" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_sede" Caption="Sede" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_centro_operacion" Caption="C.O" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_coperaciones" Caption="Descripción C.O" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_ccostos" Caption="C.C" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="dto_generic_cantidad" Caption="Cantidad" VisibleIndex="7">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor" Caption="Valor" VisibleIndex="8">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor_distribuidos" Caption="Valor Distrib" VisibleIndex="9">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor_adicional" Caption="Valor Adicional" VisibleIndex="10">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="dto_generic_valor_suma" Caption="Valor Redistribuido" VisibleIndex="11">
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="C0" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowGroupPanel="True" />
        </dx:ASPxGridView>
        
    </div>             
</asp:Content>
