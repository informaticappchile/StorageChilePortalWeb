<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Prensentacion.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .mySlides {display:none}
        .w3-left, .w3-right, .w3-badge {cursor:pointer}
        .w3-badge {height:13px;width:13px;padding:0}
    </style>
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Destacados</h1>
        </div>
        <div class="w3-content w3-display-container">
          <asp:Image runat="server" class="mySlides" ImageUrl="~/Slides/Slide1.png" Height="650" Width="1020"/>
          <asp:Image runat="server"  class="mySlides" ImageUrl="~/Slides/Slide2.jpg" Height="650" Width="1020"/>
          <asp:Image runat="server"  class="mySlides" ImageUrl="~/Slides/Slide3.jpg" Height="650" Width="1020"/>
          <div class="w3-center w3-display-bottommiddle" style="width:100%">
            <div class="w3-left" onclick="plusDivs(-1)">&#10094;</div>
            <div class="w3-right" onclick="plusDivs(1)">&#10095;</div>
            <span class="w3-badge demo w3-border" onclick="currentDiv(1)"></span>
            <span class="w3-badge demo w3-border" onclick="currentDiv(2)"></span>
            <span class="w3-badge demo w3-border" onclick="currentDiv(3)"></span>
          </div>
        </div>
        <script>
            var myIndex = 0;
            carousel();
            var slideIndex = 1;
            showDivs(slideIndex);

            function plusDivs(n) {
                showDivs(slideIndex += n);
            }

            function currentDiv(n) {
                showDivs(slideIndex = n);
            }

            function showDivs(n) {
                var i;
                var x = document.getElementsByClassName("mySlides");
                var dots = document.getElementsByClassName("demo");
                if (n > x.length) { slideIndex = 1 }
                if (n < 1) { slideIndex = x.length }
                for (i = 0; i < x.length; i++) {
                    x[i].style.display = "none";
                }
                for (i = 0; i < dots.length; i++) {
                    dots[i].className = dots[i].className.replace(" w3-white", "");
                }
                x[slideIndex - 1].style.display = "block";
                dots[slideIndex - 1].className += " w3-white";
            }

            function carousel() {
                var i;
                var x = document.getElementsByClassName("mySlides");
                for (i = 0; i < x.length; i++) {
                   x[i].style.display = "none";  
                }
                myIndex++;
                if (myIndex > x.length) {myIndex = 1}    
                x[myIndex-1].style.display = "block";  
                setTimeout(carousel, 5000); // Change image every 5 seconds
            }
        </script>
        <!--<asp:Image ID="Logo" runat="server" ImageUrl="~/Slides/Slide1.png" CssClass="slide-web"/> -->
        <asp:GridView ID="GridViewMostrarTodo" runat="server" AutoGenerateColumns="False" CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp">
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Nombre Archivo" />
                <asp:BoundField DataField="ID" HeaderText="Descripcion" />
                <asp:BoundField DataField="descripcion" HeaderText="Fecha creacion" />
                <asp:BoundField HeaderText="Propietario" />
            </Columns>
            <RowStyle CssClass="mdl-data-table__cell--non-numeric" />
        </asp:GridView>
    </div>
</asp:Content>
