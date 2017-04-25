<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Presentacion.Inicio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:textbox runat="server" ID="clave" Visible="false"></asp:textbox>

    <script src="js/push.min.js"></script>
    
    <script>//Scrip de notificaciones
        //int es el unico que no usa ""
        var a = <%=option%>;
        var m =  "<%=msg%>";
        if (a == 1) {
            Push.create("Información", {
                body: m,
                icon: 'img/informacion.png',
                timeout: 120000,
                onClick: function () {
                    window.focus();
                    this.close();
                }
            });
        }
    </script>
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Destacados</h1>
        </div>
     <title>W3.CSS</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
<body>

<div class="w3-content w3-section" style="max-width:1000px" >
  <img class="mySlides w3-animate-fading" src="Slides/foto 1.jpg" style="width:100%">
  <img class="mySlides w3-animate-fading" src="Slides/foto 2.jpg" style="width:100%">
  <img class="mySlides w3-animate-fading" src="Slides/foto 3.jpg" style="width:100%">
</div>

<script>
var myIndex = 0;
carousel();

function carousel() {
    var i;
    var x = document.getElementsByClassName("mySlides");
    for (i = 0; i < x.length; i++) {
       x[i].style.display = "none";  
    }
    myIndex++;
    if (myIndex > x.length) {myIndex = 1}    
    x[myIndex-1].style.display = "block";  
    setTimeout(carousel, 9000);    
}
</script>
    </div>
</asp:Content>
