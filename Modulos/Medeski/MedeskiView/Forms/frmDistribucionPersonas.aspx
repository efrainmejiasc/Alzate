<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionPersonas.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionPersonas" %>

<%@ Register Src="~/UserControl/VentanaValidaciones.ascx" TagPrefix="uc1" TagName="VentanaValidaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function OnUpdateClick(s, e) {
            try {

                if (grid.batchEditApi.HasChanges()) {
                    grid.UpdateEdit();
                }
            }
            catch (err) {
            }
        }

        function OnBatchEditEndEditing(s, e) {
            var retorno = CalculateSummary(s, e.rowValues, e.visibleIndex, false);

            if (retorno > 0) {
                window.setTimeout(function () {
                    s.batchEditApi.SetCellValue(e.visibleIndex, "vlrDistribucion", retorno, null, true);
                }, 0);
            }
            else
            {
                window.setTimeout(function () {
                    s.batchEditApi.SetCellValue(e.visibleIndex, "vlrDistribucion", retorno, null, true);
                }, 0);

            }
        }

        function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
            var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "vlrDistribucion");
            var newValue = rowValues[(grid.GetColumnByField("vlrDistribucion").index)].value;
            var dif = isDeleting ? -newValue : newValue - originalValue;
            var retorno;
           // var dif1 = dif * 100;
            var dif1 = dif

            if ((parseFloat(labelSum.GetValue()) + dif1) <= 100) {
                labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif1).toFixed(1));
                console.log(newValue);
                console.log(originalValue);
                $('#ctl00_ctl00_MainPane_Content_MainContent_grid_tcfooter2').val(newValue);
                retorno = newValue;
            }
            else {
                alert("La suma de los porcentajes no puede ser mayor a 100");
                retorno = 0;

            }

            return retorno;
        }
    </script>

    <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-briefcase d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro de Servicios<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Distribución de Personas</li>
            </ol>
        </div>
        
        <div id="SubTitulos" class="titulo2">
		    <h6>Distribución de Personas<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click" ></dx:ASPxButton>
        <br /><br />

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbCriterioDistribucion" ClientInstanceName="cmbCriterioDistribucion" DropDownStyle="DropDownList" CssClass="form-control" 
                            ToolTip="Seleccionar Criterio Distribución" OnSelectedIndexChanged="cmbCriterioDistribucion_SelectedIndexChanged"  AutoPostBack="true"/>
                </div>
                <div class="form-group col-md-6">
                    <dx:ASPxComboBox runat="server" ID="cmbAreas" ClientInstanceName="cmbAreas" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Area"
                            OnSelectedIndexChanged="cmbAreas_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>
     
        </div>
        
        <div>                        
            <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <dx:ASPxGridView runat="server" ID="gridPersonas" AutoGenerateColumns="False" Theme="MetropolisBlue" 
                        Width="100%"
                        CssClass="gridview"
                        ClientInstanceName="gridItems" 
                        EnableRowsCache="False" 
                        EnableCallBacks="False" 
                        KeyFieldName="gent_persona"
                        OnCustomButtonCallback="gridPersonas_CustomButtonCallback">
                        <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                        <SettingsBehavior AllowFocusedRow="True" />                    
                        <Settings GridLines="Horizontal" />                                        
                        <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />

                        <Columns>
                            <dx:GridViewDataTextColumn Caption="gent_persona" FieldName="gent_persona" VisibleIndex="0" Visible="false" />                        
                            <dx:GridViewDataTextColumn Caption="Colaborador" FieldName="GE_TPERSONAS.pers_nombres" VisibleIndex="1" Width="75%" />
                            <dx:GridViewCommandColumn VisibleIndex="8" Caption="Acción">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text=" ">
                                        <Image ToolTip="Seleccionar" Url="../Content/Imagenes/seleccionar.png"/>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                                            
                    </dx:ASPxGridView>
            </div>
            <div class="form-group col-md-6">
                <dx:ASPxGridView runat="server" ID="grid" AutoGenerateColumns="False" Theme="MetropolisBlue" CssClass="gridview"
                    Width="100%"
                    ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="consecutivo" 
                    OnRowUpdating="grid_RowUpdating" OnBatchUpdate="grid_BatchUpdate">
                    <Settings GridLines="Horizontal" />
                    <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" RowClick="function(s, e) {e.processOnServer = true;}"/>
                    <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Consecutivo" FieldName="consecutivo" VisibleIndex="0" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="Descripción" EditFormSettings-Visible="False" FieldName="descripcion" VisibleIndex="1" Visible="true" ReadOnly="true">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                                    
                        <dx:GridViewDataSpinEditColumn CellStyle-CssClass="cell-edit" Caption="Porcentaje de Distribución" FieldName="vlrDistribucion" VisibleIndex="2" PropertiesSpinEdit-AllowMouseWheel="false"
                            PropertiesSpinEdit-AllowNull="true" PropertiesSpinEdit-MaxValue="100" PropertiesSpinEdit-MinValue="0" PropertiesSpinEdit-NumberType="Float"
                            PropertiesSpinEdit-DisplayFormatString="N2" PropertiesSpinEdit-SpinButtons-Enabled="false">
                                        
                            <PropertiesSpinEdit DisplayFormatString="N2" NumberFormat="Custom" MinValue="0" MaxValue="100" NumberType="Float" AllowMouseWheel="False">
                                <SpinButtons Enabled="False"> </SpinButtons>
                            </PropertiesSpinEdit>
                                        
                            <Settings AutoFilterCondition="BeginsWith"/>
                            <FooterTemplate>
                                TPorcentaje % = 
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="labelSum" Text='<%# string.Format("{0:N2}",GetTotalSummaryValue()) %>'>
                            </dx:ASPxLabel>
                            </FooterTemplate>
                        </dx:GridViewDataSpinEditColumn>
                    </Columns>
                    <Settings ShowStatusBar="Hidden" />
                    <SettingsEditing Mode="Batch" />
                    <Settings ShowFooter="true" />

                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                    <Settings VerticalScrollableHeight="285" />

                    <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="vlrDistribucion" Tag="TPorcentaje" DisplayFormat="N2" />
                    </TotalSummary>
                    <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing"/>
                </dx:ASPxGridView>
            </div>
        </div>
        <dx:ASPxHiddenField ID="txtId" runat="server" ClientInstanceName="txtId"></dx:ASPxHiddenField>
        <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="guardar boton-formulariosintprim" AutoPostBack="False" Enabled="false">
            <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = true;}" />
        </dx:ASPxButton>
        <br /><br />
        <br /><br />
    </div>                    
</asp:Content>
