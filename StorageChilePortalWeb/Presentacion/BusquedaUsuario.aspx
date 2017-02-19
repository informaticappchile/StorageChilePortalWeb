<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BusquedaUsuario.aspx.cs" Inherits="Presentacion.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <span class="mdl-list__item-primary-content">
        <i class="material-icons  mdl-list__item-avatar">person</i>
        <span class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
            <asp:TextBox ID="usuario_buscar" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
            <label class="mdl-textfield__label" for="userpass-login-input">Usuario</label>
        </span>
    </span>
    <div class="mdl-card__actions mdl-card--border">
        <asp:Button ID="BusquedaUsuario" runat="server" Text="Buscar Usuario" OnClick="Button_Buscra_Click" CssClass="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect" />
    </div>


    <div class="demo-card-wide mdl-card mdl-shadow--2dp">
        <div class="mdl-card__title">
            <h1 class="mdl-card__title-text">Archivos</h1>
        </div>
        <asp:GridView ID="GridViewMostrarArchivosUsuario" runat="server" AutoGenerateColumns="False" CssClass="mdl-data-table mdl-js-data-table mdl-shadow--2dp listado-archivos">
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
