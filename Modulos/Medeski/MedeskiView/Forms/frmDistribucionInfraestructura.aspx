<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="frmDistribucionInfraestructura.aspx.cs" Inherits="MedeskiView.Forms.frmDistribucionInfraestructura" %>
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


    </script> 

     <div id="ContenidoDer" class="container-fluid" onclick="cerrarMenu()" >
        <div id="Titulos" class="titulo1">
            <div id="divicono"><i class="la la-briefcase d-inline-block justify-content-between align-items-center align-content-center" style="font-size:38px;padding-top:5px;"></i></div>
            <div id="divtitulo">
				<h3>Cuadro de Servicios<br /></h3>
			</div>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="../Default.aspx"><i class="fa fa-dashboard"></i> Inicio</a></li>
                <li class="breadcrumb-item active">Distribución Productos Infraestructura</li>
            </ol>
        </div>
         
        <div id="SubTitulos" class="titulo2">
		    <h6>Distribución Productos Infraestructura<br /></h6>
	    </div>
        
        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Limpiar" CssClass="limpiar boton-formulariosintsec" OnClick="btnNuevo_Click"></dx:ASPxButton>
        <br/><br/>

        <div id="ContenedorFormulario" class="">
            <div class="form-row">
                <div class="form-group col-md-12">
                    <dx:ASPxComboBox runat="server" ID="cmbProducto" ClientInstanceName="cmbProducto" DropDownStyle="DropDownList" CssClass="form-control" ToolTip="Seleccionar Producto" OnSelectedIndexChanged="cmbProducto_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>              
        
            <div>
                <uc1:VentanaValidaciones runat="server" ID="VentanaValidaciones" />
            </div>

            <div class="form-row">
                <div class="form-group col-md-6">
                    <dx:ASPxGridView runat="server" ID="gridItems" AutoGenerateColumns="False" Theme="MetropolisBlue" 
                        Width="100%"
                        CssClass="gridview"
                        ClientInstanceName="gridItems" 
                        EnableRowsCache="False" 
                        EnableCallBacks="False" 
                        KeyFieldName="prit_consecutivo"
                        OnCustomColumnDisplayText="gridItems_CustomColumnDisplayText"
                        OnCustomButtonCallback="gridItems_CustomButtonCallback">
                        <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                        <SettingsBehavior AllowFocusedRow="True" />                    
                        <Settings GridLines="Horizontal" />                                        
                        <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />

                        <Columns>
                            <dx:GridViewDataTextColumn Caption="prit_consecutivo" FieldName="prit_consecutivo" VisibleIndex="0" Visible="false" />                        
                            <dx:GridViewDataTextColumn Caption="Item" FieldName="prit_item" VisibleIndex="1" Width="75%" />
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
                            ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="dinf_consecutivo"
                            OnCustomButtonCallback="grid_CustomButtonCallback" OnDataBinding="grid_DataBinding" OnRowUpdating="grid_RowUpdating">
                        <Styles Header-CssClass="gridview-header" FilterRow-CssClass="gridview-filtro" />
                        <Settings GridLines="Horizontal" />
                        <ClientSideEvents CustomButtonClick="function(s, e) {e.processOnServer = true;}" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="dinf_consecutivo" FieldName="dinf_consecutivo" VisibleIndex="0" Visible="false" />
                            <dx:GridViewDataTextColumn Caption="dinf_servidor" FieldName="dinf_servidor" VisibleIndex="1" Visible="false" />
                            <dx:GridViewDataTextColumn Caption="Servidor" EditFormSettings-Visible="False" FieldName="GE_TSERVIDORES.serv_nombre" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn Caption="Distribución" FieldName="dinf_valor">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataCheckColumn>                
                        </Columns>
                        <Settings ShowStatusBar="Hidden"/>
                        <SettingsEditing Mode="Batch" />

            
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Settings VerticalScrollableHeight="315" />

                    </dx:ASPxGridView>
                </div>
            </div>
            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" OnClick="GuardarClicked" CssClass="guardar boton-formulariosintprim" AutoPostBack="False" Enabled="false">
                <ClientSideEvents Click="function(s, e) { OnUpdateClick(); e.processOnServer = true;}" />
            </dx:ASPxButton>
            <br /><br />
            <br /><br />     
        </div>          
    </div>
</asp:Content>
