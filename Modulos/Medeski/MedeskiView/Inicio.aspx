<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="MedeskiView.Forms.inicio" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"/>
    <title>Medeski</title>

    <link rel="shortcut icon" href="assets/img/favicon.png" type="image/x-icon" />
    
    <link type="text/css" href="../assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="../assets/fonts/font-awesome.min.css" rel="stylesheet" />
    <link type="text/css" href="../assets/fonts/ionicons.min.css" rel="stylesheet" />
    <link type="text/css" href="../assets/fonts/line-awesome.min.css" rel="stylesheet" />
    <link type="text/css" href="../assets/css/form.css" rel="stylesheet" />        
    <link type="text/css" href="../assets/css/Sidebar-Menu-SIM.css" rel="stylesheet" />
    <link type="text/css" href="../assets/css/styles.css" rel="stylesheet" />

    <link rel="stylesheet" type="text/css" href="assets/js/sweet-alert/sweetalert.css" media="all" />
    
    <script type="text/javascript" src="../assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../assets/bootstrap/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/js/cuantificador.js"></script>
    
    <script type="text/javascript" src="../assets/js/Sidebar-Menu.js"></script>    
    <script src="../assets/js/sweet-alert/sweetalert-dev.js"></script>
    <script src="../assets/js/sweet-alert/mensajes.js"></script>


</head>
<body id="cuerpo">
    <div id="zonatransparencia">
        <div id="SeccionLogin">
            <form id="form1" runat="server" class="alingform">

                <div id="logo"></div>
                
                <div class="grupo">
                    <asp:TextBox ID="username" runat="server" required="" CssClass="input_login" autocomplete="off"></asp:TextBox>
                    <span class="highlight"></span>
					<span class="bar"></span>
                    <label>Usuario</label>
				</div>

                <div class="grupo">      
					<asp:TextBox ID="password" runat="server" required="" type="password" CssClass="input_login" ></asp:TextBox>
					<span class="highlight"></span>
					<span class="bar"></span>
                    <label>Contraseña</label>
				</div>
                
                <div id ="alineacionboton">
                    <asp:Button ID="submit" runat="server" Text="Ingresar" onclick="submit_Click" CssClass="boton-formulariologin"/>
                </div>

                <asp:TextBox ID="dominio" runat="server" placeholder="Dominio"  required="" CssClass="form-control" Visible="false"></asp:TextBox>
                <p class="bg-warning"> <asp:Label ID="lbTexto" runat="server" Font-Size="Medium"></asp:Label> </p>                    
            </form>

        </div>
    </div>
    
    <script type="text/javascript" src="../assets/js/random.js"></script>
     
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="true"
       PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CssClass="loaderPanel" LoadingDivStyle-BackColor="#5e5b5b">
       <Template>
           <div>
               <div class="loader"></div>
           </div>
       </Template>
   </dx:ASPxLoadingPanel>

</body>
</html>
