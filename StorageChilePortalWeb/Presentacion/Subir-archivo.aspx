<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Subir-archivo.aspx.cs" Inherits="Prensentacion.SubirArchivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Subir archivo</h1>
        </div>
        <div>
            <div class="text-escoge">
                <label>Escoge el archivo a subir:</label>
            </div>
            <div class="mdl-card__actions mdl-card--border">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="mdl-button mdl-js-button mdl-button--primary"/>
                <br />
                <asp:LinkButton ID="Button1" runat="server" OnClick="Button_Upload_Click" CssClass="mdl-button mdl-js-button mdl-button--primary">
                <i class="material-icons">publish</i>
                Subir
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
