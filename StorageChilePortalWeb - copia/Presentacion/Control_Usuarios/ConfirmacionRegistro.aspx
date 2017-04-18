<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ConfirmacionRegistro.aspx.cs" Inherits="Presentacion.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Confirmación</h1>
        </div>
        <style>
            #contenedor{
                align-content:center;
            }
        </style>
        <div id="contenedor">
            <asp:Label ID="mensaje" runat="server" ForeColor="Black"></asp:Label>
            <asp:TextBox id="tbValidacion" runat="server" Visible="false"></asp:TextBox>
        </div>
    </div>
</asp:Content>
